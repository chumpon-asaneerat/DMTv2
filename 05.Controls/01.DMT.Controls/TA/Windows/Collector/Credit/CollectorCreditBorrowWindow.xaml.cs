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

#endregion

namespace DMT.TA.Windows.Collector.Credit
{
    /// <summary>
    /// Interaction logic for CollectorCreditBorrowWindow.xaml
    /// </summary>
    public partial class CollectorCreditBorrowWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorCreditBorrowWindow()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        
        private UserCredit srcObj;
        private UserCreditTransaction usrObj;
        private TSBCreditBalance plazaObj;
        private TSBCreditBalance sumObj;

        #region Button Handlers

        private void cmdSearchUser_Click(object sender, RoutedEventArgs e)
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

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (HasNegative())
            {
                MessageBox.Show(Application.Current.MainWindow, 
                    "ไม่สามารถดำเนินการบันทึกข้อมูลได้ เนื่องจากระบบพบว่ามีการ ยอดการยืมเงินในบางรายการ เกินจำนวนที่่ด่านมีอยู่", 
                    "Toll Admin");
                return;
            }
            if (null != srcObj && srcObj.UserCreditId == 0)
            {
                var plazaGrp = cbPlzaGroups.SelectedItem as PlazaGroup;
                if (null != plazaGrp)
                {
                    srcObj.TSBId = plazaGrp.TSBId;
                    srcObj.PlazaGroupId = plazaGrp.PlazaGroupId;
                }
                srcObj.State = UserCredit.StateTypes.Initial;
                ops.Credits.SaveUserCredit(srcObj);
                // Gets Id after save.
                var search = Search.UserCredits.GetActiveById.Create(
                    srcObj.UserId, srcObj.PlazaGroupId);
                srcObj = ops.Credits.GetActiveUserCreditById(search);
            }
            if (null != usrObj)
            {
                usrObj.UserCreditId = srcObj.UserCreditId;
                usrObj.TransactionType = UserCreditTransaction.TransactionTypes.Borrow;
                ops.Credits.SaveUserTransaction(usrObj);
            }
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        private void LoadMasters()
        {
            var tsb = ops.TSB.GetCurrent();
            if (null != tsb)
            {
                var plazaGroups = ops.TSB.GetTSBPlazaGroups(tsb);
                cbPlzaGroups.DisplayMemberPath = "PlazaGroupNameTH";
                cbPlzaGroups.ItemsSource = plazaGroups;
                if (null != plazaGroups && plazaGroups.Count > 0)
                {
                    cbPlzaGroups.SelectedIndex = 0;
                }
            }
        }

        private bool HasNegative()
        {
            return (
                sumObj.AmountST25 < 0 ||
                sumObj.AmountST50 < 0 ||
                sumObj.AmountBHT1 < 0 ||
                sumObj.AmountBHT2 < 0 ||
                sumObj.AmountBHT5 < 0 ||
                sumObj.AmountBHT10 < 0 ||
                sumObj.AmountBHT20 < 0 ||
                sumObj.AmountBHT50 < 0 ||
                sumObj.AmountBHT100 < 0 ||
                sumObj.AmountBHT500 < 0 ||
                sumObj.AmountBHT1000 < 0
                );
        }

        private void Calc()
        {
            sumObj.AmountST25 = plazaObj.AmountST25 - usrObj.AmountST25;
            sumObj.AmountST50 = plazaObj.AmountST50 - usrObj.AmountST50;
            sumObj.AmountBHT1 = plazaObj.AmountBHT1 - usrObj.AmountBHT1;
            sumObj.AmountBHT2 = plazaObj.AmountBHT2 - usrObj.AmountBHT2;
            sumObj.AmountBHT5 = plazaObj.AmountBHT5 - usrObj.AmountBHT5;
            sumObj.AmountBHT10 = plazaObj.AmountBHT10 - usrObj.AmountBHT10;
            sumObj.AmountBHT20 = plazaObj.AmountBHT20 - usrObj.AmountBHT20;
            sumObj.AmountBHT50 = plazaObj.AmountBHT50 - usrObj.AmountBHT50;
            sumObj.AmountBHT100 = plazaObj.AmountBHT100 - usrObj.AmountBHT100;
            sumObj.AmountBHT500 = plazaObj.AmountBHT500 - usrObj.AmountBHT500;
            sumObj.AmountBHT1000 = plazaObj.AmountBHT1000 - usrObj.AmountBHT1000;
        }

        public void Setup(UserCredit credit)
        {
            srcObj = credit;

            usrObj = new UserCreditTransaction();
            plazaObj = ops.Credits.GetCurrent();
            sumObj = new TSBCreditBalance();
            plazaObj.AssignTo(sumObj);

            if (null == srcObj)
            {
                srcObj = new UserCredit();
            }
            else
            {
                if (srcObj.UserCreditId == 0)
                {
                    panelSearch.Visibility = Visibility.Visible;
                }
                else panelSearch.Visibility = Visibility.Collapsed;
            }
            LoadMasters();

            this.DataContext = srcObj;

            srcObj.Description = "ยอดยืมปัจจุบัน";
            srcObj.HasRemark = false;
            usrObj.Description = "ยืมเงิน";
            usrObj.PropertyChanged += UsrObj_PropertyChanged;
            sumObj.Description = "ยอดด่านคงเหลือ";
            sumObj.HasRemark = false;

            //srcEntry.DataContext = srcObj;
            usrEntry.DataContext = usrObj;
            sumEntry.DataContext = sumObj;
        }

        private void UsrObj_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Calc();
        }
    }
}
