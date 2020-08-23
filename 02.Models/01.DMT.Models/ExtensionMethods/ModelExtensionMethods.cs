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
            return (value == DateTime.MinValue) ? new DateTime?() : value;
        }

        #endregion

        #region Int

        public static int Value(this int? value)
        {
            return value ?? 0;
        }

        public static int? Value(this int value)
        {
            return (value == int.MinValue) ? new int?() : value;
        }

        #endregion

        #region Decimal

        public static decimal Value(this decimal? value)
        {
            return value ?? decimal.MinValue;
        }

        public static decimal? Value(this decimal value)
        {
            return (value == decimal.MinValue) ? new decimal?() : value;
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
            
            inst.TransactionDate = value.TransactionDate.Value();
            inst.TransactionType = (TSBCouponTransaction.TransactionTypes)value.CouponStatus.Value();
            inst.CouponId = value.SerialNo;
            inst.CouponPK = value.CouponPK.Value();
            inst.CouponType = (CouponType)value.CouponType.Value();
            inst.FinishFlag = (TSBCouponTransaction.FinishedFlags)value.FinishFlag.Value();
            inst.Price = value.Price.Value();
            inst.SoldBy = value.SoldBy;
            inst.SoldDate = value.SoldDate.Value();
            inst.TSBId = value.TSBId;
            inst.UserId = value.UserId;
            inst.UserReceiveDate = value.UserReceiveDate.Value();

            return inst;
        }

        public static TAServerCouponTransaction ToServer(this TSBCouponTransaction value)
        {
            if (null == value) return null;
            var inst = new TAServerCouponTransaction();

            inst.TransactionDate = value.TransactionDate.Value();
            inst.CouponStatus = (int)value.TransactionType;
            inst.SerialNo = value.CouponId;
            inst.CouponPK = value.CouponPK.Value();
            inst.CouponType = (int)value.CouponType;
            inst.FinishFlag = (int)value.FinishFlag;
            inst.Price = value.Price.Value();
            inst.SoldBy = value.SoldBy;
            inst.SoldDate = value.SoldDate.Value();
            inst.TSBId = value.TSBId;
            inst.UserId = value.UserId;
            inst.UserReceiveDate = value.UserReceiveDate.Value();

            return inst;
        }

        #endregion
    }

    #endregion
}
