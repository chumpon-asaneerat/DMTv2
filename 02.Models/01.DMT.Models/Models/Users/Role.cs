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
	#region Role

	/// <summary>
	/// The Role Data Model Class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("Role")]
	public class Role : NTable<Role>
	{
		#region Intenral Variables

		private string _RoleId = string.Empty;
		private int _GroupId = 0;
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

		#region Common

		/// <summary>
		/// Gets or sets RoleId
		/// </summary>
		[Category("Role")]
		[Description("Gets or sets RoleId")]
		[ReadOnly(true)]
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
		/// Gets or sets Group Id.
		/// </summary>
		[Category("Role")]
		[Description("Gets or sets Group Id.")]
		[PeropertyMapName("GroupId")]
		public int GroupId
		{
			get
			{
				return _GroupId;
			}
			set
			{
				if (_GroupId != value)
				{
					_GroupId = value;
					this.RaiseChanged("GroupId");
				}
			}
		}
		/// <summary>
		/// Gets or sets RoleNameEN
		/// </summary>
		[Category("Role")]
		[Description("Gets or sets RoleNameEN")]
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
		[Category("Role")]
		[Description("Gets or sets RoleNameTH")]
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

		#endregion

		#region Status (DC)

		/// <summary>
		/// Gets or sets Status (1 = Sync, 0 = Unsync, etc..)
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

		#region Static Methods

		/// <summary>
		/// Gets Roles.
		/// </summary>
		/// <param name="db">The database connection.</param>
		/// <returns>Returns List of Role.</returns>
		public static NDbResult<List<Role>> GetRoles(SQLiteConnection db)
		{
			var result = new NDbResult<List<Role>>();
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
					cmd += "SELECT * FROM Role ";
					var results = NQuery.Query<Role>(cmd);
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
		/// Gets Roles.
		/// </summary>
		/// <returns>Returns List of Role.</returns>
		public static NDbResult<List<Role>> GetRoles()
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return GetRoles(db);
			}
		}
		/// <summary>
		/// Gets Role.
		/// </summary>
		/// <param name="db">The database connection.</param>
		/// <param name="roleId">The Role Id.</param>
		/// <returns>Returns Role instance.</returns>
		public static NDbResult<Role> GetRole(SQLiteConnection db, string roleId)
		{
			var result = new NDbResult<Role>();
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
					cmd += "SELECT * FROM Role ";
					cmd += " WHERE RoleId = ? ";
					var results = NQuery.Query<Role>(cmd, roleId).FirstOrDefault();
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
		/// Gets Role.
		/// </summary>
		/// <param name="roleId">The Role Id.</param>
		/// <returns>Returns Role instance.</returns>
		public static NDbResult<Role> GetRole(string roleId)
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return GetRole(db, roleId);
			}
		}

		#endregion
	}

	#endregion
}
