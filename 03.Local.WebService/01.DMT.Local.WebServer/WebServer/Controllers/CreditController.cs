#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using DMT.Models;
using DMT.Models.ExtensionMethods;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// The controller for manage credit transaction TA.
    /// </summary>
    public class CreditController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Credit.GetCurrentInitial.Name)]
        public TSBCreditTransaction GetCurrentInitial()
        {
            return TSBCreditTransaction.GetInitial();
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetInitial.Name)]
        public TSBCreditTransaction GetInitial([FromBody] TSB tsb)
        {
            return TSBCreditTransaction.GetInitial(tsb);
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.SaveTransaction.Name)]
        public void SaveTransaction([FromBody] TSBCreditTransaction value)
        {
            if (value.TransactionDate == DateTime.MinValue)
            {
                value.TransactionDate = DateTime.Now;
            }
            TSBCreditTransaction.Save(value);
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetCurrent.Name)]
        public TSBCreditBalance GetCurrent()
        {
            var ret = TSBCreditBalance.GetCurrent();
            return ret;
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.GetTSBCurrent.Name)]
        public TSBCreditBalance GetTSBCurrent([FromBody] TSB tsb)
        {
            var ret = TSBCreditBalance.GetCurrent(tsb);
            return ret;
        }

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
        [ActionName(RouteConsts.Credit.GetActiveUserCredits.Name)]
        public List<UserCredit> GetActiveUserCredits([FromBody] TSB value)
        {
            if (null == value) return null;
            var ret = UserCredit.GetActives(value);
            return ret;
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.SaveUserCredit.Name)]
        public void SaveUserCredit([FromBody] UserCredit value)
        {
            if (value.UserCreditDate == DateTime.MinValue)
            {
                value.UserCreditDate = DateTime.Now;
            }
            UserCredit.Save(value);
        }

        [HttpPost]
        [ActionName(RouteConsts.Credit.SaveUserTransaction.Name)]
        public void SaveUserTransaction([FromBody] UserCreditTransaction value)
        {
            if (value.TransactionDate == DateTime.MinValue)
            {
                value.TransactionDate = DateTime.Now;
            }
            UserCreditTransaction.Save(value);
        }
    }
}
