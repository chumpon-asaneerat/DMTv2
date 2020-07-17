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
    #region Revenue Entry

    /// <summary>
    /// The RevenueEntry class.
    /// </summary>
    //[Table("RevenueEntry")]
    public class RevenueEntry : NTable<RevenueEntry>
    {
        #region Intenral Variables

        private Guid _PKId = Guid.NewGuid();
        private DateTime _EntryDate = DateTime.MinValue;
        private DateTime _RevenueDate = DateTime.MinValue;
        private string _RevenueId = string.Empty;
        private string _BagNo = string.Empty;
        private string _BeltNo = string.Empty;

        private string _Lanes = string.Empty;
        private string _PlazaGroups = string.Empty;

        private DateTime _ShiftBegin = DateTime.MinValue;
        private DateTime _ShiftEnd = DateTime.MinValue;

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        private string _PlazaGroupId = string.Empty;
        private string _PlazaGroupNameEN = string.Empty;
        private string _PlazaGroupNameTH = string.Empty;
        private string _Direction = string.Empty;

        private int _ShiftId = 0;
        private string _ShiftNameTH = string.Empty;
        private string _ShiftNameEN = string.Empty;

        private string _UserId = string.Empty;
        private string _FullNameEN = string.Empty;
        private string _FullNameTH = string.Empty;

        private string _SupervisorId = string.Empty;
        private string _SupervisorNameEN = string.Empty;
        private string _SupervisorNameTH = string.Empty;

        // Traffic
        private int _TrafficST25 = 0;
        private int _TrafficST50 = 0;
        private int _TrafficBHT1 = 0;
        private int _TrafficBHT2 = 0;
        private int _TrafficBHT5 = 0;
        private int _TrafficBHT10 = 0;
        private int _TrafficBHT20 = 0;
        private int _TrafficBHT50 = 0;
        private int _TrafficBHT100 = 0;
        private int _TrafficBHT500 = 0;
        private int _TrafficBHT1000 = 0;
        private decimal _TrafficBHTTotal = decimal.Zero;
        private string _TrafficRemark = "";
        // Other
        private decimal _OtherBHTTotal = decimal.Zero;
        private string _OtherRemark = "";
        // Coupon Usage
        private int _CouponUsageBHT30 = 0;
        private int _CouponUsageBHT35 = 0;
        private int _CouponUsageBHT75 = 0;
        private int _CouponUsageBHT80 = 0;
        // Free Pass
        private int _FreePassUsageClassA = 0;
        private int _FreePassUsageOther = 0;
        // Coupon Sold
        private int _CouponSoldBHT35 = 0;
        private int _CouponSoldBHT80 = 0;
        private decimal _CouponSoldBHT35Factor = 665;
        private decimal _CouponSoldBHT80Factor = 1520;
        private decimal _CouponSoldBHT35Total = decimal.Zero;
        private decimal _CouponSoldBHT80Total = decimal.Zero;
        private decimal _CouponSoldBHTTotal = decimal.Zero;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueEntry() : base()
        {
        }

        #endregion

        #region Private Methods

        private void CalcTrafficTotal()
        {
            decimal total = 0;
            total += Convert.ToDecimal(_TrafficST25 * (decimal).25);
            total += Convert.ToDecimal(_TrafficST50 * (decimal).50);
            total += _TrafficBHT1 * 1;
            total += _TrafficBHT2 * 2;
            total += _TrafficBHT5 * 5;
            total += _TrafficBHT10 * 10;
            total += _TrafficBHT20 * 20;
            total += _TrafficBHT50 * 50;
            total += _TrafficBHT100 * 100;
            total += _TrafficBHT500 * 500;
            total += _TrafficBHT1000 * 1000;

            _TrafficBHTTotal = total;
            // Raise event.
            this.RaiseChanged("TrafficBHTTotal");
        }

        private void CalcCouponSoldTotal()
        {
            // Raise event.
            RaiseChanged("CntTotal");

            _CouponSoldBHT35Total = Convert.ToDecimal(_CouponSoldBHT35 * _CouponSoldBHT35Factor);
            this.RaiseChanged("CouponSoldBHT35Total");

            _CouponSoldBHT80Total = Convert.ToDecimal(_CouponSoldBHT80 * _CouponSoldBHT80Factor);
            this.RaiseChanged("CouponSoldBHT80Total");

            decimal total = 0;
            total += _CouponSoldBHT35Total;
            total += _CouponSoldBHT80Total;

            _CouponSoldBHTTotal = total;
            // Raise event.
            this.RaiseChanged("CouponSoldBHTTotal");
        }

        #endregion

        #region Public Properties

        #region Common

        /// <summary>
        /// Gets or sets PKId
        /// </summary>
        [Category("Common")]
        [Description("Gets or sets PKId")]
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
        /// Gets or sets Entry Date.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets or sets Entry Date.")]
        //[ReadOnly(true)]
        [PeropertyMapName("EntryDate")]
        public DateTime EntryDate
        {
            get { return _EntryDate; }
            set
            {
                if (_EntryDate != value)
                {
                    _EntryDate = value;
                    // Raise event.
                    this.RaiseChanged("EntryDate");
                }
            }
        }
        /// <summary>
        /// Gets Entry Date String.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets Entry Date String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string EntryDateString
        {
            get
            {
                var ret = (this._EntryDate == DateTime.MinValue) ? "" : this._EntryDate.ToThaiDateTimeString("dd/MM/yyyy");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Entry DateTime String.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets Entry DateTime String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string EntryDateTimeString
        {
            get
            {
                var ret = (this._EntryDate == DateTime.MinValue) ? "" : this._EntryDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets or sets Revenue Date.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets or sets Revenue Date.")]
        //[ReadOnly(true)]
        [PeropertyMapName("RevenueDate")]
        public DateTime RevenueDate
        {
            get { return _RevenueDate; }
            set
            {
                if (_RevenueDate != value)
                {
                    _RevenueDate = value;
                    // Raise event.
                    this.RaiseChanged("RevenueDate");
                    this.RaiseChanged("RevenueDateString");
                    this.RaiseChanged("RevenueDateTimeString");
                }
            }
        }
        /// <summary>
        /// Gets Revenue Date String.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets Revenue Date String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string RevenueDateString
        {
            get
            {
                var ret = (this._RevenueDate == DateTime.MinValue) ? "" : this._RevenueDate.ToThaiDateTimeString("dd/MM/yyyy");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Revenue DateTime String.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets Revenue DateTime String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string RevenueDateTimeString
        {
            get
            {
                var ret = (this._RevenueDate == DateTime.MinValue) ? "" : this._RevenueDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets or sets RevenueId.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets or sets RevenueId.")]
        //[ReadOnly(true)]
        [MaxLength(20)]
        [PeropertyMapName("RevenueId")]
        public string RevenueId
        {
            get { return _RevenueId; }
            set
            {
                if (_RevenueId != value)
                {
                    _RevenueId = value;
                    // Raise event.
                    this.RaiseChanged("RevenueId");
                }
            }
        }
        /// <summary>
        /// Gets or sets Bag Number.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets or sets Bag Number.")]
        //[ReadOnly(true)]
        [MaxLength(10)]
        [PeropertyMapName("BagNo")]
        public string BagNo
        {
            get { return _BagNo; }
            set
            {
                if (_BagNo != value)
                {
                    _BagNo = value;
                    // Raise event.
                    this.RaiseChanged("BagNo");
                }
            }
        }
        /// <summary>
        /// Gets or sets Belt Number.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets or sets Belt Number.")]
        //[ReadOnly(true)]
        [MaxLength(20)]
        [PeropertyMapName("BeltNo")]
        public string BeltNo
        {
            get { return _BeltNo; }
            set
            {
                if (_BeltNo != value)
                {
                    _BeltNo = value;
                    // Raise event.
                    this.RaiseChanged("BeltNo");
                }
            }
        }
        /// <summary>
        /// Gets or sets Lane Lists.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets or sets Lane Lists.")]
        //[ReadOnly(true)]
        [MaxLength(100)]
        [PeropertyMapName("Lanes")]
        public string Lanes
        {
            get { return _Lanes; }
            set
            {
                if (_Lanes != value)
                {
                    _Lanes = value;
                    // Raise event.
                    this.RaiseChanged("Lanes");
                }
            }
        }
        /// <summary>
        /// Gets or sets PlazaGroups Lists.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets or sets PlazaGroups Lists.")]
        //[ReadOnly(true)]
        [MaxLength(100)]
        [PeropertyMapName("PlazaGroups")]
        public string PlazaGroups
        {
            get { return _PlazaGroups; }
            set
            {
                if (_PlazaGroups != value)
                {
                    _PlazaGroups = value;
                    // Raise event.
                    this.RaiseChanged("PlazaGroups");
                }
            }
        }
        /// <summary>
        /// Gets or sets Shift Begin.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets or sets Shift Begin.")]
        //[ReadOnly(true)]
        [PeropertyMapName("ShiftBegin")]
        public DateTime ShiftBegin
        {
            get { return _ShiftBegin; }
            set
            {
                if (_ShiftBegin != value)
                {
                    _ShiftBegin = value;
                    // Raise event.
                    this.RaiseChanged("ShiftBegin");
                }
            }
        }
        /// <summary>
        /// Gets or sets Shift End.
        /// </summary>
        [Category("Revenue")]
        [Description(" Gets or sets Shift End.")]
        //[ReadOnly(true)]
        [PeropertyMapName("ShiftEnd")]
        public DateTime ShiftEnd
        {
            get { return _ShiftEnd; }
            set
            {
                if (_ShiftEnd != value)
                {
                    _ShiftEnd = value;
                    // Raise event.
                    this.RaiseChanged("ShiftEnd");
                }
            }
        }
        /// <summary>
        /// Gets Shift Begin Date String.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets Shift Begin Date String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string ShiftBeginDateString
        {
            get
            {
                var ret = (this.ShiftBegin == DateTime.MinValue) ? "" : this.ShiftBegin.ToThaiDateTimeString("dd/MM/yyyy");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Shift End Date String.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets Shift End Date String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string ShiftEndDateString
        {
            get
            {
                var ret = (this.ShiftEnd == DateTime.MinValue) ? "" : this.ShiftEnd.ToThaiDateTimeString("dd/MM/yyyy");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Shift Begin Time String.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets Shift Begin Time String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string ShiftBeginTimeString
        {
            get
            {
                var ret = (this.ShiftBegin == DateTime.MinValue) ? "" : this.ShiftBegin.ToThaiTimeString();
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Shift End Time String.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets Shift End Time String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string ShiftEndTimeString
        {
            get
            {
                var ret = (this.ShiftEnd == DateTime.MinValue) ? "" : this.ShiftEnd.ToThaiTimeString();
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Shift Begin DateTime String.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets Shift Begin DateTime String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string ShiftBeginDateTimeString
        {
            get
            {
                var ret = (this.ShiftBegin == DateTime.MinValue) ? "" : this.ShiftBegin.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Shift End DateTime String.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets Shift End DateTime String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string ShiftEndDateTimeString
        {
            get
            {
                var ret = (this.ShiftEnd == DateTime.MinValue) ? "" : this.ShiftEnd.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
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

        #region PlazaGroup

        /// <summary>
        /// Gets or sets PlazaGroupId.
        /// </summary>
        [Category("Plaza Group")]
        [Description("Gets or sets PlazaGroupId.")]
        [ReadOnly(true)]
        [MaxLength(10)]
        [PeropertyMapName("PlazaGroupId")]
        public string PlazaGroupId
        {
            get
            {
                return _PlazaGroupId;
            }
            set
            {
                if (_PlazaGroupId != value)
                {
                    _PlazaGroupId = value;
                    this.RaiseChanged("PlazaGroupId");
                }
            }
        }
        /// <summary>
        /// Gets or sets PlazaGroupNameEN.
        /// </summary>
        [Category("Plaza Group")]
        [Description("Gets or sets PlazaGroupNameEN.")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("PlazaGroupNameEN")]
        public virtual string PlazaGroupNameEN
        {
            get
            {
                return _PlazaGroupNameEN;
            }
            set
            {
                if (_PlazaGroupNameEN != value)
                {
                    _PlazaGroupNameEN = value;
                    this.RaiseChanged("PlazaGroupNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets PlazaGroupNameTH.
        /// </summary>
        [Category("Plaza Group")]
        [Description("Gets or sets PlazaGroupNameTH.")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("PlazaGroupNameTH")]
        public virtual string PlazaGroupNameTH
        {
            get
            {
                return _PlazaGroupNameTH;
            }
            set
            {
                if (_PlazaGroupNameTH != value)
                {
                    _PlazaGroupNameTH = value;
                    this.RaiseChanged("PlazaGroupNameTH");
                }
            }
        }
        /// <summary>
        /// Gets or sets Direction.
        /// </summary>
        [Category("Plaza Group")]
        [Description("Gets or sets Direction.")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("Direction")]
        public virtual string Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                if (_Direction != value)
                {
                    _Direction = value;
                    this.RaiseChanged("Direction");
                }
            }
        }

        #endregion

        #region Shift

        /// <summary>
        /// Gets or sets ShiftId.
        /// </summary>
        [Category("Shift")]
        [Description("Gets or sets ShiftId.")]
        [ReadOnly(true)]
        [PeropertyMapName("ShiftId")]
        public int ShiftId
        {
            get
            {
                return _ShiftId;
            }
            set
            {
                if (_ShiftId != value)
                {
                    _ShiftId = value;
                    this.RaiseChanged("ShiftId");
                }
            }
        }
        /// <summary>
        /// Gets or sets Name TH.
        /// </summary>
        [Category("Shift")]
        [Description("Gets or sets Name TH.")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("ShiftNameTH")]
        public virtual string ShiftNameTH
        {
            get
            {
                return _ShiftNameTH;
            }
            set
            {
                if (_ShiftNameTH != value)
                {
                    _ShiftNameTH = value;
                    this.RaiseChanged("ShiftNameTH");
                }
            }
        }
        /// <summary>
        /// Gets or sets Name EN.
        /// </summary>
        [Category("Shift")]
        [Description("Gets or sets Name EN.")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("ShiftNameEN")]
        public virtual string ShiftNameEN
        {
            get
            {
                return _ShiftNameEN;
            }
            set
            {
                if (_ShiftNameEN != value)
                {
                    _ShiftNameEN = value;
                    this.RaiseChanged("ShiftNameEN");
                }
            }
        }

        #endregion

        #region User

        /// <summary>
        /// Gets or sets UserId
        /// </summary>
        [Category("User")]
        [Description("Gets or sets UserId.")]
        [ReadOnly(true)]
        [MaxLength(10)]
        [PeropertyMapName("UserId")]
        public string UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if (_UserId != value)
                {
                    _UserId = value;
                    this.RaiseChanged("UserId");
                }
            }
        }
        /// <summary>
        /// Gets or sets FullNameEN
        /// </summary>
        [Category("User")]
        [Description("Gets or sets User FullName EN.")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("FullNameEN")]
        public virtual string FullNameEN
        {
            get
            {
                return _FullNameEN;
            }
            set
            {
                if (_FullNameEN != value)
                {
                    _FullNameEN = value;
                    this.RaiseChanged("FullNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets FullNameTH
        /// </summary>
        [Category("User")]
        [Description("Gets or sets User FullName TH.")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("FullNameTH")]
        public virtual string FullNameTH
        {
            get
            {
                return _FullNameTH;
            }
            set
            {
                if (_FullNameTH != value)
                {
                    _FullNameTH = value;
                    this.RaiseChanged("FullNameTH");
                }
            }
        }

        #endregion

        #region Supervisor

        /// <summary>
        /// Gets or sets SupervisorId
        /// </summary>
        [Category("Supervisor")]
        [Description("Gets or sets SupervisorId")]
        [ReadOnly(true)]
        [MaxLength(10)]
        [PeropertyMapName("SupervisorId")]
        public string SupervisorId
        {
            get
            {
                return _SupervisorId;
            }
            set
            {
                if (_SupervisorId != value)
                {
                    _SupervisorId = value;
                    this.RaiseChanged("SupervisorId");
                }
            }
        }
        /// <summary>
        /// Gets or sets SupervisorNameEN
        /// </summary>
        [Category("Supervisor")]
        [Description("Gets or sets SupervisorNameEN")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("SupervisorNameEN")]
        public virtual string SupervisorNameEN
        {
            get
            {
                return _SupervisorNameEN;
            }
            set
            {
                if (_SupervisorNameEN != value)
                {
                    _SupervisorNameEN = value;
                    this.RaiseChanged("SupervisorNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets SupervisorNameTH
        /// </summary>
        [Category("Supervisor")]
        [Description("Gets or sets SupervisorNameTH")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("SupervisorNameTH")]
        public virtual string SupervisorNameTH
        {
            get
            {
                return _SupervisorNameTH;
            }
            set
            {
                if (_SupervisorNameTH != value)
                {
                    _SupervisorNameTH = value;
                    this.RaiseChanged("SupervisorNameTH");
                }
            }
        }

        #endregion

        #region Traffic

        /// <summary>
        /// Gets or sets number of .25 baht coin.
        /// </summary>
        [Category("Traffic")]
        [Description("Gets or sets number of .25 baht coin.")]
        [PeropertyMapName("TrafficST25")]
        public int TrafficST25
        {
            get { return _TrafficST25; }
            set
            {
                if (_TrafficST25 != value)
                {
                    _TrafficST25 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficST25");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of .50 baht coin.
        /// </summary>
        [Category("Traffic")]
        [Description("Gets or sets number of .50 baht coin.")]
        [PeropertyMapName("TrafficST50")]
        public int TrafficST50
        {
            get { return _TrafficST50; }
            set
            {
                if (_TrafficST50 != value)
                {
                    _TrafficST50 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficST50");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 1 baht coin.
        /// </summary>
        [Category("Traffic")]
        [Description("Gets or sets number of 1 baht coin.")]
        [PeropertyMapName("TrafficBHT1")]
        public int TrafficBHT1
        {
            get { return _TrafficBHT1; }
            set
            {
                if (_TrafficBHT1 != value)
                {
                    _TrafficBHT1 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT1BHT1");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 2 baht coin.
        /// </summary>
        [Category("Traffic")]
        [Description("Gets or sets number of 2 baht coin.")]
        [PeropertyMapName("TrafficBHT2")]
        public int TrafficBHT2
        {
            get { return _TrafficBHT2; }
            set
            {
                if (_TrafficBHT2 != value)
                {
                    _TrafficBHT2 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT2");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 5 baht coin.
        /// </summary>
        [Category("Traffic")]
        [Description("Gets or sets number of 5 baht coin.")]
        [PeropertyMapName("TrafficBHT5")]
        public int TrafficBHT5
        {
            get { return _TrafficBHT5; }
            set
            {
                if (_TrafficBHT5 != value)
                {
                    _TrafficBHT5 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT5");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 10 baht coin.
        /// </summary>
        [Category("Traffic")]
        [Description("Gets or sets number of 10 baht coin.")]
        [PeropertyMapName("TrafficBHT10")]
        public int TrafficBHT10
        {
            get { return _TrafficBHT10; }
            set
            {
                if (_TrafficBHT10 != value)
                {
                    _TrafficBHT10 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT10");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 20 baht bill.
        /// </summary>
        [Category("Traffic")]
        [Description("Gets or sets number of 20 baht bill.")]
        [PeropertyMapName("TrafficBHT20")]
        public int TrafficBHT20
        {
            get { return _TrafficBHT20; }
            set
            {
                if (_TrafficBHT20 != value)
                {
                    _TrafficBHT20 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT20");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 50 baht bill.
        /// </summary>
        [Category("Traffic")]
        [Description("Gets or sets number of 50 baht bill.")]
        [PeropertyMapName("TrafficBHT50")]
        public int TrafficBHT50
        {
            get { return _TrafficBHT50; }
            set
            {
                if (_TrafficBHT50 != value)
                {
                    _TrafficBHT50 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT50");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 100 baht bill.
        /// </summary>
        [Category("Traffic")]
        [Description("Gets or sets number of 100 baht bill.")]
        [PeropertyMapName("TrafficBHT100")]
        public int TrafficBHT100
        {
            get { return _TrafficBHT100; }
            set
            {
                if (_TrafficBHT100 != value)
                {
                    _TrafficBHT100 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT100");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 500 baht bill.
        /// </summary>
        [Category("Traffic")]
        [Description("Gets or sets number of 500 baht bill.")]
        [PeropertyMapName("TrafficBHT500")]
        public int TrafficBHT500
        {
            get { return _TrafficBHT500; }
            set
            {
                if (_TrafficBHT500 != value)
                {
                    _TrafficBHT500 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT500");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 1000 baht bill.
        /// </summary>
        [Category("Traffic")]
        [Description("Gets or sets number of 1000 baht bill.")]
        [PeropertyMapName("TrafficBHT1000")]
        public int TrafficBHT1000
        {
            get { return _TrafficBHT1000; }
            set
            {
                if (_TrafficBHT1000 != value)
                {
                    _TrafficBHT1000 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT1000");
                }
            }
        }
        /// <summary>
        /// Gets or sets total value in baht.
        /// </summary>
        [Category("Traffic")]
        [Description("Gets or sets total value in baht.")]
        [ReadOnly(true)]
        [PeropertyMapName("TrafficBHTTotal")]
        public decimal TrafficBHTTotal
        {
            get { return _TrafficBHTTotal; }
            set { }
        }
        /// <summary>
        /// Gets or sets Traffic Remark.
        /// </summary>
        [Category("Traffic")]
        [Description("Gets or sets Traffic Remark.")]
        [MaxLength(255)]
        [PeropertyMapName("TrafficRemark")]
        public string TrafficRemark
        {
            get { return _TrafficRemark; }
            set
            {
                if (_TrafficRemark != value)
                {
                    _TrafficRemark = value;
                    // Raise event.
                    this.RaiseChanged("TrafficRemark");
                }
            }
        }

        #endregion

        #region Other

        /// <summary>
        /// Gets or sets total value in baht (Other).
        /// </summary>
        [Category("Other")]
        [Description("Gets or sets total value in baht (Other).")]
        [PeropertyMapName("OtherBHTTotal")]
        public decimal OtherBHTTotal
        {
            get { return _OtherBHTTotal; }
            set
            {
                if (_OtherBHTTotal != value)
                {
                    _OtherBHTTotal = value;
                    // Raise event.
                    this.RaiseChanged("OtherBHTTotal");
                }
            }
        }
        /// <summary>
        /// Gets or sets Other Remark.
        /// </summary>
        [Category("Other")]
        [Description("Gets or sets Other Remark.")]
        [MaxLength(255)]
        [PeropertyMapName("OtherRemark")]
        public string OtherRemark
        {
            get { return _OtherRemark; }
            set
            {
                if (_OtherRemark != value)
                {
                    _OtherRemark = value;
                    // Raise event.
                    this.RaiseChanged("OtherRemark");
                }
            }
        }

        #endregion

        #region Coupon Usage

        /// <summary>
        /// Gets or sets number of 30 BHT coupon.
        /// </summary>
        [Category("Coupon Usage")]
        [Description("Gets or sets number of 30 BHT coupon.")]
        [PeropertyMapName("CouponUsageBHT30")]
        public int CouponUsageBHT30
        {
            get { return _CouponUsageBHT30; }
            set
            {
                if (_CouponUsageBHT30 != value)
                {
                    _CouponUsageBHT30 = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageBHT30");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 35 BHT coupon.
        /// </summary>
        [Category("Coupon Usage")]
        [Description("Gets or sets number of 35 BHT coupon.")]
        [PeropertyMapName("CouponUsageBHT35")]
        public int CouponUsageBHT35
        {
            get { return _CouponUsageBHT35; }
            set
            {
                if (_CouponUsageBHT35 != value)
                {
                    _CouponUsageBHT35 = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageBHT35");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 75 BHT coupon.
        /// </summary>
        [Category("Coupon Usage")]
        [Description("Gets or sets number of 75 BHT coupon.")]
        [PeropertyMapName("CouponUsageBHT75")]
        public int CouponUsageBHT75
        {
            get { return _CouponUsageBHT75; }
            set
            {
                if (_CouponUsageBHT75 != value)
                {
                    _CouponUsageBHT75 = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageBHT75");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 80 BHT coupon.
        /// </summary>
        [Category("Coupon Usage")]
        [Description("Gets or sets number of 80 BHT coupon.")]
        [PeropertyMapName("CouponUsageBHT80")]
        public int CouponUsageBHT80
        {
            get { return _CouponUsageBHT80; }
            set
            {
                if (_CouponUsageBHT80 != value)
                {
                    _CouponUsageBHT80 = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageBHT80");
                }
            }
        }

        #endregion

        #region FreePass (Usage)

        /// <summary>
        /// Gets or sets number of FreePass Class A (4 wheel).
        /// </summary>
        [Category("FreePass")]
        [Description("Gets or sets number of FreePass Class A (4 wheel).")]
        [PeropertyMapName("FreePassUsageClassA")]
        public int FreePassUsageClassA
        {
            get { return _FreePassUsageClassA; }
            set
            {
                if (_FreePassUsageClassA != value)
                {
                    _FreePassUsageClassA = value;
                    // Raise event.
                    this.RaiseChanged("FreePassUsageClassA");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of FreePass Other (> 4 wheel).
        /// </summary>
        [Category("FreePass")]
        [Description("Gets or sets number of FreePass Other (> 4 wheel).")]
        [PeropertyMapName("FreePassUsageOther")]
        public int FreePassUsageOther
        {
            get { return _FreePassUsageOther; }
            set
            {
                if (_FreePassUsageOther != value)
                {
                    _FreePassUsageOther = value;
                    // Raise event.
                    this.RaiseChanged("FreePassUsageOther");
                }
            }
        }

        #endregion

        #region Coupon Sold

        /// <summary>
        /// Gets or sets number of 35 BHT coupon.
        /// </summary>
        [Category("Coupon Sold")]
        [Description("Gets or sets number of 35 BHT coupon.")]
        [PeropertyMapName("CouponSoldBHT35")]
        public int CouponSoldBHT35
        {
            get { return _CouponSoldBHT35; }
            set
            {
                if (_CouponSoldBHT35 != value)
                {
                    _CouponSoldBHT35 = value;
                    CalcCouponSoldTotal();
                    // Raise event.
                    this.RaiseChanged("CouponSoldBHT35");

                }
            }
        }
        /// <summary>
        /// Gets or sets number of 80 BHT coupon.
        /// </summary>
        [Category("Coupon Sold")]
        [Description("Gets or sets number of 80 BHT coupon.")]
        [PeropertyMapName("CouponSoldBHT80")]
        public int CouponSoldBHT80
        {
            get { return _CouponSoldBHT80; }
            set
            {
                if (_CouponSoldBHT80 != value)
                {
                    _CouponSoldBHT80 = value;
                    CalcCouponSoldTotal();
                    // Raise event.
                    this.RaiseChanged("CouponSoldBHT80");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 35 BHT coupon factor.
        /// </summary>
        [Category("Coupon Sold")]
        [Description("Gets or sets number of 35 BHT coupon factor.")]
        [PeropertyMapName("CouponSoldBHT35Factor")]
        public decimal CouponSoldBHT35Factor
        {
            get { return _CouponSoldBHT35Factor; }
            set
            {
                if (_CouponSoldBHT35Factor != value)
                {
                    _CouponSoldBHT35Factor = value;
                    CalcCouponSoldTotal();
                    // Raise event.
                    this.RaiseChanged("CouponSoldBHT35Factor");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 80 BHT coupon factor.
        /// </summary>
        [Category("Coupon Sold")]
        [Description("Gets or sets number of 80 BHT coupon factor.")]
        [PeropertyMapName("CouponSoldBHT80Factor")]
        public decimal CouponSoldBHT80Factor
        {
            get { return _CouponSoldBHT80Factor; }
            set
            {
                if (_CouponSoldBHT80Factor != value)
                {
                    _CouponSoldBHT80Factor = value;
                    CalcCouponSoldTotal();
                    // Raise event.
                    this.RaiseChanged("CouponSoldBHT80Factor");
                }
            }
        }
        /// <summary>
        /// Gets 35 BHT coupon total in BHT.
        /// </summary>
        [Category("Coupon Sold")]
        [Description("Gets 35 BHT coupon total in BHT.")]
        [ReadOnly(true)]
        [PeropertyMapName("CouponSoldBHT35Total")]
        public decimal CouponSoldBHT35Total
        {
            get { return _CouponSoldBHT35Total; }
            set { }
        }
        /// <summary>
        /// Gets 80 BHT coupon total in BHT.
        /// </summary>
        [Category("Coupon Sold")]
        [Description("Gets 80 BHT coupon total in BHT.")]
        [ReadOnly(true)]
        [PeropertyMapName("CouponSoldBHT80Total")]
        public decimal CouponSoldBHT80Total
        {
            get { return _CouponSoldBHT80Total; }
            set { }
        }
        /// <summary>
        /// Gets or sets total value in baht.
        /// </summary>
        [Category("Coupon Sold")]
        [Description("Gets or sets total value in baht.")]
        [ReadOnly(true)]
        [PeropertyMapName("CouponSoldBHTTotal")]
        public decimal CouponSoldBHTTotal
        {
            get { return _CouponSoldBHTTotal; }
            set { }
        }

        #endregion

        #region DC

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

        public class FKs : RevenueEntry
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

            #region PlazaGroup

            /// <summary>
            /// Gets or sets PlazaGroupNameEN.
            /// </summary>
            [MaxLength(100)]
            [PeropertyMapName("PlazaGroupNameEN")]
            public override string PlazaGroupNameEN
            {
                get { return base.PlazaGroupNameEN; }
                set { base.PlazaGroupNameEN = value; }
            }
            /// <summary>
            /// Gets or sets PlazaGroupNameTH.
            /// </summary>
            [MaxLength(100)]
            [PeropertyMapName("PlazaGroupNameTH")]
            public override string PlazaGroupNameTH
            {
                get { return base.PlazaGroupNameTH; }
                set { base.PlazaGroupNameTH = value; }
            }
            /// <summary>
            /// Gets or sets Direction.
            /// </summary>
            [MaxLength(10)]
            [PeropertyMapName("Direction")]
            public override string Direction
            {
                get { return base.Direction; }
                set { base.Direction = value; }
            }

            #endregion

            #region Shift

            /// <summary>
            /// Gets or sets Name TH.
            /// </summary>
            [MaxLength(50)]
            [PeropertyMapName("ShiftNameTH")]
            public override string ShiftNameTH
            {
                get { return base.ShiftNameTH; }
                set { base.ShiftNameTH = value; }
            }
            /// <summary>
            /// Gets or sets Name EN.
            /// </summary>
            [MaxLength(50)]
            [PeropertyMapName("ShiftNameEN")]
            public override string ShiftNameEN
            {
                get { return base.ShiftNameEN; }
                set { base.ShiftNameEN = value; }
            }

            #endregion

            #region User

            /// <summary>
            /// Gets or sets FullNameEN
            /// </summary>
            [MaxLength(100)]
            [PeropertyMapName("FullNameEN")]
            public override string FullNameEN
            {
                get { return base.FullNameEN; }
                set { base.FullNameEN = value; }
            }
            /// <summary>
            /// Gets or sets FullNameTH
            /// </summary>
            [MaxLength(100)]
            [PeropertyMapName("FullNameTH")]
            public override string FullNameTH
            {
                get { return base.FullNameTH; }
                set { base.FullNameTH = value; }
            }

            #endregion

            #region Supervisor

            /// <summary>
            /// Gets or sets SupervisorNameEN
            /// </summary>
            [MaxLength(100)]
            [PeropertyMapName("SupervisorNameEN")]
            public override string SupervisorNameEN
            {
                get { return base.SupervisorNameEN; }
                set { base.SupervisorNameEN = value; }
            }
            /// <summary>
            /// Gets or sets SupervisorNameTH
            /// </summary>
            [MaxLength(100)]
            [PeropertyMapName("SupervisorNameTH")]
            public override string SupervisorNameTH
            {
                get { return base.SupervisorNameTH; }
                set { base.SupervisorNameTH = value; }
            }

            #endregion

            #region Public Methods

            public RevenueEntry ToRevenueEntry()
            {
                RevenueEntry inst = new RevenueEntry();
                this.AssignTo(inst); // set all properties to new instance.
                return inst;
            }

            #endregion
        }

        #endregion

        #region Static Methods

        public static List<RevenueEntry> Gets()
        {
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT RevenueEntry.* ";
                cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
                cmd += "     , Shift.ShiftNameEN, Shift.ShiftNameTH ";
                cmd += "     , User.FullNameEN, User.FullNameTH ";
                cmd += "     , Sup.FullNameEN AS SupervisorNameEN ";
                cmd += "     , Sup.FullNameTH AS SupervisorNameTH ";
                cmd += "  FROM RevenueEntry, TSB, PlazaGroup, Shift, User, User as Sup ";
                cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
                cmd += "   AND RevenueEntry.TSBId = TSB.TSBId ";
                cmd += "   AND RevenueEntry.PlazaGroupId = PlazaGroup.PlazaGroupId ";
                cmd += "   AND RevenueEntry.UserId = User.UserId ";
                cmd += "   AND RevenueEntry.SupervisorId = Sup.UserId ";
                cmd += "   AND RevenueEntry.ShiftId = Shift.ShiftId ";

                var rets = NQuery.Query<FKs>(cmd).ToList();
                var results = new List<RevenueEntry>();
                if (null != rets)
                {
                    rets.ForEach(ret =>
                    {
                        results.Add(ret.ToRevenueEntry());
                    });
                }

                return results;
            }
        }

        public static List<RevenueEntry> FindByRevnueDate(DateTime begin, DateTime end)
        {
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT RevenueEntry.* ";
                cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
                cmd += "     , Shift.ShiftNameEN, Shift.ShiftNameTH ";
                cmd += "     , User.FullNameEN, User.FullNameTH ";
                cmd += "     , Sup.FullNameEN AS SupervisorNameEN ";
                cmd += "     , Sup.FullNameTH AS SupervisorNameTH ";
                cmd += "  FROM RevenueEntry, TSB, PlazaGroup, Shift, User, User as Sup ";
                cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
                cmd += "   AND RevenueEntry.TSBId = TSB.TSBId ";
                cmd += "   AND RevenueEntry.PlazaGroupId = PlazaGroup.PlazaGroupId ";
                cmd += "   AND RevenueEntry.UserId = User.UserId ";
                cmd += "   AND RevenueEntry.SupervisorId = Sup.UserId ";
                cmd += "   AND RevenueEntry.ShiftId = Shift.ShiftId ";
                cmd += "   AND RevenueEntry.RevenueDate >= ? ";
                cmd += "   AND RevenueEntry.RevenueDate <= ? ";

                var rets = NQuery.Query<FKs>(cmd, begin, end).ToList();
                var results = new List<RevenueEntry>();
                if (null != rets)
                {
                    rets.ForEach(ret =>
                    {
                        results.Add(ret.ToRevenueEntry());
                    });
                }

                return results;
            }
        }

        public static List<RevenueEntry> FindByRevnueDate(DateTime date)
        {
            DateTime begin = date.Date;
            DateTime end = date.Date.AddDays(1).AddMilliseconds(-1);
            return FindByRevnueDate(begin, end);
        }

        #endregion
    }

    #endregion
}
