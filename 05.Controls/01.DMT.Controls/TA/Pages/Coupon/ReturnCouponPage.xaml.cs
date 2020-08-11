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
using System.Windows.Threading;

#endregion

namespace DMT.TA.Pages.Coupon
{
    /// <summary>
    /// Interaction logic for ReturnCouponPage.xaml
    /// </summary>
    public partial class ReturnCouponPage : UserControl
    {
        #region Constructor

        public ReturnCouponPage()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        public void RefreshList()
        {
            grid.RefreshList();
        }
    }
}
