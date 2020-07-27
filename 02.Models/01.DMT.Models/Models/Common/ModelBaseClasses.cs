#region Using

using System.ComponentModel;
using NLib;

// required for JsonIgnore attribute.
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

#endregion

namespace DMT.Models
{
    #region DataCenterModelBase (abstract)

    /// <summary>
    /// DataCenterModelBase (abstract).
    /// </summary>
    /// <typeparam name="T">The Target Class.</typeparam>
    public abstract class DataCenterModelBase<T> : NTable<T>
        where T : NTable, new()
    {
    }

    #endregion

    #region TSBModelBase (abstract)

    /// <summary>
    /// TSBModelBase (abstract).
    /// </summary>
    /// <typeparam name="T">The Target Class.</typeparam>
    public abstract class TSBModelBase<T> : DataCenterModelBase<T>
        where T : NTable, new()
    {
    }

    #endregion
}
