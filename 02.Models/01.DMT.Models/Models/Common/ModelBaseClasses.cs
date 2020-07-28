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
		[MaxLength(10)]
		[PeropertyMapName("TSBId")]
		[PropertyOrder(101)]
		public virtual string TSBId
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
		[Ignore]
		[PropertyOrder(102)]
		public virtual string NetworkId
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
		[Ignore]
		[PropertyOrder(103)]
		public virtual string TSBNameEN
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
		[Ignore]
		[PropertyOrder(104)]
		public virtual string TSBNameTH
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
		[Ignore]
		[PropertyOrder(105)]
		public virtual bool Active
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

	#region PlazaGroupModelBase (abstract)

	/// <summary>
	/// PlazaGroupModelBase (abstract).
	/// </summary>
	/// <typeparam name="T">The Target Class.</typeparam>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	public abstract class PlazaGroupModelBase<T> : TSBModelBase<T>
		where T : NTable, new()
	{
		#region Intenral Variables

		private string _PlazaGroupId = string.Empty;
		private string _PlazaGroupNameEN = string.Empty;
		private string _PlazaGroupNameTH = string.Empty;
		private string _Direction = string.Empty;

		#endregion

		#region Public Proprties

		/// <summary>
		/// Gets or sets PlazaId.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets PlazaId.")]
		[MaxLength(10)]
		[PeropertyMapName("PlazaGroupId")]
		[PropertyOrder(201)]
		public virtual string PlazaGroupId
		{
			get
			{
				return _PlazaGroupId;
			}
			set
			{
				if (_PlazaGroupId != value)
				{
					_PlazaGroupId = value;
					this.RaiseChanged("PlazaGroupId");
				}
			}
		}
		/// <summary>
		/// Gets or sets PlazaGroupNameEN
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets PlazaGroupNameEN")]
		[MaxLength(100)]
		[Ignore]
		[PeropertyMapName("PlazaGroupNameEN")]
		[PropertyOrder(202)]
		public virtual string PlazaGroupNameEN
		{
			get
			{
				return _PlazaGroupNameEN;
			}
			set
			{
				if (_PlazaGroupNameEN != value)
				{
					_PlazaGroupNameEN = value;
					this.RaiseChanged("PlazaGroupNameEN");
				}
			}
		}
		/// <summary>
		/// Gets or sets PlazaGroupNameTH
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets PlazaGroupNameTH")]
		[MaxLength(100)]
		[Ignore]
		[PeropertyMapName("PlazaGroupNameTH")]
		[PropertyOrder(203)]
		public virtual string PlazaGroupNameTH
		{
			get
			{
				return _PlazaGroupNameTH;
			}
			set
			{
				if (_PlazaGroupNameTH != value)
				{
					_PlazaGroupNameTH = value;
					this.RaiseChanged("PlazaGroupNameTH");
				}
			}
		}
		/// <summary>
		/// Gets or sets Direction
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets Direction")]
		[MaxLength(10)]
		[Ignore]
		[PeropertyMapName("Direction")]
		[PropertyOrder(204)]
		public virtual string Direction
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

		#endregion
	}

	#endregion

	#region PlazaModelBase (abstract)

	/// <summary>
	/// PlazaModelBase (abstract).
	/// </summary>
	/// <typeparam name="T">The Target Class.</typeparam>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	public abstract class PlazaModelBase<T> : PlazaGroupModelBase<T>
		where T : NTable, new()
	{
		#region Intenral Variables

		private string _PlazaId = string.Empty;
		private string _PlazaNameEN = string.Empty;
		private string _PlazaNameTH = string.Empty;

		#endregion

		#region Public Proprties

		/// <summary>
		/// Gets or sets PlazaId.
		/// </summary>
		[Category("Plaza")]
		[Description("Gets or sets PlazaId.")]
		[MaxLength(10)]
		[PeropertyMapName("PlazaId")]
		[PropertyOrder(301)]
		public virtual string PlazaId
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
		/// Gets or sets PlazaNameEN
		/// </summary>
		[Category("Plaza")]
		[Description("Gets or sets PlazaNameEN")]
		[MaxLength(100)]
		[Ignore]
		[PeropertyMapName("PlazaNameEN")]
		[PropertyOrder(302)]
		public virtual string PlazaNameEN
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
		[Category("Plaza")]
		[Description("Gets or sets PlazaNameTH")]
		[MaxLength(100)]
		[Ignore]
		[PeropertyMapName("PlazaNameTH")]
		[PropertyOrder(303)]
		public virtual string PlazaNameTH
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

		#endregion
	}

	#endregion

	#region LaneModelBase (abstract)

	/// <summary>
	/// LaneModelBase (abstract).
	/// </summary>
	/// <typeparam name="T">The Target Class.</typeparam>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	public abstract class LaneModelBase<T> : PlazaModelBase<T>
		where T : NTable, new()
	{
		#region Intenral Variables

		private int _PkId = 0;
		private int _LaneNo = 0;
		private string _LaneId = string.Empty;
		private string _LaneType = string.Empty;
		private string _LaneAbbr = string.Empty;

		#endregion

		#region Public Proprties

		/// <summary>
		/// Gets or sets Lane PkId
		/// </summary>
		[Category("Lane")]
		[Description("Gets or sets Lane PkId")]
		[ReadOnly(true)]
		[AutoIncrement]
		[Ignore]
		[PeropertyMapName("PkId")]
		[PropertyOrder(401)]
		public virtual int PkId
		{
			get
			{
				return _PkId;
			}
			set
			{
				if (_PkId != value)
				{
					_PkId = value;
					this.RaiseChanged("PkId");
				}
			}
		}
		/// <summary>
		/// Gets or sets Lane No.
		/// </summary>
		[Category("Lane")]
		[Description("Gets or sets Lane No.")]
		[PeropertyMapName("LaneNo")]
		[PropertyOrder(402)]
		public virtual int LaneNo
		{
			get
			{
				return _LaneNo;
			}
			set
			{
				if (_LaneNo != value)
				{
					_LaneNo = value;
					this.RaiseChanged("LaneNo");
				}
			}
		}
		/// <summary>
		/// Gets or sets LaneId
		/// </summary>
		[Category("Lane")]
		[Description("Gets or sets LaneId")]
		[MaxLength(10)]
		[PeropertyMapName("LaneId")]
		[PropertyOrder(403)]
		public virtual string LaneId
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
		[Category("Lane")]
		[Description("Gets or sets LaneType")]
		[MaxLength(10)]
		[Ignore]
		[PeropertyMapName("LaneType")]
		[PropertyOrder(404)]
		public virtual string LaneType
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
		[Category("Lane")]
		[Description("Gets or sets LaneAbbr")]
		[MaxLength(10)]
		[Ignore]
		[PeropertyMapName("LaneAbbr")]
		[PropertyOrder(405)]
		public virtual string LaneAbbr
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

		#endregion
	}

	#endregion

	#region ShiftModelBase (abstract)

	/// <summary>
	/// ShiftModelBase (abstract).
	/// </summary>
	/// <typeparam name="T">The Target Class.</typeparam>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	public abstract class ShiftModelBase<T> : DataCenterModelBase<T>
		where T : NTable, new()
	{
		#region Intenral Variables

		private int _ShiftId = 0;
		private string _ShiftNameTH = string.Empty;
		private string _ShiftNameEN = string.Empty;

		#endregion

		#region Public Proprties

		/// <summary>
		/// Gets or sets ShiftId.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets ShiftId.")]
		[PeropertyMapName("ShiftId")]
		[PropertyOrder(501)]
		public virtual int ShiftId
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
		[Ignore]
		[PeropertyMapName("ShiftNameTH")]
		[PropertyOrder(502)]
		public virtual string ShiftNameTH
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
		[Ignore]
		[PeropertyMapName("ShiftNameEN")]
		[PropertyOrder(503)]
		public virtual string ShiftNameEN
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
	}

	#endregion
}
