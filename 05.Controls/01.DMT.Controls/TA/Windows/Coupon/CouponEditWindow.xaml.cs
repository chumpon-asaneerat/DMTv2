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

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private User _user = null;
        private List<TSBCouponTransaction> _tsbCoupon35 = null;
        private List<TSBCouponTransaction> _tsbCoupon80 = null;

        #region Button Handlers

        private void cmdSaveExchange_Click(object sender, RoutedEventArgs e)
        {
            // Save
            if (null != _user && null != _tsbCoupon35)
            {
                var list = _tsbCoupon35.FindAll(item =>
                {
                    return item.TransactionType == TSBCouponTransaction.TransactionTypes.User;
                }).ToList();
                var opts = Search.UserCoupons.BorrowCoupons.Create(_user, list);
                ops.Coupons.UserBorrowCoupons(opts);
            }
            if (null != _user && null != _tsbCoupon80)
            {
                var list = _tsbCoupon80.FindAll(item =>
                {
                    return item.TransactionType == TSBCouponTransaction.TransactionTypes.User;
                }).ToList();
                var opts = Search.UserCoupons.BorrowCoupons.Create(_user, list);
                ops.Coupons.UserBorrowCoupons(opts);
            }
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
            item.TransactionType = TSBCouponTransaction.TransactionTypes.User;
            RefreshBHT35Coupons();
        }

        private void btnBack35_Click(object sender, RoutedEventArgs e)
        {
            var item = lvUser35.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            item.TransactionType = TSBCouponTransaction.TransactionTypes.Received;
            RefreshBHT35Coupons();
        }

        private void btnNext80_Click(object sender, RoutedEventArgs e)
        {
            var item = lvTSB80.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            item.TransactionType = TSBCouponTransaction.TransactionTypes.User;
            RefreshBHT80Coupons();
        }

        private void btnBack80_Click(object sender, RoutedEventArgs e)
        {
            var item = lvUser80.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            item.TransactionType = TSBCouponTransaction.TransactionTypes.Received;
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
                    item.TransactionType = TSBCouponTransaction.TransactionTypes.User;
                    RefreshBHT35Coupons();
                    txtFilter35.Text = string.Empty;
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
                    item.TransactionType = TSBCouponTransaction.TransactionTypes.User;
                    RefreshBHT80Coupons();
                    txtFilter80.Text = string.Empty;
                }
            }
        }

        #endregion

        public void Setup(User user)
        {
            _user = user;
            if (null != _user)
            {
                txtUser.Content = string.Format("พนง: {0} {1}", 
                    _user.UserId, _user.FullNameTH);
            }
            else
            {
                txtUser.Content = string.Empty;
            }
            LoadCoupons();
        }

        private void LoadCoupons()
        {
            var tsb = ops.TSB.GetCurrent();
            _tsbCoupon35 = null;
            _tsbCoupon80 = null;
            lvUser35.ItemsSource = null;
            lvUser80.ItemsSource = null;

            if (null != tsb)
            {
                _tsbCoupon35 = ops.Coupons.GetTSBBHT35Coupons(tsb);
                _tsbCoupon80 = ops.Coupons.GetTSBBHT80Coupons(tsb);
            }
            if (null != _user && null != tsb)
            {
                var opts = Search.UserCoupons.ByUser.Create(tsb, _user);
                var user35Items = ops.Coupons.GetUserBHT35Coupons(opts);
                if (null != user35Items)
                {
                    var opts2 = Search.UserCoupons.ToTSBCoupons.Create(tsb, user35Items);
                    var tsb35Items = ops.Coupons.ToTSBBHT35Coupons(opts2);
                    if (null != tsb35Items)
                    {
                        _tsbCoupon35.AddRange(tsb35Items);
                    }
                }
                var user80Items = ops.Coupons.GetUserBHT80Coupons(opts);
                if (null != user80Items)
                {
                    var opts2 = Search.UserCoupons.ToTSBCoupons.Create(tsb, user80Items);
                    var tsb35Items = ops.Coupons.ToTSBBHT80Coupons(opts2);
                    if (null != tsb35Items)
                    {
                        _tsbCoupon80.AddRange(tsb35Items);
                    }
                }
            }
            lvTSB35.ItemsSource = _tsbCoupon35;
            lvTSB80.ItemsSource = _tsbCoupon80;

            RefreshBHT35Coupons();
            RefreshBHT80Coupons();
        }

        private void RefreshBHT35Coupons()
        {
            if (null != _tsbCoupon35)
            {
                lvTSB35.ItemsSource = _tsbCoupon35.FindAll(item =>
                {
                    return item.CouponId.Contains(txtFilter35.Text) && item.TransactionType == TSBCouponTransaction.TransactionTypes.Received;
                });
                lvUser35.ItemsSource = _tsbCoupon35.FindAll(item =>
                {
                    return item.TransactionType == TSBCouponTransaction.TransactionTypes.User;
                });
            }
        }

        private void RefreshBHT80Coupons()
        {
            if (null != _tsbCoupon80)
            {
                lvTSB80.ItemsSource = _tsbCoupon80.FindAll(item =>
                {
                    return item.CouponId.Contains(txtFilter80.Text) && item.TransactionType == TSBCouponTransaction.TransactionTypes.Received;
                });
                lvUser80.ItemsSource = _tsbCoupon80.FindAll(item =>
                {
                    return item.TransactionType == TSBCouponTransaction.TransactionTypes.User;
                });
            }
        }
    }
}
