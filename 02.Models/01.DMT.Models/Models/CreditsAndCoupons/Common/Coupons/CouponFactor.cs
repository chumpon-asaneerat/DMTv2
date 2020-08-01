#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using NLib;
using NLib.Design;
using NLib.Reflection;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
// required for JsonIgnore attribute.
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

#endregion

namespace DMT.Models
{
    #region Enun

    public enum CouponType : int
    {
        BHT35 = 35,
        BHT80 = 80
    }

    #endregion

    #region CouponFactor

    /// <summary>
    /// The CouponFactor Data Model class.
    /// </summary>
    [TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    //[Table("CouponFactor")]
    public class CouponFactor : NTable<CouponFactor>
    {
        #region Internal Variable

        private int _PKId = 0;
        private int _GroupPkId = 0;
        private CouponType _CouponType = CouponType.BHT35;
        private decimal _Factor = 665;

        private int _Status = 0;
        private DateTime _LastUpdate = DateTime.MinValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CouponFactor() : base() { }

        #endregion

        #region Public Properties

        #region Common

        /// <summary>
        /// Gets or sets PK Id.
        /// </summary>
        [Category("Coupon")]
        [Description("Gets or sets PK Id.")]
        [PrimaryKey, AutoIncrement]
        [PeropertyMapName("PKId")]
        public virtual int PKId
        {
            get { return _PKId; }
            set
            {
                if (_PKId != value)
                {
                    _PKId = value;
                    // Raise event.
                    this.RaiseChanged("PKId");
                }
            }
        }
        /// <summary>
        /// Gets or sets Group PK Id.
        /// </summary>
        [Category("Coupon")]
        [Description("Gets or sets Group PK Id.")]
        [PeropertyMapName("GroupPkId")]
        public virtual int GroupPkId
        {
            get { return _GroupPkId; }
            set
            {
                if (_GroupPkId != value)
                {
                    _GroupPkId = value;
                    // Raise event.
                    this.RaiseChanged("GroupPkId");
                }
            }
        }
        /// <summary>
        /// Gets or sets coupon type.
        /// </summary>
        [Category("Coupon")]
        [Description("Gets or sets coupon type.")]
        [PeropertyMapName("CouponType")]
        public virtual CouponType CouponType
        {
            get { return _CouponType; }
            set
            {
                if (_CouponType != value)
                {
                    _CouponType = value;
                    // Raise event.
                    this.RaiseChanged("CouponType");
                }
            }
        }
        /// <summary>
        /// Gets or sets coupon factor.
        /// </summary>
        [Category("Coupon")]
        [Description("Gets or sets coupon factor.")]
        [PeropertyMapName("Factor")]
        public virtual decimal Factor
        {
            get { return _Factor; }
            set
            {
                if (_Factor != value)
                {
                    _Factor = value;
                    // Raise event.
                    this.RaiseChanged("Factor");
                }
            }
        }

        #endregion

        #region Status (DC)

        /// <summary>
        /// Gets or sets Status (1 = Sync, 0 = Unsync, etc..)
        /// </summary>
        [Category("DataCenter")]
        [Description("Gets or sets Status (1 = Sync, 0 = Unsync, etc..)")]
        [ReadOnly(true)]
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
        [Category("DataCenter")]
        [Description("Gets or sets LastUpdated (Sync to DC).")]
        [ReadOnly(true)]
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

        #endregion

        #region Internal Class

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion
}
