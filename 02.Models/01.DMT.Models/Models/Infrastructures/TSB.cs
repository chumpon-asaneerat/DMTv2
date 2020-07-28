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
	//[Table("TSB")]
	public class TSB : TSBModelBase<TSB>
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets TSBId.
		/// </summary>
		[PrimaryKey]
		[PropertyOrder(101)]
		public override string TSBId
		{
			get { return base.TSBId; }
			set { base.TSBId = value; }
		}
		/// <summary>
		/// Gets or sets NetworkId.
		/// </summary>
		[PropertyOrder(102)]
		public override string NetworkId
		{
			get { return base.NetworkId; }
			set { base.NetworkId = value; }
		}
		/// <summary>
		/// Gets or sets TSBNameEN.
		/// </summary>
		[PropertyOrder(103)]
		public override string TSBNameEN
		{
			get { return base.TSBNameEN; }
			set { base.TSBNameEN = value; }
		}
		/// <summary>
		/// Gets or sets TSBNameTH.
		/// </summary>
		[PropertyOrder(104)]
		public override string TSBNameTH
		{
			get { return base.TSBNameTH; }
			set { base.TSBNameTH = value; }
		}
		/// <summary>
		/// Gets or sets is active TSB.
		/// </summary>
		[PropertyOrder(105)]
		public override bool Active
		{
			get { return base.Active; }
			set { base.Active = value;  }
		}

		#endregion
	}

	#endregion
}
