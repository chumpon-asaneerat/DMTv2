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

using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

#endregion

namespace OwinClientSample
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
            pgReq.SelectedObject = new { Name = "Test" };
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        #endregion

        #region Button Handlers

        private void button1_Click(object sender, EventArgs e)
        {
            string BaseUrl = @"http://localhost:8000";
            string actionUrl = @"api/test/getsample";
            var req = pgReq.SelectedObject;
            pgRes.SelectedObject = null;
            // send
            var client = new RestClient(BaseUrl);
            //client.ReadWriteTimeout = timeout;
            client.Timeout = 1000;
            /*
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                client.Authenticator = new HttpBasicAuthenticator(username, password);
            }
            */
            //client.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);
            client.UseNewtonsoftJson();
            var request = new RestRequest(actionUrl, Method.POST);
            request.RequestFormat = DataFormat.Json;
            if (null != req)
            {
                request.AddJsonBody(req);
            }

            var response = client.Execute(request);
            if (null != response)
            {
                pgRes.SelectedObject = JsonConvert.DeserializeObject(response.Content);
            }
        }

        #endregion
    }
}
