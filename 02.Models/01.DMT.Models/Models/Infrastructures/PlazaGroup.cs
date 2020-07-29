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
	#region PlazaGroup

	/// <summary>
	/// The PlazaGroup Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	//[Table("Plaza")]
	public class PlazaGroup : NTable<PlazaGroup>
	{
		#region Intenral Variables

		private string _PlazaGroupId = string.Empty;
		private string _PlazaGroupNameEN = string.Empty;
		private string _PlazaGroupNameTH = string.Empty;
		private string _Direction = string.Empty;

		private string _TSBId = string.Empty;
		private string _TSBNameEN = string.Empty;
		private string _TSBNameTH = string.Empty;

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public PlazaGroup() : base() { }

		#endregion

		#region Public Proprties

		#region Common

		/// <summary>
		/// Gets or sets PlazaId.
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets PlazaId.")]
		[PrimaryKey, MaxLength(10)]
		[PeropertyMapName("PlazaGroupId")]
		public string PlazaGroupId
		{
			get
			{
				return _PlazaGroupId;
			}
			set
			{
				if (_PlazaGroupId != value)
				{
					_PlazaGroupId = value;
					this.RaiseChanged("PlazaGroupId");
				}
			}
		}
		/// <summary>
		/// Gets or sets PlazaGroupNameEN
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets PlazaGroupNameEN")]
		[MaxLength(100)]
		[PeropertyMapName("PlazaGroupNameEN")]
		public string PlazaGroupNameEN
		{
			get
			{
				return _PlazaGroupNameEN;
			}
			set
			{
				if (_PlazaGroupNameEN != value)
				{
					_PlazaGroupNameEN = value;
					this.RaiseChanged("PlazaGroupNameEN");
				}
			}
		}
		/// <summary>
		/// Gets or sets PlazaGroupNameTH
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets PlazaGroupNameTH")]
		[MaxLength(100)]
		[PeropertyMapName("PlazaGroupNameTH")]
		public string PlazaGroupNameTH
		{
			get
			{
				return _PlazaGroupNameTH;
			}
			set
			{
				if (_PlazaGroupNameTH != value)
				{
					_PlazaGroupNameTH = value;
					this.RaiseChanged("PlazaGroupNameTH");
				}
			}
		}
		/// <summary>
		/// Gets or sets Direction
		/// </summary>
		[Category("Plaza Group")]
		[Description("Gets or sets Direction")]
		[MaxLength(10)]
		[PeropertyMapName("Direction")]
		public string Direction
		{
			get
			{
				return _Direction;
			}
			set
			{
				if (_Direction != value)
				{
					_Direction = value;
					this.RaiseChanged("Direction");
				}
			}
		}

		#endregion

		#region TSB

		/// <summary>
		/// Gets or sets TSBId.
		/// </summary>
		[Category("TSB")]
		[Description("Gets or sets TSBId.")]
		[ReadOnly(true)]
		[MaxLength(10)]
		[PeropertyMapName("TSBId")]
		public string TSBId
		{
			get
			{
				return _TSBId;
			}
			set
			{
				if (_TSBId != value)
				{
					_TSBId = value;
					this.RaiseChanged("TSBId");
				}
			}
		}
		/// <summary>
		/// Gets or sets TSBNameEN.
		/// </summary>
		[Category("TSB")]
		[Description("Gets or sets TSBNameEN.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("TSBNameEN")]
		public virtual string TSBNameEN
		{
			get
			{
				return _TSBNameEN;
			}
			set
			{
				if (_TSBNameEN != value)
				{
					_TSBNameEN = value;
					this.RaiseChanged("TSBNameEN");
				}
			}
		}
		/// <summary>
		/// Gets or sets TSBNameTH.
		/// </summary>
		[Category("TSB")]
		[Description("Gets or sets TSBNameTH.")]
		[ReadOnly(true)]
		[Ignore]
		[PeropertyMapName("TSBNameTH")]
		public virtual string TSBNameTH
		{
			get
			{
				return _TSBNameTH;
			}
			set
			{
				if (_TSBNameTH != value)
				{
					_TSBNameTH = value;
					this.RaiseChanged("TSBNameTH");
				}
			}
		}

		#endregion

		#region Status (DC)

		/// <summary>
		/// Gets or sets Status (1 = Sync, 0 = Unsync, etc..)
		/// </summary>
		[Category("DataCenter")]
		[Description("Gets or sets Status (1 = Sync, 0 = Unsync, etc..)")]
		[ReadOnly(true)]
		[PeropertyMapName("Status")]
		[PropertyOrder(10001)]
		public int Status
		{
			get
			{
				return _Status;
			}
			set
			{
				if (_Status != value)
				{
					_Status = value;
					this.RaiseChanged("Status");
				}
			}
		}
		/// <summary>
		/// Gets or sets LastUpdated (Sync to DC).
		/// </summary>
		[Category("DataCenter")]
		[Description("Gets or sets LastUpdated (Sync to DC).")]
		[ReadOnly(true)]
		[PeropertyMapName("LastUpdate")]
		[PropertyOrder(10002)]
		public DateTime LastUpdate
		{
			get { return _LastUpdate; }
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

		#endregion

		#region Internal Class

		public class FKs : PlazaGroup
		{
			#region TSB

			/// <summary>
			/// Gets or sets TSBNameEN.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("TSBNameEN")]
			public override string TSBNameEN
			{
				get { return base.TSBNameEN; }
				set { base.TSBNameEN = value; }
			}
			/// <summary>
			/// Gets or sets TSBNameTH.
			/// </summary>
			[MaxLength(100)]
			[PeropertyMapName("TSBNameTH")]
			public override string TSBNameTH
			{
				get { return base.TSBNameTH; }
				set { base.TSBNameTH = value; }
			}

			#endregion

			#region Public Methods

			public PlazaGroup ToPlazaGroup()
			{
				PlazaGroup inst = new PlazaGroup();
				this.AssignTo(inst); // set all properties to new instance.
				return inst;
			}

			#endregion
		}

		#endregion

		#region Static Methods

		public static List<PlazaGroup> Gets(SQLiteConnection db)
		{
			if (null == db) return new List<PlazaGroup>();
			lock (sync)
			{
				string cmd = string.Empty;
				cmd += "SELECT PlazaGroup.* ";
				cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
				cmd += "  FROM PlazaGroup, TSB ";
				cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";

				var rets = NQuery.Query<FKs>(cmd).ToList();
				var results = new List<PlazaGroup>();
				if (null != rets)
				{
					rets.ForEach(ret =>
					{
						results.Add(ret.ToPlazaGroup());
					});
				}

				return results;
			}
		}
		public static List<PlazaGroup> Gets()
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return Gets(db);
			}
		}
		public static PlazaGroup Get(SQLiteConnection db, string plazaGroupId)
		{
			if (null == db) return null;
			lock (sync)
			{
				string cmd = string.Empty;
				cmd += "SELECT PlazaGroup.* ";
				cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
				cmd += "  FROM PlazaGroup, TSB ";
				cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
				cmd += "   AND PlazaGroup.PlazaGroupId = ? ";
				var ret = NQuery.Query<FKs>(cmd, plazaGroupId).FirstOrDefault();
				return (null != ret) ? ret.ToPlazaGroup() : null;
			}
		}
		public static PlazaGroup Get(string plazaGroupId)
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return Get(db, plazaGroupId);
			}
		}
		public static List<PlazaGroup> GetTSBPlazaGroups(TSB value)
		{
			lock (sync)
			{
				if (null == value) return new List<PlazaGroup>();
				return GetTSBPlazaGroups(value.TSBId);
			}
		}
		public static List<PlazaGroup> GetTSBPlazaGroups(string tsbId)
		{
			lock (sync)
			{
				string cmd = string.Empty;
				cmd += "SELECT PlazaGroup.* ";
				cmd += "     , TSB.TSBNameEN, TSB.TSBNameTH ";
				cmd += "  FROM PlazaGroup, TSB ";
				cmd += " WHERE PlazaGroup.TSBId = TSB.TSBId ";
				cmd += "   AND PlazaGroup.TSBId = ? ";

				var rets = NQuery.Query<FKs>(cmd, tsbId).ToList();
				var results = new List<PlazaGroup>();
				if (null != rets)
				{
					rets.ForEach(ret =>
					{
						results.Add(ret.ToPlazaGroup());
					});
				}
				return results;
			}
		}

		#endregion
	}

	#endregion
}
