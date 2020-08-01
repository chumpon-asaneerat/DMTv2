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
    #region Enun

    public enum CouponType : int
    {
        BHT35 = 35,
        BHT80 = 80
    }

    #endregion

    #region CouponFactor

    /// <summary>
    /// The CouponFactor Data Model class.
    /// </summary>
    [TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    //[Table("CouponFactor")]
    public class CouponFactor : NTable<CouponFactor>
    {
        #region Internal Variable

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CouponFactor() : base() { }

        #endregion

        #region Public Properties

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion
}
