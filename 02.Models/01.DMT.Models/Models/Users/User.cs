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

        private int _Status = 1;
        private DateTime _LastUpdate = DateTime.MinValue;

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
        [MaxLength(50)]
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
        [MaxLength(20)]
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
        //[ForeignKey(typeof(Role)), MaxLength(10)]
        [MaxLength(20)]
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
        /// Gets or sets Status (1 = Active, 0 = Inactive, etc..)
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

        public static List<User> Gets(SQLiteConnection db)
        {
            if (null == db) return new List<User>();
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM User ";
                var insts = NQuery.Query<User>(cmd);
                return insts;
            }
        }
        public static List<User> Gets()
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Gets(db);
            }
        }

        public static User Get(SQLiteConnection db, string userId)
        {
            if (null == db) return null;
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM User ";
                cmd += " WHERE UserId = ? ";
                return NQuery.Query<User>(cmd, userId).FirstOrDefault();
            }
        }
        public static User Get(string userId)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Get(db, userId);
            }
        }

        public static List<User> FindByRole(string roleId)
        {
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM User ";
                cmd += " WHERE RoleId = ?";
                return NQuery.Query<User>(cmd, roleId);
            }
        }

        public static List<User> FindByRole(string roleId, int status)
        {
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM User ";
                cmd += " WHERE RoleId = ? ";
                cmd += "   AND Status = ? ";
                return NQuery.Query<User>(cmd, roleId, status);
            }
        }

        /// <summary>
        /// Gets by UserId and password.
        /// </summary>
        /// <param name="UserId">The UserId.</param>
        /// /// <param name="password">The password.</param>
        /// <returns>Returns found record.</returns>
        public static User GetByUserId(string UserId, string password)
        {
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM User ";
                cmd += " WHERE UserId = ? ";
                cmd += "   AND Password = ? ";
                return NQuery.Query<User>(cmd, UserId, password).FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets by CardId
        /// </summary>
        /// <param name="cardId">The cardId.</param>
        /// <returns>Returns found record.</returns>
        public static User GetByCardId(string cardId)
        {
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM User ";
                cmd += " WHERE CardId = ? ";
                return NQuery.Query<User>(cmd, cardId).FirstOrDefault();
            }
        }

        #endregion
    }

    #endregion
}
