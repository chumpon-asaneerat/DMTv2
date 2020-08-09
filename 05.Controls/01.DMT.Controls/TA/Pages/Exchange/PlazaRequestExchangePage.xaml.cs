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

#endregion

namespace DMT.TA.Pages.Exchange
{
    /// <summary>
    /// Interaction logic for PlazaRequestExchangePage.xaml
    /// </summary>
    public partial class PlazaRequestExchangePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaRequestExchangePage()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private TSB _tsb = null;

        #region Button Handlers

        private void cmdRequest_Click(object sender, RoutedEventArgs e)
        {
            TSBExchangeTransaction tran = new TSBExchangeTransaction();
            tran.TransactionType = TSBExchangeTransaction.TransactionTypes.Request;
            tran.TransactionDate = DateTime.Now;
            // Set TSB
            tran.TSBId = _tsb.TSBId;
            tran.TSBNameEN = _tsb.TSBNameEN;
            tran.TSBNameTH = _tsb.TSBNameTH;
            // Set user.
            var user = DMT.Controls.TAApp.User.Current;
            tran.UserId = user.UserId;
            tran.FullNameEN = user.FullNameEN;
            tran.FullNameTH = user.FullNameTH;

            var win = new Windows.Exchange.PlazaCreditRequestExchangeWindow();
            win.Title = "คำร้องขอการแลกเปลี่ยนเงิน";
            win.Owner = Application.Current.MainWindow;
            win.Setup(Windows.Exchange.ExchangeWindowMode.New,  tran);

            if (win.ShowDialog() == false)
            {
                return;
            }

            if (win.Mode == Windows.Exchange.ExchangeWindowMode.New)
            {
                if (tran.TransactionType != TSBExchangeTransaction.TransactionTypes.Request)
                    return; // invalid transaction type.
                // New required to generate group Guid.
                tran.GroupId = Guid.NewGuid();

                ops.Exchanges.SaveTSBExchangeTransaction(tran);
            }
            else if (win.Mode == Windows.Exchange.ExchangeWindowMode.Edit)
            {
                if (tran.TransactionType != TSBExchangeTransaction.TransactionTypes.Request)
                    return; // invalid transaction type.
                if (tran.TransactionId == 0)
                    return; // data is not saved so ignore it.

                ops.Exchanges.SaveTSBExchangeTransaction(tran);
            }
            else if (win.Mode == Windows.Exchange.ExchangeWindowMode.Cancel)
            {
                if (tran.TransactionType != TSBExchangeTransaction.TransactionTypes.Request)
                    return; // invalid transaction type.

                if (tran.TransactionId == 0)
                    return; // data is not saved so ignore it.
                tran.TransactionType = TSBExchangeTransaction.TransactionTypes.Canceled;
                tran.FinishFlag = TSBExchangeTransaction.FinishedFlags.Completed;
                ops.Exchanges.SaveTSBExchangeTransaction(tran);
            }

            // Request list.
            grid.RefreshList(_tsb);
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        public void RefreshPlazaInfo()
        {
            _tsb = ops.TSB.GetCurrent();

            var tsbCredit = ops.Credits.GetTSBBalance(_tsb);

            this.DataContext = tsbCredit;

            tsbCredit.Description = "ยอดที่สามารถยืมได้";
            tsbCredit.HasRemark = false;
            plaza.IsEnabled = false;
            plaza.DataContext = tsbCredit;

            loanEntry.IsEnabled = false;
            loanEntry.DataContext = tsbCredit;

            // Request list.
            grid.RefreshList(_tsb);
        }
    }
}
