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

namespace DMT.TA.Controls.Collector.Coupon
{
    /// <summary>
    /// Interaction logic for CouponReceiveView.xaml
    /// </summary>
    public partial class CouponReceiveView : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CouponReceiveView()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button);
            var trans = (null != button) ? button.DataContext as TSBCouponSummary : null;
            if (null == trans) return;
            var search = Search.Users.ById.Create(trans.UserId);
            var user = ops.Users.GetById(search);
            if (null == user) return;

            var win = new DMT.TA.Windows.Coupon.CouponEditWindow();
            win.Owner = Application.Current.MainWindow;
            win.Setup(user);
            if (win.ShowDialog() == false)
            {
                return;
            }
            RefreshList();
        }

        public void RefreshList()
        {
            var coupons = ops.Coupons.GetTSBCouponSummaries(null);
            listView.ItemsSource = coupons;
        }
    }
}
