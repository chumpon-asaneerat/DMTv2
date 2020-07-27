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
    #region DataCenterModelBase (abstract)

    /// <summary>
    /// DataCenterModelBase (abstract).
    /// </summary>
    /// <typeparam name="T">The Target Class.</typeparam>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
    public abstract class DataCenterModelBase<T> : NTable<T>
        where T : NTable, new()
    {
        #region Intenral Variables

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Public Proprties

        #region Status (DC)

        /// <summary>
        /// Gets or sets Status (1 = Sync, 0 = Unsync, etc..)
        /// </summary>
        [Category("DataCenter")]
        [Description("Gets or sets Status (1 = Sync, 0 = Unsync, etc..)")]
        [ReadOnly(true)]
        [PeropertyMapName("Status")]
        [PropertyOrder(101)]
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    this.RaiseChanged("Status");
                }
            }
        }
        /// <summary>
        /// Gets or sets LastUpdated (Sync to DC).
        /// </summary>
        [Category("DataCenter")]
        [Description("Gets or sets LastUpdated (Sync to DC).")]
        [ReadOnly(true)]
        [PeropertyMapName("LastUpdate")]
        [PropertyOrder(102)]
        public DateTime LastUpdate
        {
            get { return _LastUpdate; }
            set
            {
                if (_LastUpdate != value)
                {
                    _LastUpdate = value;
                    this.RaiseChanged("LastUpdate");
                }
            }
        }

        #endregion

        #endregion
    }

    #endregion

    #region TSBModelBase (abstract)

    /// <summary>
    /// TSBModelBase (abstract).
    /// </summary>
    /// <typeparam name="T">The Target Class.</typeparam>
	[TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
    public abstract class TSBModelBase<T> : DataCenterModelBase<T>
        where T : NTable, new()
    {
        #region Intenral Variables

        #endregion

        #region Public Proprties

        #endregion
    }

    #endregion
}
