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
    /// Interaction logic for RevenueDateSelectionPage.xaml
    /// </summary>
    public partial class RevenueDateSelectionPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueDateSelectionPage()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private User _user = null;

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Revenue Entry Page
            var page = new RevenueEntryPage();
            PageContentManager.Instance.Current = page;
            //page.Setup(_job, _entry);
        }

        #endregion

        public void Setup(User user)
        {
            _user = user;
            if (null != _user)
            {
                /*
                DateTime dt = DateTime.Now;
                cbShift.ItemsSource = ops.Shifts.GetShifts();
                var tsb = ops.TSB.GetCurrent();
                txtPlaza.Text = tsb.TSBNameTH;
                txtDate.Text = dt.ToThaiDateString();
                txtTime.Text = dt.ToThaiTimeString();
                txtID.Text = _user.UserId;
                txtName.Text = _user.FullNameTH;
                */
            }
        }
    }
}
