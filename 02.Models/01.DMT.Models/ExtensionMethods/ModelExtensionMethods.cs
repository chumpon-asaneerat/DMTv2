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
    #region Nullable Type Extenstion Methods

    public static class NullableTypeExtensionMethods
    {
        #region DateTime

        public static DateTime Value(this DateTime? value)
        {
            return value ?? DateTime.MinValue;
        }

        public static DateTime? Value(this DateTime value)
        {
            return (value == DateTime.MinValue) ? default : value;
        }

        #endregion

        #region Int

        public static int Value(this int? value)
        {
            return value ?? 0;
        }

        public static int? Value(this int value)
        {
            return (value == int.MinValue) ? default : value;
        }

        #endregion

        #region Decimal

        public static decimal Value(this decimal? value)
        {
            return value ?? decimal.MinValue;
        }

        public static decimal? Value(this decimal value)
        {
            return (value == decimal.MinValue) ? default : value;
        }

        #endregion
    }

    #endregion

    #region TAxTOD Server <-> Local Extension Methods

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
            inst.TransactionDate = value.TransactionDate.Value();
            inst.CouponPK = value.CouponPK.Value();

            return inst;
        }

        public static TAServerCouponTransaction ToServer(this TSBCouponTransaction value)
        {
            if (null == value) return null;
            var inst = new TAServerCouponTransaction();
            // TODO: map proeprties.
            inst.TransactionDate = value.TransactionDate.Value();
            inst.CouponPK = value.CouponPK.Value();

            return inst;
        }

        #endregion
    }

    #endregion
}
