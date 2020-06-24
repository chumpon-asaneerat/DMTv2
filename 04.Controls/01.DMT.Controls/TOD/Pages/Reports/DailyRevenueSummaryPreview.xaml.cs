#region

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

#endregion

namespace DMT.TOD.Pages.Reports
{
    /// <summary>
    /// Interaction logic for DailyRevenueSummaryPreview.xaml
    /// </summary>
    public partial class DailyRevenueSummaryPreview : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DailyRevenueSummaryPreview()
        {
            InitializeComponent();
        }

        #endregion

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Report Page
            var page = new Menu.ReportMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Report Page
            var page = new Menu.ReportMenu();
            PageContentManager.Instance.Current = page;
        }
    }
}
