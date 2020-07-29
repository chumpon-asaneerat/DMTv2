#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DMT.Models;
//using DMT.Models.ExtensionMethods;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// The controller for manage common data on TSB, Plaza and Lane.
    /// </summary>
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
        [ActionName(RouteConsts.TSB.GetTSBPlazaGroups.Name)]
        public List<PlazaGroup> GetTSBPlazaGroups([FromBody] TSB value)
        {
            if (null == value) return new List<PlazaGroup>();
            var results = PlazaGroup.GetTSBPlazaGroups(value);
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetPlazaGroupLanes.Name)]
        public List<Lane> GetPlazaGroupLanes([FromBody] PlazaGroup value)
        {
            if (null == value) return new List<Lane>();
            var results = Lane.GetPlazaGroupLanes(value);
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetTSBPlazas.Name)]
        public List<Plaza> GetTSBPlazas([FromBody] TSB value)
        {
            if (null == value) return new List<Plaza>();
            var results = Plaza.GetTSBPlazas(value);
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetTSBLanes.Name)]
        public List<Lane> GetTSBLanes([FromBody] TSB value)
        {
            if (null == value) return new List<Lane>();
            var results = Lane.GetTSBLanes(value);
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetPlazaLanes.Name)]
        public List<Lane> GetPlazaLanes([FromBody] Plaza value)
        {
            if (null == value) return new List<Lane>();
            var results = Lane.GetPlazaLanes(value);
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.SetActive.Name)]
        public void SetActive([FromBody] TSB value)
        {
            if (null == value) return;
            TSB.SetActive(value.TSBId);
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetCurrent.Name)]
        public TSB GetCurrent()
        {
            var results = TSB.GetCurrent();
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.SaveTSB.Name)]
        public TSB SaveTSB([FromBody] TSB value)
        {
            if (null != value)
            {
                TSB.Save(value);
            }
            return value;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.SavePlazaGroup.Name)]
        public PlazaGroup SavePlazaGroup([FromBody] PlazaGroup value)
        {
            if (null != value)
            {
                PlazaGroup.Save(value);
            }
            return value;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.SavePlaza.Name)]
        public Plaza SavePlaza([FromBody] Plaza value)
        {
            if (null != value)
            {
                Plaza.Save(value);
            }
            return value;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.SaveLane.Name)]
        public Lane SaveLane([FromBody] Lane value)
        {
            if (null != value)
            {
                Lane.Save(value);
            }
            return value;
        }
    }
}
