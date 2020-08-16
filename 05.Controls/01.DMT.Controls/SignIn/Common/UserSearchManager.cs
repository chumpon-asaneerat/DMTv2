#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;
using NLib.Reports.Rdlc;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Interop;
using NLib;
using System.Windows.Threading;

#endregion

namespace DMT.Controls
{
    /// <summary>
    /// The User Search Manager helper controls.
    /// </summary>
    public class UserSearchManager
    {
        #region Singelton

        private static UserSearchManager _instance = null;
        /// <summary>
        /// Singelton Access.
        /// </summary>
        public static UserSearchManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(UserSearchManager))
                    {
                        _instance = new UserSearchManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Internal Variables

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private UserSearchManager() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~UserSearchManager() { }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public User Search()
        {
            User ret = null;
            
            return ret;
        }

        public List<string> Roles { get; set; }

        #endregion
    }
}
