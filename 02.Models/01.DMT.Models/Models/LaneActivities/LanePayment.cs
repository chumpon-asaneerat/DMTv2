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
	#region LanePayment

	/// <summary>
	/// The LanePayment Data Model Class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("LanePayment")]
	public class LanePayment : NTable<LanePayment>
	{
		#region Intenral Variables

		private Guid _PKId = Guid.NewGuid();

		private string _ApproveCode = string.Empty;

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

		private string _PaymentId = string.Empty;
		private string _PaymentNameEN = string.Empty;
		private string _PaymentNameTH = string.Empty;

		private DateTime _PaymentDate = DateTime.MinValue;
		private decimal _Amount = decimal.Zero;

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public LanePayment() : base()
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
		/// Gets or sets Approve Code.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Approve Code.")]
		//[ReadOnly(true)]
		[MaxLength(20)]
		[PeropertyMapName("ApproveCode")]
		public string ApproveCode
		{
			get
			{
				return _ApproveCode;
			}
			set
			{
				if (_ApproveCode != value)
				{
					_ApproveCode = value;
					this.RaiseChanged("ApproveCode");
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

		#region Lane

		/// <summary>
		/// Gets or sets LaneId
		/// </summary>
		[Category("Lane")]
		[Description("Gets or sets LaneId")]
		[ReadOnly(true)]
		[Ignore]
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
		/// Gets or sets UserId
		/// </summary>
		[Category("User")]
		[Description("Gets or sets UserId.")]
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
		/// Gets or sets FullNameEN
		/// </summary>
		[Category("User")]
		[Description("Gets or sets User FullName EN.")]
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
		/// Gets or sets FullNameTH
		/// </summary>
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

		#region Payment

		/// <summary>
		/// Gets or sets PaymentId
		/// </summary>
		[Category("Payment")]
		[Description("Gets or sets PaymentId")]
		[ReadOnly(true)]
		[MaxLength(20)]
		[PeropertyMapName("PaymentId")]
		public string PaymentId
		{
			get
			{
				return _PaymentId;
			}
			set
			{
				if (_PaymentId != value)
				{
					_PaymentId = value;
					this.RaiseChanged("PaymentId");
				}
			}
		}
		/// <summary>
		/// Gets or sets PaymentNameEN
		/// </summary>
		[Category("Payment")]
		[Description("Gets or sets PaymentNameEN")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("PaymentNameEN")]
		public virtual string PaymentNameEN
		{
			get
			{
				return _PaymentNameEN;
			}
			set
			{
				if (_PaymentNameEN != value)
				{
					_PaymentNameEN = value;
					this.RaiseChanged("PaymentNameEN");
				}
			}
		}
		/// <summary>
		/// Gets or sets PaymentNameTH
		/// </summary>
		[Category("Payment")]
		[Description("Gets or sets PaymentNameTH")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("PaymentNameTH")]
		public virtual string PaymentNameTH
		{
			get
			{
				return _PaymentNameTH;
			}
			set
			{
				if (_PaymentNameTH != value)
				{
					_PaymentNameTH = value;
					this.RaiseChanged("PaymentNameTH");
				}
			}
		}

		#endregion

		#region Payment Date and Amount

		/// <summary>
		/// Gets or sets Payment Date.
		/// </summary>
		[Category("Payment")]
		[Description("Gets or sets Payment Date.")]
		//[ReadOnly(true)]
		[PeropertyMapName("PaymentDate")]
		public DateTime PaymentDate
		{
			get { return _PaymentDate; }
			set
			{
				if (_PaymentDate != value)
				{
					_PaymentDate = value;
					// Raise event.
					RaiseChanged("PaymentDate");
					RaiseChanged("PaymentDateString");
					RaiseChanged("PaymentTimeString");
				}
			}
		}
		/// <summary>
		/// Gets Payment Date String.
		/// </summary>
		[Category("Payment")]
		[Description("Gets Payment Date String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string PaymentDateString
		{
			get
			{
				var ret = (this.PaymentDate == DateTime.MinValue) ? "" : this.PaymentDate.ToThaiDateTimeString("dd/MM/yyyy");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets Payment Time String.
		/// </summary>
		[Category("Payment")]
		[Description("Gets Payment Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string PaymentTimeString
		{
			get
			{
				var ret = (this.PaymentDate == DateTime.MinValue) ? "" : this.PaymentDate.ToThaiTimeString();
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets or sets Amount.
		/// </summary>
		[Category("Payment")]
		[Description("Gets or sets Amount.")]
		//[ReadOnly(true)]
		[PeropertyMapName("Amount")]
		public decimal Amount
		{
			get { return _Amount; }
			set
			{
				if (_Amount != value)
				{
					_Amount = value;
					// Raise event.
					RaiseChanged("Amount");
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

		internal class FKs : LanePayment
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
			/// Gets or sets FullNameEN
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("FullNameEN")]
			public override string FullNameEN
			{
				get { return base.FullNameEN; }
				set { base.FullNameEN = value; }
			}
			/// <summary>
			/// Gets or sets FullNameTH
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("FullNameTH")]
			public override string FullNameTH
			{
				get { return base.FullNameTH; }
				set { base.FullNameTH = value; }
			}

			#endregion

			#region Payment

			/// <summary>
			/// Gets or sets PaymentNameEN
			/// </summary>
			[MaxLength(50)]
			[PeropertyMapName("PaymentNameEN")]
			public override string PaymentNameEN
			{
				get { return base.PaymentNameEN; }
				set { base.PaymentNameEN = value; }
			}
			/// <summary>
			/// Gets or sets PaymentNameTH
			/// </summary>
			[MaxLength(50)]
			[PeropertyMapName("PaymentNameTH")]
			public override string PaymentNameTH
			{
				get { return base.PaymentNameTH; }
				set { base.PaymentNameTH = value; }
			}

			#endregion
		}

		#endregion

		#region Static Methods

		public static NDbResult<LanePayment> Create(Lane lane, User collector,
			Payment payment, DateTime date, decimal amount)
		{
			LanePayment inst = Create();
			TSB tsb = TSB.GetCurrent();
			if (null != tsb) tsb.AssignTo(inst);
			if (null != lane) lane.AssignTo(inst);
			if (null != collector) collector.AssignTo(inst);
			if (null != payment) payment.AssignTo(inst);
			inst.PaymentDate = date;
			inst.Amount = amount;
			return inst;
		}

		public static NDbResult<List<LanePayment>> Search(UserShift shift)
		{
			var result = new NDbResult<List<LanePayment>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<LanePayment>();
				return result;
			}

			if (null == shift)
			{
				result.ParameterIsNull();
				result.data = new List<LanePayment>();
				return result;
			}

			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT LanePayment.* ";
					cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
					cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
					cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
					cmd += "     , Lane.LaneNo ";
					cmd += "     , User.FullNameEN, User.FullNameTH ";
					cmd += "     , Payment.PaymentNameEN, User.PaymentNameTH ";
					cmd += "  FROM LanePayment, TSB, PlazaGroup, Plaza, Lane, User, Payment ";
					cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND Lane.TSBId = TSB.TSBId ";
					cmd += "   AND Lane.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND Lane.PlazaId = Plaza.PlazaId ";
					cmd += "   AND LanePayment.TSBId = TSB.TSBId ";
					cmd += "   AND LanePayment.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND LanePayment.PlazaId = Plaza.PlazaId ";
					cmd += "   AND LanePayment.LaneId = Lane.LaneId ";
					cmd += "   AND LanePayment.UserId = User.UserId ";
					cmd += "   AND LanePayment.PaymentId = Payment.PaymentId ";
					cmd += "   AND LanePayment.Begin >= ? ";
					cmd += "   AND LanePayment.End <= ? ";

					var rets = NQuery.Query<FKs>(cmd, shift.Begin, shift.End,
						DateTime.MinValue).ToList();
					var results = new List<LanePayment>();
					if (null != rets)
					{
						rets.ForEach(ret =>
						{
							results.Add(ret.ToLanePayment());
						});
					}
					result.data = results;
					result.Success();
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = new List<LanePayment>();
				}

				return result;
			}
		}

		public static NDbResult<List<LanePayment>> Search(Lane lane)
		{
			var result = new NDbResult<List<LanePayment>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<LanePayment>();
				return result;
			}

			if (null == lane)
			{
				result.ParameterIsNull();
				result.data = new List<LanePayment>();
				return result;
			}
			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT LanePayment.* ";
					cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
					cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
					cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
					cmd += "     , Lane.LaneNo ";
					cmd += "     , User.FullNameEN, User.FullNameTH ";
					cmd += "     , Payment.PaymentNameEN, User.PaymentNameTH ";
					cmd += "  FROM LanePayment, TSB, PlazaGroup, Plaza, Lane, User, Payment ";
					cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND Lane.TSBId = TSB.TSBId ";
					cmd += "   AND Lane.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND Lane.PlazaId = Plaza.PlazaId ";
					cmd += "   AND LanePayment.TSBId = TSB.TSBId ";
					cmd += "   AND LanePayment.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND LanePayment.PlazaId = Plaza.PlazaId ";
					cmd += "   AND LanePayment.LaneId = Lane.LaneId ";
					cmd += "   AND LanePayment.UserId = User.UserId ";
					cmd += "   AND LanePayment.PaymentId = Payment.PaymentId ";
					cmd += "   AND LanePayment.LaneId = ? ";

					var rets = NQuery.Query<FKs>(cmd, lane.LaneId).ToList();
					var results = new List<LanePayment>();
					if (null != rets)
					{
						rets.ForEach(ret =>
						{
							results.Add(ret.ToLanePayment());
						});
					}
					result.data = results;
					result.Success();
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = new List<LanePayment>();
				}

				return result;
			}
		}

		public static NDbResult<LanePayment> GetCurrentByLane(Lane lane)
		{
			var result = new NDbResult<LanePayment>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = null;
				return result;
			}

			if (null == lane)
			{
				result.ParameterIsNull();
				result.data = null;
				return result;
			}
			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT LanePayment.* ";
					cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
					cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
					cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
					cmd += "     , Lane.LaneNo ";
					cmd += "     , User.FullNameEN, User.FullNameTH ";
					cmd += "     , Payment.PaymentNameEN, User.PaymentNameTH ";
					cmd += "  FROM LanePayment, TSB, PlazaGroup, Plaza, Lane, User, Payment ";
					cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND Lane.TSBId = TSB.TSBId ";
					cmd += "   AND Lane.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND Lane.PlazaId = Plaza.PlazaId ";
					cmd += "   AND LanePayment.TSBId = TSB.TSBId ";
					cmd += "   AND LanePayment.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND LanePayment.PlazaId = Plaza.PlazaId ";
					cmd += "   AND LanePayment.LaneId = Lane.LaneId ";
					cmd += "   AND LanePayment.UserId = User.UserId ";
					cmd += "   AND LanePayment.PaymentId = Payment.PaymentId ";
					cmd += "   AND LanePayment.LaneId = ? ";
					cmd += "   AND LanePayment.End = ? ";

					var ret = NQuery.Query<FKs>(cmd, lane.LaneId,
						DateTime.MinValue).FirstOrDefault();
					result.data = (null != ret) ? ret.ToLanePayment() : null;
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

		public static NDbResult<List<LanePayment>> Search(DateTime date)
		{
			var result = new NDbResult<List<LanePayment>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<LanePayment>();
				return result;
			}

			if (null == date || date == DateTime.MinValue)
			{
				result.ParameterIsNull();
				result.data = new List<LanePayment>();
				return result;
			}
			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT LanePayment.* ";
					cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
					cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
					cmd += "     , Plaza.PlazaNameEN, Plaza.PlazaNameTH ";
					cmd += "     , Lane.LaneNo ";
					cmd += "     , User.FullNameEN, User.FullNameTH ";
					cmd += "     , Payment.PaymentNameEN, User.PaymentNameTH ";
					cmd += "  FROM LanePayment, TSB, PlazaGroup, Plaza, Lane, User, Payment ";
					cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.TSBId = TSB.TSBId ";
					cmd += "   AND Plaza.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND Lane.TSBId = TSB.TSBId ";
					cmd += "   AND Lane.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND Lane.PlazaId = Plaza.PlazaId ";
					cmd += "   AND LanePayment.TSBId = TSB.TSBId ";
					cmd += "   AND LanePayment.PlazaGroupId = PlazaGroup.PlazaGroupId ";
					cmd += "   AND LanePayment.PlazaId = Plaza.PlazaId ";
					cmd += "   AND LanePayment.LaneId = Lane.LaneId ";
					cmd += "   AND LanePayment.UserId = User.UserId ";
					cmd += "   AND LanePayment.PaymentId = Payment.PaymentId ";
					cmd += " WHERE LanePayment.Begin >= ? ";
					cmd += "   AND LanePayment.End <= ? ";

					var rets = NQuery.Query<FKs>(cmd, date,
						DateTime.MinValue).ToList();
					var results = new List<LanePayment>();
					if (null != rets)
					{
						rets.ForEach(ret =>
						{
							results.Add(ret.ToLanePayment());
						});
					}
					result.data = results;
					result.Success();
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = new List<LanePayment>();
				}

				return result;
			}
		}

		#endregion
	}

	public class LanePaymentCreate
	{
		public Lane Lane { get; set; }
		public User User { get; set; }
		public Payment Payment { get; set; }
		public DateTime Date { get; set; }
		public decimal Amount { get; set; }
	}

	#endregion
}
