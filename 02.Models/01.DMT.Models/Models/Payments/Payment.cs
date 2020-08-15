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
	#region Payment

	/// <summary>
	/// The Payment Data Model Class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	[Serializable]
	[JsonObject(MemberSerialization.OptOut)]
	//[Table("Payment")]
	public class Payment : NTable<Payment>
	{
		#region Intenral Variables

		private string _PaymentId = string.Empty;
		private string _PaymentNameEN = string.Empty;
		private string _PaymentNameTH = string.Empty;

		private int _Status = 0;
		private DateTime _LastUpdate = DateTime.MinValue;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		public Payment() : base() { }

		#endregion

		#region Public Proprties

		#region Common

		/// <summary>
		/// Gets or sets Payment Id.
		/// </summary>
		[Category("Payment")]
		[Description("Gets or sets Payment Id.")]
		//[ReadOnly(true)]
		[PrimaryKey, MaxLength(20)]
		[PeropertyMapName("PaymentId")]
		public string PaymentId
		{
			get
			{
				return _PaymentId;
			}
			set
			{
				if (_PaymentId != value)
				{
					_PaymentId = value;
					this.RaiseChanged("PaymentId");
				}
			}
		}
		/// <summary>
		/// Gets or sets Payment Name EN
		/// </summary>
		[Category("Payment")]
		[Description("Gets or sets Payment Name EN.")]
		[MaxLength(50)]
		[PeropertyMapName("PaymentNameEN")]
		public string PaymentNameEN
		{
			get
			{
				return _PaymentNameEN;
			}
			set
			{
				if (_PaymentNameEN != value)
				{
					_PaymentNameEN = value;
					this.RaiseChanged("PaymentNameEN");
				}
			}
		}
		/// <summary>
		/// Gets or sets Payment Name TH.
		/// </summary>
		[Category("Payment")]
		[Description("Gets or sets Payment Name TH.")]
		[MaxLength(50)]
		[PeropertyMapName("PaymentNameTH")]
		public string PaymentNameTH
		{
			get
			{
				return _PaymentNameTH;
			}
			set
			{
				if (_PaymentNameTH != value)
				{
					_PaymentNameTH = value;
					this.RaiseChanged("PaymentNameTH");
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

		#region Static Methods

		public static NDbResult<List<Payment>> GetPayments(SQLiteConnection db)
		{
			var result = new NDbResult<List<Payment>>();

			if (null == db)
			{
				result.DbConenctFailed();
				return result;
			}

			lock (sync)
			{
				MethodBase med = MethodBase.GetCurrentMethod();
				try
				{
					string cmd = string.Empty;
					cmd += "SELECT * FROM Payment ";
					var data = NQuery.Query<Payment>(cmd);
					result.Success(data);

				}
				catch (Exception ex)
				{
					med.Err(ex);
					result.Error(ex);

				}
				return result;
			}
		}

		public static NDbResult<List<Payment>> GetPayments()
		{
			lock (sync)
			{
				SQLiteConnection db = Default;
				return GetPayments(db);
			}
		}

		#endregion
	}

	#endregion
}
