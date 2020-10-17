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
    public class MasterController : ApiController
    {
        #region MCurrency

        [HttpPost]
        [ActionName(RouteConsts.Master.GetCurrencies.Name)]
        public NDbResult<List<MCurrency>> GetCurrencies()
        {
            var results = MCurrency.GetCurrencies();
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.Master.SaveMCurrencies.Name)]
        public NDbResult SaveMCurrencies([FromBody] List<MCurrency> values)
        {
            var results = MCurrency.SaveMCurrencies(values);
            return results;
        }

        #endregion

        #region MCoupon

        [HttpPost]
        [ActionName(RouteConsts.Master.GetCoupons.Name)]
        public NDbResult<List<MCoupon>> GetCoupons()
        {
            var results = MCoupon.GetCoupons();
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.Master.SaveMCoupons.Name)]
        public NDbResult SaveMCoupons([FromBody] List<MCoupon> values)
        {
            var results = MCoupon.SaveMCoupons(values);
            return results;
        }

        #endregion

        #region MCardAllow

        [HttpPost]
        [ActionName(RouteConsts.Master.GetCardAllows.Name)]
        public NDbResult<List<MCardAllow>> GetCardAllows()
        {
            var results = MCardAllow.GetCardAllows();
            return results;
        }

        [HttpPost]
        [ActionName(RouteConsts.Master.SaveMCardAllows.Name)]
        public NDbResult SaveMCardAllows([FromBody] List<MCardAllow> values)
        {
            var results = MCardAllow.SaveMCardAllows(values);
            return results;
        }

        #endregion
    }
}
