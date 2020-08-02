﻿#region Using

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
	#region UserCoupon

	/// <summary>
	/// The UserCoupon Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("UserCoupon")]
	public class UserCoupon : NTable<UserCoupon>
	{
		#region Enum

		public enum StateTypes
		{
			Borrow = 1,
			Return = 2
		}

		#endregion

		#region Internal Variables

		private int _UserCouponId = 0;
		private DateTime _UserCouponDate = DateTime.MinValue;
		private StateTypes _State = StateTypes.Borrow;

		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;

		private string _UserId = string.Empty;
		private string _FullNameEN = string.Empty;
		private string _FullNameTH = string.Empty;

		private int _CouponBHT35 = 0;
		private int _CouponBHT80 = 0;
		private int _CouponTotal = 0;
		private decimal _CouponBHTTotal = decimal.Zero;

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public UserCoupon() : base() { }

		#endregion

		#region Private Methods

		private void CalcCouponTotal()
		{
			_CouponTotal = _CouponBHT35 + _CouponBHT80;
			// Raise event.
			this.RaiseChanged("CouponTotal");

		}

		#endregion

		#region Public Properties

		#region Common

		/// <summary>
		/// Gets or sets UserCouponId
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets UserCouponId")]
		[PrimaryKey, AutoIncrement]
		[ReadOnly(true)]
		[PeropertyMapName("UserCouponId")]
		public int UserCouponId
		{
			get
			{
				return _UserCouponId;
			}
			set
			{
				if (_UserCouponId != value)
				{
					_UserCouponId = value;
					this.RaiseChanged("UserCouponId");
				}
			}
		}
		/// <summary>
		/// Gets or sets UserCoupon Date.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets UserCredit Date.")]
		[ReadOnly(true)]
		[PeropertyMapName("UserCouponDate")]
		public DateTime UserCouponDate
		{
			get { return _UserCouponDate; }
			set
			{
				if (_UserCouponDate != value)
				{
					_UserCouponDate = value;
					// Raise event.
					this.RaiseChanged("UserCouponDate");
					this.RaiseChanged("UserCouponDateString");
					this.RaiseChanged("UserCouponDateTimeString");
				}
			}
		}
		/// <summary>
		/// Gets UserCoupon Date String.
		/// </summary>
		[Category("Common")]
		[Description("Gets UserCoupon Date String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string UserCouponDateString
		{
			get
			{
				var ret = (this.UserCouponDate == DateTime.MinValue) ? "" : this.UserCouponDate.ToThaiDateTimeString("dd/MM/yyyy");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets UserCoupon DateTime String.
		/// </summary>
		[Category("Common")]
		[Description("Gets UserCoupon DateTime String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string UserCouponDateTimeString
		{
			get
			{
				var ret = (this.UserCouponDate == DateTime.MinValue) ? "" : this.UserCouponDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets or sets State.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets State.")]
		[Browsable(false)]
		[PeropertyMapName("State")]
		public StateTypes State
		{
			get { return _State; }
			set
			{
				if (_State != value)
				{
					_State = value;
					// Raise event.
					this.RaiseChanged("State");
					this.RaiseChanged("ReceivedBagVisibility");
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

		#region User

		/// <summary>
		/// Gets or sets UserId
		/// </summary>
		[Category("User")]
		[Description("Gets or sets UserId")]
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
		/// Gets or sets number of 35 BHT coupon.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets number of 35 BHT coupon.")]
		[ReadOnly(true)]
		[Ignore]
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
		[ReadOnly(true)]
		[Ignore]
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
		[ReadOnly(true)]
		[Ignore]
		[JsonIgnore]
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
		[Ignore]
		[PeropertyMapName("CouponBHTTotal")]
		public virtual decimal CouponBHTTotal
		{
			get { return _CouponBHTTotal; }
			set
			{
				if (_CouponBHTTotal != value)
				{
					_CouponBHTTotal = value;
					// Raise event.
					this.RaiseChanged("CouponBHTTotal");
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

		public class FKs : UserCoupon
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

			public UserCoupon ToUserCoupon()
			{
				UserCoupon inst = new UserCoupon();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}

			#endregion
		}

		#endregion

		#region Static Methods

		#endregion
	}

	#endregion
}
