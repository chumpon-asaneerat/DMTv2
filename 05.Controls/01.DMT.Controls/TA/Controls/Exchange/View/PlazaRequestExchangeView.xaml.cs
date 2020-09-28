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

namespace DMT.TA.Controls.Exchange.View
{
    /// <summary>
    /// Interaction logic for PlazaRequestExchangeView.xaml
    /// </summary>
    public partial class PlazaRequestExchangeView : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaRequestExchangeView()
        {
            InitializeComponent();
        }

        #endregion

        private bool _isEdit = false;
        private DateTime _lastupdated = DateTime.MinValue;
        private TSBExchangeManager manager = null;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.OnTick += new EventHandler(Instance_OnTick);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.OnTick -= new EventHandler(Instance_OnTick);
        }

        #endregion

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            var group = b.CommandParameter as TSBExchangeGroup;
            if (null != manager && null != group)
            {
                _isEdit = true;

                var win = new Windows.Exchange.PlazaCreditRequestExchangeWindow();
                win.Title = "คำร้องขอการแลกเปลี่ยนเงิน";
                win.Owner = Application.Current.MainWindow;
                win.Setup(Windows.Exchange.ExchangeWindowMode.Edit, group);

                if (win.ShowDialog() == false)
                {
                    _isEdit = false;
                    return;
                }

                _isEdit = false;

                if (win.Mode == Windows.Exchange.ExchangeWindowMode.Edit)
                {
                    if (group.State != TSBExchangeGroup.StateTypes.Request)
                        return; // invalid transaction type.
                    //TODO: Check save function.
                    //ops.Exchanges.SaveTSBExchangeTransaction(item);
                }
                else if (win.Mode == Windows.Exchange.ExchangeWindowMode.Cancel)
                {
                    if (group.State != TSBExchangeGroup.StateTypes.Request)
                        return; // invalid transaction type.

                    if (group.PkId == 0)
                        return; // data is not saved so ignore it.
                    group.State = TSBExchangeGroup.StateTypes.Canceled;
                    group.FinishFlag = TSBExchangeGroup.FinishedFlags.Completed;
                    //TODO: Check save function.
                    //ops.Exchanges.SaveTSBExchangeTransaction(item);
                }
                // Request list.
                RefreshList();
            }
        }

        private void cmdApprove_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            //TODO: Change to TSB Exchange Group.
            TSBExchangeTransaction item = b.CommandParameter as TSBExchangeTransaction;
            if (null != manager && null != item)
            {
                // direct approve.
            }
        }

        private void cmdExchange_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            //TODO: Change to TSB Exchange Group.
            TSBExchangeTransaction item = b.CommandParameter as TSBExchangeTransaction;
            if (null != manager && null != item)
            {
                _isEdit = true;

                var win = new DMT.TA.Windows.Exchange.PlazaCreditUpdateExchangeWindow();
                win.Owner = Application.Current.MainWindow;
                win.Title = "ยืนยันข้อมูลการแลกเปลี่ยนเงิน";
                win.Setup(item);
                if (win.ShowDialog() == false)
                {
                    _isEdit = false;
                    return;
                }

                _isEdit = false;
                // Request list.
                RefreshList();

                /*
                // replace descriptions
                item.Request.Description = "รายการขอแลกเงินจากด่าน";
                item.Request.HasRemark = true;

                item.Approve.Description = "รายการอนุมัติจากบัญชี";
                item.Approve.HasRemark = true;

                item.Exchange.Description = "จ่ายออก ธนบัตร/เหรียญ";
                */
            }
        }

        void Instance_OnTick(object sender, EventArgs e)
        {
            var ts = DateTime.Now - _lastupdated;
            if (ts.TotalSeconds >= 10)
            {
                if (_isEdit) return; // in editing so not refresh screen.
                RefreshList();
                _lastupdated = DateTime.Now;
            }
        }

        public void RefreshList()
        {
            listView.ItemsSource = null;
            if (null == manager) return;
            listView.ItemsSource = manager.GetRequestApproves();
        }

        public void Setup(TSBExchangeManager value)
        {
            manager = value;
            RefreshList();
        }
    }
}
