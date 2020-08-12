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
	#region Lane

	/// <summary>
	/// The Lane Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("Lane")]
	public class Lane : NTable<Lane>
	{
		#region Intenral Variables

		private int _PkId = 0;
		private int _LaneNo = 0;
		private string _LaneId = string.Empty;
		private string _LaneType = string.Empty;
		private string _LaneAbbr = string.Empty;

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

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public Lane() : base() { }

		#endregion

		#region Public Proprties

		#region Common

		/// <summary>
		/// Gets or sets LanePkId
		/// </summary>
		[Category("Lane")]
		[Description("Gets or sets LanePkId")]
		[ReadOnly(true)]
		[PrimaryKey, AutoIncrement]
		[PeropertyMapName("PkId")]
		public int PkId
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
		public int LaneNo
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
		/// Gets or sets LaneType
		/// </summary>
		[Category("Lane")]
		[Description("Gets or sets LaneType")]
		[MaxLength(10)]
		[PeropertyMapName("LaneType")]
		public string LaneType
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
		[PeropertyMapName("LaneAbbr")]
		public string LaneAbbr
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
		/// Gets or sets TSBNameEN.
		/// </summary>
		[Category("TSB")]
		[Description("Gets or sets TSBNameEN.")]
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
		/// Gets or sets TSBNameTH.
		/// </summary>
		[Category("TSB")]
		[Description("Gets or sets TSBNameTH.")]
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
		/// Gets or sets PlazaGroupId.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets PlazaGroupId.")]
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
		/// Gets or sets PlazaGroupNameEN.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets PlazaGroupNameEN.")]
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
		/// Gets or sets PlazaGroupNameTH.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets PlazaGroupNameTH.")]
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
		/// Gets or sets PlazaId.
		/// </summary>
		[Category("Plaza")]
		[Description("Gets or sets PlazaId.")]
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
		/// Gets or sets PlazaNameEN
		/// </summary>
		[Category("Plaza")]
		[Description("Gets or sets PlazaNameEN")]
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
		/// Gets or sets PlazaNameTH
		/// </summary>
		[Category("Plaza")]
		[Description("Gets or sets PlazaNameTH")]
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

		public class FKs : Lane
		{
			#region TSB

			/// <summary>
			/// Gets or sets TSBNameEN.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("TSBNameEN")]
			public override string TSBNameEN
			{
				get { return base.TSBNameEN; }
				set { base.TSBNameEN = value; }
			}
			/// <summary>
			/// Gets or sets TSBNameTH.
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
			/// Gets or sets PlazaGroupNameEN.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("PlazaGroupNameEN")]
			public override string PlazaGroupNameEN
			{
				get { return base.PlazaGroupNameEN; }
				set { base.PlazaGroupNameEN = value; }
			}
			/// <summary>
			/// Gets or sets PlazaGroupNameTH.
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
			/// Gets or sets PlazaNameEN
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("PlazaNameEN")]
			public override string PlazaNameEN
			{
				get { return base.PlazaNameEN; }
				set { base.PlazaNameEN = value; }
			}
			/// <summary>
			/// Gets or sets PlazaNameTH
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("PlazaNameTH")]
			public override string PlazaNameTH
			{
				get { return base.PlazaNameTH; }
				set { base.PlazaNameTH = value; }
			}

			#endregion

			#region Public Methods

			public Lane ToLane()
			{
				Lane inst = new Lane();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}

			#endregion
		}

		#endregion

		#region Static Methods

		public static NDbResult<List<Lane>> Gets(SQLiteConnection db)
		{
			var result = new NDbResult<List<Lane>>();

			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<Lane>();
				return result;
			}

			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT Lane.* ";
					cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
					cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
					cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
					cmd += "  FROM Lane, Plaza, PlazaGroup, TSB ";
					cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND Lane.TSBId = TSB.TSBId ";
					cmd += "   AND Lane.PlazaGroupId = Plaza.PlazaGroupId ";
					cmd += "   AND Lane.PlazaId = Plaza.PlazaId ";

					var rets = NQuery.Query<FKs>(cmd).ToList();
					var results = new List<Lane>();
					if (null != rets)
					{
						rets.ForEach(ret =>
						{
							results.Add(ret.ToLane());
						});
					}
					result.data = results;
					result.Success();
				}
				catch (Exception ex)
				{
					result.Error(ex);

				}

				return result;
			}
		}

		public static NDbResult<List<Lane>> Gets()
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return Gets(db);
			}
		}

		public static NDbResult<Lane> Get(SQLiteConnection db, string laneId)
		{
			var result = new NDbResult<Lane>();

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
					cmd += "SELECT Lane.* ";
					cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
					cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
					cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
					cmd += "  FROM Lane, Plaza, PlazaGroup, TSB ";
					cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND Lane.TSBId = TSB.TSBId ";
					cmd += "   AND Lane.PlazaGroupId = Plaza.PlazaGroupId ";
					cmd += "   AND Lane.PlazaId = Plaza.PlazaId ";
					cmd += "   AND Lane.LaneId = ? ";

					var ret = NQuery.Query<FKs>(cmd, laneId).FirstOrDefault();
					result.data = (null != ret) ? ret.ToLane() : null;
					result.Success();
				}
				catch (Exception ex)
				{
					result.Error(ex);

				}
				return result;
			}
		}
		public static NDbResult<Lane> Get(string laneId)
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return Get(db, laneId);
			}
		}

		public static NDbResult<List<Lane>> GetTSBLanes(TSB value)
		{
			var result = new NDbResult<List<Lane>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<Lane>();
				return result;
			}

			lock (sync)
			{
				return GetTSBLanes(value.TSBId);
			}
		}
		public static NDbResult<List<Lane>> GetTSBLanes(string tsbId)
		{
			var result = new NDbResult<List<Lane>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<Lane>();
				return result;
			}

			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT Lane.* ";
					cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
					cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
					cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
					cmd += "  FROM Lane, Plaza, PlazaGroup, TSB ";
					cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND Lane.TSBId = TSB.TSBId ";
					cmd += "   AND Lane.PlazaGroupId = Plaza.PlazaGroupId ";
					cmd += "   AND Lane.PlazaId = Plaza.PlazaId ";
					cmd += "   AND Lane.TSBId = ? ";
					var rets = NQuery.Query<FKs>(cmd, tsbId).ToList();
					var results = new List<Lane>();
					if (null != rets)
					{
						rets.ForEach(ret =>
						{
							results.Add(ret.ToLane());
						});
					}
					result.data = results;
					result.Success();
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = new List<Lane>();
				}
				return result;
			}
		}

		public static NDbResult<List<Lane>> GetPlazaGroupLanes(PlazaGroup value)
		{
			var result = new NDbResult<List<Lane>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<Lane>();
				return result;
			}

			lock (sync)
			{
				return GetPlazaGroupLanes(value.TSBId, value.PlazaGroupId);
			}
		}
		public static NDbResult<List<Lane>> GetPlazaGroupLanes(string tsbId, string plazaGroupId)
		{
			var result = new NDbResult<List<Lane>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<Lane>();
				return result;
			}

			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT Lane.* ";
					cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
					cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
					cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
					cmd += "  FROM Lane, Plaza, PlazaGroup, TSB ";
					cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND Lane.TSBId = TSB.TSBId ";
					cmd += "   AND Lane.PlazaGroupId = Plaza.PlazaGroupId ";
					cmd += "   AND Lane.PlazaId = Plaza.PlazaId ";
					cmd += "   AND Lane.TSBId = ? ";
					cmd += "   AND Lane.PlazaGroupId = ? ";

					var rets = NQuery.Query<FKs>(cmd, tsbId, plazaGroupId).ToList();
					var results = new List<Lane>();
					if (null != rets)
					{
						rets.ForEach(ret =>
						{
							results.Add(ret.ToLane());
						});
					}
					result.data = results;
					result.Success();
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = new List<Lane>();
				}
				return result;
			}
		}

		public static NDbResult<List<Lane>> GetPlazaLanes(Plaza value)
		{
			var result = new NDbResult<List<Lane>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<Lane>();
				return result;
			}

			lock (sync)
			{
				return GetPlazaLanes(value.TSBId, value.PlazaGroupId, value.PlazaId);
			}
		}

		public static NDbResult<List<Lane>> GetPlazaLanes(string tsbId, string plazaGroupId, 
			string plazaId)
		{
			var result = new NDbResult<List<Lane>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<Lane>();
				return result;
			}

			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT Lane.* ";
					cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
					cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
					cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
					cmd += "  FROM Lane, Plaza, PlazaGroup, TSB ";
					cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND Lane.TSBId = TSB.TSBId ";
					cmd += "   AND Lane.PlazaGroupId = Plaza.PlazaGroupId ";
					cmd += "   AND Lane.PlazaId = Plaza.PlazaId ";
					cmd += "   AND Lane.TSBId = ? ";
					cmd += "   AND Lane.PlazaGroupId = ? ";
					cmd += "   AND Lane.PlazaId = ? ";

					var rets = NQuery.Query<FKs>(cmd, tsbId, plazaGroupId, plazaId).ToList();
					var results = new List<Lane>();
					if (null != rets)
					{
						rets.ForEach(ret =>
						{
							results.Add(ret.ToLane());
						});
					}
					result.data = results;
					result.Success();
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = new List<Lane>();
				}
				return result;
			}
		}

		#endregion
	}

	#endregion
}
