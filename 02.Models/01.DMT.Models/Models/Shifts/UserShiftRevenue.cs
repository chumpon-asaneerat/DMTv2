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
using System.Reflection;

#endregion

namespace DMT.Models
{
    [TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    //[Table("UserShiftRevenue")]
    public class UserShiftRevenue : NTable<UserShiftRevenue>
    {
        #region Internal Variables

        private Guid _PKId = Guid.NewGuid();

        private int _UserShiftId = 0;

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        private string _PlazaGroupId = string.Empty;
        private string _PlazaGroupNameEN = string.Empty;
        private string _PlazaGroupNameTH = string.Empty;
        private string _Direction = string.Empty;

        private int _ShiftId = 0;
        private string _ShiftNameEN = string.Empty;
        private string _ShiftNameTH = string.Empty;

        private string _UserId = string.Empty;
        private string _FullNameEN = string.Empty;
        private string _FullNameTH = string.Empty;

        private string _RevenueId = string.Empty;
        private DateTime _RevenueDate = DateTime.MinValue;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        public UserShiftRevenue() : base()
        { 
        }

        #endregion

        #region Public Properties

        #region Common

        /// <summary>
        /// Gets or sets PKId
        /// </summary>
        [Category("Common")]
        [Description("Gets or sets PK Id.")]
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

        #endregion

        #region UserShift

        /// <summary>
        /// Gets or sets User Shift Id.
        /// </summary>
        [Category("Shift")]
        [Description("Gets or sets User Shift Id.")]
        [ReadOnly(true)]
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
        /// Gets or sets TSB Name EN.
        /// </summary>
        [Category("TSB")]
        [Description("Gets or sets TSB Name EN.")]
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
        /// Gets or sets TSB Name TH.
        /// </summary>
        [Category("TSB")]
        [Description("Gets or sets TSB Name TH.")]
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
        /// Gets or sets Plaza Group Id.
        /// </summary>
        [Category("Plaza Group")]
        [Description("Gets or sets Plaza Group Id.")]
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
        /// Gets or sets Plaza Group Name EN.
        /// </summary>
        [Category("Plaza Group")]
        [Description("Gets or sets Plaza Group Name EN.")]
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
        /// Gets or sets Plaza Group Name TH.
        /// </summary>
        [Category("Plaza Group")]
        [Description("Gets or sets Plaza Group Name TH.")]
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
        /// Gets or sets User Id
        /// </summary>
        [Category("User")]
        [Description("Gets or sets User Id.")]
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
        /// Gets or sets User Full Name EN
        /// </summary>
        [Category("User")]
        [Description("Gets or sets User Full Name EN.")]
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
        /// Gets or sets User Full Name TH
        /// </summary>
        [Category("User")]
        [Description("Gets or sets User Full Name TH.")]
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

        #region Shift

        /// <summary>
        /// Gets or sets Shift Id.
        /// </summary>
        [Category("Shift")]
        [Description("Gets or sets Shift Id.")]
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
        /// Gets or sets Shift Name EN.
        /// </summary>
        [Category("Shift")]
        [Description("Gets or sets Shift Name EN.")]
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
        /// <summary>
        /// Gets or sets Shift Name TH.
        /// </summary>
        [Category("Shift")]
        [Description("Gets or sets Shift Name TH.")]
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

        #endregion

        #region Revenue

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
                    RaiseChanged("RevenueDate");
                    RaiseChanged("RevenueDateString");
                    RaiseChanged("RevenueTimeString");
                    RaiseChanged("RevenueDateTimeString");
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
                var ret = (this.RevenueDate == DateTime.MinValue) ? "" : this.RevenueDate.ToThaiDateTimeString("dd/MM/yyyy");
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Revenue Time String.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets Revenue Time String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string RevenueTimeString
        {
            get
            {
                var ret = (this.RevenueDate == DateTime.MinValue) ? "" : this.RevenueDate.ToThaiTimeString();
                return ret;
            }
            set { }
        }
        /// <summary>
        /// Gets Revenue Date Time String.
        /// </summary>
        [Category("Revenue")]
        [Description("Gets Revenue Date Time String.")]
        [ReadOnly(true)]
        [JsonIgnore]
        [Ignore]
        public string RevenueDateTimeString
        {
            get
            {
                var ret = (this.RevenueDate == DateTime.MinValue) ? "" : this.RevenueDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
                return ret;
            }
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

        public class FKs : UserShiftRevenue, IFKs<UserShiftRevenue>
        {
            #region TSB

            /// <summary>
            /// Gets or sets TSB Name EN.
            /// </summary>
            [MaxLength(100)]
            [PeropertyMapName("TSBNameEN")]
            public override string TSBNameEN
            {
                get { return base.TSBNameEN; }
                set { base.TSBNameEN = value; }
            }
            /// <summary>
            /// Gets or sets TSB Name TH.
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
            /// Gets or sets Plaza Group Name EN.
            /// </summary>
            [MaxLength(100)]
            [PeropertyMapName("PlazaGroupNameEN")]
            public override string PlazaGroupNameEN
            {
                get { return base.PlazaGroupNameEN; }
                set { base.PlazaGroupNameEN = value; }
            }
            /// <summary>
            /// Gets or sets Plaza Group Name TH.
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
            /// Gets or sets Shift Name EN.
            /// </summary>
            [MaxLength(50)]
            [PeropertyMapName("ShiftNameEN")]
            public override string ShiftNameEN
            {
                get { return base.ShiftNameEN; }
                set { base.ShiftNameEN = value; }
            }
            /// <summary>
            /// Gets or sets Shift Name TH.
            /// </summary>
            [MaxLength(50)]
            [PeropertyMapName("ShiftNameTH")]
            public override string ShiftNameTH
            {
                get { return base.ShiftNameTH; }
                set { base.ShiftNameTH = value; }
            }

            #endregion

            #region User

            /// <summary>
            /// Gets or sets Full Name EN.
            /// </summary>
            [MaxLength(150)]
            [PeropertyMapName("FullNameEN")]
            public override string FullNameEN
            {
                get { return base.FullNameEN; }
                set { base.FullNameEN = value; }
            }
            /// <summary>
            /// Gets or sets Full Name TH.
            /// </summary>
            [MaxLength(150)]
            [PeropertyMapName("FullNameTH")]
            public override string FullNameTH
            {
                get { return base.FullNameTH; }
                set { base.FullNameTH = value; }
            }

            #endregion

            #region Public Methods
            /*
            public UserShiftRevenue ToUserShiftRevenue()
            {
                UserShiftRevenue inst = new UserShiftRevenue();
                this.AssignTo(inst); // set all properties to new instance.
                return inst;
            }
            */
            #endregion
        }

        #endregion

        #region Static Methods

        public static NDbResult<UserShiftRevenue> CreatePlazaRevenue(
            UserShift shift, PlazaGroup plazaGroup)
        {
            var result = new NDbResult<UserShiftRevenue>();

            if (null == shift || null == plazaGroup)
            {
                result.ParameterIsNull();
                result.data = null;
            }
            else
            {
                UserShiftRevenue inst = Create();
                plazaGroup.AssignTo(inst);
                shift.AssignTo(inst);
                result.data = inst;
                result.Success();
            }
            return result;
        }

        public static NDbResult<UserShiftRevenue> SavePlazaRevenue(UserShiftRevenue value,
            DateTime revenueDate, string revenueId)
        {
            var result = new NDbResult<UserShiftRevenue>();
            SQLiteConnection db = Default;
            if (null == db)
            {
                result.DbConenctFailed();
                return result;
            }

            if (null == value)
            {
                result.ParameterIsNull();
                return result;
            }
            lock (sync)
            {
                MethodBase med = MethodBase.GetCurrentMethod();
                try
                {
                    value.RevenueDate = revenueDate;
                    value.RevenueId = revenueId;
                    // save.
                    result = Save(value);
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    result.Error(ex);
                }
                return result;
            }
        }

        public static NDbResult<UserShiftRevenue> GetPlazaRevenue(
            UserShift shift, PlazaGroup plazaGroup)
        {
            var result = new NDbResult<UserShiftRevenue>();
            SQLiteConnection db = Default;
            if (null == db)
            {
                result.DbConenctFailed();
                return result;
            }

            if (null == shift || null == plazaGroup)
            {
                result.ParameterIsNull();
                return result;
            }
            lock (sync)
            {
                MethodBase med = MethodBase.GetCurrentMethod();
                try
                {
                    string cmd = string.Empty;
                    cmd += "SELECT UserShiftRevenue.* ";
                    cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                    cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
                    cmd += "     , Shift.ShiftNameEN, Shift.ShiftNameTH ";
                    cmd += "     , User.FullNameEN, User.FullNameTH ";
                    cmd += "  FROM UserShiftRevenue, TSB, PlazaGroup, Shift, User, UserShift ";
                    cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
                    cmd += "   AND UserShift.ShiftId = Shift.ShiftId ";
                    cmd += "   AND UserShift.UserId = User.UserId ";
                    cmd += "   AND UserShiftRevenue.TSBId = TSB.TSBId ";
                    cmd += "   AND UserShiftRevenue.PlazaGroupId = PlazaGroup.PlazaGroupId ";
                    cmd += "   AND UserShiftRevenue.ShiftId = Shift.ShiftId ";
                    cmd += "   AND UserShiftRevenue.UserId = User.UserId ";
                    cmd += "   AND UserShiftRevenue.UserShiftId = ? ";
                    cmd += "   AND UserShiftRevenue.PlazaGroupId = ? ";
                    var ret = NQuery.Query<FKs>(cmd, shift.UserShiftId,
                        plazaGroup.PlazaGroupId).FirstOrDefault();
                    result.data = (null != ret) ? ret.ToModel() : null;
                    result.Success();
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    result.Error(ex);
                }
                return result;
            }
        }

        #endregion
    }
}
