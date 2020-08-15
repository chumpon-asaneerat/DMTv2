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

#endregion

namespace DMT.TA.Windows.Collector.Credit
{
    /// <summary>
    /// Interaction logic for CollectorCreditBorrowWindow.xaml
    /// </summary>
    public partial class CollectorCreditBorrowWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorCreditBorrowWindow()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private UserCreditBorrowManager manager = new UserCreditBorrowManager();

        #region Button Handlers

        private void cmdSearchUser_Click(object sender, RoutedEventArgs e)
        {
            string userId = txtSearchUserId.Text;
            if (string.IsNullOrEmpty(userId)) return;

            var users = ops.Users.SearchById(Search.Users.ById.Create(userId)).Value();
            if (null != users && null != manager.UserBalance)
            {
                if (users.Count == 1)
                {
                    manager.SetUser(users[0]);
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
                    manager.SetUser(win.SelectedUser);
                }
            }
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            manager.PlazaGroup = cbPlzaGroups.SelectedItem as PlazaGroup;
            if (null == manager.PlazaGroup)
            {
                // No Plaza Group Selectd.
                return;
            }

            if (manager.HasNegative())
            {
                MessageBox.Show(Application.Current.MainWindow, 
                    "ไม่สามารถดำเนินการบันทึกข้อมูลได้ เนื่องจากระบบพบว่ามีการ ยอดการยืมเงินในบางรายการ เกินจำนวนที่่ด่านมีอยู่", 
                    "Toll Admin");
                return;
            }

            if (manager.Save())
            {
                this.DialogResult = true;
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        private void LoadPlazaGroups()
        {
            var tsb = ops.TSB.GetCurrent().Value();
            if (null != tsb)
            {
                var plazaGroups = ops.TSB.GetTSBPlazaGroups(tsb).Value();
                cbPlzaGroups.DisplayMemberPath = "PlazaGroupNameTH";
                cbPlzaGroups.ItemsSource = plazaGroups;
                if (null != plazaGroups && plazaGroups.Count > 0)
                {
                    cbPlzaGroups.SelectedIndex = 0;
                }
            }
        }

        public void Setup(TSB tsb, UserCreditBalance credit)
        {
            manager.Setup(tsb, credit);
            if (manager.IsNew)
            {
                if (manager.UserBalance.UserCreditId == 0)
                {
                    panelSearch.Visibility = Visibility.Visible;
                }
                else panelSearch.Visibility = Visibility.Collapsed;
            }
            LoadPlazaGroups();

            this.DataContext = manager.UserBalance;

            manager.UserBalance.Description = "ยอดยืมปัจจุบัน";
            manager.UserBalance.HasRemark = false;

            manager.Transaction.Description = "ยืมเงิน";

            manager.ResultBalance.Description = "ยอดด่านคงเหลือ";
            manager.ResultBalance.HasRemark = false;

            srcEntry.DataContext = manager.UserBalance;
            usrEntry.DataContext = manager.Transaction;
            sumEntry.DataContext = manager.ResultBalance;
        }
    }
}
