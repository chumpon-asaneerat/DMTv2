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
    /// The controller for manage credit exchange transaction TA.
    /// </summary>
    public class ExchangeController : ApiController
    {
        #region Exchange Transaction

        [HttpPost]
        [ActionName(RouteConsts.Exchange.GetTSBExchangeTransactions.Name)]
        public List<TSBExchangeTransaction> GetTSBExchangeTransactions([FromBody] TSB tsb)
        {
            if (null == tsb)
                return TSBExchangeTransaction.GetTSBExchangeTransactions();
            return TSBExchangeTransaction.GetTSBExchangeTransactions(tsb);
        }

        [HttpPost]
        [ActionName(RouteConsts.Exchange.SaveTSBExchangeTransaction.Name)]
        public void SaveTSBExchangeTransaction([FromBody] TSBExchangeTransaction value)
        {
            if (value.TransactionDate == DateTime.MinValue)
            {
                value.TransactionDate = DateTime.Now;
            }
            TSBExchangeTransaction.SaveTSBExchangeTransaction(value);
        }

        #endregion
    }
}
