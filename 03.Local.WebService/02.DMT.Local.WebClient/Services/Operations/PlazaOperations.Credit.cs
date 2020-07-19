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

            public TSBCreditTransaction GetCurrentInitial()
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBCreditTransaction>(
                    RouteConsts.Credit.GetCurrentInitial.Url);
                return ret;
            }

            public TSBCreditTransaction GetInitial(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBCreditTransaction>(
                    RouteConsts.Credit.GetInitial.Url, tsb);
                return ret;
            }

            public void SaveTransaction(TSBCreditTransaction value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Credit.SaveTransaction.Url, value);
            }

            #endregion
        }

        #endregion
    }
}
