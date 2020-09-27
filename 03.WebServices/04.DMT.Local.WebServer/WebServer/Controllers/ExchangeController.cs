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
        [ActionName(RouteConsts.Exchange.GetTSBExchangeGroups.Name)]
        public NDbResult<List<TSBExchangeTransaction>> GetTSBExchangeGroups(
            [FromBody] TSB value)
        {
            NDbResult<List<TSBExchangeTransaction>> result;
            if (null == value)
            {
                result = new NDbResult<List<TSBExchangeTransaction>>();
                result.ParameterIsNull();
            }
            else
            {
                result = TSBExchangeGroup.GetTSBExchangeGroups(value);
            }
            return result;
        }

        [HttpPost]
        [ActionName(RouteConsts.Exchange.SaveTSBExchangeGroup.Name)]
        public NDbResult<TSBExchangeTransaction> SaveTSBExchangeGroup(
            [FromBody] TSBExchangeTransaction value)
        {
            NDbResult<TSBExchangeTransaction> result;
            if (null == value)
            {
                result = new NDbResult<TSBExchangeTransaction>();
                result.ParameterIsNull();
            }
            else
            {
                if (value.TransactionDate == DateTime.MinValue)
                {
                    value.TransactionDate = DateTime.Now;
                }
                result = TSBExchangeGroup.SaveTSBExchangeGroup(value);
            }
            return result;
        }

        #endregion
    }
}
