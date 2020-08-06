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

        }
    }
}
