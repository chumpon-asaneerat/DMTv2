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
        public TSBCreditBalance GetTSBBalance([FromBody] TSB tsb)
        {
            var ret = TSBCreditBalance.GetCurrent(tsb);
            return ret;
        }

        #endregion

        #region TSB Credit Transaction

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetInitialTSBCreditTransaction.Name)]
        public TSBCreditTransaction GetInitialTSBCreditTransaction([FromBody] TSB tsb)
        {
            return TSBCreditTransaction.GetInitialTransaction(tsb);
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.SaveTSBCreditTransaction.Name)]
        public void SaveTSBCreditTransaction([FromBody] TSBCreditTransaction value)
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
        public List<UserCreditBalance> GetActiveUserCreditBalances([FromBody] TSB value)
        {
            if (null == value) return null;
            var ret = UserCreditBalance.GetActiveUserCreditBalances(value);
            return ret;
        }

        #endregion

        #region User Credit Transaction

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

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetActiveUserCreditById.Name)]
        public UserCredit GetActiveUserCreditById([FromBody] Search.UserCredits.GetActiveById value)
        {
            if (null == value) return null;
            var ret = UserCredit.GetActive(value.UserId, value.PlazaGroupId);
            return ret;
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.SaveUserCredit.Name)]
        public int SaveUserCredit([FromBody] UserCredit value)
        {
            // save
            return UserCredit.SaveCredit(value);
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.SaveUserTransaction.Name)]
        public void SaveUserTransaction([FromBody] UserCreditTransaction value)
        {
            UserCreditTransaction.SaveTransaction(value);
        }
        */
    }
}
