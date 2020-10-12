﻿#region Using

using System;

// Owin SelfHost
using Owin;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Validation;
using System.Reflection;
using NLib;
using NLib.IO;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using DMT.Models;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// Web Server StartUp class.
    /// </summary>
    public class StartUp
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            // Controllers with Actions

            // To handle routes like `/api/controller/action`
            config.Routes.MapHttpRoute(
                name: "ControllerAndAction",
                routeTemplate: "api/{controller}/{action}"
            );

            config.Formatters.Clear();
            config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings =
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            // Replace IBodyModelValidator to Custom Model Validator to prevent
            // Insufficient Stack problem.
            config.Services.Replace(typeof(IBodyModelValidator), new CustomBodyModelValidator());

            appBuilder.UseWebApi(config);
        }
    }

    public class CustomBodyModelValidator : DefaultBodyModelValidator
    {
        public override bool ShouldValidateType(Type type)
        {
            // Ignore validation on all DMTModelBase subclasses.
            bool isDMTModel = !type.IsSubclassOf(typeof(Models.DMTModelBase));
            return isDMTModel && base.ShouldValidateType(type);
        }
    }
    /// <summary>
    /// Local Database Web Server (Self Host).
    /// </summary>
    public class LocalDatabaseWebServer
    {
        #region Internal Variables

        private string baseAddress = string.Format(@"{0}://{1}:{2}",
            ConfigManager.Instance.Plaza.Local.Http.Protocol,
            "+",
            ConfigManager.Instance.Plaza.Local.Http.PortNumber);

        private IDisposable server = null;

        private RabbitMQClient taaMQclient = null;
        private RabbitMQClient todMQclient = null;

        #endregion

        /// <summary>
        /// Gets local message json folder path name.
        /// </summary>
        static string LocalMessageFolder
        {
            get
            {
                string localFilder = Folders.Combine(
                    Folders.Assemblies.CurrentExecutingAssembly, "rabbit.mq.msgs");
                if (!Folders.Exists(localFilder))
                {
                    Folders.Create(localFilder);
                }
                return localFilder;
            }
        }
        /// <summary>
        /// Gets local TA message json folder path name.
        /// </summary>
        static string LocalTAMessageFolder
        {
            get
            {
                string localFilder = Folders.Combine(LocalMessageFolder, "TA");
                if (!Folders.Exists(localFilder))
                {
                    Folders.Create(localFilder);
                }
                return localFilder;
            }
        }
        /// <summary>
        /// Gets local TA message json folder path name.
        /// </summary>
        static string LocalTODMessageFolder
        {
            get
            {
                string localFilder = Folders.Combine(LocalMessageFolder, "TOD");
                if (!Folders.Exists(localFilder))
                {
                    Folders.Create(localFilder);
                }
                return localFilder;
            }
        }

        #region Private Methods

        private void InitOwinFirewall()
        {
            string portNum = ConfigManager.Instance.Plaza.Local.Http.PortNumber.ToString();
            string appName = "DMT TODxTA Local Service(REST)";
            var nash = new CommandLine();
            nash.Run("http add urlacl url=http://+:" + portNum + "/ user=Everyone");
            nash.Run("advfirewall firewall add rule dir=in action=allow protocol=TCP localport=" + portNum + " name=\"" + appName + "\" enable=yes profile=Any");
        }

        private void ReleaseOwinFirewall()
        {
            string portNum = ConfigManager.Instance.Plaza.Local.Http.PortNumber.ToString();
            string appName = "DMT TODxTA Local Service(REST)";
            var nash = new CommandLine();
            nash.Run("http delete urlacl url=http://+:" + portNum + "/");
            nash.Run("advfirewall firewall delete rule name=\"" + appName + "\"");
        }

        private void WriteFile(string fullFileName, string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            MethodBase med = MethodBase.GetCurrentMethod();
            // Save message.
            try
            {
                using (var stream = File.CreateText(fullFileName))
                {
                    stream.Write(message);
                    stream.Flush();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            // Update to database
            var msg = message.FromJson<Models.RabbitMQMessage>();
            if (null != msg)
            {
                if (msg.parameterName == "STAFF")
                {
                    var mq = message.FromJson<Models.RabbitMQStaffMessage>();
                    if (null != mq)
                    {
                        var staffs = Models.RabbitMQStaff.ToLocals(mq.staff);
                        if (null != staffs && staffs.Count > 0)
                        {
                            Task.Run(() => 
                            {
                                Models.User.SaveUsers(staffs);
                            });
                        }
                    }
                    else
                    {
                        med.Info("Cannot convert to STAFF message.");
                    }
                }
                else
                {
                    med.Info("message is STAFF message.");
                }
            }
            else
            {
                med.Info("message is null or cannot convert to json object.");
            }
        }

        private void WriteTAFile(string message)
        {
            // Create file.
            string fileName = "msg." + DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.ffffff") + ".txt";
            string fullFileName = Path.Combine(LocalTAMessageFolder, fileName);
            // Save message.
            WriteFile(fullFileName, message);
        }

        private void WriteTODFile(string message)
        {
            // Create file.
            string fileName = "msg." + DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.ffffff") + ".txt";
            string fullFileName = Path.Combine(LocalTODMessageFolder, fileName);
            // Save message.
            WriteFile(fullFileName, message);
        }

        private void TaaMQclient_OnMessageArrived(object sender, QueueMessageEventArgs e)
        {
            // Save message.
            WriteTAFile(e.Message);
        }

        private void TodMQclient_OnMessageArrived(object sender, QueueMessageEventArgs e)
        {
            // Save message.
            WriteTODFile(e.Message);
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            // Start database server.
            LocalDbServer.Instance.Start();

            if (null == server)
            {
                InitOwinFirewall();
                server = WebApp.Start<StartUp>(url: baseAddress);
            }

            if (null == taaMQclient)
            {
                var MQConfig = (null != ConfigManager.Instance.Plaza.TARabbitMQ) ? 
                    ConfigManager.Instance.Plaza.TARabbitMQ.RabbitMQ : null;
                if (null != MQConfig && MQConfig.Enabled)
                {
                    //WriteTAFile("init");
                    med.Info("Rabbit Host Info: " + MQConfig.GetString());
                    try
                    {
                        taaMQclient = new RabbitMQClient();
                        taaMQclient.HostName = MQConfig.HostName;
                        taaMQclient.PortNumber = MQConfig.PortNumber;
                        taaMQclient.VirtualHost = MQConfig.VirtualHost;
                        taaMQclient.UserName = MQConfig.UserName;
                        taaMQclient.Password = MQConfig.Password;
                        taaMQclient.OnMessageArrived += TaaMQclient_OnMessageArrived;
                        if (taaMQclient.Connect() && taaMQclient.Listen(MQConfig.QueueName))
                        {
                            med.Info("Success connected to : " + MQConfig.GetString());
                            med.Info("Listen to Queue: " + MQConfig.QueueName);
                        }
                        else
                        {
                            med.Err("failed connected to : " + MQConfig.HostName);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        med.Err(ex);
                    }
                }
            }
            if (null == todMQclient)
            {
                var MQConfig = (null != ConfigManager.Instance.Plaza.TODRabbitMQ) ?
                    ConfigManager.Instance.Plaza.TODRabbitMQ.RabbitMQ : null;
                if (null != MQConfig && MQConfig.Enabled)
                {
                    //WriteTODFile("init");
                    med.Info("Rabbit Host Info: " + MQConfig.GetString());
                    try
                    {
                        todMQclient = new RabbitMQClient();
                        todMQclient.HostName = MQConfig.HostName;
                        taaMQclient.PortNumber = MQConfig.PortNumber;
                        todMQclient.VirtualHost = MQConfig.VirtualHost;
                        todMQclient.UserName = MQConfig.UserName;
                        todMQclient.Password = MQConfig.Password;
                        todMQclient.OnMessageArrived += TodMQclient_OnMessageArrived;
                        if (todMQclient.Connect() && todMQclient.Listen(MQConfig.QueueName))
                        {
                            med.Info("Success connected to : " + MQConfig.GetString());
                            med.Info("Listen to Queue: " + MQConfig.QueueName);
                        }
                        else
                        {
                            med.Err("failed connected to : " + MQConfig.HostName);
                        }
                    }
                    catch (Exception ex)
                    {
                        med.Err(ex);
                    }
                }
            }
        }

        public void Shutdown()
        {
            if (null != taaMQclient)
            {
                taaMQclient.OnMessageArrived -= TaaMQclient_OnMessageArrived;
                taaMQclient.Disconnect();
            }
            taaMQclient = null;

            if (null != todMQclient)
            {
                todMQclient.OnMessageArrived -= TodMQclient_OnMessageArrived;
                todMQclient.Disconnect();
            }
            todMQclient = null;

            if (null != server)
            {
                server.Dispose();
            }
            server = null;
            ReleaseOwinFirewall();

            // Shutdown database server.
            LocalDbServer.Instance.Shutdown();
        }

        #endregion
    }
}
