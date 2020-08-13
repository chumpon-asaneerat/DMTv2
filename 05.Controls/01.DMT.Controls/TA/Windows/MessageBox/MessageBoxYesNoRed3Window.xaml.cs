#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;
using System.Windows.Media;

#endregion

namespace DMT.Windows
{
    /// <summary>
    /// Interaction logic for MessageBoxYesNoRed3Window.xaml
    /// </summary>
    public partial class MessageBoxYesNoRed3Window : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MessageBoxYesNoRed3Window()
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

        public void Setup(string msg1, string msg2, string msg3, string msg4, string msg5
            , string msg6, string msg7, string msg8, string msg9, string head)
        {
            this.Title = head;
            txtMsg1.Text = msg1;
            txtMsg2.Text = msg2;
            txtMsg3.Text = msg3;
            txtMsg4.Text = msg4;
            txtMsg5.Text = msg5;

            txtMsg6.Text = msg6;
            txtMsg7.Text = msg7;
            txtMsg8.Text = msg8;
            txtMsg9.Text = msg9;
        }
    }
}
