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
		private decimal _CouponBHT35Factor = 665;
		private decimal _CouponBHT80Factor = 1520;
		private int _CouponBHT35 = 0;
		private int _CouponBHT80 = 0;
		private decimal _CouponBHT35Total = decimal.Zero;
		private decimal _CouponBHT80Total = decimal.Zero;

		private int _CouponTotal = 0;
		private decimal _CouponBHTTotal = decimal.Zero;

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

		#region Private Methods

		private void CalcCouponTotal()
		{
			_CouponBHT35Total = Convert.ToDecimal(_CouponBHT35 * _CouponBHT35Factor);
			_CouponBHT80Total = Convert.ToDecimal(_CouponBHT80 * _CouponBHT80Factor);
			_CouponTotal = _CouponBHT35 + _CouponBHT80;
			decimal total = 0;
			total += _CouponBHT35Total;
			total += _CouponBHT80Total;
			_CouponBHTTotal = total;

			// Raise event.
			this.RaiseChanged("CouponBHT35Total");
			this.RaiseChanged("CouponBHT80Total");
			this.RaiseChanged("CouponTotal");
			this.RaiseChanged("CouponBHTTotal");

		}

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
		/// Gets or sets number of 35 BHT coupon factor.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets number of 35 BHT coupon factor.")]
		[PeropertyMapName("CouponBHT35Factor")]
		public decimal CouponBHT35Factor
		{
			get { return _CouponBHT35Factor; }
			set
			{
				if (_CouponBHT35Factor != value)
				{
					_CouponBHT35Factor = value;
					CalcCouponTotal();
					// Raise event.
					this.RaiseChanged("CouponBHT35Factor");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 80 BHT coupon factor.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets number of 80 BHT coupon factor.")]
		[PeropertyMapName("CouponBHT80Factor")]
		public decimal CouponBHT80Factor
		{
			get { return _CouponBHT80Factor; }
			set
			{
				if (_CouponBHT80Factor != value)
				{
					_CouponBHT80Factor = value;
					CalcCouponTotal();
					// Raise event.
					this.RaiseChanged("CouponBHT80Factor");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 35 BHT coupon.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets number of 35 BHT coupon.")]
		[PeropertyMapName("CouponBHT35")]
		public virtual int CouponBHT35
		{
			get { return _CouponBHT35; }
			set
			{
				if (_CouponBHT35 != value)
				{
					_CouponBHT35 = value;
					CalcCouponTotal();
					// Raise event.
					this.RaiseChanged("CouponBHT35");

				}
			}
		}
		/// <summary>
		/// Gets or sets number of 80 BHT coupon.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets number of 80 BHT coupon.")]
		[PeropertyMapName("CouponBHT80")]
		public virtual int CouponBHT80
		{
			get { return _CouponBHT80; }
			set
			{
				if (_CouponBHT80 != value)
				{
					_CouponBHT80 = value;
					CalcCouponTotal();
					// Raise event.
					this.RaiseChanged("CouponBHT80");
				}
			}
		}
		/// <summary>
		/// Gets calculate coupon total (book count).
		/// </summary>
		[Category("Coupon")]
		[Description("Gets calculate coupon total (book count).")]
		[JsonIgnore]
		[Ignore]
		[PeropertyMapName("CouponTotal")]
		public int CouponTotal
		{
			get { return _CouponTotal; }
			set { }
		}
		/// <summary>
		/// Gets or sets total value in baht.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets total value in baht.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PeropertyMapName("CouponBHTTotal")]
		public decimal CouponBHTTotal
		{
			get { return _CouponBHTTotal; }
			set { }
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
