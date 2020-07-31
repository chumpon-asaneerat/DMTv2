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

		#endregion

		#region Internal Variables

		private int _TransactionId = 0;
		private DateTime _TransactionDate = DateTime.MinValue;
		//private TransactionTypes _TransactionType = TransactionTypes.Initial;

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
		/*
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
		*/
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

		#endregion
	}

	#endregion
}
