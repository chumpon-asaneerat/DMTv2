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
	public class UserCouponTransaction
	{
		#region Enum

		#endregion

		#region Internal Variables

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

		#endregion

		#region Internal Class

		#endregion

		#region Static Methods

		#endregion
	}

	#endregion
}
