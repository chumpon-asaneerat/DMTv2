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
    public class JobController : ApiController
    {
        public JobController()
        {
            Console.WriteLine("JobController created");
        }
        /*
        [HttpPost]
        [ActionName(RouteConsts.Job.GetUser.Name)]
        public User GetUser([FromBody] User user)
        {
            User oUser = null;
            var dUser = User.Get(user.UserId);
            if (null != dUser)
            {
                oUser = dUser.CloneTo<Models.User>();
            }
            return oUser;
        }
        */
        [HttpPost]
        [ActionName(RouteConsts.Job.BeginJob.Name)]
        public string BeginJob([FromBody] Collector collector)
        {
            return collector.Name + " is Begin Job";
        }
        [HttpPost]
        [ActionName(RouteConsts.Job.EndJob.Name)]
        public string EndJob([FromBody] Collector collector)
        {
            return collector.Name + " is End Job";
        }
    }
}
