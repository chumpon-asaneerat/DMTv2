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

        private UserShiftOperations _UserShift_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets UserShift Operations.
        /// </summary>
        public UserShiftOperations UserShifts
        {
            get
            {
                if (null == _UserShift_Ops)
                {
                    lock (this)
                    {
                        _UserShift_Ops = new UserShiftOperations();
                    }
                }
                return _UserShift_Ops;
            }
        }

        #endregion

        #region UserShiftOperations (Collector TOD Shift)

        /// <summary>
        /// The Collector UserShift Operation class.
        /// Used for manage when TOD user begin job (shift).
        /// </summary>
        public class UserShiftOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal UserShiftOperations() { }

            #endregion

            #region Public Methods

            public NRestResult<UserShift> Create(Shift shift, User collector)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<UserShift> ret;
                var inst = new UserShiftCreate()
                {
                    Shift = shift,
                    User = collector
                };

                ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                    .Execute<UserShift>(RouteConsts.UserShift.Create.Url, inst);
                return ret;
            }

            public NRestResult<UserShift> GetCurrent(User value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<UserShift> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<UserShift>(RouteConsts.UserShift.GetCurrent.Url, value);
                }
                else
                {
                    ret = new NRestResult<UserShift>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            public NRestResult BeginUserShift(UserShift value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<bool>(RouteConsts.UserShift.BeginUserShift.Url, value);
                }
                else
                {
                    ret = new NRestResult();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult EndUserShift(UserShift value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute(RouteConsts.UserShift.EndUserShift.Url, value);
                }
                else
                {
                    ret = new NRestResult();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<List<UserShift>> GetUserShifts(User value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<List<UserShift>> ret;
                ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                    .Execute<List<UserShift>>(RouteConsts.UserShift.GetUserShifts.Url, value);
                return ret;
            }

            public NRestResult<List<UserShift>> GetUnCloseUserShifts()
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<List<UserShift>> ret;
                ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                    .Execute<List<UserShift>>(RouteConsts.UserShift.GetUnCloseUserShifts.Url);
                return ret;
            }

            #endregion
        }

        #endregion
    }
}
