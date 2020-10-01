#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;
using System.CodeDom;
using System.Data;

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

        private TSBExchangeTransaction CloneTransaction(TSBExchangeTransaction src, bool setZero = false)
        {
            if (null == src)
            {
                return null;
            }
            var dst = new TSBExchangeTransaction();
            CopyTransaction(src, dst, setZero);
            return dst;
        }

        private void CopyTransaction(TSBExchangeTransaction src, TSBExchangeTransaction dst, bool setZero = false)
        {
            if (null == src || null == dst) return;
            // Transaction
            dst.GroupId = src.GroupId;
            dst.TransactionDate = DateTime.Now;
            // Amount
            dst.AmountST25 = (!setZero) ? src.AmountST25 : decimal.Zero;
            dst.AmountST50 = (!setZero) ? src.AmountST50 : decimal.Zero;
            dst.AmountBHT1 = (!setZero) ? src.AmountBHT1 : decimal.Zero;
            dst.AmountBHT2 = (!setZero) ? src.AmountBHT2 : decimal.Zero;
            dst.AmountBHT5 = (!setZero) ? src.AmountBHT5 : decimal.Zero;
            dst.AmountBHT10 = (!setZero) ? src.AmountBHT10 : decimal.Zero;
            dst.AmountBHT20 = (!setZero) ? src.AmountBHT20 : decimal.Zero;
            dst.AmountBHT50 = (!setZero) ? src.AmountBHT50 : decimal.Zero;
            dst.AmountBHT100 = (!setZero) ? src.AmountBHT100 : decimal.Zero;
            dst.AmountBHT500 = (!setZero) ? src.AmountBHT500 : decimal.Zero;
            dst.AmountBHT1000 = (!setZero) ? src.AmountBHT1000 : decimal.Zero;
            // Count - no need because auto calc in model class.
            /*
            dst.CountST25 = (!setZero) ? src.CountST25 : 0;
            dst.CountST50 = (!setZero) ? src.CountST50 : 0;
            dst.CountBHT1 = (!setZero) ? src.CountBHT1 : 0;
            dst.CountBHT2 = (!setZero) ? src.CountBHT2 : 0;
            dst.CountBHT5 = (!setZero) ? src.CountBHT5 : 0;
            dst.CountBHT10 = (!setZero) ? src.CountBHT10 : 0;
            dst.CountBHT20 = (!setZero) ? src.CountBHT20 : 0;
            dst.CountBHT50 = (!setZero) ? src.CountBHT50 : 0;
            dst.CountBHT100 = (!setZero) ? src.CountBHT100 : 0;
            dst.CountBHT500 = (!setZero) ? src.CountBHT500 : 0;
            dst.CountBHT1000 = (!setZero) ? src.CountBHT1000 : 0;
            */
            // TSB
            dst.TSBId = src.TSBId;
            dst.TSBNameEN = src.TSBNameEN;
            dst.TSBNameTH = src.TSBNameTH;
            // User
            dst.UserId = src.UserId;
            dst.FullNameEN = src.FullNameEN;
            dst.FullNameTH = src.FullNameTH;
            // Amount
            dst.ExchangeBHT = (!setZero) ? src.ExchangeBHT : decimal.Zero;
            dst.BorrowBHT = (!setZero) ? src.BorrowBHT : decimal.Zero;
            dst.AdditionalBHT = (!setZero) ? src.AdditionalBHT : decimal.Zero;
            // Period
            dst.PeriodBegin = src.PeriodBegin;
            dst.PeriodEnd = src.PeriodEnd;
            /*
            if (dst.BorrowBHT > decimal.Zero)
            {
                dst.PeriodBegin = src.PeriodBegin;
                dst.PeriodEnd = src.PeriodEnd;
            }
            else
            {
                dst.PeriodBegin = new DateTime?();
                dst.PeriodEnd = new DateTime?();
            }
            */
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

        public void LoadRequest(TSBExchangeGroup value)
        {
            if (null == value) return;
            if (null == value.Request)
            {
                var filter = Search.Exchanges.Transactions.Filter.Create(this.TSB, value.GroupId,
                    TSBExchangeTransaction.TransactionTypes.Request);
                value.Request = ops.Exchanges.GetTSBExchangeTransaction(filter).Value();
            }
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

        public void LoadApprove(TSBExchangeGroup value)
        {
            if (null == value) return;
            if (null == value.Approve)
            {
                var filter = Search.Exchanges.Transactions.Filter.Create(this.TSB, value.GroupId,
                    TSBExchangeTransaction.TransactionTypes.Approve);
                value.Approve = ops.Exchanges.GetTSBExchangeTransaction(filter).Value();
            }
        }

        public void PrepareApprove(TSBExchangeGroup value)
        {
            if (null == value) return;

            // Gets Request transaction from database.
            if (null == value.Request)
            {
                LoadRequest(value);
                if (null == value.Request) return;
            }
            // Gets Approve transaction from database.
            if (null == value.Approve)
            {
                LoadApprove(value);
                // clone from request transaction if not exists.
                if (null == value.Approve)
                {
                    value.Approve = CloneTransaction(value.Request);
                    value.Approve.TransactionType = TSBExchangeTransaction.TransactionTypes.Approve;
                }
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

        public void LoadReceived(TSBExchangeGroup value)
        {
            if (null == value) return;
            // Load Received and Exchange transaction.
            if (null == value.Received)
            {
                var filter = Search.Exchanges.Transactions.Filter.Create(this.TSB, value.GroupId,
                    TSBExchangeTransaction.TransactionTypes.Received);
                value.Received = ops.Exchanges.GetTSBExchangeTransaction(filter).Value();
            }
            if (null == value.Exchange)
            {
                var filter = Search.Exchanges.Transactions.Filter.Create(this.TSB, value.GroupId,
                    TSBExchangeTransaction.TransactionTypes.Exchange);
                value.Exchange = ops.Exchanges.GetTSBExchangeTransaction(filter).Value();
            }
        }

        public void PrepareReceived(TSBExchangeGroup value)
        {
            if (null == value) return;

            // Gets Request transaction from database.
            if (null == value.Request)
            {
                LoadRequest(value);
                if (null == value.Request) return;
            }
            // Gets Approve transaction from database.
            if (null == value.Approve)
            {
                LoadApprove(value);
                if (null == value.Approve) return;
            }
            // Gets Received and Exchange transaction from database.
            if (null == value.Received || null == value.Exchange)
            {
                LoadReceived(value);
                // clone from request transaction if not exists.
                if (null == value.Received)
                {
                    value.Received = CloneTransaction(value.Approve);
                    value.Received.TransactionType = TSBExchangeTransaction.TransactionTypes.Received;
                }
                // clone from request transaction if not exists (and set all amount/count to zero).
                if (null == value.Exchange)
                {
                    value.Exchange = CloneTransaction(value.Approve, true);
                    value.Exchange.TransactionType = TSBExchangeTransaction.TransactionTypes.Exchange;
                }
            }
        }

        public void SaveReceived(TSBExchangeGroup value)
        {
            if (null == value || null == value.Approve) return;
            if (value.State != TSBExchangeGroup.StateTypes.Approve) return;
            DateTime dt = DateTime.Now;
            // Change state
            value.State = TSBExchangeGroup.StateTypes.Received;
            // Received Transaction - Common
            value.Received.TransactionDate = dt;
            value.Received.TransactionType = TSBExchangeTransaction.TransactionTypes.Received;
            // Received Transaction - TSB
            value.Received.TSBId = TSB.TSBId;
            value.Received.TSBNameEN = TSB.TSBNameEN;
            value.Received.TSBNameEN = TSB.TSBNameEN;
            // Received Transaction - User (Supervisor)
            value.Received.UserId = Supervisor.UserId;
            value.Received.FullNameEN = Supervisor.FullNameEN;
            value.Received.FullNameTH = Supervisor.FullNameTH;

            // Exchange Transaction - Common
            value.Exchange.TransactionDate = dt;
            value.Exchange.TransactionType = TSBExchangeTransaction.TransactionTypes.Exchange;
            // Exchange Transaction - TSB
            value.Exchange.TSBId = TSB.TSBId;
            value.Exchange.TSBNameEN = TSB.TSBNameEN;
            value.Exchange.TSBNameEN = TSB.TSBNameEN;
            // Exchange Transaction - User (Supervisor)
            value.Exchange.UserId = Supervisor.UserId;
            value.Exchange.FullNameEN = Supervisor.FullNameEN;
            value.Exchange.FullNameTH = Supervisor.FullNameTH;

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
