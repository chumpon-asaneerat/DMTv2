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

namespace DMT.TA.Pages.Plaza
{
    /// <summary>
    /// Interaction logic for PlazaAllCouponPage.xaml
    /// </summary>
    public partial class PlazaAllCouponPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaAllCouponPage()
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
        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
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
            if (null == _user)
            {
                return;
            }
        }
    }
}
