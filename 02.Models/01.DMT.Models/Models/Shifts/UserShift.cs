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
	#region UserShift

	/// <summary>
	/// The UserShift Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	//[Table("Plaza")]
	public class UserShift : UserShiftModelBase<Plaza>
	{
		#region Internal Variables

		private int _UserShiftId = 0;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets User Shift PK Id.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets User Shift PK Id.")]
		[ReadOnly(true)]
		[PrimaryKey]
		[AutoIncrement]
		[PeropertyMapName("UserShiftId")]
		[PropertyOrder(900)]
		public virtual int UserShiftId
		{
			get
			{
				return _UserShiftId;
			}
			set
			{
				if (_UserShiftId != value)
				{
					_UserShiftId = value;
					this.RaiseChanged("UserShiftId");
				}
			}
		}

		#endregion
	}

	#endregion
}
