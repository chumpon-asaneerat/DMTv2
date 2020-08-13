﻿#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;

#endregion

namespace DMT.Windows
{
    /// <summary>
    /// Interaction logic for MessageBoxYesNoWindow.xaml
    /// </summary>
    public partial class MessageBoxYesNoWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MessageBoxYesNoWindow()
        {
            InitializeComponent();
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

        public void Setup(string msg, string head)
        {
            this.Title = head;
            txtMsg.Text = msg;
        }
    }
}
