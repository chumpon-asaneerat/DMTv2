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
        private Models.RevenueEntry entry;
        private List<SCWQRCode> items = new List<SCWQRCode>();
        private int rowCnt = 0;
        private decimal amtVal = 0;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO: Need user/password from config table or external file.
            SCWServiceOperations.Instance.UserName = "DMTUSER";
            SCWServiceOperations.Instance.Password = "DMTPASS";
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            entry = this.DataContext as Models.RevenueEntry;
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

        public void LoadItems()
        {
            if (null == entry)
            {
                return;
            }

            this.listView.ItemsSource = null;

            // TODO: network id required.
            int nwId = 31;
            DateTime dt1, dt2;
            if (entry.IsHistorical)
            {
                dt1 = entry.RevenueDate.Date;
                dt2 = dt1.AddDays(1);
            }
            else
            {
                dt1 = entry.ShiftBegin;
                dt2 = (entry.ShiftEnd == DateTime.MinValue) ? DateTime.Now : entry.ShiftEnd;
            }

            if (null == _tsb) _tsb = ops.TSB.GetCurrent().Value();
            if (null != _tsb && !string.IsNullOrWhiteSpace(entry.UserId))
            {
                var plazas = ops.TSB.GetTSBPlazas(_tsb).Value();
                if (null != plazas && plazas.Count > 0)
                {
                    // QR Code
                    plazas.ForEach(plaza =>
                    {
                        int pzId = plaza.SCWPlazaId;
                        var qrList = server.TOD.GetQRCodeList(nwId, pzId, entry.UserId, dt1, dt2);
                        if (null != qrList && null != qrList.list)
                        {
                            items.AddRange(qrList.list);
                        }
                    });

                    var sortList = items.OrderBy(o => o.trxDateTime).Distinct().ToList();
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
                }
            }
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            txtQty.Text = rowCnt.ToString("n0");
            txtTotal.Text = amtVal.ToString("n0");
        }
    }
}
