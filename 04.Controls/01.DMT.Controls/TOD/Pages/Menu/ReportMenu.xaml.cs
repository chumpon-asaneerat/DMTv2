using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.TOD.Pages.Menu
{
    /// <summary>
    /// Interaction logic for ReportMenu.xaml
    /// </summary>
    public partial class ReportMenu : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReportMenu()
        {
            InitializeComponent();
        }

        #endregion

        private void revSlip_Click(object sender, RoutedEventArgs e)
        {
            
            var search = new DMT.Windows.SignInWindow();
            search.Owner = Application.Current.MainWindow;
            if (search.ShowDialog() == false)
            {
                return;
            }
            // Revenue Slip Preview
            var page = new Reports.RevenueSlipPreview();
            page.MenuPage = this;
            PageContentManager.Instance.Current = page;
        }

        private void revSummary_Click(object sender, RoutedEventArgs e)
        {

            var signinWin = new DMT.Windows.SignInWindow();
            signinWin.Owner = Application.Current.MainWindow;
            if (signinWin.ShowDialog() == false)
            {
                return;
            }

            // Daily Revenue Summary Preview
            var page = new Reports.DailyRevenueSummaryPreview();
            PageContentManager.Instance.Current = page;
            
        }

        private void backHome_Click(object sender, RoutedEventArgs e)
        {

            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
            
        }
    }
}
