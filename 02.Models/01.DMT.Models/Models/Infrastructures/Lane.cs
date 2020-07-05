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
        private string _PlazaId = string.Empty;

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

        #region Static Methods

        public static List<Lane> Gets(SQLiteConnection db)
        {
            if (null == db) return new List<Lane>();
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM Lane ";
                return NQuery.Query<Lane>(cmd);
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
                cmd += "SELECT * FROM Lane ";
                cmd += " WHERE LaneId = ? ";
                return NQuery.Query<Lane>(cmd, laneId).FirstOrDefault();
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
                cmd += "SELECT * FROM Lane ";
                cmd += " WHERE TSBId = ?";
                return NQuery.Query<Lane>(cmd, tsbId);
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
                cmd += "SELECT * FROM Lane ";
                cmd += " WHERE TSBId = ?";
                cmd += "   AND PlazaId = ?";
                return NQuery.Query<Lane>(cmd, tsbId, plazaId);
            }
        }

        #endregion
    }

    #endregion
}
