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

#endregion

namespace DMT.TA.Windows.Plaza
{
    /// <summary>
    /// Interaction logic for PlazaStockSummaryWindow.xaml
    /// </summary>
    public partial class PlazaStockSummaryWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaStockSummaryWindow()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private TSB _tsb = null;

        #region Button Handlers

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        public void RefreshPlazaInfo()
        {
            var ret = ops.TSB.GetCurrent();
            _tsb = (null != ret && !ret.errors.hasError) ? ret.data : null;

            var tcrRet = ops.Credits.GetTSBBalance(_tsb);
            var tsbCredit = (null != tcrRet && !tcrRet.errors.hasError) ? tcrRet.data : null;

            if (null != tsbCredit)
            {
                tsbCredit.Description = "เงินยืมทอนหมุนเวียนด่าน";
                tsbCredit.HasRemark = false;
            }
            creditEntry.IsEnabled = false;
            creditEntry.DataContext = tsbCredit;

            var tcpRet = ops.Coupons.GetTSBBalance(_tsb);
            var tsbCoupon = (null != tcpRet && !tcpRet.errors.hasError) ? tcpRet.data : null;
            if (null != tsbCoupon)
            {
                tsbCoupon.Description = "คุปอง";
                tsbCoupon.HasRemark = false;
            }
            couponEntry.IsEnabled = false;
            couponEntry.DataContext = tsbCoupon;

            loanMoneyEntry.IsEnabled = false;
            loanMoneyEntry.DataContext = tsbCredit;

            if (tsbCredit != null)
                txtMsg.Text = tsbCredit.BHTTotal.ToString("#,##0");
            else
                txtMsg.Text = "0";
        }
    }
}
