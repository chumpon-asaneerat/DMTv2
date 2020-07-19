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

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        #region Button Handlers

        private void cmdRequest_Click(object sender, RoutedEventArgs e)
        {
            var win = new DMT.TA.Windows.Exchange.PlazaCreditRequestExchangeWindow();
            win.Owner = Application.Current.MainWindow;

            if (win.ShowDialog() == false)
            {
                return;
            }
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
        }
    }
}
