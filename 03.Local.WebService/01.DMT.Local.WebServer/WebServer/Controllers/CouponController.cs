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
        [ActionName(RouteConsts.Coupon.GetCurrentInitial.Name)]
        public TSBCouponTransaction GetCurrentInitial()
        {
            return TSBCouponTransaction.GetInitial();
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetInitial.Name)]
        public TSBCouponTransaction GetInitial([FromBody] TSB tsb)
        {
            return TSBCouponTransaction.GetInitial(tsb);
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.SaveTransaction.Name)]
        public void SaveTransaction([FromBody] TSBCouponTransaction value)
        {
            if (value.TransactionDate == DateTime.MinValue)
            {
                value.TransactionDate = DateTime.Now;
            }
            TSBCouponTransaction.Save(value);
        }
    }
}
