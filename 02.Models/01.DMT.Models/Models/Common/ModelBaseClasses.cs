#region Using

using System;
using System.ComponentModel;
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
	#region DataCenterModelBase (abstract)

	/// <summary>
	/// DataCenterModelBase (abstract).
	/// </summary>
	/// <typeparam name="T">The Target Class.</typeparam>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	public abstract class DataCenterModelBase<T> : NTable<T>
		where T : NTable, new()
	{
		#region Intenral Variables

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Public Proprties

		/// <summary>
		/// Gets or sets Status (1 = Sync, 0 = Unsync, etc..)
		/// </summary>
		[Category("DataCenter")]
		[Description("Gets or sets Status (1 = Sync, 0 = Unsync, etc..)")]
		[ReadOnly(true)]
		[PeropertyMapName("Status")]
		[PropertyOrder(101)]
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
		[PropertyOrder(102)]
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
	}

	#endregion

	#region TSBModelBase (abstract)

	/// <summary>
	/// TSBModelBase (abstract).
	/// </summary>
	/// <typeparam name="T">The Target Class.</typeparam>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	public abstract class TSBModelBase<T> : DataCenterModelBase<T>
		where T : NTable, new()
	{
		#region Intenral Variables

		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;
		private string _NetworkId = string.Empty;
		private bool _Active = false;

		#endregion

		#region Public Proprties

		/// <summary>
		/// Gets or sets TSBId.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets TSBId.")]
		[PrimaryKey, MaxLength(10)]
		[PeropertyMapName("TSBId")]
		[PropertyOrder(101)]
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
		[PropertyOrder(102)]
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
		[PropertyOrder(103)]
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
		[PropertyOrder(104)]
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
		/// Gets or sets is active TSB.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets is active TSB.")]
		[ReadOnly(true)]
		[PeropertyMapName("Active")]
		[PropertyOrder(105)]
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
	}

	#endregion
}
