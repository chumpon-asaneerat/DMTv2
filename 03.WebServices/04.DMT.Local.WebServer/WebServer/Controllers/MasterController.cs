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

        #endregion

        #region MCoupon

        [HttpPost]
        [ActionName(RouteConsts.Master.GetCoupons.Name)]
        public NDbResult<List<MCoupon>> GetCoupons()
        {
            var results = MCoupon.GetCoupons();
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

        #endregion
    }
}
