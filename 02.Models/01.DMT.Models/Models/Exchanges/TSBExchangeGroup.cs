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

		public enum StateTypes : int
		{
			// Request.
			Request = 1,
			// Canceled.
			Canceled = 2,
			// Approve.
			Approve = 3,
			// Reject.
			Reject = 4,
			// Received.
			Received = 5,
			// Completed (reserved).
			Completed = 9
		}

		public enum FinishedFlags : int
		{
			Completed = 0,
			Avaliable = 1
		}

		#endregion

		#region Internal Variables

		private int _PkId = 0;
		private Guid _GroupId = Guid.NewGuid();

		private StateTypes _State = StateTypes.Request;
		// TSB
		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;

		private FinishedFlags _FinishFlag = FinishedFlags.Avaliable;

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
		[PeropertyMapName("PkId")]
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
		[PeropertyMapName("GroupId")]
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
		/// Gets or sets State.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets State.")]
		[ReadOnly(true)]
		[PeropertyMapName("State")]
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
		[PeropertyMapName("FinishFlag")]
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

		#endregion

		#region Public Properties

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

		public class FKs : TSBExchangeGroup
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

			#region Public Methods

			public TSBExchangeGroup ToTSBExchangeGroup()
			{
				TSBExchangeGroup inst = new TSBExchangeGroup();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}

			#endregion
		}

		#endregion

		#region Static Methods

		public static NDbResult<TSBExchangeGroup> SaveTSBExchangeGroup(TSBExchangeGroup value)
		{
			var result = new NDbResult<TSBExchangeGroup>();
			SQLiteConnection db = Default;
			if (null == db)
			{
				result.ConenctFailed();
				result.data = null;
				return result;
			}

			if (null == value)
			{
				result.ParameterIsNull();
				result.data = null;

			}
			result = Save(value);
			return result;
		}

		#endregion
	}

	#endregion
}
