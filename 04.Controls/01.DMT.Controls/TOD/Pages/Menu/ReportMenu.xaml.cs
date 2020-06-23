using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.Pages.TOD
{
    /// <summary>
    /// Interaction logic for ReportMenu.xaml
    /// </summary>
    public partial class ReportMenu : UserControl
    {
        public ReportMenu()
        {
            InitializeComponent();
        }

        private void revSlip_Click(object sender, RoutedEventArgs e)
        {
            /*
            var search = new Windows.TOD.Reports.RevenueSlipSearchWindow();
            search.Owner = Application.Current.MainWindow;
            if (search.ShowDialog() == false)
            {
                return;
            }
            // Revenue Slip Preview
            var page = new Reports.RevenueSlipPreview();
            page.MenuPage = this;
            PageContentManager.Instance.Current = page;
            */
        }

        private void revSummary_Click(object sender, RoutedEventArgs e)
        {
            /*
            var search = new Windows.TOD.Reports.RevenueSummarySearchWindow();
            search.Owner = Application.Current.MainWindow;
            if (search.ShowDialog() == false)
            {
                return;
            }
            // Daily Revenue Summary Preview
            var page = new Reports.DailyRevenueSummaryPreview();
            PageContentManager.Instance.Current = page;
            */
        }

        private void backHome_Click(object sender, RoutedEventArgs e)
        {
            /*
            // Main Menu Page
            var page = new TOD.MainMenu();
            PageContentManager.Instance.Current = page;
            */
        }
    }
}
