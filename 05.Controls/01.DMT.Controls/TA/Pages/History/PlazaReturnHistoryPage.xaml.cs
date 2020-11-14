#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using DMT.Controls;

using NLib.Services;
using NLib.Reflection;
using NLib.Reports.Rdlc;
using System.Reflection;
using System.ComponentModel;

#endregion

namespace DMT.TA.Pages.History
{
    /// <summary>
    /// Interaction logic for PlazaReturnHistoryPage.xaml
    /// </summary>
    public partial class PlazaReturnHistoryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaReturnHistoryPage()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private User _user = null;

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdAppend_Click(object sender, RoutedEventArgs e)
        {
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        public void RefreshPlazaInfo()
        {
            var tsb = ops.TSB.GetCurrent().Value();
            var tsbCredit = ops.Credits.GetTSBBalance(tsb).Value();

            this.DataContext = tsbCredit;
            tsbCredit.Description = "ยืมเงิน";
            tsbCredit.HasRemark = false;
            plaza.IsEnabled = false;
            plaza.DataContext = tsbCredit;
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void cmdCancelRe_Click(object sender, RoutedEventArgs e)
        {
            if (null != _user)
            {
                //string total = _user.BHTTotal.ToString("#,##0");
                //TODO: Plaza History implements not finished.
                string total = "1,000";

                DMT.Windows.MessageBoxYesNoRed2Window msg = new DMT.Windows.MessageBoxYesNoRed2Window();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("ยืนยันการ ยกเลิกรายการยืม/คืน เงินทอน", _user.FullNameTH, " จำนวนเงิน ", total, " บาท", "Toll Admin");
                if (msg.ShowDialog() == true)
                {

                }
            }
            else
            {
                DMT.Windows.MessageBoxYesNoRed2Window msg = new DMT.Windows.MessageBoxYesNoRed2Window();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("ยืนยันการ ยกเลิกรายการยืม/คืน เงินทอน", "ทดสอบ", " จำนวนเงิน ", "1,000", " บาท", "Toll Admin");
                if (msg.ShowDialog() == true)
                {

                }
            }
        }

        private void txtSearchUserId_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                Search();
            }
        }

        private void Search()
        {
            if (!string.IsNullOrEmpty(txtSearchUserId.Text))
            {
                string userId = txtSearchUserId.Text;
                if (string.IsNullOrEmpty(userId)) return;

                UserSearchManager.Instance.Title = "กรุณาเลือกพนักงานเก็บเงิน";
                _user = UserSearchManager.Instance.SelectUser(userId, 
                    "ADMINS",
                    "ACCOUNT",
                    "CTC_MGR", "CTC", "TC",
                    "MT_ADMIN", "MT_TECH",
                    "FINANCE", "SV",
                    "RAD_MGR", "RAD_SUP");
            }
        }
    }
}
