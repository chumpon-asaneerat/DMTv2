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
	#region Lane

	/// <summary>
	/// The Lane Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	//[Table("Lane")]
	public class Lane : LaneModelBase<Lane>
	{
		#region Internal Variables

		private int _PkId = 0;
		private string _LaneType = string.Empty;
		private string _LaneAbbr = string.Empty;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets Lane PkId
		/// </summary>
		[Category("Lane")]
		[Description("Gets or sets Lane PkId")]
		[ReadOnly(true)]
		[PrimaryKey]
		[AutoIncrement]
		[PeropertyMapName("PkId")]
		[PropertyOrder(401)]
		public virtual int PkId
		{
			get
			{
				return _PkId;
			}
			set
			{
				if (_PkId != value)
				{
					_PkId = value;
					this.RaiseChanged("PkId");
				}
			}
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
		[Category("Lane")]
		[Description("Gets or sets LaneType")]
		[MaxLength(10)]
		[PeropertyMapName("LaneType")]
		[PropertyOrder(404)]
		public virtual string LaneType
		{
			get
			{
				return _LaneType;
			}
			set
			{
				if (_LaneType != value)
				{
					_LaneType = value;
					this.RaiseChanged("LaneType");
				}
			}
		}
		/// <summary>
		/// Gets or sets LaneAbbr
		/// </summary>
		[Category("Lane")]
		[Description("Gets or sets LaneAbbr")]
		[MaxLength(10)]
		[PeropertyMapName("LaneAbbr")]
		[PropertyOrder(405)]
		public virtual string LaneAbbr
		{
			get
			{
				return _LaneAbbr;
			}
			set
			{
				if (_LaneAbbr != value)
				{
					_LaneAbbr = value;
					this.RaiseChanged("LaneAbbr");
				}
			}
		}

		#endregion
	}

	#endregion
}
