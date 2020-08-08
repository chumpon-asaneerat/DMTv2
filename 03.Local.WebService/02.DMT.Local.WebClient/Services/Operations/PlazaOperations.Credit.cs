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

            #region TSB Balance

            public TSBCreditBalance GetTSBBalance(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBCreditBalance>(
                    RouteConsts.Credit.GetTSBBalance.Url, tsb);
                return ret;
            }

            #endregion

            /*
            public TSBCreditTransaction GetCurrentInitial()
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBCreditTransaction>(
                    RouteConsts.Credit.GetCurrentInitial.Url, new { });
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

            public List<UserCredit> GetActiveUserCredits(TSB value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<UserCredit>>(
                    RouteConsts.Credit.GetActiveUserCredits.Url, value);
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

    #region TSBCreditManager

    public class TSBCreditManager
    {
        #region Internal Variables

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBCreditManager() : base() { }

        #endregion

        #region Private Methods


        #endregion

        #region Public Methods

        public void Refresh()
        {
            this.TSB = ops.TSB.GetCurrent();
        }

        #endregion

        #region Public Properties

        #region TSB

        /// <summary>
        /// Gets Current TSB.
        /// </summary>
        public TSB TSB { get; private set; }

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
}
