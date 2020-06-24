#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

#endregion

namespace DMT.TOD.Pages.TollAdmin
{
    /// <summary>
    /// Interaction logic for ChangeShiftPage.xaml
    /// </summary>
    public partial class ChangeShiftPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ChangeShiftPage()
        {
            InitializeComponent();
        }

        #endregion

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }
        /*
        private Models.Job _job;

        public void Setup(Models.Job job) 
        {
            _job = job;
            grid.Setup(_job.Lanes);
        }
        */
    }
}
