#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DMT.Models;

#endregion

namespace DMT.Services
{
    public class TSBController : ApiController
    {
        public TSBController()
        {
            Console.WriteLine("TSBController created.");
        }
        [HttpPost]
        [ActionName(RouteConsts.TSB.GetTSBs.Name)]
        public List<TSB> GetTSBs()
        {
            var results = TSB.Gets();
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetTSBPlazas.Name)]
        public List<Plaza> GetTSBPlazas([FromBody] TSB value)
        {
            var results = value.GetPlazas();
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetTSBLanes.Name)]
        public List<Lane> GetTSBLanes([FromBody] TSB value)
        {
            var results = value.GetLanes();
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetPlazaLanes.Name)]
        public List<Lane> GetPlazaLanes([FromBody] Plaza value)
        {
            var results = value.GetLanes();
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.SetActive.Name)]
        public void SetActive([FromBody] TSB value)
        {
            value.SetActive();
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetShifts.Name)]
        public List<Shift> GetShifts()
        {
            var results = Shift.Gets();
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetRoles.Name)]
        public List<Role> GetRoles()
        {
            var results = Role.Gets();
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetUsers.Name)]
        public List<User> GetUsers(Role value)
        {
            int status = 1; // active only
            var results = value.GetUsers(status);
            return results;
        }
    }
}
