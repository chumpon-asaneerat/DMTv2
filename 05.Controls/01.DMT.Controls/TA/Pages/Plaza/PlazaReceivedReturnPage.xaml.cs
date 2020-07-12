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
    /// Interaction logic for PlazaReceivedReturnPage.xaml
    /// </summary>
    public partial class PlazaReceivedReturnPage : UserControl
    {
        public PlazaReceivedReturnPage()
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
            //var win = new Windows.TA.Plaza.FundBorrowReturnWindow();
            //win.Owner = Application.Current.MainWindow;

           
            //win.Title = "คืนเงินทอน";
            //win.Setup(srcObj, rcvObj, retObj, resObj);
            //if (win.ShowDialog() == false)
            //{
            //    return;
            //}
        }

        private void UpdateBalance()
        {
           
        }

      
        public void Setup()
        {
            UpdateBalance();
        }

        private void _plaza_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateBalance();
        }
    }
}
