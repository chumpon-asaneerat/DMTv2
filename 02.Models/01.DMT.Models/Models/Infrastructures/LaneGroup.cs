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
    #region LaneGroup

    /// <summary>
    /// The LaneGroup Data Model class.
    /// </summary>
    //[Table("LaneGroup")]
    public class LaneGroup : NTable<LaneGroup>
    {
        #region Internal Variable

        private int _GroupPkId = 0;
        private string _GroupNameEN = string.Empty;
        private string _GroupNameTH = string.Empty;

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        private string _PlazaId = string.Empty;
        private string _PlazaNameEN = string.Empty;
        private string _PlazaNameTH = string.Empty;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LaneGroup() : base() { }

        #endregion

        #region Public Properties

        #region Common

        /// <summary>
        /// Gets or sets Group Pk Id
        /// </summary>
        [Category("Group")]
        [Description("Gets or sets Group Pk Id")]
        [ReadOnly(true)]
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("GroupPkId")]
        public int GroupPkId
        {
            get
            {
                return _GroupPkId;
            }
            set
            {
                if (_GroupPkId != value)
                {
                    _GroupPkId = value;
                    this.RaiseChanged("GroupPkId");
                }
            }
        }
        /// <summary>
        /// Gets or sets GroupNameEN
        /// </summary>
        [Category("Group")]
        [Description("Gets or sets GroupNameEN")]
        [MaxLength(100)]
        [PeropertyMapName("GroupNameEN")]
        public string GroupNameEN
        {
            get
            {
                return _GroupNameEN;
            }
            set
            {
                if (_GroupNameEN != value)
                {
                    _GroupNameEN = value;
                    this.RaiseChanged("GroupNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets GroupNameTH
        /// </summary>
        [Category("Group")]
        [Description("Gets or sets GroupNameTH")]
        [MaxLength(100)]
        [PeropertyMapName("GroupNameTH")]
        public string GroupNameTH
        {
            get
            {
                return _GroupNameTH;
            }
            set
            {
                if (_GroupNameTH != value)
                {
                    _GroupNameTH = value;
                    this.RaiseChanged("GroupNameTH");
                }
            }
        }

        #endregion

        #region TSB

        /// <summary>
        /// Gets or sets TSBId.
        /// </summary>
        [Category("TSB")]
        [Description(" Gets or sets TSBId.")]
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

        public class FKs : LaneGroup
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

            #region Public Methods

            public LaneGroup ToLaneGroup()
            {
                LaneGroup inst = new LaneGroup();
                this.AssignTo(inst); // set all properties to new instance.
                return inst;
            }

            #endregion
        }

        #endregion

        #region Static Methods

        public static List<LaneGroup> Gets(SQLiteConnection db)
        {
            if (null == db) return new List<LaneGroup>();
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT LaneGroup.* ";
                cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
                cmd += "  FROM LaneGroup, Plaza, TSB ";
                cmd += " WHERE LaneGroup.TSBId = TSB.TSBId ";
                cmd += "   AND Plaza.TSBId = TSB.TSBId ";
                cmd += "   AND LaneGroup.PlazaId = Plaza.PlazaId ";

                var rets = NQuery.Query<FKs>(cmd).ToList();
                var results = new List<LaneGroup>();
                if (null != rets)
                {
                    rets.ForEach(ret =>
                    {
                        results.Add(ret.ToLaneGroup());
                    });
                }

                return results;
            }
        }
        public static List<LaneGroup> Gets()
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Gets(db);
            }
        }
        public static LaneGroup Get(SQLiteConnection db, string groupPkId)
        {
            if (null == db) return null;
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT LaneGroup.* ";
                cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
                cmd += "  FROM LaneGroup, Plaza, TSB ";
                cmd += " WHERE LaneGroup.TSBId = TSB.TSBId ";
                cmd += "   AND Plaza.TSBId = TSB.TSBId ";
                cmd += "   AND LaneGroup.PlazaId = Plaza.PlazaId ";
                cmd += "   AND LaneGroup.GroupPkId = ? ";

                var ret = NQuery.Query<FKs>(cmd, groupPkId).FirstOrDefault();
                return (null != ret) ? ret.ToLaneGroup() : null;
            }
        }
        public static LaneGroup Get(string groupPkId)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Get(db, groupPkId);
            }
        }

        #endregion
    }

    #endregion
}
