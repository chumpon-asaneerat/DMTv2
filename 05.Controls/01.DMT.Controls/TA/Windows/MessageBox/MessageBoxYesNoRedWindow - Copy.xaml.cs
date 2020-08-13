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
    /// Interaction logic for MessageBoxYesNoRedWindow.xaml
    /// </summary>
    public partial class MessageBoxYesNoRedWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MessageBoxYesNoRedWindow()
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

        public void Setup(string msg1, string msg2, string head,bool red)
        {
            this.Title = head;
            txtMsg1.Text = msg1;
            txtMsg2.Text = msg2;

            if (red == true)
            {
                txtMsg1.Foreground = new SolidColorBrush(Colors.Red);
                txtMsg2.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
