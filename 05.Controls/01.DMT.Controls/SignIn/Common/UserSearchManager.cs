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

        #region Public Methods

        public User SelectUser(string userId, params string[] roles)
        {
            User ret = null;

            var search = Search.Users.ById.Create(userId, roles);
            var users = ops.Users.SearchById(search).Value();
            if (null != users)
            {
                if (users.Count == 1)
                {
                    ret = users[0];
                }
                else if (users.Count > 1)
                {
                    var win = new Windows.UserFilterWindow();
                    // change title.
                    if (!string.IsNullOrEmpty(this.Title)) win.Title = this.Title;
                    win.Owner = Application.Current.MainWindow;
                    // setup user list for selection.
                    win.Setup(users);
                    if (win.ShowDialog() == false || null == win.SelectedUser)
                    {
                        // No user selected.
                        ret = null;
                    }
                    else
                    {
                        // User selected.
                        ret = win.SelectedUser;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Gets or sets Popup window title.
        /// </summary>
        public string Title { get; set; }

        #endregion
    }
}
