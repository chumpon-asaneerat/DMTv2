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
    /// The controller for manage coupon transaction TA.
    /// </summary>
    public class CouponController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetCurrentTSBCoupons.Name)]
        public List<TSBCouponTransaction> GetTSBCoupons()
        {
            return TSBCouponTransaction.GetTSBCoupons();
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetTSBCoupons.Name)]
        public List<TSBCouponTransaction> GetTSBCoupons([FromBody] TSB tsb)
        {
            return TSBCouponTransaction.GetTSBCoupons(tsb);
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetCurrentTSBSoldCoupons.Name)]
        public List<TSBCouponTransaction> GetCurrentTSBSoldCoupons()
        {
            return TSBCouponTransaction.GetTSBSoldCoupons();
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetTSBSoldCoupons.Name)]
        public List<TSBCouponTransaction> GetTSBSoldCoupons([FromBody] TSB tsb)
        {
            return TSBCouponTransaction.GetTSBSoldCoupons(tsb);
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

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetCurrent.Name)]
        public TSBCouponBalance GetCurrent()
        {
            return TSBCouponBalance.GetCurrent();
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetTSBCurrent.Name)]
        public TSBCouponBalance GetTSBCurrent([FromBody] TSB tsb)
        {
            return TSBCouponBalance.GetCurrent(tsb);
        }
    }
}
