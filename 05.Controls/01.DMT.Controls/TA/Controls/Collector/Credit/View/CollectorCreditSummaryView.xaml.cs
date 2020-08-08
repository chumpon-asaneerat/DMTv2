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
using NLib;

#endregion

namespace DMT.TA.Controls.Collector.Credit.View
{
    /// <summary>
    /// Interaction logic for CollectorCreditSummaryView.xaml
    /// </summary>
    public partial class CollectorCreditSummaryView : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorCreditSummaryView()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        private void cmdReceived_Click(object sender, RoutedEventArgs e)
        {
            var userCredit = (Models.UserCredit)((FrameworkElement)sender).DataContext;
            if (null == userCredit) return;
            var win = new Windows.Collector.Credit.CollectorCreditBorrowWindow();
            win.Owner = Application.Current.MainWindow;
            win.Title = userCredit.Description;

            win.Setup(userCredit);

            if (win.ShowDialog() == false)
            {
                return;
            }

            if (null != ItemChanged) ItemChanged.Call(this, System.EventArgs.Empty);
            RefreshUserCredits();
        }

        private void cmdReturn_Click(object sender, RoutedEventArgs e)
        {
            var userCredit = (Models.UserCredit)((FrameworkElement)sender).DataContext;
            if (null == userCredit) return;
            var win = new Windows.Collector.Credit.CollectorCreditReturnWindow();
            win.Owner = Application.Current.MainWindow;
            win.Title = userCredit.Description;

            win.Setup(userCredit);

            if (win.ShowDialog() == false)
            {
                return;
            }

            if (null != ItemChanged) ItemChanged.Call(this, System.EventArgs.Empty);
            RefreshUserCredits();
        }

        private void cmdReceivedBag_Click(object sender, RoutedEventArgs e)
        {
            var userCredit = (Models.UserCredit)((FrameworkElement)sender).DataContext;
            if (null == userCredit) return;

            var win = new Windows.Collector.Credit.ReceiveMoneyBagWindow();
            win.Owner = Application.Current.MainWindow;
            win.Title = userCredit.Description;

            win.Setup(userCredit.UserId, userCredit.BagNo, userCredit.BHTTotal);

            if (win.ShowDialog() == false)
            {
                return;
            }
            else
            {
                //TODO: Fixed Credit (Save User Credit).
                /*
                userCredit.State = UserCredit.StateTypes.Received;
                ops.Credits.SaveUserCredit(userCredit);
                */
            }
        }

        #endregion

        private void RefreshUserCredits()
        {
            var tsb = ops.TSB.GetCurrent();
            //TODO: Fixed Credit (Active User Credits).
            /*
            var userCredits = ops.Credits.GetActiveUserCredits(tsb);
            listView.ItemsSource = userCredits;
            */
        }

        public void Setup()
        {
            RefreshUserCredits();
        }

        public event System.EventHandler ItemChanged;
    }
}
