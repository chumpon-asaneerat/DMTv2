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
    /// Interaction logic for MessageBoxYesNoRed2Window.xaml
    /// </summary>
    public partial class MessageBoxYesNoRed2Window : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MessageBoxYesNoRed2Window()
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

        public void Setup(string msg1, string msg2, string msg3, string msg4, string msg5, string head)
        {
            this.Title = head;
            txtMsg1.Text = msg1;
            txtMsg2.Text = msg2;
            txtMsg3.Text = msg3;
            txtMsg4.Text = msg4;
            txtMsg5.Text = msg5;

            txtMsg1.Foreground = new SolidColorBrush(Colors.Black);
            txtMsg2.Foreground = new SolidColorBrush(Colors.Red);
            txtMsg3.Foreground = new SolidColorBrush(Colors.Black);
            txtMsg4.Foreground = new SolidColorBrush(Colors.Red);
            txtMsg5.Foreground = new SolidColorBrush(Colors.Black);

        }
    }
}
