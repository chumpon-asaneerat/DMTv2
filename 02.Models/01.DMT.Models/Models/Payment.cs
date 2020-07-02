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
    #region Payment

    /// <summary>
    /// The Payment Data Model Class.
    /// </summary>
    //[Table("Payment")]
    public class Payment : NTable<Payment>
    {
        #region Intenral Variables

        private string _PaymentId = string.Empty;
        private string _PaymentNameEN = string.Empty;
        private string _PaymentNameTH = string.Empty;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Payment() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets PaymentId
        /// </summary>
        [PrimaryKey, MaxLength(20)]
        [PeropertyMapName("PaymentId")]
        public string PaymentId
        {
            get
            {
                return _PaymentId;
            }
            set
            {
                if (_PaymentId != value)
                {
                    _PaymentId = value;
                    this.RaiseChanged("PaymentId");
                }
            }
        }
        /// <summary>
        /// Gets or sets PaymentNameEN
        /// </summary>
        [MaxLength(50)]
        [PeropertyMapName("PaymentNameEN")]
        public string PaymentNameEN
        {
            get
            {
                return _PaymentNameEN;
            }
            set
            {
                if (_PaymentNameEN != value)
                {
                    _PaymentNameEN = value;
                    this.RaiseChanged("PaymentNameEN");
                }
            }
        }
        /// <summary>
        /// Gets or sets PaymentNameTH
        /// </summary>
        [MaxLength(50)]
        [PeropertyMapName("PaymentNameTH")]
        public string PaymentNameTH
        {
            get
            {
                return _PaymentNameTH;
            }
            set
            {
                if (_PaymentNameTH != value)
                {
                    _PaymentNameTH = value;
                    this.RaiseChanged("PaymentNameTH");
                }
            }
        }
        /// <summary>
        /// Gets or sets Status (1 = Sync, 0 = Unsync, etc..)
        /// </summary>
        [PeropertyMapName("Status")]
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
        [PeropertyMapName("LastUpdate")]
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

        #region Static Methods

        #endregion
    }

    #endregion
}
