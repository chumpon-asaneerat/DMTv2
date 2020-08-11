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
    /// Data Center Operations class.
    /// Main class that common all operations into properties.
    /// </summary>
    public partial class DCOperations
    {
        #region Static Constructor

        /// <summary>
        /// Static Constructor
        /// </summary>
        static DCOperations()
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
        public DCOperations() : base() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Base Address.
        /// </summary>
        public string BaseAddress
        {
            get
            {
                return string.Format(@"{0}://{1}:{2}/",
                    AppConsts.WindowsService.DC.WebServer.Protocol,
                    AppConsts.WindowsService.DC.WebServer.HostName,
                    AppConsts.WindowsService.DC.WebServer.PortNumber);
            }
        }

        #endregion
    }
}
