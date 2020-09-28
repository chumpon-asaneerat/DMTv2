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

#endregion

namespace DMT.TA.Pages.Exchange
{
    /// <summary>
    /// Interaction logic for PlazaRequestExchangePage.xaml
    /// </summary>
    public partial class PlazaRequestExchangePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaRequestExchangePage()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            grid.PlazaBalanceUpdated += Grid_PlazaBalanceUpdated;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            grid.PlazaBalanceUpdated += Grid_PlazaBalanceUpdated;
        }

        #endregion
        
        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private TSB _tsb = null;
        private TSBExchangeManager manager = new TSBExchangeManager();

        #region Grid Update Handler

        private void Grid_PlazaBalanceUpdated(object sender, EventArgs e)
        {
            RefreshPlazaInfo();
        }

        #endregion

        #region Button Handlers

        private void cmdRequest_Click(object sender, RoutedEventArgs e)
        {
            var win = new Windows.Exchange.PlazaCreditRequestExchangeWindow();
            win.Title = "คำร้องขอการแลกเปลี่ยนเงิน";
            win.Owner = Application.Current.MainWindow;
            var exchange = manager.NewRequest();
            win.Setup(Windows.Exchange.ExchangeWindowMode.New, exchange);
            if (win.ShowDialog() == false)
            {
                return;
            }

            if (win.Mode == Windows.Exchange.ExchangeWindowMode.Cancel)
            {
                manager.CancelRequest(exchange);
            }
            else
            {
                manager.SaveRequest(exchange);
            }
            // Request list.
            grid.RefreshList();
            RefreshPlazaInfo();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        public void Setup()
        {
            _tsb = ops.TSB.GetCurrent().Value();
            // Set TSB and Supervisor.
            manager.TSB = _tsb;
            manager.Supervisor = DMT.Controls.TAApp.User.Current;

            grid.Setup(manager);

            RefreshPlazaInfo();
        }

        private void RefreshPlazaInfo()
        {
            var tsbCredit = ops.Credits.GetTSBBalance(_tsb).Value();

            this.DataContext = tsbCredit;

            tsbCredit.Description = "ยอดที่สามารถยืมได้";
            tsbCredit.HasRemark = false;
            plaza.IsEnabled = false;
            plaza.DataContext = tsbCredit;

            loanEntry.IsEnabled = false;
            loanEntry.DataContext = tsbCredit;
        }
    }
}
