#region Using

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

#endregion

namespace DMT.Services
{
    public class SearchOptions
    {
        public bool Active { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }

    public class Supervisor : User
    {
        public bool Active { get; set; }
    }

    public class Collector : User
    {
        public string SupervisorId { get; set; }
        public bool Active { get; set; }
    }

    public class SupervisorController : ApiController
    {
        private List<User> users = new List<User>();

        public SupervisorController()
        {
            var items = new User[] {
                new Supervisor() { Id = "1", Name = "Sup1", Active = false },
                new Collector() { Id = "1", SupervisorId = "1" },
                new Collector() { Id = "2", SupervisorId = "1" },
                new Collector() { Id = "3", SupervisorId = "1" },

                new Supervisor() { Id = "2", Name = "Sup2", Active = true },
                new Collector() { Id = "3", SupervisorId = "2" },
                new Collector() { Id = "4", SupervisorId = "2" },
                new Collector() { Id = "5", SupervisorId = "2" }
            };
            users.AddRange(items);
        }

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
    }
}
