#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

using DMT.Models;
using DMT.Services;
using DMT.Controls;

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

            txtUserId.SelectAll();
            txtUserId.Focus();
        }

        #endregion

        #region Internal Variables

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private List<string> _roles = new List<string>();
        private User _user = null;

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SmartcardManager.Instance.UserChanged += Instance_UserChanged;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            SmartcardManager.Instance.UserChanged -= Instance_UserChanged;
            SmartcardManager.Instance.Shutdown();
        }

        #endregion

        #region Smartcard Handler(s)

        private void Instance_UserChanged(object sender, EventArgs e)
        {
            if (null != SmartcardManager.Instance.User)
            {
                _user = SmartcardManager.Instance.User;
                CheckUser(); ;
            }
        }

        #endregion

        #region Button Handler(s)

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            txtMsg.Text = string.Empty;

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

            var md5 = Utils.MD5.Encrypt(pwd);
            var ret = ops.Users.GetByLogIn(Search.Users.ByLogIn.Create(userId, md5));
            _user = ret.Value();

            CheckUser();
        }

        #endregion

        #region TextBox Keydown

        private void txtUserId_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtPassword.SelectAll();
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                cmdOK.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region Private Methods

        private void CheckUser()
        {
            if (null == _user || _roles.IndexOf(_user.RoleId) == -1)
            {
                txtMsg.Text = "LogIn Failed";
                txtUserId.SelectAll();
                txtUserId.Focus();
                return;
            }

            SmartcardManager.Instance.Shutdown();
            Controls.TAApp.User.Current = _user;
            // Init Main Menu
            PageContentManager.Instance.Current = new TA.Pages.Menu.MainMenu();
        }

        #endregion

        #region Public Methods

        public void Setup(params string[] roles)
        {
            _roles.Clear();
            _roles.AddRange(roles);

            SmartcardManager.Instance.Start();
        }

        public User User { get { return _user; } }

        #endregion
    }
}
