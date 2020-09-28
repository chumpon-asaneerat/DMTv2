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
        [ActionName(RouteConsts.Exchange.GetRequestApproveTSBExchangeGroups.Name)]
        public NDbResult<List<TSBExchangeGroup>> GetRequestApproveTSBExchangeGroups(
            [FromBody] TSB value)
        {
            NDbResult<List<TSBExchangeGroup>> result;
            if (null == value)
            {
                result = new NDbResult<List<TSBExchangeGroup>>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBExchangeGroup.GetRequestApproveTSBExchangeGroups(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Exchange.GetTSBExchangeGroups.Name)]
        public NDbResult<List<TSBExchangeGroup>> GetTSBExchangeGroups(
            [FromBody] Search.Exchanges.Filter value)
        {
            NDbResult<List<TSBExchangeGroup>> result;
            if (null == value)
            {
                result = new NDbResult<List<TSBExchangeGroup>>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBExchangeGroup.GetTSBExchangeGroups(value.TSB, value.State, value.FinishedFlag,
                    value.ReqBegin, value.ReqEnd);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Exchange.SaveTSBExchangeGroup.Name)]
        public NDbResult<TSBExchangeGroup> SaveTSBExchangeGroup(
            [FromBody] TSBExchangeGroup value)
        {
            NDbResult<TSBExchangeGroup> result;
            if (null == value)
            {
                result = new NDbResult<TSBExchangeGroup>();
                result.ParameterIsNull();
            }
            else
            {
                if (value.RequestDate == DateTime.MinValue)
                {
                    value.RequestDate = DateTime.Now;
                }
                result = TSBExchangeGroup.SaveTSBExchangeGroup(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Exchange.GetTSBExchangeTransactions.Name)]
        public NDbResult<List<TSBExchangeTransaction>> GetTSBExchangeTransactions(
            [FromBody] Search.Exchanges.Transactions.Filter value)
        {
            NDbResult<List<TSBExchangeTransaction>> result;
            if (null == value)
            {
                result = new NDbResult<List<TSBExchangeTransaction>>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBExchangeTransaction.GetTransactions(value.TSB, value.GroupId);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Exchange.GetTSBExchangeTransaction.Name)]
        public NDbResult<TSBExchangeTransaction> GetTSBExchangeTransaction(
            [FromBody] Search.Exchanges.Transactions.Filter value)
        {
            NDbResult<TSBExchangeTransaction> result;
            if (null == value)
            {
                result = new NDbResult<TSBExchangeTransaction>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBExchangeTransaction.GetTransaction(value.TSB, value.GroupId, value.TransactionType);
            }
            return result;
        }

        #endregion
    }
}
