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
    /// Interaction logic for CouponReturnView.xaml
    /// </summary>
    public partial class CouponReturnView : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CouponReturnView()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        private void cmdReturn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button);
            var trans = (null != button) ? button.DataContext as TSBCouponSummary : null;
            if (null == trans) return;

            var search = Search.Users.ById.Create(trans.UserId, 
                "ADMINS",
                "ACCOUNT",
                "CTC_MGR", "CTC", "TC",
                "MT_ADMIN", "MT_TECH",
                "FINANCE", "SV",
                "RAD_MGR", "RAD_SUP");
            var user = ops.Users.GetById(search).Value();
            if (null == user) return;

            var win = new DMT.TA.Windows.Coupon.CouponReturnWindow();
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
            var tsb = ops.TSB.GetCurrent().Value();
            var ret = ops.Coupons.GetTSBCouponSummaries(tsb);
            var coupons = ret.Value();
            listView.ItemsSource = coupons;
        }
    }
}
