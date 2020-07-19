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
    #region TSBCreditBalance (For Query only)

    /// <summary>
    /// The TSBCreditBalance Data Model class.
    /// </summary>
    //[Table("TSBCreditBalance")]
    public class TSBCreditBalance : NTable<TSBCreditBalance>
    {
        #region Internal Variables

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

        // Additional Borrow
        private decimal _AdditionalBHTTotal = decimal.Zero;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBCreditBalance() : base() { }

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
        }

        #endregion

        #region Public Properties

        #region TSB

        /// <summary>
        /// Gets or sets TSBId.
        /// </summary>
        [Category("TSB")]
        [Description("Gets or sets TSBId.")]
        [ReadOnly(true)]
        [Ignore]
        [JsonIgnore]
        [MaxLength(10)]
        [PeropertyMapName("TSBId")]
        public virtual string TSBId
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
        [ReadOnly(true)]
        [Ignore]
        [JsonIgnore]
        [PeropertyMapName("ST25")]
        public virtual int ST25
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
        [ReadOnly(true)]
        [Ignore]
        [JsonIgnore]
        [PeropertyMapName("ST50")]
        public virtual int ST50
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
        [ReadOnly(true)]
        [Ignore]
        [JsonIgnore]
        [PeropertyMapName("BHT1")]
        public virtual int BHT1
        {
            get { return _BHT1; }
            set
            {
                if (_BHT1 != value)
                {
                    _BHT1 = value;
                    CalcTotal();
                    // Raise event.
                    this.RaiseChanged("BHT1");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 2 baht coin.
        /// </summary>
        [Category("Coin/Bill")]
        [Description("Gets or sets number of 2 baht coin.")]
        [ReadOnly(true)]
        [Ignore]
        [JsonIgnore]
        [PeropertyMapName("BHT2")]
        public virtual int BHT2
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
        [ReadOnly(true)]
        [Ignore]
        [JsonIgnore]
        [PeropertyMapName("BHT5")]
        public virtual int BHT5
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
        [ReadOnly(true)]
        [Ignore]
        [JsonIgnore]
        [PeropertyMapName("BHT10")]
        public virtual int BHT10
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
        [ReadOnly(true)]
        [Ignore]
        [JsonIgnore]
        [PeropertyMapName("BHT20")]
        public virtual int BHT20
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
        [ReadOnly(true)]
        [Ignore]
        [JsonIgnore]
        [PeropertyMapName("BHT50")]
        public virtual int BHT50
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
        [ReadOnly(true)]
        [Ignore]
        [JsonIgnore]
        [PeropertyMapName("BHT100")]
        public virtual int BHT100
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
        [ReadOnly(true)]
        [Ignore]
        [JsonIgnore]
        [PeropertyMapName("BHT500")]
        public virtual int BHT500
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
        [ReadOnly(true)]
        [Ignore]
        [JsonIgnore]
        [PeropertyMapName("BHT1000")]
        public virtual int BHT1000
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
        [Ignore]
        [JsonIgnore]
        [PeropertyMapName("BHTTotal")]
        public decimal BHTTotal
        {
            get { return _BHTTotal; }
            set { }
        }

        #endregion

        #region Additional

        /// <summary>
        /// Gets or sets additional borrow in baht.
        /// </summary>
        [Category("Additional")]
        [Description("Gets or sets additional borrow/return in baht.")]
        [ReadOnly(true)]
        [Ignore]
        [JsonIgnore]
        [PeropertyMapName("AdditionalBHTTotal")]
        public virtual decimal AdditionalBHTTotal
        {
            get { return _AdditionalBHTTotal; }
            set
            {
                if (_AdditionalBHTTotal != value)
                {
                    _AdditionalBHTTotal = value;
                    // Raise event.
                    this.RaiseChanged("AdditionalBHTTotal");
                }
            }
        }

        #endregion

        #endregion

        #region Internal Class

        public class FKs : TSBCreditBalance
        {
            #region TSB

            /// <summary>
            /// Gets or sets TSBId.
            /// </summary>
            [MaxLength(10)]
            [PeropertyMapName("TSBId")]
            public override string TSBId
            {
                get { return base.TSBId; }
                set { base.TSBId = value; }
            }
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

            #region Coin/Bill

            /// <summary>
            /// Gets or sets number of .25 baht coin.
            /// </summary>
            [PeropertyMapName("ST25")]
            public override int ST25
            {
                get { return base.ST25; }
                set { base.ST25 = value; }
            }
            /// <summary>
            /// Gets or sets number of .50 baht coin.
            /// </summary>
            [PeropertyMapName("ST50")]
            public override int ST50
            {
                get { return base.ST50; }
                set { base.ST50 = value; }
            }
            /// <summary>
            /// Gets or sets number of 1 baht coin.
            /// </summary>
            [PeropertyMapName("BHT1")]
            public override int BHT1
            {
                get { return base.BHT1; }
                set { base.BHT1 = value; }
            }
            /// <summary>
            /// Gets or sets number of 2 baht coin.
            /// </summary>
            [PeropertyMapName("BHT2")]
            public override int BHT2
            {
                get { return base.BHT2; }
                set { base.BHT2 = value; }
            }
            /// <summary>
            /// Gets or sets number of 5 baht coin.
            /// </summary>
            [PeropertyMapName("BHT5")]
            public override int BHT5
            {
                get { return base.BHT5; }
                set { base.BHT5 = value; }
            }
            /// <summary>
            /// Gets or sets number of 10 baht coin.
            /// </summary>
            [PeropertyMapName("BHT10")]
            public override int BHT10
            {
                get { return base.BHT10; }
                set { base.BHT10 = value; }
            }
            /// <summary>
            /// Gets or sets number of 20 baht bill.
            /// </summary>
            [PeropertyMapName("BHT20")]
            public override int BHT20
            {
                get { return base.BHT20; }
                set { base.BHT20 = value; }
            }
            /// <summary>
            /// Gets or sets number of 50 baht bill.
            /// </summary>
            [PeropertyMapName("BHT50")]
            public override int BHT50
            {
                get { return base.BHT50; }
                set { base.BHT50 = value; }
            }
            /// <summary>
            /// Gets or sets number of 100 baht bill.
            /// </summary>
            [PeropertyMapName("BHT100")]
            public override int BHT100
            {
                get { return base.BHT100; }
                set { base.BHT100 = value; }
            }
            /// <summary>
            /// Gets or sets number of 500 baht bill.
            /// </summary>
            [PeropertyMapName("BHT500")]
            public override int BHT500
            {
                get { return base.BHT500; }
                set { base.BHT500 = value; }
            }
            /// <summary>
            /// Gets or sets number of 1000 baht bill.
            /// </summary>
            [PeropertyMapName("BHT1000")]
            public override int BHT1000
            {
                get { return base.BHT1000; }
                set { base.BHT1000 = value; }
            }

            #endregion

            #region Additional

            /// <summary>
            /// Gets or sets additional borrow in baht.
            /// </summary>
            [PeropertyMapName("AdditionalBHTTotal")]
            public override decimal AdditionalBHTTotal
            {
                get { return base.AdditionalBHTTotal; }
                set { base.AdditionalBHTTotal = value; }
            }

            #endregion

            #region Public Methods

            public TSBCreditBalance ToTSBCreditBalance()
            {
                TSBCreditBalance inst = new TSBCreditBalance();
                this.AssignTo(inst); // set all properties to new instance.
                return inst;
            }

            #endregion
        }

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion
}
