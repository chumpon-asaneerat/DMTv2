﻿#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;

#endregion

namespace DMT.Services
{
    partial class DCOperations
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

            public DCCurrencyList GetCurrencyList(
                int nwId)
            {
                DCCurrencyList ret;
                NRestClient client = NRestClient.CreateDCClient();
                if (null == client)
                {
                    ret = new DCCurrencyList();
                    return ret;
                }

                var url = "dmt-scw/api/tod/currencyDenomList";
                var value = new
                {
                    networkId = nwId
                };

                string usr = DCServiceOperations.Instance.UserName;
                string pwd = DCServiceOperations.Instance.Password;

                ret = client.Execute2<DCCurrencyList>(url, value, username: usr, password: pwd);
                return ret;
            }

            #endregion

            #region Get Coupon List

            public DCCouponList GetCouponList(
                int nwId)
            {
                DCCouponList ret;
                NRestClient client = NRestClient.CreateDCClient();
                if (null == client)
                {
                    ret = new DCCouponList();
                    return ret;
                }

                var url = "dmt-scw/api/tod/couponList";
                var value = new
                {
                    networkId = nwId
                };

                string usr = DCServiceOperations.Instance.UserName;
                string pwd = DCServiceOperations.Instance.Password;

                ret = client.Execute2<DCCouponList>(url, value, username: usr, password: pwd);
                return ret;
            }

            #endregion

            #region Get Job List

            public DCJobList GetJobList(
                int nwId, int pzId, string usrId)
            {
                DCJobList ret;
                NRestClient client = NRestClient.CreateDCClient();
                if (null == client)
                {
                    ret = new DCJobList();
                    return ret;
                }

                var url = "dmt-scw/api/tod/jobList";
                var value = new
                {
                    networkId = nwId,
                    plazaId = pzId.
                    staffId = usrId
                };

                string usr = DCServiceOperations.Instance.UserName;
                string pwd = DCServiceOperations.Instance.Password;

                ret = client.Execute2<DCJobList>(url, value, username: usr, password: pwd);
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
