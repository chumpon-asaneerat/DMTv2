#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;
using System.CodeDom;

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

            public NRestResult<List<TSBExchangeGroup>> GetTSBExchangeGroups(Search.Exchanges.Filter value)
            {
                NRestResult<List<TSBExchangeGroup>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<TSBExchangeGroup>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<TSBExchangeGroup>>(
                        RouteConsts.Exchange.GetTSBExchangeGroups.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<TSBExchangeGroup>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<TSBExchangeGroup> SaveTSBExchangeGroup(
                TSBExchangeGroup value)
            {
                NRestResult<TSBExchangeGroup> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<TSBExchangeGroup>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<TSBExchangeGroup>(
                        RouteConsts.Exchange.SaveTSBExchangeGroup.Url, value);
                }
                else
                {
                    ret = new NRestResult<TSBExchangeGroup>();
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

        #region GetRequests

        /// <summary>
        /// Gets Request List.
        /// </summary>
        public List<TSBExchangeGroup> GetRequests()
        {
            if (null == this.TSB)
                return new List<TSBExchangeGroup>();

            var filter = Search.Exchanges.Filter.Create(this.TSB);
            filter.State = TSBExchangeGroup.StateTypes.Request;
            filter.FinishedFlag = TSBExchangeGroup.FinishedFlags.Avaliable;

            var items = ops.Exchanges.GetTSBExchangeGroups(filter).Value();
            if (null == items)
                return new List<TSBExchangeGroup>();

            var results = items.FindAll(item =>
            {
                bool ret = (
                    item.State == TSBExchangeGroup.StateTypes.Request &&
                    item.FinishFlag == TSBExchangeGroup.FinishedFlags.Avaliable
                );
                return ret;
            }).OrderBy(x => x.RequestDate).ToList();

            return results;
        }

        #endregion

        #region New Request

        public TSBExchangeGroup NewRequest()
        {
            var result = new TSBExchangeGroup();

            // Set Exchange Group Information.
            result.RequestDate = DateTime.Now;
            result.State = TSBExchangeGroup.StateTypes.Request;
            result.FinishFlag = TSBExchangeGroup.FinishedFlags.Avaliable;
            // Exchange Group - TSB
            result.TSBId = TSB.TSBId;
            result.TSBNameEN = TSB.TSBNameEN;
            result.TSBNameEN = TSB.TSBNameEN;

            // Set Request Transaction.
            result.Request = new TSBExchangeTransaction();
            result.Request.TransactionDate = result.RequestDate;
            result.Request.TransactionType = TSBExchangeTransaction.TransactionTypes.Request;
            // Transaction - TSB
            result.Request.TSBId = TSB.TSBId;
            result.Request.TSBNameEN = TSB.TSBNameEN;
            result.Request.TSBNameEN = TSB.TSBNameEN;
            // Transaction - User (Supervisor)
            result.Request.UserId = Supervisor.UserId;
            result.Request.FullNameEN = Supervisor.FullNameEN;
            result.Request.FullNameTH = Supervisor.FullNameTH;

            return result;
        }

        public void Cancel(TSBExchangeGroup value)
        {
            if (null == value) return;
            // Set Exchange Group state to Completed.
            value.State = TSBExchangeGroup.StateTypes.Canceled;
            // Set Transaction Completed.
            value.Request.FinishFlag = TSBExchangeTransaction.FinishedFlags.Completed;
        }

        public void Approve(TSBExchangeGroup value)
        {
            if (null == value) return;
        }

        public void Received(TSBExchangeGroup value)
        {
            if (null == value) return;
        }

        public void Returns(TSBExchangeGroup value)
        {
            if (null == value) return;
        }

        #endregion

        #region Save

        public void Save(TSBExchangeGroup value)
        {
            if (null != value)
            {
                ops.Exchanges.SaveTSBExchangeGroup(value);
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

        #endregion
    }
}
