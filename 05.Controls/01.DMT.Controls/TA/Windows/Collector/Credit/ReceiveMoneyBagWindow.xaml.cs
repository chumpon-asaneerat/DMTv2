#region Using

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;

#endregion

namespace DMT.TA.Windows.Collector.Credit
{
    /// <summary>
    /// Interaction logic for ReceiveMoneyBagWindow.xaml
    /// </summary>
    public partial class ReceiveMoneyBagWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReceiveMoneyBagWindow()
        {
            InitializeComponent();

            txtUserId.SelectAll();
            txtUserId.Focus();
        }

        #endregion

        #region Internal Variables

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private List<string> _roles = new List<string>();
        private string _userId = null;

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

            // TODO: MD5 Required here.
            var user = ops.Users.GetByLogIn(Search.Users.ByLogIn.Create(userId, pwd)).Value();
            if (null == user || _roles.IndexOf(user.RoleId) == -1)
            {
                //Console.WriteLine("LogIn Failed");
                txtMsg.Text = "LogIn Failed";

                txtUserId.SelectAll();
                txtUserId.Focus();
                return;
            }

            if (null != user && user.UserId != _userId)
            {
                txtMsg.Text = "LogIn Failed";

                txtUserId.SelectAll();
                txtUserId.Focus();
                return;
            }

            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
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

        public void Setup(string userId, string msg1, decimal? msg2)
        {
            _userId = userId;

            txtBagID.Text = msg1;

            if (msg2 != null)
                txtAmount.Text = msg2.Value.ToString("#,##0");
            else
                txtAmount.Text = "0";

            _roles.Clear();
            _roles.Add("COLLECTOR");
        }
    }
}
