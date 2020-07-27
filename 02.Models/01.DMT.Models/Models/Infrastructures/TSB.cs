#region Using

using System;
using System.ComponentModel;
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
	#region TSB

	/// <summary>
	/// The TSB Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	public class TSB : TSBModelBase<TSB>
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets NetworkId.
		/// </summary>
		public override string NetworkId
		{
			get { return base.NetworkId; }
			set { base.NetworkId = value; }
		}
		/// <summary>
		/// Gets or sets TSBNameEN.
		/// </summary>
		public override string TSBNameEN
		{
			get { return base.TSBNameEN; }
			set { base.TSBNameEN = value; }
		}
		/// <summary>
		/// Gets or sets TSBNameTH.
		/// </summary>
		public override string TSBNameTH
		{
			get { return base.TSBNameTH; }
			set { base.TSBNameTH = value; }
		}
		/// <summary>
		/// Gets or sets is active TSB.
		/// </summary>
		public override bool Active
		{
			get { return base.Active; }
			set { base.Active = value;  }
		}

		#endregion
	}

	#endregion
}
