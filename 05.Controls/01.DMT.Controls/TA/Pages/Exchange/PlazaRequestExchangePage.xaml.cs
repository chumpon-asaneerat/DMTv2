using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.TA.Pages.Exchange
{
    /// <summary>
    /// Interaction logic for PlazaRequestExchangePage.xaml
    /// </summary>
    public partial class PlazaRequestExchangePage : UserControl
    {
        public PlazaRequestExchangePage()
        {
            InitializeComponent();
        }

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

    }
}
