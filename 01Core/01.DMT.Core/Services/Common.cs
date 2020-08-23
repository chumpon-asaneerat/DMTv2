#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DMT.Services
{
    #region InstalledStatus

    /// <summary>
    /// The DMT Window Service Installed Status class.
    /// </summary>
    public class InstalledStatus
    {
        #region Public properties

        /// <summary>
        /// Gets (or internal set) all service count.
        /// </summary>
        public int ServiceCount { get; set; }
        /// <summary>
        /// Gets (or internal set) service install count.
        /// </summary>
        public int InstalledCount { get; set; }
        /// <summary>
        /// Gets (or internal set) is Plaza Local Service installed.
        /// </summary>
        public bool PlazaLocalServiceInstalled { get; set; }

        #endregion
    }

    #endregion
}
