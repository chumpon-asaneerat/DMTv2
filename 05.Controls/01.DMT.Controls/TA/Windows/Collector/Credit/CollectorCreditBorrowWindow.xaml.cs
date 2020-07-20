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
            if (string.IsNullOrEmpty(userId) || userId.Length < 5) return;
            var user = ops.Users.GetById(Search.Users.ById.Create(userId));
            if (null != user && null != srcObj)
            {
                srcObj.UserId = user.UserId;
                srcObj.FullNameEN = user.FullNameEN;
                srcObj.FullNameTH = user.FullNameTH;
            }
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
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

        private void Calc()
        {
            sumObj.BHT1 = plazaObj.BHT1 - usrObj.BHT1;
            sumObj.BHT2 = plazaObj.BHT2 - usrObj.BHT2;
            sumObj.BHT5 = plazaObj.BHT5 - usrObj.BHT5;
            sumObj.BHT10 = plazaObj.BHT10 - usrObj.BHT10;
            sumObj.BHT20 = plazaObj.BHT20 - usrObj.BHT20;
            sumObj.BHT50 = plazaObj.BHT50 - usrObj.BHT50;
            sumObj.BHT100 = plazaObj.BHT100 - usrObj.BHT100;
            sumObj.BHT500 = plazaObj.BHT500 - usrObj.BHT500;
            sumObj.BHT1000 = plazaObj.BHT1000 - usrObj.BHT1000;
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

            srcEntry.DataContext = srcObj;
            usrEntry.DataContext = usrObj;
            sumEntry.DataContext = sumObj;
        }

        private void UsrObj_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Calc();
        }
    }
}
