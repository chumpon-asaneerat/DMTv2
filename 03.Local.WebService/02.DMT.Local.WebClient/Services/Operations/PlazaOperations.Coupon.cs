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

            public TSBCouponTransaction GetCurrentInitial()
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBCouponTransaction>(
                    RouteConsts.Coupon.GetCurrentInitial.Url, new { });
                return ret;
            }

            public TSBCouponTransaction GetInitial(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBCouponTransaction>(
                    RouteConsts.Coupon.GetInitial.Url, tsb);
                return ret;
            }

            public void SaveTransaction(TSBCouponTransaction value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Coupon.SaveTransaction.Url, value);
            }

            public TSBCouponBalance GetCurrent()
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBCouponBalance>(
                    RouteConsts.Coupon.GetCurrent.Url, new { });
                return ret;
            }

            public TSBCouponBalance GetTSBCurrent(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBCouponBalance>(
                    RouteConsts.Coupon.GetTSBCurrent.Url, tsb);
                return ret;
            }

            #endregion
        }

        #endregion
    }
}
