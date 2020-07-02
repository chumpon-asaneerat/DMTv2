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
    #region Plaza

    /// <summary>
    /// The Plaza Data Model class.
    /// </summary>
    //[Table("Plaza")]
    public class Plaza : NTable<Plaza>
    {
        #region Intenral Variables

        private string _PlazaId = string.Empty;
        private string _TSBId = string.Empty;
        private string _PlazaNameEN = string.Empty;
        private string _PlazaNameTH = string.Empty;
        private string _Direction = string.Empty;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Plaza() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets PlazaId
        /// </summary>
        [PrimaryKey, MaxLength(10)]
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
        /// Gets or sets TSBId
        /// </summary>
        //[ForeignKey(typeof(TSB)), MaxLength(10)]
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
        /// Gets or sets PlazaNameEN
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("PlazaNameEN")]
        public string PlazaNameEN
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
        [MaxLength(100)]
        [PeropertyMapName("PlazaNameTH")]
        public string PlazaNameTH
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
        /// Gets or sets Direction
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("Direction")]
        public string Direction
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

        public static List<Plaza> GetTSBPlazas(TSB value)
        {
            lock (sync)
            {
                if (null == value) return new List<Plaza>();
                return GetTSBPlazas(value.TSBId);
            }
        }
        public static List<Plaza> GetTSBPlazas(string tsbId)
        {
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM Plaza ";
                cmd += " WHERE TSBId = ?";
                return NQuery.Query<Plaza>(cmd, tsbId);
            }
        }

        #endregion
    }

    #endregion
}
