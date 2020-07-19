#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DMT.Models;
using DMT.Models.ExtensionMethods;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// The controller for manage coupon transaction TA.
    /// </summary>
    public class CouponController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetInitial.Name)]
        public TSBCouponTransaction GetInitial()
        {
            return TSBCouponTransaction.GetInitial();
        }
    }
}
