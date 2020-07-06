﻿#region Using

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

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private UserShift _userShift = null;
        private Plaza _plaza = null;
        private DateTime _entryDate = DateTime.MinValue;
        private DateTime _revDate = DateTime.MinValue;
        private Models.RevenueEntry _revenueEntry = null;

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Slip Preview
            var page = new Reports.RevenueSlipPreview();
            page.MenuPage = new Menu.MainMenu(); // Set MenPage to main menu.
            page.CallerPage = this; // Set CallerPage for click back.
            page.Setup(_userShift, _plaza, _entryDate, _revDate, _revenueEntry);
            PageContentManager.Instance.Current = page;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        public void Setup(UserShift userShift, Plaza plaza,
            DateTime entryDate, DateTime revDate)
        {
            _userShift = userShift;
            _plaza = plaza;

            if (null == _userShift || null == plaza)
            {
                _entryDate = DateTime.MinValue;
                _revDate = DateTime.MinValue;

                txtRevDate.Text = string.Empty;
                txtPlazaName.Text = string.Empty;

                txtShiftName.Text = string.Empty;

                txtUserId.Text = string.Empty;
                txtUserName.Text = string.Empty;

                revEntry.DataContext = null;
            }
            else
            {
                _entryDate = entryDate;
                _revDate = revDate;

                txtRevDate.Text = _revDate.ToThaiDateTimeString("dd/MM/yyyy");
                txtPlazaName.Text = _plaza.PlazaNameTH;

                txtShiftName.Text = _userShift.ShiftNameTH;
                
                txtUserId.Text = _userShift.UserId;
                txtUserName.Text = _userShift.FullNameTH;

                _revenueEntry = new Models.RevenueEntry();
                // assigned plaza.
                _revenueEntry.PlazaId = _plaza.PlazaId;

                revEntry.DataContext = _revenueEntry;
            }
        }
    }
}
