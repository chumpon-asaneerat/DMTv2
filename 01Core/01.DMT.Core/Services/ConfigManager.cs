#region Using

using System;
using System.IO;
using System.Reflection;
using System.Threading;
using NLib;
using NLib.IO;
using Newtonsoft.Json;
using NLib.Controls.Design;

#endregion

namespace DMT.Services
{
    #region Plaza Config and related classes

    #region WebServiceConfig

    /// <summary>
    /// The WebServiceConfig class.
    /// </summary>
    public class WebServiceConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WebServiceConfig()
        {
            this.Protocol = "http";
            this.HostName = "localhost";
            this.PortNumber = 9000;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets protocol.
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// Gets or sets Host Name or IP Address.
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// Gets or sets port number.
        /// </summary>
        public int PortNumber { get; set; }

        #endregion
    }

    #endregion

    #region LocalWebServiceConfig

    /// <summary>
    /// The LocalWebServiceConfig class.
    /// </summary>
    public class LocalWebServiceConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
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

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Http service.
        /// </summary>
        public WebServiceConfig Http { get; set; }
        /// <summary>
        /// Gets or sets Web Socket service.
        /// </summary>
        public WebServiceConfig WebSocket { get; set; }

        #endregion
    }

    #endregion

    #region TAxTODWebServiceConfig

    /// <summary>
    /// The TAxTODWebServiceConfig class.
    /// </summary>
    public class TAxTODWebServiceConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TAxTODWebServiceConfig()
        {
            this.Http = new WebServiceConfig()
            {
                Protocol = "http",
                HostName = "localhost",
                PortNumber = 3000
            };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Http service.
        /// </summary>
        public WebServiceConfig Http { get; set; }

        #endregion
    }

    #endregion

    #region DCWebServiceConfig

    /// <summary>
    /// The DCWebServiceConfig class.
    /// </summary>
    public class DCWebServiceConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DCWebServiceConfig()
        {
            this.Http = new WebServiceConfig()
            {
                Protocol = "http",
                HostName = "localhost",
                PortNumber = 5000
            };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Http service.
        /// </summary>
        public WebServiceConfig Http { get; set; }

        #endregion
    }

    #endregion

    #region PlazaConfig

    /// <summary>
    /// The PlazaConfig class.
    /// </summary>
    public class PlazaConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaConfig() : base()
        {
            this.Local = new LocalWebServiceConfig();
            this.TAxTOD = new TAxTODWebServiceConfig();
            this.DC = new DCWebServiceConfig();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Local Service Config.
        /// </summary>
        public LocalWebServiceConfig Local { get; set; }
        /// <summary>
        /// Gets or sets TAxTOD Service Config.
        /// </summary>
        public TAxTODWebServiceConfig TAxTOD { get; set; }
        /// <summary>
        /// Gets or sets DC Service Config.
        /// </summary>
        public DCWebServiceConfig DC { get; set; }

        #endregion
    }

    #endregion

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
        private DateTime _lastUpdate = DateTime.MinValue;
        private int _timeout = 15 * 1000; // timeout in ms.

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
            TimeSpan ts;
            while (null != _th && IsRunning &&
                !ApplicationManager.Instance.IsExit)
            {
                ts = DateTime.Now - _lastUpdate;
                if (ts.TotalMilliseconds > _timeout)
                {
                    UpdateConfig();
                    _lastUpdate = DateTime.Now;
                }
            }
            Shutdown();
        }

        private void UpdateConfig()
        {
            lock (this)
            {
                try
                {
                    //var oldCfg = _plazaCfg;
                    if (!NJson.ConfigExists(_fileName))
                    {
                        _plazaCfg = new PlazaConfig();
                    }
                    else
                    {
                        _plazaCfg = NJson.LoadFromFile<PlazaConfig>(_fileName);
                    }
                    // save back to file.
                    if (null == _plazaCfg)
                    {
                        _plazaCfg = new PlazaConfig();
                        Console.WriteLine("New Config: {0}", _plazaCfg);
                    }
                    else
                    {
                        Console.WriteLine("Loaded Config: {0}", _plazaCfg);
                    }
                    NJson.SaveToFile(_plazaCfg, _fileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
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
