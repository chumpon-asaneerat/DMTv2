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

#endregion

namespace DMT.Models
{
	#region UniqueCode

	/// <summary>
	/// The UniqueCode Data Model Class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("UniqueCode")]
	public class UniqueCode : NTable<UniqueCode>
	{
		#region Enum

		public enum ResetMode
		{
			Yearly = 1,
			Monthly = 2
		}

		#endregion

		#region Intenral Variables

		private string _Key = string.Empty;
		private ResetMode _Mode = ResetMode.Yearly;
		private string _Prefix = string.Empty;
		private int _LastNumber = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public UniqueCode() : base() { }

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets Key
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Key")]
		[PrimaryKey, MaxLength(50)]
		[PropertyMapName("Key")]
		public string Key
		{
			get
			{
				return _Key;
			}
			set
			{
				if (_Key != value)
				{
					_Key = value;
					this.RaiseChanged("Key");
				}
			}
		}
		/// <summary>
		/// Gets or sets Mode
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Mode")]
		[PropertyMapName("Mode")]
		public ResetMode Mode
		{
			get
			{
				return _Mode;
			}
			set
			{
				if (_Mode != value)
				{
					_Mode = value;
					this.RaiseChanged("Mode");
				}
			}
		}
		/// <summary>
		/// Gets or sets Prefix
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Prefix")]
		[PrimaryKey, MaxLength(30)]
		[PropertyMapName("Prefix")]
		public string Prefix
		{
			get
			{
				return _Prefix;
			}
			set
			{
				if (_Prefix != value)
				{
					_Prefix = value;
					this.RaiseChanged("Prefix");
				}
			}
		}
		/// <summary>
		/// Gets or sets Last Number
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Last Number")]
		[PropertyMapName("LastNumber")]
		public int LastNumber
		{
			get
			{
				return _LastNumber;
			}
			set
			{
				if (_LastNumber != value)
				{
					_LastNumber = value;
					this.RaiseChanged("LastNumber");
				}
			}
		}
		/// <summary>
		/// Gets or sets Last Update
		/// </summary>
		[Category("Common")]
		[Description("Gets or sets Last Update")]
		[ReadOnly(true)]
		[PropertyMapName("LastUpdate")]
		public DateTime LastUpdate
		{
			get
			{
				return _LastUpdate;
			}
			set
			{
				if (_LastUpdate != value)
				{
					_LastUpdate = value;
					this.RaiseChanged("LastUpdate");
				}
			}
		}

		#endregion

		#region Static Methods


		#endregion
	}

	#endregion
}
