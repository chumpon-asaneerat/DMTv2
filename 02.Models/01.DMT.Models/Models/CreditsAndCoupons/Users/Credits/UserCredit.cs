#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;

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
	public class UserCredit : NTable<UserCredit>
	{
		#region Enum

		public enum StateTypes : int
		{
			Initial = 0,
			// received bag
			Received = 1,
			// Returns all credit.
			Completed = 2
		}

		#endregion

		#region Internal Variables

		// For Runtime Used
		private string _description = string.Empty;
		private bool _hasRemark = false;

		private int _UserCreditId = 0;
		private DateTime _UserCreditDate = DateTime.MinValue;
		private StateTypes _State = StateTypes.Initial;
		private string _BagNo = string.Empty;
		private string _BeltNo = string.Empty;

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

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public UserCredit() : base() { }

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
		[JsonIgnore]
		[Ignore]
		[PeropertyMapName("RemarkVisibility")]
		public System.Windows.Visibility RemarkVisibility
		{
			get { return (_hasRemark) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; }
			set { }
		}
		[Category("Runtime")]
		[Description("Gets or sets ReceivedBagVisibility.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PeropertyMapName("ReceivedBagVisibility")]
		public System.Windows.Visibility ReceivedBagVisibility
		{
			get { return (_State == StateTypes.Initial) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; }
			set { }
		}

		#endregion

		#region Valid Colors

		[Category("Runtime")]
		[Description("Gets or sets Foreground color for ST25.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public SolidColorBrush ST25Foreground
		{
			get { return (IsValidST25) ? BlackForeground : RedForeground; }
			set { }
		}
		[Category("Runtime")]
		[Description("Gets or sets Foreground color for ST50.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public SolidColorBrush ST50Foreground
		{
			get { return (IsValidST50) ? BlackForeground : RedForeground; }
			set { }
		}
		[Category("Runtime")]
		[Description("Gets or sets Foreground color for BHT1.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public SolidColorBrush BHT1Foreground
		{
			get { return (IsValidBHT1) ? BlackForeground : RedForeground; }
			set { }
		}
		[Category("Runtime")]
		[Description("Gets or sets Foreground color for BHT2.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public SolidColorBrush BHT2Foreground
		{
			get { return (IsValidBHT2) ? BlackForeground : RedForeground; }
			set { }
		}
		[Category("Runtime")]
		[Description("Gets or sets Foreground color for BHT5.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public SolidColorBrush BHT5Foreground
		{
			get { return (IsValidBHT5) ? BlackForeground : RedForeground; }
			set { }
		}
		[Category("Runtime")]
		[Description("Gets or sets Foreground color for BHT10.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public SolidColorBrush BHT10Foreground
		{
			get { return (IsValidBHT10) ? BlackForeground : RedForeground; }
			set { }
		}
		[Category("Runtime")]
		[Description("Gets or sets Foreground color for BHT20.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public SolidColorBrush BHT20Foreground
		{
			get { return (IsValidBHT20) ? BlackForeground : RedForeground; }
			set { }
		}
		[Category("Runtime")]
		[Description("Gets or sets Foreground color for BHT50.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public SolidColorBrush BHT50Foreground
		{
			get { return (IsValidBHT50) ? BlackForeground : RedForeground; }
			set { }
		}
		[Category("Runtime")]
		[Description("Gets or sets Foreground color for BHT100.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public SolidColorBrush BHT100Foreground
		{
			get { return (IsValidBHT100) ? BlackForeground : RedForeground; }
			set { }
		}
		[Category("Runtime")]
		[Description("Gets or sets Foreground color for BHT500.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public SolidColorBrush BHT500Foreground
		{
			get { return (IsValidBHT500) ? BlackForeground : RedForeground; }
			set { }
		}
		[Category("Runtime")]
		[Description("Gets or sets Foreground color for BHT1000.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public SolidColorBrush BHT1000Foreground
		{
			get { return (IsValidBHT1000) ? BlackForeground : RedForeground; }
			set { }
		}

		#endregion

		#region Common

		/// <summary>
		/// Gets or sets UserCreditId
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets UserCreditId")]
		[PrimaryKey, AutoIncrement]
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
		/// <summary>
		/// Gets or sets UserCredit Date.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets UserCredit Date.")]
		[ReadOnly(true)]
		[PeropertyMapName("UserCreditDate")]
		public DateTime UserCreditDate
		{
			get { return _UserCreditDate; }
			set
			{
				if (_UserCreditDate != value)
				{
					_UserCreditDate = value;
					// Raise event.
					this.RaiseChanged("UserCreditDate");
					this.RaiseChanged("UserCreditDateString");
					this.RaiseChanged("UserCreditDateTimeString");
				}
			}
		}
		/// <summary>
		/// Gets UserCredit Date String.
		/// </summary>
		[Category("Common")]
		[Description("Gets UserCredit Date String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string UserCreditDateString
		{
			get
			{
				var ret = (this.UserCreditDate == DateTime.MinValue) ? "" : this.UserCreditDate.ToThaiDateTimeString("dd/MM/yyyy");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets UserCredit DateTime String.
		/// </summary>
		[Category("Common")]
		[Description("Gets UserCredit DateTime String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string UserCreditDateTimeString
		{
			get
			{
				var ret = (this.UserCreditDate == DateTime.MinValue) ? "" : this.UserCreditDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
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
		/// <summary>
		/// Gets or sets Bag Number.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Bag Number.")]
		//[ReadOnly(true)]
		[MaxLength(10)]
		[PeropertyMapName("BagNo")]
		public string BagNo
		{
			get { return _BagNo; }
			set
			{
				if (_BagNo != value)
				{
					_BagNo = value;
					// Raise event.
					this.RaiseChanged("BagNo");
				}
			}
		}
		/// <summary>
		/// Gets or sets Belt Number.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Belt Number.")]
		//[ReadOnly(true)]
		[MaxLength(20)]
		[PeropertyMapName("BeltNo")]
		public string BeltNo
		{
			get { return _BeltNo; }
			set
			{
				if (_BeltNo != value)
				{
					_BeltNo = value;
					// Raise event.
					this.RaiseChanged("BeltNo");
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

		#region PlazaGroup

		/// <summary>
		/// Gets or sets PlazaGroupId.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets PlazaGroupId.")]
		[ReadOnly(true)]
		[MaxLength(10)]
		[PeropertyMapName("PlazaGroupId")]
		public string PlazaGroupId
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
					this.RaiseChanged("IsValidST25");
					this.RaiseChanged("ST25Foreground");

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
					this.RaiseChanged("IsValidST50");
					this.RaiseChanged("ST50Foreground");
					this.RaiseChanged("BHT1Foreground");

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
					this.RaiseChanged("IsValidBHT1");
					this.RaiseChanged("BHT2Foreground");

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
					this.RaiseChanged("IsValidBHT2");
					this.RaiseChanged("BHT5Foreground");

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
					this.RaiseChanged("IsValidBHT5");
					this.RaiseChanged("BHT5Foreground");

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
					this.RaiseChanged("IsValidBHT10");
					this.RaiseChanged("BHT10Foreground");

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
					this.RaiseChanged("IsValidBHT20");
					this.RaiseChanged("BHT20Foreground");

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
					this.RaiseChanged("IsValidBHT50");
					this.RaiseChanged("BHT50Foreground");

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
					this.RaiseChanged("IsValidBHT100");
					this.RaiseChanged("BHT100Foreground");

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
					this.RaiseChanged("IsValidBHT500");
					this.RaiseChanged("BHT500Foreground");

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
					this.RaiseChanged("IsValidBHT1000");
					this.RaiseChanged("BHT1000Foreground");

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
		[PropertyOrder(21)]
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
					this.RaiseChanged("IsValidST25");
					this.RaiseChanged("ST25Foreground");

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
		[PropertyOrder(22)]
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
					this.RaiseChanged("IsValidST50");
					this.RaiseChanged("ST50Foreground");

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
		[PropertyOrder(23)]
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
					this.RaiseChanged("IsValidBHT1");
					this.RaiseChanged("BHT1Foreground");

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
		[PropertyOrder(24)]
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
					this.RaiseChanged("IsValidBHT2");
					this.RaiseChanged("BHT2Foreground");

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
		[PropertyOrder(25)]
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
					this.RaiseChanged("IsValidBHT5");
					this.RaiseChanged("BHT5Foreground");

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
		[PropertyOrder(26)]
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
					this.RaiseChanged("IsValidBHT10");
					this.RaiseChanged("BHT10Foreground");

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
		[PropertyOrder(27)]
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
					this.RaiseChanged("IsValidBHT20");
					this.RaiseChanged("BHT20Foreground");

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
		[PropertyOrder(28)]
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
					this.RaiseChanged("IsValidBHT50");
					this.RaiseChanged("BHT50Foreground");

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
		[PropertyOrder(29)]
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
					this.RaiseChanged("IsValidBHT100");
					this.RaiseChanged("BHT100Foreground");

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
		[PropertyOrder(30)]
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
					this.RaiseChanged("IsValidBHT500");
					this.RaiseChanged("BHT500Foreground");

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
		[PropertyOrder(31)]
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
					this.RaiseChanged("IsValidBHT1000");
					this.RaiseChanged("BHT1000Foreground");

					CalcTotalAmount();
				}
			}
		}

		#endregion

		#region Coin/Bill (IsValid)

		/// <summary>
		/// Gets amount is exact match .25 baht coin.
		/// </summary>
		[Category("Coin/Bill (IsValid)")]
		[Description("Gets amount is exact match .25 baht coin.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(32)]
		public virtual bool IsValidST25
		{
			get { return (_AmtST25 % (decimal).25) == 0; }
			set { }
		}
		/// <summary>
		/// Gets amount is exact match .50 baht coin.
		/// </summary>
		[Category("Coin/Bill (IsValid)")]
		[Description("Gets amount is exact match .50 baht coin.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(33)]
		public virtual bool IsValidST50
		{
			get { return (_AmtST50 % (decimal).5) == 0; }
			set { }
		}
		/// <summary>
		/// Gets amount is exact match 1 baht coin.
		/// </summary>
		[Category("Coin/Bill (IsValid)")]
		[Description("Gets amount is exact match 1 baht coin.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(34)]
		public virtual bool IsValidBHT1
		{
			get { return _AmtBHT1 == _CntBHT1; }
			set { }
		}
		/// <summary>
		/// Gets amount is exact match 2 baht coin.
		/// </summary>
		[Category("Coin/Bill (IsValid)")]
		[Description("Gets amount is exact match 2 baht coin.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(35)]
		public virtual bool IsValidBHT2
		{
			get { return (_AmtBHT2 % 2) == 0; }
			set { }
		}
		/// <summary>
		/// Gets amount is exact match 5 baht coin.
		/// </summary>
		[Category("Coin/Bill (IsValid)")]
		[Description("Gets amount is exact match 5 baht coin.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(36)]
		public virtual bool IsValidBHT5
		{
			get { return (_AmtBHT5 % 5) == 0; }
			set { }
		}
		/// <summary>
		/// Gets amount is exact match 10 baht coin.
		/// </summary>
		[Category("Coin/Bill (IsValid)")]
		[Description("Gets amount is exact match 10 baht coin.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(37)]
		public virtual bool IsValidBHT10
		{
			get { return (_AmtBHT10 % 10) == 0; }
			set { }
		}
		/// <summary>
		/// Gets amount is exact match 20 baht bill.
		/// </summary>
		[Category("Coin/Bill (IsValid)")]
		[Description("Gets amount is exact match 20 baht bill.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(38)]
		public virtual bool IsValidBHT20
		{
			get { return (_AmtBHT20 % 20) == 0; }
			set { }
		}
		/// <summary>
		/// Gets amount is exact match 50 baht bill.
		/// </summary>
		[Category("Coin/Bill (IsValid)")]
		[Description("Gets amount is exact match 50 baht bill.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(39)]
		public virtual bool IsValidBHT50
		{
			get { return (_AmtBHT50 % 50) == 0; }
			set { }
		}
		/// <summary>
		/// Gets amount is exact match 100 baht bill.
		/// </summary>
		[Category("Coin/Bill (IsValid)")]
		[Description("Gets amount is exact match 100 baht bill.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(40)]
		public virtual bool IsValidBHT100
		{
			get { return (_AmtBHT100 % 100) == 0; }
			set { }
		}
		/// <summary>
		/// Gets amount is exact match 500 baht bill.
		/// </summary>
		[Category("Coin/Bill (IsValid)")]
		[Description("Gets amount is exact match 500 baht bill.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(41)]
		public virtual bool IsValidBHT500
		{
			get { return (_AmtBHT500 % 500) == 0; }
			set { }
		}
		/// <summary>
		/// Gets amount is exact match 1000 baht bill.
		/// </summary>
		[Category("Coin/Bill (IsValid)")]
		[Description("Gets amount is exact match 1000 baht bill.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		[PropertyOrder(41)]
		public virtual bool IsValidBHT1000
		{
			get { return (_AmtBHT1000 % 1000) == 0; }
			set { }
		}

		#endregion

		#region Coin/Bill (Summary)

		/// <summary>
		/// Gets or sets total (coin/bill) value in baht.
		/// </summary>
		[Category("Coin/Bill (Summary)")]
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

		public class FKs : UserCredit
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

			#region PlazaGroup

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

			#region Public Methods

			public UserCredit ToUserCredit()
			{
				UserCredit inst = new UserCredit();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}

			#endregion
		}

		#endregion

		#region Static Methods

		public static UserCredit GetActive(User user, PlazaGroup plazaGroup)
		{
			lock (sync)
			{
				if (null == user || null == plazaGroup) return null;


				string cmd = @"
					SELECT *
					  FROM UserCreditSummaryView
					 WHERE UserCreditSummaryView.UserId = ?
					   AND UserCreditSummaryView.TSBId = ? 
					   AND UserCreditSummaryView.State <> ? 
				";

				var ret = NQuery.Query<FKs>(cmd,
					user.UserId, plazaGroup.TSBId, StateTypes.Completed).FirstOrDefault();
				
				UserCredit inst;
				if (null == ret)
				{
					inst = Create();

					inst.TSBId = plazaGroup.TSBId;
					inst.TSBNameEN = plazaGroup.TSBNameEN;
					inst.TSBNameTH = plazaGroup.TSBNameTH;

					inst.PlazaGroupId = plazaGroup.PlazaGroupId;
					inst.PlazaGroupNameEN = plazaGroup.PlazaGroupNameEN;
					inst.PlazaGroupNameTH = plazaGroup.PlazaGroupNameTH;
					inst.Direction = plazaGroup.Direction;

					inst.UserId = user.UserId;
					inst.FullNameEN = user.FullNameEN;
					inst.FullNameTH = user.FullNameTH;

					inst.State = StateTypes.Initial;
				}
				else
				{
					inst = ret.ToUserCredit();
				}

				return inst;
			}
		}

		public static UserCredit GetActive(string userId, string plazaGroupId)
		{
			lock (sync)
			{
				if (string.IsNullOrWhiteSpace(userId) || 
					string.IsNullOrWhiteSpace(plazaGroupId)) return null;


				string cmd = @"
					SELECT *
					  FROM UserCreditSummaryView
					 WHERE UserCreditSummaryView.UserId = ?
					   AND UserCreditSummaryView.PlazaGroupId = ? 
					   AND UserCreditSummaryView.State <> ? 
				";

				var ret = NQuery.Query<FKs>(cmd,
					userId, plazaGroupId, StateTypes.Completed).FirstOrDefault();

				UserCredit inst = (null != ret) ? ret.ToUserCredit() : null;
				return inst;
			}
		}

		public static List<UserCredit> GetActives(TSB tsb)
		{
			lock (sync)
			{
				if (null == tsb) return null;

				string cmd = @"
					SELECT *
					  FROM UserCreditSummaryView
					 WHERE UserCreditSummaryView.TSBId = ? 
					   AND UserCreditSummaryView.State <> ? 
				";

				var rets = NQuery.Query<FKs>(cmd,
					tsb.TSBId, StateTypes.Completed).ToList();
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
		/*
		public static UserCredit Create(User user, PlazaGroup plazaGroup, 
			string bagNo, string beltNo)
		{
			lock (sync)
			{
				if (null == user || null == plazaGroup) return null;
				UserCredit inst = Create();

				inst.TSBId = plazaGroup.TSBId;
				inst.TSBNameEN = plazaGroup.TSBNameEN;
				inst.TSBNameTH = plazaGroup.TSBNameTH;

				inst.PlazaGroupId = plazaGroup.PlazaGroupId;
				inst.PlazaGroupNameEN = plazaGroup.PlazaGroupNameEN;
				inst.PlazaGroupNameTH = plazaGroup.PlazaGroupNameTH;
				inst.Direction = plazaGroup.Direction;

				inst.UserId = user.UserId;
				inst.FullNameEN = user.FullNameEN;
				inst.FullNameTH = user.FullNameTH;

				inst.BagNo = bagNo;
				inst.BeltNo = beltNo;

				return inst;
			}
		}
		*/
		public static void SaveCredit(UserCredit value)
		{
			lock (sync)
			{
				if (null == value) return;
				// set date if not assigned.
				if (value.UserCreditDate == DateTime.MinValue)
				{
					value.UserCreditDate = DateTime.Now;
				}
				Save(value);
			}
		}

		/*
		public static void Borrow(UserCredit credit, TSBCreditBalance balance)
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
					TSBCreditBalance.Save(balance);

					Default.Commit();
				}
				catch
				{
					Default.Rollback();
				}
			}
		}

		public static void Return(UserCredit credit, TSBCreditBalance balance)
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
					TSBCreditBalance.Save(balance);

					Default.Commit();
				}
				catch
				{
					Default.Rollback();
				}
			}
		}

		public static void Undo(UserCredit credit, TSBCreditBalance balance)
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
					TSBCreditBalance.Save(balance);

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
