#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;
using NLib.ServiceProcess;

using DMT.Models;

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

    #region PlazaOperations

    /// <summary>
    /// Plaza Operations class.
    /// </summary>
    public class PlazaOperations
    {
        #region Static Constructor.

        /// <summary>
        /// Static Constructor
        /// </summary>
        static PlazaOperations()
        {
            // Required for HTTPS.
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 |
                SecurityProtocolType.Tls11 |
                SecurityProtocolType.Tls |
                (SecurityProtocolType)768 | (SecurityProtocolType)3072 |
                SecurityProtocolType.SystemDefault;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaOperations()
        {
            TSB = new TSBOprations();
            Users = new UserOperations();
            Shifts = new ShiftOperations();
            Jobs = new JobOperations();
            Lanes = new LaneOperations();
        }

        #endregion

        #region Public Properties

        public TSBOprations TSB { get; private set; }
        public UserOperations Users { get; private set; }
        public ShiftOperations Shifts { get; private set; }
        public JobOperations Jobs { get; private set; }
        public LaneOperations Lanes { get; private set; }

        #endregion

        #region TSBOprations

        /// <summary>
        /// The TSBOprations class.
        /// </summary>
        public class TSBOprations
        {
            private DateTime LastUpdated = DateTime.MinValue;
            private TSB _current = null;

            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal TSBOprations() { }

            #endregion

            #region Public Methods

            public List<TSB> GetTSBs()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSB>>(
                    RouteConsts.TSB.GetTSBs.Url, new { });
                return ret;
            }

            public List<Plaza> GetTSBPlazas(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Plaza>>(
                    RouteConsts.TSB.GetTSBPlazas.Url, tsb);
                return ret;
            }

            public List<Lane> GetTSBLanes(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Lane>>(
                    RouteConsts.TSB.GetTSBLanes.Url, tsb);
                return ret;
            }

            public List<Lane> GetPlazaLanes(Plaza plaza)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Lane>>(
                    RouteConsts.TSB.GetPlazaLanes.Url, plaza);
                return ret;
            }

            public void SetActive(TSB tsb)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.TSB.SetActive.Url, tsb);
            }

            public TSB GetCurrent()
            {
                TSB ret = _current;
                TimeSpan ts = DateTime.Now - LastUpdated;
                if (ts.TotalMinutes >= 1)
                {
                    _current = NRestClient.Create(port: 9000).Execute<TSB>(
                        RouteConsts.TSB.GetCurrent.Url, new { });

                    LastUpdated = DateTime.Now;
                }                
                return _current;
            }
        }

        #endregion

        #region UserOperations

        /// <summary>
        /// The UserOperations class.
        /// </summary>
        public class UserOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal UserOperations() { }

            #endregion

            #region Public Methods

            public List<Role> GetRoles()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Role>>(
                    RouteConsts.User.GetRoles.Url, new { });
                return ret;
            }

            public List<User> GetUsers(Role role)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<User>>(
                    RouteConsts.User.GetUsers.Url, role);
                return ret;
            }

            public User GetByCardId(Search.Users.ByCardId value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<User>(
                    RouteConsts.User.GetByCardId.Url, value);
                return ret;
            }

            public User GetById(Search.Users.ById value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<User>(
                    RouteConsts.User.GetById.Url, value);
                return ret;
            }

            public User GetByLogIn(Search.Users.ByLogIn value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<User>(
                    RouteConsts.User.GetByLogIn.Url, value);
                return ret;
            }

            #endregion
        }

        #endregion

        #region ShiftOperations

        /// <summary>
        /// The ShiftOperations class.
        /// </summary>
        public class ShiftOperations
        {
            private DateTime LastUpdated = DateTime.MinValue;
            private TSBShift _current = null;

            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal ShiftOperations() { }

            #endregion

            #region Public Methods

            public List<Shift> GetShifts()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Shift>>(
                    RouteConsts.Shift.GetShifts.Url, new { });
                return ret;
            }

            public void ChangeShift(TSBShift shift)
            {
                if (null == shift) return;
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Shift.ChangeShift.Url, shift);
                
                // reset last update for reload new shirt.
                LastUpdated = DateTime.MinValue;
            }

            public TSBShift Create(Shift shift, User supervisor)
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBShift>(
                    RouteConsts.Shift.Create.Url, 
                    new TSBShiftCreate() 
                    { 
                        Shift = shift, User = supervisor 
                    });
                return ret;
            }

            public TSBShift GetCurrent()
            {
                TimeSpan ts = DateTime.Now - LastUpdated;
                if (ts.TotalMinutes >= 1)
                {
                    _current = NRestClient.Create(port: 9000).Execute<TSBShift>(
                        RouteConsts.Shift.GetCurrent.Url, new { });

                    LastUpdated = DateTime.Now;
                }
                return _current;

            }

            #endregion
        }

        #endregion

        #region JobOperations

        public class JobOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal JobOperations() { }

            #endregion

            #region Public Methods

            public UserShift Create(Shift shift, User supervisor)
            {
                var ret = NRestClient.Create(port: 9000).Execute<UserShift>(
                    RouteConsts.Job.Create.Url,
                    new UserShiftCreate()
                    {
                        Shift = shift,
                        User = supervisor
                    });
                return ret;
            }

            #endregion
        }

        #endregion

        #region LaneOperations

        public class LaneOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal LaneOperations() { }

            #endregion

            #region Public Methods

            public LaneAttendance Create(Lane lane, User supervisor)
            {
                var ret = NRestClient.Create(port: 9000).Execute<LaneAttendance>(
                    RouteConsts.Lane.Create.Url,
                    new LaneAttendanceCreate()
                    {
                        Lane = lane,
                        User = supervisor
                    });
                return ret;
            }

            #endregion
        }

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Base Address.
        /// </summary>
        public string BaseAddress
        {
            get
            {
                return string.Format(@"{0}://{1}:{2}/",
                    AppConsts.WindowsService.Plaza.LocaWebServer.Protocol,
                    AppConsts.WindowsService.Plaza.LocaWebServer.HostName,
                    AppConsts.WindowsService.Plaza.LocaWebServer.PortNumber);
            }
        }

        #endregion
    }

    #endregion

    #region DMTServiceOperations

    /// <summary>
    /// The DMT Service Operations class.
    /// </summary>
    public partial class DMTServiceOperations
    {
        #region Singelton

        private static DMTServiceOperations _instance = null;
        /// <summary>
        /// Singelton Access.
        /// </summary>
        public static DMTServiceOperations Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(DMTServiceOperations))
                    {
                        _instance = new DMTServiceOperations();
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
        private DMTServiceOperations() : base()
        {
            ServiceMonitor = new NServiceMonitor();
            // Init windows service monitor.
            InitWindowsServices();

            Plaza = new PlazaOperations();
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~DMTServiceOperations()
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
                    ServiceName = DMT.AppConsts.WindowsService.Plaza.ServiceName,
                    // The File Name must match actual path related to entry (main execute)
                    // assembly.
                    FileName = System.IO.Path.Combine(path, AppConsts.WindowsService.Plaza.ExecutableFileName)
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
                                if (srvInfo.ServiceName == AppConsts.WindowsService.Plaza.ServiceName)
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
        public PlazaOperations Plaza { get; private set; }

        #endregion
    }

    #endregion
}
