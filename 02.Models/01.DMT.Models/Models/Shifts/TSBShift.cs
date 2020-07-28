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
	#region TSBShift

	/// <summary>
	/// The TSBShift Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	//[Table("Plaza")]
	public class TSBShift : TSBShiftModelBase<Plaza>
	{
		#region Internal Variables

		private int _TSBShiftId = 0;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets PK Id.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets PK Id.")]
		[ReadOnly(true)]
		[PrimaryKey]
		[AutoIncrement]
		[PeropertyMapName("TSBShiftId")]
		[PropertyOrder(800)]
		public virtual int TSBShiftId
		{
			get
			{
				return _TSBShiftId;
			}
			set
			{
				if (_TSBShiftId != value)
				{
					_TSBShiftId = value;
					this.RaiseChanged("TSBShiftId");
				}
			}
		}

		#endregion
	}

	#endregion
}
