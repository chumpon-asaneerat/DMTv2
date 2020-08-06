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

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        private void cmdReturn_Click(object sender, RoutedEventArgs e)
        {

        }

        public void RefreshList()
        {
            // need to change get coupon on lanes instead.
            var coupons = ops.Coupons.GetTSBCouponSummaries(null);
            listView.ItemsSource = coupons;
        }
    }
}
