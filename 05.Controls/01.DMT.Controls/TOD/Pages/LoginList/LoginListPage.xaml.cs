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
            Refresh();
        }

        #endregion

        private void Refresh()
        {
            var tsb = ops.TSB.GetCurrent().Value();
            if (null == tsb) return;
            var plazaGroups = ops.TSB.GetTSBPlazaGroups(tsb).Value();
            if (null != plazaGroups)
            {
                plazaGroups.ForEach(plazaGroup =>
                {
                    // set required data
                    _manager.User = _user;
                    _manager.PlazaGroup = plazaGroup;
                    // reload jobs.
                    _manager.SyncJobList();
                });
            }
            grid.RefreshUsers();
        }

        public void Setup(User user)
        {
            _user = user;
            grid.Setup(_user);
            Refresh();
        }
    }
}
