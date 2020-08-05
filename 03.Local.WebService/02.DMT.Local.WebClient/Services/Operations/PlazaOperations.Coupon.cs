#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;

#endregion

namespace DMT.Services
{
    partial class PlazaOperations
    {
        #region Internal Variables

        private CouponOperations _Coupon_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Coupons Operations.
        /// </summary>
        public CouponOperations Coupons
        {
            get
            {
                if (null == _Coupon_Ops)
                {
                    lock (this)
                    {
                        _Coupon_Ops = new CouponOperations();
                    }
                }
                return _Coupon_Ops;
            }
        }

        #endregion

        #region CouponOperations

        public class CouponOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal CouponOperations() : base() { }

            #endregion

            #region Public Methods

            public List<TSBCouponTransaction> GetCurrentTSBCoupons()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.GetCurrentTSBCoupons.Url, new { });
                return ret;
            }

            public List<TSBCouponTransaction> GetTSBCoupons(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.GetTSBCoupons.Url, tsb);
                return ret;
            }

            public List<TSBCouponTransaction> GetTSBBHT35Coupons(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.GetTSBBHT35Coupons.Url, tsb);
                return ret;
            }

            public List<TSBCouponTransaction> GetTSBBHT80Coupons(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.GetTSBBHT80Coupons.Url, tsb);
                return ret;
            }

            public List<TSBCouponTransaction> ToTSBBHT35Coupons(Search.UserCoupons.ToTSBCoupons value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.ToTSBBHT35Coupons.Url, value);
                return ret;
            }

            public List<TSBCouponTransaction> ToTSBBHT80Coupons(Search.UserCoupons.ToTSBCoupons value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.ToTSBBHT80Coupons.Url, value);
                return ret;
            }

            public List<UserCouponTransaction> GetUserBHT35Coupons(
                Search.UserCoupons.ByUser value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<UserCouponTransaction>>(
                    RouteConsts.Coupon.GetUserBHT35Coupons.Url, value);
                return ret;
            }

            public List<UserCouponTransaction> GetUserBHT80Coupons(
                Search.UserCoupons.ByUser value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<UserCouponTransaction>>(
                    RouteConsts.Coupon.GetUserBHT80Coupons.Url, value);
                return ret;
            }

            public List<TSBCouponTransaction> GetCurrentTSBSoldCoupons()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.GetCurrentTSBSoldCoupons.Url, new { });
                return ret;
            }

            public List<TSBCouponTransaction> GetTSBSoldCoupons(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.GetTSBSoldCoupons.Url, tsb);
                return ret;
            }

            public void SaveTransaction(TSBCouponTransaction value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Coupon.SaveTransaction.Url, value);
            }

            public List<TSBCouponBalance> GetCurrent()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponBalance>>(
                    RouteConsts.Coupon.GetCurrent.Url, new { });
                return ret;
            }

            public List<TSBCouponBalance> GetTSBCurrent(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponBalance>>(
                    RouteConsts.Coupon.GetTSBCurrent.Url, tsb);
                return ret;
            }

            public void UserBorrowCoupons(Search.UserCoupons.BorrowCoupons value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Coupon.UserBorrowCoupons.Url, value);
            }

            public void UserReturnCoupons(Search.UserCoupons.ReturnCoupons value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Coupon.UserReturnCoupons.Url, value);
            }

            #endregion
        }

        #endregion
    }
}
