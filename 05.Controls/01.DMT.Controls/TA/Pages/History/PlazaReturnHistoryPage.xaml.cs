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

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private UserCredit srcObj;
        private UserCreditTransaction usrObj;

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
            var tsbCredit = ops.Credits.GetCurrent();

            this.DataContext = tsbCredit;
            tsbCredit.Description = "ยืมเงิน";
            tsbCredit.HasRemark = false;
            plaza.IsEnabled = false;
            plaza.DataContext = tsbCredit;

        }

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            string userId = txtSearchUserId.Text;
            if (string.IsNullOrEmpty(userId)) return;

            var users = ops.Users.SearchById(Search.Users.ById.Create(userId));
            if (null != users && null != srcObj)
            {
                if (users.Count == 1)
                {
                    var user = users[0];
                    srcObj.UserId = user.UserId;
                    srcObj.FullNameEN = user.FullNameEN;
                    srcObj.FullNameTH = user.FullNameTH;
                }
                else if (users.Count > 1)
                {
                    var win = new TA.Windows.Collector.Searchs.CollectorFilterWindow();
                    win.Owner = Application.Current.MainWindow;
                    win.Setup(users);
                    if (win.ShowDialog() == false || null == win.SelectedUser)
                    {
                        // No user selected.
                        return;
                    }
                    var user = win.SelectedUser;
                    srcObj.UserId = user.UserId;
                    srcObj.FullNameEN = user.FullNameEN;
                    srcObj.FullNameTH = user.FullNameTH;
                }
            }
        }

        private void cmdCancelRe_Click(object sender, RoutedEventArgs e)
        {
            if (null != srcObj && null != usrObj)
            {
                DMT.Windows.MessageBoxYesNoRed2Window msg = new DMT.Windows.MessageBoxYesNoRed2Window();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("ยืนยันการ ยกเลิกรายการยืม/คืน เงินทอน", srcObj.FullNameTH, " จำนวนเงิน ", srcObj.BHTTotal.ToString("#,##0"), " บาท", "Toll Admin");
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
    }
}
