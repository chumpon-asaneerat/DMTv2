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
    /// TAxTOD Operations class.
    /// Main class that common all operations into properties.
    /// </summary>
    public partial class TAxTODOperations
    {
        #region Static Constructor

        /// <summary>
        /// Static Constructor
        /// </summary>
        static TAxTODOperations()
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
        public TAxTODOperations() : base() { }

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
                if (null == ConfigManager.Instance.Plaza.TAxTOD) return string.Empty;

                return string.Format(@"{0}://{1}:{2}/",
                    ConfigManager.Instance.Plaza.TAxTOD.Http.Protocol,
                    ConfigManager.Instance.Plaza.TAxTOD.Http.HostName,
                    ConfigManager.Instance.Plaza.TAxTOD.Http.PortNumber);
            }
        }

        #endregion
    }
}
