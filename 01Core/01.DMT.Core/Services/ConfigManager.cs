#region Using

using System;
using System.IO;
using System.Reflection;
using System.Threading;
using NLib;
using NLib.IO;
using Newtonsoft.Json;

#endregion

namespace DMT.Services
{
    #region Plaza Config and related classes

    public class WebServiceConfig
    {
        public WebServiceConfig()
        {
            this.Protocol = "http";
            this.HostName = "localhost";
            this.PortNumber = 9000;
        }

        public string Protocol { get; set; }
        public string HostName { get; set; }
        public int PortNumber { get; set; }
    }

    public class LocalWebServiceConfig
    {
        public LocalWebServiceConfig() 
        {
            this.Http = new WebServiceConfig()
            {
                Protocol = "http",
                HostName = "localhost",
                PortNumber = 9000
            };

            this.WebSocket = new WebServiceConfig()
            {
                Protocol = "ws",
                HostName = "localhost",
                PortNumber = 9100
            };
        }

        public WebServiceConfig Http { get; set; }
        public WebServiceConfig WebSocket { get; set; }
    }

    public class TAxTODWebServiceConfig
    {
        public TAxTODWebServiceConfig()
        {
            this.Http = new WebServiceConfig()
            {
                Protocol = "http",
                HostName = "localhost",
                PortNumber = 3000
            };
        }

        public WebServiceConfig Http { get; set; }
    }

    public class DCWebServiceConfig
    {
        public DCWebServiceConfig()
        {
            this.Http = new WebServiceConfig()
            {
                Protocol = "http",
                HostName = "localhost",
                PortNumber = 3000
            };
        }

        public WebServiceConfig Http { get; set; }
    }

    public class PlazaConfig
    {
        public PlazaConfig() : base()
        {
            this.Local = new LocalWebServiceConfig();
            this.TAxTOD = new TAxTODWebServiceConfig();
            this.DC = new DCWebServiceConfig();
        }

        public LocalWebServiceConfig Local { get; set; }
        public TAxTODWebServiceConfig TAxTOD { get; set; }
        public DCWebServiceConfig DC { get; set; }
    }

    #endregion

    #region ConfigManager

    /// <summary>
    /// The ConfigManager class.
    /// </summary>
    public class ConfigManager
    {
        #region Static Instance Access

        private static ConfigManager _instance = null;

        /// <summary>
        /// Gets DMTAppManager instance access.
        /// </summary>
        public static ConfigManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ConfigManager))
                    {
                        _instance = new ConfigManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Internal Variables

        private Thread _th = null;
        private string _fileName = NJson.LocalConfigFile("plaza.config.json");
        private PlazaConfig _plazaCfg = new PlazaConfig();

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private ConfigManager() : base()
        {
            IsRunning = false;
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~ConfigManager()
        {
            Shutdown();
        }

        #endregion

        #region Private Methods

        private void Processing()
        {
            DateTime dt = DateTime.Now;
            TimeSpan ts;
            while (null != _th && IsRunning &&
                !ApplicationManager.Instance.IsExit)
            {
                ts = DateTime.Now - dt;
                if (ts.TotalMilliseconds > 1000)
                {
                    UpdateConfig();
                    dt = DateTime.Now;
                }
            }
            Shutdown();
        }

        private void UpdateConfig()
        {
            lock (this)
            {
                if (!NJson.ConfigExists(_fileName))
                {
                    if (null == _plazaCfg)
                    {
                        _plazaCfg = new PlazaConfig();
                    }
                    NJson.SaveToFile(_plazaCfg, _fileName);
                }

                _plazaCfg = NJson.LoadFromFile<PlazaConfig>(_fileName);
                if (null == _plazaCfg)
                {
                    // Read file failed.
                    _plazaCfg = new PlazaConfig();
                    NJson.SaveToFile(_plazaCfg, _fileName);
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start Service.
        /// </summary>
        public void Start()
        {
            if (null == _th)
            {
                _th = new Thread(Processing);
                _th.Priority = ThreadPriority.BelowNormal;
                _th.Name = "DMT Config Manager Thread";
                _th.IsBackground = true;
                IsRunning = true;
                _th.Start();
            }
        }
        /// <summary>
        /// Shutdown Service.
        /// </summary>
        public void Shutdown()
        {
            IsRunning = false;
            if (null != _th)
            {
                try { _th.Abort(); }
                catch (ThreadAbortException) { }
            }
            _th = null;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets is service is running.
        /// </summary>
        public bool IsRunning { get; private set; }
        /// <summary>
        /// Gets current plaza app information.
        /// </summary>
        public PlazaConfig Plaza 
        { 
            get { return _plazaCfg; }
            set { }
        }

        #endregion
    }

    #endregion
}
