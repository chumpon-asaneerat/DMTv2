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
            //

            #region Exchange Transaction

            public NRestResult<List<TSBExchangeGroup>> GetRequestApproveTSBExchangeGroups(TSB value)
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
                        RouteConsts.Exchange.GetRequestApproveTSBExchangeGroups.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<TSBExchangeGroup>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

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

            public NRestResult<List<TSBExchangeTransaction>> GetTSBExchangeTransactions(
                Search.Exchanges.Transactions.Filter value)
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
                    ret.data = null;
                }
                return ret;
            }

            public NRestResult<TSBExchangeTransaction> GetTSBExchangeTransaction(
                Search.Exchanges.Transactions.Filter value)
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
                        RouteConsts.Exchange.GetTSBExchangeTransaction.Url, value);
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

        #region Private Methods

        private TSBExchangeTransaction CloneTransaction(TSBExchangeTransaction src)
        {
            if (null == src)
            {
                return null;
            }
            var dst = new TSBExchangeTransaction();
            CopyTransaction(src, dst);
            return dst;
        }

        private void CopyTransaction(TSBExchangeTransaction src, TSBExchangeTransaction dst)
        {
            if (null == src || null == dst) return;
            // Transaction
            dst.GroupId = src.GroupId;
            dst.TransactionDate = DateTime.Now;
            // Amount
            dst.AmountST25 = src.AmountST25;
            dst.AmountST50 = src.AmountST50;
            dst.AmountBHT1 = src.AmountBHT1;
            dst.AmountBHT2 = src.AmountBHT2;
            dst.AmountBHT5 = src.AmountBHT5;
            dst.AmountBHT10 = src.AmountBHT10;
            dst.AmountBHT20 = src.AmountBHT20;
            dst.AmountBHT50 = src.AmountBHT50;
            dst.AmountBHT100 = src.AmountBHT100;
            dst.AmountBHT500 = src.AmountBHT500;
            dst.AmountBHT1000 = src.AmountBHT1000;
            // Count
            dst.CountST25 = src.CountST25;
            dst.CountST50 = src.CountST50;
            dst.CountBHT1 = src.CountBHT1;
            dst.CountBHT2 = src.CountBHT2;
            dst.CountBHT5 = src.CountBHT5;
            dst.CountBHT10 = src.CountBHT10;
            dst.CountBHT20 = src.CountBHT20;
            dst.CountBHT50 = src.CountBHT50;
            dst.CountBHT100 = src.CountBHT100;
            dst.CountBHT500 = src.CountBHT500;
            dst.CountBHT1000 = src.CountBHT1000;
            // TSB
            dst.TSBId = src.TSBId;
            dst.TSBNameEN = src.TSBNameEN;
            dst.TSBNameTH = src.TSBNameTH;
            // User
            dst.UserId = src.UserId;
            dst.FullNameEN = src.FullNameEN;
            dst.FullNameTH = src.FullNameTH;
            // Amount
            dst.ExchangeBHT = src.ExchangeBHT;
            dst.BorrowBHT = src.BorrowBHT;
            dst.AdditionalBHT = src.AdditionalBHT;
            // Period
            dst.PeriodBegin = src.PeriodBegin;
            dst.PeriodEnd = src.PeriodEnd;
        }

        private void Save(TSBExchangeGroup value)
        {
            if (null != value)
            {
                ops.Exchanges.SaveTSBExchangeGroup(value);
            }
        }

        #endregion

        #region Public Methods

        #region GetRequestApproves

        /// <summary>
        /// Gets Request or Approve List.
        /// </summary>
        public List<TSBExchangeGroup> GetRequestApproves()
        {
            if (null == this.TSB)
                return new List<TSBExchangeGroup>();

            var items = ops.Exchanges.GetRequestApproveTSBExchangeGroups(this.TSB).Value();
            if (null == items)
                return new List<TSBExchangeGroup>();

            var results = items.FindAll(item =>
            {
                bool ret = (
                    (item.State == TSBExchangeGroup.StateTypes.Request ||
                     item.State == TSBExchangeGroup.StateTypes.Approve) &&
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
            result.PkId = 0;
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

        public void CancelRequest(TSBExchangeGroup value)
        {
            if (null == value) return;
            if (value.PkId == 0) return;
            // Set Exchange Group state to Completed.
            value.State = TSBExchangeGroup.StateTypes.Canceled;
            // Set Transaction Completed.
            value.Request.FinishFlag = TSBExchangeTransaction.FinishedFlags.Completed;
            // Save Group and Transaction.
            this.Save(value);
        }

        public void SaveRequest(TSBExchangeGroup value)
        {
            if (null == value) return;
            if (value.State != TSBExchangeGroup.StateTypes.Request) return;
            // Save Group and Transaction.
            this.Save(value);
        }

        public void PrepareApprove(TSBExchangeGroup value)
        {
            if (null == value || null == value.Request) return;
            // Gets Request transaction from database.

            // clone from request transaction.
            if (null == value.Approve)
            {
                value.Approve = CloneTransaction(value.Request);
            }
        }

        public void CancelApprove(TSBExchangeGroup value)
        {
            /*
            if (null != value.Approve && value.Approve.TransactionId != 0)
            {
                // data in database so set finished flag.
                value.Approve.FinishFlag = TSBExchangeTransaction.FinishedFlags.Completed;
                // Save Group and Transaction.
                Save(value);
            }
            value.Approve = null;
            */
        }

        public void SaveApprove(TSBExchangeGroup value)
        {
            if (null == value || null == value.Approve) return;
            if (value.State != TSBExchangeGroup.StateTypes.Request) return;
            // Change state
            value.State = TSBExchangeGroup.StateTypes.Approve;

            value.Approve.TransactionDate = DateTime.Now;
            value.Approve.TransactionType = TSBExchangeTransaction.TransactionTypes.Approve;
            // Transaction - TSB
            value.Approve.TSBId = TSB.TSBId;
            value.Approve.TSBNameEN = TSB.TSBNameEN;
            value.Approve.TSBNameEN = TSB.TSBNameEN;
            // Transaction - User (Supervisor)
            value.Approve.UserId = Supervisor.UserId;
            value.Approve.FullNameEN = Supervisor.FullNameEN;
            value.Approve.FullNameTH = Supervisor.FullNameTH;

            // Save Group and Transaction.
            this.Save(value);
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
