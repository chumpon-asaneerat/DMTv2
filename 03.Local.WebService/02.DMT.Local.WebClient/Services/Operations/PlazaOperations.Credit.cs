#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;
using NLib.Reflection;

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

            #region TSB Credit Balance

            public TSBCreditBalance GetTSBBalance(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBCreditBalance>(
                    RouteConsts.Credit.GetTSBBalance.Url, tsb);
                return ret;
            }

            #endregion

            #region TSB Credit Transaction

            public TSBCreditTransaction GetInitialTSBCreditTransaction(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBCreditTransaction>(
                    RouteConsts.Credit.GetInitialTSBCreditTransaction.Url, tsb);
                return ret;
            }

            public void SaveTSBCreditTransaction(TSBCreditTransaction value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Credit.SaveTSBCreditTransaction.Url, value);
            }

            #endregion

            #region User Credit Balance

            public List<UserCreditBalance> GetActiveUserCreditBalances(TSB value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<UserCreditBalance>>(
                    RouteConsts.Credit.GetActiveUserCreditBalances.Url, value);
                return ret;
            }

            public int SaveUserCreditBalance(UserCreditBalance value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<int>(
                    RouteConsts.Credit.SaveUserCreditBalance.Url, value);
                return ret;
            }

            #endregion

            #region User Credit Transaction

            #endregion

            /*

            public UserCredit GetActiveUserCredit(Search.UserCredits.GetActive value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<UserCredit>(
                    RouteConsts.Credit.GetActiveUserCredit.Url, value);
                return ret;
            }

            public UserCredit GetActiveUserCreditById(Search.UserCredits.GetActiveById value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<UserCredit>(
                    RouteConsts.Credit.GetActiveUserCreditById.Url, value);
                return ret;
            }

            public void SaveUserTransaction(UserCreditTransaction value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Credit.SaveUserTransaction.Url, value);
            }
            */
            #endregion
        }

        #endregion
    }

    #region TSBCreditManager (abstract)

    public abstract class TSBCreditManager
    {
        #region Internal Variables

        protected PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

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
                return ops.Credits.GetTSBBalance(this.TSB);
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

        #region Private Methods

        #endregion

        #region Protected Methods

        protected override void OnRefresh() { }

        #endregion

        #region Public Methods

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
                return ops.Credits.GetInitialTSBCreditTransaction(TSB);
            }
        }

        #endregion
    }

    #endregion

    #region UserCreditManager (abstract)

    public abstract class UserCreditManager
    {
        #region Internal Variables

        protected PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

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

            this.TSBBalance = ops.Credits.GetTSBBalance(tsb);
            this.ResultBalance = new TSBCreditBalance();

            this.TSBBalance.AssignTo(this.ResultBalance);


            // Hook Event to recalcuate when transaction's property changed.
            this.Transaction.PropertyChanged += Transaction_PropertyChanged;
        }
        /*
        public void SetUser(User user)
        {
            if (null != user)
            {
                UserBalance.UserId = user.UserId;
                UserBalance.FullNameEN = user.FullNameEN;
                UserBalance.FullNameTH = user.FullNameTH;
            }
        }
        */
        public abstract void Save();

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
        public User User { get; set; }
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

        public override void Save()
        {
            // Check User Balance is already inserted
            if (null != UserBalance && UserBalance.UserCreditId == 0)
            {
                // not inserted so insert new record.
                if (null != PlazaGroup)
                {
                    UserBalance.TSBId = PlazaGroup.TSBId;
                    UserBalance.PlazaGroupId = PlazaGroup.PlazaGroupId;
                }
                UserBalance.State = UserCreditBalance.StateTypes.Initial;
                int pkid = ops.Credits.SaveUserCreditBalance(UserBalance);
                UserBalance.UserCreditId = pkid;

            }
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

        public override void Save()
        {

        }

        #endregion
    }

    #endregion
}
