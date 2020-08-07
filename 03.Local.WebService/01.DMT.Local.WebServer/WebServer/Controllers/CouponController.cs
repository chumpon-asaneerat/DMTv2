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
        #region TSB Coupon Balance

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetTSBBalance.Name)]
        public TSBCouponBalance GetTSBBalance([FromBody] TSB tsb)
        {
            if (null == tsb)
                return TSBCouponBalance.GetTSBBalance();
            return TSBCouponBalance.GetTSBBalance(tsb);
        }

        #endregion

        #region TSB Coupon Summary

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetTSBCouponSummaries.Name)]
        public List<TSBCouponSummary> GetTSBCouponSummaries([FromBody] TSB tsb)
        {
            if (null == tsb)
                return TSBCouponSummary.GetTSBCouponSummaries();
            return TSBCouponSummary.GetTSBCouponSummaries(tsb);
        }

        #endregion

        #region TSB Coupon Transaction

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetTSBCouponTransactions.Name)]
        public List<TSBCouponTransaction> GetTSBCouponTransactions([FromBody] TSB tsb)
        {
            if (null == tsb)
                return TSBCouponTransaction.GetTSBCouponTransactions();
            return TSBCouponTransaction.GetTSBCouponTransactions(tsb);
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.SaveTSBCouponTransaction.Name)]
        public void SaveTransaction([FromBody] TSBCouponTransaction value)
        {
            TSBCouponTransaction.SaveTransaction(value);
        }

        #endregion


        /*
        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetCurrentTSBCoupons.Name)]
        public List<TSBCouponTransaction> GetCurrentTSBCoupons()
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
        [ActionName(RouteConsts.Coupon.GetTSBBHT35Coupons.Name)]
        public List<TSBCouponTransaction> GetTSBBHT35Coupons([FromBody] TSB tsb)
        {
            return TSBCouponTransaction.GetTSBBHT35Coupons(tsb);
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetTSBBHT80Coupons.Name)]
        public List<TSBCouponTransaction> GetTSBBHT80Coupons([FromBody] TSB tsb)
        {
            return TSBCouponTransaction.GetTSBBHT80Coupons(tsb);
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.ToTSBBHT35Coupons.Name)]
        public List<TSBCouponTransaction> ToTSBBHT35Coupons(
            [FromBody] Search.UserCoupons.ToTSBCoupons value)
        {
            if (null == value) return new List<TSBCouponTransaction>();
            return TSBCouponTransaction.ToTSBBHT35Coupons(value.TSB, value.Coupons);
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.ToTSBBHT80Coupons.Name)]
        public List<TSBCouponTransaction> ToTSBBHT80Coupons(
            [FromBody] Search.UserCoupons.ToTSBCoupons value)
        {
            if (null == value) return new List<TSBCouponTransaction>();
            return TSBCouponTransaction.ToTSBBHT80Coupons(value.TSB, value.Coupons);
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetUserBHT35Coupons.Name)]
        public List<UserCouponTransaction> GetUserBHT35Coupons(
            [FromBody] Search.UserCoupons.ByUser value)
        {
            if (null == value) return new List<UserCouponTransaction>();
            return UserCouponTransaction.GetUserBHT35Coupons(value.TSB, value.User);
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetUserBHT80Coupons.Name)]
        public List<UserCouponTransaction> GetUserBHT80Coupons(
            [FromBody] Search.UserCoupons.ByUser value)
        {
            if (null == value) return new List<UserCouponTransaction>();
            return UserCouponTransaction.GetUserBHT80Coupons(value.TSB, value.User);
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
        public List<TSBCouponBalance> GetCurrent()
        {
            return TSBCouponBalance.GetCurrent();
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetTSBCurrent.Name)]
        public List<TSBCouponBalance> GetTSBCurrent([FromBody] TSB tsb)
        {
            return TSBCouponBalance.GetCurrent(tsb);
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.UserBorrowCoupons.Name)]
        public void UserBorrowCoupons([FromBody] Search.UserCoupons.BorrowCoupons value)
        {
            if (null == value) return;
            UserCouponTransaction.UserBorrowCoupons(value.User, value.Coupons);
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.UserReturnCoupons.Name)]
        public void UserReturnCoupons([FromBody] Search.UserCoupons.ReturnCoupons value)
        {
            if (null == value) return;
            UserCouponTransaction.UserReturnCoupons(value.User, value.Coupons);
        }
        */
    }
}
