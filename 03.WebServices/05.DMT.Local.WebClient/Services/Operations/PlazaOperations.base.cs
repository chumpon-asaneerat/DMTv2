#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;

#endregion

namespace DMT.Services
{
    /// <summary>
    /// Plaza Operations class.
    /// Main class that common all operations into properties.
    /// </summary>
    public partial class LocalOperations
    {
        #region Static Constructor

        /// <summary>
        /// Static Constructor
        /// </summary>
        static LocalOperations()
        {
            // Required for HTTPS.
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 |
                SecurityProtocolType.Tls11 |
                SecurityProtocolType.Tls |
                (SecurityProtocolType)768 | (SecurityProtocolType)3072 |
                SecurityProtocolType.SystemDefault;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LocalOperations() : base() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Base Address.
        /// </summary>
        public string BaseAddress
        {
            get
            {
                if (null == ConfigManager.Instance.Plaza) return string.Empty;
                if (null == ConfigManager.Instance.Plaza.Local) return string.Empty;
                if (null == ConfigManager.Instance.Plaza.Local.Http) return string.Empty;

                return string.Format(@"{0}://{1}:{2}/",
                    ConfigManager.Instance.Plaza.Local.Http.Protocol,
                    ConfigManager.Instance.Plaza.Local.Http.HostName,
                    ConfigManager.Instance.Plaza.Local.Http.PortNumber);
            }
        }

        #endregion
    }
}
