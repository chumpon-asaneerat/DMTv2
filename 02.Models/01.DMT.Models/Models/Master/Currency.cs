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
using System.Reflection;

#endregion

namespace DMT.Models
{
	/// <summary>
	/// The TSB Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("MCurrency")]
	public class MCurrency : NTable<MCurrency>
	{
		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public MCurrency() : base() { }

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets currencyDenomId.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets currencyDenomId.")]
		[PrimaryKey]
		[PeropertyMapName("currencyDenomId")]
		public int currencyDenomId { get; set; }
		/// <summary>
		/// Gets or sets currencyId.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets currencyId.")]
		[PeropertyMapName("currencyId")]
		public int currencyId { get; set; }
		/// <summary>
		/// Gets or sets denomTypeId.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets denomTypeId.")]
		[PeropertyMapName("denomTypeId")]
		public int denomTypeId { get; set; }
		/// <summary>
		/// Gets or sets denomValue.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets denomValue.")]
		[PeropertyMapName("denomValue")]
		public decimal denomValue { get; set; }
		/// <summary>
		/// Gets or sets abbreviation.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets abbreviation.")]
		[PeropertyMapName("abbreviation")]
		public string abbreviation { get; set; }
		/// <summary>
		/// Gets or sets description.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets description.")]
		[PeropertyMapName("description")]
		public string description { get; set; }

		#endregion
	}
}
