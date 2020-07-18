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
using System.ComponentModel;

#endregion

namespace DMT.Models
{
    #region TSBCouponBalance (For Query only)

    /// <summary>
    /// The TSBCouponBalance Data Model class.
    /// </summary>
    //[Table("TSBCouponBalance")]
    public class TSBCouponBalance : NTable<TSBCouponBalance>
    {
        #region Internal Variables

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBCouponBalance() : base() { }

        #endregion

        #region Public Properties

        #endregion

        #region Internal Class

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion
}
