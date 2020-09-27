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
	#region TSBExchangeGroup

	/// <summary>
	/// The TSBExchangeGroup Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("TSBExchangeGroup")]
	public class TSBExchangeGroup : NTable<TSBExchangeGroup>
	{
		#region Enum

		/// <summary>
		/// The Request Types.
		/// </summary>
		public enum RequestTypes : int
		{
			/// <summary>
			/// Exchange with Account.
			/// </summary>
			Account = 1,
			/// <summary>
			/// Exchange internal.
			/// </summary>
			Internal = 2
		}
		/// <summary>
		/// The Request state enum.
		/// </summary>
		public enum StateTypes : int
		{
			/// <summary>
			/// Request by plaza.
			/// </summary>
			Request = 1,
			/// <summary>
			/// Canceled.
			/// </summary>
			Canceled = 2,
			/// <summary>
			/// Approve by account.
			/// </summary>
			Approve = 3,
			/// <summary>
			/// Reject by account.
			/// </summary>
			Reject = 4,
			/// <summary>
			/// Received by plaza.
			/// </summary>
			Received = 5,
			/// <summary>
			/// Return from plaza but account not update status.
			/// </summary>
			Return = 6,
			/// <summary>
			/// Completed when account received returns credits and update status.
			/// </summary>
			Completed = 9
		}
		/// <summary>
		/// The Finished Flags
		/// </summary>
		public enum FinishedFlags : int
		{
			/// <summary>
			/// Completed
			/// </summary>
			Completed = 0,
			/// <summary>
			/// Avaliable
			/// </summary>
			Avaliable = 1
		}

		#endregion

		#region Internal Variables

		private int _PkId = 0;
		private Guid _GroupId = Guid.Empty;

		private RequestTypes _RequestType = RequestTypes.Account;
		private StateTypes _State = StateTypes.Request;
		private FinishedFlags _FinishFlag = FinishedFlags.Avaliable;
		private DateTime _RequestDate = DateTime.MinValue;
		// TSB
		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public TSBExchangeGroup() : base() { }

		#endregion

		#region Private Methods

		#endregion

		#region Public Properties

		#region Common

		/// <summary>
		/// Gets or sets PkId
		/// </summary>
		[Category("Common")]
		[Description(" Gets or sets PkId")]
		[ReadOnly(true)]
		[PrimaryKey, AutoIncrement]
		[PropertyMapName("PkId")]
		public int PkId
		{
			get
			{
				return _PkId;
			}
			set
			{
				if (_PkId != value)
				{
					_PkId = value;
					this.RaiseChanged("PkId");
				}
			}
		}
		/// <summary>
		/// Gets or sets Exchange GroupId
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Exchange GroupId")]
		[ReadOnly(true)]
		[PropertyMapName("GroupId")]
		public Guid GroupId
		{
			get
			{
				return _GroupId;
			}
			set
			{
				if (_GroupId != value)
				{
					_GroupId = value;
					this.RaiseChanged("GroupId");
				}
			}
		}
		/// <summary>
		/// Gets or sets Request Type.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Request Type.")]
		[ReadOnly(true)]
		[PropertyMapName("RequestType")]
		public RequestTypes RequestType
		{
			get { return _RequestType; }
			set
			{
				if (_RequestType != value)
				{
					_RequestType = value;
					this.RaiseChanged("RequestType");
				}
			}
		}
		/// <summary>
		/// Gets or sets State.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets State.")]
		[ReadOnly(true)]
		[PropertyMapName("State")]
		public StateTypes State
		{
			get { return _State; }
			set
			{
				if (_State != value)
				{
					_State = value;
					this.RaiseChanged("State");
				}
			}
		}
		/// <summary>
		/// Gets or sets Finish Flag (0: Completed, 1: Avaliable).
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Is Finished (0: Completed, 1: Avaliable).")]
		[ReadOnly(true)]
		[PropertyMapName("FinishFlag")]
		public virtual FinishedFlags FinishFlag
		{
			get
			{
				return _FinishFlag;
			}
			set
			{
				if (_FinishFlag != value)
				{
					_FinishFlag = value;
					this.RaiseChanged("FinishFlag");
				}
			}
		}
		/// <summary>
		/// Gets or sets Request Date.
		/// </summary>
		[Category("Common")]
		[Description(" Gets or sets Request Date")]
		[ReadOnly(true)]
		[PropertyMapName("RequestDate")]
		public DateTime RequestDate
		{
			get
			{
				return _RequestDate;
			}
			set
			{
				if (_RequestDate != value)
				{
					_RequestDate = value;
					this.RaiseChanged("RequestDate");
					this.RaiseChanged("RequestDateString");
					this.RaiseChanged("RequestTimeString");
					this.RaiseChanged("RequestDateTimeString");
				}
			}
		}
		/// <summary>
		/// Gets Request Date String.
		/// </summary>
		[Category("Common")]
		[Description("Gets Request Date String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string RequestDateString
		{
			get
			{
				var ret = (this.RequestDate == DateTime.MinValue) ? "" : this.RequestDate.ToThaiDateTimeString("dd/MM/yyyy");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets Request Time String.
		/// </summary>
		[Category("Common")]
		[Description("Gets Request Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string RequestTimeString
		{
			get
			{
				var ret = (this.RequestDate == DateTime.MinValue) ? "" : this.RequestDate.ToThaiTimeString();
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets Request Date Time String.
		/// </summary>
		[Category("Common")]
		[Description("Gets Request Date Time String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string RequestDateTimeString
		{
			get
			{
				var ret = (this.RequestDate == DateTime.MinValue) ? "" : this.RequestDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
				return ret;
			}
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
		[MaxLength(10)]
		[PropertyMapName("TSBId")]
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
		/// Gets or sets TSB Name EN.
		/// </summary>
		[Category("TSB")]
		[Description("Gets or sets TSB Name EN.")]
		[ReadOnly(true)]
		[Ignore]
		[PropertyMapName("TSBNameEN")]
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
		/// Gets or sets TSB Name TH.
		/// </summary>
		[Category("TSB")]
		[Description("Gets or sets TSB Name TH.")]
		[ReadOnly(true)]
		[Ignore]
		[PropertyMapName("TSBNameTH")]
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

		#region Transactions

		/// <summary>
		/// Gets or sets Request transacton.
		/// </summary>
		[Category("Transaction")]
		[Description("Gets or sets Request transacton.")]
		[Ignore]
		public TSBExchangeTransaction Request { get; set; }
		/// <summary>
		/// Gets or sets Approve transacton.
		/// </summary>
		[Category("Transaction")]
		[Description("Gets or sets Approve transacton.")]
		[Ignore]
		public TSBExchangeTransaction Approve { get; set; }
		/// <summary>
		/// Gets or sets Received transacton.
		/// </summary>
		[Category("Transaction")]
		[Description("Gets or sets Received transacton.")]
		[Ignore]
		public TSBExchangeTransaction Received { get; set; }
		/// <summary>
		/// Gets or sets Returns transacton.
		/// </summary>
		[Category("Transaction")]
		[Description("Gets or sets Returns transacton.")]
		[Ignore]
		public TSBExchangeTransaction Return { get; set; }

		#endregion

		#region Status (DC)

		/// <summary>
		/// Gets or sets Status (1 = Sync, 0 = Unsync, etc..)
		/// </summary>
		[Category("DataCenter")]
		[Description("Gets or sets Status (1 = Sync, 0 = Unsync, etc..)")]
		[ReadOnly(true)]
		[PropertyMapName("Status", typeof(TSBExchangeGroup))]
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
		[PropertyMapName("LastUpdate", typeof(TSBExchangeGroup))]
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

		/// <summary>
		/// The internal FKs class for query data.
		/// </summary>
		public class FKs : TSBExchangeGroup, IFKs<TSBExchangeGroup>
		{
			#region TSB

			/// <summary>
			/// Gets or sets TSB Name EN.
			/// </summary>
			[MaxLength(100)]
			[PropertyMapName("TSBNameEN")]
			public override string TSBNameEN
			{
				get { return base.TSBNameEN; }
				set { base.TSBNameEN = value; }
			}
			/// <summary>
			/// Gets or sets TSB Name TH.
			/// </summary>
			[MaxLength(100)]
			[PropertyMapName("TSBNameTH")]
			public override string TSBNameTH
			{
				get { return base.TSBNameTH; }
				set { base.TSBNameTH = value; }
			}

			#endregion
		}

		#endregion

		#region Static Methods

		public static NDbResult<List<TSBExchangeGroup>> GetTSBExchangeGroupByDate(DateTime value)
		{
			var result = new NDbResult<List<TSBExchangeGroup>>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			if (null == value)
			{
				result.ParameterIsNull();
			}
			var tsb = TSB.GetCurrent().Value();
			if (null == tsb)
			{
				result.ParameterIsNull();
				return result;
			}

			return result;
		}

		/// <summary>
		/// Save TSB Exchange Group.
		/// </summary>
		/// <param name="value">The TSBExchangeGroup instance.</param>
		/// <returns>Returns save TSBExchangeGroup instance.</returns>
		public static NDbResult<TSBExchangeGroup> SaveTSBExchangeGroup(TSBExchangeGroup value)
		{
			var result = new NDbResult<TSBExchangeGroup>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}
			if (null == value)
			{
				result.ParameterIsNull();
			}
			if (value.GroupId == Guid.Empty)
			{
				value.GroupId = Guid.NewGuid();
			}
			result = Save(value);
			// save each transaction
			if (null != value.Request)
			{
				value.Request.GroupId = value.GroupId;
				TSBExchangeTransaction.Save(value.Request);
			}
			if (null != value.Approve)
			{
				value.Approve.GroupId = value.GroupId;
				TSBExchangeTransaction.Save(value.Approve);
			}
			if (null != value.Received)
			{
				value.Received.GroupId = value.GroupId;
				TSBExchangeTransaction.Save(value.Received);
			}
			if (null != value.Return)
			{
				value.Return.GroupId = value.GroupId;
				TSBExchangeTransaction.Save(value.Return);
			}

			return result;
		}

		#endregion
	}

	#endregion
}
