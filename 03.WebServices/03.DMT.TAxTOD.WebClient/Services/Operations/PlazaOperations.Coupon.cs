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
    partial class TAxTODOperations
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

            #region TA Server Coupon Transaction

            public NRestResult<List<TAServerCouponTransaction>> GetTAServerCouponTransactions(string tsbId, string userId, int? transactionType, int? couponType)
            {
                NRestResult<List<TAServerCouponTransaction>> ret;
                NRestClient client = NRestClient.CreateTAxTODClient();
                if (null == client)
                {
                    ret = new NRestResult<List<TAServerCouponTransaction>>();
                    ret.ParameterIsNull();
                    return ret;
                }

                var url = "api/users/coupons/getlist";
                var value = new
                {
                    tsbid = tsbId,
                    userid = userId,
                    transactiontype = transactionType,
                    coupontype = couponType
                };
                if (null != value)
                {
                    ret = client.Execute<List<TAServerCouponTransaction>>(url, value);
                }
                else
                {
                    ret = new NRestResult<List<TAServerCouponTransaction>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
