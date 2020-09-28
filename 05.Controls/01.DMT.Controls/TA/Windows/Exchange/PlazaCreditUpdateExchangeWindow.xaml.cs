#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;
using NLib.Reports.Rdlc;
using System.Reflection;

#endregion

namespace DMT.TA.Windows.Exchange
{
    /// <summary>
    /// Interaction logic for PlazaCreditUpdateExchangeWindow.xaml
    /// </summary>
    public partial class PlazaCreditUpdateExchangeWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaCreditUpdateExchangeWindow()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        #region Button Handlers

        private void cmdSaveExchange_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region Public Methods

        public void Setup(TSBExchangeGroup value)
        {
            /*
            //srcEntry.DataContext = value.TSB;
            requestEntry.DataContext = value.Request;
            approveEntry.DataContext = value.Approve;
            exchangeEntry.DataContext = value.Exchange;
            */
        }

        #endregion
    }
}
