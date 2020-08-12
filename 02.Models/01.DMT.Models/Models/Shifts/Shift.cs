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
	#region Shift

	/// <summary>
	/// The Shift Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("Shift")]
	public class Shift : NTable<Shift>
	{
		#region Intenral Variables

		private int _ShiftId = 0;
		private string _ShiftNameTH = string.Empty;
		private string _ShiftNameEN = string.Empty;

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public Shift() : base() { }

		#endregion

		#region Public Proprties

		#region Common

		/// <summary>
		/// Gets or sets ShiftId.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets ShiftId.")]
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
		[Category("Common")]
		[Description("Gets or sets Name TH.")]
		[MaxLength(50)]
		[PeropertyMapName("ShiftNameTH")]
		public string ShiftNameTH
		{
			get
			{
				return _ShiftNameTH;
			}
			set
			{
				if (_ShiftNameTH != value)
				{
					_ShiftNameTH = value;
					this.RaiseChanged("ShiftNameTH");
				}
			}
		}
		/// <summary>
		/// Gets or sets Name EN.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Name EN.")]
		[MaxLength(50)]
		[PeropertyMapName("ShiftNameEN")]
		public string ShiftNameEN
		{
			get
			{
				return _ShiftNameEN;
			}
			set
			{
				if (_ShiftNameEN != value)
				{
					_ShiftNameEN = value;
					this.RaiseChanged("ShiftNameEN");
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

		public static NDbResult<List<Shift>> Gets(SQLiteConnection db)
		{
			var result = new NDbResult<List<Shift>>();

			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<Shift>();
				return result;
			}

			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * FROM Shift ";
					result.data = NQuery.Query<Shift>(cmd);
					result.Success();
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = new List<Shift>();
				}
				return result;
			}
		}

		public static NDbResult<List<Shift>> Gets()
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return Gets(db);
			}
		}

		public static NDbResult<Shift> Get(SQLiteConnection db, string shiftId)
		{
			var result = new NDbResult<Shift>();

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
					cmd += "SELECT * FROM Shift ";
					cmd += " WHERE ShiftId = ? ";
					result.data = NQuery.Query<Shift>(cmd, shiftId).FirstOrDefault();
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

		public static NDbResult<Shift> Get(string shiftId)
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return Get(db, shiftId);
			}
		}

		#endregion
	}

	#endregion
}
