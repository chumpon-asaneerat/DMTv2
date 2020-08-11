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
// web socket
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;
using System.Reflection;
using NLib;
using System.Net;

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
            AppConsts.WindowsService.Local.WebServer.Protocol,
            AppConsts.WindowsService.Local.WebServer.HostName,
            AppConsts.WindowsService.Local.WebServer.PortNumber);
        
        
        private string wsAddress = string.Format(@"{0}://{1}:{2}",
            AppConsts.WindowsService.Local.WebSocket.Protocol,
            AppConsts.WindowsService.Local.WebSocket.HostName,
            AppConsts.WindowsService.Local.WebSocket.PortNumber);

        private IDisposable server = null;
        private WebSocketSharp.Server.WebSocketServer wsserver = null;

        #endregion

        #region Public Methods

        public void Start()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            // Start database server.
            LocalDbServer.Instance.Start();

            if (null == server)
            {
                server = WebApp.Start<StartUp>(url: baseAddress);
            }
            if (null == wsserver)
            {
                try
                {
                    wsserver = new WebSocketSharp.Server.WebSocketServer(wsAddress);
                    // Add web socket service
                    wsserver.AddWebSocketService<Behaviors.NotifyBehavior>("/nofify");
                    wsserver.Start();
                    if (wsserver.IsListening)
                    {
                        //string msg = string.Format("Listening on port {0}, and providing WebSocket services:", httpsv.Port);
                        //lbStatus.Text = msg;
                    }
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                }
            }
        }

        public void Shutdown()
        {
            if (null != wsserver)
            {
                wsserver.Stop();
            }
            wsserver = null;

            if (null != server)
            {
                server.Dispose();
            }
            server = null;

            // Shutdown database server.
            LocalDbServer.Instance.Shutdown();
        }

        #endregion
    }
}
