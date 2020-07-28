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
	//[Table("Lane")]
	public class Lane : LaneModelBase<Lane>
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets Lane PkId
		/// </summary>
		[PrimaryKey]
		[PropertyOrder(401)]
		public override int PkId
		{
			get { return base.PkId; }
			set { base.PkId = value; }
		}
		/// <summary>
		/// Gets or sets Lane No.
		/// </summary>
		[PropertyOrder(402)]
		public override int LaneNo
		{
			get { return base.LaneNo; }
			set { base.LaneNo = value; }
		}
		/// <summary>
		/// Gets or sets LaneId
		/// </summary>
		[PropertyOrder(403)]
		public override string LaneId
		{
			get { return base.LaneId; }
			set { base.LaneId = value; }
		}
		/// <summary>
		/// Gets or sets LaneType
		/// </summary>
		[PropertyOrder(404)]
		public override string LaneType
		{
			get { return base.LaneType; }
			set { base.LaneType = value; }
		}
		/// <summary>
		/// Gets or sets LaneAbbr
		/// </summary>
		[PropertyOrder(405)]
		public override string LaneAbbr
		{
			get { return base.LaneAbbr; }
			set { base.LaneAbbr = value; }
		}

		#endregion
	}

	#endregion
}
