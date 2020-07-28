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
	#region Shift

	/// <summary>
	/// The Shift Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	//[Table("Shift")]
	public class Shift : ShiftModelBase<Shift>
	{
		#region Public Proprties

		/// <summary>
		/// Gets or sets ShiftId.
		/// </summary>
		[PrimaryKey]
		[PropertyOrder(501)]
		public override int ShiftId
		{
			get { return base.ShiftId; }
			set { base.ShiftId = value; }
		}
		/// <summary>
		/// Gets or sets Name TH.
		/// </summary>
		[PropertyOrder(502)]
		public override string ShiftNameTH
		{
			get { return base.ShiftNameTH; }
			set { base.ShiftNameTH = value; }
		}
		/// <summary>
		/// Gets or sets Name EN.
		/// </summary>
		[PropertyOrder(503)]
		public override string ShiftNameEN
		{
			get { return base.ShiftNameEN; }
			set { base.ShiftNameEN = value; }
		}

		#endregion
	}

	#endregion
}
