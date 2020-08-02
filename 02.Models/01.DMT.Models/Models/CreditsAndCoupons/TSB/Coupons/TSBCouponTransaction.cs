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
	#region TSBCouponTransaction

	/// <summary>
	/// The TSBCouponTransaction Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("TSBCouponTransaction")]
	public class TSBCouponTransaction : NTable<TSBCouponTransaction>
	{
		#region Enum

		public enum TransactionTypes : int
		{
			Initial = 0,
			// received from account
			Received = 1,
			// Sold
			Sold = 2
		}

		#endregion

		#region Internal Variables

		private int _TransactionId = 0;
		private DateTime _TransactionDate = DateTime.MinValue;
		private TransactionTypes _TransactionType = TransactionTypes.Initial;

		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;

		// Coupon 
		private int _CouponId = 0;
		private CouponType _CouponType = CouponType.BHT35;
		private decimal _Factor = 665;

		private string _Remark = string.Empty;

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public TSBCouponTransaction() : base() { }

		#endregion

		#region Public Properties

		#region Common

		/// <summary>
		/// Gets or sets TransactionId
		/// </summary>
		[Category("Common")]
		[Description(" Gets or sets TransactionId")]
		[ReadOnly(true)]
		[PrimaryKey, AutoIncrement]
		[PeropertyMapName("TransactionId")]
		public int TransactionId
		{
			get
			{
				return _TransactionId;
			}
			set
			{
				if (_TransactionId != value)
				{
					_TransactionId = value;
					this.RaiseChanged("TransactionId");
				}
			}
		}
		/// <summary>
		/// Gets or sets Transaction Date.
		/// </summary>
		[Category("Common")]
		[Description(" Gets or sets Transaction Date")]
		[ReadOnly(true)]
		[PeropertyMapName("TransactionDate")]
		public DateTime TransactionDate
		{
			get
			{
				return _TransactionDate;
			}
			set
			{
				if (_TransactionDate != value)
				{
					_TransactionDate = value;
					this.RaiseChanged("TransactionDate");
				}
			}
		}
		/// <summary>
		/// Gets Transaction Date String.
		/// </summary>
		[Category("Common")]
		[Description("Gets Transaction Date String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string TransactionDateString
		{
			get
			{
				var ret = (this.TransactionDate == DateTime.MinValue) ? "" : this.TransactionDate.ToThaiDateTimeString("dd/MM/yyyy");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets Transaction Time String.
		/// </summary>
		[Category("Common")]
		[Description("Gets Transaction Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string TransactionTimeString
		{
			get
			{
				var ret = (this.TransactionDate == DateTime.MinValue) ? "" : this.TransactionDate.ToThaiTimeString();
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets Transaction Date Time String.
		/// </summary>
		[Category("Common")]
		[Description("Gets Transaction Date Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string TransactionDateTimeString
		{
			get
			{
				var ret = (this.TransactionDate == DateTime.MinValue) ? "" : this.TransactionDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets or sets Transaction Type.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Transaction Type.")]
		[ReadOnly(true)]
		[PeropertyMapName("TransactionType")]
		public TransactionTypes TransactionType
		{
			get { return _TransactionType; }
			set
			{
				if (_TransactionType != value)
				{
					_TransactionType = value;
					this.RaiseChanged("TransactionType");
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

		#region Coupon

		/// <summary>
		/// Gets or sets coupon book id.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets coupon book id.")]
		[Unique]
		[ReadOnly(true)]
		[PeropertyMapName("CouponId")]
		public int CouponId
		{
			get { return _CouponId; }
			set
			{
				if (_CouponId != value)
				{
					_CouponId = value;
					// Raise event.
					this.RaiseChanged("CouponId");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of coupon type.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets number of coupon type.")]
		[PeropertyMapName("CouponType")]
		public CouponType CouponType
		{
			get { return _CouponType; }
			set
			{
				if (_CouponType != value)
				{
					_CouponType = value;
					// Raise event.
					this.RaiseChanged("CouponType");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of coupon factor.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets number of coupon factor.")]
		[PeropertyMapName("Factor")]
		public decimal Factor
		{
			get { return _Factor; }
			set
			{
				if (_Factor != value)
				{
					_Factor = value;
					// Raise event.
					this.RaiseChanged("Factor");
				}
			}
		}
		/// <summary>
		/// Gets or sets Remark.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets Remark.")]
		[MaxLength(255)]
		[PeropertyMapName("Remark")]
		public string Remark
		{
			get { return _Remark; }
			set
			{
				if (_Remark != value)
				{
					_Remark = value;
					// Raise event.
					this.RaiseChanged("Remark");
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

		public class FKs : TSBCouponTransaction
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

			#region Public Methods

			public TSBCouponTransaction ToTSBCouponTransaction()
			{
				TSBCouponTransaction inst = new TSBCouponTransaction();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}

			#endregion
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Gets Active TSB Coupon transactions.
		/// </summary>
		/// <returns>
		/// Returns Current Active TSB Coupon transactions. If not found returns null.
		/// </returns>
		public static List<TSBCouponTransaction> Gets()
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return Gets(tsb);
			}
		}
		/// <summary>
		/// Gets TSB Coupon transactions.
		/// </summary>
		/// <param name="tsb">The target TSB to get coupon transaction.</param>
		/// <returns>Returns TSB Coupon transactions. If TSB not found returns null.</returns>
		public static List<TSBCouponTransaction> Gets(TSB tsb)
		{
			if (null == tsb) return null;
			lock (sync)
			{
				string cmd = string.Empty;
				cmd += "SELECT TSBCouponTransaction.* ";
				cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
				cmd += "  FROM TSBCouponTransaction, TSB ";
				cmd += " WHERE TSBCouponTransaction.TSBId = TSB.TSBId ";
				cmd += "   AND TSBCouponTransaction.TSBId = ? ";

				var rets = NQuery.Query<FKs>(cmd, tsb.TSBId).ToList();
				if (null == rets)
				{
					return new List<TSBCouponTransaction>();
				}
				else
				{
					var results = new List<TSBCouponTransaction>();
					rets.ForEach(ret =>
					{
						results.Add(ret.ToTSBCouponTransaction());
					});
					return results;
				}
			}
		}

		public static TSBCouponTransaction GetInitial()
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return GetInitial(tsb);
			}
		}

		public static TSBCouponTransaction GetInitial(TSB tsb)
		{
			if (null == tsb) return null;
			lock (sync)
			{
				string cmd = string.Empty;
				cmd += "SELECT TSBCouponTransaction.* ";
				cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
				cmd += "  FROM TSBCouponTransaction, TSB ";
				cmd += " WHERE TSBCouponTransaction.TSBId = TSB.TSBId ";
				cmd += "   AND TSBCouponTransaction.TSBId = ? ";
				cmd += "   AND TSBCouponTransaction.TransactionType = ? ";

				var ret = NQuery.Query<FKs>(cmd,
					tsb.TSBId, TransactionTypes.Initial).FirstOrDefault();
				TSBCouponTransaction inst;
				if (null == ret)
				{
					inst = Create();
					tsb.AssignTo(inst);
					inst.TransactionType = TransactionTypes.Initial;
				}
				else
				{
					inst = ret.ToTSBCouponTransaction();
				}
				return inst;
			}
		}

		#endregion
	}

	#endregion
}
