#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;

#endregion

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

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private User _user = null;

        #region Button Handlers

        private void revSlip_Click(object sender, RoutedEventArgs e)
        {
            var search = new DMT.TOD.Windows.Reports.RevenueSlipSearchWindow();
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
            var search = new DMT.TOD.Windows.Reports.RevenueSummarySearchWindow();
            search.Owner = Application.Current.MainWindow;
            if (search.ShowDialog() == false)
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

        #endregion

        public void Setup(User user)
        {
            _user = user;
            if (null != _user)
            {
            }
        }
    }
}
