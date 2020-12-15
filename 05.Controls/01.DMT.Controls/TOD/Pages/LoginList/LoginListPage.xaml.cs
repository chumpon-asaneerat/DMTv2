#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;

#endregion

namespace DMT.TOD.Pages.Job
{
    /// <summary>
    /// Interaction logic for LoginListPage.xaml
    /// </summary>
    public partial class LoginListPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoginListPage()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private RevenueEntryManager _manager = new RevenueEntryManager();
        private User _user = null;

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Required to reload jobs
            /*
            // get selected plaza group
            _manager.PlazaGroup = cbPlazas.SelectedItem as PlazaGroup;
            // reload jobs.
            _manager.RefreshJobs();
            */
            grid.RefreshUsers();
        }

        #endregion

        public void Setup(User user)
        {
            _user = user;
            grid.Setup(_user);
            grid.RefreshUsers();
        }
    }
}
