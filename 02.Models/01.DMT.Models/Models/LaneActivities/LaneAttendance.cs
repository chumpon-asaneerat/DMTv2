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
	#region LaneAttendance

	/// <summary>
	/// The LaneAttendance Data Model Class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("LaneAttendance")]
	public class LaneAttendance : NTable<LaneAttendance>
	{
		#region Intenral Variables

		private Guid _PKId = Guid.NewGuid();

		private string _JobId = string.Empty;

		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;

		private string _PlazaGroupId = string.Empty;
		private string _PlazaGroupNameEN = string.Empty;
		private string _PlazaGroupNameTH = string.Empty;
		private string _Direction = string.Empty;

		private string _PlazaId = string.Empty;
		private string _PlazaNameEN = string.Empty;
		private string _PlazaNameTH = string.Empty;

		private string _LaneId = string.Empty;
		private int _LaneNo = 0;

		private string _UserId = string.Empty;
		private string _FullNameEN = string.Empty;
		private string _FullNameTH = string.Empty;

		private DateTime _Begin = DateTime.MinValue;
		private DateTime _End = DateTime.MinValue;

		private string _RevenueId = string.Empty;
		private DateTime _RevenueDate = DateTime.MinValue;

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public LaneAttendance() : base()
		{
		}

		#endregion

		#region Public Properties

		#region Common

		/// <summary>
		/// Gets or sets PKId
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets PKId")]
		[ReadOnly(true)]
		[PrimaryKey]
		[PeropertyMapName("PKId")]
		public Guid PKId
		{
			get
			{
				return _PKId;
			}
			set
			{
				if (_PKId != value)
				{
					_PKId = value;
					this.RaiseChanged("PKId");
				}
			}
		}
		/// <summary>
		/// Gets or sets JobId
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets JobId")]
		[ReadOnly(true)]
		[MaxLength(20)]
		[PeropertyMapName("JobId")]
		public string JobId
		{
			get
			{
				return _JobId;
			}
			set
			{
				if (_JobId != value)
				{
					_JobId = value;
					this.RaiseChanged("JobId");
				}
			}
		}

		#endregion

		#region TSB

		/// <summary>
		/// Gets or sets TSBId.
		/// </summary>
		[Category("TSB")]
		[Description("Gets or sets TSBId.")]
		[ReadOnly(true)]
		[MaxLength(10)]
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
		/// Gets or sets TSB Name EN.
		/// </summary>
		[Category("TSB")]
		[Description("Gets or sets TSB Name EN.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("TSBNameEN")]
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
		/// Gets or sets TSB Name TH.
		/// </summary>
		[Category("TSB")]
		[Description("Gets or sets TSB Name TH.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("TSBNameTH")]
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

		#region PlazaGroup

		/// <summary>
		/// Gets or sets Plaza Group Id.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets Plaza Group Id.")]
		[ReadOnly(true)]
		[MaxLength(10)]
		[PeropertyMapName("PlazaGroupId")]
		public string PlazaGroupId
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
		/// Gets or sets Plaza Group Name EN.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets Plaza Group Name EN.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("PlazaGroupNameEN")]
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
		/// Gets or sets Plaza Group Name TH.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets Plaza Group Name TH.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("PlazaGroupNameTH")]
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
		/// Gets or sets Direction.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets Direction.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("Direction")]
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

		#region Plaza

		/// <summary>
		/// Gets or sets Plaza Id.
		/// </summary>
		[Category("Plaza")]
		[Description("Gets or sets Plaza Id.")]
		[ReadOnly(true)]
		[MaxLength(10)]
		[PeropertyMapName("PlazaId")]
		public string PlazaId
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
		/// Gets or sets Plaza Name EN
		/// </summary>
		[Category("Plaza")]
		[Description("Gets or sets Plaza Name EN")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("PlazaNameEN")]
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
		/// Gets or sets Plaza Name TH
		/// </summary>
		[Category("Plaza")]
		[Description("Gets or sets Plaza Name TH")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("PlazaNameTH")]
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

		#region Lane

		/// <summary>
		/// Gets or sets LaneId
		/// </summary>
		[Category("Lane")]
		[Description("Gets or sets LaneId")]
		[ReadOnly(true)]
		[MaxLength(10)]
		[PeropertyMapName("LaneId")]
		public string LaneId
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
		/// Gets or sets Lane No.
		/// </summary>
		[Category("Lane")]
		[Description("Gets or sets Lane No.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("LaneNo")]
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

		#endregion

		#region User

		/// <summary>
		/// Gets or sets User Id
		/// </summary>
		[Category("User")]
		[Description("Gets or sets User Id.")]
		[ReadOnly(true)]
		[MaxLength(10)]
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
		/// Gets or sets User Full Name EN
		/// </summary>
		[Category("User")]
		[Description("Gets or sets User Full Name EN.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("FullNameEN")]
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
		/// Gets or sets User Full Name TH
		/// </summary>
		[Category("User")]
		[Description("Gets or sets User Full Name TH.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("FullNameTH")]
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
		public DateTime Begin
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
		public DateTime End
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
		/// Gets or sets Begin Date Time String.
		/// </summary>
		[Category("Shift")]
		[Description("Gets or sets Begin Date Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
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
		/// Gets or sets End Date Time String.
		/// </summary>
		[Category("Shift")]
		[Description("Gets or sets End Date Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
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

		#region Revenue

		/// <summary>
		/// Gets or sets Revenue Date.
		/// </summary>
		[Category("Revenue")]
		[Description("Gets or sets Revenue Date.")]
		//[ReadOnly(true)]
		[PeropertyMapName("RevenueDate")]
		public DateTime RevenueDate
		{
			get { return _RevenueDate; }
			set
			{
				if (_RevenueDate != value)
				{
					_RevenueDate = value;
					// Raise event.
					this.RaiseChanged("RevenueDate");
					this.RaiseChanged("RevenueDateString");
					this.RaiseChanged("RevenueDateTimeString");
				}
			}
		}
		/// <summary>
		/// Gets Revenue Date String.
		/// </summary>
		[Category("Revenue")]
		[Description("Gets Revenue Date String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string RevenueDateString
		{
			get
			{
				var ret = (this.RevenueDate == DateTime.MinValue) ? "" : this.RevenueDate.ToThaiDateTimeString("dd/MM/yyyy");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets Revenue Date Time String.
		/// </summary>
		[Category("Revenue")]
		[Description("Gets Revenue Date Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string RevenueDateTimeString
		{
			get
			{
				var ret = (this.RevenueDate == DateTime.MinValue) ? "" : this.RevenueDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets or sets RevenueId.
		/// </summary>
		[Category("Revenue")]
		[Description("Gets or sets RevenueId.")]
		//[ReadOnly(true)]
		[MaxLength(20)]
		[PeropertyMapName("RevenueId")]
		public string RevenueId
		{
			get { return _RevenueId; }
			set
			{
				if (_RevenueId != value)
				{
					_RevenueId = value;
					// Raise event.
					this.RaiseChanged("RevenueId");
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

		#region Internal Class

		/// <summary>
		/// The internal FKs class for query data.
		/// </summary>
		public class FKs : LaneAttendance, IFKs<LaneAttendance>
		{
			#region TSB

			/// <summary>
			/// Gets or sets TSB Name EN.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("TSBNameEN")]
			public override string TSBNameEN
			{
				get { return base.TSBNameEN; }
				set { base.TSBNameEN = value; }
			}
			/// <summary>
			/// Gets or sets TSB Name TH.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("TSBNameTH")]
			public override string TSBNameTH
			{
				get { return base.TSBNameTH; }
				set { base.TSBNameTH = value; }
			}

			#endregion

			#region PlazaGroup

			/// <summary>
			/// Gets or sets Plaza Group Name EN.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("PlazaGroupNameEN")]
			public override string PlazaGroupNameEN
			{
				get { return base.PlazaGroupNameEN; }
				set { base.PlazaGroupNameEN = value; }
			}
			/// <summary>
			/// Gets or sets Plaza Group Name TH.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("PlazaGroupNameTH")]
			public override string PlazaGroupNameTH
			{
				get { return base.PlazaGroupNameTH; }
				set { base.PlazaGroupNameTH = value; }
			}
			/// <summary>
			/// Gets or sets Direction.
			/// </summary>
			[MaxLength(10)]
			[PeropertyMapName("Direction")]
			public override string Direction
			{
				get { return base.Direction; }
				set { base.Direction = value; }
			}

			#endregion

			#region Plaza

			/// <summary>
			/// Gets or sets Plaza Name EN.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("PlazaNameEN")]
			public override string PlazaNameEN
			{
				get { return base.PlazaNameEN; }
				set { base.PlazaNameEN = value; }
			}
			/// <summary>
			/// Gets or sets Plaza Name TH.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("PlazaNameTH")]
			public override string PlazaNameTH
			{
				get { return base.PlazaNameTH; }
				set { base.PlazaNameTH = value; }
			}

			#endregion

			#region Lane

			/// <summary>
			/// Gets or set Lane No.
			/// </summary>
			[PeropertyMapName("LaneNo")]
			public override int LaneNo
			{
				get { return base.LaneNo; }
				set { base.LaneNo = value; }
			}

			#endregion

			#region User

			/// <summary>
			/// Gets or sets Full Name EN.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("FullNameEN")]
			public override string FullNameEN
			{
				get { return base.FullNameEN; }
				set { base.FullNameEN = value; }
			}
			/// <summary>
			/// Gets or sets Full Name TH.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("FullNameTH")]
			public override string FullNameTH
			{
				get { return base.FullNameTH; }
				set { base.FullNameTH = value; }
			}

			#endregion
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Create Lane Attendance.
		/// </summary>
		/// <param name="lane">The Lane instance.</param>
		/// <param name="supervisor">The User instance.</param>
		/// <returns>Returns LaneAttendance instance.</returns>
		public static NDbResult<LaneAttendance> Create(Lane lane, User supervisor)
		{
			var result = new NDbResult<LaneAttendance>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			LaneAttendance inst = Create();
			var tsb = TSB.GetCurrent().Value();
			if (null == tsb)
			{
				result.ParameterIsNull();
			}
			else
			{
				if (null != tsb) tsb.AssignTo(inst);
				if (null != lane) lane.AssignTo(inst);
				if (null != supervisor) supervisor.AssignTo(inst);
				result.Success(inst);
			}
			return result;
		}
		/// <summary>
		/// Search.
		/// </summary>
		/// <param name="shift">The User Shift instance.</param>
		/// <param name="plazaGroup">The PlazaGroup instance.</param>
		/// <param name="revenueDate">The Revenue Date Time.</param>
		/// <returns>Returns List of LaneAttendance.</returns>
		public static NDbResult<List<LaneAttendance>> Search(UserShift shift, 
			PlazaGroup plazaGroup, DateTime revenueDate)
		{
			var result = new NDbResult<List<LaneAttendance>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			if (null == shift)
			{
				result.ParameterIsNull();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;

					cmd += "SELECT * ";
					cmd += "  FROM LaneAttendanceView ";
					cmd += " WHERE UserId = ? ";
					cmd += "   AND (Begin >= ? AND Begin <= ?)";
					cmd += "   AND ((End >= ? AND End <= ?) " +
						"        OR  End = ?)";
					if (null != plazaGroup)
					{
						cmd += "   AND PlazaGroupId = ? ";
					}
					if (revenueDate == DateTime.MinValue)
					{
						cmd += "   AND (RevenueDate IS NULL ";
						cmd += "        OR RevenueDate = ?) ";
					}
					else
					{
						cmd += "   AND RevenueDate = ? ";
					}

					DateTime end = (shift.End == DateTime.MinValue) ? DateTime.Now : shift.End;
					if (null != plazaGroup)
					{
						var rets = NQuery.Query<FKs>(cmd,
							shift.UserId,
							shift.Begin, end,
							shift.Begin, end,
							DateTime.MinValue,
							plazaGroup.PlazaGroupId,
							revenueDate).ToList();
						var results = rets.ToModels();
						result.Success(results);
					}
					else
					{
						var rets = NQuery.Query<FKs>(cmd,
							shift.UserId,
							shift.Begin, end,
							shift.Begin, end,
							DateTime.MinValue,
							revenueDate).ToList();
						var results = rets.ToModels();
						result.Success(results);
					}
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
		/// Search.
		/// </summary>
		/// <param name="shift">The User Shift instance.</param>
		/// <returns>Returns List of LaneAttendance.</returns>
		public static NDbResult<List<LaneAttendance>> Search(UserShift shift)
		{
			var result = new NDbResult<List<LaneAttendance>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			if (null == shift)
			{
				result.ParameterIsNull();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM LaneAttendanceView ";
					cmd += " WHERE UserId = ? ";
					cmd += "   AND (Begin >= ? AND Begin <= ?)";
					cmd += "   AND (   (End >= ? AND End <= ?) " +
						"           OR  End = ?)";

					DateTime end = (shift.End == DateTime.MinValue) ? DateTime.Now : shift.End;
					var rets = NQuery.Query<FKs>(cmd, 
						shift.UserId,
						shift.Begin, end,
						shift.Begin, end,
						DateTime.MinValue).ToList();
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
		/// Search.
		/// </summary>
		/// <param name="lane">The Lane instance.</param>
		/// <returns>Returns List of LaneAttendance.</returns>
		public static NDbResult<List<LaneAttendance>> Search(Lane lane)
		{
			var result = new NDbResult<List<LaneAttendance>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			if (null == lane)
			{
				result.ParameterIsNull();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM LaneAttendanceView ";
					cmd += " WHERE LaneId = ? ";
					var rets = NQuery.Query<FKs>(cmd, lane.LaneId).ToList();
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
		/// Search.
		/// </summary>
		/// <param name="date">The date to search.</param>
		/// <returns>Returns List of LaneAttendance.</returns>
		public static NDbResult<List<LaneAttendance>> Search(DateTime date)
		{
			var result = new NDbResult<List<LaneAttendance>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			if (null == date || date == DateTime.MinValue)
			{
				result.ParameterIsNull();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM LaneAttendanceView ";
					cmd += " WHERE Begin >= ? ";
					cmd += "   AND End <= ? ";
					var rets = NQuery.Query<FKs>(cmd, date, DateTime.MinValue).ToList();
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
		/// Gets Current Lane Attendance by Lane.
		/// </summary>
		/// <param name="lane">The Lane instance.</param>
		/// <returns>Returns LaneAttendance instance.</returns>
		public static NDbResult<LaneAttendance> GetCurrentByLane(Lane lane)
		{
			var result = new NDbResult<LaneAttendance>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			if (null == lane)
			{
				result.ParameterIsNull();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM LaneAttendanceView ";
					cmd += " WHERE LaneId = ? ";
					cmd += "   AND End = ? ";
					var ret = NQuery.Query<FKs>(cmd, lane.LaneId, 
						DateTime.MinValue).FirstOrDefault();
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
		/// Gets all LaneAttendance that has not enter revenue.
		/// </summary>
		/// <returns>Returns List of LaneAttendance.</returns>
		public static NDbResult<List<LaneAttendance>> GetAllNotHasRevenueEntry()
		{
			var result = new NDbResult<List<LaneAttendance>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			var tsb = TSB.GetCurrent().Value();
			if (null == tsb)
			{
				result.ParameterIsNull();
				return result;
			}
			result = GetAllNotHasRevenueEntry(tsb);
			return result;
		}
		/// <summary>
		/// Gets all LaneAttendance that has not enter revenue.
		/// </summary>
		/// <param name="tsb">The TSB instance.</param>
		/// <returns>Returns List of LaneAttendance.</returns>
		public static NDbResult<List<LaneAttendance>> GetAllNotHasRevenueEntry(TSB tsb)
		{
			var result = new NDbResult<List<LaneAttendance>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			if (null == tsb)
			{
				result.ParameterIsNull();
				return result;
			}
			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * ";
					cmd += "  FROM LaneAttendanceView ";
					cmd += " WHERE TSBId = ? ";
					cmd += "   AND (RevenueDate = ?";
					cmd += "    OR  RevenueId IS NULL ";
					cmd += "    OR  RevenueId = ?)";
					var rets = NQuery.Query<FKs>(cmd,
						tsb.TSBId, DateTime.MinValue, string.Empty).ToList();
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


		public static NDbResult SaveLaneAttendance(LaneAttendance value)
		{
			return new NDbResult();
		}

		public static NDbResult SaveLaneAttendances(List<LaneAttendance> values)
		{
			return new NDbResult();
		}

		#endregion
	}

	/// <summary>
	/// The LaneAttendanceCreate class.
	/// </summary>
	public class LaneAttendanceCreate
	{
		/// <summary>
		/// Gets or sets Lane.
		/// </summary>
		public Lane Lane { get; set; }
		/// <summary>
		/// Gets or sets User.
		/// </summary>
		public User User { get; set; }
	}

	#endregion
}
