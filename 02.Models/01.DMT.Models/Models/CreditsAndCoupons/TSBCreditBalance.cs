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
    #region TSBCreditBalance (For Query only)

    /// <summary>
    /// The TSBCreditBalance Data Model class.
    /// </summary>
    //[Table("TSBCreditBalance")]
    public class TSBCreditBalance : NTable<TSBCreditBalance>
    {
        #region Internal Variables

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBCreditBalance() : base() { }

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
