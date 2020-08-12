#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NLib.Reflection;

using DMT.Models;
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
        public LaneAttendance Create([FromBody] LaneAttendanceCreate value)
        {
            if (null == value) return null;
            return LaneAttendance.Create(value.Lane, value.User);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.SaveAttendance.Name)]
        public void SaveAttendance([FromBody] LaneAttendance value)
        {
            if (null == value) return;
            Random rand = new Random();
            if (string.IsNullOrWhiteSpace(value.JobId))
            {
                value.JobId = rand.Next(100000).ToString("D5"); // auto generate.
            }
            LaneAttendance.Save(value);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetAttendancesByDate.Name)]
        public List<LaneAttendance> GetAttendancesByDate([FromBody] Search.Lanes.Attendances.ByDate value)
        {
            if (null == value) return new List<LaneAttendance>();
            return LaneAttendance.Search(value.Date);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetAttendancesByUserShift.Name)]
        public List<LaneAttendance> GetAttendancesByUserShift([FromBody] Search.Lanes.Attendances.ByUserShift value)
        {
            if (null == value) return new List<LaneAttendance>();
            return LaneAttendance.Search(value.Shift, value.PlazaGroup, value.RevenueDate);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetAllAttendancesByUserShift.Name)]
        public List<LaneAttendance> GetAllAttendancesByUserShift([FromBody] UserShift value)
        {
            if (null == value) return new List<LaneAttendance>();
            return LaneAttendance.Search(value);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetAttendancesByLane.Name)]
        public List<LaneAttendance> GetAttendancesByLane([FromBody] Search.Lanes.Attendances.ByLane value)
        {
            if (null == value) return new List<LaneAttendance>();
            return LaneAttendance.Search(value.Lane);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetCurrentAttendancesByLane.Name)]
        public LaneAttendance GetCurrentAttendancesByLane([FromBody] Search.Lanes.Current.AttendanceByLane value)
        {
            if (null == value) return null;
            return LaneAttendance.GetCurrentByLane(value.Lane);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetAllNotHasRevenueEntry.Name)]
        public List<LaneAttendance> GetAllNotHasRevenueEntry()
        {
            return LaneAttendance.GetAllNotHasRevenueEntry();
        }

        #endregion

        #region Lane Payment

        [HttpPost]
        [ActionName(RouteConsts.Lane.SavePayment.Name)]
        public LanePayment Create([FromBody] LanePaymentCreate value)
        {
            if (null == value) return null;
            return LanePayment.Create(value.Lane, value.User, 
                value.Payment, value.Date, value.Amount);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.SavePayment.Name)]
        public void SavePayment([FromBody] LanePayment value)
        {
            if (null == value) return;
            Random rand = new Random();
            if (string.IsNullOrWhiteSpace(value.ApproveCode))
            {
                value.ApproveCode = rand.Next(10000000).ToString("D8"); // auto generate.
            }
            LanePayment.Save(value);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetPaymentsByDate.Name)]
        public List<LanePayment> GetPaymentsByDate([FromBody] Search.Lanes.Payments.ByDate value)
        {
            if (null == value) return new List<LanePayment>();
            return LanePayment.Search(value.Date);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetPaymentsByUserShift.Name)]
        public List<LanePayment> GetPaymentsByUserShifts([FromBody] Search.Lanes.Payments.ByUserShift value)
        {
            if (null == value) return new List<LanePayment>();
            return LanePayment.Search(value.Shift);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetPaymentsByLane.Name)]
        public List<LanePayment> GetPaymentsByLane([FromBody] Search.Lanes.Payments.ByLane value)
        {
            if (null == value) return new List<LanePayment>();
            return LanePayment.Search(value.Lane);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetCurrentPaymentsByLane.Name)]
        public LanePayment GetCurrentPaymentsByLane([FromBody] Search.Lanes.Current.PaymentByLane value)
        {
            if (null == value) return null;
            return LanePayment.GetCurrentByLane(value.Lane);
        }

        #endregion
    }
}
