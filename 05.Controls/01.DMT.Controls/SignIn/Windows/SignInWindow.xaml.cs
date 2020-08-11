#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;

#endregion

namespace DMT.Windows
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SignInWindow()
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

            var user = ops.Users.GetByLogIn(
                Search.Users.ByLogIn.Create(userId, pwd));
            if (null == user || _roles.IndexOf(user.RoleId) == -1)
            {
                Console.WriteLine("LogIn Failed");
                txtMsg.Text = "LogIn Failed";

                txtUserId.SelectAll();
                txtUserId.Focus();
                return;
            }
            _user = user;
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region Public Methods

        public void Setup(params string[] roles)
        {
            _roles.Clear();
            _roles.AddRange(roles);
        }

        public User User { get { return _user;} }

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
    }
}
