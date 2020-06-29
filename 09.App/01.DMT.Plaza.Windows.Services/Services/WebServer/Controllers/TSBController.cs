﻿#region Using

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
        [ActionName(RouteConsts.TSB.GetCurrent.Name)]
        public TSB GetCurrent()
        {
            var results = TSB.GetCurrent();
            return results;
        }
    }
}
