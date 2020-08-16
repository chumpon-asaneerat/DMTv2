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
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<LaneAttendance> ret;
                ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                    .Execute<LaneAttendance>(RouteConsts.Lane.CreateAttendance.Url,
                    new LaneAttendanceCreate()
                    {
                        Lane = lane,
                        User = supervisor
                    });
                return ret;
            }

            public NRestResult<LaneAttendance> SaveAttendance(LaneAttendance value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<LaneAttendance> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<LaneAttendance>(RouteConsts.Lane.SaveAttendance.Url, value);
                }
                else
                {
                    ret = new NRestResult<LaneAttendance>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            public NRestResult<List<LaneAttendance>> GetAttendancesByDate(
                Search.Lanes.Attendances.ByDate value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<List<LaneAttendance>> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<List<LaneAttendance>>(RouteConsts.Lane.GetAttendancesByDate.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LaneAttendance>>();
                    ret.ParameterIsNull();
                    ret.data = new List<LaneAttendance>();
                }
                return ret;
            }

            public NRestResult<List<LaneAttendance>> GetAttendancesByUserShift(
                Search.Lanes.Attendances.ByUserShift value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<List<LaneAttendance>> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<List<LaneAttendance>>(RouteConsts.Lane.GetAttendancesByUserShift.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LaneAttendance>>();
                    ret.ParameterIsNull();
                    ret.data = new List<LaneAttendance>();
                }
                return ret;
            }

            public NRestResult<List<LaneAttendance>> GetAllAttendancesByUserShift(
                UserShift value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<List<LaneAttendance>> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<List<LaneAttendance>>(RouteConsts.Lane.GetAllAttendancesByUserShift.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LaneAttendance>>();
                    ret.ParameterIsNull();
                    ret.data = new List<LaneAttendance>();
                }
                return ret;
            }

            public NRestResult<List<LaneAttendance>> GetAttendancesByLane(
                Search.Lanes.Attendances.ByLane value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<List<LaneAttendance>> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<List<LaneAttendance>>(RouteConsts.Lane.GetAttendancesByLane.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LaneAttendance>>();
                    ret.ParameterIsNull();
                    ret.data = new List<LaneAttendance>();
                }
                return ret;
            }

            public NRestResult<LaneAttendance> GetCurrentAttendancesByLane(
                Search.Lanes.Current.AttendanceByLane value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<LaneAttendance> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<LaneAttendance>(RouteConsts.Lane.GetCurrentAttendancesByLane.Url, value);
                }
                else
                {
                    ret = new NRestResult<LaneAttendance>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            public NRestResult<List<LaneAttendance>> GetAllNotHasRevenueEntry()
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<List<LaneAttendance>> ret;
                ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                    .Execute<List<LaneAttendance>>(RouteConsts.Lane.GetAllNotHasRevenueEntry.Url, null);
                return ret;
            }

            #endregion

            #region Lane Payment

            public NRestResult<LanePayment> CreatePayment(Lane lane, User supervisor,
                Payment payment, DateTime date, decimal amount)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<LanePayment> ret;
                ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                    .Execute<LanePayment>(RouteConsts.Lane.CreatePayment.Url,
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
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<LanePayment> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<LanePayment>(RouteConsts.Lane.SavePayment.Url, value);
                }
                else
                {
                    ret = new NRestResult<LanePayment>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            public NRestResult<List<LanePayment>> GetPaymentsByDate(
                Search.Lanes.Payments.ByDate value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<List<LanePayment>> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<List<LanePayment>>(RouteConsts.Lane.GetPaymentsByDate.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LanePayment>>();
                    ret.ParameterIsNull();
                    ret.data = new List<LanePayment>();
                }
                return ret;
            }

            public NRestResult<List<LanePayment>> GetPaymentsByUserShift(
                Search.Lanes.Attendances.ByUserShift value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<List<LanePayment>> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<List<LanePayment>>(RouteConsts.Lane.GetPaymentsByUserShift.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LanePayment>>();
                    ret.ParameterIsNull();
                    ret.data = new List<LanePayment>();
                }
                return ret;
            }

            public NRestResult<List<LanePayment>> GetPaymentsByLane(
                Search.Lanes.Attendances.ByLane value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<List<LanePayment>> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<List<LanePayment>>(RouteConsts.Lane.GetPaymentsByLane.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<LanePayment>>();
                    ret.ParameterIsNull();
                    ret.data = new List<LanePayment>();
                }
                return ret;
            }

            public NRestResult<LanePayment> GetCurrentPaymentsByLane(
                Search.Lanes.Current.PaymentByLane value)
            {
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.Local.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.Local.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.Local.Http.PortNumber;

                NRestResult<LanePayment> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<LanePayment>(RouteConsts.Lane.GetCurrentPaymentsByLane.Url, value);
                }
                else
                {
                    ret = new NRestResult<LanePayment>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
