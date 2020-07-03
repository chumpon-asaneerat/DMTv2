using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.TA.Pages.Coupon
{
    /// <summary>
    /// Interaction logic for ReceivedCouponPage.xaml
    /// </summary>
    public partial class ReceivedCouponPage : UserControl
    {
        public ReceivedCouponPage()
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


        private void cmdAppend_Click(object sender, RoutedEventArgs e)
        {
            var win = new DMT.TA.Windows.Coupon.CouponEditWindow();

            if (win.ShowDialog() == false)
            {
                return;
            }

        }
    }
}
