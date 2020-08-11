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
	#region TSB

	/// <summary>
	/// The TSB Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("TSB")]
	public class TSB : NTable<TSB>
	{
		#region Intenral Variables

		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;
		private string _NetworkId = string.Empty;

		private decimal _MaxCredit = decimal.Zero;

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

		#region Common

		/// <summary>
		/// Gets or sets TSBId.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets TSBId.")]
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
		[Category("Common")]
		[Description("Gets or sets NetworkId.")]
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
		[Category("Common")]
		[Description("Gets or sets TSBNameEN.")]
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
		[Category("Common")]
		[Description("Gets or sets TSBNameTH.")]
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
		/// <summary>
		/// Gets or sets Max TSB Credit.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Max TSB Credit.")]
		[PeropertyMapName("MaxCredit")]
		public decimal MaxCredit
		{
			get
			{
				return _MaxCredit;
			}
			set
			{
				if (_MaxCredit != value)
				{
					_MaxCredit = value;
					this.RaiseChanged("MaxCredit");
				}
			}
		}
		/// <summary>
		/// Gets or sets is active TSB.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets is active TSB.")]
		[ReadOnly(true)]
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

		public static NDbResult<List<TSB>> Gets(SQLiteConnection db)
		{
			var result = new NDbResult<List<TSB>>();

			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<TSB>();
				return result;
			}
			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * FROM TSB ";
					result.Success();
					result.data = NQuery.Query<TSB>(cmd);
					result.Success();
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = new List<TSB>();

				}
				return result;
			}
		}
		public static NDbResult<List<TSB>> Gets()
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return Gets(db);
			}
		}
		public static NDbResult<TSB> Get(SQLiteConnection db, string tsbId)
		{
			var result = new NDbResult<TSB>();

			if (null == db)
			{
				result.ConenctFailed();
				result.data = null;
				return result;
			}
			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * FROM TSB ";
					cmd += " WHERE TSBId = ? ";
					result.data = NQuery.Query<TSB>(cmd, tsbId).FirstOrDefault();
					result.Success();
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = null;
				}
				return result;
			}
		}
		public static NDbResult<TSB> Get(string tsbId)
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return Get(db, tsbId);
			}
		}
		/// <summary>
		/// Gets Active TSB.
		/// </summary>
		/// <returns>Returns Active TSB instance.</returns>
		public static NDbResult<TSB> GetCurrent()
		{
			var result = new NDbResult<TSB>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = null;
				return result;
			}
			lock (sync)
			{
				try
				{
					// inactive all TSBs
					string cmd = string.Empty;
					cmd += "SELECT * FROM TSB ";
					cmd += " WHERE Active = 1 ";
					var results = NQuery.Query<TSB>(cmd);
					result.data = (null != results) ? results.FirstOrDefault() : null;
					result.Success();
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = null;
				}
				return result;
			}
		}
		public static NDbResult SetActive(string tsbId)
		{
			var result = new NDbResult();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				return result;
			}
			lock (sync)
			{
				try
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
					result.Success();
				}
				catch (Exception ex)
				{
					result.Error(ex);
				}
				return result;
			}
		}

		#endregion
	}

	#endregion
}
