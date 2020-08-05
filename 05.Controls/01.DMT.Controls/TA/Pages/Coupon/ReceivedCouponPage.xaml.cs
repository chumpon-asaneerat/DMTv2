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

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
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
        }

        #endregion

        #region Timer Handler

        void timer_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        #endregion

        private DispatcherTimer timer = new DispatcherTimer();
        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private User _user = null;

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
            string userId = txtSearchUserId.Text;
            if (string.IsNullOrEmpty(userId)) return;

            var users = ops.Users.SearchById(Search.Users.ById.Create(userId));
            if (null != users)
            {
                if (users.Count == 1)
                {
                    _user = users[0];
                }
                else if (users.Count > 1)
                {
                    var win = new TA.Windows.Collector.Searchs.CollectorFilterWindow();
                    win.Owner = Application.Current.MainWindow;
                    win.Setup(users);
                    if (win.ShowDialog() == false || null == win.SelectedUser)
                    {
                        // No user selected.
                        return;
                    }
                    _user = win.SelectedUser;
                }

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

        private void cmdAppend_Click(object sender, RoutedEventArgs e)
        {
            var win = new DMT.TA.Windows.Coupon.CouponEditWindow();
            win.Owner = Application.Current.MainWindow;
            win.Setup(_user);
            if (win.ShowDialog() == false)
            {
                return;
            }
            RefreshList();
        }

        #endregion

        public void RefreshList()
        {
            grid.RefreshList();
        }
    }
}
