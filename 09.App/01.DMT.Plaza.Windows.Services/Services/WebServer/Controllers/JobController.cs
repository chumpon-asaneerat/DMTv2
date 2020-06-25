#region Using

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NLib.Reflection;

#endregion

namespace DMT.Services
{
    public class JobController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Job.GetUser.Name)]
        public Models.User GetUser([FromBody] Models.User user)
        {
            Models.User oUser = null;
            var dUser = Models.User.Get(user.UserId);
            if (null != dUser)
            {
                oUser = dUser.CloneTo<Models.User>();
            }
            return oUser;
        }

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
