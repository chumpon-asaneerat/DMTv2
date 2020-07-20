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
    #region UserCredit

    /// <summary>
    /// The UserCredit Data Model class.
    /// </summary>
    //[Table("UserCredit")]
    public class UserCreditTransaction : NTable<UserCreditTransaction>
    {
        #region Enum

        public enum TransactionTypes
        {
            Borrow = 1,
            Return = 2,
            Undo = 3
        }

        #endregion

        #region Internal Variables

        // For Runtime Used
        private string _description = string.Empty;
        private bool _hasRemark = false;

        private int _TransactionId = 0;
        private DateTime _TransactionDate = DateTime.MinValue;
        private TransactionTypes _TransactionType = TransactionTypes.Borrow;

        private int _RefId = 0; // for undo.

        private int _UserCreditId = 0;

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        private string _PlazaGroupId = string.Empty;
        private string _PlazaGroupNameEN = string.Empty;
        private string _PlazaGroupNameTH = string.Empty;
        private string _Direction = string.Empty;

        private string _UserId = string.Empty;
        private string _FullNameEN = string.Empty;
        private string _FullNameTH = string.Empty;

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
        public UserCreditTransaction() : base() { }

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

        #region Runtime

        /// <summary>
        /// Gets or sets has remark.
        /// </summary>
        [Category("Runtime")]
        [Description("Gets or sets HasRemark.")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("Description")]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    // Raise event.
                    this.RaiseChanged("Description");
                }
            }
        }
        /// <summary>
        /// Gets or sets has remark.
        /// </summary>
        [Category("Runtime")]
        [Description("Gets or sets HasRemark.")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("HasRemark")]
        public bool HasRemark
        {
            get { return _hasRemark; }
            set
            {
                if (_hasRemark != value)
                {
                    _hasRemark = value;
                    // Raise event.
                    this.RaiseChanged("HasRemark");
                    this.RaiseChanged("RemarkVisibility");
                }
            }
        }

        [Category("Runtime")]
        [Description("Gets or sets RemarkVisibility.")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("RemarkVisibility")]
        public System.Windows.Visibility RemarkVisibility
        {
            get { return (_hasRemark) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; }
            set { }
        }

        #endregion

        #region Common

        /// <summary>
        /// Gets or sets TransactionId
        /// </summary>
        [Category("Common")]
        [Description(" Gets or sets TransactionId")]
        [ReadOnly(true)]
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("TransactionId")]
        public int TransactionId
        {
            get
            {
                return _TransactionId;
            }
            set
            {
                if (_TransactionId != value)
                {
                    _TransactionId = value;
                    this.RaiseChanged("TransactionId");
                }
            }
        }
        /// <summary>
        /// Gets or sets Transaction Date.
        /// </summary>
        [Category("Common")]
        [Description(" Gets or sets Transaction Date")]
        [ReadOnly(true)]
        [PeropertyMapName("TransactionDate")]
        public DateTime TransactionDate
        {
            get
            {
                return _TransactionDate;
            }
            set
            {
                if (_TransactionDate != value)
                {
                    _TransactionDate = value;
                    this.RaiseChanged("TransactionDate");
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
        /// Gets Transaction Time String.
        /// </summary>
        [Category("Common")]
        [Description("Gets Transaction Time String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string TransactionTimeString
        {
            get
            {
                var ret = (this.TransactionDate == DateTime.MinValue) ? "" : this.TransactionDate.ToThaiTimeString();
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Transaction Date Time String.
        /// </summary>
        [Category("Common")]
        [Description("Gets Transaction Date Time String.")]
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
        /// <summary>
        /// Gets or sets Transaction Type.
        /// </summary>
        [Category("Common")]
        [Description("Gets or sets Transaction Type.")]
        [ReadOnly(true)]
        [PeropertyMapName("TransactionType")]
        public TransactionTypes TransactionType
        {
            get { return _TransactionType; }
            set
            {
                if (_TransactionType != value)
                {
                    _TransactionType = value;
                    this.RaiseChanged("TransactionType");
                }
            }
        }
        /// <summary>
        /// Gets or sets RefId
        /// </summary>
        [Category("Common")]
        [Description("Gets or sets RefId")]
        [ReadOnly(true)]
        [PeropertyMapName("RefId")]
        public int RefId
        {
            get
            {
                return _RefId;
            }
            set
            {
                if (_RefId != value)
                {
                    _RefId = value;
                    this.RaiseChanged("RefId");
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
        [Ignore]
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

        #region PlazaGroup

        /// <summary>
        /// Gets or sets PlazaGroupId.
        /// </summary>
        [Category("Plaza Group")]
        [Description("Gets or sets PlazaGroupId.")]
        [ReadOnly(true)]
        [Ignore]
        [MaxLength(10)]
        [PeropertyMapName("PlazaGroupId")]
        public virtual string PlazaGroupId
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

        #region User

        /// <summary>
        /// Gets or sets UserId
        /// </summary>
        [Category("User")]
        [Description("Gets or sets UserId")]
        [ReadOnly(true)]
        [Ignore]
        [MaxLength(10)]
        [PeropertyMapName("UserId")]
        public virtual string UserId
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
        [Description("Gets or sets FullNameEN")]
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
        [Description("Gets or sets FullNameTH")]
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

        #region UserCredit

        /// <summary>
        /// Gets or sets UserCreditId
        /// </summary>
        [Category("UserCredit")]
        [Description("Gets or sets UserCreditId")]
        [ReadOnly(true)]
        [PeropertyMapName("UserCreditId")]
        public int UserCreditId
        {
            get
            {
                return _UserCreditId;
            }
            set
            {
                if (_UserCreditId != value)
                {
                    _UserCreditId = value;
                    this.RaiseChanged("UserCreditId");
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
                    this.RaiseChanged("BHT1");
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

        public class FKs : UserCreditTransaction
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

            #region PlazaGroup

            /// <summary>
            /// Gets or sets PlazaGroupId.
            /// </summary>
            [MaxLength(10)]
            [PeropertyMapName("PlazaGroupId")]
            public override string PlazaGroupId
            {
                get { return base.PlazaGroupId; }
                set { base.PlazaGroupId = value; }
            }
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

            #region User

            /// <summary>
            /// Gets or sets UserId
            /// </summary>
            [MaxLength(10)]
            [PeropertyMapName("UserId")]
            public override string UserId
            {
                get { return base.UserId; }
                set { base.UserId = value; }
            }
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

            #region Public Methods

            public UserCreditTransaction ToUserCreditTransaction()
            {
                UserCreditTransaction inst = new UserCreditTransaction();
                this.AssignTo(inst); // set all properties to new instance.
                return inst;
            }

            #endregion
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets Active TSB User Credit transactions.
        /// </summary>
        /// <returns>Returns Current Active TSB User Credit transactions. If not found returns null.</returns>
        public static List<UserCreditTransaction> Gets()
        {
            lock (sync)
            {
                var tsb = TSB.GetCurrent();
                return Gets(tsb);
            }
        }
        /// <summary>
        /// Gets User Credit transactions.
        /// </summary>
        /// <param name="tsb">The target TSB to get transactions.</param>
        /// <returns>Returns User Credit transactions. If TSB not found returns null.</returns>
        public static List<UserCreditTransaction> Gets(TSB tsb)
        {
            if (null == tsb) return null;
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT UserCreditTransaction.* ";
                cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
                cmd += "     , User.UserId, User.FullNameEN, User.FullNameTH ";
                cmd += "     , UserCredit.UserCreditDate, ";
                cmd += "     , UserCredit.State, UserCredit.BagNo, UserCredit.BeltNo ";
                cmd += "  FROM UserCreditTransaction, TSB, PlazaGroup, User, UserCredit ";
                cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
                cmd += "   AND UserCredit.TSBId = TSB.TSBId ";
                cmd += "   AND UserCredit.PlazaGroupId = PlazaGroup.PlazaGroupId ";
                cmd += "   AND UserCredit.UserId = User.UserId ";
                cmd += "   AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId ";
                cmd += "   AND UserCredit.TSBId = ? ";

                var rets = NQuery.Query<FKs>(cmd, tsb.TSBId).ToList();
                if (null == rets)
                {
                    return new List<UserCreditTransaction>();
                }
                else
                {
                    var results = new List<UserCreditTransaction>();
                    rets.ForEach(ret =>
                    {
                        results.Add(ret.ToUserCreditTransaction());
                    });
                    return results;
                }
            }
        }

        /*
        public static UserCreditTransaction Create(User user, TSB tsb)
        {
            lock (sync)
            {
                if (null == user || null == tsb) return null;
                UserCreditTransaction inst = Create();

                inst.TSBId = tsb.TSBId;
                inst.TSBNameEN = tsb.TSBNameEN;
                inst.TSBNameTH = tsb.TSBNameTH;

                inst.UserId = user.UserId;
                inst.FullNameEN = user.FullNameEN;
                inst.FullNameTH = user.FullNameTH;

                return inst;
            }
        }

        public static UserCreditTransaction Create(User user)
        {
            lock (sync)
            {
                if (null == user) return null;

                TSB tsb = TSB.GetCurrent();
                if (null == tsb) return null; // active tsb not found.

                UserCreditTransaction inst = Create();

                inst.TSBId = tsb.TSBId;
                inst.TSBNameEN = tsb.TSBNameEN;
                inst.TSBNameTH = tsb.TSBNameTH;

                inst.UserId = user.UserId;
                inst.FullNameEN = user.FullNameEN;
                inst.FullNameTH = user.FullNameTH;

                return inst;
            }
        }

        public static void Borrow(UserCredit credit, TSBBalance balance)
        {
            lock (sync)
            {
                if (null == credit || null == balance) return;
                if (null == Default) return;
                int sign = -1;
                try
                {
                    Default.BeginTransaction();

                    credit.PKId = Guid.NewGuid(); // always create new.
                    credit.TransactionDate = DateTime.Now;
                    credit.TransactionType = UserCreditTransactionType.Borrow;

                    balance.ST25 += sign * credit.ST25;
                    balance.ST50 += sign * credit.ST50;
                    balance.BHT1 += sign * credit.BHT1;
                    balance.BHT2 += sign * credit.BHT2;
                    balance.BHT5 += sign * credit.BHT5;
                    balance.BHT10 += sign * credit.BHT10;
                    balance.BHT20 += sign * credit.BHT20;
                    balance.BHT50 += sign * credit.BHT50;
                    balance.BHT100 += sign * credit.BHT100;
                    balance.BHT500 += sign * credit.BHT500;
                    balance.BHT1000 += sign * credit.BHT1000;

                    balance.UserBHTTotal += -1 * sign * credit.BHTTotal;

                    Save(credit);
                    TSBBalance.Save(balance);

                    Default.Commit();
                }
                catch
                {
                    Default.Rollback();
                }
            }
        }

        public static void Return(UserCredit credit, TSBBalance balance)
        {
            lock (sync)
            {
                if (null == credit || null == balance) return;
                if (null == Default) return;
                int sign = 1;
                try
                {
                    Default.BeginTransaction();

                    credit.PKId = Guid.NewGuid(); // always create new.
                    credit.TransactionDate = DateTime.Now;
                    credit.TransactionType = UserCreditTransactionType.Return;

                    balance.ST25 += sign * credit.ST25;
                    balance.ST50 += sign * credit.ST50;
                    balance.BHT1 += sign * credit.BHT1;
                    balance.BHT2 += sign * credit.BHT2;
                    balance.BHT5 += sign * credit.BHT5;
                    balance.BHT10 += sign * credit.BHT10;
                    balance.BHT20 += sign * credit.BHT20;
                    balance.BHT50 += sign * credit.BHT50;
                    balance.BHT100 += sign * credit.BHT100;
                    balance.BHT500 += sign * credit.BHT500;
                    balance.BHT1000 += sign * credit.BHT1000;

                    balance.UserBHTTotal += -1 * sign * credit.BHTTotal;

                    Save(credit);
                    TSBBalance.Save(balance);

                    Default.Commit();
                }
                catch
                {
                    Default.Rollback();
                }
            }
        }

        public static void Undo(UserCredit credit, TSBBalance balance)
        {
            lock (sync)
            {
                if (null == credit || null == balance) return;
                if (null == Default) return;
                int sign = 0;
                if (credit.TransactionType == UserCreditTransactionType.Borrow)
                {
                    sign = 1;
                }
                else if (credit.TransactionType == UserCreditTransactionType.Return)
                {
                    sign = -1;
                }
                if (sign == 0) return; // not allow other type.
                try
                {
                    Default.BeginTransaction();

                    credit.RefId = credit.PKId; // set reference id.
                    credit.PKId = Guid.NewGuid(); // always create new.
                    credit.TransactionDate = DateTime.Now;
                    credit.TransactionType = UserCreditTransactionType.Undo;

                    balance.ST25 += sign * credit.ST25;
                    balance.ST50 += sign * credit.ST50;
                    balance.BHT1 += sign * credit.BHT1;
                    balance.BHT2 += sign * credit.BHT2;
                    balance.BHT5 += sign * credit.BHT5;
                    balance.BHT10 += sign * credit.BHT10;
                    balance.BHT20 += sign * credit.BHT20;
                    balance.BHT50 += sign * credit.BHT50;
                    balance.BHT100 += sign * credit.BHT100;
                    balance.BHT500 += sign * credit.BHT500;
                    balance.BHT1000 += sign * credit.BHT1000;

                    balance.UserBHTTotal += -1 * sign * credit.BHTTotal;

                    Save(credit);
                    TSBBalance.Save(balance);

                    Default.Commit();
                }
                catch
                {
                    Default.Rollback();
                }
            }
        }

        public static List<UserCredit> GetUserCredits(TSB tsb)
        {
            lock (sync)
            {
                if (null == tsb) return new List<UserCredit>();

                string cmd = string.Empty;
                cmd += "SELECT UserCredit.* ";
                cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                cmd += "  FROM UserCredit, TSB ";
                cmd += " WHERE UserCredit.TSBId = TSB.TSBId ";
                cmd += "   AND UserCredit.TSBId = ? ";

                var rets = NQuery.Query<FKs>(cmd, tsb.TSBId).ToList();
                var results = new List<UserCredit>();
                if (null != rets)
                {
                    rets.ForEach(ret =>
                    {
                        results.Add(ret.ToUserCredit());
                    });
                }
                return results;
            }
        }
        */

        #endregion
    }

    #endregion
}
