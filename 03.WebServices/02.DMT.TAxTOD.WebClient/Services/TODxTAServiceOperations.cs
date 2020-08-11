#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;
using NLib.ServiceProcess;

using DMT.Models;
using System.Runtime.InteropServices;
using System.Security.Permissions;

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

    #region TODxTAServiceOperations

    /// <summary>
    /// The TODxTA Service Operations class.
    /// </summary>
    public partial class TODxTAServiceOperations
    {
        #region Singelton

        private static TODxTAServiceOperations _instance = null;
        /// <summary>
        /// Singelton Access.
        /// </summary>
        public static TODxTAServiceOperations Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(TODxTAServiceOperations))
                    {
                        _instance = new TODxTAServiceOperations();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private TODxTAServiceOperations() : base()
        {
            ServiceMonitor = new NServiceMonitor();
            // Init windows service monitor.
            InitWindowsServices();

            Plaza = new TAxTODOperations();
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~TODxTAServiceOperations()
        {
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
                    ServiceName = DMT.AppConsts.WindowsService.TAxTOD.ServiceName,
                    // The File Name must match actual path related to entry (main execute)
                    // assembly.
                    FileName = System.IO.Path.Combine(path, AppConsts.WindowsService.TAxTOD.ExecutableFileName)
                });
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
                                if (srvInfo.ServiceName == AppConsts.WindowsService.TAxTOD.ServiceName)
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
        public NServiceMonitor ServiceMonitor { get; private set; }
        /// <summary>
        /// Gets instance of Plaza Operations.
        /// </summary>
        public TAxTODOperations Plaza { get; private set; }

        #endregion
    }

    #endregion
}
