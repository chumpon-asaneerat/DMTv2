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
using Microsoft.Win32;
using System.ComponentModel;

#endregion

namespace DMT.Models
{
    //[Table("UserShiftRevenue")]
    public class UserShiftRevenue : NTable<UserShiftRevenue>
    {
        #region Internal Variables

        private Guid _PKId = Guid.NewGuid();

        private int _UserShiftId = 0;

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        private string _PlazaId = string.Empty;
        private string _PlazaNameEN = string.Empty;
        private string _PlazaNameTH = string.Empty;

        private int _ShiftId = 0;
        private string _ShiftNameTH = string.Empty;
        private string _ShiftNameEN = string.Empty;

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
        /// Gets or sets UserShift Id.
        /// </summary>
        [Category("Shift")]
        [Description("Gets or sets UserShift Id.")]
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

        #region Plaza

        /// <summary>
        /// Gets or sets PlazaId.
        /// </summary>
        [Category("Plaza")]
        [Description("Gets or sets PlazaId.")]
        [ReadOnly(true)]
        [MaxLength(10)]
        [PeropertyMapName("PlazaId")]
        public string PlazaId
        {
            get
            {
                return _PlazaId;
            }
            set
            {
                if (_PlazaId != value)
                {
                    _PlazaId = value;
                    this.RaiseChanged("PlazaId");
                }
            }
        }
        /// <summary>
        /// Gets or sets PlazaNameEN
        /// </summary>
        [Category("Plaza")]
        [Description("Gets or sets PlazaNameEN")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("PlazaNameEN")]
        public virtual string PlazaNameEN
        {
            get
            {
                return _PlazaNameEN;
            }
            set
            {
                if (_PlazaNameEN != value)
                {
                    _PlazaNameEN = value;
                    this.RaiseChanged("PlazaNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets PlazaNameTH
        /// </summary>
        [Category("Plaza")]
        [Description("Gets or sets PlazaNameTH")]
        [ReadOnly(true)]
        [Ignore]
        [PeropertyMapName("PlazaNameTH")]
        public virtual string PlazaNameTH
        {
            get
            {
                return _PlazaNameTH;
            }
            set
            {
                if (_PlazaNameTH != value)
                {
                    _PlazaNameTH = value;
                    this.RaiseChanged("PlazaNameTH");
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

        public class FKs : UserShiftRevenue
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

            #region Plaza

            /// <summary>
            /// Gets or sets PlazaNameEN
            /// </summary>
            [MaxLength(100)]
            [PeropertyMapName("PlazaNameEN")]
            public override string PlazaNameEN
            {
                get { return base.PlazaNameEN; }
                set { base.PlazaNameEN = value; }
            }
            /// <summary>
            /// Gets or sets PlazaNameTH
            /// </summary>
            [MaxLength(100)]
            [PeropertyMapName("PlazaNameTH")]
            public override string PlazaNameTH
            {
                get { return base.PlazaNameTH; }
                set { base.PlazaNameTH = value; }
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

            public UserShiftRevenue ToUserShiftRevenue()
            {
                UserShiftRevenue inst = new UserShiftRevenue();
                this.AssignTo(inst); // set all properties to new instance.
                return inst;
            }

            #endregion
        }

        #endregion

        #region Static Methods

        public static UserShiftRevenue CreatePlazaRevenue(UserShift shift, Plaza plaza)
        {
            if (null == shift || null == plaza) return null;
            UserShiftRevenue inst = new UserShiftRevenue();
            plaza.AssignTo(inst);
            shift.AssignTo(inst);
            return inst;
        }

        public static void SavePlazaRevenue(UserShiftRevenue value,
            DateTime revenueDate, string revenueId)
        {
            lock (sync)
            {
                if (null == value) return;
                value.RevenueDate = revenueDate;
                value.RevenueId = revenueId;
                // save.
                Save(value);
            }
        }

        public static UserShiftRevenue GetPlazaRevenue(UserShift shift, Plaza plaza)
        {
            lock (sync)
            {
                if (null == shift || null == plaza) return null;
                string cmd = string.Empty;
                cmd += "SELECT UserShiftRevenue.* ";
                cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                cmd += "     , Shift.ShiftNameEN, Shift.ShiftNameTH ";
                cmd += "     , User.FullNameEN, User.FullNameTH ";
                cmd += "  FROM UserShiftRevenue, TSB, Plaza, Shift, User, UserShift ";
                cmd += " WHERE Plaza.TSBId = TSB.TSBId ";
                cmd += "   AND UserShift.ShiftId = Shift.ShiftId ";
                cmd += "   AND UserShiftRevenue.TSBId = TSB.TSBId ";
                cmd += "   AND UserShiftRevenue.PlazaId = Plaza.PlazaId ";
                cmd += "   AND UserShiftRevenue.ShiftId = Shift.ShiftId ";
                cmd += "   AND UserShiftRevenue.UserId = User.UserId ";
                cmd += "   AND UserShiftRevenue.UserShiftId = ? ";
                cmd += "   AND UserShiftRevenue.PlazaId = ? ";
                var ret = NQuery.Query<FKs>(cmd, shift.UserShiftId,
                    plaza.PlazaId).FirstOrDefault();
                return (null != ret) ? ret.ToUserShiftRevenue() : null;
            }
        }

        #endregion
    }
}
