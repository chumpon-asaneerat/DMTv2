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
                NRestClient.WebProtocol protocol =
                    (ConfigManager.Instance.Plaza.TAxTOD.Http.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = ConfigManager.Instance.Plaza.TAxTOD.Http.HostName;
                int portNo = ConfigManager.Instance.Plaza.TAxTOD.Http.PortNumber;

                NRestResult<List<TAServerCouponTransaction>> ret;

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
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<List<TAServerCouponTransaction>>(url, value);
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
