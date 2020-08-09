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

    public class TSBExchangeManager
    {
        #region Internal Variables

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBExchangeManager() : base()
        {
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~TSBExchangeManager()
        {

        }

        #endregion

        #region Public Methods

        #region Save

        public void Save(TSBExchangeTransaction value)
        {
            if (null != value)
            {
                ops.Exchanges.SaveTSBExchangeTransaction(value);
            }
        }

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets TSB.
        /// </summary>
        public TSB TSB { get; set; }
        /// <summary>
        /// Gets Request List.
        /// </summary>
        public List<TSBExchangeTransaction> Requests
        {
            get 
            {
                if (null == this.TSB)
                    return new List<TSBExchangeTransaction>();
                var items = ops.Exchanges.GetTSBExchangeTransactions(this.TSB);
                if (null == this.TSB)
                    return new List<TSBExchangeTransaction>();

                var results = items.FindAll(item =>
                {
                    bool ret = (
                        item.TransactionType == TSBExchangeTransaction.TransactionTypes.Request &&
                        item.FinishFlag == TSBExchangeTransaction.FinishedFlags.Avaliable
                    );
                    return ret;
                }).OrderBy(x => x.TransactionId).ToList();

                return results;
            }
        }

        #endregion
    }
}
