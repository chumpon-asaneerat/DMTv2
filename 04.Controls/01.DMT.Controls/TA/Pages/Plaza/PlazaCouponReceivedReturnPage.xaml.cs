﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.TA.Pages.Plaza
{
    /// <summary>
    /// Interaction logic for PlazaCouponReceivedReturnPage.xaml
    /// </summary>
    public partial class PlazaCouponReceivedReturnPage : UserControl
    {
        public PlazaCouponReceivedReturnPage()
        {
            InitializeComponent();
        }

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
        /*
        public void Setup(List<Models.Coupon> coupons)
        {
            var couponTypes = new List<string>();
            couponTypes.Add("คูปอง 35 บาท");
            couponTypes.Add("คูปอง 80 บาท");
            cbCouponTypes.DataContext = couponTypes;
            cbCouponTypes.SelectedIndex = 0;
            grid.Setup(coupons);
            var plazaCoupons = new Models.CouponEntry();
            plazaCoupons.Description = "สรุปยอดคูปอง";
            plazaCoupons.BHT35 = 200;
            plazaCoupons.BHT80 = 200;
            plaza.DataContext = plazaCoupons;
            plaza.IsEnabled = false;
        }
        */
    }
}
