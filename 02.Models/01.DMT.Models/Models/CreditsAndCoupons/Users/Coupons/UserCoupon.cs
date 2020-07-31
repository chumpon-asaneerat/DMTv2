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
	public class UserCoupon
	{
		#region Enum

		#endregion

		#region Internal Variables

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

		#endregion

		#region Internal Class

		#endregion

		#region Static Methods

		#endregion
	}

	#endregion
}
