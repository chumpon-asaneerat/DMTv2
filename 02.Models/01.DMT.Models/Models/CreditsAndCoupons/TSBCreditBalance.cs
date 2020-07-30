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
	#region TSBCreditBalance (For Query only)

	/// <summary>
	/// The TSBCreditBalance Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
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
		// Coin/Bill (Count)
		private int _CntST25 = 0;
		private int _CntST50 = 0;
		private int _CntBHT1 = 0;
		private int _CntBHT2 = 0;
		private int _CntBHT5 = 0;
		private int _CntBHT10 = 0;
		private int _CntBHT20 = 0;
		private int _CntBHT50 = 0;
		private int _CntBHT100 = 0;
		private int _CntBHT500 = 0;
		private int _CntBHT1000 = 0;
		// Coin/Bill (Amount)
		private decimal _AmtST25 = 0;
		private decimal _AmtST50 = 0;
		private decimal _AmtBHT1 = 0;
		private decimal _AmtBHT2 = 0;
		private decimal _AmtBHT5 = 0;
		private decimal _AmtBHT10 = 0;
		private decimal _AmtBHT20 = 0;
		private decimal _AmtBHT50 = 0;
		private decimal _AmtBHT100 = 0;
		private decimal _AmtBHT500 = 0;
		private decimal _AmtBHT1000 = 0;

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

		private void CalcTotalAmount()
		{
			decimal total = 0;
			total += _AmtST25;
			total += _AmtST50;
			total += _AmtBHT1;
			total += _AmtBHT2;
			total += _AmtBHT5;
			total += _AmtBHT10;
			total += _AmtBHT20;
			total += _AmtBHT50;
			total += _AmtBHT100;
			total += _AmtBHT500;
			total += _AmtBHT1000;

			_BHTTotal = total;
			// Raise event.
			this.RaiseChanged("BHTTotal");
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

		#region Coin/Bill (Count)

		/// <summary>
		/// Gets or sets number of .25 baht coin.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of .25 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("CountST25")]
		[PropertyOrder(10)]
		public virtual int CountST25
		{
			get { return _CntST25; }
			set
			{
				if (_CntST25 != value)
				{
					_CntST25 = value;
					_AmtST25 = Convert.ToDecimal(_CntST25 * (decimal).25);
					// Raise event.
					this.RaiseChanged("CountST25");
					this.RaiseChanged("AmountST25");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets number of .50 baht coin.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of .50 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("CountST50")]
		[PropertyOrder(11)]
		public virtual int CountST50
		{
			get { return _CntST50; }
			set
			{
				if (_CntST50 != value)
				{
					_CntST50 = value;
					_AmtST50 = Convert.ToDecimal(_CntST50 * (decimal).50);
					// Raise event.
					this.RaiseChanged("CountST50");
					this.RaiseChanged("AmountST50");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 1 baht coin.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 1 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("CountBHT1")]
		[PropertyOrder(12)]
		public virtual int CountBHT1
		{
			get { return _CntBHT1; }
			set
			{
				if (_CntBHT1 != value)
				{
					_CntBHT1 = value;
					_AmtBHT1 = _CntBHT1;
					// Raise event.
					this.RaiseChanged("CountBHT1");
					this.RaiseChanged("AmountBHT1");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 2 baht coin.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 2 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("CountBHT2")]
		[PropertyOrder(13)]
		public virtual int CountBHT2
		{
			get { return _CntBHT2; }
			set
			{
				if (_CntBHT2 != value)
				{
					_CntBHT2 = value;
					_AmtBHT2 = _CntBHT2 * 2;
					// Raise event.
					this.RaiseChanged("CountBHT2");
					this.RaiseChanged("AmountBHT2");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 5 baht coin.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 5 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("CountBHT5")]
		[PropertyOrder(14)]
		public virtual int CountBHT5
		{
			get { return _CntBHT5; }
			set
			{
				if (_CntBHT5 != value)
				{
					_CntBHT5 = value;
					_AmtBHT5 = _CntBHT5 * 5;
					// Raise event.
					this.RaiseChanged("CountBHT5");
					this.RaiseChanged("AmountBHT5");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 10 baht coin.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 10 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("CountBHT10")]
		[PropertyOrder(15)]
		public virtual int CountBHT10
		{
			get { return _CntBHT10; }
			set
			{
				if (_CntBHT10 != value)
				{
					_CntBHT10 = value;
					_AmtBHT10 = _CntBHT10 * 10;
					// Raise event.
					this.RaiseChanged("CountBHT10");
					this.RaiseChanged("AmountBHT10");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 20 baht bill.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 20 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("CountBHT20")]
		[PropertyOrder(16)]
		public virtual int CountBHT20
		{
			get { return _CntBHT20; }
			set
			{
				if (_CntBHT20 != value)
				{
					_CntBHT20 = value;
					_AmtBHT20 = _CntBHT20 * 20;
					// Raise event.
					this.RaiseChanged("CountBHT20");
					this.RaiseChanged("AmountBHT20");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 50 baht bill.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 50 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("CountBHT50")]
		[PropertyOrder(17)]
		public virtual int CountBHT50
		{
			get { return _CntBHT50; }
			set
			{
				if (_CntBHT50 != value)
				{
					_CntBHT50 = value;
					_AmtBHT50 = _CntBHT50 * 50;
					// Raise event.
					this.RaiseChanged("CountBHT50");
					this.RaiseChanged("AmountBHT50");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 100 baht bill.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 100 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("CountBHT100")]
		[PropertyOrder(18)]
		public virtual int CountBHT100
		{
			get { return _CntBHT100; }
			set
			{
				if (_CntBHT100 != value)
				{
					_CntBHT100 = value;
					_AmtBHT100 = _CntBHT100 * 100;
					// Raise event.
					this.RaiseChanged("CountBHT100");
					this.RaiseChanged("AmountBHT100");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 500 baht bill.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 500 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("CountBHT500")]
		[PropertyOrder(19)]
		public virtual int CountBHT500
		{
			get { return _CntBHT500; }
			set
			{
				if (_CntBHT500 != value)
				{
					_CntBHT500 = value;
					_AmtBHT500 = _CntBHT500 * 500;
					// Raise event.
					this.RaiseChanged("CountBHT500");
					this.RaiseChanged("AmountBHT500");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 1000 baht bill.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 1000 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("CountBHT1000")]
		[PropertyOrder(20)]
		public virtual int CountBHT1000
		{
			get { return _CntBHT1000; }
			set
			{
				if (_CntBHT1000 != value)
				{
					_CntBHT1000 = value;
					_AmtBHT1000 = _CntBHT1000 * 1000;
					// Raise event.
					this.RaiseChanged("CountBHT1000");
					this.RaiseChanged("AmountBHT1000");

					CalcTotalAmount();
				}
			}
		}

		#endregion

		#region Coin/Bill (Amount)

		/// <summary>
		/// Gets or sets amount of .25 baht coin.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of .25 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AmountST25")]
		public virtual decimal AmountST25
		{
			get { return _AmtST25; }
			set
			{
				if (_AmtST25 != value)
				{
					_AmtST25 = value;
					_CntST25 = Convert.ToInt32(Math.Floor(_AmtST25 / (decimal).25));
					// Raise event.
					this.RaiseChanged("AmountST25");
					this.RaiseChanged("CountST25");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of .50 baht coin.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of .50 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AmountST50")]
		public virtual decimal AmountST50
		{
			get { return _AmtST50; }
			set
			{
				if (_AmtST50 != value)
				{
					_AmtST50 = value;
					_CntST50 = Convert.ToInt32(Math.Floor(_AmtST50 / (decimal).50));
					// Raise event.
					this.RaiseChanged("AmountST50");
					this.RaiseChanged("CountST25");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 1 baht coin.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 1 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AmountBHT1")]
		public virtual decimal AmountBHT1
		{
			get { return _AmtBHT1; }
			set
			{
				if (_AmtBHT1 != value)
				{
					_AmtBHT1 = value;
					_CntBHT1 = Convert.ToInt32(_AmtBHT1);
					// Raise event.
					this.RaiseChanged("AmountBHT1");
					this.RaiseChanged("CountBHT1");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 2 baht coin.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 2 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AmountBHT2")]
		public virtual decimal AmountBHT2
		{
			get { return _AmtBHT2; }
			set
			{
				if (_AmtBHT2 != value)
				{
					_AmtBHT2 = value;
					_CntBHT2 = Convert.ToInt32(Math.Floor(_AmtBHT2 / 2));
					// Raise event.
					this.RaiseChanged("AmountBHT2");
					this.RaiseChanged("CountBHT2");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 5 baht coin.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 5 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AmountBHT5")]
		public virtual decimal AmountBHT5
		{
			get { return _AmtBHT5; }
			set
			{
				if (_AmtBHT5 != value)
				{
					_AmtBHT5 = value;
					_CntBHT5 = Convert.ToInt32(Math.Floor(_AmtBHT5 / 5));
					// Raise event.
					this.RaiseChanged("AmountBHT5");
					this.RaiseChanged("CountBHT5");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 10 baht coin.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 10 baht coin.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AmountBHT10")]
		public virtual decimal AmountBHT10
		{
			get { return _AmtBHT10; }
			set
			{
				if (_AmtBHT10 != value)
				{
					_AmtBHT10 = value;
					_CntBHT10 = Convert.ToInt32(Math.Floor(_AmtBHT10 / 10));
					// Raise event.
					this.RaiseChanged("AmountBHT10");
					this.RaiseChanged("CountBHT10");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 20 baht bill.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 20 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AmountBHT20")]
		public virtual decimal AmountBHT20
		{
			get { return _AmtBHT20; }
			set
			{
				if (_AmtBHT20 != value)
				{
					_AmtBHT20 = value;
					_CntBHT20 = Convert.ToInt32(Math.Floor(_AmtBHT20 / 20));
					// Raise event.
					this.RaiseChanged("AmountBHT20");
					this.RaiseChanged("CountBHT20");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 50 baht bill.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 50 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AmountBHT50")]
		public virtual decimal AmountBHT50
		{
			get { return _AmtBHT50; }
			set
			{
				if (_AmtBHT50 != value)
				{
					_AmtBHT50 = value;
					_CntBHT50 = Convert.ToInt32(Math.Floor(_AmtBHT50 / 50));
					// Raise event.
					this.RaiseChanged("AmountBHT50");
					this.RaiseChanged("CountBHT50");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 100 baht bill.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 100 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AmountBHT100")]
		public virtual decimal AmountBHT100
		{
			get { return _AmtBHT100; }
			set
			{
				if (_AmtBHT100 != value)
				{
					_AmtBHT100 = value;
					_CntBHT100 = Convert.ToInt32(Math.Floor(_AmtBHT100 / 100));
					// Raise event.
					this.RaiseChanged("AmountBHT100");
					this.RaiseChanged("CountBHT100");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 500 baht bill.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 500 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AmountBHT500")]
		public virtual decimal AmountBHT500
		{
			get { return _AmtBHT500; }
			set
			{
				if (_AmtBHT500 != value)
				{
					_AmtBHT500 = value;
					_CntBHT500 = Convert.ToInt32(Math.Floor(_AmtBHT500 / 500));
					// Raise event.
					this.RaiseChanged("AmountBHT500");
					this.RaiseChanged("CountBHT500");

					CalcTotalAmount();
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 1000 baht bill.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 1000 baht bill.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("AmountBHT1000")]
		public virtual decimal AmountBHT1000
		{
			get { return _AmtBHT1000; }
			set
			{
				if (_AmtBHT1000 != value)
				{
					_AmtBHT1000 = value;
					_CntBHT1000 = Convert.ToInt32(Math.Floor(_AmtBHT1000 / 1000));
					// Raise event.
					this.RaiseChanged("AmountBHT1000");
					this.RaiseChanged("CountBHT1000");

					CalcTotalAmount();
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
		[PeropertyMapName("CreditFlowBHTTotal")]
		public decimal CreditFlowBHTTotal
		{
			get { return _AdditionalBHTTotal + _BHTTotal + _UserBHTTotal; }
			set { }
		}
		
		/// <summary>
		/// Gets or sets total (coin/bill) value in baht.
		/// </summary>
		[Category("Summary")]
		[Description("Gets or sets total (coin/bill) value in baht.")]
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
					this.RaiseChanged("CreditFlowBHTTotal");
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
					this.RaiseChanged("CreditFlowBHTTotal");
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

			#region Coin/Bill (count)

			/// <summary>
			/// Gets or sets number of .25 baht coin.
			/// </summary>
			[PeropertyMapName("CountST25")]
			public override int CountST25
			{
				get { return base.CountST25; }
				set { base.CountST25 = value; }
			}
			/// <summary>
			/// Gets or sets number of .50 baht coin.
			/// </summary>
			[PeropertyMapName("CountST50")]
			public override int CountST50
			{
				get { return base.CountST50; }
				set { base.CountST50 = value; }
			}
			/// <summary>
			/// Gets or sets number of 1 baht coin.
			/// </summary>
			[PeropertyMapName("CountBHT1")]
			public override int CountBHT1
			{
				get { return base.CountBHT1; }
				set { base.CountBHT1 = value; }
			}
			/// <summary>
			/// Gets or sets number of 2 baht coin.
			/// </summary>
			[PeropertyMapName("CountBHT2")]
			public override int CountBHT2
			{
				get { return base.CountBHT2; }
				set { base.CountBHT2 = value; }
			}
			/// <summary>
			/// Gets or sets number of 5 baht coin.
			/// </summary>
			[PeropertyMapName("CountBHT5")]
			public override int CountBHT5
			{
				get { return base.CountBHT5; }
				set { base.CountBHT5 = value; }
			}
			/// <summary>
			/// Gets or sets number of 10 baht coin.
			/// </summary>
			[PeropertyMapName("CountBHT10")]
			public override int CountBHT10
			{
				get { return base.CountBHT10; }
				set { base.CountBHT10 = value; }
			}
			/// <summary>
			/// Gets or sets number of 20 baht bill.
			/// </summary>
			[PeropertyMapName("CountBHT20")]
			public override int CountBHT20
			{
				get { return base.CountBHT20; }
				set { base.CountBHT20 = value; }
			}
			/// <summary>
			/// Gets or sets number of 50 baht bill.
			/// </summary>
			[PeropertyMapName("CountBHT50")]
			public override int CountBHT50
			{
				get { return base.CountBHT50; }
				set { base.CountBHT50 = value; }
			}
			/// <summary>
			/// Gets or sets number of 100 baht bill.
			/// </summary>
			[PeropertyMapName("CountBHT100")]
			public override int CountBHT100
			{
				get { return base.CountBHT100; }
				set { base.CountBHT100 = value; }
			}
			/// <summary>
			/// Gets or sets number of 500 baht bill.
			/// </summary>
			[PeropertyMapName("CountBHT500")]
			public override int CountBHT500
			{
				get { return base.CountBHT500; }
				set { base.CountBHT500 = value; }
			}
			/// <summary>
			/// Gets or sets number of 1000 baht bill.
			/// </summary>
			[PeropertyMapName("CountBHT1000")]
			public override int CountBHT1000
			{
				get { return base.CountBHT1000; }
				set { base.CountBHT1000 = value; }
			}

			#endregion

			#region Coin/Bill (Amount)

			/// <summary>
			/// Gets or sets amount of .25 baht coin.
			/// </summary>
			[PeropertyMapName("AmountST25")]
			public override decimal AmountST25
			{
				get { return base.AmountST25; }
				set { base.AmountST25 = value; }
			}
			/// <summary>
			/// Gets or sets amount of .50 baht coin.
			/// </summary>
			[PeropertyMapName("AmountST50")]
			public override decimal AmountST50
			{
				get { return base.AmountST50; }
				set { base.AmountST50 = value; }
			}
			/// <summary>
			/// Gets or sets amount of 1 baht coin.
			/// </summary>
			[PeropertyMapName("AmountBHT1")]
			public override decimal AmountBHT1
			{
				get { return base.AmountBHT1; }
				set { base.AmountBHT1 = value; }
			}
			/// <summary>
			/// Gets or sets amount of 2 baht coin.
			/// </summary>
			[PeropertyMapName("AmountBHT2")]
			public override decimal AmountBHT2
			{
				get { return base.AmountBHT2; }
				set { base.AmountBHT2 = value; }
			}
			/// <summary>
			/// Gets or sets amount of 5 baht coin.
			/// </summary>
			[PeropertyMapName("AmountBHT5")]
			public override decimal AmountBHT5
			{
				get { return base.AmountBHT5; }
				set { base.AmountBHT5 = value; }
			}
			/// <summary>
			/// Gets or sets amount of 10 baht coin.
			/// </summary>
			[PeropertyMapName("AmountBHT10")]
			public override decimal AmountBHT10
			{
				get { return base.AmountBHT10; }
				set { base.AmountBHT10 = value; }
			}
			/// <summary>
			/// Gets or sets amount of 20 baht bill.
			/// </summary>
			[PeropertyMapName("AmountBHT20")]
			public override decimal AmountBHT20
			{
				get { return base.AmountBHT20; }
				set { base.AmountBHT20 = value; }
			}
			/// <summary>
			/// Gets or sets amount of 50 baht bill.
			/// </summary>
			[PeropertyMapName("AmountBHT50")]
			public override decimal AmountBHT50
			{
				get { return base.AmountBHT50; }
				set { base.AmountBHT50 = value; }
			}
			/// <summary>
			/// Gets or sets amount of 100 baht bill.
			/// </summary>
			[PeropertyMapName("AmountBHT100")]
			public override decimal AmountBHT100
			{
				get { return base.AmountBHT100; }
				set { base.AmountBHT100 = value; }
			}
			/// <summary>
			/// Gets or sets amount of 500 baht bill.
			/// </summary>
			[PeropertyMapName("AmountBHT500")]
			public override decimal AmountBHT500
			{
				get { return base.AmountBHT500; }
				set { base.AmountBHT500 = value; }
			}
			/// <summary>
			/// Gets or sets amount of 1000 baht bill.
			/// </summary>
			[PeropertyMapName("AmountBHT1000")]
			public override decimal AmountBHT1000
			{
				get { return base.AmountBHT1000; }
				set { base.AmountBHT1000 = value; }
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
							 SELECT (IFNULL(SUM(UserCreditTransaction.ST25), 0) * .25 +
									 IFNULL(SUM(UserCreditTransaction.ST50), 0) * .5 +
									 IFNULL(SUM(UserCreditTransaction.BHT1), 0) +
									 IFNULL(SUM(UserCreditTransaction.BHT2), 0) * 2 +
									 IFNULL(SUM(UserCreditTransaction.BHT5), 0) * 5 +
									 IFNULL(SUM(UserCreditTransaction.BHT10), 0) * 10 +
									 IFNULL(SUM(UserCreditTransaction.BHT20), 0) * 20 +
									 IFNULL(SUM(UserCreditTransaction.BHT50), 0) * 50 +
									 IFNULL(SUM(UserCreditTransaction.BHT100), 0) * 100 +
									 IFNULL(SUM(UserCreditTransaction.BHT500), 0) * 500 +
									 IFNULL(SUM(UserCreditTransaction.BHT1000), 0) * 1000)
							   FROM UserCreditTransaction, UserCredit
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
								AND UserCredit.TSBId = TSB.TSBId
							) -
							(
							 SELECT (IFNULL(SUM(UserCreditTransaction.ST25), 0) * .25 +
									 IFNULL(SUM(UserCreditTransaction.ST50), 0) * .5 +
									 IFNULL(SUM(UserCreditTransaction.BHT1), 0) +
									 IFNULL(SUM(UserCreditTransaction.BHT2), 0) * 2 +
									 IFNULL(SUM(UserCreditTransaction.BHT5), 0) * 5 +
									 IFNULL(SUM(UserCreditTransaction.BHT10), 0) * 10 +
									 IFNULL(SUM(UserCreditTransaction.BHT20), 0) * 20 +
									 IFNULL(SUM(UserCreditTransaction.BHT50), 0) * 50 +
									 IFNULL(SUM(UserCreditTransaction.BHT100), 0) * 100 +
									 IFNULL(SUM(UserCreditTransaction.BHT500), 0) * 500 +
									 IFNULL(SUM(UserCreditTransaction.BHT1000), 0) * 1000)
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
								AND UserCredit.TSBId = TSB.TSBId
							)) AS UserBHTTotal
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.ST25), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.ST25), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.ST50), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.ST50), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT1), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT1), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT2), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT2), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT5), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT5), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT10), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT10), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT20), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT20), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT50), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT50), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT100), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT100), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT500), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT500), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT1000), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT1000), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT1000
					  FROM TSB
					 WHERE TSB.TSBId = ?
				";
				var ret = NQuery.Query<FKs>(cmd, tsb.TSBId).FirstOrDefault();
				var result = (null != ret) ? ret.ToTSBCreditBalance() : null;
				if (null != result)
				{
					var addition = TSBAdditionBalance.GetCurrent();
					result.AdditionalBHTTotal = (null != addition) ? addition.AdditionalBHTTotal : decimal.Zero;
				}
				return result;
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
							 SELECT (IFNULL(SUM(UserCreditTransaction.ST25), 0) * .25 +
									 IFNULL(SUM(UserCreditTransaction.ST50), 0) * .5 +
									 IFNULL(SUM(UserCreditTransaction.BHT1), 0) +
									 IFNULL(SUM(UserCreditTransaction.BHT2), 0) * 2 +
									 IFNULL(SUM(UserCreditTransaction.BHT5), 0) * 5 +
									 IFNULL(SUM(UserCreditTransaction.BHT10), 0) * 10 +
									 IFNULL(SUM(UserCreditTransaction.BHT20), 0) * 20 +
									 IFNULL(SUM(UserCreditTransaction.BHT50), 0) * 50 +
									 IFNULL(SUM(UserCreditTransaction.BHT100), 0) * 100 +
									 IFNULL(SUM(UserCreditTransaction.BHT500), 0) * 500 +
									 IFNULL(SUM(UserCreditTransaction.BHT1000), 0) * 1000)
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
								AND UserCredit.TSBId = TSB.TSBId
							) -
							(
							 SELECT (IFNULL(SUM(UserCreditTransaction.ST25), 0) * .25 +
									 IFNULL(SUM(UserCreditTransaction.ST50), 0) * .5 +
									 IFNULL(SUM(UserCreditTransaction.BHT1), 0) +
									 IFNULL(SUM(UserCreditTransaction.BHT2), 0) * 2 +
									 IFNULL(SUM(UserCreditTransaction.BHT5), 0) * 5 +
									 IFNULL(SUM(UserCreditTransaction.BHT10), 0) * 10 +
									 IFNULL(SUM(UserCreditTransaction.BHT20), 0) * 20 +
									 IFNULL(SUM(UserCreditTransaction.BHT50), 0) * 50 +
									 IFNULL(SUM(UserCreditTransaction.BHT100), 0) * 100 +
									 IFNULL(SUM(UserCreditTransaction.BHT500), 0) * 500 +
									 IFNULL(SUM(UserCreditTransaction.BHT1000), 0) * 1000)
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
								AND UserCredit.TSBId = TSB.TSBId
							)) AS UserBHTTotal
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.ST25), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.ST25), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.ST50), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.ST50), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT1), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT1), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT2), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT2), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT5), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT5), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT10), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT10), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT20), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT20), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT50), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT50), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT100), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT100), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT500), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT500), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
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
							) - 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT1000), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 1 -- Borrow
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) + 
							(
							 SELECT IFNULL(SUM(UserCreditTransaction.BHT1000), 0) 
							   FROM UserCreditTransaction, UserCredit 
							  WHERE UserCredit.TSBId = TSB.TSBId
								AND UserCreditTransaction.TransactionType = 2 -- Return
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT1000
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
