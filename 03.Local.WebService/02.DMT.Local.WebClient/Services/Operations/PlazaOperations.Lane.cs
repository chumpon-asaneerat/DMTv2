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

        private LaneOperations _Lane_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Lanes Operations.
        /// </summary>
        public LaneOperations Lanes
        {
            get
            {
                if (null == _Lane_Ops)
                {
                    lock (this)
                    {
                        _Lane_Ops = new LaneOperations();
                    }
                }
                return _Lane_Ops;
            }
        }

        #endregion

        #region LaneOperations (Used for Lane Attendance/Leave)

        /// <summary>
        /// The LaneOperations class.
        /// Used for manage Lane Attendance operation(s).
        /// </summary>
        public class LaneOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal LaneOperations() { }

            #endregion

            #region Public Methods

            public LaneAttendance CreateAttendance(Lane lane, User supervisor)
            {
                var ret = NRestClient.Create(port: 9000).Execute<LaneAttendance>(
                    RouteConsts.Lane.CreateAttendance.Url,
                    new LaneAttendanceCreate()
                    {
                        Lane = lane,
                        User = supervisor
                    });
                return ret;
            }

            public void SaveAttendance(LaneAttendance value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Lane.SaveAttendance.Url, value);
            }

            public List<LaneAttendance> GetAttendancesByDate(
                Search.Lanes.Attendances.ByDate value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<LaneAttendance>>(
                    RouteConsts.Lane.GetAttendancesByDate.Url, value);
                return ret;
            }

            public List<LaneAttendance> GetAttendancesByUserShift(
                Search.Lanes.Attendances.ByUserShift value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<LaneAttendance>>(
                    RouteConsts.Lane.GetAttendancesByUserShift.Url, value);
                return ret;
            }

            public List<LaneAttendance> GetAttendancesByLane(
                Search.Lanes.Attendances.ByLane value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<LaneAttendance>>(
                    RouteConsts.Lane.GetAttendancesByLane.Url, value);
                return ret;
            }

            public LaneAttendance GetCurrentAttendancesByLane(
                Search.Lanes.Current.AttendanceByLane value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<LaneAttendance>(
                    RouteConsts.Lane.GetCurrentAttendancesByLane.Url, value);
                return ret;
            }

            public List<LaneAttendance> GetAllNotHasRevenueEntry()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<LaneAttendance>>(
                    RouteConsts.Lane.GetAllNotHasRevenueEntry.Url, null);
                return ret;
            }

            public LaneAttendance CreatePayment(Lane lane, User supervisor,
                Payment payment, DateTime date, decimal amount)
            {
                var ret = NRestClient.Create(port: 9000).Execute<LaneAttendance>(
                    RouteConsts.Lane.CreatePayment.Url,
                    new LanePaymentCreate()
                    {
                        Lane = lane,
                        User = supervisor,
                        Payment = payment,
                        Date = date,
                        Amount = amount
                    });
                return ret;
            }

            public void SavePayment(LanePayment value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Lane.SavePayment.Url, value);
            }

            public List<LanePayment> GetPaymentsByDate(
                Search.Lanes.Payments.ByDate value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<LanePayment>>(
                    RouteConsts.Lane.GetPaymentsByDate.Url, value);
                return ret;
            }

            public List<LanePayment> GetPaymentsByUserShift(
                Search.Lanes.Attendances.ByUserShift value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<LanePayment>>(
                    RouteConsts.Lane.GetPaymentsByUserShift.Url, value);
                return ret;
            }

            public List<LanePayment> GetPaymentsByLane(
                Search.Lanes.Attendances.ByLane value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<LanePayment>>(
                    RouteConsts.Lane.GetPaymentsByLane.Url, value);
                return ret;
            }

            public LanePayment GetCurrentPaymentsByLane(
                Search.Lanes.Current.PaymentByLane value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<LanePayment>(
                    RouteConsts.Lane.GetCurrentPaymentsByLane.Url, value);
                return ret;
            }

            #endregion
        }

        #endregion
    }
}
