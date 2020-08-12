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
    /// The controller for handle Collector Begin UserShift (start TOD shift) and
    /// Get List of Lane Attendance on specificed UserShift (between Begin to End).
    /// </summary>
    public class UserShiftController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.UserShift.Create.Name)]
        public NDbResult<UserShift> Create([FromBody] UserShiftCreate value)
        {
            NDbResult<UserShift> result;

            if (null == value)
            {
                result = new NDbResult<UserShift>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                result = UserShift.Create(value.Shift, value.User);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.UserShift.GetCurrent.Name)]
        public NDbResult<UserShift> GetCurrent([FromBody] User value)
        {
            NDbResult<UserShift> result;

            if (null == value)
            {
                result = new NDbResult<UserShift>();
                result.ParameterIsNull();
            }
            else
            {
                result = UserShift.GetCurrent(value.UserId);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.UserShift.BeginUserShift.Name)]
        public NDbResult BeginUserShift([FromBody] UserShift value)
        {
            NDbResult result;

            if (null == value)
            {
                result = new NDbResult<Shift>();
                result.ParameterIsNull();
            }
            else
            {
                result = UserShift.BeginUserShift(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.UserShift.EndUserShift.Name)]
        public NDbResult EndUserShift([FromBody] UserShift value)
        {
            NDbResult result;

            if (null == value)
            {
                result = new NDbResult<Shift>();
                result.ParameterIsNull();
            }
            else
            {
                result = UserShift.EndUserShift(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.UserShift.GetUserShifts.Name)]
        public NDbResult<List<UserShift>> GetUserShifts([FromBody] User value)
        {
            NDbResult<List<UserShift>> result;
            if (null == value)
            {
                result = new NDbResult<List<UserShift>>();
                result.ParameterIsNull();
                result.data = new List<UserShift>();
            }
            else
            {
                result = UserShift.GetUserShifts(value.UserId);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.UserShift.GetUnCloseUserShifts.Name)]
        public NDbResult<List<UserShift>> GetUnCloseUserShifts()
        {
            NDbResult<List<UserShift>> result;
            result = UserShift.GetUnCloseUserShifts();
            return result;
        }
    }
}
