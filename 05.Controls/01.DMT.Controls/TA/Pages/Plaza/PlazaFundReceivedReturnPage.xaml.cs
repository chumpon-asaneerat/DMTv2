using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.TA.Pages.Plaza
{
    /// <summary>
    /// Interaction logic for PlazaFundReceivedReturnPage.xaml
    /// </summary>
    public partial class PlazaFundReceivedReturnPage : UserControl
    {
        public PlazaFundReceivedReturnPage()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdAppend_Click(object sender, RoutedEventArgs e)
        {
            var win = new DMT.TA.Windows.Collector.Credit.CollectorCreditBorrowWindow();
            win.Owner = Application.Current.MainWindow;
            if (win.ShowDialog() == false)
            {
                return;
            }

            
        }

    }
}
