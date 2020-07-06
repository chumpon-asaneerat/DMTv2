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

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private UserShift _userShift = null;
        private Plaza _plaza = null;
        private DateTime _entryDate = DateTime.MinValue;
        private DateTime _revDate = DateTime.MinValue;
        private Models.RevenueEntry _revenueEntry = null;

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Report Page
            var page = (null != this.CallerPage) ? this.CallerPage : new Menu.ReportMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Report Page
            var page = (null != this.MenuPage) ? this.MenuPage : new Menu.ReportMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        public ContentControl MenuPage { get; set; }
        public ContentControl CallerPage { get; set; }

        public void Setup(UserShift userShift, Plaza plaza,
            DateTime entryDate, DateTime revDate, 
            Models.RevenueEntry revenueEntry)
        {
            _userShift = userShift;
            _plaza = plaza;
            _entryDate = entryDate;
            _revDate = revDate;
            _revenueEntry = revenueEntry;
            if (null == _userShift || null == _plaza || null == _revenueEntry)
            {
                // No data.
            }
            else
            {

            }
        }
    }
}
