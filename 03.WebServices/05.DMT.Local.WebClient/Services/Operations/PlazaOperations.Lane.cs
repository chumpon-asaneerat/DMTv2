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

            #region Lane Attendance

            public NRestResult<LaneAttendance> CreateAttendance(Lane lane, User supervisor)
            {
                NRestResult<LaneAttendance> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<LaneAttendance>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<LaneAttendance>(RouteConsts.Lane.CreateAttendance.Url,
                    new LaneAttendanceCreate()
                    {
                        Lane = lane,
                        User = supervisor
                    });
                return ret;
            }

            public NRestResult<LaneAttendance> SaveAttendance(LaneAttendance value)
            {
                NRestResult<LaneAttendance> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<LaneAttendance>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<LaneAttendance>(
                        RouteConsts.Lane.SaveAttendance.Url, value);
                }
                else
                {
                    ret = new NRestResult<LaneAttendance>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult SaveAttendances(List<LaneAttendance> values)
            {
                NRestResult ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<LaneAttendance>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != values)
                {
                    ret = client.Execute(
                        RouteConsts.Lane.SaveAttendances.Url, values);
                }
                else
                {
                    ret = new NRestResult();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<List<LaneAttendance>> GetAttendancesByDate(
                Search.Lanes.Attendances.ByDate value)
            {
                NRestResult<List<LaneAttendance>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<LaneAttendance>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<LaneAttendance>>(
                        RouteConsts.Lane.GetAttendancesByDate.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LaneAttendance>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<List<LaneAttendance>> GetAttendancesByUserShift(
                Search.Lanes.Attendances.ByUserShift value)
            {
                NRestResult<List<LaneAttendance>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<LaneAttendance>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<LaneAttendance>>(
                        RouteConsts.Lane.GetAttendancesByUserShift.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LaneAttendance>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<List<LaneAttendance>> GetAllAttendancesByUserShift(
                UserShift value)
            {
                NRestResult<List<LaneAttendance>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<LaneAttendance>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<LaneAttendance>>(
                        RouteConsts.Lane.GetAllAttendancesByUserShift.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LaneAttendance>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<List<LaneAttendance>> GetAttendancesByLane(
                Search.Lanes.Attendances.ByLane value)
            {
                NRestResult<List<LaneAttendance>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<LaneAttendance>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<LaneAttendance>>(
                        RouteConsts.Lane.GetAttendancesByLane.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LaneAttendance>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<LaneAttendance> GetCurrentAttendancesByLane(
                Search.Lanes.Current.AttendanceByLane value)
            {
                NRestResult<LaneAttendance> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<LaneAttendance>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<LaneAttendance>(
                        RouteConsts.Lane.GetCurrentAttendancesByLane.Url, value);
                }
                else
                {
                    ret = new NRestResult<LaneAttendance>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<List<LaneAttendance>> GetAllNotHasRevenueEntry()
            {
                NRestResult<List<LaneAttendance>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<LaneAttendance>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<List<LaneAttendance>>(
                    RouteConsts.Lane.GetAllNotHasRevenueEntry.Url, null);
                return ret;
            }

            #endregion

            #region Lane Payment

            public NRestResult<LanePayment> CreatePayment(Lane lane, User supervisor,
                Payment payment, DateTime date, decimal amount)
            {
                NRestResult<LanePayment> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<LanePayment>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<LanePayment>(
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

            public NRestResult<LanePayment> SavePayment(LanePayment value)
            {
                NRestResult<LanePayment> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<LanePayment>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<LanePayment>(
                        RouteConsts.Lane.SavePayment.Url, value);
                }
                else
                {
                    ret = new NRestResult<LanePayment>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<List<LanePayment>> GetPaymentsByDate(
                Search.Lanes.Payments.ByDate value)
            {
                NRestResult<List<LanePayment>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<LanePayment>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<LanePayment>>(
                        RouteConsts.Lane.GetPaymentsByDate.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LanePayment>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<List<LanePayment>> GetPaymentsByUserShift(
                Search.Lanes.Attendances.ByUserShift value)
            {
                NRestResult<List<LanePayment>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<LanePayment>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<LanePayment>>(
                        RouteConsts.Lane.GetPaymentsByUserShift.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LanePayment>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<List<LanePayment>> GetPaymentsByLane(
                Search.Lanes.Attendances.ByLane value)
            {
                NRestResult<List<LanePayment>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<LanePayment>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<LanePayment>>(
                        RouteConsts.Lane.GetPaymentsByLane.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LanePayment>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<LanePayment> GetCurrentPaymentsByLane(
                Search.Lanes.Current.PaymentByLane value)
            {
                NRestResult<LanePayment> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<LanePayment>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<LanePayment>(
                        RouteConsts.Lane.GetCurrentPaymentsByLane.Url, value);
                }
                else
                {
                    ret = new NRestResult<LanePayment>();
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
