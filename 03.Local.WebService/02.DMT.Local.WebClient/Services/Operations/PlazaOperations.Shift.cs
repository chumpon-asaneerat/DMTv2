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

            public List<Shift> GetShifts()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Shift>>(
                    RouteConsts.Shift.GetShifts.Url, new { });
                return ret;
            }

            public TSBShift Create(Shift shift, User supervisor)
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBShift>(
                    RouteConsts.Shift.Create.Url,
                    new TSBShiftCreate()
                    {
                        Shift = shift,
                        User = supervisor
                    });
                return ret;
            }

            public TSBShift GetCurrent()
            {
                TimeSpan ts = DateTime.Now - LastUpdated;
                if (ts.TotalSeconds >= 1)
                {
                    _current = NRestClient.Create(port: 9000).Execute<TSBShift>(
                        RouteConsts.Shift.GetCurrent.Url, new { });

                    LastUpdated = DateTime.Now;
                }
                return _current;

            }

            public void ChangeShift(TSBShift shift)
            {
                if (null == shift) return;
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Shift.ChangeShift.Url, shift);

                // reset last update for reload new shirt.
                LastUpdated = DateTime.MinValue;
            }

            public Shift SaveShift(Shift value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<Shift>(
                    RouteConsts.Shift.SaveShift.Url, value);
                return ret;
            }

            #endregion
        }

        #endregion
    }
}
