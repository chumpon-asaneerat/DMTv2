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
    partial class SCWOperations
    {
        #region Internal Variables

        private MasterOperations _Master_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Masters Operations.
        /// </summary>
        public MasterOperations Masters
        {
            get
            {
                if (null == _Master_Ops)
                {
                    lock (this)
                    {
                        _Master_Ops = new MasterOperations();
                    }
                }
                return _Master_Ops;
            }
        }

        #endregion

        #region MasterOperations

        public class MasterOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal MasterOperations() : base() { }

            #endregion

            #region Public Methods

            #region Get Currency Demon List

            public SCWCurrencyList GetCurrencyList(
                int nwId)
            {
                SCWCurrencyList ret;
                NRestClient client = NRestClient.CreateDCClient();
                if (null == client)
                {
                    ret = new SCWCurrencyList();
                    return ret;
                }

                var url = "dmt-scw/api/tod/currencyDenomList";
                var value = new
                {
                    networkId = nwId
                };

                string usr = SCWServiceOperations.Instance.UserName;
                string pwd = SCWServiceOperations.Instance.Password;

                ret = client.Execute2<SCWCurrencyList>(url, value, username: usr, password: pwd);
                return ret;
            }

            #endregion

            #region Get Coupon List

            public SCWCouponList GetCouponList(
                int nwId)
            {
                SCWCouponList ret;
                NRestClient client = NRestClient.CreateDCClient();
                if (null == client)
                {
                    ret = new SCWCouponList();
                    return ret;
                }

                var url = "dmt-scw/api/tod/couponList";
                var value = new
                {
                    networkId = nwId
                };

                string usr = SCWServiceOperations.Instance.UserName;
                string pwd = SCWServiceOperations.Instance.Password;

                ret = client.Execute2<SCWCouponList>(url, value, username: usr, password: pwd);
                return ret;
            }

            #endregion

            #region Get Card Allow List

            public SCWCardAllowList GetCardAllowList(
                int nwId)
            {
                SCWCardAllowList ret;
                NRestClient client = NRestClient.CreateDCClient();
                if (null == client)
                {
                    ret = new SCWCardAllowList();
                    return ret;
                }

                var url = "dmt-scw/api/tod/cardAllowList";
                var value = new
                {
                    networkId = nwId
                };

                string usr = SCWServiceOperations.Instance.UserName;
                string pwd = SCWServiceOperations.Instance.Password;

                ret = client.Execute2<SCWCardAllowList>(url, value, username: usr, password: pwd);
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
