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
    #region Role

    /// <summary>
    /// The Role Data Model Class.
    /// </summary>
    //[Table("Role")]
    public class Role : NTable<Role>
    {
        #region Intenral Variables

        private string _RoleId = string.Empty;
        private string _RoleNameEN = string.Empty;
        private string _RoleNameTH = string.Empty;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Role() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets RoleId
        /// </summary>
        [PrimaryKey, MaxLength(20)]
        [PeropertyMapName("RoleId")]
        public string RoleId
        {
            get
            {
                return _RoleId;
            }
            set
            {
                if (_RoleId != value)
                {
                    _RoleId = value;
                    this.RaiseChanged("RoleId");
                }
            }
        }
        /// <summary>
        /// Gets or sets RoleNameEN
        /// </summary>
        [MaxLength(50)]
        [PeropertyMapName("RoleNameEN")]
        public string RoleNameEN
        {
            get
            {
                return _RoleNameEN;
            }
            set
            {
                if (_RoleNameEN != value)
                {
                    _RoleNameEN = value;
                    this.RaiseChanged("RoleNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets RoleNameTH
        /// </summary>
        [MaxLength(50)]
        [PeropertyMapName("RoleNameTH")]
        public string RoleNameTH
        {
            get
            {
                return _RoleNameTH;
            }
            set
            {
                if (_RoleNameTH != value)
                {
                    _RoleNameTH = value;
                    this.RaiseChanged("RoleNameTH");
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

        public static List<Role> Gets(SQLiteConnection db)
        {
            if (null == db) return new List<Role>();
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM Role ";
                return NQuery.Query<Role>(cmd);
            }
        }
        public static List<Role> Gets()
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Gets(db);
            }
        }

        public static Role Get(SQLiteConnection db, string roleId)
        {
            if (null == db) return null;
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM Role ";
                cmd += " WHERE RoleId = ? ";
                return NQuery.Query<Role>(cmd, roleId).FirstOrDefault();
            }
        }
        public static Role Get(string roleId)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Get(db, roleId);
            }
        }

        #endregion
    }

    #endregion
}
