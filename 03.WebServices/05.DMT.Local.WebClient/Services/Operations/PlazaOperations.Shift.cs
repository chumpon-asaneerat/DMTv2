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
            private DateTime LastUpdated = DateTime.MinValue;
            private TSBShift _current = null;

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
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<List<Shift>> ret;
                ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                    .Execute<List<Shift>>(RouteConsts.Shift.GetShifts.Url, new { });
                return ret;
            }

            public NRestResult<Shift> SaveShift(Shift value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

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
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<TSBShift> ret;
                if (null != value)
                {
                    var inst = new TSBShiftCreate()
                    {
                        Shift = shift,
                        User = supervisor
                    };
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<TSBShift>(RouteConsts.Shift.Create.Url inst);
                }
                else
                {
                    ret = new NRestResult<TSBShift>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            public NRestResult<TSBShift> GetCurrent()
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<TSBShift> ret;
                TimeSpan ts = DateTime.Now - LastUpdated;
                if (null != value)
                {
                    if (ts.TotalSeconds >= 1)
                    {
                        ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                            .Execute<TSBShift>(RouteConsts.Shift.GetCurrent.Url, new { });

                        LastUpdated = DateTime.Now;
                    }
                }
                else
                {
                    ret = new NRestResult<TSBShift>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            public NRestResult ChangeShift(TSBShift value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

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
                // reset last update for reload new shirt.
                LastUpdated = DateTime.MinValue;

                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
