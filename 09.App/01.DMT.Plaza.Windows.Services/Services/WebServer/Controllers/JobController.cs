#region Using

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

#endregion

namespace DMT.Services
{
    public class JobController : ApiController
    {
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
