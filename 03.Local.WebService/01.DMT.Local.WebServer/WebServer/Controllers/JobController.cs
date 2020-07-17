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
    /// The controller for handle Collector Begin UserShift (start TOD shift) and
    /// Get List of Lane Attendance on specificed UserShift (between Begin to End).
    /// </summary>
    public class UserShiftController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.UserShift.Create.Name)]
        public UserShift Create([FromBody] UserShiftCreate value)
        {
            if (null == value) return null;
            return UserShift.Create(value.Shift, value.User);
        }

        [HttpPost]
        [ActionName(RouteConsts.UserShift.GetCurrent.Name)]
        public UserShift GetCurrent([FromBody] User value)
        {
            if (null == value) return null;
            return UserShift.GetCurrent(value.UserId);
        }

        [HttpPost]
        [ActionName(RouteConsts.UserShift.BeginUserShift.Name)]
        public bool BeginUserShift([FromBody] UserShift shift)
        {
            if (null == shift) return false;
            return UserShift.BeginUserShift(shift);
        }

        [HttpPost]
        [ActionName(RouteConsts.UserShift.EndUserShift.Name)]
        public void EndUserShift([FromBody] UserShift shift)
        {
            if (null == shift) return;
            UserShift.EndUserShift(shift);
        }

        [HttpPost]
        [ActionName(RouteConsts.UserShift.GetUserShifts.Name)]
        public List<UserShift> GetUserShifts([FromBody] User value)
        {
            if (null == value) return new List<UserShift>();
            return UserShift.GetUserShifts(value.UserId);
        }

        [HttpPost]
        [ActionName(RouteConsts.UserShift.GetUnCloseUserShifts.Name)]
        public List<UserShift> GetUnCloseUserShifts()
        {
            return UserShift.GetUnCloseUserShifts();
        }
    }
}
