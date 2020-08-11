#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using System.ComponentModel;
//using DMT.Services;
// required for JsonIgnore.
using Newtonsoft.Json;
using NLib;
using NLib.Reflection;
using System.Reflection;

#endregion

namespace DMT.Models
{
    // Note:
    // - The Default connection should seperate by Domain class but can initialize with
    //   value assigned in NTable class.
    // - Query static methods (in NTable<T> class) required for custom search/filter.

    #region NTable

    /// <summary>
    /// The NTable abstract class.
    /// </summary>
    public abstract class NTable : DMTModelBase
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

    #region NTable<T>

    /// <summary>
    /// The NTable (Generic) abstract class.
    /// </summary>
    /// <typeparam name="T">The Target Class.</typeparam>
    public abstract class NTable<T> : NTable
        where T : NTable, new()
    {
        #region Static Resources

        protected static SolidColorBrush RedForeground = new SolidColorBrush(Colors.Red);
        protected static SolidColorBrush BlackForeground = new SolidColorBrush(Colors.Black);

        #endregion

        #region Static Methods

        #region Create

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <returns>Returns new instance.</returns>
        public static T Create()
        {
            return new T();
        }

        #endregion

        #region Used Custom Connection

        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(SQLiteConnection db, T value)
        {
            lock (sync)
            {
                if (null == db || null == value) return false;
                // read mapping information.
                var map = db.GetMapping<T>(CreateFlags.None);
                if (null == map) return false;

                string tableName = map.TableName;
                string columnName = map.PK.Name;
                string propertyName = map.PK.PropertyName;
                // get pk id.
                object Id = PropertyAccess.GetValue(value, propertyName);
                // init query string.
                string cmd = string.Empty;
                cmd += string.Format("SELECT * FROM {0} WHERE {1} = ?", tableName, columnName);
                // execute query.
                var item = db.Query<T>(cmd, Id).FirstOrDefault();
                return (null != item);
            }
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to save to database.</param>
        public static NDbResult<T> Save(SQLiteConnection db, T value)
        {
            NDbResult<T> result = new NDbResult<T>();
            lock (sync)
            {
                if (null == db)
                {
                    result.ConenctFailed();
                    result.data = null;
                    return result;
                }
                if (null == db)
                {
                    result.ParameterIsNull();
                    return result;
                }
                MethodBase med = MethodBase.GetCurrentMethod();

                if (!Exists(db, value))
                {
                    try
                    {
                        db.Insert(value);
                        result.Success();
                        result.data = value;
                    }
                    catch (Exception ex)
                    {
                        med.Err(ex);
                        result.Error(ex);
                        result.data = null;
                    }
                }
                else
                {
                    //db.Update(value);
                    try
                    {
                        db.Update(value);
                        result.Success();
                        result.data = value;
                    }
                    catch (Exception ex)
                    {
                        med.Err(ex);
                        result.Error(ex);
                        result.data = null;
                    }
                }

                return result;
            }
        }
        /// <summary>
        /// Update relationship with children that assigned with relationship attribute.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="value">The item to load children.</param>
        public static void UpdateWithChildren(SQLiteConnection db, T value)
        {
            lock (sync)
            {
                if (null == db || null == value) return;
                db.UpdateWithChildren(value);
            }
        }
        /// <summary>
        /// Get All with children.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<T> GetAllWithChildren(SQLiteConnection db, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db) return new List<T>();
                return db.GetAllWithChildren<T>(recursive: recursive);
            }
        }
        /// <summary>
        /// Gets by Id with children.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="Id">The Id (primary key).</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static T GetWithChildren(SQLiteConnection db, object Id, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db || null == Id) return null;
                // read mapping information.
                var map = db.GetMapping<T>(CreateFlags.None);
                if (null == map) return null;

                string tableName = map.TableName;
                string columnName = map.PK.Name;
                string propertyName = map.PK.PropertyName;
                // init query string.
                string cmd = string.Empty;
                cmd += string.Format("SELECT * FROM {0} WHERE {1} = ?", tableName, columnName);
                // execute query.
                T item = db.Query<T>(cmd, Id).FirstOrDefault();
                if (null != item)
                {
                    // read children.
                    db.GetChildren(item, recursive);
                }
                return item;
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll(SQLiteConnection db)
        {
            lock (sync)
            {
                if (null == db) return 0;
                return db.DeleteAll<T>();
            }
        }
        /// <summary>
        /// Delete by Id with children.
        /// </summary>
        /// <param name="db">The connection.</param>
        /// <param name="Id">The Id (primary key).</param>
        /// <param name="recursive">True for load related nested children.</param>
        public static void DeleteWithChildren(SQLiteConnection db, object Id, bool recursive = false)
        {
            lock (sync)
            {
                if (null == db || null == Id) return;
                T inst = GetWithChildren(db, Id, recursive);
                db.Delete(inst, recursive);
            }
        }

        #endregion

        #region Used Default Connection

        /// <summary>
        /// Checks is item is already exists in database.
        /// </summary>
        /// <param name="value">The item to checks.</param>
        /// <returns>Returns true if item is already in database.</returns>
        public static bool Exists(T value)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return Exists(db, value);
            }
        }
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value">The item to save to database.</param>
        public static void Save(T value)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                Save(db, value);
            }
        }
        /// <summary>
        /// Update relationship with children that assigned with relationship attribute.
        /// </summary>
        /// <param name="value">The item to load children.</param>
        public static void UpdateWithChildren(T value)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                UpdateWithChildren(db, value);
            }
        }
        /// <summary>
        /// Gets All with children.
        /// </summary>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns List of all records</returns>
        public static List<T> GetAllWithChildren(bool recursive = false)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return GetAllWithChildren(db, recursive);
            }
        }
        /// <summary>
        /// Gets by Id with children.
        /// </summary>
        /// <param name="Id">The Id (primary key).</param>
        /// <param name="recursive">True for load related nested children.</param>
        /// <returns>Returns found record.</returns>
        public static T GetWithChildren(object Id, bool recursive = false)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return GetWithChildren(db, Id, recursive);
            }
        }
        /// <summary>
        /// Delete All.
        /// </summary>
        /// <returns>Returns number of rows deleted.</returns>
        public static int DeleteAll()
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                return DeleteAll(db);
            }
        }
        /// <summary>
        /// Delete by Id with children.
        /// </summary>
        /// <param name="Id">The Id (primary key).</param>
        /// <param name="recursive">True for load related nested children.</param>
        public static void DeleteWithChildren(object Id, bool recursive = false)
        {
            lock (sync)
            {
                SQLiteConnection db = Default;
                DeleteWithChildren(db, recursive);
            }
        }

        #endregion

        #endregion
    }

    #endregion
}
