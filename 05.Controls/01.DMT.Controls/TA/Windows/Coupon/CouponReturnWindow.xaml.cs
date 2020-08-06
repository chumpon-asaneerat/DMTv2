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
            Verified(); // set finish flag if sold and return to stock if unsold.
            manager.Save();
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnNext35_Click(object sender, RoutedEventArgs e)
        {
            var item = lvUserSold35.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            if (item.TransactionType != TSBCouponTransaction.TransactionTypes.SoldByLane) return;
            manager.UnsoldByLane(item);
            RefreshBHT35Coupons();
        }

        private void btnBack35_Click(object sender, RoutedEventArgs e)
        {
            var item = lvUserOnHand35.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Lane) return;
            manager.SoldByLane(item);
            RefreshBHT35Coupons();
        }

        private void btnNext80_Click(object sender, RoutedEventArgs e)
        {
            var item = lvUserSold80.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            if (item.TransactionType != TSBCouponTransaction.TransactionTypes.SoldByLane) return;
            manager.UnsoldByLane(item);
            RefreshBHT80Coupons();
        }

        private void btnBack80_Click(object sender, RoutedEventArgs e)
        {
            var item = lvUserOnHand80.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Lane) return;
            manager.SoldByLane(item);
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

        private void Verified()
        {
            // Mark Finish Flag.
            var userSold35coupons = lvUserSold35.ItemsSource as List<TSBCouponTransaction>;
            MarkCompleted(userSold35coupons);
            var userSold80coupons = lvUserSold80.ItemsSource as List<TSBCouponTransaction>;
            MarkCompleted(userSold80coupons);
            // Returns to stock.
            var userUnSold35coupons = lvUserOnHand35.ItemsSource as List<TSBCouponTransaction>;
            ReturnToStock(userUnSold35coupons);
            var userUnSold80coupons = lvUserOnHand80.ItemsSource as List<TSBCouponTransaction>;
            ReturnToStock(userUnSold80coupons);
        }

        private void MarkCompleted(List<TSBCouponTransaction> items)
        {
            if (null == items) return;
            items.ForEach(item => 
            {
                if (item.TransactionType != TSBCouponTransaction.TransactionTypes.SoldByLane)
                    return;
                item.FinishFlag = 0;
            });
        }
        private void ReturnToStock(List<TSBCouponTransaction> items)
        {
            if (null == items) return;
            items.ForEach(item =>
            {
                if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Lane)
                    return;
                item.TransactionType = TSBCouponTransaction.TransactionTypes.Stock;
                item.UserId = string.Empty;
                item.UserReceiveDate = DateTime.MinValue;
            });
        }

        private void RefreshBHT35Coupons()
        {
            lvUserSold35.ItemsSource = null;
            lvUserSold35.ItemsSource = manager.C35UserSolds.FindAll(item =>
            {
                return item.TransactionType == TSBCouponTransaction.TransactionTypes.SoldByLane;
            });

            lvUserOnHand35.ItemsSource = null;
            lvUserOnHand35.ItemsSource = manager.C35UserOnHands;
        }

        private void RefreshBHT80Coupons()
        {
            lvUserSold80.ItemsSource = null;
            lvUserSold80.ItemsSource = manager.C80UserSolds.FindAll(item =>
            {
                return item.TransactionType == TSBCouponTransaction.TransactionTypes.SoldByLane;
            });

            lvUserOnHand80.ItemsSource = null;
            lvUserOnHand80.ItemsSource = manager.C80UserOnHands;
        }
    }
}
