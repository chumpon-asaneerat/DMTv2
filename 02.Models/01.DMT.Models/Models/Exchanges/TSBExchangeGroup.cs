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
	/// The TSBExchangeTransaction Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("TSBExchangeTransaction")]
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

		private StateTypes _State = StateTypes.Request;

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

		#endregion
	}

	#endregion
}
