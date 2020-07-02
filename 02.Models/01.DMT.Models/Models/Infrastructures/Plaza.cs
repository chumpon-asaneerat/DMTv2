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

#endregion

namespace DMT.Models
{
    #region Plaza

    /// <summary>
    /// The Plaza Data Model class.
    /// </summary>
    //[Table("Plaza")]
    public class Plaza : PlazaBase<Plaza>
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Plaza() : base() { }

        #endregion

        #region Static Methods

        public static List<Plaza> GetTSBPlazas(TSB value)
        {
            lock (sync)
            {
                if (null == value) return new List<Plaza>();
                return GetTSBPlazas(value.TSBId);
            }
        }
        public static List<Plaza> GetTSBPlazas(string tsbId)
        {
            lock (sync)
            {
                string cmd = string.Empty;
                cmd += "SELECT * FROM Plaza ";
                cmd += " WHERE TSBId = ?";
                return NQuery.Query<Plaza>(cmd, tsbId);
            }
        }

        #endregion
    }

    #endregion
}
