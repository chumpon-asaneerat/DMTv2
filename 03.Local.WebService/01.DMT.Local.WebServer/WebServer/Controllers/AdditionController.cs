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
        [ActionName(RouteConsts.Addition.GetInitial.Name)]
        public TSBAdditionTransaction GetInitial()
        {
            return TSBAdditionTransaction.GetInitial();
        }
    }
}
