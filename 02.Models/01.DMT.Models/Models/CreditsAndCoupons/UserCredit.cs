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
	#region UserCredit

	/// <summary>
	/// The UserCredit Data Model class.
	/// </summary>
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

		[Category("Runtime")]
		[Description("Gets or sets ReceivedBagVisibility.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("ReceivedBagVisibility")]
		public System.Windows.Visibility ReceivedBagVisibility
		{
			get { return (_State == StateTypes.Initial) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; }
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
		/// <summary>
		/// Gets or sets total (coin/bill) value in baht.
		/// </summary>
		[Category("Coin/Bill")]
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
					SELECT UserCredit.* 
						 , TSB.TSBNameEN
						 , TSB.TSBNameTH
						 , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction 
						 , User.FullNameEN, User.FullNameTH 
						 , ((
							 SELECT IFNULL(SUM(ST25), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(ST25), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS ST25
						 , ((
							 SELECT IFNULL(SUM(ST50), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(ST50), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS ST50
						 , ((
							 SELECT IFNULL(SUM(BHT1), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT1), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT1
						 , ((
							 SELECT IFNULL(SUM(BHT2), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT2), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT2
						 , ((
							 SELECT IFNULL(SUM(BHT5), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT5), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT5
						 , ((
							 SELECT IFNULL(SUM(BHT10), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT10), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT10
						 , ((
							 SELECT IFNULL(SUM(BHT20), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT20), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT20
						 , ((
							 SELECT IFNULL(SUM(BHT50), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT50), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT50
						 , ((
							 SELECT IFNULL(SUM(BHT100), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT100), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT100
						 , ((
							 SELECT IFNULL(SUM(BHT500), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT500), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT500
						 , ((
							 SELECT IFNULL(SUM(BHT1000), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT1000), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT1000
					  FROM UserCredit, TSB, PlazaGroup, User
					 WHERE PlazaGroup.TSBId = TSB.TSBId 
					   AND UserCredit.TSBId = TSB.TSBId 
					   AND UserCredit.PlazaGroupId = PlazaGroup.PlazaGroupId 
					   AND UserCredit.UserId = User.UserId 
					   AND UserCredit.TSBId = ? 
					   AND UserCredit.State <> ? 
				";

				var ret = NQuery.Query<FKs>(cmd,
					plazaGroup.TSBId, StateTypes.Completed).FirstOrDefault();
				
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
					SELECT UserCredit.* 
						 , TSB.TSBNameEN
						 , TSB.TSBNameTH
						 , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction 
						 , User.FullNameEN, User.FullNameTH 
						 , ((
							 SELECT IFNULL(SUM(ST25), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(ST25), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS ST25
						 , ((
							 SELECT IFNULL(SUM(ST50), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(ST50), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS ST50
						 , ((
							 SELECT IFNULL(SUM(BHT1), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT1), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT1
						 , ((
							 SELECT IFNULL(SUM(BHT2), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT2), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT2
						 , ((
							 SELECT IFNULL(SUM(BHT5), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT5), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT5
						 , ((
							 SELECT IFNULL(SUM(BHT10), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT10), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT10
						 , ((
							 SELECT IFNULL(SUM(BHT20), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT20), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT20
						 , ((
							 SELECT IFNULL(SUM(BHT50), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT50), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT50
						 , ((
							 SELECT IFNULL(SUM(BHT100), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT100), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT100
						 , ((
							 SELECT IFNULL(SUM(BHT500), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT500), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT500
						 , ((
							 SELECT IFNULL(SUM(BHT1000), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT1000), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT1000
					  FROM UserCredit, TSB, PlazaGroup, User
					 WHERE PlazaGroup.TSBId = TSB.TSBId 
					   AND UserCredit.TSBId = TSB.TSBId 
					   AND UserCredit.PlazaGroupId = PlazaGroup.PlazaGroupId 
					   AND UserCredit.UserId = User.UserId 
					   AND UserCredit.PlazaGroupId = ? 
					   AND UserCredit.State <> ? 
				";

				var ret = NQuery.Query<FKs>(cmd,
					plazaGroupId, StateTypes.Completed).FirstOrDefault();

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
					SELECT UserCredit.* 
						 , TSB.TSBNameEN
						 , TSB.TSBNameTH
						 , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction 
						 , User.FullNameEN, User.FullNameTH 
						 , ((
							 SELECT IFNULL(SUM(ST25), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(ST25), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS ST25
						 , ((
							 SELECT IFNULL(SUM(ST50), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(ST50), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS ST50
						 , ((
							 SELECT IFNULL(SUM(BHT1), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT1), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT1
						 , ((
							 SELECT IFNULL(SUM(BHT2), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT2), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT2
						 , ((
							 SELECT IFNULL(SUM(BHT5), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT5), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT5
						 , ((
							 SELECT IFNULL(SUM(BHT10), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT10), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT10
						 , ((
							 SELECT IFNULL(SUM(BHT20), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT20), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT20
						 , ((
							 SELECT IFNULL(SUM(BHT50), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT50), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT50
						 , ((
							 SELECT IFNULL(SUM(BHT100), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT100), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT100
						 , ((
							 SELECT IFNULL(SUM(BHT500), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT500), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT500
						 , ((
							 SELECT IFNULL(SUM(BHT1000), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							) -
							(
							 SELECT IFNULL(SUM(BHT1000), 0) 
							   FROM UserCreditTransaction 
							  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
								AND UserCreditTransaction.UserCreditId = UserCredit.UserCreditId
							)) AS BHT1000
					  FROM UserCredit, TSB, PlazaGroup, User
					 WHERE PlazaGroup.TSBId = TSB.TSBId 
					   AND UserCredit.TSBId = TSB.TSBId 
					   AND UserCredit.PlazaGroupId = PlazaGroup.PlazaGroupId 
					   AND UserCredit.UserId = User.UserId 
					   AND UserCredit.TSBId = ? 
					   AND UserCredit.State <> ? 
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
