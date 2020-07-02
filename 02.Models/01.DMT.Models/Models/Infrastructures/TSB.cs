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
    #region TSB

    /// <summary>
    /// The TSB Data Model class.
    /// </summary>
    //[Table("TSB")]
    public class TSB : TSBBase<TSB>
    {
        #region Intenral Variables

        private string _NetworkId = string.Empty;
        private bool _Active = false;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSB() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets TSBId.
        /// </summary>
        [PrimaryKey, MaxLength(10)]
        [PeropertyMapName("TSBId")]
        public new string TSBId
        {
            get { return base.TSBId; }
            set { base.TSBId = value;  }
        }
        /// <summary>
        /// Gets or sets NetworkId.
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("NetworkId")]
        public string NetworkId
        {
            get
            {
                return _NetworkId;
            }
            set
            {
                if (_NetworkId != value)
                {
                    _NetworkId = value;
                    this.RaiseChanged("NetworkId");
                }
            }
        }
        /// <summary>
        /// Gets or sets is active TSB.
        /// </summary>
        [PeropertyMapName("Active")]
        public bool Active
        {
            get
            {
                return _Active;
            }
            set
            {
                if (_Active != value)
                {
                    _Active = value;
                    this.RaiseChanged("Active");
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

        public static void SetActive(string tsbId)
        {
            lock (sync)
            {
                // inactive all TSBs
                string cmd = string.Empty;
                cmd += "UPDATE TSB ";
                cmd += "   SET Active = 0";
                NQuery.Execute(cmd);
                // Set active TSB
                cmd = string.Empty;
                cmd += "UPDATE TSB ";
                cmd += "   SET Active = 1 ";
                cmd += " WHERE TSBId = ? ";
                NQuery.Execute(cmd, tsbId);
            }
        }

        public static TSB GetCurrent()
        {
            lock (sync)
            {
                // inactive all TSBs
                string cmd = string.Empty;
                cmd += "SELECT * FROM TSB ";
                cmd += " WHERE Active = 1 ";
                var results = NQuery.Query<TSB>(cmd, true);
                return (null != results) ? results.FirstOrDefault() : null;
            }
        }

        #endregion
    }

    #endregion
}
