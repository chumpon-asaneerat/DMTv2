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
using System.ComponentModel;
using System.Windows.Interop;
using NLib;

#endregion

namespace DMT.TA.Windows.Collector.Searchs
{
    /// <summary>
    /// Interaction logic for CollectorFilterWindow.xaml
    /// </summary>
    public partial class CollectorFilterWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorFilterWindow()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private List<User> _users = null;

        #region Button Handlers

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        public void Setup(List<User> users)
        {
            lvUsers.ItemsSource = null;

            _users = users;

            lvUsers.ItemsSource = _users;
        }

        public User SelectedUser
        {
            get { return lvUsers.SelectedItem as User; }
        }
    }
}
