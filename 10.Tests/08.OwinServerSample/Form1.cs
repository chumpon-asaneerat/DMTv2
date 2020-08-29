#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Owin SelfHost
using Owin;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Validation;

#endregion

namespace OwinServerSample
{
    public partial class Form1 : Form
    {
        #region Constructor

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Load/Closing

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Shutdown();
        }

        #endregion

        #region Button Handlers

        private void button1_Click(object sender, EventArgs e)
        {
            // Start
            Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Shutdown
            Shutdown();
        }

        #endregion

        private IDisposable server = null;

        private void Start()
        {
            if (null == server)
            {
                string baseAddress = @"http://+:8000";
                //string baseAddress = @"http://localhost:8000";
                server = WebApp.Start<StartUp>(url: baseAddress);
            }
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void Shutdown()
        {
            if (null != server)
            {
                server.Dispose();
            }
            server = null;
            button1.Enabled = true;
            button2.Enabled = false;
        }
    }


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
            /*
            bool isDMTModel = !type.IsSubclassOf(typeof(Models.DMTModelBase));
            return isDMTModel && base.ShouldValidateType(type);
            */
            return base.ShouldValidateType(type);
        }
    }

    public class TestController : ApiController
    {
        [HttpPost]
        [ActionName(@"getsample")]
        public SampleResult getsample([FromBody] SampleRequest value)
        {
            SampleResult result;
            if (null == value)
            {
                result = new SampleResult();
                result.Greating = "Welcome [anonymous].";
            }
            else
            {
                result = new SampleResult();
                result.Greating = "Welcome [" + value.Name + "].";
            }
            return result;
        }
    }

    public class SampleRequest
    {
        public string Name { get; set; }
    }

    public class SampleResult
    {
        public string Greating { get; set; }
    }
}
