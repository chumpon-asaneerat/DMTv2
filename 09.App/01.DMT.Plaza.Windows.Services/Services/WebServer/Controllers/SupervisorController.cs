#region Using

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

#endregion

namespace DMT.Services
{
    public class SupervisorController : ApiController
    {
        public SupervisorController()
        {
        }
        /*
        [HttpPost]
        [ActionName("ActiveSups")]
        public List<Supervisor> GetSupervisors([FromBody] SearchOptions options)
        {
            List<Supervisor> results = new List<Supervisor>();

            var matchs = users.FindAll(
                p => p is Supervisor && (p as Supervisor).Active == options.Active
            ).ToList();

            matchs.ForEach(p =>
            {
                if (p is Supervisor) results.Add(p as Supervisor);
            });

            return results;
        }
        [HttpPost]
        [ActionName("ListCollectorsUnderSup")]
        public List<Collector> GetCollectors([FromBody] Supervisor sup)
        {
            List<Collector> results = new List<Collector>();

            if (null != sup)
            {
                var matchs = users.FindAll(
                    p => p is Collector && (p as Collector).SupervisorId == sup.Id
                ).ToList();
                matchs.ForEach(p =>
                {
                    if (p is Collector) results.Add(p as Collector);
                });
            }
            else
            {
                var matchs = users.FindAll(
                    p => p is Collector
                ).ToList();
                matchs.ForEach(p =>
                {
                    if (p is Collector) results.Add(p as Collector);
                });
            }

            return results;
        }
        */
    }
}
