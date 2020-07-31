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
	#region UserCoupon

	/// <summary>
	/// The UserCoupon Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("UserCoupon")]
	public class UserCoupon : NTable<UserCoupon>
	{
		#region Enum

		#endregion

		#region Internal Variables

		private int _UserCouponId = 0;
		private DateTime _UserCouponDate = DateTime.MinValue;
		//private StateTypes _State = StateTypes.Initial;

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public UserCoupon() : base() { }

		#endregion

		#region Private Methods

		#endregion

		#region Public Properties

		#region Common

		/// <summary>
		/// Gets or sets UserCouponId
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets UserCouponId")]
		[PrimaryKey, AutoIncrement]
		[ReadOnly(true)]
		[PeropertyMapName("UserCouponId")]
		public int UserCouponId
		{
			get
			{
				return _UserCouponId;
			}
			set
			{
				if (_UserCouponId != value)
				{
					_UserCouponId = value;
					this.RaiseChanged("UserCouponId");
				}
			}
		}
		/// <summary>
		/// Gets or sets UserCoupon Date.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets UserCredit Date.")]
		[ReadOnly(true)]
		[PeropertyMapName("UserCouponDate")]
		public DateTime UserCouponDate
		{
			get { return _UserCouponDate; }
			set
			{
				if (_UserCouponDate != value)
				{
					_UserCouponDate = value;
					// Raise event.
					this.RaiseChanged("UserCouponDate");
					this.RaiseChanged("UserCouponDateString");
					this.RaiseChanged("UserCouponDateTimeString");
				}
			}
		}
		/// <summary>
		/// Gets UserCoupon Date String.
		/// </summary>
		[Category("Common")]
		[Description("Gets UserCoupon Date String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string UserCouponDateString
		{
			get
			{
				var ret = (this.UserCouponDate == DateTime.MinValue) ? "" : this.UserCouponDate.ToThaiDateTimeString("dd/MM/yyyy");
				return ret;
			}
			set { }
		}
		/// <summary>
		/// Gets UserCoupon DateTime String.
		/// </summary>
		[Category("Common")]
		[Description("Gets UserCoupon DateTime String.")]
		[ReadOnly(true)]
		[JsonIgnore]
		[Ignore]
		public string UserCouponDateTimeString
		{
			get
			{
				var ret = (this.UserCouponDate == DateTime.MinValue) ? "" : this.UserCouponDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
				return ret;
			}
			set { }
		}
		/*
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
		*/
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

		public class FKs : UserCoupon
		{

		}

		#endregion

		#region Static Methods

		#endregion
	}

	#endregion
}
