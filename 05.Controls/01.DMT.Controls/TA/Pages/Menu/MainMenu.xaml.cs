
#region Using

using System.Windows;
using System.Windows.Controls;

using NLib.Services;

using System;
using System.Collections.Generic;

using NLib;

#endregion

namespace DMT.TA.Pages.Menu
{
    /// <summary>
    /// Interaction logic for TODMainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
        }

        #endregion

        #region Button (Menu) Command Handlers

        // Implement #1 ยืม/แลก เงินยืมทอบ ฝ่ายบัญชี
        private void exchangeBankNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Exchange Fund Page.
                var page = new Pages.Exchange.PlazaRequestExchangePage();
                page.RefreshPlazaInfo();
                PageContentManager.Instance.Current = page;
            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }
        }
        // OK - หัวหน่าขายคูปอง
        private void plazaReceivedCoupon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var page = new Pages.Coupon.CouponReturnPage();
                page.Setup(DMT.Controls.TAApp.User.Current);
                PageContentManager.Instance.Current = page;
            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }
        }
        // Implement #1 คืนเงินยืมทอนฝ่ายบัญชี
        private void refundBankNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Plaza Fund Received
                var page = new DMT.TA.Pages.Plaza.PlazaReceivedReturnPage();
                page.RefreshPlazaInfo();
                PageContentManager.Instance.Current = page;
            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }
        }
        // NO Implementation ประวัติการขายคูปอง
        private void plazaAllCoupon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Plaza Fund Received
                var page = new DMT.TA.Pages.Plaza.PlazaAllCouponPage();
                PageContentManager.Instance.Current = page;
            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }
        }
        // Implement #1 แลกเงินหมุนเวียนในด่าน
        private void exchangeMoney_Click(object sender, RoutedEventArgs e)
        {
            var page = new DMT.TA.Pages.Plaza.PlazaInternalCreditExchangePage();
            page.RefreshPlazaInfo();
            PageContentManager.Instance.Current = page;
        }
        // Implement #1 - no report (currently used image) รายงานสรุปการยืมเงินทอน
        private void collectorFundReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var page = new Reports.CollectorFundSummaryReportPage();
                PageContentManager.Instance.Current = page;
            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }

        }
        // OK - เงินยืมทอน (collector)
        private void collectorFund_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var page = new Collector.CollectorFundViewPage();
                page.RefreshPlazaInfo();
                PageContentManager.Instance.Current = page;
            }
            catch (Exception)
            {
                //Console.WriteLine("Refresh data error.");
            }
        }
        // OK - รับคูปอง (collector) (ขาดเรื่อง แสดงรายการที่ขายใน lane)
        private void collectorReveivedCoupon_Click(object sender, RoutedEventArgs e)
        {
            var page = new Coupon.ReceivedCouponPage();
            PageContentManager.Instance.Current = page;
        }
        // OK - คืนคูปอง (collector)
        private void collectorReturnCoupon_Click(object sender, RoutedEventArgs e)
        {
            var page = new Coupon.ReturnCouponPage();
            PageContentManager.Instance.Current = page;
        }
        // NO Implementation ประวัติการแลกเงินยืมทอน (collector)
        private void changeBorrowingHistory_Click(object sender, RoutedEventArgs e)
        {
            var page = new History.PlazaReturnHistoryPage();
            PageContentManager.Instance.Current = page;
        }
        // Implement #1 - เช็คยอดด่าน (ขาดเรื่อง ยืมเพิ่ม กับการตรวจ max balance)
        private void plazaAllStock_Click(object sender, RoutedEventArgs e)
        {
            var win = new Windows.Plaza.PlazaStockSummaryWindow();
            win.Owner = Application.Current.MainWindow;
            win.RefreshPlazaInfo();
            if (win.ShowDialog() == false)
            {
                return;
            }
        }
        // OK. ออกจากระบบ
        private void logout_Click(object sender, RoutedEventArgs e)
        {
            // When enter Sign In Screen reset current user.
            DMT.Controls.TAApp.User.Current = null;

            var page = new DMT.Pages.SignInPage();
            page.Setup("CTC", "ACCOUNT", "ADMINS");
            PageContentManager.Instance.Current = page;
        }

        #endregion
    }
}
