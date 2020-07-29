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
	#region UserCredit

	/// <summary>
	/// The UserCredit Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("UserCredit")]
	public class UserCreditTransaction : NTable<UserCreditTransaction>
	{
		#region Enum

		public enum TransactionTypes
		{
			Borrow = 1,
			Return = 2,
			Undo = 3
		}

		#endregion

		#region Internal Variables

		// For Runtime Used
		private string _description = string.Empty;
		private bool _hasRemark = false;

		private int _TransactionId = 0;
		private DateTime _TransactionDate = DateTime.MinValue;
		private TransactionTypes _TransactionType = TransactionTypes.Borrow;

		private int _RefId = 0; // for undo.

		private int _UserCreditId = 0;

		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;

		private string _PlazaGroupId = string.Empty;
		private string _PlazaGroupNameEN = string.Empty;
		private string _PlazaGroupNameTH = string.Empty;
		private string _Direction = string.Empty;

		private string _UserId = string.Empty;
		private string _FullNameEN = string.Empty;
		private string _FullNameTH = string.Empty;

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
		private int _AmtST25 = 0;
		private int _AmtST50 = 0;
		private int _AmtBHT1 = 0;
		private int _AmtBHT2 = 0;
		private int _AmtBHT5 = 0;
		private int _AmtBHT10 = 0;
		private int _AmtBHT20 = 0;
		private int _AmtBHT50 = 0;
		private int _AmtBHT100 = 0;
		private int _AmtBHT500 = 0;
		private int _AmtBHT1000 = 0;

		private decimal _BHTTotal = decimal.Zero;
		private string _Remark = "";

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public UserCreditTransaction() : base() { }

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
		/// Gets or sets RefId
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets RefId")]
		[ReadOnly(true)]
		[PeropertyMapName("RefId")]
		public int RefId
		{
			get
			{
				return _RefId;
			}
			set
			{
				if (_RefId != value)
				{
					_RefId = value;
					this.RaiseChanged("RefId");
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

		#region PlazaGroup

		/// <summary>
		/// Gets or sets PlazaGroupId.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets PlazaGroupId.")]
		[ReadOnly(true)]
		[Ignore]
		[MaxLength(10)]
		[PeropertyMapName("PlazaGroupId")]
		public virtual string PlazaGroupId
		{
			get
			{
				return _PlazaGroupId;
			}
			set
			{
				if (_PlazaGroupId != value)
				{
					_PlazaGroupId = value;
					this.RaiseChanged("PlazaGroupId");
				}
			}
		}
		/// <summary>
		/// Gets or sets PlazaGroupNameEN.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets PlazaGroupNameEN.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("PlazaGroupNameEN")]
		public virtual string PlazaGroupNameEN
		{
			get
			{
				return _PlazaGroupNameEN;
			}
			set
			{
				if (_PlazaGroupNameEN != value)
				{
					_PlazaGroupNameEN = value;
					this.RaiseChanged("PlazaGroupNameEN");
				}
			}
		}
		/// <summary>
		/// Gets or sets PlazaGroupNameTH.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets PlazaGroupNameTH.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("PlazaGroupNameTH")]
		public virtual string PlazaGroupNameTH
		{
			get
			{
				return _PlazaGroupNameTH;
			}
			set
			{
				if (_PlazaGroupNameTH != value)
				{
					_PlazaGroupNameTH = value;
					this.RaiseChanged("PlazaGroupNameTH");
				}
			}
		}
		/// <summary>
		/// Gets or sets Direction.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets Direction.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("Direction")]
		public virtual string Direction
		{
			get
			{
				return _Direction;
			}
			set
			{
				if (_Direction != value)
				{
					_Direction = value;
					this.RaiseChanged("Direction");
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
		[Ignore]
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

		#region UserCredit

		/// <summary>
		/// Gets or sets UserCreditId
		/// </summary>
		[Category("UserCredit")]
		[Description("Gets or sets UserCreditId")]
		[ReadOnly(true)]
		[PeropertyMapName("UserCreditId")]
		public int UserCreditId
		{
			get
			{
				return _UserCreditId;
			}
			set
			{
				if (_UserCreditId != value)
				{
					_UserCreditId = value;
					this.RaiseChanged("UserCreditId");
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
		[PeropertyMapName("CountST25")]
		public int CountST25
		{
			get { return _CntST25; }
			set
			{
				if (_CntST25 != value)
				{
					_CntST25 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("CountST25");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of .50 baht coin.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of .50 baht coin.")]
		[PeropertyMapName("CountST50")]
		public int CountST50
		{
			get { return _CntST50; }
			set
			{
				if (_CntST50 != value)
				{
					_CntST50 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("CountST50");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 1 baht coin.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 1 baht coin.")]
		[PeropertyMapName("CountBHT1")]
		public int CountBHT1
		{
			get { return _CntBHT1; }
			set
			{
				if (_CntBHT1 != value)
				{
					_CntBHT1 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("CountBHT1");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 2 baht coin.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 2 baht coin.")]
		[PeropertyMapName("CountBHT2")]
		public int CountBHT2
		{
			get { return _CntBHT2; }
			set
			{
				if (_CntBHT2 != value)
				{
					_CntBHT2 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("CountBHT2");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 5 baht coin.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 5 baht coin.")]
		[PeropertyMapName("CountBHT5")]
		public int CountBHT5
		{
			get { return _CntBHT5; }
			set
			{
				if (_CntBHT5 != value)
				{
					_CntBHT5 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("CountBHT5");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 10 baht coin.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 10 baht coin.")]
		[PeropertyMapName("CountBHT10")]
		public int CountBHT10
		{
			get { return _CntBHT10; }
			set
			{
				if (_CntBHT10 != value)
				{
					_CntBHT10 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("CountBHT10");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 20 baht bill.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 20 baht bill.")]
		[PeropertyMapName("CountBHT20")]
		public int CountBHT20
		{
			get { return _CntBHT20; }
			set
			{
				if (_CntBHT20 != value)
				{
					_CntBHT20 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("CountBHT20");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 50 baht bill.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 50 baht bill.")]
		[PeropertyMapName("CountBHT50")]
		public int CountBHT50
		{
			get { return _CntBHT50; }
			set
			{
				if (_CntBHT50 != value)
				{
					_CntBHT50 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("CountBHT50");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 100 baht bill.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 100 baht bill.")]
		[PeropertyMapName("CountBHT100")]
		public int CountBHT100
		{
			get { return _CntBHT100; }
			set
			{
				if (_CntBHT100 != value)
				{
					_CntBHT100 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("CountBHT100");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 500 baht bill.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 500 baht bill.")]
		[PeropertyMapName("CountBHT500")]
		public int CountBHT500
		{
			get { return _CntBHT500; }
			set
			{
				if (_CntBHT500 != value)
				{
					_CntBHT500 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("CountBHT500");
				}
			}
		}
		/// <summary>
		/// Gets or sets number of 1000 baht bill.
		/// </summary>
		[Category("Coin/Bill (Count)")]
		[Description("Gets or sets number of 1000 baht bill.")]
		[PeropertyMapName("CountBHT1000")]
		public int CountBHT1000
		{
			get { return _CntBHT1000; }
			set
			{
				if (_CntBHT1000 != value)
				{
					_CntBHT1000 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("CountBHT1000");
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
		[PeropertyMapName("AmountST25")]
		public int AmountST25
		{
			get { return _AmtST25; }
			set
			{
				if (_AmtST25 != value)
				{
					_AmtST25 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("AmountST25");
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of .50 baht coin.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of .50 baht coin.")]
		[PeropertyMapName("AmountST50")]
		public int AmountST50
		{
			get { return _AmtST50; }
			set
			{
				if (_AmtST50 != value)
				{
					_AmtST50 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("AmountST50");
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 1 baht coin.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 1 baht coin.")]
		[PeropertyMapName("AmountBHT1")]
		public int AmountBHT1
		{
			get { return _AmtBHT1; }
			set
			{
				if (_AmtBHT1 != value)
				{
					_AmtBHT1 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("AmountBHT1");
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 2 baht coin.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 2 baht coin.")]
		[PeropertyMapName("AmountBHT2")]
		public int AmountBHT2
		{
			get { return _AmtBHT2; }
			set
			{
				if (_AmtBHT2 != value)
				{
					_AmtBHT2 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("AmountBHT2");
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 5 baht coin.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 5 baht coin.")]
		[PeropertyMapName("AmountBHT5")]
		public int AmountBHT5
		{
			get { return _AmtBHT5; }
			set
			{
				if (_AmtBHT5 != value)
				{
					_AmtBHT5 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("AmountBHT5");
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 10 baht coin.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 10 baht coin.")]
		[PeropertyMapName("AmountBHT10")]
		public int AmountBHT10
		{
			get { return _AmtBHT10; }
			set
			{
				if (_AmtBHT10 != value)
				{
					_AmtBHT10 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("AmountBHT10");
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 20 baht bill.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 20 baht bill.")]
		[PeropertyMapName("AmountBHT20")]
		public int AmountBHT20
		{
			get { return _AmtBHT20; }
			set
			{
				if (_AmtBHT20 != value)
				{
					_AmtBHT20 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("AmountBHT20");
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 50 baht bill.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 50 baht bill.")]
		[PeropertyMapName("AmountBHT50")]
		public int AmountBHT50
		{
			get { return _AmtBHT50; }
			set
			{
				if (_AmtBHT50 != value)
				{
					_AmtBHT50 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("AmountBHT50");
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 100 baht bill.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 100 baht bill.")]
		[PeropertyMapName("AmountBHT100")]
		public int AmountBHT100
		{
			get { return _AmtBHT100; }
			set
			{
				if (_AmtBHT100 != value)
				{
					_AmtBHT100 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("AmountBHT100");
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 500 baht bill.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 500 baht bill.")]
		[PeropertyMapName("AmountBHT500")]
		public int AmountBHT500
		{
			get { return _AmtBHT500; }
			set
			{
				if (_AmtBHT500 != value)
				{
					_AmtBHT500 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("AmountBHT500");
				}
			}
		}
		/// <summary>
		/// Gets or sets amount of 1000 baht bill.
		/// </summary>
		[Category("Coin/Bill (Amount)")]
		[Description("Gets or sets amount of 1000 baht bill.")]
		[PeropertyMapName("AmountBHT1000")]
		public int AmountBHT1000
		{
			get { return _AmtBHT1000; }
			set
			{
				if (_AmtBHT1000 != value)
				{
					_AmtBHT1000 = value;
					CalcTotal();
					// Raise event.
					this.RaiseChanged("AmountBHT1000");
				}
			}
		}

		#endregion

		#region Coin/Bill (Other)

		/// <summary>
		/// Gets or sets total value in baht.
		/// </summary>
		[Category("Coin/Bill")]
		[Description("Gets or sets total value in baht.")]
		[ReadOnly(true)]
		[PeropertyMapName("BHTTotal")]
		public decimal BHTTotal
		{
			get { return _BHTTotal; }
			set { }
		}
		/// <summary>
		/// Gets or sets  Remark.
		/// </summary>
		[Category("Remark")]
		[Description("Gets or sets  Remark.")]
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

		public class FKs : UserCreditTransaction
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

			#region PlazaGroup

			/// <summary>
			/// Gets or sets PlazaGroupId.
			/// </summary>
			[MaxLength(10)]
			[PeropertyMapName("PlazaGroupId")]
			public override string PlazaGroupId
			{
				get { return base.PlazaGroupId; }
				set { base.PlazaGroupId = value; }
			}
			/// <summary>
			/// Gets or sets PlazaGroupNameEN.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("PlazaGroupNameEN")]
			public override string PlazaGroupNameEN
			{
				get { return base.PlazaGroupNameEN; }
				set { base.PlazaGroupNameEN = value; }
			}
			/// <summary>
			/// Gets or sets PlazaGroupNameTH.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("PlazaGroupNameTH")]
			public override string PlazaGroupNameTH
			{
				get { return base.PlazaGroupNameTH; }
				set { base.PlazaGroupNameTH = value; }
			}
			/// <summary>
			/// Gets or sets Direction.
			/// </summary>
			[MaxLength(10)]
			[PeropertyMapName("Direction")]
			public override string Direction
			{
				get { return base.Direction; }
				set { base.Direction = value; }
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

			#region Public Methods

			public UserCreditTransaction ToUserCreditTransaction()
			{
				UserCreditTransaction inst = new UserCreditTransaction();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}

			#endregion
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Gets Active TSB User Credit transactions.
		/// </summary>
		/// <returns>Returns Current Active TSB User Credit transactions. If not found returns null.</returns>
		public static List<UserCreditTransaction> Gets()
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return Gets(tsb);
			}
		}
		/// <summary>
		/// Gets User Credit transactions.
		/// </summary>
		/// <param name="tsb">The target TSB to get transactions.</param>
		/// <returns>Returns User Credit transactions. If TSB not found returns null.</returns>
		public static List<UserCreditTransaction> Gets(TSB tsb)
		{
			if (null == tsb) return null;
			lock (sync)
			{
				string cmd = string.Empty;
				cmd += "SELECT UserCreditTransaction.* ";
				cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
				cmd += "     , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction ";
				cmd += "     , User.UserId, User.FullNameEN, User.FullNameTH ";
				cmd += "     , UserCredit.UserCreditDate, ";
				cmd += "     , UserCredit.State, UserCredit.BagNo, UserCredit.BeltNo ";
				cmd += "  FROM UserCreditTransaction, TSB, PlazaGroup, User, UserCredit ";
				cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
				cmd += "   AND UserCredit.TSBId = TSB.TSBId ";
				cmd += "   AND UserCredit.PlazaGroupId = PlazaGroup.PlazaGroupId ";
				cmd += "   AND UserCredit.UserId = User.UserId ";
				cmd += "   AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId ";
				cmd += "   AND UserCredit.TSBId = ? ";

				var rets = NQuery.Query<FKs>(cmd, tsb.TSBId).ToList();
				if (null == rets)
				{
					return new List<UserCreditTransaction>();
				}
				else
				{
					var results = new List<UserCreditTransaction>();
					rets.ForEach(ret =>
					{
						results.Add(ret.ToUserCreditTransaction());
					});
					return results;
				}
			}
		}

		/*
		public static UserCreditTransaction Create(User user, TSB tsb)
		{
			lock (sync)
			{
				if (null == user || null == tsb) return null;
				UserCreditTransaction inst = Create();

				inst.TSBId = tsb.TSBId;
				inst.TSBNameEN = tsb.TSBNameEN;
				inst.TSBNameTH = tsb.TSBNameTH;

				inst.UserId = user.UserId;
				inst.FullNameEN = user.FullNameEN;
				inst.FullNameTH = user.FullNameTH;

				return inst;
			}
		}

		public static UserCreditTransaction Create(User user)
		{
			lock (sync)
			{
				if (null == user) return null;

				TSB tsb = TSB.GetCurrent();
				if (null == tsb) return null; // active tsb not found.

				UserCreditTransaction inst = Create();

				inst.TSBId = tsb.TSBId;
				inst.TSBNameEN = tsb.TSBNameEN;
				inst.TSBNameTH = tsb.TSBNameTH;

				inst.UserId = user.UserId;
				inst.FullNameEN = user.FullNameEN;
				inst.FullNameTH = user.FullNameTH;

				return inst;
			}
		}

		public static void Borrow(UserCredit credit, TSBBalance balance)
		{
			lock (sync)
			{
				if (null == credit || null == balance) return;
				if (null == Default) return;
				int sign = -1;
				try
				{
					Default.BeginTransaction();

					credit.PKId = Guid.NewGuid(); // always create new.
					credit.TransactionDate = DateTime.Now;
					credit.TransactionType = UserCreditTransactionType.Borrow;

					balance.ST25 += sign * credit.ST25;
					balance.ST50 += sign * credit.ST50;
					balance.BHT1 += sign * credit.BHT1;
					balance.BHT2 += sign * credit.BHT2;
					balance.BHT5 += sign * credit.BHT5;
					balance.BHT10 += sign * credit.BHT10;
					balance.BHT20 += sign * credit.BHT20;
					balance.BHT50 += sign * credit.BHT50;
					balance.BHT100 += sign * credit.BHT100;
					balance.BHT500 += sign * credit.BHT500;
					balance.BHT1000 += sign * credit.BHT1000;

					balance.UserBHTTotal += -1 * sign * credit.BHTTotal;

					Save(credit);
					TSBBalance.Save(balance);

					Default.Commit();
				}
				catch
				{
					Default.Rollback();
				}
			}
		}

		public static void Return(UserCredit credit, TSBBalance balance)
		{
			lock (sync)
			{
				if (null == credit || null == balance) return;
				if (null == Default) return;
				int sign = 1;
				try
				{
					Default.BeginTransaction();

					credit.PKId = Guid.NewGuid(); // always create new.
					credit.TransactionDate = DateTime.Now;
					credit.TransactionType = UserCreditTransactionType.Return;

					balance.ST25 += sign * credit.ST25;
					balance.ST50 += sign * credit.ST50;
					balance.BHT1 += sign * credit.BHT1;
					balance.BHT2 += sign * credit.BHT2;
					balance.BHT5 += sign * credit.BHT5;
					balance.BHT10 += sign * credit.BHT10;
					balance.BHT20 += sign * credit.BHT20;
					balance.BHT50 += sign * credit.BHT50;
					balance.BHT100 += sign * credit.BHT100;
					balance.BHT500 += sign * credit.BHT500;
					balance.BHT1000 += sign * credit.BHT1000;

					balance.UserBHTTotal += -1 * sign * credit.BHTTotal;

					Save(credit);
					TSBBalance.Save(balance);

					Default.Commit();
				}
				catch
				{
					Default.Rollback();
				}
			}
		}

		public static void Undo(UserCredit credit, TSBBalance balance)
		{
			lock (sync)
			{
				if (null == credit || null == balance) return;
				if (null == Default) return;
				int sign = 0;
				if (credit.TransactionType == UserCreditTransactionType.Borrow)
				{
					sign = 1;
				}
				else if (credit.TransactionType == UserCreditTransactionType.Return)
				{
					sign = -1;
				}
				if (sign == 0) return; // not allow other type.
				try
				{
					Default.BeginTransaction();

					credit.RefId = credit.PKId; // set reference id.
					credit.PKId = Guid.NewGuid(); // always create new.
					credit.TransactionDate = DateTime.Now;
					credit.TransactionType = UserCreditTransactionType.Undo;

					balance.ST25 += sign * credit.ST25;
					balance.ST50 += sign * credit.ST50;
					balance.BHT1 += sign * credit.BHT1;
					balance.BHT2 += sign * credit.BHT2;
					balance.BHT5 += sign * credit.BHT5;
					balance.BHT10 += sign * credit.BHT10;
					balance.BHT20 += sign * credit.BHT20;
					balance.BHT50 += sign * credit.BHT50;
					balance.BHT100 += sign * credit.BHT100;
					balance.BHT500 += sign * credit.BHT500;
					balance.BHT1000 += sign * credit.BHT1000;

					balance.UserBHTTotal += -1 * sign * credit.BHTTotal;

					Save(credit);
					TSBBalance.Save(balance);

					Default.Commit();
				}
				catch
				{
					Default.Rollback();
				}
			}
		}

		public static List<UserCredit> GetUserCredits(TSB tsb)
		{
			lock (sync)
			{
				if (null == tsb) return new List<UserCredit>();

				string cmd = string.Empty;
				cmd += "SELECT UserCredit.* ";
				cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
				cmd += "  FROM UserCredit, TSB ";
				cmd += " WHERE UserCredit.TSBId = TSB.TSBId ";
				cmd += "   AND UserCredit.TSBId = ? ";

				var rets = NQuery.Query<FKs>(cmd, tsb.TSBId).ToList();
				var results = new List<UserCredit>();
				if (null != rets)
				{
					rets.ForEach(ret =>
					{
						results.Add(ret.ToUserCredit());
					});
				}
				return results;
			}
		}
		*/

		#endregion
	}

	#endregion
}
