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
    [RoutePrefix("api/transaction/credit")]
    public class CreditController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Transaction.Credit.GetInitial.Name)]
        public TSBCreditTransaction GetInitial()
        {
            return TSBCreditTransaction.GetInitial();
        }
    }

    /// <summary>
    /// The controller for manage coupon transaction TA.
    /// </summary>
    [RoutePrefix("api/transaction/coupon")]
    public class CouponController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Transaction.Coupon.GetInitial.Name)]
        public TSBCouponTransaction GetInitial()
        {
            return TSBCouponTransaction.GetInitial();
        }
    }

    /// <summary>
    /// The controller for manage addition transaction TA.
    /// </summary>
    [RoutePrefix("api/transaction/addition")]
    public class AdditionController : ApiController
    {
        [HttpPost]
        [ActionName(RouteConsts.Transaction.Addition.GetInitial.Name)]
        public TSBAdditionTransaction GetInitial()
        {
            return TSBAdditionTransaction.GetInitial();
        }
    }
}
