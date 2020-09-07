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
using System.Reflection;

#endregion

namespace DMT.Models
{
	#region User

	/// <summary>
	/// The User Data Model Class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("User")]
	public class User : NTable<User>
	{
		#region Enum

		/// <summary>
		/// The Account Flags enum.
		/// </summary>
		public enum AccountFlags : int
		{
			/// <summary>
			/// Account still valid.
			/// </summary>
			Valid = 0,
			/// <summary>
			/// Account is invalid.
			/// </summary>
			Invalid = 1
		}

		#endregion

		#region Intenral Variables

		private string _UserId = string.Empty;

		private string _PrefixEN = string.Empty;
		private string _PrefixTH = string.Empty;

		private string _FirstNameEN = string.Empty;
		private string _FirstNameTH = string.Empty;

		private string _MiddleNameEN = string.Empty;
		private string _MiddleNameTH = string.Empty;

		private string _LastNameEN = string.Empty;
		private string _LastNameTH = string.Empty;

		private string _Password = string.Empty;
		private string _CardId = string.Empty;

		private string _RoleId = string.Empty;
		private string _RoleNameEN = string.Empty;
		private string _RoleNameTH = string.Empty;
		// Expiration
		private DateTime _PasswordDate = DateTime.MinValue;
		private int _ExpireDays = 0;
		private AccountFlags _AccountStatus = AccountFlags.Valid;
		// Validation (runtime)
		private string _NewPassword = string.Empty;
		private string _ConfirmPassword = string.Empty;

		private int _Status = 1;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public User() : base() { }

		#endregion

		#region Private Methods

		private void ApplyNewPassword()
		{
			if (string.IsNullOrEmpty(_NewPassword) || string.IsNullOrEmpty(_ConfirmPassword))
				return;
			if (string.CompareOrdinal(_NewPassword, _ConfirmPassword) == 0)
			{
				_Password = Utils.MD5.Encrypt(_NewPassword);
				_NewPassword = string.Empty;
				_ConfirmPassword = string.Empty;
				// Raise event.
				this.RaiseChanged("Password");
				this.RaiseChanged("NewPassword");
				this.RaiseChanged("ConfirmPassword");
			}
		}

		#endregion

		#region Public Proprties

		#region Common

		/// <summary>
		/// Gets or sets User Id.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets User Id.")]
		[PrimaryKey, MaxLength(10)]
		[PropertyMapName("UserId")]
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
		/// Gets or sets Prefix EN.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets Prefix EN.")]
		[MaxLength(20)]
		[PropertyMapName("PrefixEN")]
		public string PrefixEN
		{
			get { return _PrefixEN; }
			set
			{
				if (_PrefixEN != value)
				{
					_PrefixEN = value;
					this.RaiseChanged("PrefixEN");
					this.RaiseChanged("FullNameEN");
				}
			}
		}
		/// <summary>
		/// Gets or sets First Name EN.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets First Name EN")]
		[MaxLength(50)]
		[PropertyMapName("FirstNameEN")]
		public string FirstNameEN
		{
			get { return _FirstNameEN; }
			set
			{
				if (_FirstNameEN != value)
				{
					_FirstNameEN = value;
					this.RaiseChanged("FirstNameEN");
					this.RaiseChanged("FullNameEN");
				}
			}
		}
		/// <summary>
		/// Gets or sets Middle Name EN.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets Middle Name EN")]
		[MaxLength(20)]
		[PropertyMapName("MiddleNameEN")]
		public string MiddleNameEN
		{
			get { return _MiddleNameEN; }
			set
			{
				if (_MiddleNameEN != value)
				{
					_MiddleNameEN = value;
					this.RaiseChanged("MiddleNameEN");
					this.RaiseChanged("FullNameEN");
				}
			}
		}
		/// <summary>
		/// Gets or sets Last Name EN.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets Last Name EN")]
		[MaxLength(50)]
		[PropertyMapName("LastNameEN")]
		public string LastNameEN
		{
			get { return _LastNameEN; }
			set
			{
				if (_LastNameEN != value)
				{
					_LastNameEN = value;
					this.RaiseChanged("LastNameEN");
					this.RaiseChanged("FullNameEN");
				}
			}
		}
		/// <summary>
		/// Gets or sets FullName EN.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets FullName EN.")]
		[ReadOnly(true)]
		[Ignore]
		[PropertyMapName("FullNameEN")]
		public string FullNameEN
		{
			get
			{
				string ret = string.Empty;
				if (!string.IsNullOrEmpty(_PrefixEN)) ret += _PrefixEN + " ";
				if (!string.IsNullOrEmpty(_FirstNameEN)) ret += _FirstNameEN + " ";
				if (!string.IsNullOrEmpty(_MiddleNameEN)) ret += _MiddleNameEN + " ";
				if (!string.IsNullOrEmpty(_LastNameEN)) ret += _LastNameEN + " ";
				return ret.Trim();
			}
			set { }
		}

		/// <summary>
		/// Gets or sets Prefix TH.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets Prefix TH.")]
		[MaxLength(20)]
		[PropertyMapName("PrefixTH")]
		public string PrefixTH
		{
			get { return _PrefixTH; }
			set
			{
				if (_PrefixTH != value)
				{
					_PrefixTH = value;
					this.RaiseChanged("PrefixTH");
					this.RaiseChanged("FullNameTH");
				}
			}
		}
		/// <summary>
		/// Gets or sets First Name TH.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets First Name TH")]
		[MaxLength(50)]
		[PropertyMapName("FirstNameTH")]
		public string FirstNameTH
		{
			get { return _FirstNameTH; }
			set
			{
				if (_FirstNameTH != value)
				{
					_FirstNameTH = value;
					this.RaiseChanged("FirstNameTH");
					this.RaiseChanged("FullNameTH");
				}
			}
		}
		/// <summary>
		/// Gets or sets Middle Name TH.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets Middle Name TH")]
		[MaxLength(20)]
		[PropertyMapName("MiddleNameTH")]
		public string MiddleNameTH
		{
			get { return _MiddleNameTH; }
			set
			{
				if (_MiddleNameTH != value)
				{
					_MiddleNameTH = value;
					this.RaiseChanged("MiddleNameTH");
					this.RaiseChanged("FullNameTH");
				}
			}
		}
		/// <summary>
		/// Gets or sets Last Name TH.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets Last Name TH")]
		[MaxLength(50)]
		[PropertyMapName("LastNameTH")]
		public string LastNameTH
		{
			get { return _LastNameTH; }
			set
			{
				if (_LastNameTH != value)
				{
					_LastNameTH = value;
					this.RaiseChanged("LastNameTH");
					this.RaiseChanged("FullNameTH");
				}
			}
		}
		/// <summary>
		/// Gets or sets FullName TH.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets FullName TH.")]
		[ReadOnly(true)]
		[Ignore]
		[PropertyMapName("FullNameTH")]
		public string FullNameTH
		{
			get
			{
				string ret = string.Empty;
				if (!string.IsNullOrEmpty(_PrefixTH)) ret += _PrefixTH + " ";
				if (!string.IsNullOrEmpty(_FirstNameTH)) ret += _FirstNameTH + " ";
				if (!string.IsNullOrEmpty(_MiddleNameTH)) ret += _MiddleNameTH + " ";
				if (!string.IsNullOrEmpty(_LastNameTH)) ret += _LastNameTH + " ";
				return ret.Trim();
			}
			set { }
		}
		/// <summary>
		/// Gets or sets Password
		/// </summary>
		[Category("User")]
		[Description("Gets or sets Password")]
		[MaxLength(50)]
		[ReadOnly(true)]
		[PropertyMapName("Password")]
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
		[Category("User")]
		[Description("Gets or sets CardId")]
		[MaxLength(20)]
		[PropertyMapName("CardId")]
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
		/// Gets or sets Role Id.
		/// </summary>
		//[ForeignKey(typeof(Role)), MaxLength(10)]
		[Category("Role")]
		[Description("Gets or sets Role Id.")]
		[ReadOnly(true)]
		[MaxLength(20)]
		[PropertyMapName("RoleId")]
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
		/// Gets or sets Role Name EN.
		/// </summary>
		[Category("Role")]
		[Description("Gets or sets Role Name EN.")]
		[ReadOnly(true)]
		[Ignore]
		[PropertyMapName("RoleNameEN")]
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
		/// Gets or sets Role Name TH
		/// </summary>
		[Category("Role")]
		[Description("Gets or sets Role Name TH.")]
		[ReadOnly(true)]
		[Ignore]
		[PropertyMapName("RoleNameTH")]
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

		#region Expiration

		/// <summary>
		/// Gets or sets Password Date.
		/// </summary>
		[Category("Expiration")]
		[Description("Gets or sets Password Date.")]
		[ReadOnly(true)]
		[PropertyMapName("PasswordDate")]
		public DateTime PasswordDate
		{
			get { return _PasswordDate; }
			set
			{
				if (_PasswordDate != value)
				{
					_PasswordDate = value;
					this.RaiseChanged("PasswordDate");
				}
			}
		}
		/// <summary>
		/// Gets or sets Expire Days.
		/// </summary>
		[Category("Expiration")]
		[Description("Gets or sets Expire Days.")]
		[ReadOnly(true)]
		[PropertyMapName("ExpireDays")]
		public int ExpireDays
		{
			get { return _ExpireDays; }
			set
			{
				if (_ExpireDays != value)
				{
					_ExpireDays = value;
					this.RaiseChanged("ExpireDays");
				}
			}
		}
		/// <summary>
		/// Gets or sets Account Status Flag.
		/// </summary>
		[Category("Expiration")]
		[Description("Gets or sets Account Status Flag.")]
		[ReadOnly(true)]
		[PropertyMapName("AccountStatus")]
		public AccountFlags AccountStatus
		{
			get { return _AccountStatus; }
			set
			{
				if (_AccountStatus != value)
				{
					_AccountStatus = value;
					this.RaiseChanged("AccountStatus");
				}
			}
		}

		#endregion

		#region Validation

		/// <summary>
		/// Gets or sets New Password.
		/// </summary>
		[Category("Validation - Runtime")]
		[Description("Gets or sets New Password.")]
		[Ignore]
		[JsonIgnore]
		[PropertyMapName("NewPassword")]
		public string NewPassword
		{
			get
			{
				return _NewPassword;
			}
			set
			{
				if (_NewPassword != value)
				{
					_NewPassword = value;
					ApplyNewPassword();
				}
			}
		}
		/// <summary>
		/// Gets or sets Confirm Password.
		/// </summary>
		[Category("Validation - Runtime")]
		[Description("Gets or sets Comfirm Password.")]
		[Ignore]
		[JsonIgnore]
		[PropertyMapName("ConfirmPassword")]
		public string ConfirmPassword
		{
			get
			{
				return _ConfirmPassword;
			}
			set
			{
				if (_ConfirmPassword != value)
				{
					_ConfirmPassword = value;
					ApplyNewPassword();
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
		[PropertyMapName("Status", typeof(User))]
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
		[PropertyMapName("LastUpdate", typeof(User))]
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

		/// <summary>
		/// The internal FKs class for query data.
		/// </summary>
		public class FKs : User, IFKs<User>
		{
			#region Role

			/// <summary>
			/// Gets or sets Role Name EN.
			/// </summary>
			[MaxLength(50)]
			[PropertyMapName("RoleNameEN")]
			public override string RoleNameEN
			{
				get { return base.RoleNameEN; }
				set { base.RoleNameEN = value; }
			}
			/// <summary>
			/// Gets or sets Role Name TH.
			/// </summary>
			[MaxLength(50)]
			[PropertyMapName("RoleNameTH")]
			public override string RoleNameTH
			{
				get { return base.RoleNameTH; }
				set { base.RoleNameTH = value; }
			}

			#endregion
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Gets Users.
		/// </summary>
		/// <returns>Returns List of User.</returns>
		public static NDbResult<List<User>> GetUsers()
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return GetUsers(db);
			}
		}
		/// <summary>
		/// Gets Users.
		/// </summary>
		/// <param name="db">The database connection.</param>
		/// <returns>Returns List of User.</returns>
		public static NDbResult<List<User>> GetUsers(SQLiteConnection db)
		{
			var result = new NDbResult<List<User>>();
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM UserView ";

					var rets = NQuery.Query<FKs>(cmd).ToList();
					var results = rets.ToModels();
					result.Success(results);
				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}
		/// <summary>
		/// Get User by User Id (Exact match).
		/// </summary>
		/// <param name="userId">The User Id.</param>
		/// <returns>Returns User instance.</returns>
		public static NDbResult<User> GetUser(string userId)
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return GetUser(db, userId);
			}
		}
		/// <summary>
		/// Get User by User Id (Exact match).
		/// </summary>
		/// <param name="db">The database connection.</param>
		/// <param name="userId">The User Id.</param>
		/// <returns>Returns User instance.</returns>
		public static NDbResult<User> GetUser(SQLiteConnection db, string userId)
		{
			var result = new NDbResult<User>();
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM UserView ";
					cmd += " WHERE UserId = ? ";

					var ret = NQuery.Query<FKs>(cmd, userId).FirstOrDefault();
					var data = (null != ret) ? ret.ToModel() : null;
					result.Success(data);
				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}
		/// <summary>
		/// Search By User Id (with SQL Like filter).
		/// </summary>
		/// <param name="userId">The User Id.</param>
		/// <param name="roles">The roles Id list.</param>
		/// <returns>Returns List of User.</returns>
		public static NDbResult<List<User>> SearchById(string userId, 
			string[] roles)
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return SearchById(db, userId, roles);
			}
		}
		/// <summary>
		/// Search By User Id (with SQL Like filter).
		/// </summary>
		/// <param name="db">The database connection.</param>
		/// <param name="userId">The User Id.</param>
		/// <param name="roles">The roles Id list.</param>
		/// <returns>Returns List of User.</returns>
		public static NDbResult<List<User>> SearchById(SQLiteConnection db, 
			string userId, string[] roles)
		{
			var result = new NDbResult<List<User>>();
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM UserView ";
					cmd += " WHERE UserId like ? ";
					if (null != roles && roles.Length > 0)
					{
						cmd += "   AND RoleId IN ( ";
						for (int i = 0; i < roles.Length; i++)
						{
							cmd += string.Format("'{0}'", roles[i]);
							if (i < roles.Length - 1)
							{
								cmd += ", ";
							}
						}
						cmd += "                 ) ";
					}
					var rets = NQuery.Query<FKs>(cmd, "%" + userId + "%").ToList();
					var results = rets.ToModels();
					result.Success(results);
				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}
		/// <summary>
		/// Find Users by Role Id.
		/// </summary>
		/// <param name="roleId">The Role Id.</param>
		/// <returns>Returns List of User.</returns>
		public static NDbResult<List<User>> FindByRole(string roleId)
		{
			var result = new NDbResult<List<User>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM UserView ";
					cmd += " WHERE RoleId = ? ";

					var rets = NQuery.Query<FKs>(cmd, roleId).ToList();
					var results = rets.ToModels();
					result.Success(results);
				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}
		/// <summary>
		/// Find Users by Role Id.
		/// </summary>
		/// <param name="roleId">The Role Id.</param>
		/// <param name="status">The Status.</param>
		/// <returns>Returns List of User.</returns>
		public static NDbResult<List<User>> FindByRole(string roleId, int status)
		{
			var result = new NDbResult<List<User>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM UserView ";
					cmd += " WHERE RoleId = ? ";
					cmd += "   AND Status = ? ";

					var rets = NQuery.Query<FKs>(cmd, roleId, status).ToList();
					var results = rets.ToModels();
					result.Success(results);
				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}
		/// <summary>
		/// Find Users by Group Id.
		/// </summary>
		/// <param name="groupId">The Group Id.</param>
		/// <param name="status">The status.</param>
		/// <returns>Returns List of User.</returns>
		public static NDbResult<List<User>> FindByGroupId(int groupId, int status)
		{
			var result = new NDbResult<List<User>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM UserView ";
					cmd += " WHERE GroupId = ? ";
					cmd += "   AND Status = ? ";

					var rets = NQuery.Query<FKs>(cmd, groupId, status).ToList();
					var results = rets.ToModels();
					result.Success(results);
				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}
		/// <summary>
		/// Gets by UserId and password.
		/// </summary>
		/// <param name="userId">The UserId.</param>
		/// /// <param name="password">The password in MD5.</param>
		/// <returns>Returns User instance.</returns>
		public static NDbResult<User> GetByUserId(string userId, string password)
		{
			var result = new NDbResult<User>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM UserView ";
					cmd += " WHERE UserId = ? ";
					cmd += "   AND Password = ? ";

					var ret = NQuery.Query<FKs>(cmd, userId, password).FirstOrDefault();
					var data = (null != ret) ? ret.ToModel() : null;
					result.Success(data);
				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}
		/// <summary>
		/// Gets by Card Id
		/// </summary>
		/// <param name="cardId">The cardId.</param>
		/// <returns>Returns User instance.</returns>
		public static NDbResult<User> GetByCardId(string cardId)
		{
			var result = new NDbResult<User>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM UserView ";
					cmd += " WHERE CardId = ? ";

					var ret = NQuery.Query<FKs>(cmd, cardId).FirstOrDefault();
					var data = (null != ret) ? ret.ToModel() : null;
					result.Success(data);
				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}
		/// <summary>
		/// Save User.
		/// </summary>
		/// <param name="value">The User instance.</param>
		/// <returns>Returns User instance.</returns>
		public static NDbResult<User> SaveUser(User value)
		{
			var result = new NDbResult<User>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					result = Save(value);

				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);
				}
				return result;
			}
		}

		#endregion
	}

	#endregion
}
