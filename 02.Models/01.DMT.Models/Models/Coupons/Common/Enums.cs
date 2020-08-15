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
    #region CouponType

    /// <summary>
    /// The Coupon Type enum.
    /// </summary>
    public enum CouponType : int
    {
        BHT35 = 35,
        BHT80 = 80
    }

    #endregion
}
