﻿#region Using

using DMT.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace DMT.TA.Windows.Exchange
{
    /// <summary>
    /// Interaction logic for PlazaCreditRequestExchangeWindow.xaml
    /// </summary>
    public partial class PlazaCreditRequestExchangeWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaCreditRequestExchangeWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cmdCancelRequest_Click(object sender, RoutedEventArgs e)
        {
            // change mode to cancel.
            Mode = ExchangeWindowMode.Cancel;
            this.DialogResult = true;
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region Public Methods

        public void Setup(ExchangeWindowMode mode, TSBExchangeGroup group)
        {
            this.Mode = mode;
            if (null != group)
            {
                requestEntry.DataContext = group.Request;
                requestDetailEntry.DataContext = group.Request;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets current transaction editing mode.
        /// </summary>
        public ExchangeWindowMode Mode { get; private set; }

        #endregion
    }
}
