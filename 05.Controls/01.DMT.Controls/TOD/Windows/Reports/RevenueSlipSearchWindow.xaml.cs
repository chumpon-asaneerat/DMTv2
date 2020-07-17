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

namespace DMT.TOD.Windows.Reports
{
    /// <summary>
    /// Interaction logic for RevenueSlipSearchWindow.xaml
    /// </summary>
    public partial class RevenueSlipSearchWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueSlipSearchWindow()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private User _user = null;

        private bool loaded = false;

        #region Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion

        #region Buton Handlers

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #region Date Change Handler

        private void dtDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSource();
        }

        #endregion

        private void UpdateSource()
        {
            if (!loaded) return;
            if (!dtDate.SelectedDate.HasValue) return;
            var items = ops.Revenue.GetRevenues(dtDate.SelectedDate.Value);
            grid.Setup(items);
        }

        public void Setup(User user)
        {
            _user = user;
            dtDate.SelectedDate = DateTime.Today;
            if (null != _user)
            {
                loaded = true;
                UpdateSource();
            }
        }

        public Models.RevenueEntry SelectedEntry
        {
            get
            {
                return grid.SelectedEntry;
            }
        }
    }
}
