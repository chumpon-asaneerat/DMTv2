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
    /// Interaction logic for RevenueSlipPreview.xaml
    /// </summary>
    public partial class RevenueSlipPreview : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueSlipPreview()
        {
            InitializeComponent();
        }

        #endregion

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Report Page
            var page = (null != this.MenuPage) ? this.MenuPage : new Menu.ReportMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Report Page
            var page = (null != this.MenuPage) ? this.MenuPage : new Menu.ReportMenu();
            PageContentManager.Instance.Current = page;
        }

        public ContentControl MenuPage { get; set; }
    }
}
