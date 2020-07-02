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
    #region TSB

    /// <summary>
    /// The TSB Data Model class.
    /// </summary>
    //[Table("TSB")]
    public class TSB : TSBBase<TSB>
    {
        #region Intenral Variables

        private string _NetworkId = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSB() : base() { }

        #endregion

        #region Public Proprties

        /// <summary>
        /// Gets or sets NetworkId.
        /// </summary>
        [MaxLength(10)]
        [PeropertyMapName("NetworkId")]
        public string NetworkId
        {
            get
            {
                return _NetworkId;
            }
            set
            {
                if (_NetworkId != value)
                {
                    _NetworkId = value;
                    this.RaiseChanged("NetworkId");
                }
            }
        }

        #endregion

        #region Static Methods

        public static void SetActive(string tsbId)
        {
            lock (sync)
            {
                // inactive all TSBs
                string cmd = string.Empty;
                cmd += "UPDATE TSB ";
                cmd += "   SET Active = 0";
                NQuery.Execute(cmd);
                // Set active TSB
                cmd = string.Empty;
                cmd += "UPDATE TSB ";
                cmd += "   SET Active = 1 ";
                cmd += " WHERE TSBId = ? ";
                NQuery.Execute(cmd, tsbId);
            }
        }

        public static TSB GetCurrent()
        {
            lock (sync)
            {
                // inactive all TSBs
                string cmd = string.Empty;
                cmd += "SELECT * FROM TSB ";
                cmd += " WHERE Active = 1 ";
                var results = NQuery.Query<TSB>(cmd, true);
                return (null != results) ? results.FirstOrDefault() : null;
            }
        }

        #endregion
    }

    #endregion
}
