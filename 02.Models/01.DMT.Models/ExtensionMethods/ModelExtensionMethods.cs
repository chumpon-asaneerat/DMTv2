#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DMT.Models;

#endregion

namespace DMT.Models.ExtensionMethods
{
    #region TAxTOD Server - Local

    /// <summary>
    /// The TAxTOD Extension Methods
    /// </summary>
    public static class TAxTODExtensionMethods
    {
        #region Coupons

        public static TSBCouponTransaction ToLocal(this TAServerCouponTransaction value)
        {
            if (null == value) return null;
            var inst = new TSBCouponTransaction();
            // TODO: map proeprties.

            return inst;
        }

        public static TAServerCouponTransaction ToServer(this TSBCouponTransaction value)
        {
            if (null == value) return null;
            var inst = new TAServerCouponTransaction();
            // TODO: map proeprties.

            return inst;
        }

        #endregion
    }

    #endregion
}
