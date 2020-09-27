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
		[PropertyMapName("PKId")]
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
		[PropertyMapName("ApproveCode")]
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
		[PropertyMapName("TSBId")]
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
		[PropertyMapName("TSBNameEN")]
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
		[PropertyMapName("TSBNameTH")]
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
		[PropertyMapName("PlazaGroupId")]
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
		[PropertyMapName("PlazaGroupNameEN")]
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
		[PropertyMapName("PlazaGroupNameTH")]
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
		[PropertyMapName("Direction")]
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
		[PropertyMapName("PlazaId")]
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
		[PropertyMapName("PlazaNameEN")]
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
		[PropertyMapName("PlazaNameTH")]
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
		[PropertyMapName("LaneId")]
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
		[Ignore]
		[PropertyMapName("LaneNo")]
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
		[PropertyMapName("UserId")]
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
		[MaxLength(150)]
		[PropertyMapName("FullNameEN")]
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
		[MaxLength(150)]
		[PropertyMapName("FullNameTH")]
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
		[PropertyMapName("PaymentId")]
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
		/// Gets or sets Payment Name EN.
		/// </summary>
		[Category("Payment")]
		[Description("Gets or sets Payment Name EN.")]
		[ReadOnly(true)]
		[Ignore]
		[PropertyMapName("PaymentNameEN")]
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
		/// Gets or sets Payment Name TH.
		/// </summary>
		[Category("Payment")]
		[Description("Gets or sets Payment Name TH.")]
		[ReadOnly(true)]
		[Ignore]
		[PropertyMapName("PaymentNameTH")]
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
		[PropertyMapName("PaymentDate")]
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
		[PropertyMapName("Amount")]
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
		[PropertyMapName("Status", typeof(LanePayment))]
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
		[PropertyMapName("LastUpdate", typeof(LanePayment))]
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
		internal class FKs : LanePayment, IFKs<LanePayment>
		{
			#region TSB

			/// <summary>
			/// Gets or sets TSB Name EN.
			/// </summary>
			[MaxLength(100)]
			[PropertyMapName("TSBNameEN")]
			public override string TSBNameEN
			{
				get { return base.TSBNameEN; }
				set { base.TSBNameEN = value; }
			}
			/// <summary>
			/// Gets or sets TSB Name TH.
			/// </summary>
			[MaxLength(100)]
			[PropertyMapName("TSBNameTH")]
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
			[PropertyMapName("PlazaGroupNameEN")]
			public override string PlazaGroupNameEN
			{
				get { return base.PlazaGroupNameEN; }
				set { base.PlazaGroupNameEN = value; }
			}
			/// <summary>
			/// Gets or sets Plaza Group Name TH.
			/// </summary>
			[MaxLength(100)]
			[PropertyMapName("PlazaGroupNameTH")]
			public override string PlazaGroupNameTH
			{
				get { return base.PlazaGroupNameTH; }
				set { base.PlazaGroupNameTH = value; }
			}
			/// <summary>
			/// Gets or sets Direction.
			/// </summary>
			[MaxLength(10)]
			[PropertyMapName("Direction")]
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
			[PropertyMapName("PlazaNameEN")]
			public override string PlazaNameEN
			{
				get { return base.PlazaNameEN; }
				set { base.PlazaNameEN = value; }
			}
			/// <summary>
			/// Gets or sets Plaza Name TH.
			/// </summary>
			[MaxLength(100)]
			[PropertyMapName("PlazaNameTH")]
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
			[PropertyMapName("LaneNo")]
			public override int LaneNo
			{
				get { return base.LaneNo; }
				set { base.LaneNo = value; }
			}

			#endregion

			#region Payment

			/// <summary>
			/// Gets or sets Payment Name EN.
			/// </summary>
			[MaxLength(50)]
			[PropertyMapName("PaymentNameEN")]
			public override string PaymentNameEN
			{
				get { return base.PaymentNameEN; }
				set { base.PaymentNameEN = value; }
			}
			/// <summary>
			/// Gets or sets Payment Name TH.
			/// </summary>
			[MaxLength(50)]
			[PropertyMapName("PaymentNameTH")]
			public override string PaymentNameTH
			{
				get { return base.PaymentNameTH; }
				set { base.PaymentNameTH = value; }
			}

			#endregion
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Create Lane Payment.
		/// </summary>
		/// <param name="lane">The Lane instance.</param>
		/// <param name="collector">The User instance.</param>
		/// <param name="payment">The Payment instance.</param>
		/// <param name="date">The payment date time.</param>
		/// <param name="amount">The payment amount.</param>
		/// <returns>Returns LanePayment instance.</returns>
		public static NDbResult<LanePayment> Create(Lane lane, User collector,
			Payment payment, DateTime date, decimal amount)
		{
			var result = new NDbResult<LanePayment>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			LanePayment inst = Create();
			var tsb = TSB.GetCurrent().Value();
			if (null == tsb)
			{
				result.ParameterIsNull();
			}
			else
			{
				// TODO: Assign ReCheck.
				if (null != tsb) tsb.AssignTo(inst);
				if (null != lane) lane.AssignTo(inst);
				if (null != collector) collector.AssignTo(inst);
				if (null != payment) payment.AssignTo(inst);
				inst.PaymentDate = date;
				inst.Amount = amount;
				result.Success(inst);
			}
			return result;
		}
		/// <summary>
		/// Search.
		/// </summary>
		/// <param name="shift">The UserShift instance.</param>
		/// <returns>Returns List of LanePayment.</returns>
		public static NDbResult<List<LanePayment>> Search(UserShift shift)
		{
			var result = new NDbResult<List<LanePayment>>();
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
					cmd += "  FROM LanePaymentView ";
					cmd += " WHERE Begin >= ? ";
					cmd += "   AND End <= ? ";

					var rets = NQuery.Query<FKs>(cmd, shift.Begin, shift.End,
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
		/// <returns>Returns List of LanePayment.</returns>
		public static NDbResult<List<LanePayment>> Search(Lane lane)
		{
			var result = new NDbResult<List<LanePayment>>();
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
					cmd += "  FROM LanePaymentView ";
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
		/// <param name="date">The search date.</param>
		/// <returns>Returns List of LanePayment.</returns>
		public static NDbResult<List<LanePayment>> Search(DateTime date)
		{
			var result = new NDbResult<List<LanePayment>>();
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
					cmd += "  FROM LanePaymentView ";
					cmd += " WHERE Begin >= ? ";
					cmd += "   AND End <= ? ";

					var rets = NQuery.Query<FKs>(cmd, date,
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
		/// Gets Current Lane Payment by Lane.
		/// </summary>
		/// <param name="lane">The Lane Instance.</param>
		/// <returns>Returns LanePayment instance.</returns>
		public static NDbResult<LanePayment> GetCurrentByLane(Lane lane)
		{
			var result = new NDbResult<LanePayment>();
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
					cmd += "  FROM LanePaymentView ";
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

		#endregion
	}

	/// <summary>
	/// The LanePaymentCreate class.
	/// </summary>
	public class LanePaymentCreate
	{
		/// <summary>
		/// Gets or sets Lane.
		/// </summary>
		public Lane Lane { get; set; }
		/// <summary>
		/// Gets or sets User.
		/// </summary>
		public User User { get; set; }
		/// <summary>
		/// Gets or sets Payment.
		/// </summary>
		public Payment Payment { get; set; }
		/// <summary>
		/// Gets or sets Payment date.
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// Gets or sets Payment amount.
		/// </summary>
		public decimal Amount { get; set; }
	}

	#endregion
}
