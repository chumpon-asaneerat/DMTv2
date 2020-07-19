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
        [ActionName(RouteConsts.Credit.GetInitial.Name)]
        public TSBCreditTransaction GetInitial()
        {
            return TSBCreditTransaction.GetInitial();
        }
    }
}
