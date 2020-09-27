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
    partial class LocalOperations
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

            //TODO: Change to TSB Exchange Group.
            public NRestResult<List<TSBExchangeTransaction>> GetTSBExchangeTransactions(TSB value)
            {
                NRestResult<List<TSBExchangeTransaction>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<TSBExchangeTransaction>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<TSBExchangeTransaction>>(
                        RouteConsts.Exchange.GetTSBExchangeTransactions.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<TSBExchangeTransaction>>();
                    ret.ParameterIsNull();
                    ret.data = new List<TSBExchangeTransaction>();
                }
                return ret;
            }
            //TODO: Change to TSB Exchange Group.
            public NRestResult<TSBExchangeTransaction> SaveTSBExchangeTransaction(
                TSBExchangeTransaction value)
            {
                NRestResult<TSBExchangeTransaction> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<TSBExchangeTransaction>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<TSBExchangeTransaction>(
                        RouteConsts.Exchange.SaveTSBExchangeTransaction.Url, value);
                }
                else
                {
                    ret = new NRestResult<TSBExchangeTransaction>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }

    public class TSBExchangeManager2
    {
        #region Internal Variables

        protected LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBExchangeManager2() : base()
        {
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~TSBExchangeManager2()
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

                var items = ops.Exchanges.GetTSBExchangeTransactions(this.TSB).Value();
                if (null == items)
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

    public class TSBExchangeManager
    {
        #region Internal Variables

        protected LocalOperations ops = LocalServiceOperations.Instance.Plaza;

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

        //TODO: Change to TSB Exchange Group.
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
        /// Gets or sets Supervisor.
        /// </summary>
        public User Supervisor { get; set; }

        //TODO: Change to TSB Exchange Group.

        /// <summary>
        /// Gets Request List.
        /// </summary>
        public List<TSBExchangeTransaction> Requests
        {
            get
            {
                if (null == this.TSB)
                    return new List<TSBExchangeTransaction>();

                var items = ops.Exchanges.GetTSBExchangeTransactions(this.TSB).Value();
                if (null == items)
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
