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
    #region TSBBase<T>

    /// <summary>
    /// The TSB Base Data Model abstract class.
    /// </summary>
    /// <typeparam name="T">The target class type.</typeparam>
    public abstract class TSBBase<T> : NTable<T>
        where T : NTable, new()
    {
        #region Intenral Variables

        private string _TSBId = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBBase() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets TSBId.
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("TSBId")]
        public string TSBId
        {
            get
            {
                return _TSBId;
            }
            set
            {
                if (_TSBId != value)
                {
                    _TSBId = value;
                    this.RaiseChanged("TSBId");
                }
            }
        }

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion
}
