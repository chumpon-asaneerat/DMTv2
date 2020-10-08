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
using System.Collections;
using System.Linq;

#endregion

namespace DMT.TA.Windows.Coupon
{
    /// <summary>
    /// Interaction logic for CouponEditWindow.xaml
    /// </summary>
    public partial class CouponEditWindow : Window
    {
        #region Constructor

        public CouponEditWindow()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private TSBCouponManager manager = new TSBCouponManager();

        #region Button Handlers

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            // Save
            manager.Save();
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnNext35_Click(object sender, RoutedEventArgs e)
        {
            var items = lvTSB35.SelectedItems;
            if (null == items || items.Count <= 0) return;
            foreach (TSBCouponTransaction item in items)
            {
                if (null == item) return;
                if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
                manager.Borrow(item);
            }
            RefreshBHT35Coupons();
        }

        private void btnBack35_Click(object sender, RoutedEventArgs e)
        {
            var items = lvUser35.SelectedItems;
            if (null == items || items.Count <= 0) return;
            foreach (TSBCouponTransaction item in items)
            {
                if (null == item) return;
                manager.Return(item);
            }
            RefreshBHT35Coupons();
        }

        private void btnNext80_Click(object sender, RoutedEventArgs e)
        {
            var items = lvTSB80.SelectedItems;
            if (null == items || items.Count <= 0) return;
            foreach (TSBCouponTransaction item in items)
            {
                if (null == item) return;
                if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
                manager.Borrow(item);
            }
            RefreshBHT80Coupons();
        }

        private void btnBack80_Click(object sender, RoutedEventArgs e)
        {
            var items = lvUser80.SelectedItems;
            if (null == items || items.Count <= 0) return;
            foreach (TSBCouponTransaction item in items)
            {
                if (null == item) return;
                manager.Return(item);
            }
            RefreshBHT80Coupons();
        }

        #endregion

        #region Filter Handler

        private void txtFilter35_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            RefreshBHT35Coupons();
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                var list = lvTSB35.ItemsSource as IList;
                if (null != list && list.Count == 1)
                {
                    var item = list[0] as TSBCouponTransaction;
                    if (null == item) return;
                    if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
                    //item.TransactionType = TSBCouponTransaction.TransactionTypes.Lane;
                    manager.Borrow(item);
                    txtFilter35.Text = string.Empty;
                    RefreshBHT35Coupons();
                }
            }
        }

        private void txtFilter80_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            RefreshBHT80Coupons();
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                var list = lvTSB80.ItemsSource as IList;
                if (null != list && list.Count == 1)
                {
                    var item = list[0] as TSBCouponTransaction;
                    if (null == item) return;
                    if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
                    //item.TransactionType = TSBCouponTransaction.TransactionTypes.Lane;
                    manager.Borrow(item);
                    txtFilter80.Text = string.Empty;
                    RefreshBHT80Coupons();
                }
            }
        }

        #endregion

        public void Setup(User user)
        {
            manager.User = user;
            if (null != manager.User)
            {
                txtUser.Content = string.Format("พนง: {0} {1}",
                    manager.User.UserId, manager.User.FullNameTH);
            }
            else
            {
                txtUser.Content = string.Empty;
            }
            LoadCoupons();
        }

        private void LoadCoupons()
        {
            manager.Refresh();

            RefreshBHT35Coupons();
            RefreshBHT80Coupons();
        }

        private void RefreshBHT35Coupons()
        {
            lvTSB35.ItemsSource = null;
            lvTSB35.ItemsSource = manager.C35Stocks.FindAll(item =>
            {
                return item.CouponId.Contains(txtFilter35.Text) && item.TransactionType == TSBCouponTransaction.TransactionTypes.Stock;
            });

            lvUser35.ItemsSource = null;
            lvUser35.ItemsSource = manager.C35Users;
        }

        private void RefreshBHT80Coupons()
        {
            lvTSB80.ItemsSource = null;
            lvTSB80.ItemsSource = manager.C80Stocks.FindAll(item =>
            {
                return item.CouponId.Contains(txtFilter80.Text) && item.TransactionType == TSBCouponTransaction.TransactionTypes.Stock;
            });
            lvUser80.ItemsSource = null;
            lvUser80.ItemsSource = manager.C80Users;
        }
    }
}
