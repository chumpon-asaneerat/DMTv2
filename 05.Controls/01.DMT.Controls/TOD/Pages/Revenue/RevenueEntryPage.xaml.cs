#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

#endregion

namespace DMT.TOD.Pages.Revenue
{
    /// <summary>
    /// Interaction logic for RevenueEntryPage.xaml
    /// </summary>
    public partial class RevenueEntryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueEntryPage()
        {
            InitializeComponent();
        }

        #endregion

        /*
        private Models.Job _job;
        private Models.RevenueEntry _entry;

        public void Setup(Models.Job job, Models.RevenueEntry entry)
        {
            _job = job;
            _entry = entry;
            revEntry.Setup(_job, _entry);
        }
        */

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Slip Preview
            var page = new Reports.RevenueSlipPreview();
            // back to main menu.
            page.MenuPage = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }
    }
}
