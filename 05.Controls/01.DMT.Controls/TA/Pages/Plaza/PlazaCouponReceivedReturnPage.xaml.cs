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


namespace DMT.TA.Pages.Plaza
{
    /// <summary>
    /// Interaction logic for PlazaCouponReceivedReturnPage.xaml
    /// </summary>
    public partial class PlazaCouponReceivedReturnPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaCouponReceivedReturnPage()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        #region Button Handlers

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        public void RefreshPlazaInfo()
        {
            var tsbCoupon = ops.Coupons.GetTSBBalance(null);

            this.DataContext = tsbCoupon;
            tsbCoupon.Description = "คุปอง";
            tsbCoupon.HasRemark = false;
            plaza.IsEnabled = false;
            plaza.DataContext = tsbCoupon;
        }
    }
}
