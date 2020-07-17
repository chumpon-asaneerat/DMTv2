#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;
using System.Runtime.Remoting;

#endregion

namespace DMT.TOD.Windows.Reports
{
    /// <summary>
    /// Interaction logic for RevenueSummarySearchWindow.xaml
    /// </summary>
    public partial class RevenueSummarySearchWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueSummarySearchWindow()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private User _user = null;
        private List<Models.RevenueEntry> _revenues = null;
        private bool loaded = false;

        #region Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion

        #region Button Handlers

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
            _revenues = ops.Revenue.GetRevenues(dtDate.SelectedDate.Value);
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

        public List<Models.RevenueEntry> Revenues
        {
            get { return _revenues; }
        }
    }
}
