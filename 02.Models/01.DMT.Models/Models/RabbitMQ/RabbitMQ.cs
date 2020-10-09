#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DMT.Models
{
    #region RabbitMQMessage

    /// <summary>
    /// The Common Rabbit MQ Message
    /// </summary>
    public class RabbitMQMessage
    {
        /// <summary>
        /// Gets or sets Parameter Name.
        /// </summary>
        public string parameterName { get; set; }
        /// <summary>
        /// Gets or sets TimeStamp.
        /// </summary>
        public DateTime? timestamp { get; set; }
        /// <summary>
        /// Gets or sets version.
        /// </summary>
        public int? version { get; set; }
    }

    #endregion

    #region RabbitMQMessage with Staff

    /// <summary>
    /// The RabbitMQStaff class.
    /// </summary>
    public class RabbitMQStaff
    {
        /// <summary>
        /// Gets or sets Staff Id.
        /// </summary>
        public string staffId { get; set; }
        /// <summary>
        /// Gets or sets Staff Family Name.
        /// </summary>
        public string staffFamilyName { get; set; }
        /// <summary>
        /// Gets or sets Staff First Name.
        /// </summary>
        public string staffFirstName { get; set; }
        /// <summary>
        /// Gets or sets Staff Middle Name.
        /// </summary>
        public string staffMiddleName { get; set; }
        /// <summary>
        /// Gets or sets Staff Title.
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Gets or sets Group.
        /// </summary>
        public int group { get; set; }
        /// <summary>
        /// Gets or sets Card S/N.
        /// </summary>
        public string cardSerialNo { get; set; }

        public static User ToLocal(RabbitMQStaff value)
        {
            if (null == value) return null;
            User ret = new User();

            ret.UserId = value.staffId;
            ret.PrefixEN = value.title;
            ret.FirstNameEN = value.staffFirstName;
            ret.MiddleNameEN = value.staffMiddleName;
            ret.LastNameEN = value.staffFamilyName;
            ret.PrefixTH = value.title;
            ret.FirstNameTH = value.staffFirstName;
            ret.MiddleNameEN = value.staffMiddleName;
            ret.LastNameTH = value.staffFamilyName;
            ret.Password = value.password;
            ret.CardId = value.cardSerialNo;

            return ret;
        }
    }

    /// <summary>
    /// The RabbitMQStaffMessage class.
    /// </summary>
    public class RabbitMQStaffMessage : RabbitMQMessage
    {
        /// <summary>
        /// Gets or sets staff list.
        /// </summary>
        public List<RabbitMQStaff> staff { get; set; }
    }


    #endregion
}
