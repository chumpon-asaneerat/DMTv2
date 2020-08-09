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
        private UserCreditReturnManager manager = new UserCreditReturnManager();

        #region Button Handlers

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (manager.HasNegative())
            {
                MessageBox.Show(Application.Current.MainWindow,
                    "ไม่สามารถดำเนินการบันทึกข้อมูลได้ เนื่องจากระบบพบว่ามีการ คืนเงิน เกินจำนวนที่่ได้ยืมไป",
                    "Toll Admin");
                return;
            }
            if (null != manager.UserBalance && null != manager.Transaction)
            {
                DMT.Windows.MessageBoxYesNoRedWindow msg = new DMT.Windows.MessageBoxYesNoRedWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("ยืนยันการคืนเงิน ยืมทอน",
                    manager.UserBalance.FullNameTH + " จำนวนเงิน " + manager.Transaction.BHTTotal.ToString("#,##0") + " บาท", 
                    "Toll Admin", true);
                if (msg.ShowDialog() == true)
                {
                    if (manager.Save())
                    {
                        this.DialogResult = true;
                    }
                }
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        public void Setup(TSB tsb, UserCreditBalance credit)
        {
            manager.Setup(tsb, credit);
            this.DataContext = manager.UserBalance;

            if (null != manager.UserBalance)
            {
                manager.UserBalance.Description = manager.UserBalance.FullNameTH;
                manager.UserBalance.HasRemark = false;
                manager.Transaction.Description = "คืนเงิน";
            }

            srcEntry.DataContext = manager.UserBalance;
            usrEntry.DataContext = manager.Transaction;
        }
    }
}
