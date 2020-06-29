#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

using DMT.Models;
using DMT.Services;

#endregion

namespace DMT.Pages
{
    /// <summary>
    /// Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class SignInPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SignInPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<string> _roles = new List<string>();
        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        #endregion

        #region Button Handler(s)

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            string userId = txtUserId.Text.Trim();
            string pwd = txtPassword.Password.Trim();
            if (string.IsNullOrWhiteSpace(userId))
            {
                txtUserId.SelectAll();
                txtUserId.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(pwd))
            {
                txtPassword.SelectAll();
                txtPassword.Focus();
                return;
            }

            var user = ops.Users.GetByLogIn(
                Search.Users.ByLogIn.Create(userId, pwd));
            if (null == user || _roles.IndexOf(user.RoleId) == -1)
            {
                Console.WriteLine("LogIn Failed");
                txtUserId.SelectAll();
                txtUserId.Focus();
                return;
            }
            // Init Main Menu
            PageContentManager.Instance.Current = new TA.Pages.Menu.MainMenu();
        }

        #endregion

        #region Public Methods

        public void Setup(params string[] roles)
        {
            _roles.Clear();
            _roles.AddRange(roles);
        }

        #endregion
    }
}
