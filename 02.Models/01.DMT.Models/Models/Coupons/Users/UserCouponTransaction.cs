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

		/// <summary>
		/// The User Coupon Transaction Types
		/// </summary>
		public enum TransactionTypes
		{
			/// <summary>
			/// Borrow
			/// </summary>
			Borrow = 1,
			/// <summary>
			/// Return
			/// </summary>
			Return = 2,
			/// <summary>
			/// Sold
			/// </summary>
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

		#region Public Properties

		#region Common

		/// <summary>
		/// Gets or sets TransactionId
		/// </summary>
		[Category("Common")]
		[Description(" Gets or sets TransactionId")]
		[ReadOnly(true)]
		[PrimaryKey, AutoIncrement]
		[PropertyMapName("TransactionId")]
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
		[PropertyMapName("TransactionDate")]
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
		[PropertyMapName("TransactionType")]
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
		[Ignore]
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
		[Ignore]
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

		#region Coupon

		/// <summary>
		/// Gets or sets coupon serial number.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets coupon serial number.")]
		[ReadOnly(true)]
		[MaxLength(20)]
		[PropertyMapName("CouponId")]
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
		[PropertyMapName("CouponType")]
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
		[PropertyMapName("Price")]
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
		[PropertyMapName("Status", typeof(UserCouponTransaction))]
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
		[PropertyMapName("LastUpdate", typeof(UserCouponTransaction))]
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
		public class FKs : UserCouponTransaction, IFKs<UserCouponTransaction>
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

			#region User

			/// <summary>
			/// Gets or sets Full Name EN.
			/// </summary>
			[MaxLength(100)]
			[PropertyMapName("FullNameEN")]
			public override string FullNameEN
			{
				get { return base.FullNameEN; }
				set { base.FullNameEN = value; }
			}
			/// <summary>
			/// Gets or sets Full Name TH.
			/// </summary>
			[MaxLength(100)]
			[PropertyMapName("FullNameTH")]
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
		/// Gets Active TSB User transactions.
		/// </summary>
		/// <returns>
		/// Returns Current Active TSB's User transactions. If not found returns null.
		/// </returns>
		public static NDbResult<List<UserCouponTransaction>> GetTransactions()
		{
			var result = new NDbResult<List<UserCouponTransaction>>();
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
			result = GetTransactions(tsb);
			return result;
		}
		/// <summary>
		/// Gets User Coupon transactions.
		/// </summary>
		/// <param name="tsb">The target User to get coupon transaction.</param>
		/// <returns>Returns User transactions. If TSB not found returns null.</returns>
		public static NDbResult<List<UserCouponTransaction>> GetTransactions(TSB tsb)
		{
			var result = new NDbResult<List<UserCouponTransaction>>();
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
					cmd += "  FROM UserCouponTransactionView ";
					cmd += " WHERE TSBId = ? ";

					var rets = NQuery.Query<FKs>(cmd, tsb.TSBId).ToList();
					var results = rets.ToModels();
					result.Success(results);
				}
				catch (Exception ex)
				{
					result.Error(ex);
					result.data = new List<UserCouponTransaction>();
				}
				return result;
			}
		}
		/// <summary>
		/// Gets List of BHT35 by User (Active TSB).
		/// </summary>
		/// <param name="user">The user instance.</param>
		/// <returns>Returns List of BHT35.</returns>
		public static NDbResult<List<UserCouponTransaction>> GetUserBHT35Coupons(User user)
		{
			var result = new NDbResult<List<UserCouponTransaction>>();
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
			result = GetUserBHT35Coupons(tsb, user);
			return result;
		}
		/// <summary>
		/// Gets List of BHT35 by User in target TSB.
		/// </summary>
		/// <param name="tsb">The target TSB instance.</param>
		/// <param name="user">The user instance.</param>
		/// <returns>Returns List of BHT35.</returns>
		public static NDbResult<List<UserCouponTransaction>> GetUserBHT35Coupons(
			TSB tsb, User user)
		{
			var result = new NDbResult<List<UserCouponTransaction>>();
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
					cmd += "  FROM UserCouponTransactionView ";
					cmd += " WHERE TSBId = ? ";
					cmd += "   AND CouponType = ? ";
					if (null != user)
					{
						cmd += "   AND UserId = ? ";
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
		/// Gets List of BHT80 by User (Active TSB).
		/// </summary>
		/// <param name="user">The user instance.</param>
		/// <returns>Returns List of BHT80.</returns>
		public static NDbResult<List<UserCouponTransaction>> GetUserBHT80Coupons(User user)
		{
			var result = new NDbResult<List<UserCouponTransaction>>();
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
			result = GetUserBHT80Coupons(tsb, user);
			return result;
		}
		/// <summary>
		/// Gets List of BHT80 by User in target TSB.
		/// </summary>
		/// <param name="tsb">The target TSB instance.</param>
		/// <param name="user">The user instance.</param>
		/// <returns>Returns List of BHT80.</returns>
		public static NDbResult<List<UserCouponTransaction>> GetUserBHT80Coupons(
			TSB tsb, User user)
		{
			var result = new NDbResult<List<UserCouponTransaction>>();
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
					cmd += "  FROM UserCouponTransactionView ";
					cmd += " WHERE TSBId = ? ";
					cmd += "   AND CouponType = ? ";
					if (null != user)
					{
						cmd += "   AND UserId = ? ";
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

		#endregion
	}

	#endregion
}
