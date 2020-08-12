#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using NLib;
using NLib.Design;
using NLib.Reflection;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
// required for JsonIgnore attribute.
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

#endregion

namespace DMT.Models
{
    #region UserShift

    [TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    //[Table("UserShift")]
    public class UserShift : NTable<UserShift>
    {
        #region Intenral Variables

        private int _UserShiftId = 0;

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        private int _ShiftId = 0;
        private string _ShiftNameTH = string.Empty;
        private string _ShiftNameEN = string.Empty;

        private string _UserId = string.Empty;
        private string _FullNameEN = string.Empty;
        private string _FullNameTH = string.Empty;

        private DateTime _Begin = DateTime.MinValue;
        private DateTime _End = DateTime.MinValue;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserShift() : base() { }

        #endregion

        #region Public Properties

        #region Common

        /// <summary>
        /// Gets or sets PK Id.
        /// </summary>
        [Category("Common")]
        [Description("Gets or sets PK Id.")]
        [ReadOnly(true)]
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("UserShiftId")]
        public int UserShiftId
        {
            get
            {
                return _UserShiftId;
            }
            set
            {
                if (_UserShiftId != value)
                {
                    _UserShiftId = value;
                    this.RaiseChanged("UserShiftId");
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

        #region Begin/End

        /// <summary>
        /// Gets or sets Begin Date.
        /// </summary>
        [Category("Shift")]
        [Description("Gets or sets Begin Date.")]
        //[ReadOnly(true)]
        [PeropertyMapName("Begin")]
        public DateTime Begin
        {
            get { return _Begin; }
            set
            {
                if (_Begin != value)
                {
                    _Begin = value;
                    // Raise event.
                    RaiseChanged("Begin");
                    RaiseChanged("BeginDateString");
                    RaiseChanged("BeginTimeString");
                    RaiseChanged("BeginDateTimeString");
                }
            }
        }
        /// <summary>
        /// Gets or sets End Date.
        /// </summary>
        [Category("Shift")]
        [Description("Gets or sets End Date.")]
        //[ReadOnly(true)]
        [PeropertyMapName("End")]
        public DateTime End
        {
            get { return _End; }
            set
            {
                if (_End != value)
                {
                    _End = value;
                    // Raise event.
                    RaiseChanged("End");
                    RaiseChanged("EndDateString");
                    RaiseChanged("EndTimeString");
                    RaiseChanged("EndDateTimeString");
                }
            }
        }
        /// <summary>
        /// Gets Begin Date String.
        /// </summary>
        [Category("Shift")]
        [Description("Gets Begin Date String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string BeginDateString
        {
            get
            {
                var ret = (this.Begin == DateTime.MinValue) ? "" : this.Begin.ToThaiDateTimeString("dd/MM/yyyy");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets End Date String.
        /// </summary>
        [Category("Shift")]
        [Description("Gets End Date String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string EndDateString
        {
            get
            {
                var ret = (this.End == DateTime.MinValue) ? "" : this.End.ToThaiDateTimeString("dd/MM/yyyy");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Begin Time String.
        /// </summary>
        [Category("Shift")]
        [Description("Gets Begin Time String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string BeginTimeString
        {
            get
            {
                var ret = (this.Begin == DateTime.MinValue) ? "" : this.Begin.ToThaiTimeString();
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets End Time String.
        /// </summary>
        [Category("Shift")]
        [Description("Gets End Time String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string EndTimeString
        {
            get
            {
                var ret = (this.End == DateTime.MinValue) ? "" : this.End.ToThaiTimeString();
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets or sets Begin Date Time String..
        /// </summary>
        [Category("Shift")]
        [Description("Gets or sets Begin Date Time String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string BeginDateTimeString
        {
            get
            {
                var ret = (this.Begin == DateTime.MinValue) ? "" : this.Begin.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets or sets End Date Time String.
        /// </summary>
        [Category("Shift")]
        [Description("Gets or sets End Date Time String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string EndDateTimeString
        {
            get
            {
                var ret = (this.End == DateTime.MinValue) ? "" : this.End.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
                return ret;
            }
            set { }
        }

        #endregion

        #region Status (DC)

        /// <summary>
        /// Gets or sets Status (1 = Sync, 0 = Unsync, etc..)
        /// </summary>
        /// 
        [Category("DataCenter")]
        [Description("Gets or sets Status (1 = Sync, 0 = Unsync, etc..)")]
        [ReadOnly(true)]
        [PeropertyMapName("Status")]
        [PropertyOrder(10001)]
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
        [PropertyOrder(10002)]
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

        public class FKs : UserShift
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

            #region Public Methods

            public UserShift ToUserShift()
            {
                UserShift inst = new UserShift();
                this.AssignTo(inst); // set all properties to new instance.
                return inst;
            }

            #endregion
        }

        #endregion

        #region Static Methods

        public static NDbResult<UserShift> Create(Shift shift, User supervisor)
        {
            var result = new NDbResult<UserShift>();
            UserShift inst = Create();

            var tsbRet = TSB.GetCurrent();
            if (tsbRet.errors.hasError)
            {
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                var tsb = tsbRet.data;
                if (null != tsb) tsb.AssignTo(inst);
                if (null != shift) shift.AssignTo(inst);
                if (null != supervisor) supervisor.AssignTo(inst);
                result.data = inst;
                result.Success();
            }

            return result;
        }

        public static NDbResult<UserShift> GetCurrent(string userId)
        {
            var result = new NDbResult<UserShift>();
            SQLiteConnection db = Default;
            if (null == db)
            {
                result.ConenctFailed();
                result.data = null;
                return result;
            }

            lock (sync)
            {
                try
                {
                    string cmd = string.Empty;
                    cmd += "SELECT UserShift.* ";
                    cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                    cmd += "     , Shift.ShiftNameEN, Shift.ShiftNameTH ";
                    cmd += "     , User.FullNameEN, User.FullNameTH ";
                    cmd += "  FROM UserShift, Shift, User, TSB ";
                    cmd += " WHERE UserShift.ShiftId = Shift.ShiftId ";
                    cmd += "   AND TSB.Active = 1 ";
                    cmd += "   AND UserShift.UserId = User.UserId ";
                    cmd += "   AND UserShift.TSBId = TSB.TSBId ";
                    cmd += "   AND UserShift.UserId = ? ";
                    cmd += "   AND UserShift.End = ? ";
                    var ret = NQuery.Query<FKs>(cmd, userId,
                        DateTime.MinValue).FirstOrDefault();
                    result.data = (null != ret) ? ret.ToUserShift() : null;
                    result.Success();
                }
                catch (Exception ex)
                {
                    result.Error(ex);
                    result.data = null;
                }
                return result;
            }
        }

        public static NDbResult BeginUserShift(UserShift value)
        {
            var result = new NDbResult();
            SQLiteConnection db = Default;
            if (null == db)
            {
                result.ConenctFailed();
                return result;
            }

            if (null == value)
            {
                result.ParameterIsNull();
                return result;
            }

            lock (sync)
            {
                try
                {
                    var last = GetCurrent(value.UserId);
                    if (null != last)
                    {
                        // not enter revenue entry.
                        result.Error(new Exception("ยังมีกะที่ยังไม่ได้ป้อนรายได้"));
                        result.errors.errNum = -10;
                        return result;
                    }
                    // Begin new shift.
                    if (value.Begin == DateTime.MinValue)
                        value.Begin = DateTime.Now;

                    var saveRet = Save(value);

                    result.errors = saveRet.errors;
                    if (!result.errors.hasError)
                    {
                        result.Success();
                    }
                }
                catch (Exception ex)
                {
                    result.Error(ex);
                }
                return result;
            }
        }

        public static NDbResult EndUserShift(UserShift value)
        {
            var result = new NDbResult();
            SQLiteConnection db = Default;
            if (null == db)
            {
                result.ConenctFailed();
                return result;
            }

            if (null == value)
            {
                result.ParameterIsNull();
                return result;
            }

            lock (sync)
            {
                try
                {
                    // End shift.
                    if (value.End == DateTime.MinValue)
                        value.End = DateTime.Now;

                    var saveRet = Save(value);

                    result.errors = saveRet.errors;
                    if (!result.errors.hasError)
                    {
                        result.Success();
                    }
                }
                catch (Exception ex)
                {
                    result.Error(ex);
                }
                return result;
            }
        }

        public static NDbResult<List<UserShift>> GetUserShifts(string userId)
        {
            var result = new NDbResult<List<UserShift>>();
            SQLiteConnection db = Default;
            if (null == db)
            {
                result.ConenctFailed();
                result.data = new List<UserShift>();
                return result;
            }

            lock (sync)
            {
                try
                {
                    string cmd = string.Empty;
                    cmd += "SELECT UserShift.* ";
                    cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                    cmd += "     , Shift.ShiftNameEN, Shift.ShiftNameTH ";
                    cmd += "     , User.FullNameEN, User.FullNameTH ";
                    cmd += "  FROM UserShift, Shift, User, TSB ";
                    cmd += " WHERE UserShift.ShiftId = Shift.ShiftId ";
                    cmd += "   AND TSB.Active = 1 ";
                    cmd += "   AND UserShift.UserId = User.UserId ";
                    cmd += "   AND UserShift.TSBId = TSB.TSBId ";
                    cmd += "   AND UserShift.UserId = ? ";

                    var rets = NQuery.Query<FKs>(cmd, userId).ToList();
                    var results = new List<UserShift>();
                    if (null != rets)
                    {
                        rets.ForEach(ret =>
                        {
                            results.Add(ret.ToUserShift());
                        });
                    }

                    result.data = results;
                    result.Success();
                }
                catch (Exception ex)
                {
                    result.Error(ex);
                    result.data = new List<UserShift>();
                }
                return result;
            }
        }

        public static NDbResult<List<UserShift>> GetUnCloseUserShifts()
        {
            var result = new NDbResult<List<UserShift>>();
            SQLiteConnection db = Default;
            if (null == db)
            {
                result.ConenctFailed();
                result.data = new List<UserShift>();
                return result;
            }

            lock (sync)
            {
                try
                {
                    string cmd = string.Empty;
                    cmd += "SELECT UserShift.* ";
                    cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                    cmd += "     , Shift.ShiftNameEN, Shift.ShiftNameTH ";
                    cmd += "     , User.FullNameEN, User.FullNameTH ";
                    cmd += "  FROM UserShift, Shift, User, TSB ";
                    cmd += " WHERE UserShift.ShiftId = Shift.ShiftId ";
                    cmd += "   AND TSB.Active = 1 ";
                    cmd += "   AND UserShift.UserId = User.UserId ";
                    cmd += "   AND UserShift.TSBId = TSB.TSBId ";
                    cmd += "   AND UserShift.End = ? ";

                    var rets = NQuery.Query<FKs>(cmd, DateTime.MinValue).ToList();
                    var results = new List<UserShift>();
                    if (null != rets)
                    {
                        rets.ForEach(ret =>
                        {
                            results.Add(ret.ToUserShift());
                        });
                    }

                    result.data = results;
                    result.Success();
                }
                catch (Exception ex)
                {
                    result.Error(ex);
                    result.data = new List<UserShift>();
                }
                return result;
            }
        }

        #endregion
    }

    #endregion

    #region UserShiftCreate

    public class UserShiftCreate
    {
        public Shift Shift { get; set; }
        public User User { get; set; }
    }

    #endregion
}
