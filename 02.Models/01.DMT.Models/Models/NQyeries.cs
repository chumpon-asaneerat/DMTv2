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
    public abstract class NQuery : DMTModelBase
    {
        #region Static Variables and Properties

        /// <summary>
        /// sync object used for lock concurrent access.
        /// </summary>
        protected static object sync = new object();
        /// <summary>
        /// Gets default Connection.
        /// </summary>
        public static SQLiteConnection Default { get; set; }

        #endregion
    }

    #endregion

    #region NQuery<T>

    /// <summary>
    /// The NQuery (Generic) abstract class.
    /// </summary>
    /// <typeparam name="T">The Target Class.</typeparam>
    public abstract class NQuery<T> : NQuery
        where T : NQuery, new()
    {
        #region Static Methods

        /// <summary>
        /// Query.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="query">The query string.</param>
        /// <param name="args">The query arguments.</param>
        /// <returns>Returns query result in List.</returns>
        public static List<T> Query(SQLiteConnection db, string query, params object[] args)
        {
            lock (sync)
            {
                List<T> results = null;
                if (null == db || string.IsNullOrEmpty(query)) return results;
                // execute query.
                results = db.Query<T>(query, args).ToList();
                return results;
            }
        }
        /// <summary>
        /// Query.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <param name="args">The query arguments.</param>
        /// <returns>Returns query result in List.</returns>
        public static List<T> Query(string query, params object[] args)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Query(query, args);
            }
        }

        #endregion
    }

    #endregion
}
