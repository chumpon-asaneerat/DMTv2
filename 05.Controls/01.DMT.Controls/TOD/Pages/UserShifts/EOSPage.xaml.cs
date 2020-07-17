#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;
using DMT.TOD.Windows.Job;

#endregion

namespace DMT.TOD.Pages.UserShifts
{
    /// <summary>
    /// Interaction logic for EOSPage .xaml
    /// </summary>
    public partial class EOSPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EOSPage()
        {
            InitializeComponent();
        }

        #endregion

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            EOSWindow win = new EOSWindow();
            win.Owner = Application.Current.MainWindow;
            // setup
            //win.Job = _job;

            if (win.ShowDialog() == false)
            {
                return;
            }

            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }
    }
}
