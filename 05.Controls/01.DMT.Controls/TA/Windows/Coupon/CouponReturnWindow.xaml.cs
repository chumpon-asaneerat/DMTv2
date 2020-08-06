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

namespace DMT.TA.Windows.Coupon
{
    /// <summary>
    /// Interaction logic for CouponReturnWindow.xaml
    /// </summary>
    public partial class CouponReturnWindow : Window
    {
        #region Constructor

        public CouponReturnWindow()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
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
            var item = lvTSB35.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
            manager.Borrow(item);
            RefreshBHT35Coupons();
        }

        private void btnBack35_Click(object sender, RoutedEventArgs e)
        {
            var item = lvUser35.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            manager.Return(item);
            RefreshBHT35Coupons();
        }

        private void btnNext80_Click(object sender, RoutedEventArgs e)
        {
            var item = lvTSB80.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
            manager.Borrow(item);
            RefreshBHT80Coupons();
        }

        private void btnBack80_Click(object sender, RoutedEventArgs e)
        {
            var item = lvUser80.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            manager.Return(item);
            RefreshBHT80Coupons();
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

        }

        private void RefreshBHT80Coupons()
        {

        }
    }
}
