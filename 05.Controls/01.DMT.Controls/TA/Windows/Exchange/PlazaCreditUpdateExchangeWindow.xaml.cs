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

namespace DMT.TA.Windows.Exchange
{
    /// <summary>
    /// Interaction logic for PlazaCreditUpdateExchangeWindow.xaml
    /// </summary>
    public partial class PlazaCreditUpdateExchangeWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaCreditUpdateExchangeWindow()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private TSBExchangeManager manager = null;

        #region Button Handlers

        private void cmdSaveExchange_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region Public Methods

        public void Setup(TSBExchangeManager value, TSBExchangeGroup group)
        {
            manager = value;
            if (null == manager || null == group) return;

            // request from plaza.
            if (null == group.Request) 
                manager.LoadRequest(group);
            if (null == group.Request) 
                return; // load failed
            group.Request.Description = "รายการขอแลกเงินจากด่าน";
            requestEntry.DataContext = group.Request;
            
            // approve from account.
            if (null == group.Approve) 
                manager.LoadApprove(group);
            if (null == group.Approve) 
                return; // load failed
                group.Approve.Description = "รายการอนุมัติจากบัญชี";
            approveEntry.DataContext = group.Approve;

            // check received/exchange transaction.
            if (null == group.Received || null == group.Exchange) 
                manager.PrepareReceived(group);
            if (null == group.Received || null == group.Exchange) 
                return; // load failed
            // reveived from account->plaza
            group.Received.Description = "เงินที่ได้รับจริง";
            trueReciveEntry.DataContext = group.Received;
            // exchange from plaza->account
            group.Exchange.Description = "จ่ายออก ธนบัตร/เหรียญ";
            exchangeEntry.DataContext = group.Exchange;
        }

        #endregion
    }
}
