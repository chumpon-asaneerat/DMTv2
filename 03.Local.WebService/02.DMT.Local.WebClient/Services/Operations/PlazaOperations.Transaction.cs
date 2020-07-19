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

        private CreditOperations _Credit_Ops = null;
        private CouponOperations _Coupon_Ops = null;
        private AdditionOperations _Addition_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Credits Operations.
        /// </summary>
        public CreditOperations Credits
        {
            get
            {
                if (null == _Credit_Ops)
                {
                    lock (this)
                    {
                        _Credit_Ops = new CreditOperations();
                    }
                }
                return _Credit_Ops;
            }
        }
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
        /// <summary>
        /// Gets Additions Operations.
        /// </summary>
        public AdditionOperations Additions
        {
            get
            {
                if (null == _Addition_Ops)
                {
                    lock (this)
                    {
                        _Addition_Ops = new AdditionOperations();
                    }
                }
                return _Addition_Ops;
            }
        }

        #endregion

        #region CreditOperations

        public class CreditOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal CreditOperations() : base() { }

            #endregion

            #region Public Methods

            public TSBCreditTransaction GetInitial()
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBCreditTransaction>(
                    RouteConsts.Transaction.Credit.GetInitial.Url);
                return ret;
            }

            #endregion
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

            public TSBCouponTransaction GetInitial()
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBCouponTransaction>(
                    RouteConsts.Transaction.Coupon.GetInitial.Url);
                return ret;
            }

            #endregion
        }

        #endregion

        #region AdditionOperations

        public class AdditionOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal AdditionOperations() : base() { }

            #endregion

            #region Public Methods

            public TSBAdditionTransaction GetInitial()
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBAdditionTransaction>(
                    RouteConsts.Transaction.Addition.GetInitial.Url);
                return ret;
            }

            #endregion
        }

        #endregion
    }
}
