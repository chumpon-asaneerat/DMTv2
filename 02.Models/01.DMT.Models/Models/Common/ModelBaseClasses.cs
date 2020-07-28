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
		/// Gets or sets TSBNameEN.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets TSBNameEN.")]
		[MaxLength(100)]
		[PeropertyMapName("TSBNameEN")]
		[Ignore]
		[PropertyOrder(102)]
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
		[PropertyOrder(103)]
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

		private int _LaneNo = 0;
		private string _LaneId = string.Empty;

		#endregion

		#region Public Proprties

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

		#endregion
	}

	#endregion

	#region RoleModelBase (abstract)

	/// <summary>
	/// RoleModelBase (abstract).
	/// </summary>
	/// <typeparam name="T">The Target Class.</typeparam>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	public abstract class RoleModelBase<T> : DataCenterModelBase<T>
		where T : NTable, new()
	{
		#region Intenral Variables

		private string _RoleId = string.Empty;
		private string _RoleNameEN = string.Empty;
		private string _RoleNameTH = string.Empty;

		#endregion

		#region Public Proprties

		/// <summary>
		/// Gets or sets RoleId
		/// </summary>
		[Category("Role")]
		[Description("Gets or sets RoleId")]
		[ReadOnly(true)]
		[MaxLength(20)]
		[PeropertyMapName("RoleId")]
		[PropertyOrder(601)]
		public virtual string RoleId
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
		[MaxLength(50)]
		[Ignore]
		[PeropertyMapName("RoleNameEN")]
		[PropertyOrder(602)]
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
		[MaxLength(50)]
		[Ignore]
		[PeropertyMapName("RoleNameTH")]
		[PropertyOrder(603)]
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
	}

	#endregion

	#region UserModelBase (abstract)

	/// <summary>
	/// UserModelBase (abstract).
	/// </summary>
	/// <typeparam name="T">The Target Class.</typeparam>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	public abstract class UserModelBase<T> : RoleModelBase<T>
		where T : NTable, new()
	{
		#region Intenral Variables

		private string _UserId = string.Empty;
		private string _FullNameEN = string.Empty;
		private string _FullNameTH = string.Empty;

		#endregion

		#region Public Proprties

		/// <summary>
		/// Gets or sets UserId
		/// </summary>
		[Category("User")]
		[Description("Gets or sets UserId")]
		[MaxLength(10)]
		[PeropertyMapName("UserId")]
		[PropertyOrder(701)]
		public virtual string UserId
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
		[Ignore]
		[PeropertyMapName("FullNameEN")]
		[PropertyOrder(702)]
		public virtual string FullNameEN
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
		[Ignore]
		[PeropertyMapName("FullNameTH")]
		[PropertyOrder(703)]
		public virtual string FullNameTH
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

	#region UserShiftModelBase (abstract)

	/// <summary>
	/// UserShiftModelBase (abstract).
	/// </summary>
	/// <typeparam name="T">The Target Class.</typeparam>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	public abstract class UserShiftModelBase<T> : TSBModelBase<T>
		where T : NTable, new()
	{
		#region Intenral Variables

		// Shift
		private int _ShiftId = 0;
		private string _ShiftNameTH = string.Empty;
		private string _ShiftNameEN = string.Empty;
		// User
		private string _UserId = string.Empty;
		private string _FullNameEN = string.Empty;
		private string _FullNameTH = string.Empty;
		// Shift Begin/End
		private DateTime _Begin = DateTime.MinValue;
		private DateTime _End = DateTime.MinValue;

		#endregion

		#region Public Proprties

		#region Shift

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

		#region User

		/// <summary>
		/// Gets or sets UserId
		/// </summary>
		[Category("User")]
		[Description("Gets or sets UserId")]
		[MaxLength(10)]
		[PeropertyMapName("UserId")]
		[PropertyOrder(701)]
		public virtual string UserId
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
		[Ignore]
		[PeropertyMapName("FullNameEN")]
		[PropertyOrder(702)]
		public virtual string FullNameEN
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
		[Ignore]
		[PeropertyMapName("FullNameTH")]
		[PropertyOrder(703)]
		public virtual string FullNameTH
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

		#endregion

		#region Begin/End

		/// <summary>
		/// Gets or sets Begin Date.
		/// </summary>
		[Category("Shift")]
		[Description("Gets or sets Begin Date.")]
		//[ReadOnly(true)]
		[PeropertyMapName("Begin")]
		[PropertyOrder(801)]
		public virtual DateTime Begin
		{
			get { return _Begin; }
			set
			{
				if (_Begin != value)
				{
					_Begin = value;
					// Raise event.
					RaiseChanged("Begin");
					RaiseChanged("BeginDateString");
					RaiseChanged("BeginTimeString");
					RaiseChanged("BeginDateTimeString");
				}
			}
		}
		/// <summary>
		/// Gets or sets End Date.
		/// </summary>
		[Category("Shift")]
		[Description("Gets or sets End Date.")]
		//[ReadOnly(true)]
		[PeropertyMapName("End")]
		[PropertyOrder(805)]
		public virtual DateTime End
		{
			get { return _End; }
			set
			{
				if (_End != value)
				{
					_End = value;
					// Raise event.
					RaiseChanged("End");
					RaiseChanged("EndDateString");
					RaiseChanged("EndTimeString");
					RaiseChanged("EndDateTimeString");
				}
			}
		}
		/// <summary>
		/// Gets Begin Date String.
		/// </summary>
		[Category("Shift")]
		[Description("Gets Begin Date String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(802)]
		public string BeginDateString
		{
			get
			{
				var ret = (this.Begin == DateTime.MinValue) ? "" : this.Begin.ToThaiDateTimeString("dd/MM/yyyy");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets End Date String.
		/// </summary>
		[Category("Shift")]
		[Description("Gets End Date String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(806)]
		public string EndDateString
		{
			get
			{
				var ret = (this.End == DateTime.MinValue) ? "" : this.End.ToThaiDateTimeString("dd/MM/yyyy");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets Begin Time String.
		/// </summary>
		[Category("Shift")]
		[Description("Gets Begin Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(803)]
		public string BeginTimeString
		{
			get
			{
				var ret = (this.Begin == DateTime.MinValue) ? "" : this.Begin.ToThaiTimeString();
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets End Time String.
		/// </summary>
		[Category("Shift")]
		[Description("Gets End Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(807)]
		public string EndTimeString
		{
			get
			{
				var ret = (this.End == DateTime.MinValue) ? "" : this.End.ToThaiTimeString();
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets or sets Begin Date Time String..
		/// </summary>
		[Category("Shift")]
		[Description("Gets or sets Begin Date Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(804)]
		public string BeginDateTimeString
		{
			get
			{
				var ret = (this.Begin == DateTime.MinValue) ? "" : this.Begin.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets End Date Time String.
		/// </summary>
		[Category("Shift")]
		[Description("Gets End Date Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(808)]
		public string EndDateTimeString
		{
			get
			{
				var ret = (this.End == DateTime.MinValue) ? "" : this.End.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
				return ret;
			}
			set { }
		}

		#endregion

		#endregion
	}

	#endregion
}
