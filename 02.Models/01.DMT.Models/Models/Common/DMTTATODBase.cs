#region Using

using System;
using System.Collections.Generic;
using System.Linq;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;

// required for JsonIgnore.
using Newtonsoft.Json;
using NLib;
using NLib.Reflection;
using System.ComponentModel;

#endregion

namespace DMT.Models
{
    public abstract class DMTDataCenterBase<T> : NTable<T>
        where T : NTable, new()
    {
        #region Internal Variables

        #endregion

        #region Public Properties

        #endregion
    }

    public abstract class DMTTAEntryTable<T> : DMTDataCenterBase<T>
        where T : NTable, new()
    {
        #region Internal Variables

        #endregion

        #region Public Properties

        #endregion
    }
}
