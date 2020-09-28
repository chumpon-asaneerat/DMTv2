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

#endregion

namespace DMT.TA.Pages.Plaza
{
    /// <summary>
    /// Interaction logic for PlazaInternalCreditExchangePage.xaml
    /// </summary>
    public partial class PlazaInternalCreditExchangePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaInternalCreditExchangePage()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private TSB _tsb = null;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

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

        private void cmdAppend_Click(object sender, RoutedEventArgs e)
        {
            var win = new Windows.Plaza.PlazaInternalCreditExchangeWindow();
            win.Owner = Application.Current.MainWindow;
            win.Title = "แลกเงินหมุนเวียนภายในด่าน";
            win.Setup();
            if (win.ShowDialog() == false)
            {
                return;
            }
            RefreshList();
            RefreshPlazaInfo();
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        #endregion

        public void Setup()
        {
            dtDate.SelectedDate = DateTime.Today;

            RefreshList();
            RefreshPlazaInfo();
        }

        private void RefreshPlazaInfo()
        {
            this.DataContext = null;
            _tsb = ops.TSB.GetCurrent().Value();
            var tsbCredit = ops.Credits.GetTSBBalance(_tsb).Value();
            if (null != tsbCredit)
            {
                this.DataContext = tsbCredit;
                tsbCredit.Description = "เงินยืมทอน";
                tsbCredit.HasRemark = false;
                plaza.IsEnabled = false;
                plaza.DataContext = tsbCredit;

                loanEntry.IsEnabled = false;
                loanEntry.DataContext = tsbCredit;
            }
        }

        private void RefreshList()
        {
            this.grid.Setup(null);
            var items = ops.Credits.GetReplaceTSBCreditTransaction(dtDate.SelectedDate.Value).Value();
            this.grid.Setup(items);
        }
    }
}
