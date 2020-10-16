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
                NRestResult<UserShift> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<UserShift>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                var inst = new UserShiftCreate()
                {
                    Shift = shift,
                    User = collector
                };

                ret = client.Execute<UserShift>(RouteConsts.UserShift.Create.Url, inst);
                return ret;
            }

            public NRestResult<UserShift> GetCurrent(User value)
            {
                NRestResult<UserShift> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<UserShift>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<UserShift>(
                        RouteConsts.UserShift.GetCurrent.Url, value);
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
                NRestResult ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<bool>(
                        RouteConsts.UserShift.BeginUserShift.Url, value);
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
                NRestResult ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute(RouteConsts.UserShift.EndUserShift.Url, value);
                }
                else
                {
                    ret = new NRestResult();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult SaveUserShift(UserShift value)
            {
                NRestResult ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute(RouteConsts.UserShift.SaveUserShift.Url, value);
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
                NRestResult<List<UserShift>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<UserShift>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<List<UserShift>>(
                    RouteConsts.UserShift.GetUserShifts.Url, value);
                return ret;
            }

            public NRestResult<List<UserShift>> GetUnCloseUserShifts()
            {
                NRestResult<List<UserShift>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<UserShift>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<List<UserShift>>(
                    RouteConsts.UserShift.GetUnCloseUserShifts.Url);
                return ret;
            }

            #endregion
        }

        #endregion
    }
}
