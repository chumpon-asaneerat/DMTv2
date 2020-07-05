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
        private string _RoleNameEN = string.Empty;
        private string _RoleNameTH = string.Empty;

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

        #region Common

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

        #endregion

        #region Role

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
        /// Gets or sets RoleNameEN
        /// </summary>
        [Ignore]
        [PeropertyMapName("RoleNameEN")]
        public virtual string RoleNameEN
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
        [Ignore]
        [PeropertyMapName("RoleNameTH")]
        public virtual string RoleNameTH
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

        #endregion

        #region Status (DC)

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

        #endregion

        #region Internal Class

        internal class FKs : User
        {
            /// <summary>
            /// Gets or sets RoleNameEN
            /// </summary>
            [MaxLength(50)]
            [PeropertyMapName("RoleNameEN")]
            public override string RoleNameEN
            {
                get { return base.RoleNameEN; }
                set { base.RoleNameEN = value; }
            }
            /// <summary>
            /// Gets or sets RoleNameTH
            /// </summary>
            [MaxLength(50)]
            [PeropertyMapName("RoleNameTH")]
            public override string RoleNameTH
            {
                get { return base.RoleNameTH; }
                set { base.RoleNameTH = value; }
            }
        }

        #endregion

        #region Static Methods

        public static List<User> Gets(SQLiteConnection db)
        {
            List<User> insts = new List<User>();
            if (null == db) return insts;
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT User.* ";
                cmd += "     , Role.RoleNameEN, Role.RoleNameTH ";
                cmd += "  FROM User, Role ";
                cmd += " WHERE User.RoleId = Role.RoleId ";
                return NQuery.Query<FKs>(cmd).ToList<User>();
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
                cmd += "SELECT User.* ";
                cmd += "     , Role.RoleNameEN, Role.RoleNameTH ";
                cmd += "  FROM User, Role ";
                cmd += " WHERE User.RoleId = Role.RoleId ";
                return NQuery.Query<FKs>(cmd, userId).FirstOrDefault<User>();
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
                cmd += "SELECT User.* ";
                cmd += "     , Role.RoleNameEN, Role.RoleNameTH ";
                cmd += "  FROM User, Role ";
                cmd += " WHERE User.RoleId = Role.RoleId ";
                cmd += "   AND User.RoleId = ? ";
                return NQuery.Query<FKs>(cmd, roleId).ToList<User>();
            }
        }

        public static List<User> FindByRole(string roleId, int status)
        {
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT User.* ";
                cmd += "     , Role.RoleNameEN, Role.RoleNameTH ";
                cmd += "  FROM User, Role ";
                cmd += " WHERE User.RoleId = Role.RoleId ";
                cmd += "   AND User.RoleId = ? ";
                cmd += "   AND User.Status = ? ";
                return NQuery.Query<FKs>(cmd, roleId, status).ToList<User>();
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
                cmd += "SELECT User.* ";
                cmd += "     , Role.RoleNameEN, Role.RoleNameTH ";
                cmd += "  FROM User, Role ";
                cmd += " WHERE User.RoleId = Role.RoleId ";
                cmd += "   AND User.UserId = ? ";
                cmd += "   AND User.Password = ? ";
                return NQuery.Query<FKs>(cmd, UserId, password).FirstOrDefault<User>();
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
                cmd += "SELECT User.* ";
                cmd += "     , Role.RoleNameEN, Role.RoleNameTH ";
                cmd += "  FROM User, Role ";
                cmd += " WHERE User.RoleId = Role.RoleId ";
                cmd += "   AND CardId = ? ";
                return NQuery.Query<FKs>(cmd, cardId).FirstOrDefault<User>();
            }
        }

        #endregion
    }

    #endregion
}
