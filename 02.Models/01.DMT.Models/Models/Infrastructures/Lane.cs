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

#endregion

namespace DMT.Models
{
    #region Lane

    /// <summary>
    /// The Lane Data Model class.
    /// </summary>
    //[Table("Lane")]
    public class Lane : NTable<Lane>
    {
        #region Intenral Variables

        private int _PkId = 0;
        private int _LaneNo = 0;
        private string _LaneId = string.Empty;
        private string _LaneType = string.Empty;
        private string _LaneAbbr = string.Empty;

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
        public Lane() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets LanePkId
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("PkId")]
        public int PkId
        {
            get
            {
                return _PkId;
            }
            set
            {
                if (_PkId != value)
                {
                    _PkId = value;
                    this.RaiseChanged("PkId");
                }
            }
        }
        /// <summary>
        /// Gets or sets Lane No.
        /// </summary>
        [PeropertyMapName("LaneNo")]
        public int LaneNo
        {
            get
            {
                return _LaneNo;
            }
            set
            {
                if (_LaneNo != value)
                {
                    _LaneNo = value;
                    this.RaiseChanged("LaneNo");
                }
            }
        }
        /// <summary>
        /// Gets or sets LaneType
        /// </summary>
        /// <summary>
        /// Gets or sets LaneId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("LaneId")]
        public string LaneId
        {
            get
            {
                return _LaneId;
            }
            set
            {
                if (_LaneId != value)
                {
                    _LaneId = value;
                    this.RaiseChanged("LaneId");
                }
            }
        }
        [MaxLength(10)]
        [PeropertyMapName("LaneType")]
        public string LaneType
        {
            get
            {
                return _LaneType;
            }
            set
            {
                if (_LaneType != value)
                {
                    _LaneType = value;
                    this.RaiseChanged("LaneType");
                }
            }
        }
        /// <summary>
        /// Gets or sets LaneAbbr
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("LaneAbbr")]
        public string LaneAbbr
        {
            get
            {
                return _LaneAbbr;
            }
            set
            {
                if (_LaneAbbr != value)
                {
                    _LaneAbbr = value;
                    this.RaiseChanged("LaneAbbr");
                }
            }
        }
        /// <summary>
        /// Gets or sets TSBId.
        /// </summary>
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
        /// <summary>
        /// Gets or sets PlazaId.
        /// </summary>
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

        /// <summary>
        /// Gets or sets Status (1 = Sync, 0 = Unsync, etc..)
        /// </summary>
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

        #region Internal Class

        internal class FKs : Lane
        {
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
        }

        #endregion

        #region Static Methods

        public static List<Lane> Gets(SQLiteConnection db)
        {
            if (null == db) return new List<Lane>();
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT Lane.* ";
                cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
                cmd += "  FROM Lane, Plaza, TSB ";
                cmd += " WHERE Lane.TSBId = TSB.TSBId ";
                cmd += "   AND Plaza.TSBId = TSB.TSBId ";
                cmd += "   AND Lane.PlazaId = Plaza.PlazaId ";
                var results = NQuery.Query<FKs>(cmd).ToList<Lane>();
                return results;
            }
        }
        public static List<Lane> Gets()
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Gets(db);
            }
        }
        public static Lane Get(SQLiteConnection db, string laneId)
        {
            if (null == db) return null;
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT Lane.* ";
                cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
                cmd += "  FROM Lane, Plaza, TSB ";
                cmd += " WHERE Lane.TSBId = TSB.TSBId ";
                cmd += "   AND Plaza.TSBId = TSB.TSBId ";
                cmd += "   AND Lane.PlazaId = Plaza.PlazaId ";
                cmd += "   AND Lane.LaneId = ? ";
                return NQuery.Query<FKs>(cmd, laneId).FirstOrDefault<Lane>();
            }
        }
        public static Lane Get(string laneId)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Get(db, laneId);
            }
        }

        public static List<Lane> GetTSBLanes(TSB value)
        {
            lock (sync)
            {
                if (null == value) return new List<Lane>();
                return GetTSBLanes(value.TSBId);
            }
        }
        public static List<Lane> GetTSBLanes(string tsbId)
        {
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT Lane.* ";
                cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
                cmd += "  FROM Lane, Plaza, TSB ";
                cmd += " WHERE Lane.TSBId = TSB.TSBId ";
                cmd += "   AND Plaza.TSBId = TSB.TSBId ";
                cmd += "   AND Lane.PlazaId = Plaza.PlazaId ";
                cmd += "   AND Lane.TSBId = ? ";
                return NQuery.Query<FKs>(cmd, tsbId).ToList<Lane>();
            }
        }
        public static List<Lane> GetPlazaLanes(Plaza value)
        {
            lock (sync)
            {
                if (null == value) return new List<Lane>();
                return GetPlazaLanes(value.TSBId, value.PlazaId);
            }
        }
        public static List<Lane> GetPlazaLanes(string tsbId, string plazaId)
        {
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT Lane.* ";
                cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
                cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
                cmd += "  FROM Lane, Plaza, TSB ";
                cmd += " WHERE Lane.TSBId = TSB.TSBId ";
                cmd += "   AND Plaza.TSBId = TSB.TSBId ";
                cmd += "   AND Lane.PlazaId = Plaza.PlazaId ";
                cmd += "   AND Lane.TSBId = ? ";
                cmd += "   AND Lane.PlazaId = ? ";
                return NQuery.Query<FKs>(cmd, tsbId, plazaId).ToList<Lane>();
            }
        }

        #endregion
    }

    #endregion
}
