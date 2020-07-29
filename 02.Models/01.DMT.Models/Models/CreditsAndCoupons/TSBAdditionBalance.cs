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
	#region TSBAdditionBalance (For Query only)

	/// <summary>
	/// The TSBAdditionBalance Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("TSBAdditionBalance")]
	public class TSBAdditionBalance : NTable<TSBAdditionBalance>
	{
		#region Internal Variables

		// For Runtime Used
		private string _description = string.Empty;
		private bool _hasRemark = false;

		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;

		// Additional Borrow
		private decimal _AdditionalBHTTotal = decimal.Zero;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public TSBAdditionBalance() : base() { }

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

		#region Additional

		/// <summary>
		/// Gets or sets additional borrow in baht.
		/// </summary>
		[Category("Additional")]
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

		#endregion

		#endregion

		#region Internal Class

		public class FKs : TSBAdditionBalance
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

			#endregion

			#region Public Methods

			public TSBAdditionBalance ToTSBAdditionBalance()
			{
				TSBAdditionBalance inst = new TSBAdditionBalance();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}

			#endregion
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Gets Active TSB Addition balance.
		/// </summary>
		/// <returns>Returns Current Active TSB Addition balance. If not found returns null.</returns>
		public static TSBAdditionBalance GetCurrent()
		{
			lock (sync)
			{
				var tsb = TSB.GetCurrent();
				return GetCurrent(tsb);
			}
		}
		/// <summary>
		/// Gets TSB Addition Balance.
		/// </summary>
		/// <param name="tsb">The target TSB to get balance.</param>
		/// <returns>Returns TSB Addition balance. If TSB not found returns null.</returns>
		public static TSBAdditionBalance GetCurrent(TSB tsb)
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
					  FROM TSB
					 WHERE TSB.TSBId = ?
				";
				var ret = NQuery.Query<FKs>(cmd, tsb.TSBId).FirstOrDefault();
				return (null != ret) ? ret.ToTSBAdditionBalance() : null;
			}
		}
		/// <summary>
		/// Gets All TSB Addition Balance.
		/// </summary>
		/// <returns>Returns List fo all TSB Addition balance.</returns>
		public static List<TSBAdditionBalance> Gets()
		{
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
					  FROM TSB
				";
				var rets = NQuery.Query<FKs>(cmd).ToList();
				var results = new List<TSBAdditionBalance>();
				if (null != rets)
				{
					rets.ForEach(ret =>
					{
						results.Add(ret.ToTSBAdditionBalance());
					});
				}
				return results;
			}
		}

		#endregion
	}

	#endregion
}
