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

namespace DMT.TOD.Pages.Revenue
{
    /// <summary>
    /// Interaction logic for RevenueEntryPage.xaml
    /// </summary>
    public partial class RevenueEntryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueEntryPage()
        {
            InitializeComponent();
        }

        #endregion

        private RevenueEntryManager _manager = null;

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            /*
            // Slip Preview
            var page = new Reports.RevenueSlipPreview();
            page.MenuPage = new Menu.MainMenu(); // Set MenPage to main menu.
            page.CallerPage = this; // Set CallerPage for click back.
            page.Setup(_manager);
            PageContentManager.Instance.Current = page;
            */
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        public void Setup(RevenueEntryManager manager)
        {
            /*
            _manager = manager;

            if (null == _manager || null == _manager.UserShift ||
                null == _manager.PlazaGroup || null == _manager.RevenueShift)
            {
                txtRevDate.Text = string.Empty;
                txtPlazaName.Text = string.Empty;

                txtShiftName.Text = string.Empty;

                txtUserId.Text = string.Empty;
                txtUserName.Text = string.Empty;

                revEntry.DataContext = null;
            }
            else 
            {
                _manager.NewRevenueEntry();

                txtRevDate.Text = _manager.RevenueDate.ToThaiDateTimeString("dd/MM/yyyy");
                txtPlazaName.Text = _manager.PlazaGroup.PlazaGroupNameTH;

                txtShiftName.Text = _manager.UserShift.ShiftNameTH;

                txtUserId.Text = _manager.UserShift.UserId;
                txtUserName.Text = _manager.UserShift.FullNameTH;

                revEntry.DataContext = _manager.RevenueEntry;
            }
            */
        }
    }
}
