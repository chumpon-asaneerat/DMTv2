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
    /// Interaction logic for CouponReturnPage.xaml
    /// </summary>
    public partial class CouponReturnPage : UserControl
    {
        #region Constructor

        public CouponReturnPage()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private TSBCouponManager manager = new TSBCouponManager();

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            DMT.Windows.MessageBoxYesNoRed3Window msg = new DMT.Windows.MessageBoxYesNoRed3Window();
            msg.Owner = Application.Current.MainWindow;
            msg.Setup("ยืนยันการขายคูปอง จำนวน ","5", " เล่ม"
                , "คูปอง 35 บาท = ", "2"," เล่ม"
                , "คูปอง 80 บาท = ", "3", " เล่ม"
                , "Toll Admin");
            if (msg.ShowDialog() == true)
            {
                // Main Menu Page
                var page = new Menu.MainMenu();
                PageContentManager.Instance.Current = page;
            }
            
        }

        private void btnNext35_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnBack35_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnNext80_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnBack80_Click(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion

        #region Filter Handlers

        private void txtFilter35_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void txtFilter80_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        #endregion

        public void Setup(User user)
        {
            manager.User = user;
            LoadCoupons();
        }

        private void LoadCoupons()
        {
            manager.Refresh();

            RefreshBHT35Coupons();
            RefreshBHT80Coupons();
        }

        private void RefreshBHT35Coupons()
        {
            lvTSB35.ItemsSource = null;
            lvTSB35.ItemsSource = manager.C35Stocks.FindAll(item =>
            {
                return item.CouponId.Contains(txtFilter35.Text) && item.TransactionType == TSBCouponTransaction.TransactionTypes.Stock;
            });

            lvSold35.ItemsSource = null;
            lvSold35.ItemsSource = manager.C35Users;
        }

        private void RefreshBHT80Coupons()
        {
            lvTSB80.ItemsSource = null;
            lvTSB80.ItemsSource = manager.C80Stocks.FindAll(item =>
            {
                return item.CouponId.Contains(txtFilter80.Text) && item.TransactionType == TSBCouponTransaction.TransactionTypes.Stock;
            });
            lvSold80.ItemsSource = null;
            lvSold80.ItemsSource = manager.C80Users;
        }
    }
}
