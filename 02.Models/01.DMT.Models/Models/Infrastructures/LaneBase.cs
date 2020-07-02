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
    #region LaneBase<T>

    /// <summary>
    /// The Plaza Base Data Model abstract class.
    /// </summary>
    /// <typeparam name="T">The target class type.</typeparam>
    public abstract class LaneBase<T> : PlazaBase<T>
        where T : NTable, new()
    {
        #region Intenral Variables

        private int _LaneNo = 0;
        private string _LaneId = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LaneBase() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets Lane No.
        /// </summary>
        [PeropertyMapName("LaneNo")]
        public int LaneNo
        {
            get
            {
                return _LaneNo;
            }
            set
            {
                if (_LaneNo != value)
                {
                    _LaneNo = value;
                    this.RaiseChanged("LaneNo");
                }
            }
        }
        /// <summary>
        /// Gets or sets LaneId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("LaneId")]
        public string LaneId
        {
            get
            {
                return _LaneId;
            }
            set
            {
                if (_LaneId != value)
                {
                    _LaneId = value;
                    this.RaiseChanged("LaneId");
                }
            }
        }

        #endregion
    }

    #endregion
}
