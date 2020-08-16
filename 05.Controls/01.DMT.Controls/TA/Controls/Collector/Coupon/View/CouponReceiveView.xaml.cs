﻿#region Using

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
    public delegate void PrintCouponReceivedReportEventHandler(object sender, PrintCouponReceivedReportEventArgs e);
    
    public class PrintCouponReceivedReportEventArgs
    {
        public TSBCouponSummary Transaction { get; set; }
        public User User { get; set; }
    }

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

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button);
            var trans = (null != button) ? button.DataContext as TSBCouponSummary : null;
            if (null == trans) return;

            var search = Search.Users.ById.Create(trans.UserId, "TC");
            var user = ops.Users.GetById(search).Value();
            if (null == user) return;

            // Raise Event.
            OnPrint.Invoke(this, new PrintCouponReceivedReportEventArgs()
            {
                Transaction = trans,
                User = user
            });
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button);
            var trans = (null != button) ? button.DataContext as TSBCouponSummary : null;
            if (null == trans) return;

            var search = Search.Users.ById.Create(trans.UserId, "TC");
            var user = ops.Users.GetById(search).Value();
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
            var tsb = ops.TSB.GetCurrent().Value();
            var ret = ops.Coupons.GetTSBCouponSummaries(tsb);
            var coupons = ret.Value();
            listView.ItemsSource = coupons;
        }

        public event PrintCouponReceivedReportEventHandler OnPrint;
    }
}
