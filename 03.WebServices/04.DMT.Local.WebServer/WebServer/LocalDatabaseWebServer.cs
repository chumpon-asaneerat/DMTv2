#region Using

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
using System.Threading;
using System.Timers;
using System.Collections.Generic;
using System.Linq;

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

        private System.Timers.Timer _rabbit_timer = null;
        private bool _rabbit_scanning = false;

        private System.Timers.Timer _declare_timer = null;
        private bool _declare_scanning = false;

        #endregion

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

        #region Rabbit

        /// <summary>
        /// Gets local message json folder path name.
        /// </summary>
        public static string LocalRabbitMessageFolder
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
        public static string LocalRabbitTAMessageFolder
        {
            get
            {
                string localFilder = Folders.Combine(LocalRabbitMessageFolder, "TA");
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
        public static string LocalRabbitTODMessageFolder
        {
            get
            {
                string localFilder = Folders.Combine(LocalRabbitMessageFolder, "TOD");
                if (!Folders.Exists(localFilder))
                {
                    Folders.Create(localFilder);
                }
                return localFilder;
            }
        }

        private void WriteRabbitFile(string fullFileName, string message)
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
        }

        private void WriteRabbitTAFile(string message)
        {
            // Create file.
            string fileName = "msg." + DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.ffffff", 
                System.Globalization.DateTimeFormatInfo.InvariantInfo) + ".json";
            string fullFileName = Path.Combine(LocalRabbitTAMessageFolder, fileName);
            // Save message.
            WriteRabbitFile(fullFileName, message);
        }

        private void WriteRabbitTODFile(string message)
        {
            // Create file.
            string fileName = "msg." + DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.ffffff",
                System.Globalization.DateTimeFormatInfo.InvariantInfo) + ".json";
            string fullFileName = Path.Combine(LocalRabbitTODMessageFolder, fileName);
            // Save message.
            WriteRabbitFile(fullFileName, message);
        }

        private void TaaMQclient_OnMessageArrived(object sender, QueueMessageEventArgs e)
        {
            // Save message.
            WriteRabbitTAFile(e.Message);
        }

        private void TodMQclient_OnMessageArrived(object sender, QueueMessageEventArgs e)
        {
            // Save message.
            WriteRabbitTODFile(e.Message);
        }
        
        private void _rabbit_timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_rabbit_scanning) return;
            _rabbit_scanning = true;
            try
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                List<string> files = new List<string>();
                var taFiles = Directory.GetFiles(LocalRabbitTAMessageFolder, "*.json");
                if (null != taFiles && taFiles.Length > 0) files.AddRange(taFiles);
                var todFiles = Directory.GetFiles(LocalRabbitTODMessageFolder, "*.json");
                if (null != todFiles && todFiles.Length > 0) files.AddRange(todFiles);
                files.ForEach(file => 
                {
                    try
                    {
                        string json = File.ReadAllText(file);
                        // Update to database
                        var msg = json.FromJson<Models.RabbitMQMessage>();
                        if (null != msg)
                        {
                            if (msg.parameterName == "STAFF")
                            {
                                var mq = json.FromJson<Models.RabbitMQStaffMessage>();
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
                                    // process success backup file.
                                    Rabbit_MoveToBackup(file);
                                }
                                else
                                {
                                    med.Info("Cannot convert to STAFF message.");
                                    // process success error file.
                                    Rabbit_MoveToError(file);
                                }
                            }
                            else
                            {
                                med.Info("message is not STAFF message.");
                                // process not staff list so Invalid file.
                                Rabbit_MoveToInvalid(file);
                            }
                        }
                        else
                        {
                            med.Info("message is null or cannot convert to json object.");
                            // process success error file.
                            Rabbit_MoveToError(file);
                        }

                    }
                    catch (Exception ex)
                    {
                        med.Err(ex);
                    }
                });
            }
            catch (Exception) { }
            _rabbit_scanning = false;
        }

        private void Rabbit_MoveToBackup(string file)
        {
            string parentDir = Path.GetDirectoryName(file);
            string fileName = Path.GetFileName(file);
            string targetPath = Path.Combine(parentDir, "Backup");
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            if (!Directory.Exists(targetPath)) return;
            string targetFile = Path.Combine(targetPath, fileName);
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                if (File.Exists(targetFile)) File.Delete(targetFile);
                File.Move(file, targetFile);
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void Rabbit_MoveToError(string file)
        {
            string parentDir = Path.GetDirectoryName(file);
            string fileName = Path.GetFileName(file);
            string targetPath = Path.Combine(parentDir, "Error");
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            if (!Directory.Exists(targetPath)) return;
            string targetFile = Path.Combine(targetPath, fileName);
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                if (File.Exists(targetFile)) File.Delete(targetFile);
                File.Move(file, targetFile);
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void Rabbit_MoveToInvalid(string file)
        {
            string parentDir = Path.GetDirectoryName(file);
            string fileName = Path.GetFileName(file);
            string targetPath = Path.Combine(parentDir, "Invalid");
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            if (!Directory.Exists(targetPath)) return;
            string targetFile = Path.Combine(targetPath, fileName);
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                if (File.Exists(targetFile)) File.Delete(targetFile);
                File.Move(file, targetFile);
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        #endregion

        #region Declare (TOD)

        /// <summary>
        /// Gets local scw message json folder path name.
        /// </summary>
        public static string LocalSCWMessageFolder
        {
            get
            {
                string localFilder = Folders.Combine(
                    Folders.Assemblies.CurrentExecutingAssembly, "scw");
                if (!Folders.Exists(localFilder))
                {
                    Folders.Create(localFilder);
                }
                return localFilder;
            }
        }
        /// <summary>
        /// Gets local TA declare message json folder path name.
        /// </summary>
        public static string LocalTODDeclareolder
        {
            get
            {
                string localFilder = Folders.Combine(LocalSCWMessageFolder, "declare");
                if (!Folders.Exists(localFilder))
                {
                    Folders.Create(localFilder);
                }
                return localFilder;
            }
        }

        private void _declare_timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_declare_scanning) return;
            _declare_scanning = true;
            try
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                var server = SCWServiceOperations.Instance.Plaza;

                List<string> files = new List<string>();
                var declareFiles = Directory.GetFiles(LocalTODDeclareolder, "*.json");
                if (null != declareFiles && declareFiles.Length > 0) files.AddRange(declareFiles);
                files.ForEach(file =>
                {
                    try
                    {
                        string json = File.ReadAllText(file);

                        // TODO: Need user/password from config table or external file.
                        SCWServiceOperations.Instance.UserName = "DMTUSER";
                        SCWServiceOperations.Instance.Password = "DMTPASS";

                        // Update to database
                        var declare = json.FromJson<Models.SCWDeclare>();
                        if (null != declare)
                        {
                            var ret = server.TOD.Declare(declare);
                            if (null != ret && null != ret.status)
                            {
                                if (ret.status.code == "S200")
                                {
                                    // process success backup file.
                                    Declare_MoveToBackup(file);
                                }
                                else
                                {
                                    // process error send file.
                                    Declare_MoveToError(file);
                                }
                                // write log.
                                med.Info("declare - code: {0}, msg: {1}", ret.status.code, ret.status.message);
                            }
                            else
                            {
                                // send failed do nothing.
                                med.Info("declare error: SCW service connect failed.");
                            }
                        }
                        else
                        {
                            med.Info("message is null or cannot convert to json object.");
                            // process success error file.
                            Declare_MoveToError(file);
                        }

                    }
                    catch (Exception ex)
                    {
                        med.Err(ex);
                    }
                });
            }
            catch (Exception) { }
            _declare_scanning = false;
        }

        private void Declare_MoveToBackup(string file)
        {
            string parentDir = Path.GetDirectoryName(file);
            string fileName = Path.GetFileName(file);
            string targetPath = Path.Combine(parentDir, "Backup");
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            if (!Directory.Exists(targetPath)) return;
            string targetFile = Path.Combine(targetPath, fileName);
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                if (File.Exists(targetFile)) File.Delete(targetFile);
                File.Move(file, targetFile);
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private void Declare_MoveToError(string file)
        {
            string parentDir = Path.GetDirectoryName(file);
            string fileName = Path.GetFileName(file);
            string targetPath = Path.Combine(parentDir, "Error");
            if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
            if (!Directory.Exists(targetPath)) return;
            string targetFile = Path.Combine(targetPath, fileName);
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                if (File.Exists(targetFile)) File.Delete(targetFile);
                File.Move(file, targetFile);
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        #endregion

        #endregion

        #region Public Methods

        public void Start()
        {
            _rabbit_scanning = false;
            _rabbit_timer = new System.Timers.Timer();
            _rabbit_timer.Interval = 1000;
            _rabbit_timer.Elapsed += _rabbit_timer_Elapsed;
            _rabbit_timer.Start();

            _declare_scanning = false;
            _declare_timer = new System.Timers.Timer();
            _declare_timer.Interval = 1000;
            _declare_timer.Elapsed += _declare_timer_Elapsed;
            _declare_timer.Start();

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
            try
            {
                if (null != _declare_timer)
                {
                    _declare_timer.Elapsed -= _declare_timer_Elapsed;
                    _declare_timer.Stop();
                    _declare_timer.Dispose();
                }
            }
            catch { }
            _declare_timer = null;
            _declare_scanning = false;

            try
            {
                if (null != _rabbit_timer)
                {
                    _rabbit_timer.Elapsed -= _rabbit_timer_Elapsed;
                    _rabbit_timer.Stop();
                    _rabbit_timer.Dispose();
                }
            }
            catch { }
            _rabbit_timer = null;
            _rabbit_scanning = false;

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
