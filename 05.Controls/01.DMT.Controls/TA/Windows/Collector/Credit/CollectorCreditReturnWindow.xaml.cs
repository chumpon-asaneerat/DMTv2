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
    /// Interaction logic for CollectorCreditReturnWindow.xaml
    /// </summary>
    public partial class CollectorCreditReturnWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorCreditReturnWindow()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private UserCredit srcObj;
        private UserCreditTransaction usrObj;

        #region Button Handlers

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (HasNegative())
            {
                MessageBox.Show(Application.Current.MainWindow,
                    "ไม่สามารถดำเนินการบันทึกข้อมูลได้ เนื่องจากระบบพบว่ามีการ คืนเงิน เกินจำนวนที่่ได้ยืมไป",
                    "Toll Admin");
                return;
            }
            if (null != srcObj && null != usrObj)
            {
                usrObj.UserCreditId = srcObj.UserCreditId;
                usrObj.TransactionType = UserCreditTransaction.TransactionTypes.Return;
                ops.Credits.SaveUserTransaction(usrObj);
                // Check is total borrow is reach zero.
                var search = Search.UserCredits.GetActiveById.Create(
                    srcObj.UserId, srcObj.PlazaGroupId);
                var inst = ops.Credits.GetActiveUserCreditById(search);
                if (null != inst)
                {
                    if (inst.BHTTotal <= decimal.Zero)
                    {
                        // change source state.
                        srcObj.State = UserCredit.StateTypes.Completed;
                        ops.Credits.SaveUserCredit(srcObj);
                    }
                }
            }
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        private bool HasNegative()
        {
            return (usrObj.BHTTotal > srcObj.BHTTotal);
        }

        public void Setup(UserCredit credit)
        {
            srcObj = credit;

            usrObj = new UserCreditTransaction();

            this.DataContext = srcObj;

            if (null != srcObj)
            {
                srcObj.Description = srcObj.FullNameTH;
                srcObj.HasRemark = false;
                usrObj.Description = "คืนเงิน";
            }

            srcEntry.DataContext = srcObj;
            usrEntry.DataContext = usrObj;
        }
    }
}
