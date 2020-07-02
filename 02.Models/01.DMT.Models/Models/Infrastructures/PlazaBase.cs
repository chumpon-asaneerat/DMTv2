﻿#region Using

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
    public abstract class PlazaBase<T> : TSBBase<T>
        where T : NTable, new()
    {
        #region Intenral Variables

        private string _PlazaId = string.Empty;
        private string _PlazaNameEN = string.Empty;
        private string _PlazaNameTH = string.Empty;
        private string _Direction = string.Empty;

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
        [PrimaryKey, MaxLength(10)]
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
        /// <summary>
        /// Gets or sets TSBId
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("TSBId")]
        public new string TSBId
        {
            get { return base.TSBId; }
            set { base.TSBId = value; }
        }
        /// <summary>
        /// Gets or sets PlazaNameEN
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("PlazaNameEN")]
        public string PlazaNameEN
        {
            get
            {
                return _PlazaNameEN;
            }
            set
            {
                if (_PlazaNameEN != value)
                {
                    _PlazaNameEN = value;
                    this.RaiseChanged("PlazaNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets PlazaNameTH
        /// </summary>
        [MaxLength(100)]
        [PeropertyMapName("PlazaNameTH")]
        public string PlazaNameTH
        {
            get
            {
                return _PlazaNameTH;
            }
            set
            {
                if (_PlazaNameTH != value)
                {
                    _PlazaNameTH = value;
                    this.RaiseChanged("PlazaNameTH");
                }
            }
        }
        /// <summary>
        /// Gets or sets Direction
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("Direction")]
        public string Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                if (_Direction != value)
                {
                    _Direction = value;
                    this.RaiseChanged("Direction");
                }
            }
        }

        #endregion
    }
}
