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
    #region PlazaBase<T>

    /// <summary>
    /// The Plaza Base Data Model abstract class.
    /// </summary>
    /// <typeparam name="T">The target class type.</typeparam>
    public abstract class PlazaBase<T> : TSBBase<T>
        where T : NTable, new()
    {
        #region Intenral Variables

        private string _PlazaId = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaBase() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets PlazaId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("PlazaId")]
        public string PlazaId
        {
            get
            {
                return _PlazaId;
            }
            set
            {
                if (_PlazaId != value)
                {
                    _PlazaId = value;
                    this.RaiseChanged("PlazaId");
                }
            }
        }

        #endregion
    }

    #endregion
}
