﻿#region Using

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

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private TSB _tsb = null;
        private TSBExchangeManager manager = new TSBExchangeManager();

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

            if (win.Mode == Windows.Exchange.ExchangeWindowMode.New)
            {
                manager.Save(exchange);
            }
            else if (win.Mode == Windows.Exchange.ExchangeWindowMode.Edit)
            {
                if (exchange.PkId == 0) return; // not exists in database so do nothing.
                manager.Save(exchange);
            }
            else if (win.Mode == Windows.Exchange.ExchangeWindowMode.Cancel)
            {
                if (exchange.PkId == 0) return; // not exists in database so do nothing.
                manager.Cancel(exchange);
                manager.Save(exchange);
            }
            // Request list.
            grid.RefreshList();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        public void RefreshPlazaInfo()
        {
            _tsb = ops.TSB.GetCurrent().Value();
            var tsbCredit = ops.Credits.GetTSBBalance(_tsb).Value();
            // Set TSB and Supervisor.
            manager.TSB = _tsb;
            manager.Supervisor = DMT.Controls.TAApp.User.Current;

            this.DataContext = tsbCredit;

            tsbCredit.Description = "ยอดที่สามารถยืมได้";
            tsbCredit.HasRemark = false;
            plaza.IsEnabled = false;
            plaza.DataContext = tsbCredit;

            loanEntry.IsEnabled = false;
            loanEntry.DataContext = tsbCredit;

            grid.Setup(manager);
        }
    }
}
