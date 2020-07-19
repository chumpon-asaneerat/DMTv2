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
	#region TSBCreditBalance (For Query only)

	/// <summary>
	/// The TSBCreditBalance Data Model class.
	/// </summary>
	//[Table("TSBCreditBalance")]
	public class TSBCreditBalance : NTable<TSBCreditBalance>
	{
		#region Internal Variables

		// For Runtime Used
		private string _description = string.Empty;
		private bool _hasRemark = false;

		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;
		// Coin/Bill
		private int _ST25 = 0;
		private int _ST50 = 0;
		private int _BHT1 = 0;
		private int _BHT2 = 0;
		private int _BHT5 = 0;
		private int _BHT10 = 0;
		private int _BHT20 = 0;
		private int _BHT50 = 0;
		private int _BHT100 = 0;
		private int _BHT500 = 0;
		private int _BHT1000 = 0;
		private decimal _BHTTotal = decimal.Zero;

		// Summary
		private decimal _UserBHTTotal = decimal.Zero;
		private decimal _AdditionalBHTTotal = decimal.Zero;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public TSBCreditBalance() : base() { }

		#endregion

		#region Private Methods

		private void CalcTotal()
		{
			decimal total = 0;
			total += Convert.ToDecimal(_ST25 * (decimal).25);
			total += Convert.ToDecimal(_ST50 * (decimal).50);
			total += _BHT1 * 1;
			total += _BHT2 * 2;
			total += _BHT5 * 5;
			total += _BHT10 * 10;
			total += _BHT20 * 20;
			total += _BHT50 * 50;
			total += _BHT100 * 100;
			total += _BHT500 * 500;
			total += _BHT1000 * 1000;

			_BHTTotal = total;
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

		#region Coin/Bill

		/// <summary>
		/// Gets or sets number of .25 baht coin.
		/// </summary>
		[Category("Coin/Bill")]
		[Description("Gets or sets number of .25 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("ST25")]
		public virtual int ST25
		{
			get { return _ST25; }
			set
			{
				if (_ST25 != value)
				{
					_ST25 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("ST25");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of .50 baht coin.
		/// </summary>
		[Category("Coin/Bill")]
		[Description("Gets or sets number of .50 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("ST50")]
		public virtual int ST50
		{
			get { return _ST50; }
			set
			{
				if (_ST50 != value)
				{
					_ST50 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("ST50");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 1 baht coin.
		/// </summary>
		[Category("Coin/Bill")]
		[Description("Gets or sets number of 1 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("BHT1")]
		public virtual int BHT1
		{
			get { return _BHT1; }
			set
			{
				if (_BHT1 != value)
				{
					_BHT1 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("BHT1");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 2 baht coin.
		/// </summary>
		[Category("Coin/Bill")]
		[Description("Gets or sets number of 2 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("BHT2")]
		public virtual int BHT2
		{
			get { return _BHT2; }
			set
			{
				if (_BHT2 != value)
				{
					_BHT2 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("BHT2");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 5 baht coin.
		/// </summary>
		[Category("Coin/Bill")]
		[Description("Gets or sets number of 5 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("BHT5")]
		public virtual int BHT5
		{
			get { return _BHT5; }
			set
			{
				if (_BHT5 != value)
				{
					_BHT5 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("BHT5");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 10 baht coin.
		/// </summary>
		[Category("Coin/Bill")]
		[Description("Gets or sets number of 10 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("BHT10")]
		public virtual int BHT10
		{
			get { return _BHT10; }
			set
			{
				if (_BHT10 != value)
				{
					_BHT10 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("BHT10");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 20 baht bill.
		/// </summary>
		[Category("Coin/Bill")]
		[Description("Gets or sets number of 20 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("BHT20")]
		public virtual int BHT20
		{
			get { return _BHT20; }
			set
			{
				if (_BHT20 != value)
				{
					_BHT20 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("BHT20");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 50 baht bill.
		/// </summary>
		[Category("Coin/Bill")]
		[Description("Gets or sets number of 50 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("BHT50")]
		public virtual int BHT50
		{
			get { return _BHT50; }
			set
			{
				if (_BHT50 != value)
				{
					_BHT50 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("BHT50");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 100 baht bill.
		/// </summary>
		[Category("Coin/Bill")]
		[Description("Gets or sets number of 100 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("BHT100")]
		public virtual int BHT100
		{
			get { return _BHT100; }
			set
			{
				if (_BHT100 != value)
				{
					_BHT100 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("BHT100");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 500 baht bill.
		/// </summary>
		[Category("Coin/Bill")]
		[Description("Gets or sets number of 500 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("BHT500")]
		public virtual int BHT500
		{
			get { return _BHT500; }
			set
			{
				if (_BHT500 != value)
				{
					_BHT500 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("BHT500");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 1000 baht bill.
		/// </summary>
		[Category("Coin/Bill")]
		[Description("Gets or sets number of 1000 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("BHT1000")]
		public virtual int BHT1000
		{
			get { return _BHT1000; }
			set
			{
				if (_BHT1000 != value)
				{
					_BHT1000 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("BHT1000");
				}
			}
		}

		#endregion

		#region Summary

		/// <summary>
		/// Gets or sets total (credit flow) value in baht.
		/// </summary>
		[Category("Summary")]
		[Description("Gets or sets total (credit flow) value in baht.")]
		[ReadOnly(true)]
		[Ignore]
		[JsonIgnore]
		[PeropertyMapName("BHTTotal")]
		public decimal BHTTotal
		{
			get { return _BHTTotal; }
			set { }
		}
		/// <summary>
		/// Gets or sets additional borrow in baht.
		/// </summary>
		[Category("Summary")]
		[Description("Gets or sets additional borrow/return in baht.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AdditionalBHTTotal")]
		public virtual decimal AdditionalBHTTotal
		{
			get { return _AdditionalBHTTotal; }
			set
			{
				if (_AdditionalBHTTotal != value)
				{
					_AdditionalBHTTotal = value;
					// Raise event.
					this.RaiseChanged("AdditionalBHTTotal");
				}
			}
		}
		/// <summary>
		/// Gets or sets users borrow in baht.
		/// </summary>
		[Category("Summary")]
		[Description("Gets or sets users borrow/return in baht.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("UserBHTTotal")]
		public virtual decimal UserBHTTotal
		{
			get { return _UserBHTTotal; }
			set
			{
				if (_UserBHTTotal != value)
				{
					_UserBHTTotal = value;
					// Raise event.
					this.RaiseChanged("UserBHTTotal");
				}
			}
		}

		#endregion

		#endregion

		#region Internal Class

		public class FKs : TSBCreditBalance
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

			#region Coin/Bill

			/// <summary>
			/// Gets or sets number of .25 baht coin.
			/// </summary>
			[PeropertyMapName("ST25")]
			public override int ST25
			{
				get { return base.ST25; }
				set { base.ST25 = value; }
			}
			/// <summary>
			/// Gets or sets number of .50 baht coin.
			/// </summary>
			[PeropertyMapName("ST50")]
			public override int ST50
			{
				get { return base.ST50; }
				set { base.ST50 = value; }
			}
			/// <summary>
			/// Gets or sets number of 1 baht coin.
			/// </summary>
			[PeropertyMapName("BHT1")]
			public override int BHT1
			{
				get { return base.BHT1; }
				set { base.BHT1 = value; }
			}
			/// <summary>
			/// Gets or sets number of 2 baht coin.
			/// </summary>
			[PeropertyMapName("BHT2")]
			public override int BHT2
			{
				get { return base.BHT2; }
				set { base.BHT2 = value; }
			}
			/// <summary>
			/// Gets or sets number of 5 baht coin.
			/// </summary>
			[PeropertyMapName("BHT5")]
			public override int BHT5
			{
				get { return base.BHT5; }
				set { base.BHT5 = value; }
			}
			/// <summary>
			/// Gets or sets number of 10 baht coin.
			/// </summary>
			[PeropertyMapName("BHT10")]
			public override int BHT10
			{
				get { return base.BHT10; }
				set { base.BHT10 = value; }
			}
			/// <summary>
			/// Gets or sets number of 20 baht bill.
			/// </summary>
			[PeropertyMapName("BHT20")]
			public override int BHT20
			{
				get { return base.BHT20; }
				set { base.BHT20 = value; }
			}
			/// <summary>
			/// Gets or sets number of 50 baht bill.
			/// </summary>
			[PeropertyMapName("BHT50")]
			public override int BHT50
			{
				get { return base.BHT50; }
				set { base.BHT50 = value; }
			}
			/// <summary>
			/// Gets or sets number of 100 baht bill.
			/// </summary>
			[PeropertyMapName("BHT100")]
			public override int BHT100
			{
				get { return base.BHT100; }
				set { base.BHT100 = value; }
			}
			/// <summary>
			/// Gets or sets number of 500 baht bill.
			/// </summary>
			[PeropertyMapName("BHT500")]
			public override int BHT500
			{
				get { return base.BHT500; }
				set { base.BHT500 = value; }
			}
			/// <summary>
			/// Gets or sets number of 1000 baht bill.
			/// </summary>
			[PeropertyMapName("BHT1000")]
			public override int BHT1000
			{
				get { return base.BHT1000; }
				set { base.BHT1000 = value; }
			}

			#endregion

			#region Additional

			/// <summary>
			/// Gets or sets additional borrow in baht.
			/// </summary>
			[PeropertyMapName("AdditionalBHTTotal")]
			public override decimal AdditionalBHTTotal
			{
				get { return base.AdditionalBHTTotal; }
				set { base.AdditionalBHTTotal = value; }
			}
			/// <summary>
			/// Gets or sets users borrow in baht.
			/// </summary>
			[PeropertyMapName("UserBHTTotal")]
			public override decimal UserBHTTotal
			{
				get { return base.UserBHTTotal; }
				set { base.UserBHTTotal = value; }
			}

			#endregion

			#region Public Methods

			public TSBCreditBalance ToTSBCreditBalance()
			{
				TSBCreditBalance inst = new TSBCreditBalance();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}

			#endregion
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Gets Active TSB Credit balance.
		/// </summary>
		/// <returns>Returns Current Active TSB Credit balance. If not found returns null.</returns>
		public static TSBCreditBalance GetCurrent()
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return GetCurrent(tsb);
			}
		}
		/// <summary>
		/// Gets TSB Credit Balance.
		/// </summary>
		/// <param name="tsb">The target TSB to get balance.</param>
		/// <returns>Returns TSB Credit balance. If TSB not found returns null.</returns>
		public static TSBCreditBalance GetCurrent(TSB tsb)
		{
			if (null == tsb) return null;
			lock (sync)
			{
				string cmd = @"
					SELECT TSB.TSBId
						 , TSB.TSBNameEN
						 , TSB.TSBNameTH
						 , ((
							 SELECT IFNULL(SUM(AdditionalBHTTotal), 0) 
							   FROM TSBAdditionTransaction 
							  WHERE (   TSBAdditionTransaction.TransactionType = 0 
									 OR TSBAdditionTransaction.TransactionType = 1
									) -- Initial = 0, Borrow = 1
								AND TSBAdditionTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(AdditionalBHTTotal), 0) 
							   FROM TSBAdditionTransaction 
							  WHERE TSBAdditionTransaction.TransactionType = 2 -- Returns = 2
								AND TSBAdditionTransaction.TSBId = TSB.TSBId
							)) AS AdditionalBHTTotal
						 , ((
							 SELECT IFNULL(SUM(ST25), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(ST25), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS ST25
						 , ((
							 SELECT IFNULL(SUM(ST50), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(ST50), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS ST50
						 , ((
							 SELECT IFNULL(SUM(BHT1), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT1), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT1
						 , ((
							 SELECT IFNULL(SUM(BHT2), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT2), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT2
						 , ((
							 SELECT IFNULL(SUM(BHT5), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT5), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT5
						 , ((
							 SELECT IFNULL(SUM(BHT10), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT10), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT10
						 , ((
							 SELECT IFNULL(SUM(BHT20), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT20), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT20
						 , ((
							 SELECT IFNULL(SUM(BHT50), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT50), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT50
						 , ((
							 SELECT IFNULL(SUM(BHT100), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT100), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT100
						 , ((
							 SELECT IFNULL(SUM(BHT500), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT500), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT500
						 , ((
							 SELECT IFNULL(SUM(BHT1000), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT1000), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT1000
					  FROM TSB
					 WHERE TSB.TSBId = ?
				";
				var ret = NQuery.Query<FKs>(cmd, tsb.TSBId).FirstOrDefault();
				return (null != ret) ? ret.ToTSBCreditBalance() : null;
			}
		}
		/// <summary>
		/// Gets All TSB Credit Balance.
		/// </summary>
		/// <returns>Returns List fo all TSB Credit balance.</returns>
		public static List<TSBCreditBalance> Gets()
		{
			lock (sync)
			{
				string cmd = @"
					SELECT TSB.TSBId
						 , TSB.TSBNameEN
						 , TSB.TSBNameTH
						 , ((
							 SELECT IFNULL(SUM(ST25), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(ST25), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS ST25
						 , ((
							 SELECT IFNULL(SUM(ST50), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(ST50), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS ST50
						 , ((
							 SELECT IFNULL(SUM(BHT1), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT1), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT1
						 , ((
							 SELECT IFNULL(SUM(BHT2), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT2), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT2
						 , ((
							 SELECT IFNULL(SUM(BHT5), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT5), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT5
						 , ((
							 SELECT IFNULL(SUM(BHT10), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT10), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT10
						 , ((
							 SELECT IFNULL(SUM(BHT20), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT20), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT20
						 , ((
							 SELECT IFNULL(SUM(BHT50), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT50), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT50
						 , ((
							 SELECT IFNULL(SUM(BHT100), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT100), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT100
						 , ((
							 SELECT IFNULL(SUM(BHT500), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT500), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT500
						 , ((
							 SELECT IFNULL(SUM(BHT1000), 0) 
							   FROM TSBCreditTransaction 
							  WHERE (   TSBCreditTransaction.TransactionType = 0 
									 OR TSBCreditTransaction.TransactionType = 1
									) -- Initial = 0, Received = 1
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(BHT1000), 0) 
							   FROM TSBCreditTransaction 
							  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
								AND TSBCreditTransaction.TSBId = TSB.TSBId
							)) AS BHT1000
						 , ((
							 SELECT IFNULL(SUM(AdditionalBHTTotal), 0) 
							   FROM TSBAdditionTransaction 
							  WHERE (   TSBAdditionTransaction.TransactionType = 0 
									 OR TSBAdditionTransaction.TransactionType = 1
									) -- Initial = 0, Borrow = 1
								AND TSBAdditionTransaction.TSBId = TSB.TSBId
							) -
							(
							 SELECT IFNULL(SUM(AdditionalBHTTotal), 0) 
							   FROM TSBAdditionTransaction 
							  WHERE TSBAdditionTransaction.TransactionType = 2 -- Returns = 2
								AND TSBAdditionTransaction.TSBId = TSB.TSBId
							)) AS AdditionalBHTTotal
					  FROM TSB
				";
				var rets = NQuery.Query<FKs>(cmd).ToList();
				var results = new List<TSBCreditBalance>();
				if (null != rets)
				{
					rets.ForEach(ret =>
					{
						results.Add(ret.ToTSBCreditBalance());
					});
				}
				return results;
			}
		}

		#endregion
	}

	#endregion
}
