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
    public enum UserCreditTransactionType
    {
        Borrow = 1,
        Return = 2,
        Undo = 3
    }

    #region UserCredit

    /// <summary>
    /// The UserCredit Data Model class.
    /// </summary>
    //[Table("UserCredit")]
    public class UserCredit : NTable<UserCredit>
    {
        #region Internal Variables

        private Guid _PKId = Guid.NewGuid();
        private DateTime _TransactionDate = DateTime.MinValue;
        private UserCreditTransactionType _TransactionType = UserCreditTransactionType.Borrow;

        private Guid _RefId = Guid.Empty; // for undo.

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

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
        public UserCredit() : base() { }

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
        /// Gets or sets PKId
        /// </summary>
        [Category("Common")]
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
        [PeropertyMapName("TransactionType")]
        public UserCreditTransactionType TransactionType
        {
            get { return _TransactionType; }
            set
            {
                if (_TransactionType != value)
                {
                    _TransactionType = value;
                    // Raise event.
                    this.RaiseChanged("TransactionType");
                }
            }
        }
        /// <summary>
        /// Gets or sets RefId
        /// </summary>
        [Category("Common")]
        [PeropertyMapName("RefId")]
        public Guid RefId
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

        #region User

        /// <summary>
        /// Gets or sets UserId
        /// </summary>
        [Category("User")]
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

        #region Coin/Bill

        /// <summary>
        /// Gets or sets number of .25 baht coin.
        /// </summary>
        [Category("Coin/Bill")]
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

        public class FKs : UserCredit
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

            #region Public Methods

            public UserCredit ToUserCredit()
            {
                UserCredit inst = new UserCredit();
                this.AssignTo(inst); // set all properties to new instance.
                return inst;
            }

            #endregion
        }

        #endregion

        #region Static Methods

        public static UserCredit Create(User user, TSB tsb)
        {
            lock (sync)
            {
                if (null == user || null == tsb) return null;
                UserCredit inst = Create();

                inst.TSBId = tsb.TSBId;
                inst.TSBNameEN = tsb.TSBNameEN;
                inst.TSBNameTH = tsb.TSBNameTH;

                inst.UserId = user.UserId;
                inst.FullNameEN = user.FullNameEN;
                inst.FullNameTH = user.FullNameTH;

                return inst;
            }
        }

        public static UserCredit Create(User user)
        {
            lock (sync)
            {
                if (null == user) return null;

                TSB tsb = TSB.GetCurrent();
                if (null == tsb) return null; // active tsb not found.

                UserCredit inst = Create();

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

        #endregion
    }

    #endregion
}
