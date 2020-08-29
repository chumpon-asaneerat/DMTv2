#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;

using DMT.Controls;

using NLib.Services;
using NLib.Reflection;
using NLib.Reports.Rdlc;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Interop;
using NLib;
using System.Windows.Threading;

#endregion

namespace DMT.TA.Pages.Coupon
{
    /// <summary>
    /// Interaction logic for ReceivedCouponPage.xaml
    /// </summary>
    public partial class ReceivedCouponPage : UserControl
    {
        #region Constructor

        public ReceivedCouponPage()
        {
            InitializeComponent();
        }

        #endregion

        private DispatcherTimer timer = new DispatcherTimer();
        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private User _user = null;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            grid.OnPrint += Grid_OnPrint;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            RefreshList();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (null == timer) return;
            timer.Stop();
            timer = null;

            grid.OnPrint -= Grid_OnPrint;
        }

        #endregion

        #region Timer Handler

        void timer_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        #endregion

        private void UpdateDateTime()
        {
            couponDate.Text = DateTime.Now.ToThaiDateTimeString("yyyy/MM/dd HH:mm");
        }

        #region Button Handlers

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdSearchUser_Click(object sender, RoutedEventArgs e)
        {
            SearchUser();
        }

        private void cmdAppend_Click(object sender, RoutedEventArgs e)
        {
            var win = new DMT.TA.Windows.Coupon.CouponEditWindow();
            win.Owner = Application.Current.MainWindow;
            win.Setup(_user);
            if (win.ShowDialog() == false)
            {
                return;
            }
            else
            {
                txtSearchUserId.Text = string.Empty;
                txtUserId.Text = string.Empty;
                txtUserName.Text = string.Empty;
            }
            RefreshList();
        }

        #endregion

        #region On Print Handler

        private void Grid_OnPrint(object sender, Controls.Collector.Coupon.PrintCouponReceivedReportEventArgs e)
        {
            var page = new Pages.Reports.CollectorCouponReceivedReportPage();
            page.CallerPage = this;
            page.Setup(e.User, e.Transaction);
            PageContentManager.Instance.Current = page;
        }

        #endregion

        public void RefreshList()
        {
            TSBCouponManager.Sync();
            grid.RefreshList();
        }

        private void txtSearchUserId_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                SearchUser();
            }
        }

        private void SearchUser()
        {
            if (!string.IsNullOrEmpty(txtSearchUserId.Text))
            {
                string userId = txtSearchUserId.Text;
                if (string.IsNullOrEmpty(userId)) return;

                UserSearchManager.Instance.Title = "กรุณาเลือกพนักงานเก็บเงิน";
                _user = UserSearchManager.Instance.SelectUser(userId, "CTC", "TC");
                if (null != _user)
                {
                    txtUserId.Text = _user.UserId;
                    txtUserName.Text = _user.FullNameTH;
                }
                else
                {
                    txtUserId.Text = string.Empty;
                    txtUserName.Text = string.Empty;
                }
            }
        }
    }
}
