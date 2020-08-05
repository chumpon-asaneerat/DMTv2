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
	#region TSBCouponSummary (For Query only)

	/// <summary>
	/// The TSBCouponSummary Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("TSBCouponSummary")]
	public class TSBCouponSummary : NTable<TSBCouponSummary>
	{
		#region Internal Variables

		// For Runtime Used
		private string _description = string.Empty;
		private bool _hasRemark = false;

		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;

		private string _UserId = string.Empty;
		private string _FullNameEN = string.Empty;
		private string _FullNameTH = string.Empty;

		private int _CountCouponBHT35 = 0;
		private int _CountCouponBHT80 = 0;

		private decimal _AmountCouponBHT35 = decimal.Zero;
		private decimal _AmountCouponBHT80 = decimal.Zero;

		private int _CountCouponTotal = 0;
		private decimal _CouponBHTTotal = decimal.Zero;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public TSBCouponSummary() : base() { }

		#endregion

		#region Private Methods

		private void CalcCouponTotal()
		{
			_CountCouponTotal = _CountCouponBHT35 + _CountCouponBHT80;
			// Raise event.
			this.RaiseChanged("CountCouponTotal");

		}

		private void CalcAmountTotal()
		{
			_CouponBHTTotal = _AmountCouponBHT35 + _AmountCouponBHT80;
			// Raise event.
			this.RaiseChanged("CouponBHTTotal");
		}

		#endregion

		#region Public Properties

		#region Runtime

		/// <summary>
		/// Gets or sets has remark.
		/// </summary>
		[Category("Runtime")]
		[Description("Gets or sets HasRemark.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("Description")]
		public string Description
		{
			get { return _description; }
			set
			{
				if (_description != value)
				{
					_description = value;
					// Raise event.
					this.RaiseChanged("Description");
				}
			}
		}
		/// <summary>
		/// Gets or sets has remark.
		/// </summary>
		[Category("Runtime")]
		[Description("Gets or sets HasRemark.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("HasRemark")]
		public bool HasRemark
		{
			get { return _hasRemark; }
			set
			{
				if (_hasRemark != value)
				{
					_hasRemark = value;
					// Raise event.
					this.RaiseChanged("HasRemark");
					this.RaiseChanged("RemarkVisibility");
				}
			}
		}

		[Category("Runtime")]
		[Description("Gets or sets RemarkVisibility.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("RemarkVisibility")]
		public System.Windows.Visibility RemarkVisibility
		{
			get { return (_hasRemark) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; }
			set { }
		}

		#endregion

		#region TSB

		/// <summary>
		/// Gets or sets TSBId.
		/// </summary>
		[Category("TSB")]
		[Description("Gets or sets TSBId.")]
		[ReadOnly(true)]
		[Ignore]
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
		/// Gets or sets number of 35 BHT coupon.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets number of 35 BHT coupon.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("CouponBHT35")]
		public virtual int CountCouponBHT35
		{
			get { return _CountCouponBHT35; }
			set
			{
				if (_CountCouponBHT35 != value)
				{
					_CountCouponBHT35 = value;
					CalcCouponTotal();
					// Raise event.
					this.RaiseChanged("CountCouponBHT35");

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
		[PeropertyMapName("CountCouponBHT80")]
		public virtual int CountCouponBHT80
		{
			get { return _CountCouponBHT80; }
			set
			{
				if (_CountCouponBHT80 != value)
				{
					_CountCouponBHT80 = value;
					CalcCouponTotal();
					// Raise event.
					this.RaiseChanged("CountCouponBHT80");
				}
			}
		}
		/// <summary>
		/// Gets or sets Amount of 35 BHT coupon.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets Amount of 35 BHT coupon.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AmountCouponBHT35")]
		public virtual decimal AmountCouponBHT35
		{
			get { return _AmountCouponBHT35; }
			set
			{
				if (_AmountCouponBHT35 != value)
				{
					_AmountCouponBHT35 = value;
					CalcAmountTotal();
					// Raise event.
					this.RaiseChanged("AmountCouponBHT35");

				}
			}
		}
		/// <summary>
		/// Gets or sets Amount of 80 BHT coupon.
		/// </summary>
		[Category("Coupon")]
		[Description("Gets or sets Amount of 80 BHT coupon.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AmountCouponBHT80")]
		public virtual decimal AmountCouponBHT80
		{
			get { return _AmountCouponBHT80; }
			set
			{
				if (_AmountCouponBHT80 != value)
				{
					_AmountCouponBHT80 = value;
					CalcAmountTotal();
					// Raise event.
					this.RaiseChanged("AmountCouponBHT80");
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
		public int CountCouponTotal
		{
			get { return _CountCouponTotal; }
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
			set { }
		}

		#endregion

		#endregion

		#region Internal Class

		public class FKs : TSBCouponSummary
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
			/// Gets or sets UserId
			/// </summary>
			[MaxLength(10)]
			[PeropertyMapName("UserId")]
			public override string UserId
			{
				get { return base.UserId; }
				set { base.UserId = value; }
			}
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

			#region Coupon

			/// <summary>
			/// Gets or sets number of 35 BHT coupon.
			/// </summary>
			[PeropertyMapName("CountCouponBHT35")]
			public override int CountCouponBHT35
			{
				get { return base.CountCouponBHT35; }
				set { base.CountCouponBHT35 = value; }
			}
			/// <summary>
			/// Gets or sets number of 80 BHT coupon.
			/// </summary>
			[PeropertyMapName("CountCouponBHT80")]
			public override int CountCouponBHT80
			{
				get { return base.CountCouponBHT80; }
				set { base.CountCouponBHT80 = value; }
			}
			/// <summary>
			/// Gets or sets Amount of 35 BHT coupon.
			/// </summary>
			[PeropertyMapName("AmountCouponBHT35")]
			public override decimal AmountCouponBHT35
			{
				get { return base.AmountCouponBHT35; }
				set { base.AmountCouponBHT35 = value; }
			}
			/// <summary>
			/// Gets or sets Amount of 80 BHT coupon.
			/// </summary>
			[PeropertyMapName("AmountCouponBHT80")]
			public override decimal AmountCouponBHT80
			{
				get { return base.AmountCouponBHT80; }
				set { base.AmountCouponBHT80 = value; }
			}

			#endregion

			#region Public Methods

			public TSBCouponSummary ToTSBCouponSummary()
			{
				TSBCouponSummary inst = new TSBCouponSummary();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}

			#endregion
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Gets Active TSB Coupon summaries.
		/// </summary>
		/// <returns>Returns Current Active TSB Coupon balance. If not found returns null.</returns>
		public static List<TSBCouponSummary> GetTSBCouponSummaries()
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return GetTSBCouponSummaries(tsb);
			}
		}
		/// <summary>
		/// Gets TSB Coupon summaries.
		/// </summary>
		/// <param name="tsb">The target TSB to get balance.</param>
		/// <returns>Returns List of TSB Coupon balance. If TSB not found returns null.</returns>
		public static List<TSBCouponSummary> GetTSBCouponSummaries(TSB tsb)
		{
			if (null == tsb) return new List<TSBCouponSummary>();
			lock (sync)
			{
				string cmd = @"
					SELECT * 
					  FROM TSBCouponSummarryView
					 WHERE TSBCouponSummarryView.TSBId = ?
				";
				var rets = NQuery.Query<FKs>(cmd, tsb.TSBId).ToList();
				var results = new List<TSBCouponSummary>();
				if (null != rets)
				{
					rets.ForEach(ret =>
					{
						results.Add(ret.ToTSBCouponSummary());
					});
				}
				return results;
			}
		}
		/// <summary>
		/// Gets TSB Coupon summaries.
		/// </summary>
		/// <param name="tsb">The target TSB to get balance.</param>
		/// <param name="user">The target User to get balance.</param>
		/// <returns>Returns List of TSB Coupon balance. If TSB not found returns null.</returns>
		public static List<TSBCouponSummary> GetByUser(User user)
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return GetByUser(tsb, user);
			}
		}
		/// <summary>
		/// Gets TSB Coupon summaries.
		/// </summary>
		/// <param name="tsb">The target TSB to get balance.</param>
		/// <param name="user">The target User to get balance.</param>
		/// <returns>Returns List of TSB Coupon balance. If TSB not found returns null.</returns>
		public static List<TSBCouponSummary> GetByUser(TSB tsb, User user)
		{
			if (null == tsb || null == user) return new List<TSBCouponSummary>();
			lock (sync)
			{
				string cmd = @"
					SELECT * 
					  FROM TSBCouponSummarryView
					 WHERE TSBCouponSummarryView.TSBId = ?
					   AND TSBCouponSummarryView.UserId = ?
				";
				var rets = NQuery.Query<FKs>(cmd, tsb.TSBId, user.UserId).ToList();
				var results = new List<TSBCouponSummary>();
				if (null != rets)
				{
					rets.ForEach(ret =>
					{
						results.Add(ret.ToTSBCouponSummary());
					});
				}
				return results;
			}
		}


		#endregion
	}

	#endregion
}
