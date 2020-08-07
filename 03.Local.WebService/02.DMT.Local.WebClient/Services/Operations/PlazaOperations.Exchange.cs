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

        private ExchangeOperations _Exchange_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Exchanges Operations.
        /// </summary>
        public ExchangeOperations Exchanges
        {
            get
            {
                if (null == _Exchange_Ops)
                {
                    lock (this)
                    {
                        _Exchange_Ops = new ExchangeOperations();
                    }
                }
                return _Exchange_Ops;
            }
        }

        #endregion

        #region ExchangeOperations

        public class ExchangeOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal ExchangeOperations() : base() { }

            #endregion

            #region Public Methods

            #region Exchange Transaction

            public List<TSBExchangeTransaction> GetTSBExchangeTransactions(TSB value)
            {
                return NRestClient.Create(port: 9000).Execute<List<TSBExchangeTransaction>>(
                    RouteConsts.Exchange.GetTSBExchangeTransactions.Url, value);
            }

            public void SaveTSBExchangeTransaction(TSBExchangeTransaction value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Exchange.SaveTSBExchangeTransaction.Url, value);
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
