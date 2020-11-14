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

namespace DMT.TOD.Pages.TollAdmin
{
    /// <summary>
    /// Interaction logic for EMVQRCodePage.xaml
    /// </summary>
    public partial class EMVQRCodePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EMVQRCodePage()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private SCWOperations server = SCWServiceOperations.Instance.Plaza;
        //private User _user = null;
        private User _selectUser = null;
        private TSB _tsb = null;

        #region TextBox Handlers

        private void txtSearchUserId_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                SearchUser();
            }
        }

        #endregion

        #region Button Handlers

        private void cmdSearchUser_Click(object sender, RoutedEventArgs e)
        {
            SearchUser();
        }

        private void rbEMV_Click(object sender, RoutedEventArgs e)
        {
            RefreshEMV_QRCODE();
        }

        private void rbQRCode_Click(object sender, RoutedEventArgs e)
        {
            RefreshEMV_QRCODE();
        }

        private void dtEntryDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshEMV_QRCODE();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            dtEntryDate.SelectedDate = DateTime.Now.Date;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        private void RefreshEMV_QRCODE()
        {
            if (!dtEntryDate.SelectedDate.HasValue)
            {
                dtEntryDate.Focus();
                return;
            }

            // TODO: network id required.
            int nwId = 31;
            DateTime dt1 = dtEntryDate.SelectedDate.Value.Date;
            DateTime dt2 = dt1.AddDays(1);

            grid.Setup();

            if (null != _selectUser && null != _tsb)
            {
                var plazas = ops.TSB.GetTSBPlazas(_tsb).Value();
                if (null != plazas && plazas.Count > 0)
                {
                    if (rbEMV.IsChecked.Value)
                    {
                        // EMV
                        var EMVList = new List<SCWEMV>();
                        plazas.ForEach(plaza => 
                        {
                            int pzId = plaza.SCWPlazaId;
                            var emvList = server.TOD.GetEMVList(nwId, pzId, _selectUser.UserId, dt1, dt2);
                            if (null != emvList && null != emvList.list)
                            {
                                EMVList.AddRange(emvList.list);
                            }
                        });

                        var sortList = EMVList.OrderBy(o => o.trxDateTime).Distinct().ToList();
                        grid.Setup(sortList);
                    }
                    else
                    {
                        // QR Code
                        var QRCODEList = new List<SCWQRCode>();
                        plazas.ForEach(plaza =>
                        {
                            int pzId = plaza.SCWPlazaId;
                            var qrList = server.TOD.GetQRCodeList(nwId, pzId, _selectUser.UserId, dt1, dt2);
                            if (null != qrList && null != qrList.list)
                            {
                                QRCODEList.AddRange(qrList.list);
                            }
                        });

                        var sortList = QRCODEList.OrderBy(o => o.trxDateTime).Distinct().ToList();
                        grid.Setup(sortList);
                    }
                }
            }
        }

        private void SearchUser()
        {
            if (!string.IsNullOrEmpty(txtSearchUserId.Text))
            {
                string userId = txtSearchUserId.Text;
                if (string.IsNullOrEmpty(userId)) return;

                UserSearchManager.Instance.Title = "กรุณาเลือกพนักงานเก็บเงิน";
                _selectUser = UserSearchManager.Instance.SelectUser(userId,
                    "ADMINS", 
                    "ACCOUNT",
                    "CTC_MGR", "CTC", "TC",
                    "MT_ADMIN", "MT_TECH", 
                    "FINANCE", "SV", 
                    "RAD_MGR", "RAD_SUP");
                if (null != _selectUser)
                {
                    RefreshEMV_QRCODE();
                }
                else
                {
                    grid.Setup(); // setup null list.
                }
            }
        }
 
        public void Setup(User user)
        {
            //_user = user;
            _tsb = ops.TSB.GetCurrent().Value();
            if (null != _tsb)
            {
                dtEntryDate.SelectedDate = DateTime.Now.Date;
                grid.Setup(); // setup null list.
            }
        }
    }
}
