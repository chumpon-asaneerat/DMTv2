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

        #region Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtDate.SelectedDate = DateTime.Today;
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
            _revenues = ops.Revenue.GetRevenues(dtDate.SelectedDate.Value);
        }

        #endregion

        public void Setup(User user)
        {
            _user = user;
            if (null != _user)
            {
            }
        }

        public List<Models.RevenueEntry> Revenues
        {
            get { return _revenues; }
        }
    }
}
