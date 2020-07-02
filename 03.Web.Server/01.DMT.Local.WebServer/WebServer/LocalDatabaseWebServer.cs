#region Using

using System;

// Owin SelfHost
using Owin;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Web.Http;

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

            appBuilder.UseWebApi(config);
        }
    }

    /// <summary>
    /// Local Database Web Server (Self Host).
    /// </summary>
    public class LocalDatabaseWebServer
    {
        private string baseAddress = string.Format(@"{0}://{1}:{2}/",
            AppConsts.WindowsService.Plaza.LocaWebServer.Protocol,
            AppConsts.WindowsService.Plaza.LocaWebServer.HostName,
            AppConsts.WindowsService.Plaza.LocaWebServer.PortNumber);
        private IDisposable server = null;

        public void Start()
        {
            // Start database server.
            LocalDbServer.Instance.Start();

            if (null == server)
            {
                server = WebApp.Start<StartUp>(url: baseAddress);
            }
        }
        public void Shutdown()
        {
            if (null != server)
            {
                server.Dispose();
            }
            server = null;

            // Shutdown database server.
            LocalDbServer.Instance.Shutdown();
        }
    }
}
