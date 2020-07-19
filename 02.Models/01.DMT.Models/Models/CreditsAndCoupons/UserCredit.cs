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
    public class UserCredit : NTable<UserCredit>
    {
        #region Enum

        public enum StateTypes : int
        {
            Initial = 0,
            // received bag
            Received = 1,
            // Returns all credit.
            Completed = 2
        }

        #endregion

        #region Internal Variables

        private int _UserCreditId = 0;
        private DateTime _UserCreditDate = DateTime.MinValue;
        private StateTypes _State = StateTypes.Initial;
        private string _BagNo = string.Empty;
        private string _BeltNo = string.Empty;

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

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserCredit() : base() { }

        #endregion

        #region Public Properties

        #region Common

        /// <summary>
        /// Gets or sets UserCreditId
        /// </summary>
        [Category("Common")]
        [Description("Gets or sets UserCreditId")]
        [PrimaryKey, AutoIncrement]
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
        /// <summary>
        /// Gets or sets UserCredit Date.
        /// </summary>
        [Category("Common")]
        [Description("Gets or sets UserCredit Date.")]
        [ReadOnly(true)]
        [PeropertyMapName("UserCreditDate")]
        public DateTime UserCreditDate
        {
            get { return _UserCreditDate; }
            set
            {
                if (_UserCreditDate != value)
                {
                    _UserCreditDate = value;
                    // Raise event.
                    this.RaiseChanged("UserCreditDate");
                    this.RaiseChanged("UserCreditDateString");
                    this.RaiseChanged("UserCreditDateTimeString");
                }
            }
        }
        /// <summary>
        /// Gets UserCredit Date String.
        /// </summary>
        [Category("Common")]
        [Description("Gets UserCredit Date String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string UserCreditDateString
        {
            get
            {
                var ret = (this.UserCreditDate == DateTime.MinValue) ? "" : this.UserCreditDate.ToThaiDateTimeString("dd/MM/yyyy");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets UserCredit DateTime String.
        /// </summary>
        [Category("Common")]
        [Description("Gets UserCredit DateTime String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string UserCreditDateTimeString
        {
            get
            {
                var ret = (this.UserCreditDate == DateTime.MinValue) ? "" : this.UserCreditDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets or sets State.
        /// </summary>
        [Category("Common")]
        [Description("Gets or sets State.")]
        [Browsable(false)]
        [PeropertyMapName("State")]
        public StateTypes State
        {
            get { return _State; }
            set
            {
                if (_State != value)
                {
                    _State = value;
                    // Raise event.
                    this.RaiseChanged("State");
                }
            }
        }
        /// <summary>
        /// Gets or sets Bag Number.
        /// </summary>
        [Category("Common")]
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
        [Category("Common")]
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

        #region User

        /// <summary>
        /// Gets or sets UserId
        /// </summary>
        [Category("User")]
        [Description("Gets or sets UserId")]
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

        public static UserCredit Create(User user, PlazaGroup plazaGroup, 
            string bagNo, string beltNo)
        {
            lock (sync)
            {
                if (null == user || null == plazaGroup) return null;
                UserCredit inst = Create();

                inst.TSBId = plazaGroup.TSBId;
                inst.TSBNameEN = plazaGroup.TSBNameEN;
                inst.TSBNameTH = plazaGroup.TSBNameTH;

                inst.PlazaGroupId = plazaGroup.PlazaGroupId;
                inst.PlazaGroupNameEN = plazaGroup.PlazaGroupNameEN;
                inst.PlazaGroupNameTH = plazaGroup.PlazaGroupNameTH;
                inst.Direction = plazaGroup.Direction;

                inst.UserId = user.UserId;
                inst.FullNameEN = user.FullNameEN;
                inst.FullNameTH = user.FullNameTH;

                inst.BagNo = bagNo;
                inst.BeltNo = beltNo;

                return inst;
            }
        }

        public static void SaveCredit(UserCredit value)
        {
            lock (sync)
            {
                if (null == value) return;
                // set date if not assigned.
                if (value.UserCreditDate == DateTime.MinValue)
                {
                    value.UserCreditDate = DateTime.Now;
                }
                Save(value);
            }
        }

        /*
        public static void Borrow(UserCredit credit, TSBCreditBalance balance)
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
                    TSBCreditBalance.Save(balance);

                    Default.Commit();
                }
                catch
                {
                    Default.Rollback();
                }
            }
        }

        public static void Return(UserCredit credit, TSBCreditBalance balance)
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
                    TSBCreditBalance.Save(balance);

                    Default.Commit();
                }
                catch
                {
                    Default.Rollback();
                }
            }
        }

        public static void Undo(UserCredit credit, TSBCreditBalance balance)
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
                    TSBCreditBalance.Save(balance);

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
