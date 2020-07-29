#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using NLib;
using NLib.Design;
using NLib.Reflection;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
// required for JsonIgnore attribute.
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

#endregion

namespace DMT.Models
{
	#region User

	/// <summary>
	/// The User Data Model Class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	//[Table("User")]
	public class User : NTable<User>
	{
		#region Intenral Variables

		private string _UserId = string.Empty;
		private string _FullNameEN = string.Empty;
		private string _FullNameTH = string.Empty;
		private string _UserName = string.Empty;
		private string _Password = string.Empty;
		private string _PasswordMD5 = string.Empty;
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
		[Category("User")]
		[Description("Gets or sets UserId")]
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
		[Category("User")]
		[Description("Gets or sets FullNameEN")]
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
		[Category("User")]
		[Description("Gets or sets FullNameTH")]
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
		[Category("User")]
		[Description("Gets or sets UserName")]
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
		[Category("User")]
		[Description("Gets or sets Password")]
		[MaxLength(50)]
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
		/// Gets or sets MD5 Hash
		/// </summary>
		[Category("User")]
		[Description("Gets or sets MD5 Hash")]
		[MaxLength(50)]
		[PeropertyMapName("PasswordMD5")]
		public string PasswordMD5
		{
			get
			{
				return _PasswordMD5;
			}
			set
			{
				if (_PasswordMD5 != value)
				{
					_PasswordMD5 = value;
					this.RaiseChanged("PasswordMD5");
				}
			}
		}
		/// <summary>
		/// Gets or sets CardId
		/// </summary>
		[Category("User")]
		[Description("Gets or sets CardId")]
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
		[Category("Role")]
		[Description("Gets or sets RoleId")]
		[ReadOnly(true)]
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
		[Category("Role")]
		[Description("Gets or sets RoleNameEN")]
		[ReadOnly(true)]
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
		[Category("Role")]
		[Description("Gets or sets RoleNameTH")]
		[ReadOnly(true)]
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
		[Category("DataCenter")]
		[Description("Gets or sets Status (1 = Sync, 0 = Unsync, etc..)")]
		[ReadOnly(true)]
		[PeropertyMapName("Status")]
		[PropertyOrder(10001)]
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
		[PropertyOrder(10002)]
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

		public class FKs : User
		{
			#region Role

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

			#endregion

			#region Public Methods

			public User ToUser()
			{
				User inst = new User();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}

			#endregion
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

				var rets = NQuery.Query<FKs>(cmd).ToList();
				var results = new List<User>();
				if (null != rets)
				{
					rets.ForEach(ret =>
					{
						results.Add(ret.ToUser());
					});
				}

				return results;
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
				cmd += "   AND User.UserId = ? ";

				var ret = NQuery.Query<FKs>(cmd, userId).FirstOrDefault();
				return (null != ret) ? ret.ToUser() : null;
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

				var rets = NQuery.Query<FKs>(cmd, roleId).ToList();
				var results = new List<User>();
				if (null != rets)
				{
					rets.ForEach(ret =>
					{
						results.Add(ret.ToUser());
					});
				}

				return results;
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

				var rets = NQuery.Query<FKs>(cmd, roleId, status).ToList();
				var results = new List<User>();
				if (null != rets)
				{
					rets.ForEach(ret =>
					{
						results.Add(ret.ToUser());
					});
				}

				return results;
			}
		}

		/// <summary>
		/// Gets by UserId and password.
		/// </summary>
		/// <param name="userId">The UserId.</param>
		/// /// <param name="password">The password.</param>
		/// <returns>Returns found record.</returns>
		public static User GetByUserId(string userId, string password)
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

				var ret = NQuery.Query<FKs>(cmd, userId, password).FirstOrDefault();
				return (null != ret) ? ret.ToUser() : null;
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

				var ret = NQuery.Query<FKs>(cmd, cardId).FirstOrDefault();
				return (null != ret) ? ret.ToUser() : null;
			}
		}

		#endregion
	}

	#endregion
}
