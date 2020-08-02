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
        private List<TSBCouponTransaction> _tsbCoupon35;
        private List<TSBCouponTransaction> _tsbCoupon80;

        #region Button Handlers

        private void cmdSaveExchange_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }


        private void btnNext35_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack35_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNext80_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack80_Click(object sender, RoutedEventArgs e)
        {

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
            RefreshCoupons();
        }

        private void RefreshCoupons()
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
            lvTSB35.ItemsSource = _tsbCoupon35;
            lvTSB80.ItemsSource = _tsbCoupon80;
        }
    }
}
