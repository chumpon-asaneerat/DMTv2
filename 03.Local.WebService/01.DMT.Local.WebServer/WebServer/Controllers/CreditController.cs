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
    }
}
