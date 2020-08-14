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
		/// Gets or sets User Id.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets User Id.")]
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
		/// Gets or sets Prefix EN.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets Prefix EN.")]
		[MaxLength(20)]
		[PeropertyMapName("PrefixEN")]
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
		[PeropertyMapName("FirstNameEN")]
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
		[PeropertyMapName("MiddleNameEN")]
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
		[PeropertyMapName("LastNameEN")]
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
		[PeropertyMapName("FullNameEN")]
		public string FullNameEN
		{
			get
			{
				return string.Format("{0} {1} {2} {3}", 
					_PrefixEN, _FirstNameEN, _MiddleNameEN, _LastNameEN).Trim();
			}
			set { }
		}

		/// <summary>
		/// Gets or sets Prefix TH.
		/// </summary>
		[Category("User")]
		[Description("Gets or sets Prefix TH.")]
		[MaxLength(20)]
		[PeropertyMapName("PrefixTH")]
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
		[PeropertyMapName("FirstNameTH")]
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
		[PeropertyMapName("MiddleNameTH")]
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
		[PeropertyMapName("LastNameTH")]
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
		[PeropertyMapName("FullNameTH")]
		public string FullNameTH
		{
			get
			{
				return string.Format("{0} {1} {2} {3}",
					_PrefixTH, _FirstNameTH, _MiddleNameTH, _LastNameTH).Trim();
			}
			set { }
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
		/// Gets or sets Role Id.
		/// </summary>
		//[ForeignKey(typeof(Role)), MaxLength(10)]
		[Category("Role")]
		[Description("Gets or sets Role Id.")]
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
		/// Gets or sets Role Name EN.
		/// </summary>
		[Category("Role")]
		[Description("Gets or sets Role Name EN.")]
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
		/// Gets or sets Role Name TH
		/// </summary>
		[Category("Role")]
		[Description("Gets or sets Role Name TH.")]
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

		public class FKs : User, IFKs<User>
		{
			#region Role

			/// <summary>
			/// Gets or sets Role Name EN.
			/// </summary>
			[MaxLength(50)]
			[PeropertyMapName("RoleNameEN")]
			public override string RoleNameEN
			{
				get { return base.RoleNameEN; }
				set { base.RoleNameEN = value; }
			}
			/// <summary>
			/// Gets or sets Role Name TH.
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
			/*
			public User ToUser()
			{
				User inst = new User();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}
			*/
			#endregion
		}

		#endregion

		#region Static Methods

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
					/*
					var results = new List<User>();
					if (null != rets)
					{
						rets.ForEach(ret =>
						{
							results.Add(ret.ToModel());
						});
					}
					*/
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

		public static NDbResult<List<User>> GetUsers()
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return GetUsers(db);
			}
		}

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

		public static NDbResult<User> GetUser(string userId)
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return GetUser(db, userId);
			}
		}

		public static NDbResult<List<User>> SearchById(SQLiteConnection db, string userId)
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

					var rets = NQuery.Query<FKs>(cmd, "%" + userId + "%").ToList();
					var results = rets.ToModels();
					/*
					var results = new List<User>();
					if (null != rets)
					{
						rets.ForEach(ret =>
						{
							results.Add(ret.ToModel());
						});
					}
					*/
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

		public static NDbResult<List<User>> SearchById(string userId)
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return SearchById(db, userId);
			}
		}

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
					/*
					var results = new List<User>();
					if (null != rets)
					{
						rets.ForEach(ret =>
						{
							results.Add(ret.ToModel());
						});
					}
					*/
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
					/*
					var results = new List<User>();
					if (null != rets)
					{
						rets.ForEach(ret =>
						{
							results.Add(ret.ToModel());
						});
					}
					*/
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
					/*
					var results = new List<User>();
					if (null != rets)
					{
						rets.ForEach(ret =>
						{
							results.Add(ret.ToModel());
						});
					}
					*/
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
		/// /// <param name="password">The password.</param>
		/// <returns>Returns found record.</returns>
		public static NDbResult<User> GetByUserId(string userId, string password)
		{
			// TODO: MD5 password required for login and save.
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
		/// Gets by CardId
		/// </summary>
		/// <param name="cardId">The cardId.</param>
		/// <returns>Returns found record.</returns>
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

		public static NDbResult<User> SaveUser(User value)
		{
			// TODO: MD5 password required for login and save.
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
