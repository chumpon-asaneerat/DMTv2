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
		#region Internal Variables

		private string _NetworkId = string.Empty;
		private bool _Active = false;

		#endregion

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
		/// Gets or sets TSBNameEN.
		/// </summary>
		[PropertyOrder(102)]
		public override string TSBNameEN
		{
			get { return base.TSBNameEN; }
			set { base.TSBNameEN = value; }
		}
		/// <summary>
		/// Gets or sets TSBNameTH.
		/// </summary>
		[PropertyOrder(103)]
		public override string TSBNameTH
		{
			get { return base.TSBNameTH; }
			set { base.TSBNameTH = value; }
		}
		/// <summary>
		/// Gets or sets NetworkId.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets NetworkId.")]
		[MaxLength(10)]
		[PeropertyMapName("NetworkId")]
		[PropertyOrder(104)]
		public virtual string NetworkId
		{
			get
			{
				return _NetworkId;
			}
			set
			{
				if (_NetworkId != value)
				{
					_NetworkId = value;
					this.RaiseChanged("NetworkId");
				}
			}
		}
		/// <summary>
		/// Gets or sets is active TSB.
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets is active TSB.")]
		[ReadOnly(true)]
		[PeropertyMapName("Active")]
		[PropertyOrder(105)]
		public virtual bool Active
		{
			get
			{
				return _Active;
			}
			set
			{
				if (_Active != value)
				{
					_Active = value;
					this.RaiseChanged("Active");
				}
			}
		}

		#endregion
	}

	#endregion
}
