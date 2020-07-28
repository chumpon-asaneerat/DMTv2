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
	#region PlazaGroup

	/// <summary>
	/// The PlazaGroup Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	//[Table("Plaza")]
	public class Plaza : PlazaModelBase<Plaza>
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets PlazaId.
		/// </summary>
		[PrimaryKey]
		[PropertyOrder(301)]
		public override string PlazaId
		{
			get { return base.PlazaId; }
			set { base.PlazaId = value; }
		}
		/// <summary>
		/// Gets or sets PlazaNameEN
		/// </summary>
		[PropertyOrder(302)]
		public override string PlazaNameEN
		{
			get { return base.PlazaNameEN; }
			set { base.PlazaNameEN = value; }
		}
		/// <summary>
		/// Gets or sets PlazaNameTH
		/// </summary>
		[PropertyOrder(303)]
		public override string PlazaNameTH
		{
			get { return base.PlazaNameTH; }
			set { base.PlazaNameTH = value; }
		}

		#endregion
	}

	#endregion
}
