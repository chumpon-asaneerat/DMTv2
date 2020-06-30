#region Using

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NLib.Reflection;

using DMT.Models;
using System;

#endregion

namespace DMT.Services
{
    public class LaneController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Job.Create.Name)]
        public LaneAttendance Create([FromBody] LaneAttendanceCreate value)
        {
            if (null == value) return null;
            return LaneAttendance.Create(value.Lane, value.User);
        }
    }
}
