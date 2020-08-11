﻿#region Using

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
        private TSB _tsb = null;

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

        public void Setup(TSBExchangeTransaction transaction)
        {
            /*
            //srcEntry.DataContext = item.Plaza;
            requestEntry.DataContext = item.Request;
            approveEntry.DataContext = item.Approve;
            exchangeEntry.DataContext = item.Exchange;
            */
        }

        #endregion
    }
}
