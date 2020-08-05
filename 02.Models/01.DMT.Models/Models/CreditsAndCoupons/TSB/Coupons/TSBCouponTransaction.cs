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
using System.Net;

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
			// received from account
			Received = 0,
			// User On Hand
			User = 1,
			// Sold
			Sold = 2
		}

		#endregion

		#region Internal Variables

		private int _TransactionId = 0;
		private DateTime _TransactionDate = DateTime.MinValue;
		private TransactionTypes _TransactionType = TransactionTypes.Received;

		// Coupon 
		private string _CouponId = string.Empty;
		private CouponType _CouponType = CouponType.BHT35;
		private decimal _Price = 665;
		// TSB
		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;
		// User
		private string _UserId = string.Empty;
		private string _FullNameEN = string.Empty;
		private string _FullNameTH = string.Empty;
		// Sold
		private DateTime _SoldDate = DateTime.MinValue;
		private string _SoldBy = string.Empty;
		private string _SoldByFullNameEN = string.Empty;
		private string _SoldByFullNameTH = string.Empty;

		private bool _IsFinished = false;
		//private string _Remark = string.Empty;

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
		/// <summary>
		/// Gets or sets Is Finished (0: not finished, 1: already finished).
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Is Finished (0: not finished, 1: already finished).")]
		[ReadOnly(true)]
		[PeropertyMapName("IsFinished")]
		public virtual bool IsFinished
		{
			get
			{
				return _IsFinished;
			}
			set
			{
				if (_IsFinished != value)
				{
					_IsFinished = value;
					this.RaiseChanged("IsFinished");
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
		[ReadOnly(true)]
		[MaxLength(20)]
		[PeropertyMapName("CouponId")]
		public string CouponId
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
		[ReadOnly(true)]
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
					this.RaiseChanged("CouponTypeString");
				}
			}
		}
		/// <summary>
		/// Gets Coupon Type String.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets Coupon Type String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string CouponTypeString
		{
			get
			{
				if (CouponType == CouponType.BHT35)
					return "35";
				else if (CouponType == CouponType.BHT80)
					return "80";
				else return "N/A"; // N/A
			}
			set { }
		}
		/// <summary>
		/// Gets or sets number of coupon price.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets number of coupon price.")]
		[ReadOnly(true)]
		[PeropertyMapName("Price")]
		public decimal Price
		{
			get { return _Price; }
			set
			{
				if (_Price != value)
				{
					_Price = value;
					// Raise event.
					this.RaiseChanged("Price");
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
		[ReadOnly(true)]
		[MaxLength(10)]
		[PeropertyMapName("UserId")]
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
		[Category("User")]
		[Description("Gets or sets FullNameTH")]
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

		#region Sold

		/// <summary>
		/// Gets or sets SoldBy (UserId)
		/// </summary>
		[Category("Sold")]
		[Description("Gets or sets SoldBy (UserId)")]
		[ReadOnly(true)]
		[MaxLength(10)]
		[PeropertyMapName("SoldBy")]
		public virtual string SoldBy
		{
			get
			{
				return _SoldBy;
			}
			set
			{
				if (_SoldBy != value)
				{
					_SoldBy = value;
					this.RaiseChanged("UserId");
				}
			}
		}
		/// <summary>
		/// Gets or sets Sold User FullNameEN
		/// </summary>
		[Category("Sold")]
		[Description("Gets or sets Sold User FullNameEN")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("SoldByFullNameEN")]
		public virtual string SoldByFullNameEN
		{
			get
			{
				return _SoldByFullNameEN;
			}
			set
			{
				if (_SoldByFullNameEN != value)
				{
					_SoldByFullNameEN = value;
					this.RaiseChanged("SoldByFullNameEN");
				}
			}
		}
		/// <summary>
		/// Gets or sets Sold User FullNameTH
		/// </summary>
		[Category("Sold")]
		[Description("Gets or sets Sold User FullNameTH")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("SoldByFullNameTH")]
		public virtual string SoldByFullNameTH
		{
			get
			{
				return _SoldByFullNameTH;
			}
			set
			{
				if (_SoldByFullNameTH != value)
				{
					_SoldByFullNameTH = value;
					this.RaiseChanged("SoldByFullNameTH");
				}
			}
		}
		/// <summary>
		/// Gets or sets Sold Date.
		/// </summary>
		[Category("Sold")]
		[Description(" Gets or sets Sold Date")]
		[ReadOnly(true)]
		[PeropertyMapName("SoldDate")]
		public DateTime SoldDate
		{
			get
			{
				return _SoldDate;
			}
			set
			{
				if (_SoldDate != value)
				{
					_SoldDate = value;
					this.RaiseChanged("SoldDate");
				}
			}
		}
		/// <summary>
		/// Gets Sold Date String.
		/// </summary>
		[Category("Common")]
		[Description("Gets Sold Date String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string SoldDateString
		{
			get
			{
				var ret = (this.SoldDate == DateTime.MinValue) ? "" : this.SoldDate.ToThaiDateTimeString("dd/MM/yyyy");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets Sold Time String.
		/// </summary>
		[Category("Sold")]
		[Description("Gets Sold Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string SoldTimeString
		{
			get
			{
				var ret = (this.SoldDate == DateTime.MinValue) ? "" : this.SoldDate.ToThaiTimeString();
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets Sold Date Time String.
		/// </summary>
		[Category("Sold")]
		[Description("Gets Sold Date Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string SoldDateTimeString
		{
			get
			{
				var ret = (this.SoldDate == DateTime.MinValue) ? "" : this.SoldDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
				return ret;
			}
			set { }
		}

		#endregion

		#region Remark
		/*
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
		*/
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

		public static List<TSBCouponTransaction> GetTSBCoupons()
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return GetTSBCoupons(tsb);
			}
		}

		public static List<TSBCouponTransaction> GetTSBCoupons(TSB tsb)
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

				var rets = NQuery.Query<FKs>(cmd,
					tsb.TSBId, TransactionTypes.Received).ToList();

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

		public static List<TSBCouponTransaction> GetBHT35Coupons()
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return GetTSBBHT35Coupons(tsb);
			}
		}

		public static List<TSBCouponTransaction> GetTSBBHT35Coupons(TSB tsb)
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
				cmd += "   AND TSBCouponTransaction.CouponType = ? ";
				cmd += "   AND TSBCouponTransaction.TransactionType = ? ";

				var rets = NQuery.Query<FKs>(cmd,
					tsb.TSBId, CouponType.BHT35, TransactionTypes.Received).ToList();
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

		public static List<TSBCouponTransaction> GetTSBBHT80Coupons()
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return GetTSBBHT80Coupons(tsb);
			}
		}

		public static List<TSBCouponTransaction> GetTSBBHT80Coupons(TSB tsb)
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
				cmd += "   AND TSBCouponTransaction.CouponType = ? ";
				cmd += "   AND TSBCouponTransaction.TransactionType = ? ";

				var rets = NQuery.Query<FKs>(cmd, 
					tsb.TSBId, CouponType.BHT80, TransactionTypes.Received).ToList();
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

		public static List<TSBCouponTransaction> GetTSBSoldCoupons()
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return GetTSBSoldCoupons(tsb);
			}
		}

		public static List<TSBCouponTransaction> GetTSBSoldCoupons(TSB tsb)
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

				var rets = NQuery.Query<FKs>(cmd,
					tsb.TSBId, TransactionTypes.Sold).ToList();

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

		public static TSBCouponTransaction FindById(string couponId)
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return FindById(tsb, couponId);
			}
		}

		public static TSBCouponTransaction FindById(TSB tsb, string couponId)
		{
			lock (sync)
			{
				string cmd = string.Empty;
				cmd += "SELECT TSBCouponTransaction.* ";
				cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
				cmd += "  FROM TSBCouponTransaction, TSB ";
				cmd += " WHERE TSBCouponTransaction.TSBId = TSB.TSBId ";
				cmd += "   AND TSBCouponTransaction.TSBId = ? ";
				cmd += "   AND TSBCouponTransaction.CouponId = ? ";

				var ret = NQuery.Query<FKs>(cmd,
					tsb.TSBId, couponId).FirstOrDefault();
				return (null != ret) ? ret.ToTSBCouponTransaction() : null;
			}
		}

		public static List<TSBCouponTransaction> ToTSBBHT35Coupons(List<UserCouponTransaction> coupons)
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return ToTSBBHT35Coupons(tsb, coupons);
			}
		}

		public static List<TSBCouponTransaction> ToTSBBHT35Coupons(TSB tsb, List<UserCouponTransaction> coupons)
		{
			List<TSBCouponTransaction> results = new List<TSBCouponTransaction>();
			if (null == tsb || null == coupons || coupons.Count <= 0) return results;

			coupons.ForEach(coupon => 
			{
				var inst = FindById(coupon.CouponId);
				if (null != inst)
				{
					results.Add(inst);
				}
			});

			return results;
		}

		public static List<TSBCouponTransaction> ToTSBBHT80Coupons(List<UserCouponTransaction> coupons)
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return ToTSBBHT80Coupons(tsb, coupons);
			}
		}

		public static List<TSBCouponTransaction> ToTSBBHT80Coupons(TSB tsb, List<UserCouponTransaction> coupons)
		{
			List<TSBCouponTransaction> results = new List<TSBCouponTransaction>();
			if (null == tsb || null == coupons || coupons.Count <= 0) return results;

			coupons.ForEach(coupon =>
			{
				var inst = FindById(coupon.CouponId);
				if (null != inst)
				{
					results.Add(inst);
				}
			});

			return results;
		}

		public static void Sold(TSBCouponTransaction coupon)
		{
			if (null == coupon) return;
			lock (sync)
			{
				coupon.TransactionType = TSBCouponTransaction.TransactionTypes.Sold;
				TSBCouponTransaction.Save(coupon);
			}
		}

		#endregion
	}

	#endregion
}
