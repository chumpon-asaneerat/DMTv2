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
	#region User

	/// <summary>
	/// The User Data Model class.
	/// </summary>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
	//[Table("User")]
	public class User : UserModelBase<User>
	{
		#region Internal Variables

		private string _UserName = string.Empty;
		private string _Password = string.Empty;
		private string _PasswordMD5 = string.Empty;
		private string _CardId = string.Empty;

		#endregion

		#region Public Proprties

		/// <summary>
		/// Gets or sets UserId
		/// </summary>
		[PrimaryKey]
		[PropertyOrder(701)]
		public override string UserId
		{
			get { return base.UserId; }
			set { base.UserId = value; }
		}
		/// <summary>
		/// Gets or sets FullNameEN
		/// </summary>
		[PropertyOrder(702)]
		public override string FullNameEN
		{
			get { return base.FullNameEN; }
			set { base.FullNameEN = value; }
		}
		/// <summary>
		/// Gets or sets FullNameTH
		/// </summary>
		[PropertyOrder(703)]
		public override string FullNameTH
		{
			get { return base.FullNameTH; }
			set { base.FullNameTH = value; }
		}
		/// <summary>
		/// Gets or sets UserName
		/// </summary>
		[Category("User")]
		[Description("Gets or sets UserName")]
		[MaxLength(50)]
		[PeropertyMapName("UserName")]
		[PropertyOrder(704)]
		public virtual string UserName
		{
			get
			{
				return _UserName;
			}
			set
			{
				if (_UserName != value)
				{
					_UserName = value;
					this.RaiseChanged("UserName");
				}
			}
		}
		/// <summary>
		/// Gets or sets Password
		/// </summary>
		[Category("User")]
		[Description("Gets or sets Password")]
		[MaxLength(50)]
		[PeropertyMapName("Password")]
		[PropertyOrder(705)]
		public virtual string Password
		{
			get
			{
				return _Password;
			}
			set
			{
				if (_Password != value)
				{
					_Password = value;
					this.RaiseChanged("Password");
				}
			}
		}
		/// <summary>
		/// Gets or sets MD5 Hash
		/// </summary>
		[Category("User")]
		[Description("Gets or sets MD5 Hash")]
		[MaxLength(50)]
		[PeropertyMapName("PasswordMD5")]
		[PropertyOrder(706)]
		public virtual string PasswordMD5
		{
			get
			{
				return _PasswordMD5;
			}
			set
			{
				if (_PasswordMD5 != value)
				{
					_PasswordMD5 = value;
					this.RaiseChanged("PasswordMD5");
				}
			}
		}
		/// <summary>
		/// Gets or sets CardId
		/// </summary>
		[Category("User")]
		[Description("Gets or sets CardId")]
		[MaxLength(20)]
		[PeropertyMapName("CardId")]
		[PropertyOrder(707)]
		public virtual string CardId
		{
			get
			{
				return _CardId;
			}
			set
			{
				if (_CardId != value)
				{
					_CardId = value;
					this.RaiseChanged("CardId");
				}
			}
		}

		#endregion
	}

	#endregion
}
