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
using System.Collections;
using System.Linq;

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

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
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
            var sold35 = lvSold35.ItemsSource as IList;
            var cnt35 = (null != sold35) ? sold35.Count : 0;
            var sold80 = lvSold80.ItemsSource as IList;
            var cnt80 = (null != sold80) ? sold80.Count : 0;
            var cntTotal = cnt35 + cnt80;

            DMT.Windows.MessageBoxYesNoRed3Window msg = new DMT.Windows.MessageBoxYesNoRed3Window();
            msg.Owner = Application.Current.MainWindow;
            msg.Setup("ยืนยันการขายคูปอง จำนวน ", cntTotal.ToString("n0"), " เล่ม"
                , "คูปอง 35 บาท = ", cnt35.ToString("n0"), " เล่ม"
                , "คูปอง 80 บาท = ", cnt80.ToString("n0"), " เล่ม"
                , "Toll Admin");
            if (msg.ShowDialog() == true)
            {
                // Save
                Verified(); // set finish flag if sold and return to stock if unsold.
                manager.Save();


                // Main Menu Page
                //var page = new Menu.MainMenu();
                //PageContentManager.Instance.Current = page;

                var page = new Pages.Reports.CouponSalesReceiptReportPage();
                page.CallerPage = this;
                page.Setup(manager.User, null);
                PageContentManager.Instance.Current = page;

            }
            
        }

        private void btnNext35_Click(object sender, RoutedEventArgs e)
        {
            var item = lvTSB35.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
            manager.SoldByTSB(item);
            RefreshBHT35Coupons();
        }

        private void btnBack35_Click(object sender, RoutedEventArgs e)
        {
            var item = lvSold35.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            manager.UnsoldByTSB(item);
            RefreshBHT35Coupons();
        }

        private void btnNext80_Click(object sender, RoutedEventArgs e)
        {
            var item = lvTSB80.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
            manager.SoldByTSB(item);
            RefreshBHT80Coupons();
        }

        private void btnBack80_Click(object sender, RoutedEventArgs e)
        {
            var item = lvSold80.SelectedItem as TSBCouponTransaction;
            if (null == item) return;
            manager.UnsoldByTSB(item);
            RefreshBHT80Coupons();
        }

        #endregion

        #region Filter Handlers

        private void txtFilter35_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            RefreshBHT35Coupons();
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                var list = lvTSB35.ItemsSource as IList;
                if (null != list && list.Count == 1)
                {
                    var item = list[0] as TSBCouponTransaction;
                    if (null == item) return;
                    if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
                    item.TransactionType = TSBCouponTransaction.TransactionTypes.SoldByTSB;
                    txtFilter35.Text = string.Empty;
                    RefreshBHT35Coupons();
                }
            }
        }

        private void txtFilter80_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            RefreshBHT80Coupons();
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                var list = lvTSB80.ItemsSource as IList;
                if (null != list && list.Count == 1)
                {
                    var item = list[0] as TSBCouponTransaction;
                    if (null == item) return;
                    if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
                    item.TransactionType = TSBCouponTransaction.TransactionTypes.SoldByTSB;
                    txtFilter80.Text = string.Empty;
                    RefreshBHT80Coupons();
                }
            }
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
            lvSold35.ItemsSource = manager.C35TSBSolds;

            UpdateCounters();
        }

        private void RefreshBHT80Coupons()
        {
            lvTSB80.ItemsSource = null;
            lvTSB80.ItemsSource = manager.C80Stocks.FindAll(item =>
            {
                return item.CouponId.Contains(txtFilter80.Text) && item.TransactionType == TSBCouponTransaction.TransactionTypes.Stock;
            });
            lvSold80.ItemsSource = null;
            lvSold80.ItemsSource = manager.C80TSBSolds;

            UpdateCounters();
        }

        private void UpdateCounters()
        {
            var stock35 = lvTSB35.ItemsSource as IList;
            txtTSBCount35.Text = (null != stock35) ? stock35.Count.ToString("n0") : "0";

            var sold35 = lvSold35.ItemsSource as IList;
            txtSoldCount35.Text = (null != sold35) ? sold35.Count.ToString("n0") : "0";
            
            var stock80 = lvTSB80.ItemsSource as IList;
            txtTSBCount80.Text = (null != stock80) ? stock80.Count.ToString("n0") : "0";

            var sold80 = lvSold80.ItemsSource as IList;
            txtSoldCount80.Text = (null != sold80) ? sold80.Count.ToString("n0") : "0";
        }


        private void Verified()
        {
            // Mark Finish Flag.
            var Sold35coupons = lvSold35.ItemsSource as List<TSBCouponTransaction>;
            MarkCompleted(Sold35coupons);
            var Sold80coupons = lvSold80.ItemsSource as List<TSBCouponTransaction>;
            MarkCompleted(Sold80coupons);
            // Returns to stock.
            var Stock35coupons = lvTSB35.ItemsSource as List<TSBCouponTransaction>;
            ReturnToStock(Stock35coupons);
            var Stock80coupons = lvTSB80.ItemsSource as List<TSBCouponTransaction>;
            ReturnToStock(Stock80coupons);
        }

        private void MarkCompleted(List<TSBCouponTransaction> items)
        {
            if (null == items) return;
            items.ForEach(item =>
            {
                if (item.TransactionType != TSBCouponTransaction.TransactionTypes.SoldByTSB)
                    return;
                item.FinishFlag = 0;
            });
        }
        private void ReturnToStock(List<TSBCouponTransaction> items)
        {
            if (null == items) return;
            items.ForEach(item =>
            {
                if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Stock)
                    return;
                item.TransactionType = TSBCouponTransaction.TransactionTypes.Stock;
                item.SoldBy = string.Empty;
                item.SoldDate = DateTime.MinValue;
            });
        }
    }
}
