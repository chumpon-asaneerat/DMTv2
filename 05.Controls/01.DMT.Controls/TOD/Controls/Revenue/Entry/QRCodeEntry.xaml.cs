#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;

using DMT.Controls;

#endregion

namespace DMT.TOD.Controls.Revenue.Entry
{
    /// <summary>
    /// Interaction logic for QRCodeEntry.xaml
    /// </summary>
    public partial class QRCodeEntry : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public QRCodeEntry()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private SCWOperations server = SCWServiceOperations.Instance.Plaza;
        private TSB _tsb = null;
        private RevenueEntryManager _manager;
        private Models.RevenueEntry entry;
        private List<SCWQRCode> items = new List<SCWQRCode>();
        private int rowCnt = 0;
        private decimal amtVal = 0;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (null != entry)
            {
                if (entry.IsHistorical)
                {
                    LoadItems();
                }
                else
                {
                    LoadItems();
                }
            }
            else
            {
                UpdateSummary();
            }
        }

        public void Setup(RevenueEntryManager manager)
        {
            _manager = manager;
            entry = (null != _manager) ? _manager.RevenueEntry : null;
            this.DataContext = entry;
        }

        public void LoadItems()
        {
            if (null == entry)
            {
                return;
            }

            this.listView.ItemsSource = null;
            if (null == _tsb) _tsb = ops.TSB.GetCurrent().Value();
            var sortList = RevenueEntryManager.GetQRCodeList(_tsb, entry);
            if (null != sortList && sortList.Count > 0) 
            {
                rowCnt = sortList.Count;
                amtVal = decimal.Zero;
                sortList.ForEach(item =>
                {
                    amtVal += (item.amount.HasValue) ? item.amount.Value : decimal.Zero;
                });
            }
            else
            {
                rowCnt = 0;
                amtVal = decimal.Zero;
            }
            this.listView.ItemsSource = sortList;
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            txtQty.Text = rowCnt.ToString("n0");
            txtTotal.Text = amtVal.ToString("n0");
        }
    }
}
