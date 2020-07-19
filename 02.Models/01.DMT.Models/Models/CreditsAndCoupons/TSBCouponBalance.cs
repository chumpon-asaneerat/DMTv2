#region Using

using System;
using System.Collections.Generic;
using System.Linq;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;

// required for JsonIgnore.
using Newtonsoft.Json;
using NLib;
using NLib.Reflection;
using System.ComponentModel;

#endregion

namespace DMT.Models
{
	#region TSBCouponBalance (For Query only)

	/// <summary>
	/// The TSBCouponBalance Data Model class.
	/// </summary>
	//[Table("TSBCouponBalance")]
	public class TSBCouponBalance : NTable<TSBCouponBalance>
	{
		#region Internal Variables

		// For Runtime Used
		private string _description = string.Empty;
		private bool _hasRemark = false;

		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;

		private int _CouponBHT35 = 0;
		private int _CouponBHT80 = 0;
		private int _CouponTotal = 0;
		private decimal _CouponBHTTotal = decimal.Zero;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public TSBCouponBalance() : base() { }

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
		[JsonIgnore]
		[PeropertyMapName("CouponBHTTotal")]
		public decimal CouponBHTTotal
		{
			get { return _CouponBHTTotal; }
			set { }
		}

		#endregion

		#endregion

		#region Internal Class

		public class FKs : TSBCouponBalance
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

			#region Coupon

			/// <summary>
			/// Gets or sets number of 35 BHT coupon.
			/// </summary>
			[PeropertyMapName("CouponBHT35")]
			public override int CouponBHT35
			{
				get { return base.CouponBHT35; }
				set { base.CouponBHT35 = value; }
			}
			/// <summary>
			/// Gets or sets number of 80 BHT coupon.
			/// </summary>
			[PeropertyMapName("CouponBHT80")]
			public override int CouponBHT80
			{
				get { return base.CouponBHT80; }
				set { base.CouponBHT80 = value; }
			}

			#endregion

			#region Public Methods

			public TSBCouponBalance ToTSBCouponBalance()
			{
				TSBCouponBalance inst = new TSBCouponBalance();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}

			#endregion
		}

		#endregion

		#region Static Methods
		/// <summary>
		/// Gets Active TSB Coupon balance.
		/// </summary>
		/// <returns>Returns Current Active TSB Coupon balance. If not found returns null.</returns>
		public static TSBCouponBalance GetCurrent()
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return GetCurrent(tsb);
			}
		}
		/// <summary>
		/// Gets TSB Coupon Balance.
		/// </summary>
		/// <param name="tsb">The target TSB to get balance.</param>
		/// <returns>Returns TSB Coupon balance. If TSB not found returns null.</returns>
		public static TSBCouponBalance GetCurrent(TSB tsb)
		{
			if (null == tsb) return null;
			lock (sync)
			{
				string cmd = @"
					SELECT TSB.TSBId
						 , TSB.TSBNameEN
						 , TSB.TSBNameTH
						 , ((
							 SELECT IFNULL(SUM(CouponBHT35), 0) 
							   FROM TSBCouponTransaction 
							  WHERE (   TSBCouponTransaction.TransactionType = 0 
									 OR TSBCouponTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCouponTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(CouponBHT35), 0) 
							   FROM TSBCouponTransaction 
							  WHERE TSBCouponTransaction.TransactionType = 2 -- Sold = 2
								AND TSBCouponTransaction.TSBId = TSB.TSBId
							)) AS ST25
						 , ((
							 SELECT IFNULL(SUM(CouponBHT80), 0) 
							   FROM TSBCouponTransaction 
							  WHERE (   TSBCouponTransaction.TransactionType = 0 
									 OR TSBCouponTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCouponTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(CouponBHT80), 0) 
							   FROM TSBCouponTransaction 
							  WHERE TSBCouponTransaction.TransactionType = 2 -- Sold = 2
								AND TSBCouponTransaction.TSBId = TSB.TSBId
							)) AS ST50
					  FROM TSB
					 WHERE TSB.TSBId = ?
				";
				var ret = NQuery.Query<FKs>(cmd, tsb.TSBId).FirstOrDefault();
				return (null != ret) ? ret.ToTSBCouponBalance() : null;
			}
		}
		/// <summary>
		/// Gets All TSB Coupon Balance.
		/// </summary>
		/// <returns>Returns List fo all TSB Coupon balance.</returns>
		public static List<TSBCouponBalance> Gets()
		{
			lock (sync)
			{
				string cmd = @"
					SELECT TSB.TSBId
						 , TSB.TSBNameEN
						 , TSB.TSBNameTH
						 , ((
							 SELECT IFNULL(SUM(CouponBHT35), 0) 
							   FROM TSBCouponTransaction 
							  WHERE (   TSBCouponTransaction.TransactionType = 0 
									 OR TSBCouponTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCouponTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(CouponBHT35), 0) 
							   FROM TSBCouponTransaction 
							  WHERE TSBCouponTransaction.TransactionType = 2 -- Sold = 2
								AND TSBCouponTransaction.TSBId = TSB.TSBId
							)) AS ST25
						 , ((
							 SELECT IFNULL(SUM(CouponBHT80), 0) 
							   FROM TSBCouponTransaction 
							  WHERE (   TSBCouponTransaction.TransactionType = 0 
									 OR TSBCouponTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCouponTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(CouponBHT80), 0) 
							   FROM TSBCouponTransaction 
							  WHERE TSBCouponTransaction.TransactionType = 2 -- Sold = 2
								AND TSBCouponTransaction.TSBId = TSB.TSBId
							)) AS ST50
					  FROM TSB
				";
				var rets = NQuery.Query<FKs>(cmd).ToList();
				var results = new List<TSBCouponBalance>();
				if (null != rets)
				{
					rets.ForEach(ret =>
					{
						results.Add(ret.ToTSBCouponBalance());
					});
				}
				return results;
			}
		}

		#endregion
	}

	#endregion
}
