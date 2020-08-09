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

            public int SaveUserCredit(UserCredit value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<int>(
                    RouteConsts.Credit.SaveUserCredit.Url, value);
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

    #region UserCreditManager

    public class UserCreditManager
    {
        #region Internal Variables

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserCreditManager() : base() { }

        #endregion

        #region Public Methods

        #endregion

        #region Public Properties

        #endregion
    }

    #endregion
}
