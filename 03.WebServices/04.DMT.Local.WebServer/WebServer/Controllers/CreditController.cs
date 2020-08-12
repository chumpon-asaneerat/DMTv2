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
        public NDbResult<TSBCreditBalance> GetTSBBalance([FromBody] TSB tsb)
        {
            var ret = TSBCreditBalance.GetCurrent(tsb);
            return ret;
        }

        #endregion

        #region TSB Credit Transaction

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetInitialTSBCreditTransaction.Name)]
        public NDbResult<TSBCreditTransaction> GetInitialTSBCreditTransaction([FromBody] TSB tsb)
        {
            return TSBCreditTransaction.GetInitialTransaction(tsb);
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.SaveTSBCreditTransaction.Name)]
        public NDbResult<TSBCreditTransaction> SaveTSBCreditTransaction(
            [FromBody] TSBCreditTransaction value)
        {
            if (null == value)
            {
                Console.WriteLine("Transaction is null.");
                return;
            }
            Console.WriteLine("Transaction is : {0}", value);
            TSBCreditTransaction.SaveTransaction(value);
        }

        #endregion

        #region User Credit Balance

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetActiveUserCreditBalances.Name)]
        public NDbResult<List<UserCreditBalance>> GetActiveUserCreditBalances([FromBody] TSB value)
        {
            if (null == value) return null;
            var ret = UserCreditBalance.GetActiveUserCreditBalances(value);
            return ret;
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetActiveUserCreditBalanceById.Name)]
        public NDbResult<UserCreditBalance> GetActiveUserCreditBalanceById([FromBody] Search.UserCredits.GetActiveById value)
        {
            if (null == value) return null;
            var ret = UserCreditBalance.GetActiveUserCreditBalanceById(value.UserId, value.PlazaGroupId);
            return ret;
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.SaveUserCreditBalance.Name)]
        public NDbResult<UserCreditTransaction> SaveUserCreditBalance([FromBody] UserCreditBalance value)
        {
            // save
            return UserCreditBalance.SaveUserCreditBalance(value);
        }

        #endregion

        #region User Credit Transaction

        [HttpPost]
        [ActionName(RouteConsts.Credit.SaveUserCreditTransaction.Name)]
        public NDbResult<UserCreditTransaction> SaveUserCreditTransaction([FromBody] UserCreditTransaction value)
        {
            return UserCreditTransaction.SaveUserCreditTransaction(value);
        }

        #endregion

        /*

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetActiveUserCredit.Name)]
        public UserCredit GetActiveUserCredit([FromBody] Search.UserCredits.GetActive value)
        {
            if (null == value) return null;
            var ret = UserCredit.GetActive(value.User, value.PlazaGroup);
            return ret;
        }
        */
    }
}
