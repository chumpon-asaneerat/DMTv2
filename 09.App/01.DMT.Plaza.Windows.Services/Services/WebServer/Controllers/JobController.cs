﻿#region Using

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
    /// The controller for handle Collector Begin Job (start TOD shift) and
    /// Get List of Lane Attendance on specificed Job (between Begin to End).
    /// </summary>
    public class JobController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Job.Create.Name)]
        public UserShift Create([FromBody] UserShiftCreate value)
        {
            if (null == value) return null;
            return UserShift.Create(value.Shift, value.User);
        }

        [HttpPost]
        [ActionName(RouteConsts.Job.GetCurrent.Name)]
        public UserShift GetCurrent([FromBody] User value)
        {
            if (null == value) return null;
            return UserShift.GetCurrent(value.UserId);
        }

        [HttpPost]
        [ActionName(RouteConsts.Job.BeginJob.Name)]
        public bool BeginJob(UserShift shift)
        {
            if (null == shift) return false;
            return UserShift.BeginJob(shift);
        }

        [HttpPost]
        [ActionName(RouteConsts.Job.EndJob.Name)]
        public void EndJob(UserShift shift)
        {
            if (null == shift) return;
            UserShift.EndJob(shift);
        }
    }
}
