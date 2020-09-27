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
            SearchUser();
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBagNo.Text) && !string.IsNullOrEmpty(txtBeltNo.Text))
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
            else
            {
                if (string.IsNullOrEmpty(txtBagNo.Text))
                {
                    MessageBox.Show(Application.Current.MainWindow,
                          "โปรดระบุ หมายเลขถุงเงิน",
                          "Toll Admin");

                    txtBagNo.SelectAll();
                    txtBagNo.Focus();
                }
                else if (string.IsNullOrEmpty(txtBeltNo.Text))
                {
                    MessageBox.Show(Application.Current.MainWindow,
                      "โปรดระบุ หมายเลขเข็มขัดนิรภัย",
                      "Toll Admin");

                    txtBeltNo.SelectAll();
                    txtBeltNo.Focus();
                }
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

            if (manager.UserBalance.UserCreditId == 0)
            {
                panelSearch.Visibility = Visibility.Visible;
            }
            else panelSearch.Visibility = Visibility.Collapsed;

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

        private void txtSearchUserId_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                SearchUser();

                txtBagNo.SelectAll();
                txtBagNo.Focus();
                e.Handled = true;
            }
        }

        private void txtBagNo_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtBeltNo.SelectAll();
                txtBeltNo.Focus();
                e.Handled = true;
            }
        }

        private void txtBeltNo_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                usrEntry.Focus();
                usrEntry.txtBHT1.SelectAll();
                usrEntry.txtBHT1.Focus();
                e.Handled = true;
            }
        }

        private void SearchUser()
        {
            if (!string.IsNullOrEmpty(txtSearchUserId.Text))
            {
                string userId = txtSearchUserId.Text;
                if (string.IsNullOrEmpty(userId)) return;

                UserSearchManager.Instance.Title = "กรุณาเลือกพนักงานเก็บเงิน";
                var user = UserSearchManager.Instance.SelectUser(userId, "CTC", "TC");
                if (null != user && null != manager.UserBalance)
                {
                    manager.SetUser(user);
                }
            }
        }
    }
}
