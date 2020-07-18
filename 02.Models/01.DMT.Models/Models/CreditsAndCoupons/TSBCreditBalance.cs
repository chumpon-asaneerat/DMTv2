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

        private string _TSBId = string.Empty;
        private string _TSBNameEN = string.Empty;
        private string _TSBNameTH = string.Empty;
        // Coin/Bill
        private int _ST25 = 0;
        private int _ST50 = 0;
        private int _BHT1 = 0;
        private int _BHT2 = 0;
        private int _BHT5 = 0;
        private int _BHT10 = 0;
        private int _BHT20 = 0;
        private int _BHT50 = 0;
        private int _BHT100 = 0;
        private int _BHT500 = 0;
        private int _BHT1000 = 0;
        private decimal _BHTTotal = decimal.Zero;

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
