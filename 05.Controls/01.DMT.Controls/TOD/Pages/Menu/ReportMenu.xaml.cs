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

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private User _user = null;

        #region Button Handlers

        private void revSlip_Click(object sender, RoutedEventArgs e)
        {
            var search = new DMT.TOD.Windows.Reports.RevenueSlipSearchWindow();
            search.Owner = Application.Current.MainWindow;
            search.Setup(_user);
            if (search.ShowDialog() == false)
            {
                return;
            }
            Models.RevenueEntry revenueEntry = search.SelectedEntry;

            if (null == revenueEntry)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("กรุณาเลือกรายการที่ต้องการเเรียกดูใบนำส่งรายได้", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    return;
                }

                //MessageBox.Show("กรุณาเลือกรายการที่ต้องการเเรียกดูใบนำส่งรายได้");
                //return;
            }
            // Revenue Slip Preview
            var page = new Reports.RevenueSlipPreview();
            page.MenuPage = this;
            page.CallerPage = this; // Set CallerPage for click back.
            page.Setup(_user, null, null, null, null,
                DateTime.MinValue, DateTime.MinValue, revenueEntry);
            PageContentManager.Instance.Current = page;
        }

        private void revSummary_Click(object sender, RoutedEventArgs e)
        {
            var search = new DMT.TOD.Windows.Reports.RevenueSummarySearchWindow();
            search.Owner = Application.Current.MainWindow;
            search.Setup(_user);
            if (search.ShowDialog() == false)
            {
                return;
            }
            var revenues = search.Revenues;

            // Daily Revenue Summary Preview
            var page = new Reports.DailyRevenueSummaryPreview();
            page.MenuPage = this;
            page.CallerPage = this; // Set CallerPage for click back.

            page.Setup(revenues);
            PageContentManager.Instance.Current = page;
        }

        private void repSlipNull_Click(object sender, RoutedEventArgs e)
        {
            Models.RevenueEntry revenueEntry = null;
            // Slip Preview
            var page = new Reports.RevenueSlipPreviewNull();
            page.MenuPage = new Menu.MainMenu(); // Set MenPage to main menu.
            page.CallerPage = this; // Set CallerPage for click back.
            page.Setup(_user, revenueEntry);
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
