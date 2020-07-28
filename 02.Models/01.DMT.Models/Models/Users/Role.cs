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
	#region Role

	/// <summary>
	/// The Role Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	//[Table("Role")]
	public class Role : RoleModelBase<Shift>
	{
		#region Public Proprties

		/// <summary>
		/// Gets or sets RoleId
		/// </summary>
		[PrimaryKey]
		[PropertyOrder(601)]
		public override string RoleId
		{
			get { return base.RoleId; }
			set { base.RoleId = value; }
		}
		/// <summary>
		/// Gets or sets RoleNameEN
		/// </summary>
		[PropertyOrder(602)]
		public override string RoleNameEN
		{
			get { return base.RoleNameEN; }
			set { base.RoleNameEN = value; }
		}
		/// <summary>
		/// Gets or sets RoleNameTH
		/// </summary>
		[PropertyOrder(603)]
		public override string RoleNameTH
		{
			get { return base.RoleNameTH; }
			set { base.RoleNameTH = value; }
		}

		#endregion
	}

	#endregion
}
