#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NLib.Reflection;

using DMT.Models;
using System.Runtime.InteropServices.WindowsRuntime;
//using DMT.Models.ExtensionMethods;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// The controller for handle Lane Attendance (Attendance/Leave).
    /// </summary>
    public class LaneController : ApiController
    {
        #region Lane Attendance

        [HttpPost]
        [ActionName(RouteConsts.Lane.CreateAttendance.Name)]
        public NDbResult<LaneAttendance> Create([FromBody] LaneAttendanceCreate value)
        {
            NDbResult<LaneAttendance> result;
            if (null == value)
            {
                result = new NDbResult<LaneAttendance>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = LaneAttendance.Create(value.Lane, value.User);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.SaveAttendance.Name)]
        public NDbResult<LaneAttendance> SaveAttendance([FromBody] LaneAttendance value)
        {
            NDbResult<LaneAttendance> result;
            if (null == value)
            {
                result = new NDbResult<LaneAttendance>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                Random rand = new Random();
                if (string.IsNullOrWhiteSpace(value.JobId))
                {
                    value.JobId = rand.Next(100000).ToString("D5"); // auto generate.
                }
                result = LaneAttendance.Save(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetAttendancesByDate.Name)]
        public NDbResult<List<LaneAttendance>> GetAttendancesByDate([FromBody] Search.Lanes.Attendances.ByDate value)
        {
            NDbResult<List<LaneAttendance>> result;
            if (null == value)
            {
                result = new NDbResult<List<LaneAttendance>>();
                result.ParameterIsNull();
                result.data = new List<LaneAttendance>();
            }
            else
            {
                if (null == value) return new List<LaneAttendance>();
                result = LaneAttendance.Search(value.Date);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetAttendancesByUserShift.Name)]
        public NDbResult<List<LaneAttendance>> GetAttendancesByUserShift([FromBody] Search.Lanes.Attendances.ByUserShift value)
        {
            NDbResult<List<LaneAttendance>> result;
            if (null == value)
            {
                result = new NDbResult<List<LaneAttendance>>();
                result.ParameterIsNull();
                result.data = new List<LaneAttendance>();
            }
            else
            {
                result = LaneAttendance.Search(value.Shift, value.PlazaGroup, value.RevenueDate);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetAllAttendancesByUserShift.Name)]
        public NDbResult<List<LaneAttendance>> GetAllAttendancesByUserShift([FromBody] UserShift value)
        {
            NDbResult<List<LaneAttendance>> result;
            if (null == value)
            {
                result = new NDbResult<List<LaneAttendance>>();
                result.ParameterIsNull();
                result.data = new List<LaneAttendance>();
            }
            else
            {
                result = LaneAttendance.Search(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetAttendancesByLane.Name)]
        public NDbResult<List<LaneAttendance>> GetAttendancesByLane([FromBody] Search.Lanes.Attendances.ByLane value)
        {
            NDbResult<List<LaneAttendance>> result;
            if (null == value)
            {
                result = new NDbResult<List<LaneAttendance>>();
                result.ParameterIsNull();
                result.data = new List<LaneAttendance>();
            }
            else
            {
                result = LaneAttendance.Search(value.Lane);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetCurrentAttendancesByLane.Name)]
        public NDbResult<LaneAttendance> GetCurrentAttendancesByLane([FromBody] Search.Lanes.Current.AttendanceByLane value)
        {
            NDbResult<LaneAttendance> result;
            if (null == value)
            {
                result = new NDbResult<LaneAttendance>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = LaneAttendance.GetCurrentByLane(value.Lane);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetAllNotHasRevenueEntry.Name)]
        public NDbResult<List<LaneAttendance>> GetAllNotHasRevenueEntry()
        {
            NDbResult<List<LaneAttendance>> result;
            result = LaneAttendance.GetAllNotHasRevenueEntry();
            return result;
        }

        #endregion

        #region Lane Payment

        [HttpPost]
        [ActionName(RouteConsts.Lane.SavePayment.Name)]
        public NDbResult<LanePayment> Create([FromBody] LanePaymentCreate value)
        {
            NDbResult<LanePayment> result;
            if (null == value)
            {
                result = new NDbResult<LanePayment>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = LanePayment.Create(value.Lane, value.User,
                    value.Payment, value.Date, value.Amount);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.SavePayment.Name)]
        public NDbResult<LanePayment> SavePayment([FromBody] LanePayment value)
        {
            NDbResult<LanePayment> result;
            if (null == value)
            {
                result = new NDbResult<LanePayment>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                Random rand = new Random();
                if (string.IsNullOrWhiteSpace(value.ApproveCode))
                {
                    value.ApproveCode = rand.Next(10000000).ToString("D8"); // auto generate.
                }
                result = LanePayment.Save(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetPaymentsByDate.Name)]
        public NDbResult<List<LanePayment>> GetPaymentsByDate([FromBody] Search.Lanes.Payments.ByDate value)
        {
            NDbResult<List<LanePayment>> result;
            if (null == value)
            {
                result = new NDbResult<List<LanePayment>>();
                result.ParameterIsNull();
                result.data = new List<LanePayment>();
            }
            else
            {
                result = LanePayment.Search(value.Date);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetPaymentsByUserShift.Name)]
        public NDbResult<List<LanePayment>> GetPaymentsByUserShifts([FromBody] Search.Lanes.Payments.ByUserShift value)
        {
            NDbResult<List<LanePayment>> result;
            if (null == value)
            {
                result = new NDbResult<List<LanePayment>>();
                result.ParameterIsNull();
                result.data = new List<LanePayment>();
            }
            else
            {
                result = LanePayment.Search(value.Shift);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetPaymentsByLane.Name)]
        public NDbResult<List<LanePayment>> GetPaymentsByLane([FromBody] Search.Lanes.Payments.ByLane value)
        {
            NDbResult<List<LanePayment>> result;
            if (null == value)
            {
                result = new NDbResult<List<LanePayment>>();
                result.ParameterIsNull();
                result.data = new List<LanePayment>();
            }
            else
            {
                result = LanePayment.Search(value.Lane);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetCurrentPaymentsByLane.Name)]
        public NDbResult<LanePayment> GetCurrentPaymentsByLane([FromBody] Search.Lanes.Current.PaymentByLane value)
        {
            NDbResult<LanePayment> result;
            if (null == value)
            {
                result = new NDbResult<LanePayment>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = LanePayment.GetCurrentByLane(value.Lane);
            }
            return result;
        }

        #endregion
    }
}
