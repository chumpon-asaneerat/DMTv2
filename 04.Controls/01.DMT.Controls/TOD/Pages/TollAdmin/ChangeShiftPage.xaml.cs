#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

using DMT.Models;
using DMT.Services;

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

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private User _user = null;

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (cbShifts.SelectedIndex == -1)
            {
                cbShifts.Focus();
                return;
            }
            Shift shift = cbShifts.SelectedItem as Shift;
            if (null != shift)
            {
                ops.Shifts.ChangeShift(shift);
            }
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        public void Setup(User user)
        {
            _user = user;
            if (null != _user)
            {
                DateTime dt = DateTime.Now;
                cbShifts.ItemsSource = ops.Shifts.GetShifts();
                // setup lane view.
                //grid.setup()
            }
        }
    }
}
