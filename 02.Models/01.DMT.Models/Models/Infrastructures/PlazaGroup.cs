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
    #region PlazaGroup

    /// <summary>
    /// The PlazaGroup Data Model class.
    /// </summary>
    [TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
    //[Table("PlazaGroup")]
    public class PlazaGroup : TSBModelBase<PlazaGroup>
    {
        #region Public Properties

        #endregion
    }

    #endregion
}
