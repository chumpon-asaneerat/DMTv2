#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;
using NLib.Reflection;
using System.Windows.Forms;

#endregion

namespace DMT.Services
{
    partial class LocalOperations
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

            #region TSB Credit Balance

            public NRestResult<TSBCreditBalance> GetTSBBalance(TSB value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<TSBCreditBalance> ret;

                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<TSBCreditBalance>(RouteConsts.Credit.GetTSBBalance.Url, value);
                }
                else
                {
                    ret = new NRestResult<TSBCreditBalance>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            #endregion

            #region TSB Credit Transaction

            public NRestResult<TSBCreditTransaction> GetInitialTSBCreditTransaction(TSB value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<TSBCreditTransaction> ret;

                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<TSBCreditTransaction>(RouteConsts.Credit.GetInitialTSBCreditTransaction.Url, value);
                }
                else
                {
                    ret = new NRestResult<TSBCreditTransaction>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            public NRestResult<TSBCreditTransaction> SaveTSBCreditTransaction(
                TSBCreditTransaction value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<TSBCreditTransaction> ret;

                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<TSBCreditTransaction>(RouteConsts.Credit.SaveTSBCreditTransaction.Url, value);
                }
                else
                {
                    ret = new NRestResult<TSBCreditTransaction>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            #endregion

            #region User Credit Balance

            public NRestResult<List<UserCreditBalance>> GetActiveUserCreditBalances(TSB value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<List<UserCreditBalance>> ret;

                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<List<UserCreditBalance>>(RouteConsts.Credit.GetActiveUserCreditBalances.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<UserCreditBalance>>();
                    ret.ParameterIsNull();
                    ret.data = new List<UserCreditBalance>();
                }
                return ret;
            }

            public NRestResult<UserCreditBalance> GetActiveUserCreditBalanceById(
                Search.UserCredits.GetActiveById value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<UserCreditBalance> ret;

                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<UserCreditBalance>(RouteConsts.Credit.GetActiveUserCreditBalanceById.Url, value);
                }
                else
                {
                    ret = new NRestResult<UserCreditBalance>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            public NRestResult<UserCreditBalance> SaveUserCreditBalance(UserCreditBalance value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<UserCreditBalance> ret;

                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<UserCreditBalance>(RouteConsts.Credit.SaveUserCreditBalance.Url, value);
                }
                else
                {
                    ret = new NRestResult<UserCreditBalance>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            #endregion

            #region User Credit Transaction

            public NRestResult<UserCreditTransaction> SaveUserCreditTransaction(
                UserCreditTransaction value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<UserCreditTransaction> ret;

                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<UserCreditTransaction>(RouteConsts.Credit.SaveUserCreditTransaction.Url, value);
                }
                else
                {
                    ret = new NRestResult<UserCreditTransaction>();
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

    #region TSBCreditManager (abstract)

    public abstract class TSBCreditManager
    {
        #region Internal Variables

        protected LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBCreditManager() : base() { }

        #endregion

        #region Protected Methods

        protected virtual void OnRefresh() { }

        #endregion

        #region Public Methods

        public void Refresh()
        {
            OnRefresh();
        }

        #endregion

        #region Public Properties

        #region TSB

        /// <summary>
        /// Gets Current TSB.
        /// </summary>
        public TSB TSB { get; set; }

        #endregion

        #region TSB Balance

        /// <summary>
        /// Gets Current TSB Balance.
        /// </summary>
        public TSBCreditBalance Balance
        {
            get
            {
                if (null == this.TSB)
                {
                    return null;
                }
                return ops.Credits.GetTSBBalance(this.TSB).Value();
            }
        }

        #endregion

        #endregion
    }

    #endregion

    #region TSBCreditInitManager

    public class TSBCreditInitManager : TSBCreditManager
    {
        #region Internal Variables

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBCreditInitManager() : base() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Current Initial TSB Credit Transaction. If TSB not assigned null returns.
        /// </summary>
        public TSBCreditTransaction Current
        {
            get 
            {
                if (null == TSB) return null;
                return ops.Credits.GetInitialTSBCreditTransaction(TSB).Value();
            }
        }

        #endregion
    }

    #endregion

    #region UserCreditManager (abstract)

    public abstract class UserCreditManager
    {
        #region Internal Variables

        protected LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserCreditManager() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~UserCreditManager()
        {
            if (null != this.Transaction)
            {
                this.Transaction.PropertyChanged += Transaction_PropertyChanged;
            }
            this.Transaction = null;
        }

        #endregion

        #region Private Methods

        private void Transaction_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.Calc();
        }

        #endregion

        #region Abstract Methods

        protected abstract void Calc();

        #endregion

        #region Public Methods

        public void Setup(TSB tsb, UserCreditBalance current)
        {
            this.TSB = tsb;
            this.UserBalance = current;
            this.IsNew = (null == this.UserBalance);
            if (this.IsNew)
            {
                this.UserBalance = new UserCreditBalance();
            }

            this.Transaction = new UserCreditTransaction();

            this.TSBBalance = ops.Credits.GetTSBBalance(tsb).Value();
            this.ResultBalance = new TSBCreditBalance();

            this.TSBBalance.AssignTo(this.ResultBalance);


            // Hook Event to recalcuate when transaction's property changed.
            this.Transaction.PropertyChanged += Transaction_PropertyChanged;
        }

        public void SetUser(User user)
        {
            User = user;
            if (null != User)
            {
                UserBalance.UserId = User.UserId;
                UserBalance.FullNameEN = User.FullNameEN;
                UserBalance.FullNameTH = User.FullNameTH;
            }
        }

        public abstract bool Save();
        public virtual bool HasNegative() { return false; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Current TSB.
        /// </summary>
        public TSB TSB { get; private set; }
        /// <summary>
        /// Gets or sets Plaza Group.
        /// </summary>
        public PlazaGroup PlazaGroup { get; set; }
        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public User User { get; private set; }
        /// <summary>
        /// Checks is new UserBalance.
        /// </summary>
        public bool IsNew { get; private set; }
        /// <summary>
        /// Gets Current user credit (original).
        /// </summary>
        public UserCreditBalance UserBalance { get; private set; }
        /// <summary>
        /// Gets Editable Transaction.
        /// </summary>
        public UserCreditTransaction Transaction { get; private set; }
        /// <summary>
        /// Gets Current TSB Balance.
        /// </summary>
        public TSBCreditBalance TSBBalance { get; private set; }
        /// <summary>
        /// Gets result TSB Balance after apply transaction.
        /// </summary>
        public TSBCreditBalance ResultBalance { get; private set; }

        #endregion
    }

    #endregion

    #region UserCreditBorrowManager

    public class UserCreditBorrowManager : UserCreditManager
    {
        #region Override Methods

        protected override void Calc()
        {
            if (null == ResultBalance || null == TSBBalance || null == Transaction)
                return;

            ResultBalance.AmountST25 = TSBBalance.AmountST25 - Transaction.AmountST25;
            ResultBalance.AmountST50 = TSBBalance.AmountST50 - Transaction.AmountST50;
            ResultBalance.AmountBHT1 = TSBBalance.AmountBHT1 - Transaction.AmountBHT1;
            ResultBalance.AmountBHT2 = TSBBalance.AmountBHT2 - Transaction.AmountBHT2;
            ResultBalance.AmountBHT5 = TSBBalance.AmountBHT5 - Transaction.AmountBHT5;
            ResultBalance.AmountBHT10 = TSBBalance.AmountBHT10 - Transaction.AmountBHT10;
            ResultBalance.AmountBHT20 = TSBBalance.AmountBHT20 - Transaction.AmountBHT20;
            ResultBalance.AmountBHT50 = TSBBalance.AmountBHT50 - Transaction.AmountBHT50;
            ResultBalance.AmountBHT100 = TSBBalance.AmountBHT100 - Transaction.AmountBHT100;
            ResultBalance.AmountBHT500 = TSBBalance.AmountBHT500 - Transaction.AmountBHT500;
            ResultBalance.AmountBHT1000 = TSBBalance.AmountBHT1000 - Transaction.AmountBHT1000;
        }

        public override bool HasNegative()
        {
            return (
                ResultBalance.AmountST25 < 0 ||
                ResultBalance.AmountST50 < 0 ||
                ResultBalance.AmountBHT1 < 0 ||
                ResultBalance.AmountBHT2 < 0 ||
                ResultBalance.AmountBHT5 < 0 ||
                ResultBalance.AmountBHT10 < 0 ||
                ResultBalance.AmountBHT20 < 0 ||
                ResultBalance.AmountBHT50 < 0 ||
                ResultBalance.AmountBHT100 < 0 ||
                ResultBalance.AmountBHT500 < 0 ||
                ResultBalance.AmountBHT1000 < 0);
        }

        public override bool Save()
        {
            bool result = false;
            // Check User Balance is already inserted
            if (null != UserBalance && UserBalance.UserCreditId == 0)
            {
                // not inserted so insert new record.

                if (string.IsNullOrWhiteSpace(UserBalance.UserId) && null != User)
                {
                    UserBalance.UserId = User.UserId;
                }

                if (null != PlazaGroup)
                {
                    UserBalance.TSBId = PlazaGroup.TSBId;
                    UserBalance.PlazaGroupId = PlazaGroup.PlazaGroupId;
                }
                UserBalance.State = UserCreditBalance.StateTypes.Initial;
                var newBalance = ops.Credits.SaveUserCreditBalance(UserBalance).Value();
                int pkid = (null != newBalance) ? newBalance.UserCreditId : 0;
                UserBalance.UserCreditId = pkid;
            }
            // Save User Credit Transaction.
            if (null != Transaction && null != UserBalance)
            {
                Transaction.UserCreditId = UserBalance.UserCreditId;
                Transaction.TransactionType = UserCreditTransaction.TransactionTypes.Borrow;
                if (string.IsNullOrWhiteSpace(Transaction.TSBId))
                {
                    Transaction.TSBId = UserBalance.TSBId;
                }
                if (string.IsNullOrWhiteSpace(Transaction.PlazaGroupId))
                {
                    Transaction.PlazaGroupId = UserBalance.PlazaGroupId;
                }
                if (string.IsNullOrWhiteSpace(Transaction.UserId))
                {
                    Transaction.UserId = UserBalance.UserId;
                }

                ops.Credits.SaveUserCreditTransaction(Transaction);

                result = true; // save success.
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region UserCreditReturnManager

    public class UserCreditReturnManager : UserCreditManager
    {
        #region Override Methods

        protected override void Calc()
        {
            if (null == ResultBalance || null == TSBBalance || null == Transaction)
                return;
        }

        public override bool Save()
        {
            var result = false;
            if (null != Transaction && null != UserBalance)
            {
                Transaction.UserCreditId = UserBalance.UserCreditId;
                Transaction.TransactionType = UserCreditTransaction.TransactionTypes.Return;
                ops.Credits.SaveUserCreditTransaction(Transaction);

                // Check is total borrow is reach zero.
                var search = Search.UserCredits.GetActiveById.Create(
                    UserBalance.UserId, UserBalance.PlazaGroupId);
                var inst = ops.Credits.GetActiveUserCreditBalanceById(search).Value();
                if (null != inst)
                {
                    if (inst.BHTTotal <= decimal.Zero)
                    {
                        // change source state.
                        UserBalance.State = UserCreditBalance.StateTypes.Completed;
                        ops.Credits.SaveUserCreditBalance(UserBalance);
                    }
                }

                result = true;
            }
            return result;
        }

        public override bool HasNegative()
        {
            return (Transaction.BHTTotal > UserBalance.BHTTotal);
        }

        #endregion
    }

    #endregion
}
