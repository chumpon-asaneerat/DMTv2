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
	#region UserCouponTransaction

	/// <summary>
	/// The UserCouponTransaction Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("UserCouponTransaction")]
	public class UserCouponTransaction : NTable<UserCouponTransaction>
	{
		#region Enum

		public enum TransactionTypes
		{
			Borrow = 1,
			Return = 2,
			Sold = 3
		}

		#endregion

		#region Internal Variables

		private int _TransactionId = 0;
		private DateTime _TransactionDate = DateTime.MinValue;
		private TransactionTypes _TransactionType = TransactionTypes.Borrow;

		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;

		private string _UserId = string.Empty;
		private string _FullNameEN = string.Empty;
		private string _FullNameTH = string.Empty;

		// Coupon 
		private string _CouponId = string.Empty;
		private CouponType _CouponType = CouponType.BHT35;
		private decimal _Price = 665;

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public UserCouponTransaction() : base() { }

		#endregion

		#region Private Methods

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
					this.RaiseChanged("TransactionDateString");
					this.RaiseChanged("TransactionTimeString");
					this.RaiseChanged("TransactionDateTimeString");
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

		public class FKs : UserCouponTransaction
		{
			#region TSB

			/// <summary>
			/// Gets or sets TSBId.
			/// </summary>
			[MaxLength(10)]
			[PeropertyMapName("TSBId")]
			public override string TSBId
			{
				get { return base.TSBId; }
				set { base.TSBId = value; }
			}
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

			#region Public Methods

			public UserCouponTransaction ToUserCouponTransaction()
			{
				UserCouponTransaction inst = new UserCouponTransaction();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}

			#endregion
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Gets Active TSB User transactions.
		/// </summary>
		/// <returns>
		/// Returns Current Active TSB's User transactions. If not found returns null.
		/// </returns>
		public static NDbResult<List<UserCouponTransaction>> Gets()
		{
			var result = new NDbResult<List<UserCouponTransaction>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = null;
				return result;
			}

			lock (sync)
			{
				var tsbRet = TSB.GetCurrent();
				if (null != tsbRet && !tsbRet.errors.hasError)
				{
					var tsb = tsbRet.data;
					return Gets(tsb);
				}
				else
				{
					result.Error(new Exception("Cannot get active TSB."));
					result.errors.errNum = -20;
					result.data = null;
				}
				return result;
			}
		}

		/// <summary>
		/// Gets User Coupon transactions.
		/// </summary>
		/// <param name="tsb">The target User to get coupon transaction.</param>
		/// <returns>Returns User transactions. If TSB not found returns null.</returns>
		public static NDbResult<List<UserCouponTransaction>> Gets(TSB tsb)
		{
			var result = new NDbResult<List<UserCouponTransaction>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = null;
				return result;
			}

			if (null == tsb)
			{
				result.ParameterIsNull();
				result.data = new List<UserCouponTransaction>();
				return result;
			}
			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT UserCouponTransaction.* ";
					cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
					cmd += "     , User.FullNameEN, User.FullNameTH ";
					cmd += "  FROM UserCouponTransaction, TSB, User ";
					cmd += " WHERE UserCouponTransaction.TSBId = TSB.TSBId ";
					cmd += "   AND UserCouponTransaction.UserId = User.UserId ";
					cmd += "   AND UserCouponTransaction.TSBId = ? ";

					var rets = NQuery.Query<FKs>(cmd, tsb.TSBId).ToList();
					if (null == rets)
					{
						result.data = new List<UserCouponTransaction>();
						result.Success();
					}
					else
					{
						var results = new List<UserCouponTransaction>();
						rets.ForEach(ret =>
						{
							results.Add(ret.ToUserCouponTransaction());
						});
						result.data = results;
						result.Success();
					}
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = new List<UserCouponTransaction>();
				}
				return result;
			}
		}

		public static NDbResult<List<UserCouponTransaction>> GetUserBHT35Coupons(User user)
		{
			var result = new NDbResult<List<UserCouponTransaction>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = null;
				return result;
			}

			lock (sync)
			{
				var tsbRet = TSB.GetCurrent();
				if (null != tsbRet && !tsbRet.errors.hasError)
				{
					var tsb = tsbRet.data;
					return GetUserBHT35Coupons(tsb, user);
				}
				else
				{
					result.Error(new Exception("Cannot get active TSB."));
					result.errors.errNum = -20;
					result.data = null;
				}
				return result;
			}
		}

		public static NDbResult<List<UserCouponTransaction>> GetUserBHT35Coupons(
			TSB tsb, User user)
		{
			var result = new NDbResult<List<UserCouponTransaction>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<UserCouponTransaction>();
				return result;
			}

			if (null == tsb)
			{
				result.ParameterIsNull();
				result.data = new List<UserCouponTransaction>();
				return result;
			}
			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT UserCouponTransaction.* ";
					cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
					cmd += "     , User.FullNameEN, User.FullNameTH ";
					cmd += "  FROM UserCouponTransaction, TSB, User ";
					cmd += " WHERE UserCouponTransaction.TSBId = TSB.TSBId ";
					cmd += "   AND UserCouponTransaction.UserId = User.UserId ";
					cmd += "   AND UserCouponTransaction.TSBId = ? ";
					cmd += "   AND UserCouponTransaction.CouponType = ? ";
					if (null != user)
					{
						cmd += "   AND UserCouponTransaction.UserId = ? ";
					}

					List<FKs> rets;
					if (null != user)
					{
						rets = NQuery.Query<FKs>(cmd,
							tsb.TSBId, CouponType.BHT35, user.UserId).ToList();
					}
					else
					{
						rets = NQuery.Query<FKs>(cmd, tsb.TSBId, CouponType.BHT35).ToList();
					}
					if (null == rets)
					{
						result.data = new List<UserCouponTransaction>();
						result.Success();
					}
					else
					{
						var results = new List<UserCouponTransaction>();
						rets.ForEach(ret =>
						{
							results.Add(ret.ToUserCouponTransaction());
						});
						result.data = results;
						result.Success();
					}
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = new List<UserCouponTransaction>();
				}
				return result;
			}
		}

		public static NDbResult<List<UserCouponTransaction>> GetUserBHT80Coupons(User user)
		{
			var result = new NDbResult<List<UserCouponTransaction>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<UserCouponTransaction>();
				return result;
			}

			lock (sync)
			{
				var tsbRet = TSB.GetCurrent();
				if (null != tsbRet && !tsbRet.errors.hasError)
				{
					var tsb = tsbRet.data;
					return GetUserBHT80Coupons(tsb, user);
				}
				else
				{
					result.Error(new Exception("Cannot get active TSB."));
					result.errors.errNum = -20;
					result.data = null;
				}
				return result;
			}
		}

		public static NDbResult<List<UserCouponTransaction>> GetUserBHT80Coupons(
			TSB tsb, User user)
		{
			var result = new NDbResult<List<UserCouponTransaction>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = new List<UserCouponTransaction>();
				return result;
			}

			if (null == tsb)
			{
				result.ParameterIsNull();
				result.data = new List<UserCouponTransaction>();
				return result;
			}
			lock (sync)
			{
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT UserCouponTransaction.* ";
					cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
					cmd += "     , User.FullNameEN, User.FullNameTH ";
					cmd += "  FROM UserCouponTransaction, TSB, User ";
					cmd += " WHERE UserCouponTransaction.TSBId = TSB.TSBId ";
					cmd += "   AND UserCouponTransaction.UserId = User.UserId ";
					cmd += "   AND UserCouponTransaction.TSBId = ? ";
					cmd += "   AND UserCouponTransaction.CouponType = ? ";
					if (null != user)
					{
						cmd += "   AND UserCouponTransaction.UserId = ? ";
					}
					List<FKs> rets;
					if (null != user)
					{
						rets = NQuery.Query<FKs>(cmd,
							tsb.TSBId, CouponType.BHT80, user.UserId).ToList();
					}
					else
					{
						rets = NQuery.Query<FKs>(cmd, tsb.TSBId, CouponType.BHT80).ToList();
					}
					if (null == rets)
					{
						result.data = new List<UserCouponTransaction>();
						result.Success();
					}
					else
					{
						var results = new List<UserCouponTransaction>();
						rets.ForEach(ret =>
						{
							results.Add(ret.ToUserCouponTransaction());
						});
						result.data = results;
						result.Success();
					}
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = new List<UserCouponTransaction>();
				}
				return result;
			}
		}

		public static NDbResult<TSBCouponTransaction> UserBorrowCoupons(
			User user, List<TSBCouponTransaction> coupons)
		{
			var result = new NDbResult<TSBCouponTransaction>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = null;
				return result;
			}

			if (null == user || null == coupons || coupons.Count <= 0)
			{
				result.ParameterIsNull();
				result.data = null;
				return result;
			}
			lock (sync)
			{
				try
				{
					DateTime receivedDate = DateTime.Now;
					coupons.ForEach(coupon =>
					{
						coupon.TransactionType = TSBCouponTransaction.TransactionTypes.Lane;
						coupon.UserId = user.UserId;
						coupon.UserReceiveDate = receivedDate;
						TSBCouponTransaction.Save(coupon);
						var inst = new UserCouponTransaction();
						inst._TransactionDate = DateTime.Now;
						inst.TransactionType = TransactionTypes.Borrow;
						inst.TSBId = coupon.TSBId;
						inst.UserId = user.UserId;
						inst.CouponId = coupon.CouponId;
						inst.CouponType = coupon.CouponType;
						inst.Price = coupon.Price;
						UserCouponTransaction.Save(inst);
						result.data = null;
						result.Success();
					});
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = null;
				}
				return result;
			}
		}

		public static NDbResult<TSBCouponTransaction> UserReturnCoupons(User user, List<TSBCouponTransaction> coupons)
		{
			var result = new NDbResult<TSBCouponTransaction>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = null;
				return result;
			}

			if (null == user || null == coupons || coupons.Count <= 0)
			{
				result.ParameterIsNull();
				result.data = null;
				return result;
			}
			lock (sync)
			{
				try
				{
					coupons.ForEach(coupon =>
					{
						coupon.TransactionType = TSBCouponTransaction.TransactionTypes.Stock;
						TSBCouponTransaction.Save(coupon);
						var inst = new UserCouponTransaction();
						inst._TransactionDate = DateTime.Now;
						inst.TransactionType = TransactionTypes.Return;
						inst.TSBId = coupon.TSBId;
						inst.UserId = user.UserId;
						inst.CouponId = coupon.CouponId;
						inst.CouponType = coupon.CouponType;
						inst.Price = coupon.Price;
						UserCouponTransaction.Save(inst);
						result.data = null;
						result.Success();
					});
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = null;
				}
				return result;
			}
		}

		public static NDbResult<TSBCouponTransaction> Sold(UserCouponTransaction coupon)
		{
			var result = new NDbResult<TSBCouponTransaction>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = null;
				return result;
			}

			if (null == coupon)
			{
				result.ParameterIsNull();
				result.data = null;
				return result;
			}
			/*
			lock (sync)
			{
				coupon.TransactionType = UserCouponTransaction.TransactionTypes.Sold;
				UserCouponTransaction.Save(coupon);
				var tsbCoupon = TSBCouponTransaction.FindById(coupon.CouponId);
				if (null != tsbCoupon)
				{
					TSBCouponTransaction.Sold(tsbCoupon);
				}
			}
			*/
			return result;
		}

		#endregion
	}

	#endregion
}
