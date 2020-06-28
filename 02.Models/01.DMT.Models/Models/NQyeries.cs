#region Using

using System;
using System.Collections.Generic;
using System.Linq;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using System.ComponentModel;
using DMT.Services;
// required for JsonIgnore.
using Newtonsoft.Json;
using NLib;
using NLib.Reflection;

#endregion

namespace DMT.Models
{
    #region NQuery

    /// <summary>
    /// The NQuery abstract class.
    /// </summary>
    public abstract class NQuery
    {
        #region Static Methods

        #endregion
    }

    #endregion

    #region NQuery<T>

    /// <summary>
    /// The NQuery (Generic) abstract class.
    /// </summary>
    /// <typeparam name="T">The Target Class.</typeparam>
    public abstract class NQuery<T>
    {
        #region Static Methods

        #endregion
    }

    #endregion
}
