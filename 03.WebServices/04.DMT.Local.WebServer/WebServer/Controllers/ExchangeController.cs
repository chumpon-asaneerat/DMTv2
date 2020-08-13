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
        public NDbResult<List<TSBExchangeTransaction>> GetTSBExchangeTransactions(
            [FromBody] TSB value)
        {
            NDbResult<List<TSBExchangeTransaction>> result;
            if (null == value)
            {
                result = new NDbResult<List<TSBExchangeTransaction>>();
                result.ParameterIsNull();
                result.data = new List<TSBExchangeTransaction>();
            }
            else
            {
                result = TSBExchangeTransaction.GetTransactions(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Exchange.SaveTSBExchangeTransaction.Name)]
        public NDbResult<TSBExchangeTransaction> SaveTSBExchangeTransaction(
            [FromBody] TSBExchangeTransaction value)
        {
            NDbResult<TSBExchangeTransaction> result;
            if (null == value)
            {
                result = new NDbResult<TSBExchangeTransaction>();
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                if (value.TransactionDate == DateTime.MinValue)
                {
                    value.TransactionDate = DateTime.Now;
                }
                result = TSBExchangeTransaction.SaveTransaction(value);
            }
            return result;
        }

        #endregion
    }
}
