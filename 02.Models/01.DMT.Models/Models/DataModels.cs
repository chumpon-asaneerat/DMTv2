﻿#region Using

using System;
using System.Collections.Generic;
using System.Linq;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using System.ComponentModel;
using DMT.Services;
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
    public class TSB : NTable<TSB>
    {
        #region Intenral Variables

        private string _TSBId = string.Empty;
        private string _NetworkId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

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
        /// Gets or sets TSBNameEN.
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("TSBNameEN")]
        public string TSBNameEN
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
        [MaxLength(100)]
        [PeropertyMapName("TSBNameTH")]
        public string TSBNameTH
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

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Plaza> Plazas { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="TSBId">The TSBId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static TSB Get(SQLiteConnection db, string TSBId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<TSB>(
                    p => p.TSBId == TSBId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="TSBId">The TSBId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static TSB Get(string TSBId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, TSBId, recursive);
        }

        #endregion
    }

    #endregion

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
        [ForeignKey(typeof(TSB)), MaxLength(10)]
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

        [JsonIgnore]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public TSB TSB { get; set; }

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

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Lane> Lanes { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static Plaza Get(SQLiteConnection db, string PlazaId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<Plaza>(
                    p => p.PlazaId == PlazaId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static Plaza Get(string PlazaId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, PlazaId, recursive);
        }

        #endregion
    }

    #endregion

    #region Shift

    /// <summary>
    /// The Shift Data Model class.
    /// </summary>
    //[Table("Shift")]
    public class Shift : NTable<Shift>
    {
        #region Intenral Variables

        private int _ShiftId = 0;
        private string _NameTH = string.Empty;
        private string _NameEN = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Shift() : base() { }

        #endregion

        #region Public Proprties

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
        [MaxLength(10)]
        [PeropertyMapName("NameTH")]
        public string NameTH
        {
            get
            {
                return _NameTH;
            }
            set
            {
                if (_NameTH != value)
                {
                    _NameTH = value;
                    this.RaiseChanged("NameTH");
                }
            }
        }
        /// <summary>
        /// Gets or sets Name EN.
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("NameEN")]
        public string NameEN
        {
            get
            {
                return _NameEN;
            }
            set
            {
                if (_NameEN != value)
                {
                    _NameEN = value;
                    this.RaiseChanged("NameEN");
                }
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="shiftId">The TSBId.</param>
        /// <returns>Returns found record.</returns>
        internal static Shift Get(SQLiteConnection db, int shiftId)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<Shift>(
                    p => p.ShiftId == shiftId).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="shiftId">The shiftId.</param>
        /// <returns>Returns found record.</returns>
        public static Shift Get(int shiftId)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, shiftId);
        }

        #endregion
    }

    #endregion

    #region Lane

    /// <summary>
    /// The Lane Data Model class.
    /// </summary>
    //[Table("Lane")]
    public class Lane : NTable<Lane>
    {
        #region Intenral Variables

        private int _LanePkId = 0;
        private int _LaneId = 0;
        private string _LaneType = string.Empty;
        private string _LaneAbbr = string.Empty;
        private string _PlazaId = string.Empty;

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
        [PeropertyMapName("LanePkId")]
        public int LanePkId
        {
            get
            {
                return _LanePkId;
            }
            set
            {
                if (_LanePkId != value)
                {
                    _LanePkId = value;
                    this.RaiseChanged("LanePkId");
                }
            }
        }
        /// <summary>
        /// Gets or sets LaneId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("LaneId")]
        public int LaneId
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
        /// <summary>
        /// Gets or sets LaneType
        /// </summary>
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
        /// Gets or sets PlazaId
        /// </summary>
        [ForeignKey(typeof(TSB)), MaxLength(10)]
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

        [JsonIgnore]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Plaza Plaza { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <param name="LaneId">The LaneId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static Lane Get(SQLiteConnection db, string PlazaId, int LaneId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<Lane>(
                    p => p.PlazaId == PlazaId &&
                    p.LaneId == LaneId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="PlazaId">The PlazaId.</param>
        /// <param name="LaneId">The LaneId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static Lane Get(string PlazaId, int LaneId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, PlazaId, LaneId, recursive);
        }

        #endregion
    }

    #endregion

    #region Role

    /// <summary>
    /// The Role Data Model Class.
    /// </summary>
    //[Table("Role")]
    public class Role : NTable<Role>
    {
        #region Intenral Variables

        private string _RoleId = string.Empty;
        private string _RoleName = string.Empty;

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
        /// Gets or sets RoleName
        /// </summary>
        [MaxLength(50)]
        [PeropertyMapName("RoleName")]
        public string RoleName
        {
            get
            {
                return _RoleName;
            }
            set
            {
                if (_RoleName != value)
                {
                    _RoleName = value;
                    this.RaiseChanged("RoleName");
                }
            }
        }

        [JsonIgnore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<User> Users { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="RoleId">The RoleId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static Role Get(SQLiteConnection db, string RoleId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<Role>(
                    p => p.RoleId == RoleId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="RoleId">The RoleId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static Role Get(string RoleId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, RoleId, recursive);
        }

        #endregion
    }

    #endregion

    #region User

    /// <summary>
    /// The User Data Model Class.
    /// </summary>
    //[Table("User")]
    public class User : NTable<User>
    {
        #region Intenral Variables

        private string _UserId = string.Empty;
        private string _FullNameEN = string.Empty;
        private string _FullNameTH = string.Empty;
        private string _UserName = string.Empty;
        private string _Password = string.Empty;
        private string _CardId = string.Empty;
        private string _RoleId = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public User() : base() { }

        #endregion

        #region Public Proprties
        /// <summary>
        /// Gets or sets UserId
        /// </summary>
        [PrimaryKey, MaxLength(10)]
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
        [MaxLength(100)]
        [PeropertyMapName("FullNameEN")]
        public string FullNameEN
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
        [MaxLength(100)]
        [PeropertyMapName("FullNameTH")]
        public string FullNameTH
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


        /// <summary>
        /// Gets or sets UserName
        /// </summary>
        [MaxLength(20)]
        [PeropertyMapName("UserName")]
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    this.RaiseChanged("UserName");
                }
            }
        }

        /// <summary>
        /// Gets or sets Password
        /// </summary>
        [MaxLength(20)]
        [PeropertyMapName("Password")]
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    this.RaiseChanged("Password");
                }
            }
        }

        /// <summary>
        /// Gets or sets CardId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("CardId")]
        public string CardId
        {
            get
            {
                return _CardId;
            }
            set
            {
                if (_CardId != value)
                {
                    _CardId = value;
                    this.RaiseChanged("CardId");
                }
            }
        }

        /// <summary>
        /// Gets or sets RoleId
        /// </summary>
        [ForeignKey(typeof(Role)), MaxLength(10)]
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

        [JsonIgnore]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Role Role { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets by Id without password.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="UserId">The UserId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static User Get(SQLiteConnection db, string UserId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<User>(
                    p => p.UserId == UserId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by UserId and password.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="UserId">The UserId.</param>
        /// /// <param name="password">The password.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static User GetByUserId(SQLiteConnection db, string UserId, string password, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<User>(
                    p => p.UserId == UserId && p.Password == password,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by UserName and password.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="userName">The userName.</param>
        /// /// <param name="password">The password.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static User GetByUserName(SQLiteConnection db, string userName, string password, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<User>(
                    p => p.UserName == userName && p.Password == password,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by CardId
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="cardId">The cardId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static User GetByCardId(SQLiteConnection db, string cardId, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<User>(
                    p => p.CardId == cardId,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="UserId">The UserId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static User Get(string UserId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, UserId, recursive);
        }
        /// <summary>
        /// Gets by UserId and password.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="UserId">The UserId.</param>
        /// /// <param name="password">The password.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static User GetByUserId(string UserId, string password, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return GetByUserId(db, UserId, password, recursive);
        }
        /// <summary>
        /// Gets by UserName and password.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="userName">The userName.</param>
        /// /// <param name="password">The password.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static User GetByUserName(string userName, string password, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return GetByUserName(db, userName, password, recursive);
        }
        /// <summary>
        /// Gets by CardId
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="cardId">The cardId.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static User GetByCardId(string cardId, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return GetByCardId(db, cardId, recursive);
        }

        #endregion
    }

    #endregion

    #region Config

    /// <summary>
    /// The Config Data Model Class.
    /// </summary>
    [Table("Config")]
    public class Config : NTable<Config>
    {
        #region Intenral Variables

        private string _Key = string.Empty;
        private string _Value = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Config() : base() { }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets Key
        /// </summary>
        [PrimaryKey, MaxLength(20)]
        [PeropertyMapName("Key")]
        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                if (_Key != value)
                {
                    _Key = value;
                    this.RaiseChanged("Key");
                }
            }
        }

        /// <summary>
        /// Gets or sets Value
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("Value")]
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    this.RaiseChanged("Value");
                }
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets by Key.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="key">The key.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        internal static Config Get(SQLiteConnection db, string key, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return null;
                return db.GetAllWithChildren<Config>(
                    p => p.Key == key,
                    recursive: recursive).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by Id
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static Config Get(string key, bool recursive = false)
        {
            SQLiteConnection db = LocalDbServer.Instance.Db;
            return Get(db, key, recursive);
        }

        #endregion
    }

    #endregion

    #region SupervisorShift

    /// <summary>
    /// The SupervisorShift Data Model Class.
    /// </summary>
    //[Table("SupervisorShift")]
    public class SupervisorShift : NTable<SupervisorShift>
    {
        #region Intenral Variables

        private int _SupervisorShiftId = 0;
        private string _PlazaId = string.Empty;
        private string _SupervisorId = string.Empty;
        private DateTime _Begin = DateTime.MinValue;
        private DateTime _End = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SupervisorShift() : base() { }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets SupervisorShiftId
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("SupervisorShiftId")]
        public int SupervisorShiftId
        {
            get
            {
                return _SupervisorShiftId;
            }
            set
            {
                if (_SupervisorShiftId != value)
                {
                    _SupervisorShiftId = value;
                    this.RaiseChanged("SupervisorShiftId");
                }
            }
        }

        /// <summary>
        /// Gets or sets PlazaId
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
        /// Gets or sets SupervisorId
        /// </summary>
        [ForeignKey(typeof(User), Name = "UserId"), MaxLength(10)]
        [PeropertyMapName("SupervisorId")]
        public string SupervisorId
        {
            get
            {
                return _SupervisorId;
            }
            set
            {
                if (_SupervisorId != value)
                {
                    _SupervisorId = value;
                    this.RaiseChanged("SupervisorId");
                }
            }
        }

        [JsonIgnore]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "SupervisorId", CascadeOperations = CascadeOperation.All)]
        public User User { get; set; }

        /// <summary>
        /// Gets or sets Begin Date.
        /// </summary>
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
                }
            }
        }
        /// <summary>
        /// Gets or sets End Date.
        /// </summary>
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
                }
            }
        }

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion

    #region CollectorJob

    /// <summary>
    /// The CollectorJob Data Model Class.
    /// </summary>
    //[Table("CollectorJob")]
    public class CollectorJob : NTable<CollectorJob>
    {
        #region Intenral Variables

        private int _JobId = 0;
        private int _ShiftId = 0;
        private string _PlazaId = string.Empty;
        private string _CollectorId = string.Empty;
        private DateTime _Begin = DateTime.MinValue;
        private DateTime _End = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorJob() : base() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets CollectorShiftId
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("JobId")]
        public int JobId
        {
            get
            {
                return _JobId;
            }
            set
            {
                if (_JobId != value)
                {
                    _JobId = value;
                    this.RaiseChanged("JobId");
                }
            }
        }
        /// <summary>
        /// Gets or sets ShiftId
        /// </summary>
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
        /// Gets or sets PlazaId
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
        /// Gets or sets CollectorId
        /// </summary>
        [ForeignKey(typeof(User), Name = "UserId"), MaxLength(10)]
        [PeropertyMapName("CollectorId")]
        public string CollectorId
        {
            get
            {
                return _CollectorId;
            }
            set
            {
                if (_CollectorId != value)
                {
                    _CollectorId = value;
                    this.RaiseChanged("CollectorId");
                }
            }
        }

        [JsonIgnore]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "CollectorId", CascadeOperations = CascadeOperation.All)]
        public User User { get; set; }

        /// <summary>
        /// Gets or sets Begin Date.
        /// </summary>
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
                }
            }
        }
        /// <summary>
        /// Gets or sets End Date.
        /// </summary>
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
                }
            }
        }

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion

    #region CollectorShift

    /// <summary>
    /// The CollectorShift Data Model Class.
    /// </summary>
    //[Table("CollectorShift")]
    public class CollectorShift : NTable<CollectorShift>
    {
        #region Intenral Variables

        private int _CollectorShiftId = 0;
        private string _PlazaId = string.Empty;
        private string _CollectorId = string.Empty;
        private DateTime _Begin = DateTime.MinValue;
        private DateTime _End = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorShift() : base() { }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets CollectorShiftId
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("CollectorShiftId")]
        public int CollectorShiftId
        {
            get
            {
                return _CollectorShiftId;
            }
            set
            {
                if (_CollectorShiftId != value)
                {
                    _CollectorShiftId = value;
                    this.RaiseChanged("CollectorShiftId");
                }
            }
        }

        /// <summary>
        /// Gets or sets PlazaId
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
        /// Gets or sets CollectorId
        /// </summary>
        [ForeignKey(typeof(User), Name = "UserId"), MaxLength(10)]
        [PeropertyMapName("CollectorId")]
        public string CollectorId
        {
            get
            {
                return _CollectorId;
            }
            set
            {
                if (_CollectorId != value)
                {
                    _CollectorId = value;
                    this.RaiseChanged("CollectorId");
                }
            }
        }

        [JsonIgnore]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "CollectorId", CascadeOperations = CascadeOperation.All)]
        public User User { get; set; }

        /// <summary>
        /// Gets or sets Begin Date.
        /// </summary>
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
                }
            }
        }
        /// <summary>
        /// Gets or sets End Date.
        /// </summary>
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
                }
            }
        }

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion

    #region CollectorLane

    /// <summary>
    /// The CollectorLane Data Model Class.
    /// </summary>
    public class CollectorLane : NTable<CollectorLane>
    {
        #region Intenral Variables

        private int _CollectorShiftId = 0;
        private string _PlazaId = string.Empty;
        private string _CollectorId = string.Empty;
        private int _LaneId = 0;
        private DateTime _Begin = DateTime.MinValue;
        private DateTime _End = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorLane() : base() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets CollectorShiftId
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("CollectorShiftId")]
        public int CollectorShiftId
        {
            get
            {
                return _CollectorShiftId;
            }
            set
            {
                if (_CollectorShiftId != value)
                {
                    _CollectorShiftId = value;
                    this.RaiseChanged("CollectorShiftId");
                }
            }
        }

        /// <summary>
        /// Gets or sets PlazaId
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
        /// Gets or sets CollectorId
        /// </summary>
        [ForeignKey(typeof(User), Name = "UserId"), MaxLength(10)]
        [PeropertyMapName("CollectorId")]
        public string CollectorId
        {
            get
            {
                return _CollectorId;
            }
            set
            {
                if (_CollectorId != value)
                {
                    _CollectorId = value;
                    this.RaiseChanged("CollectorId");
                }
            }
        }

        [JsonIgnore]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "CollectorId", CascadeOperations = CascadeOperation.All)]
        public User User { get; set; }

        [PeropertyMapName("LaneId")]
        public int LaneId
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

        /// <summary>
        /// Gets or sets Begin Date.
        /// </summary>
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
                }
            }
        }
        /// <summary>
        /// Gets or sets End Date.
        /// </summary>
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
                }
            }
        }

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion

    #region Revenue Entry

    /// <summary>
    /// The RevenueEntry class.
    /// </summary>
    //[Table("RevenueEntry")]
    public class RevenueEntry : NTable<RevenueEntry>
    {
        #region Intenral Variables

        // Common
        private int _EntryId = 0;

        private DateTime _EntryDate = DateTime.MinValue;
        private DateTime _RevDate = DateTime.MinValue;
        private int _ShiftId = 0;
        private string _PlazaId = string.Empty;
        private string _CollectorId = string.Empty;

        private string _bagNo = string.Empty;
        private string _bagNo2 = string.Empty;
        // Traffic
        private int _TrafficBHT1 = 0;
        private int _TrafficBHT2 = 0;
        private int _TrafficBHT5 = 0;
        private int _TrafficBHT10 = 0;
        private int _TrafficBHT20 = 0;
        private int _TrafficBHT50 = 0;
        private int _TrafficBHT100 = 0;
        private int _TrafficBHT500 = 0;
        private int _TrafficBHT1000 = 0;
        private decimal _TrafficBHTTotal = decimal.Zero;
        private string _TrafficRemark = "";
        // Other
        private decimal _OtherBHTTotal = decimal.Zero;
        private string _OtherRemark = "";
        // Coupon Usage
        private int _CouponUsageBHT30 = 0;
        private int _CouponUsageBHT35 = 0;
        private int _CouponUsageBHT75 = 0;
        private int _CouponUsageBHT80 = 0;
        private int _CouponUsageFreePassA = 0;
        private int _CouponUsageFreePassOther = 0;
        // Coupon Sold
        private int _CouponSoldBHT35 = 0;
        private int _CouponSoldBHT80 = 0;
        private int _CouponSoldBHT35Total = 0;
        private int _CouponSoldBHT80Total = 0;
        private int _CouponSoldBHTTotal = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueEntry() : base() { }

        #endregion

        #region Private Methods

        private void CalcTrafficTotal()
        {
            decimal total = 0;
            total += _TrafficBHT1 * 1;
            total += _TrafficBHT2 * 2;
            total += _TrafficBHT5 * 5;
            total += _TrafficBHT10 * 10;
            total += _TrafficBHT20 * 20;
            total += _TrafficBHT50 * 50;
            total += _TrafficBHT100 * 100;
            total += _TrafficBHT500 * 500;
            total += _TrafficBHT1000 * 1000;

            _TrafficBHTTotal = total;
            // Raise event.
            this.RaiseChanged("TrafficBHTTotal");
        }

        private void CalcCouponSoldTotal()
        {
            // Raise event.
            RaiseChanged("CntTotal");

            int total = 0;
            total += _CouponSoldBHT35Total;
            total += _CouponSoldBHT80Total;

            _CouponSoldBHTTotal = total;
            // Raise event.
            this.RaiseChanged("CouponSoldBHTTotal");
        }

        #endregion

        #region Public Properties

        #region Common

        /// <summary>
        /// Gets or sets EntryId
        /// </summary>
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("EntryId")]
        public int EntryId
        {
            get
            {
                return _EntryId;
            }
            set
            {
                if (_EntryId != value)
                {
                    _EntryId = value;
                    this.RaiseChanged("EntryId");
                }
            }
        }
        /// <summary>
        /// Gets or sets Entry Date.
        /// </summary>
        [PeropertyMapName("EntryDate")]
        public DateTime EntryDate
        {
            get { return _EntryDate; }
            set
            {
                if (_EntryDate != value)
                {
                    _EntryDate = value;
                    // Raise event.
                    this.RaiseChanged("EntryDate");
                }
            }
        }
        /// <summary>
        /// Gets or sets Revenue Date.
        /// </summary>
        [PeropertyMapName("RevDate")]
        public DateTime RevDate
        {
            get { return _RevDate; }
            set
            {
                if (_RevDate != value)
                {
                    _RevDate = value;
                    // Raise event.
                    this.RaiseChanged("RevDate");
                }
            }
        }

        /// <summary>
        /// Gets or sets ShiftId
        /// </summary>
        [ForeignKey(typeof(Shift), Name = "ShiftId")]
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

        [JsonIgnore]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "ShiftId", CascadeOperations = CascadeOperation.All)]
        public Shift Shift { get; set; }

        /// <summary>
        /// Gets or sets PlazaId
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
        /// Gets or sets CollectorId
        /// </summary>
        [ForeignKey(typeof(User), Name = "UserId"), MaxLength(10)]
        [PeropertyMapName("CollectorId")]
        public string CollectorId
        {
            get
            {
                return _CollectorId;
            }
            set
            {
                if (_CollectorId != value)
                {
                    _CollectorId = value;
                    this.RaiseChanged("CollectorId");
                }
            }
        }

        [JsonIgnore]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [OneToOne(foreignKey: "CollectorId", CascadeOperations = CascadeOperation.All)]
        public User User { get; set; }

        /// <summary>
        /// Gets or sets Bag Number.
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("BagNo")]
        public string BagNo
        {
            get { return _bagNo; }
            set
            {
                if (_bagNo != value)
                {
                    _bagNo = value;
                    // Raise event.
                    this.RaiseChanged("BagNo");
                }
            }
        }
        /// <summary>
        /// Gets or sets Bag Number (2).
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("BagNo2")]
        public string BagNo2
        {
            get { return _bagNo2; }
            set
            {
                if (_bagNo2 != value)
                {
                    _bagNo2 = value;
                    // Raise event.
                    this.RaiseChanged("BagNo2");
                }
            }
        }

        #endregion

        #region Traffic

        /// <summary>
        /// Gets or sets number of 1 baht coin.
        /// </summary>
        [PeropertyMapName("TrafficBHT1")]
        public int TrafficBHT1
        {
            get { return _TrafficBHT1; }
            set
            {
                if (_TrafficBHT1 != value)
                {
                    _TrafficBHT1 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT1BHT1");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 2 baht coin.
        /// </summary>
        [PeropertyMapName("TrafficBHT2")]
        public int TrafficBHT2
        {
            get { return _TrafficBHT2; }
            set
            {
                if (_TrafficBHT2 != value)
                {
                    _TrafficBHT2 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT2");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 5 baht coin.
        /// </summary>
        [PeropertyMapName("TrafficBHT5")]
        public int TrafficBHT5
        {
            get { return _TrafficBHT5; }
            set
            {
                if (_TrafficBHT5 != value)
                {
                    _TrafficBHT5 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT5");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 10 baht coin.
        /// </summary>
        [PeropertyMapName("TrafficBHT10")]
        public int TrafficBHT10
        {
            get { return _TrafficBHT10; }
            set
            {
                if (_TrafficBHT10 != value)
                {
                    _TrafficBHT10 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT10");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 20 baht bill.
        /// </summary>
        [PeropertyMapName("TrafficBHT20")]
        public int TrafficBHT20
        {
            get { return _TrafficBHT20; }
            set
            {
                if (_TrafficBHT20 != value)
                {
                    _TrafficBHT20 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT20");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 50 baht bill.
        /// </summary>
        [PeropertyMapName("TrafficBHT50")]
        public int TrafficBHT50
        {
            get { return _TrafficBHT50; }
            set
            {
                if (_TrafficBHT50 != value)
                {
                    _TrafficBHT50 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT50");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 100 baht bill.
        /// </summary>
        [PeropertyMapName("TrafficBHT100")]
        public int TrafficBHT100
        {
            get { return _TrafficBHT100; }
            set
            {
                if (_TrafficBHT100 != value)
                {
                    _TrafficBHT100 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT100");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 500 baht bill.
        /// </summary>
        [PeropertyMapName("TrafficBHT500")]
        public int TrafficBHT500
        {
            get { return _TrafficBHT500; }
            set
            {
                if (_TrafficBHT500 != value)
                {
                    _TrafficBHT500 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT500");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 1000 baht bill.
        /// </summary>
        [PeropertyMapName("TrafficBHT1000")]
        public int TrafficBHT1000
        {
            get { return _TrafficBHT1000; }
            set
            {
                if (_TrafficBHT1000 != value)
                {
                    _TrafficBHT1000 = value;
                    CalcTrafficTotal();
                    // Raise event.
                    this.RaiseChanged("TrafficBHT1000");
                }
            }
        }
        /// <summary>
        /// Gets or sets total value in baht.
        /// </summary>
        [PeropertyMapName("TrafficBHTTotal")]
        public decimal TrafficBHTTotal
        {
            get { return _TrafficBHTTotal; }
            set { }
        }

        /// <summary>
        /// Gets or sets Traffic Remark.
        /// </summary>
        [MaxLength(255)]
        [PeropertyMapName("TrafficRemark")]
        public string TrafficRemark
        {
            get { return _TrafficRemark; }
            set
            {
                if (_TrafficRemark != value)
                {
                    _TrafficRemark = value;
                    // Raise event.
                    //this.RaiseChanged("Remark");
                }
            }
        }

        #endregion

        #region Other

        /// <summary>
        /// Gets or sets total value in baht (Other).
        /// </summary>
        [PeropertyMapName("OtherBHTTotal")]
        public decimal OtherBHTTotal
        {
            get { return _OtherBHTTotal; }
            set
            {
                if (_OtherBHTTotal != value)
                {
                    _OtherBHTTotal = value;
                    // Raise event.
                    this.RaiseChanged("OtherBHTTotal");
                }
            }
        }
        /// <summary>
        /// Gets or sets Other Remark.
        /// </summary>
        [MaxLength(255)]
        [PeropertyMapName("OtherRemark")]
        public string OtherRemark
        {
            get { return _OtherRemark; }
            set
            {
                if (_OtherRemark != value)
                {
                    _OtherRemark = value;
                    // Raise event.
                    //this.RaiseChanged("Remark");
                }
            }
        }

        #endregion

        #region Coupon Usage

        /// <summary>
        /// Gets or sets number of 30 BHT coupon.
        /// </summary>
        [PeropertyMapName("CouponUsageBHT30")]
        public int CouponUsageBHT30
        {
            get { return _CouponUsageBHT30; }
            set
            {
                if (_CouponUsageBHT30 != value)
                {
                    _CouponUsageBHT30 = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageBHT30");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 35 BHT coupon.
        /// </summary>
        [PeropertyMapName("CouponUsageBHT35")]
        public int CouponUsageBHT35
        {
            get { return _CouponUsageBHT35; }
            set
            {
                if (_CouponUsageBHT35 != value)
                {
                    _CouponUsageBHT35 = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageBHT35");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 75 BHT coupon.
        /// </summary>
        [PeropertyMapName("CouponUsageBHT75")]
        public int CouponUsageBHT75
        {
            get { return _CouponUsageBHT75; }
            set
            {
                if (_CouponUsageBHT75 != value)
                {
                    _CouponUsageBHT75 = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageBHT75");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 80 BHT coupon.
        /// </summary>
        [PeropertyMapName("CouponUsageBHT80")]
        public int CouponUsageBHT80
        {
            get { return _CouponUsageBHT80; }
            set
            {
                if (_CouponUsageBHT80 != value)
                {
                    _CouponUsageBHT80 = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageBHT80");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of FreePass Class A (4 wheel).
        /// </summary>
        [PeropertyMapName("CouponUsageFreePassA")]
        public int CouponUsageFreePassA
        {
            get { return _CouponUsageFreePassA; }
            set
            {
                if (_CouponUsageFreePassA != value)
                {
                    _CouponUsageFreePassA = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageFreePassA");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of FreePass Other (> 4 wheel).
        /// </summary>
        [PeropertyMapName("CouponUsageFreePassOther")]
        public int CouponUsageFreePassOther
        {
            get { return _CouponUsageFreePassOther; }
            set
            {
                if (_CouponUsageFreePassOther != value)
                {
                    _CouponUsageFreePassOther = value;
                    // Raise event.
                    this.RaiseChanged("CouponUsageFreePassOther");
                }
            }
        }

        #endregion

        #region Coupon Sold

        /// <summary>
        /// Gets or sets number of 35 BHT coupon.
        /// </summary>
        [PeropertyMapName("CouponSoldBHT35")]
        public int CouponSoldBHT35
        {
            get { return _CouponSoldBHT35; }
            set
            {
                if (_CouponSoldBHT35 != value)
                {
                    _CouponSoldBHT35 = value;
                    _CouponSoldBHT35Total = _CouponSoldBHT35 * 665;
                    CalcCouponSoldTotal();
                    // Raise event.
                    this.RaiseChanged("CouponSoldBHT35");
                    this.RaiseChanged("CouponSoldBHT35Total");
                }
            }
        }
        /// <summary>
        /// Gets or sets number of 80 BHT coupon.
        /// </summary>
        [PeropertyMapName("CouponSoldBHT80")]
        public int CouponSoldBHT80
        {
            get { return _CouponSoldBHT80; }
            set
            {
                if (_CouponSoldBHT80 != value)
                {
                    _CouponSoldBHT80 = value;
                    _CouponSoldBHT80Total = _CouponSoldBHT80 * 1520;
                    CalcCouponSoldTotal();
                    // Raise event.
                    this.RaiseChanged("CouponSoldBHT80");
                    this.RaiseChanged("CouponSoldBHT80Total");
                }
            }
        }
        [PeropertyMapName("CouponSoldBHT35Total")]
        public int CouponSoldBHT35Total
        {
            get { return _CouponSoldBHT35Total; }
            set { }
        }
        [PeropertyMapName("CouponSoldBHT80Total")]
        public int CouponSoldBHT80Total
        {
            get { return _CouponSoldBHT80Total; }
            set { }
        }
        /// <summary>
        /// Gets or sets total value in baht.
        /// </summary>
        [PeropertyMapName("CouponSoldBHT80Total")]
        public decimal CouponSoldBHTTotal
        {
            get { return _CouponSoldBHTTotal; }
            set { }
        }

        #endregion

        #endregion
    }

    #endregion
}
