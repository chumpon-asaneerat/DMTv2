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
    #region TSBBalance

    /// <summary>
    /// The TSBBalance Data Model class.
    /// </summary>
    //[Table("TSBBalance")]
    public class TSBBalance : NTable<TSBBalance>
    {
        #region Internal Variables

        private int _TSBBalanceId = 0;

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        // Coin/Bill
        private int _ST25 = 0;
        private int _ST50 = 0;
        private int _BHT1 = 0;
        private int _BHT2 = 0;
        private int _BHT5 = 0;
        private int _BHT10 = 0;
        private int _BHT20 = 0;
        private int _BHT50 = 0;
        private int _BHT100 = 0;
        private int _BHT500 = 0;
        private int _BHT1000 = 0;
        private decimal _BHTTotal = decimal.Zero;
        private string _Remark = "";

        // Coupon 
        private int _CouponBHT35 = 0;
        private int _CouponBHT80 = 0;
        private decimal _CouponTotal = decimal.Zero;
        private decimal _CouponBHTTotal = decimal.Zero;

        // Additional Borrow
        private decimal _AdditionalBHTTotal = decimal.Zero;
        // Collector Borrow
        private decimal _UserBHTTotal = decimal.Zero;
        // TSB Total.
        private decimal _TSBBHTTotal = decimal.Zero;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBBalance() : base() { }

        #endregion

        #region Private Methods

        private void CalcTotal()
        {
            decimal total = 0;
            total += Convert.ToDecimal(_ST25 * (decimal).25);
            total += Convert.ToDecimal(_ST50 * (decimal).50);
            total += _BHT1 * 1;
            total += _BHT2 * 2;
            total += _BHT5 * 5;
            total += _BHT10 * 10;
            total += _BHT20 * 20;
            total += _BHT50 * 50;
            total += _BHT100 * 100;
            total += _BHT500 * 500;
            total += _BHT1000 * 1000;

            _BHTTotal = total;

            _TSBBHTTotal = total + _AdditionalBHTTotal + _UserBHTTotal;
            // Raise event.
            this.RaiseChanged("BHTTotal");
            this.RaiseChanged("TSBBHTTotal");
        }

        private void CalcCouponTotal()
        {
            _CouponTotal = _CouponBHT35 + _CouponBHT80;
            this.RaiseChanged("CouponTotal");
        }

        #endregion

        #region Public Properties

        #region Common

        /// <summary>
        /// Gets or sets TSBBalanceId
        /// </summary>
        [Category("Common")]
        [Description(" Gets or sets TSBBalanceId")]
        [ReadOnly(true)]
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("TSBBalanceId")]
        public int TSBBalanceId
        {
            get
            {
                return _TSBBalanceId;
            }
            set
            {
                if (_TSBBalanceId != value)
                {
                    _TSBBalanceId = value;
                    this.RaiseChanged("TSBBalanceId");
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

        #region Coin/Bill

        /// <summary>
        /// Gets or sets number of .25 baht coin.
        /// </summary>
        [Category("Coin/Bill")]
        [Description("Gets or sets number of .25 baht coin.")]
        [PeropertyMapName("ST25")]
        public int ST25
        {
            get { return _ST25; }
            set
            {
                if (_ST25 != value)
                {
                    _ST25 = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("ST25");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of .50 baht coin.
        /// </summary>
        [Category("Coin/Bill")]
        [Description("Gets or sets number of .50 baht coin.")]
        [PeropertyMapName("ST50")]
        public int ST50
        {
            get { return _ST50; }
            set
            {
                if (_ST50 != value)
                {
                    _ST50 = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("ST50");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 1 baht coin.
        /// </summary>
        [Category("Coin/Bill")]
        [Description("Gets or sets number of 1 baht coin.")]
        [PeropertyMapName("BHT1")]
        public int BHT1
        {
            get { return _BHT1; }
            set
            {
                if (_BHT1 != value)
                {
                    _BHT1 = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("BHT1BHT1");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 2 baht coin.
        /// </summary>
        [Category("Coin/Bill")]
        [Description("Gets or sets number of 2 baht coin.")]
        [PeropertyMapName("BHT2")]
        public int BHT2
        {
            get { return _BHT2; }
            set
            {
                if (_BHT2 != value)
                {
                    _BHT2 = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("BHT2");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 5 baht coin.
        /// </summary>
        [Category("Coin/Bill")]
        [Description("Gets or sets number of 5 baht coin.")]
        [PeropertyMapName("BHT5")]
        public int BHT5
        {
            get { return _BHT5; }
            set
            {
                if (_BHT5 != value)
                {
                    _BHT5 = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("BHT5");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 10 baht coin.
        /// </summary>
        [Category("Coin/Bill")]
        [Description("Gets or sets number of 10 baht coin.")]
        [PeropertyMapName("BHT10")]
        public int BHT10
        {
            get { return _BHT10; }
            set
            {
                if (_BHT10 != value)
                {
                    _BHT10 = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("BHT10");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 20 baht bill.
        /// </summary>
        [Category("Coin/Bill")]
        [Description("Gets or sets number of 20 baht bill.")]
        [PeropertyMapName("BHT20")]
        public int BHT20
        {
            get { return _BHT20; }
            set
            {
                if (_BHT20 != value)
                {
                    _BHT20 = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("BHT20");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 50 baht bill.
        /// </summary>
        [Category("Coin/Bill")]
        [Description("Gets or sets number of 50 baht bill.")]
        [PeropertyMapName("BHT50")]
        public int BHT50
        {
            get { return _BHT50; }
            set
            {
                if (_BHT50 != value)
                {
                    _BHT50 = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("BHT50");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 100 baht bill.
        /// </summary>
        [Category("Coin/Bill")]
        [Description("Gets or sets number of 100 baht bill.")]
        [PeropertyMapName("BHT100")]
        public int BHT100
        {
            get { return _BHT100; }
            set
            {
                if (_BHT100 != value)
                {
                    _BHT100 = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("BHT100");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 500 baht bill.
        /// </summary>
        [Category("Coin/Bill")]
        [Description("Gets or sets number of 500 baht bill.")]
        [PeropertyMapName("BHT500")]
        public int BHT500
        {
            get { return _BHT500; }
            set
            {
                if (_BHT500 != value)
                {
                    _BHT500 = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("BHT500");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 1000 baht bill.
        /// </summary>
        [Category("Coin/Bill")]
        [Description("Gets or sets number of 1000 baht bill.")]
        [PeropertyMapName("BHT1000")]
        public int BHT1000
        {
            get { return _BHT1000; }
            set
            {
                if (_BHT1000 != value)
                {
                    _BHT1000 = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("BHT1000");
                }
            }
        }
        /// <summary>
        /// Gets or sets total value in baht.
        /// </summary>
        [Category("Coin/Bill")]
        [Description("Gets or sets total value in baht.")]
        [ReadOnly(true)]
        //[Ignore]
        //[JsonIgnore]
        [PeropertyMapName("BHTTotal")]
        public decimal BHTTotal
        {
            get { return _BHTTotal; }
            set { }
        }
        /// <summary>
        /// Gets or sets  Remark.
        /// </summary>
        [Category("Renark")]
        [Description("Gets or sets  Remark.")]
        [MaxLength(255)]
        [PeropertyMapName("Remark")]
        public string Remark
        {
            get { return _Remark; }
            set
            {
                if (_Remark != value)
                {
                    _Remark = value;
                    // Raise event.
                    this.RaiseChanged("Remark");
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
            set{ }
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
