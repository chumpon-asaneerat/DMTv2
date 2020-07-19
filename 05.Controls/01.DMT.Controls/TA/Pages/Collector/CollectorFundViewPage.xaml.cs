using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.TA.Pages.Collector
{
    /// <summary>
    /// Interaction logic for CollectorFundViewPage.xaml
    /// </summary>
    public partial class CollectorFundViewPage : UserControl
    {
        public CollectorFundViewPage()
        {
            InitializeComponent();
        }


        private void addCollector_Click(object sender, RoutedEventArgs e)
        {
            var win = new DMT.TA.Windows.Plaza.PlazaReceivedCreditWindow();
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
