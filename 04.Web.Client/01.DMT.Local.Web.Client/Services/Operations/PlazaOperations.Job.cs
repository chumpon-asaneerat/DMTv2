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
    partial class PlazaOperations
    {
        #region Internal Variables

        private JobOperations _Job_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Jobs Operations.
        /// </summary>
        public JobOperations Jobs
        {
            get
            {
                if (null == _Job_Ops)
                {
                    lock (this)
                    {
                        _Job_Ops = new JobOperations();
                    }
                }
                return _Job_Ops;
            }
        }

        #endregion

        #region JobOperations (Collector TOD Shift)

        /// <summary>
        /// The Collector Job Operation class.
        /// Used for manage when TOD user begin job (shift).
        /// </summary>
        public class JobOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal JobOperations() { }

            #endregion

            #region Public Methods

            public UserShift Create(Shift shift, User collector)
            {
                var ret = NRestClient.Create(port: 9000).Execute<UserShift>(
                    RouteConsts.Job.Create.Url,
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
                    RouteConsts.Job.GetCurrent.Url, user);
            }

            public bool BeginJob(UserShift shift)
            {
                if (null == shift) return false;
                return NRestClient.Create(port: 9000).Execute<bool>(
                    RouteConsts.Job.BeginJob.Url, shift);
            }

            public void EndJob(UserShift shift)
            {
                if (null == shift) return;
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Job.EndJob.Url, shift);
            }

            public List<UserShift> GetUserShifts(User collector)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<UserShift>>(
                    RouteConsts.Job.GetUserShifts.Url, collector);
                return ret;
            }

            #endregion
        }

        #endregion
    }
}
