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
        public NDbResult<TSBCouponBalance> GetTSBBalance([FromBody] TSB value)
        {
            NDbResult<TSBCouponBalance> result;
            if (null == value)
            {
                result = new NDbResult<TSBCouponBalance>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBCouponBalance.GetTSBBalance(value);
            }
            return result;
        }

        #endregion

        #region TSB Coupon Summary

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetTSBCouponSummaries.Name)]
        public NDbResult<List<TSBCouponSummary>> GetTSBCouponSummaries([FromBody] TSB value)
        {
            NDbResult<List<TSBCouponSummary>> result;
            if (null == value)
            {
                result = new NDbResult<List<TSBCouponSummary>>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBCouponSummary.GetTSBCouponSummaries(value);
            }
            return result;
        }

        #endregion

        #region TSB Coupon Transaction

        [HttpPost]
        [ActionName(RouteConsts.Coupon.GetTSBCouponTransactions.Name)]
        public NDbResult<List<TSBCouponTransaction>> GetTSBCouponTransactions([FromBody] TSB value)
        {
            NDbResult<List<TSBCouponTransaction>> result;
            if (null == value)
            {
                result = new NDbResult<List<TSBCouponTransaction>>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBCouponTransaction.GetTSBCouponTransactions(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.SaveTSBCouponTransaction.Name)]
        public NDbResult<TSBCouponTransaction> SaveTransaction(
            [FromBody] TSBCouponTransaction value)
        {
            NDbResult<TSBCouponTransaction> result;
            if (null == value)
            {
                result = new NDbResult<TSBCouponTransaction>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBCouponTransaction.SaveTransaction(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.SaveTSBCouponTransactions.Name)]
        public NDbResult SaveTransactions([FromBody] List<TSBCouponTransaction> values)
        {
            NDbResult result;
            if (null == values)
            {
                result = new NDbResult();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBCouponTransaction.SaveTransactions(values);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.SyncTSBCouponTransaction.Name)]
        public NDbResult SyncTransaction(
            [FromBody] TSBCouponTransaction value)
        {
            NDbResult result;
            if (null == value)
            {
                result = new NDbResult();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBCouponTransaction.SyncTransaction(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Coupon.SyncTSBCouponTransactions.Name)]
        public NDbResult SyncTransactions(
            [FromBody] List<TSBCouponTransaction> values)
        {
            NDbResult result;
            if (null == values)
            {
                result = new NDbResult();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBCouponTransaction.SyncTransactions(values);
            }
            return result;
        }

        #endregion
    }
}
