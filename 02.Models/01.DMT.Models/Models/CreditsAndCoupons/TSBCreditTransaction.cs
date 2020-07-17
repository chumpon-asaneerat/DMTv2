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
    #region TSBCreditTransaction

    /// <summary>
    /// The TSBCreditTransaction Data Model class.
    /// </summary>
    //[Table("TSBCredit")]
    public class TSBCreditTransaction : NTable<TSBCreditTransaction>
    {
        #region Internal Variables

        private Guid _PKId = Guid.NewGuid();
        private DateTime _TransactionDate = DateTime.MinValue;

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

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBCreditTransaction() : base() { }

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
            // Raise event.
            this.RaiseChanged("BHTTotal");
        }

        #endregion

        #region Public Properties

        #region Common

        /// <summary>
        /// Gets or sets PKId.
        /// </summary>
        [Category("Common")]
        [Description("Gets or sets PKId.")]
        [ReadOnly(true)]
        [PrimaryKey]
        [PeropertyMapName("PKId")]
        public Guid PKId
        {
            get
            {
                return _PKId;
            }
            set
            {
                if (_PKId != value)
                {
                    _PKId = value;
                    this.RaiseChanged("PKId");
                }
            }
        }
        /// <summary>
        /// Gets or sets Transaction Date.
        /// </summary>
        [Category("Common")]
        [Description("Gets or sets Transaction Date.")]
        [ReadOnly(true)]
        [PeropertyMapName("TransactionDate")]
        public DateTime TransactionDate
        {
            get { return _TransactionDate; }
            set
            {
                if (_TransactionDate != value)
                {
                    _TransactionDate = value;
                    // Raise event.
                    this.RaiseChanged("TransactionDate");
                    this.RaiseChanged("TransactionDateString");
                    this.RaiseChanged("TransactionDateTimeString");
                }
            }
        }
        /// <summary>
        /// Gets Transaction Date String.
        /// </summary>
        [Category("Common")]
        [Description("Gets Transaction Date String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string TransactionDateString
        {
            get
            {
                var ret = (this.TransactionDate == DateTime.MinValue) ? "" : this.TransactionDate.ToThaiDateTimeString("dd/MM/yyyy");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Transaction DateTime String.
        /// </summary>
        [Category("Common")]
        [Description("Gets Transaction DateTime String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string TransactionDateTimeString
        {
            get
            {
                var ret = (this.TransactionDate == DateTime.MinValue) ? "" : this.TransactionDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
                return ret;
            }
            set { }
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
        [PeropertyMapName("BHTTotal")]
        public decimal BHTTotal
        {
            get { return _BHTTotal; }
            set { }
        }
        /// <summary>
        /// Gets or sets  Remark.
        /// </summary>
        [Category("Remark")]
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

        public class FKs : TSBCreditTransaction
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

            public TSBCreditTransaction ToTSBCreditTransaction()
            {
                TSBCreditTransaction inst = new TSBCreditTransaction();
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
