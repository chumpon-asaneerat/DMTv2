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

namespace DMT.TA.Pages.Collector
{
    /// <summary>
    /// Interaction logic for CollectorFundViewPage.xaml
    /// </summary>
    public partial class CollectorFundViewPage : UserControl
    {
        #region Constructor

        public CollectorFundViewPage()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        #region Button Handlers

        private void addCollector_Click(object sender, RoutedEventArgs e)
        {
            //var win = new DMT.TA.Windows.Plaza.PlazaReceivedCreditWindow();
            var win = new DMT.TA.Windows.Collector.Credit.CollectorCreditBorrowWindow();
            win.Owner = Application.Current.MainWindow;
            win.Setup(null);
            if (win.ShowDialog() == false)
            {
                return;
            }
            RefreshPlazaInfo();
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
            var tsbCredit = ops.Credits.GetCurrent();
            this.DataContext = tsbCredit;

            tsbCredit.Description = "ยอดที่สามารถยืมได้";
            tsbCredit.HasRemark = false;
            plaza.IsEnabled = false;
            plaza.DataContext = tsbCredit;

            loanEntry.IsEnabled = false;
            loanEntry.DataContext = tsbCredit;

            grid.Setup();
        }

    }
}
