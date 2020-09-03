#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using DMT.Models;
using NLib.Reflection;
using NLib.Utils;

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
            if (inst.TransactionType == TSBCouponTransaction.TransactionTypes.Stock)
            {
                inst.UserId = string.Empty;
            }
            else
            {
                inst.UserId = value.UserId;
            }
            inst.UserReceiveDate = value.UserReceiveDate.Value();

            return inst;
        }

        public static List<TSBCouponTransaction> ToLocals(this List<TAServerCouponTransaction> values)
        {
            var rets = new List<TSBCouponTransaction>();
            if (null != values) 
            {
                values.ForEach(inst =>
                {
                    rets.Add(inst.ToLocal());
                });
            }
            return rets;

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
            if (value.TransactionType == TSBCouponTransaction.TransactionTypes.Stock)
            {
                inst.UserId = string.Empty;
            }
            else
            {
                inst.UserId = value.UserId;
            }
            inst.UserReceiveDate = value.UserReceiveDate.Value();

            return inst;
        }

        public static List<TAServerCouponTransaction> ToServers(this List<TSBCouponTransaction> values)
        {
            var rets = new List<TAServerCouponTransaction>();
            if (null != values)
            {
                values.ForEach(inst =>
                {
                    rets.Add(inst.ToServer());
                });
            }
            return rets;

        }

        #endregion
    }

    #endregion

    #region SCW Server <-> Local Extension Methods

    /// <summary>
    /// The SCW Extension Methods
    /// </summary>
    public static class SCWExtensionMethods
    {
        public static LaneAttendance ToLocal(this SCWJob value)
        {
            if (null == value) return null;

            var inst = new LaneAttendance();
            //value.networkId;

            if (value.plazaId.HasValue)
            {
                inst.PlazaId = value.plazaId.Value.ToString();
            }
            if (value.laneId.HasValue)
            {
                inst.LaneNo = value.laneId.Value;
            }
            inst.UserId = value.staffId;
            inst.JobId = (value.jobNo.HasValue) ? 
                value.jobNo.Value.ToString() : string.Empty;
            inst.Begin = value.bojDateTime.Value();
            inst.End = value.eojDateTime.Value();

            return inst;
        }

        public static SCWJob ToServer(this LaneAttendance value)
        {
            if (null == value) return null;
            var inst = new SCWJob();

            //inst.networkId = 31;
            inst.laneId = value.LaneNo;
            inst.plazaId = (!string.IsNullOrEmpty(value.PlazaId)) ?
                Convert.ToInt32(value.PlazaId) : default(int?);
            inst.staffId = value.UserId;
            inst.jobNo = (!string.IsNullOrEmpty(value.JobId)) ? 
                Convert.ToInt32(value.JobId) : default(int?);
            inst.bojDateTime = value.Begin.Value();
            inst.eojDateTime = value.End.Value();

            return inst;
        }


        public static SCWDeclareCash Create(this List<MCurrency> currencies, 
            decimal value, int number)
        {
            if (null == currencies) return null;
            if (number <= 0) return null;

            var match = currencies.Find((obj) => { return obj.denomValue == value; });
            if (null == match)
                return null;

            SCWDeclareCash inst = new SCWDeclareCash();
            inst.currencyDenomId = match.currencyDenomId;
            inst.currencyId = match.currencyId;
            inst.denomValue = match.denomValue;
            inst.number = number;
            inst.total = inst.denomValue * number;
            return inst;
        }

        public static SCWDeclare ToServer(this RevenueEntry value,
            List<MCurrency> currencies, List<MCoupon> coupons, 
            List<LaneAttendance> jobs)
        {
            if (null == value) return null;
            if (null == currencies) return null;
            if (null == coupons) return null;

            var inst = new SCWDeclare();
            inst.networkId = 31;
            //inst.plazaId = value.PlazaGroupId;
            inst.staffId = value.UserId;

            inst.chiefId = value.SupervisorId;
            inst.chiefName = value.SupervisorNameTH;

            inst.bagNumber = value.BagNo;
            inst.safetyBeltNumber = value.BeltNo;

            //inst.shiftTypeId = ?
            inst.declareDateTime = value.EntryDate;
            inst.attendanceDateTime = value.ShiftBegin;
            inst.departureDateTime = value.ShiftEnd;
            inst.operationDate = value.RevenueDate;
            // Traffic
            inst.cashTotalAmount = value.TrafficBHTTotal;
            inst.cashList = new List<SCWDeclareCash>();

            Action<List<SCWDeclareCash>, decimal, int> addToCashList = (list, bhtVal, num) => 
            {
                if (null == list) return;
                var item = currencies.Create(bhtVal, num);
                if (null == item) return;
                list.Add(item);
            };
            if (inst.cashTotalAmount > 0)
            {
                addToCashList(inst.cashList, (decimal).25, value.TrafficST25);
                addToCashList(inst.cashList, (decimal).5, value.TrafficST50);
                addToCashList(inst.cashList, 1, value.TrafficBHT1);
                addToCashList(inst.cashList, 2, value.TrafficBHT2);
                addToCashList(inst.cashList, 5, value.TrafficBHT5);
                addToCashList(inst.cashList, 10, value.TrafficBHT10);
                addToCashList(inst.cashList, 20, value.TrafficBHT20);
                addToCashList(inst.cashList, 50, value.TrafficBHT50);
                addToCashList(inst.cashList, 100, value.TrafficBHT100);
                addToCashList(inst.cashList, 500, value.TrafficBHT500);
                addToCashList(inst.cashList, 1000, value.TrafficBHT1000);
            }
            // Coupon Sold
            inst.couponTotalAmount = value.CouponSoldBHTTotal;
            inst.couponList = new List<SCWDeclareCoupon>();
            if (inst.couponTotalAmount > 0)
            {

            }
            // Coupon Usage
            inst.couponBookTotalAmount = value.CouponUsageBHT30 +
            value.CouponUsageBHT35 + 
            value.CouponUsageBHT75 + 
            value.CouponUsageBHT80;
            inst.couponBookList = new List<SCWDeclareCouponBook>();
            if (inst.couponBookTotalAmount > 0)
            {

            }
            // Free Pass.
            inst.cardAllowTotalAmount = value.FreePassUsageClassA + 
                value.FreePassUsageOther;
            inst.cardAllowList = new List<SCWDeclareFreePass>();
            if (inst.cardAllowTotalAmount > 0)
            {

            }

            inst.cashRemark = value.TrafficRemark;

            inst.otherTotalAmount = value.OtherBHTTotal;
            inst.otherRemark = value.OtherRemark;

            inst.jobList = new List<SCWJobList>();
            if (null != jobs)
            {

            }

            inst.qrcodeTotalAmount = 0;
            inst.qrcodeList = new List<SCWDeclareQRCode>();

            inst.emvTotalAmount = 0;
            inst.emvList = new List<SCWDeclareEMV>();

            return inst;
        }
    }

    #endregion
}
