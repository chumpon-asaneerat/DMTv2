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
    #region Shift

    /// <summary>
    /// The Shift Data Model class.
    /// </summary>
    //[Table("Shift")]
    public class Shift : NTable<Shift>
    {
        #region Intenral Variables

        private int _ShiftId = 0;
        private string _ShiftNameTH = string.Empty;
        private string _ShiftNameEN = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Shift() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets ShiftId.
        /// </summary>
        [PrimaryKey]
        [PeropertyMapName("ShiftId")]
        public int ShiftId
        {
            get
            {
                return _ShiftId;
            }
            set
            {
                if (_ShiftId != value)
                {
                    _ShiftId = value;
                    this.RaiseChanged("ShiftId");
                }
            }
        }
        /// <summary>
        /// Gets or sets Name TH.
        /// </summary>
        [MaxLength(50)]
        [PeropertyMapName("ShiftNameTH")]
        public string ShiftNameTH
        {
            get
            {
                return _ShiftNameTH;
            }
            set
            {
                if (_ShiftNameTH != value)
                {
                    _ShiftNameTH = value;
                    this.RaiseChanged("ShiftNameTH");
                }
            }
        }
        /// <summary>
        /// Gets or sets Name EN.
        /// </summary>
        [MaxLength(50)]
        [PeropertyMapName("ShiftNameEN")]
        public string ShiftNameEN
        {
            get
            {
                return _ShiftNameEN;
            }
            set
            {
                if (_ShiftNameEN != value)
                {
                    _ShiftNameEN = value;
                    this.RaiseChanged("ShiftNameEN");
                }
            }
        }

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion
}
