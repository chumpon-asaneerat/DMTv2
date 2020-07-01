#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NLib.Reflection;

using DMT.Models;
using DMT.Models.ExtensionMethods;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// The controller for handle Lane Attendance (Attendance/Leave).
    /// </summary>
    public class LaneController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Lane.CreateAttendance.Name)]
        public LaneAttendance Create([FromBody] LaneAttendanceCreate value)
        {
            if (null == value) return null;
            return LaneAttendance.Create(value.Lane, value.User);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.SavePayment.Name)]
        public LanePayment Create([FromBody] LanePaymentCreate value)
        {
            if (null == value) return null;
            return LanePayment.Create(value.Lane, value.User);
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
        [ActionName(RouteConsts.Lane.GetAttendancesByDate.Name)]
        public List<LaneAttendance> GetAttendancesByDate([FromBody] Search.Lanes.Attendances.ByDate value)
        {
            if (null == value) return new List<LaneAttendance>();
            return LaneAttendance.Search(value.Date);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetAttendancesByShift.Name)]
        public List<LaneAttendance> GetAttendancesByDate([FromBody] Search.Lanes.Attendances.ByShift value)
        {
            if (null == value) return new List<LaneAttendance>();
            return LaneAttendance.Search(value.Shift);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetPaymentsByDate.Name)]
        public List<LanePayment> GetPaymentsByDate([FromBody] Search.Lanes.Payments.ByDate value)
        {
            if (null == value) return new List<LanePayment>();
            return LanePayment.Search(value.Date);
        }

        [HttpPost]
        [ActionName(RouteConsts.Lane.GetPaymentsByShift.Name)]
        public List<LanePayment> GetPaymentsByDate([FromBody] Search.Lanes.Payments.ByShift value)
        {
            if (null == value) return new List<LanePayment>();
            return LanePayment.Search(value.Shift);
        }
    }
}
