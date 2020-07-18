#region Using

using System;
using System.Collections.Generic;
using System.Linq;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;

// required for JsonIgnore.
using Newtonsoft.Json;
using NLib;
using NLib.Reflection;
using System.ComponentModel;

#endregion

namespace DMT.Models
{
    #region TSBCouponBalance

    /// <summary>
    /// The TSBCouponBalance Data Model class.
    /// </summary>
    //[Table("TSBCouponBalance")]
    public class TSBCouponBalance : NTable<TSBCouponBalance>
    {
        #region Internal Variables

        private int _TSBCouponBalanceId = 0;

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        // TSB Coupon
        private int _TSB_CouponBHT35 = 0;
        private int _TSB_CouponBHT80 = 0;
        private decimal _TSB_CouponTotal = decimal.Zero;
        private decimal _TSB_CouponBHTTotal = decimal.Zero;

        // Coupon 
        private int _CouponBHT35 = 0;
        private int _CouponBHT80 = 0;
        private decimal _CouponTotal = decimal.Zero;
        private decimal _CouponBHTTotal = decimal.Zero;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBCouponBalance() : base() { }

        #endregion

        #region Private Methods

        private void CalcCouponTotal()
        {
            _CouponTotal = _CouponBHT35 + _CouponBHT80;
            this.RaiseChanged("CouponTotal");
        }

        #endregion

        #region Public Properties

        #region Common

        /// <summary>
        /// Gets or sets TSBCouponBalanceId
        /// </summary>
        [Category("Common")]
        [Description(" Gets or sets TSBCouponBalanceId")]
        [ReadOnly(true)]
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("TSBCouponBalanceId")]
        public int TSBCouponBalanceId
        {
            get
            {
                return _TSBCouponBalanceId;
            }
            set
            {
                if (_TSBCouponBalanceId != value)
                {
                    _TSBCouponBalanceId = value;
                    this.RaiseChanged("TSBCouponBalanceId");
                }
            }
        }

        #endregion

        #region TSB

        /// <summary>
        /// Gets or sets TSBId.
        /// </summary>
        [Category("TSB")]
        [Description("Gets or sets TSBId.")]
        [ReadOnly(true)]
        [MaxLength(10)]
        [PeropertyMapName("TSBId")]
        public string TSBId
        {
            get
            {
                return _TSBId;
            }
            set
            {
                if (_TSBId != value)
                {
                    _TSBId = value;
                    this.RaiseChanged("TSBId");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBNameEN.
        /// </summary>
        [Category("TSB")]
        [Description("Gets or sets TSBNameEN.")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("TSBNameEN")]
        public virtual string TSBNameEN
        {
            get
            {
                return _TSBNameEN;
            }
            set
            {
                if (_TSBNameEN != value)
                {
                    _TSBNameEN = value;
                    this.RaiseChanged("TSBNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBNameTH.
        /// </summary>
        [Category("TSB")]
        [Description("Gets or sets TSBNameTH.")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("TSBNameTH")]
        public virtual string TSBNameTH
        {
            get
            {
                return _TSBNameTH;
            }
            set
            {
                if (_TSBNameTH != value)
                {
                    _TSBNameTH = value;
                    this.RaiseChanged("TSBNameTH");
                }
            }
        }

        #endregion

        #region Coupon 

        /// <summary>
        /// Gets or sets number of 35 BHT coupon.
        /// </summary>
        [Category("Coupon")]
        [Description("Gets or sets number of 35 BHT coupon.")]
        [PeropertyMapName("CouponBHT35")]
        public int CouponBHT35
        {
            get { return _CouponBHT35; }
            set
            {
                if (_CouponBHT35 != value)
                {
                    _CouponBHT35 = value;
                    CalcCouponTotal();
                    // Raise event.
                    this.RaiseChanged("CouponBHT35");

                }
            }
        }
        /// <summary>
        /// Gets or sets number of 80 BHT coupon.
        /// </summary>
        [Category("Coupon")]
        [Description("Gets or sets number of 80 BHT coupon.")]
        [PeropertyMapName("CouponBHT80")]
        public int CouponBHT80
        {
            get { return _CouponBHT80; }
            set
            {
                if (_CouponBHT80 != value)
                {
                    _CouponBHT80 = value;
                    CalcCouponTotal();
                    // Raise event.
                    this.RaiseChanged("CouponBHT80");
                }
            }
        }
        /// <summary>
        /// Gets calculate coupon total (book count).
        /// </summary>
        [Category("Coupon")]
        [Description("Gets calculate coupon total (book count).")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        [PeropertyMapName("CouponTotal")]
        public decimal CouponTotal
        {
            get { return _CouponTotal; }
            set { }
        }
        /// <summary>
        /// Gets or sets total value in baht.
        /// </summary>
        [Category("Coupon")]
        [Description("Gets or sets total value in baht.")]
        [ReadOnly(true)]
        [PeropertyMapName("CouponBHTTotal")]
        public decimal CouponBHTTotal
        {
            get { return _CouponBHTTotal; }
            set
            {
                if (_CouponBHTTotal != value)
                {
                    _CouponBHTTotal = value;
                    // Raise event.
                    this.RaiseChanged("CouponBHTTotal");
                }
            }
        }

        #endregion

        #region Additional/User Borrow and TSB BHT Total

        /// <summary>
        /// Gets or sets additional borrow in baht.
        /// </summary>
        [Category("Borrow")]
        [Description("Gets or sets additional borrow in baht.")]
        [PeropertyMapName("AdditionalBHTTotal")]
        public decimal AdditionalBHTTotal
        {
            get { return _AdditionalBHTTotal; }
            set
            {
                if (_AdditionalBHTTotal != value)
                {
                    _AdditionalBHTTotal = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("AdditionalBHTTotal");
                }
            }
        }
        /// <summary>
        /// Gets or sets all user borrow in baht.
        /// </summary>
        [Category("Borrow")]
        [Description("Gets or sets all user borrow in baht.")]
        [ReadOnly(true)]
        [PeropertyMapName("UserBHTTotal")]
        public decimal UserBHTTotal
        {
            get { return _UserBHTTotal; }
            set
            {
                if (_UserBHTTotal != value)
                {
                    _UserBHTTotal = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("UserBHTTotal");
                }
            }
        }

        #endregion

        #region TSB BHT Total

        /// <summary>
        /// Gets or sets TSB Total (From total calc coin/bill + additional + user borrow).
        /// </summary>
        [Category("Summary")]
        [Description("Gets or sets TSB Total (From total calc coin/bill + additional + user borrow).")]
        [ReadOnly(true)]
        //[Ignore]
        //[JsonIgnore]
        [PeropertyMapName("TSBBHTTotal")]
        public decimal TSBBHTTotal
        {
            get { return _TSBBHTTotal; }
            set { }
        }

        #endregion

        #region Status (DC)

        /// <summary>
        /// Gets or sets Status (1 = Sync, 0 = Unsync, etc..)
        /// </summary>
        [Category("DataCenter")]
        [Description("Gets or sets Status (1 = Sync, 0 = Unsync, etc..)")]
        [ReadOnly(true)]
        [PeropertyMapName("Status")]
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    this.RaiseChanged("Status");
                }
            }
        }
        /// <summary>
        /// Gets or sets LastUpdated (Sync to DC).
        /// </summary>
        [Category("DataCenter")]
        [Description("Gets or sets LastUpdated (Sync to DC).")]
        [ReadOnly(true)]
        [PeropertyMapName("LastUpdate")]
        public DateTime LastUpdate
        {
            get { return _LastUpdate; }
            set
            {
                if (_LastUpdate != value)
                {
                    _LastUpdate = value;
                    this.RaiseChanged("LastUpdate");
                }
            }
        }

        #endregion

        #endregion

        #region Internal Class

        public class FKs : TSBBalance
        {
            #region TSB

            /// <summary>
            /// Gets or sets TSBNameEN.
            /// </summary>
            [MaxLength(100)]
            [PeropertyMapName("TSBNameEN")]
            public override string TSBNameEN
            {
                get { return base.TSBNameEN; }
                set { base.TSBNameEN = value; }
            }
            /// <summary>
            /// Gets or sets TSBNameTH.
            /// </summary>
            [MaxLength(100)]
            [PeropertyMapName("TSBNameTH")]
            public override string TSBNameTH
            {
                get { return base.TSBNameTH; }
                set { base.TSBNameTH = value; }
            }

            #endregion

            #region Public Methods

            public TSBBalance ToTSBBalance()
            {
                TSBBalance inst = new TSBBalance();
                this.AssignTo(inst); // set all properties to new instance.
                return inst;
            }

            #endregion
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets Active TSB Balance.
        /// </summary>
        /// <returns>Returns Current Active TSB balance. If not found returns null.</returns>
        public static TSBBalance GetCurrent()
        {
            lock (sync)
            {
                var tsb = TSB.GetCurrent();
                return GetCurrent(tsb);
            }
        }
        /// <summary>
        /// Gets TSB Balance.
        /// </summary>
        /// <param name="tsb">The target TSB to get balance.</param>
        /// <returns>Returns TSB balance. If TSB not found returns null.</returns>
        public static TSBBalance GetCurrent(TSB tsb)
        {
            if (null == tsb) return null;
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT TSBBalance.* ";
                cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                cmd += "  FROM TSBBalance, TSB ";
                cmd += " WHERE TSBBalance.TSBId = TSB.TSBId ";
                cmd += "   AND TSBBalance.TSBId = ? ";

                var ret = NQuery.Query<FKs>(cmd, tsb.TSBId).FirstOrDefault();
                if (null == ret)
                {
                    TSBBalance inst = Create();
                    // assigned TSB info.
                    inst.TSBId = tsb.TSBId;
                    inst.TSBNameEN = tsb.TSBNameEN;
                    inst.TSBNameTH = tsb.TSBNameTH;
                    Save(inst);
                    return inst;
                }
                else
                {
                    return ret.ToTSBBalance();
                }
            }
        }

        #endregion
    }

    #endregion
}
