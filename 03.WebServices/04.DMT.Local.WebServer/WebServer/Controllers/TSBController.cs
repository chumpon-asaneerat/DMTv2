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
        #region TSB

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetTSBs.Name)]
        public NDbResult<List<TSB>> GetTSBs()
        {
            var results = TSB.Gets();
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetCurrent.Name)]
        public NDbResult<TSB> GetCurrent()
        {
            var result = TSB.GetCurrent();
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.SetActive.Name)]
        public NDbResult SetActive([FromBody] TSB value)
        {
            if (null == value) return new NDbResult();
            var result = TSB.SetActive(value.TSBId);
            // Raise event.
            LocalDbServer.Instance.ActiveTSBChanged();
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.SaveTSB.Name)]
        public NDbResult<TSB> SaveTSB([FromBody] TSB value)
        {
            if (null != value)
            {
                return TSB.Save(value);
            }
            return new NDbResult<TSB>();
        }

        #endregion

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetTSBPlazaGroups.Name)]
        public NDbResult<List<PlazaGroup>> GetTSBPlazaGroups([FromBody] TSB value)
        {
            if (null == value) return new List<PlazaGroup>();
            var results = PlazaGroup.GetTSBPlazaGroups(value);
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetPlazaGroupLanes.Name)]
        public NDbResult<List<Lane>> GetPlazaGroupLanes([FromBody] PlazaGroup value)
        {
            if (null == value) return new List<Lane>();
            var results = Lane.GetPlazaGroupLanes(value);
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetTSBPlazas.Name)]
        public NDbResult<List<Plaza>> GetTSBPlazas([FromBody] TSB value)
        {
            if (null == value) return new List<Plaza>();
            var results = Plaza.GetTSBPlazas(value);
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetTSBLanes.Name)]
        public NDbResult<List<Lane>> GetTSBLanes([FromBody] TSB value)
        {
            if (null == value) return new List<Lane>();
            var results = Lane.GetTSBLanes(value);
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.GetPlazaLanes.Name)]
        public NDbResult<List<Lane>> GetPlazaLanes([FromBody] Plaza value)
        {
            if (null == value) return new List<Lane>();
            var results = Lane.GetPlazaLanes(value);
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.SavePlazaGroup.Name)]
        public NDbResult<PlazaGroup> SavePlazaGroup([FromBody] PlazaGroup value)
        {
            if (null != value)
            {
                PlazaGroup.Save(value);
            }
            return value;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.SavePlaza.Name)]
        public NDbResult<Plaza> SavePlaza([FromBody] Plaza value)
        {
            if (null != value)
            {
                Plaza.Save(value);
            }
            return value;
        }

        [HttpPost]
        [ActionName(RouteConsts.TSB.SaveLane.Name)]
        public NDbResult<Lane> SaveLane([FromBody] Lane value)
        {
            if (null != value)
            {
                Lane.Save(value);
            }
            return value;
        }
    }
}
