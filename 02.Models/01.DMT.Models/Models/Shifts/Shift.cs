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
    #region Shift

    /// <summary>
    /// The Shift Data Model class.
    /// </summary>
    //[Table("Shift")]
    public class Shift : NTable<Shift>
    {
        #region Intenral Variables

        private int _ShiftId = 0;
        private string _ShiftNameTH = string.Empty;
        private string _ShiftNameEN = string.Empty;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Shift() : base() { }

        #endregion

        #region Public Proprties

        #region Common

        /// <summary>
        /// Gets or sets ShiftId.
        /// </summary>
        [PrimaryKey]
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
        [MaxLength(50)]
        [PeropertyMapName("ShiftNameTH")]
        public string ShiftNameTH
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
        [MaxLength(50)]
        [PeropertyMapName("ShiftNameEN")]
        public string ShiftNameEN
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

        #region Status (DC)

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

        #endregion

        #region Static Methods

        public static List<Shift> Gets(SQLiteConnection db)
        {
            if (null == db) return new List<Shift>();
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM Shift ";
                return NQuery.Query<Shift>(cmd);
            }
        }
        public static List<Shift> Gets()
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Gets(db);
            }
        }
        public static Shift Get(SQLiteConnection db, string shiftId)
        {
            if (null == db) return null;
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM Shift ";
                cmd += " WHERE ShiftId = ? ";
                return NQuery.Query<Shift>(cmd, shiftId).FirstOrDefault();
            }
        }
        public static Shift Get(string shiftId)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Get(db, shiftId);
            }
        }

        #endregion
    }

    #endregion
}
