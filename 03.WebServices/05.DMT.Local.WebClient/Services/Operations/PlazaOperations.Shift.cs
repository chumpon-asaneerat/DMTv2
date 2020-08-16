#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;

#endregion

namespace DMT.Services
{
    partial class LocalOperations
    {
        #region Internal Variables

        private ShiftOperations _Shift_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Shift Operations.
        /// </summary>
        public ShiftOperations Shifts
        {
            get
            {
                if (null == _Shift_Ops)
                {
                    lock (this)
                    {
                        _Shift_Ops = new ShiftOperations();
                    }
                }
                return _Shift_Ops;
            }
        }

        #endregion

        #region ShiftOperations (Supervisor Shift)

        /// <summary>
        /// The ShiftOperations class.
        /// Used for Manage Supervisor's Shift operation(s).
        /// </summary>
        public class ShiftOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal ShiftOperations() { }

            #endregion

            #region Public Methods

            #region Shift

            public NRestResult<List<Shift>> GetShifts()
            {
                // TODO: Config - Remove Later.
                /*
                NRestClient.WebProtocol protocol = 
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ? 
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;
                */
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<List<Shift>> ret;
                ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                    .Execute<List<Shift>>(RouteConsts.Shift.GetShifts.Url, new { });
                return ret;
            }

            public NRestResult<Shift> SaveShift(Shift value)
            {
                // TODO: Config - Remove Later.
                /*
                NRestClient.WebProtocol protocol = 
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ? 
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;
                */
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<Shift> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<Shift>(RouteConsts.Shift.SaveShift.Url, value);
                }
                else
                {
                    ret = new NRestResult<Shift>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            #endregion

            #region TSB Shift

            public NRestResult<TSBShift> Create(Shift shift, User supervisor)
            {
                // TODO: Config - Remove Later.
                /*
                NRestClient.WebProtocol protocol = 
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ? 
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;
                */
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<TSBShift> ret;
                var inst = new TSBShiftCreate()
                {
                    Shift = shift,
                    User = supervisor
                };
                ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                    .Execute<TSBShift>(RouteConsts.Shift.Create.Url, inst);
                return ret;
            }

            public NRestResult<TSBShift> GetCurrent()
            {
                // TODO: Config - Remove Later.
                /*
                NRestClient.WebProtocol protocol = 
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ? 
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;
                */
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<TSBShift> ret;
                ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                    .Execute<TSBShift>(RouteConsts.Shift.GetCurrent.Url, new { });
                return ret;
            }

            public NRestResult ChangeShift(TSBShift value)
            {
                // TODO: Config - Remove Later.
                /*
                NRestClient.WebProtocol protocol = 
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ? 
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;
                */
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute(RouteConsts.Shift.ChangeShift.Url, value);
                }
                else
                {
                    ret = new NRestResult();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
