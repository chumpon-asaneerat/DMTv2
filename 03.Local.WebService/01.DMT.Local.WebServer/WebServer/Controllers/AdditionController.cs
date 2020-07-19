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
    /// The controller for manage addition transaction TA.
    /// </summary>
    public class AdditionController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Addition.GetCurrentInitial.Name)]
        public TSBAdditionTransaction GetCurrentInitial()
        {
            return TSBAdditionTransaction.GetInitial();
        }

        [HttpPost]
        [ActionName(RouteConsts.Addition.GetInitial.Name)]
        public TSBAdditionTransaction GetInitial([FromBody] TSB tsb)
        {
            return TSBAdditionTransaction.GetInitial(tsb);
        }

        [HttpPost]
        [ActionName(RouteConsts.Addition.SaveTransaction.Name)]
        public void SaveTransaction([FromBody] TSBAdditionTransaction value)
        {
            if (value.TransactionDate == DateTime.MinValue)
            {
                value.TransactionDate = DateTime.Now;
            }
            TSBAdditionTransaction.Save(value);
        }
    }
}
