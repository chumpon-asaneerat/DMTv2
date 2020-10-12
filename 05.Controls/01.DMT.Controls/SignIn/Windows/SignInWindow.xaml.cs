﻿#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using DMT.Controls;

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

        #region Loaded/Unloaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SmartcardManager.Instance.UserChanged += Instance_UserChanged;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
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
                if (tabs.SelectedIndex == 0)
                {
                    _user = SmartcardManager.Instance.User;
                    CheckUser();
                }
                else if (tabs.SelectedIndex == 1)
                {
                    txtNewPassword.SelectAll();
                    txtNewPassword.Focus();
                }
            }
        }

        #endregion

        #region Button Handler(s)

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (tabs.SelectedIndex != 0) return;
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

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            SmartcardManager.Instance.Shutdown();
            this.DialogResult = false;
        }

        private void cmdChangePwd_Click(object sender, RoutedEventArgs e)
        {
            tabs.SelectedIndex = 1;
        }

        private void cmdOK2_Click(object sender, RoutedEventArgs e)
        {
            // Call change password.
            if (ChangePassword())
            {
                tabs.SelectedIndex = 0;
            }
        }

        private void cmdCancel2_Click(object sender, RoutedEventArgs e)
        {
            tabs.SelectedIndex = 0;
        }

        #endregion

        #region TextBox Keydown

        private void txtUserId_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tabs.SelectedIndex != 0) return;
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtPassword.SelectAll();
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tabs.SelectedIndex != 0) return;
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                cmdOK.Focus();
                e.Handled = true;
            }
        }

        private void txtUserId2_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tabs.SelectedIndex != 1) return;
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtPassword2.SelectAll();
                txtPassword2.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword2_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tabs.SelectedIndex != 1) return;
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtNewPassword.SelectAll();
                txtNewPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtNewPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tabs.SelectedIndex != 1) return;
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtConfirmPassword.SelectAll();
                txtConfirmPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtConfirmPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tabs.SelectedIndex != 1) return;
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                cmdOK2.Focus();
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
            this.DialogResult = true;
        }

        private bool ChangePassword()
        {
            bool ret = true;

            return ret;
        }

        #endregion

        #region Public Methods

        public void Setup(params string[] roles)
        {
            _roles.Clear();
            _roles.AddRange(roles);

            SmartcardManager.Instance.Start();
        }

        public User User { get { return _user;} }

        #endregion
    }
}
