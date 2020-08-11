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

            public UserShift Create(Shift shift, User collector)
            {
                var ret = NRestClient.Create(port: 9000).Execute<UserShift>(
                    RouteConsts.UserShift.Create.Url,
                    new UserShiftCreate()
                    {
                        Shift = shift,
                        User = collector
                    });
                return ret;
            }

            public UserShift GetCurrent(User user)
            {
                return NRestClient.Create(port: 9000).Execute<UserShift>(
                    RouteConsts.UserShift.GetCurrent.Url, user);
            }

            public bool BeginUserShift(UserShift shift)
            {
                if (null == shift) return false;
                return NRestClient.Create(port: 9000).Execute<bool>(
                    RouteConsts.UserShift.BeginUserShift.Url, shift);
            }

            public void EndUserShift(UserShift shift)
            {
                if (null == shift) return;
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.UserShift.EndUserShift.Url, shift);
            }

            public List<UserShift> GetUserShifts(User collector)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<UserShift>>(
                    RouteConsts.UserShift.GetUserShifts.Url, collector);
                return ret;
            }

            public List<UserShift> GetUnCloseUserShifts()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<UserShift>>(
                    RouteConsts.UserShift.GetUnCloseUserShifts.Url);
                return ret;
            }

            #endregion
        }

        #endregion
    }
}
