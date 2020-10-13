#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DMT.Models;
//using DMT.Models.ExtensionMethods;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// The controller for manage credit transaction TA.
    /// </summary>
    public class CreditController : ApiController
    {
        #region TSB Balance

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetTSBBalance.Name)]
        public NDbResult<TSBCreditBalance> GetTSBBalance([FromBody] TSB value)
        {
            NDbResult<TSBCreditBalance> result;
            if (null == value)
            {
                result = new NDbResult<TSBCreditBalance>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBCreditBalance.GetCurrent(value);
            }
            return result;
        }

        #endregion

        #region TSB Credit Transaction

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetInitialTSBCreditTransaction.Name)]
        public NDbResult<TSBCreditTransaction> GetInitialTSBCreditTransaction([FromBody] TSB value)
        {
            NDbResult<TSBCreditTransaction> result;
            if (null == value)
            {
                result = new NDbResult<TSBCreditTransaction>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBCreditTransaction.GetInitialTransaction(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetReplaceTSBCreditTransaction.Name)]
        public NDbResult<List<TSBCreditTransaction>> GetReplaceTSBCreditTransaction([FromBody] DateTime value)
        {
            NDbResult<List<TSBCreditTransaction>> result;
            if (null == value)
            {
                result = new NDbResult<List<TSBCreditTransaction>>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBCreditTransaction.GetReplaceTransactions(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.SaveTSBCreditTransaction.Name)]
        public NDbResult<TSBCreditTransaction> SaveTSBCreditTransaction(
            [FromBody] TSBCreditTransaction value)
        {
            NDbResult<TSBCreditTransaction> result;
            if (null == value)
            {
                result = new NDbResult<TSBCreditTransaction>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBCreditTransaction.SaveTransaction(value);
            }
            return result;
        }

        #endregion

        #region User Credit Balance

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetActiveUserCreditBalances.Name)]
        public NDbResult<List<UserCreditBalance>> GetActiveUserCreditBalances([FromBody] TSB value)
        {
            NDbResult<List<UserCreditBalance>> result;
            if (null == value)
            {
                result = new NDbResult<List<UserCreditBalance>>();
                result.ParameterIsNull();
            }
            else
            {
                result = UserCreditBalance.GetActiveUserCreditBalances(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetActiveUserCreditBalanceById.Name)]
        public NDbResult<UserCreditBalance> GetActiveUserCreditBalanceById(
            [FromBody] Search.UserCredits.GetActiveById value)
        {
            NDbResult<UserCreditBalance> result;
            if (null == value)
            {
                result = new NDbResult<UserCreditBalance>();
                result.ParameterIsNull();
            }
            else
            {
                result = UserCreditBalance.GetActiveUserCreditBalanceById(
                    value.UserId, value.PlazaGroupId);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetNoRevenueEntryUserCreditBalanceById.Name)]
        public NDbResult<UserCreditBalance> GetNoRevenueEntryUserCreditBalanceById(
            [FromBody] Search.UserCredits.GetActiveById value)
        {
            NDbResult<UserCreditBalance> result;
            if (null == value)
            {
                result = new NDbResult<UserCreditBalance>();
                result.ParameterIsNull();
            }
            else
            {
                result = UserCreditBalance.GetNoRevenueEntryUserCreditBalanceById(
                    value.UserId, value.PlazaGroupId);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.SaveUserCreditBalance.Name)]
        public NDbResult<UserCreditBalance> SaveUserCreditBalance(
            [FromBody] UserCreditBalance value)
        {
            NDbResult<UserCreditBalance> result;
            if (null == value)
            {
                result = new NDbResult<UserCreditBalance>();
                result.ParameterIsNull();
            }
            else
            {

                result = UserCreditBalance.SaveUserCreditBalance(value);
            }
            return result;
        }

        #endregion

        #region User Credit Transaction

        [HttpPost]
        [ActionName(RouteConsts.Credit.SaveUserCreditTransaction.Name)]
        public NDbResult<UserCreditTransaction> SaveUserCreditTransaction(
            [FromBody] UserCreditTransaction value)
        {
            NDbResult<UserCreditTransaction> result;
            if (null == value)
            {
                result = new NDbResult<UserCreditTransaction>();
                result.ParameterIsNull();
            }
            else
            {
                result = UserCreditTransaction.SaveUserCreditTransaction(value);
            }
            return result;
        }

        #endregion
    }
}
