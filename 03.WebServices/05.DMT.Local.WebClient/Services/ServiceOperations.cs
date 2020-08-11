#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;

using NLib;
using NLib.ServiceProcess;

using RestSharp;

using WebSocketSharp;

using DMT.Models;
using System.Reflection;

#endregion

namespace DMT.Services
{
    #region InstalledStatus

    /// <summary>
    /// The DMT Window Service Installed Status class.
    /// </summary>
    public class InstalledStatus
    {
        #region Public properties

        /// <summary>
        /// Gets (or internal set) all service count.
        /// </summary>
        public int ServiceCount { get; internal set; }
        /// <summary>
        /// Gets (or internal set) service install count.
        /// </summary>
        public int InstalledCount { get; internal set; }
        /// <summary>
        /// Gets (or internal set) is Plaza Local Service installed.
        /// </summary>
        public bool PlazaLocalServiceInstalled { get; internal set; }

        #endregion
    }

    #endregion

    #region LocalServiceOperations

    /// <summary>
    /// The Local Service Operations class.
    /// </summary>
    public partial class LocalServiceOperations
    {
        #region Singelton

        private static LocalServiceOperations _instance = null;
        /// <summary>
        /// Singelton Access.
        /// </summary>
        public static LocalServiceOperations Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(LocalServiceOperations))
                    {
                        _instance = new LocalServiceOperations();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Internal Variables

        private string wsAddress = string.Format(@"{0}://{1}:{2}",
            AppConsts.WindowsService.Local.WebSocket.Protocol,
            AppConsts.WindowsService.Local.WebSocket.HostName,
            AppConsts.WindowsService.Local.WebSocket.PortNumber);

        private WebSocket _ws = null;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private LocalServiceOperations() : base()
        {
            ServiceMonitor = new NServiceMonitor();
            // Init windows service monitor.
            InitWindowsServices();

            Connect();
            Plaza = new LocalOperations();
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~LocalServiceOperations()
        {
            Disconnect();
            // Shutdown windows service monitor.
            if (null != ServiceMonitor)
            {
                ServiceMonitor.Shutdown();
            }
            ServiceMonitor = null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Init windows service list to monitor.
        /// </summary>
        private void InitWindowsServices()
        {
            if (null == ServiceMonitor)
                return;
            // Init Service to monitor
            ServiceMonitor.ServiceNames.Clear();
            string path = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location);

            // Append Local Plaza Window Service application
            ServiceMonitor.ServiceNames.Add(
                new NServiceName()
                {
                    // The Service Name must match the name that declare name 
                    // in NServiceInstaller inherited class
                    ServiceName = DMT.AppConsts.WindowsService.Local.ServiceName,
                    // The File Name must match actual path related to entry (main execute)
                    // assembly.
                    FileName = System.IO.Path.Combine(path, AppConsts.WindowsService.Local.ExecutableFileName)
                });
        }

        private void Connect()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            if (null == _ws)
            {
                try
                {
                    _ws = new WebSocket(wsAddress + "/nofify");
                    _ws.OnMessage += Ws_OnMessage;
                    _ws.OnError += Ws_OnError;
                    _ws.OnClose += Ws_OnClose;
                    _ws.Connect();
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    _ws = null;
                }
            }
        }

        private void Disconnect()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                if (null != _ws)
                {
                    _ws.OnClose -= Ws_OnClose;
                    _ws.OnError -= Ws_OnError;
                    _ws.OnMessage -= Ws_OnMessage;
                    _ws.Close();
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
            _ws = null;
        }

        void Reconnect(ushort code, string error)
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                if (code != (ushort)CloseStatusCode.Normal)
                {
                    if (null != _ws)
                    {
                        _ws.Connect();
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        #endregion

        #region WS Handlers

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            // Cross thread wrapper.
            string msg = e.Data;
            if (string.IsNullOrWhiteSpace(msg)) return;
            string[] msgs = msg.Split(':');
            string evtName = (null != msgs && msgs.Length > 0) ? msgs[0] : string.Empty;
            if (string.IsNullOrWhiteSpace(evtName)) return;

            if (evtName == "changeshift")
            {
                OnChangeShift.Call(this, EventArgs.Empty);
            }
        }

        private void Ws_OnClose(object sender, CloseEventArgs e)
        {
            Disconnect();
            //TODO: Need Reconnect logic.
            //Reconnect(e.Code, e.Reason);
        }

        private void Ws_OnError(object sender, ErrorEventArgs e)
        {
            Disconnect();
        }

        #endregion

        #region Public Methods

        #region Install/Uninstall/CheckInstalled

        /// <summary>
        /// Install all registered windows services.
        /// </summary>
        public void Install()
        {
            if (null == ServiceMonitor)
                return;
            ServiceMonitor.InstallAll();
        }
        /// <summary>
        /// Uninstall all registered windows services.
        /// </summary>
        public void Uninstall()
        {
            if (null == ServiceMonitor)
                return;
            ServiceMonitor.UninstallAll();
        }
        /// <summary>
        /// Checks services installed status.
        /// </summary>
        /// <returns>Returns ServiceStatus instance.</returns>
        public InstalledStatus CheckInstalled()
        {
            InstalledStatus result = new InstalledStatus();
            result.ServiceCount = 0;
            result.InstalledCount = 0;
            result.PlazaLocalServiceInstalled = false;
            if (null != ServiceMonitor)
            {
                try
                {
                    NServiceInfo[] srvs = ServiceMonitor.ServiceInformations;
                    if (null != srvs)
                    {
                        result.ServiceCount = srvs.Length;
                        foreach (NServiceInfo srvInfo in srvs)
                        {
                            if (srvInfo.IsInstalled)
                            {
                                ++result.InstalledCount;
                                if (srvInfo.ServiceName == AppConsts.WindowsService.Local.ServiceName)
                                {
                                    result.PlazaLocalServiceInstalled = true;
                                }
                            }
                        }
                    }
                }
                catch { }
            }            
            return result; // return scan result.
        }

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Instance of Windows Services Monitor.
        /// </summary>
        public NServiceMonitor ServiceMonitor { get;  private set; }
        /// <summary>
        /// Gets instance of Plaza Operations.
        /// </summary>
        public LocalOperations Plaza { get; private set; }

        #endregion

        #region Public Events

        public event EventHandler OnChangeShift;

        #endregion
    }

    #endregion
}
