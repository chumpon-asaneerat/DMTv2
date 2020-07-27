#region Using

using System;
using System.ComponentModel;
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
    #region PlazaGroup

    /// <summary>
    /// The PlazaGroup Data Model class.
    /// </summary>
    [TypeConverter(typeof(PropertySorterSupportExpandableTypeConverter))]
    //[Table("PlazaGroup")]
    public class PlazaGroup : PlazaGroupModelBase<PlazaGroup>
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets PlazaId.
        /// </summary>
        [PropertyOrder(201)]
        public override string PlazaGroupId
        {
            get { return base.PlazaGroupId; }
            set { base.PlazaGroupId = value; }
        }
        /// <summary>
        /// Gets or sets PlazaGroupNameEN
        /// </summary>
        [PropertyOrder(202)]
        public override string PlazaGroupNameEN
        {
            get { return base.PlazaGroupNameEN; }
            set { base.PlazaGroupNameEN = value; }
        }
        /// <summary>
        /// Gets or sets PlazaGroupNameTH
        /// </summary>
        [PropertyOrder(203)]
        public override string PlazaGroupNameTH
        {
            get { return base.PlazaGroupNameTH; }
            set { base.PlazaGroupNameTH = value; }
        }
        /// <summary>
        /// Gets or sets Direction
        /// </summary>
        [PropertyOrder(204)]
        public override string Direction
        {
            get { return base.Direction; }
            set { base.Direction = value; }
        }

        #endregion
    }

    #endregion
}
