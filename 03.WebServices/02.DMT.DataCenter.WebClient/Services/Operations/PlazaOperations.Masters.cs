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
    partial class DCOperations
    {
        #region Internal Variables

        private MasterOperations _Master_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Masters Operations.
        /// </summary>
        public MasterOperations Coupons
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

            #region Get Currency

            public NRestResult<DCCurrencyList> GetCurrencyList(
                int networkId)
            {
                NRestResult<DCCurrencyList> ret;
                NRestClient client = NRestClient.CreateDCClient();
                if (null == client)
                {
                    ret = new NRestResult<DCCurrencyList>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                var url = "dmt-scw/api/tod/currencyDenomList";
                var value = new
                {
                    networkId = networkId
                };

                ret = client.Execute<DCCurrencyList>(url, value);
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
