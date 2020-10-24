#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;

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
        private User _user = null;
        private TSB _tsb = null;

        #region Button Handlers

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            // TODO: network id required.
            int nwId = 31;
            DateTime dt1 = DateTime.Now.Date;
            DateTime dt2 = dt1.AddDays(1);
            if (null != _user && null != _tsb)
            {
                var plazas = ops.TSB.GetTSBPlazas(_tsb).Value();
                if (null != plazas && plazas.Count > 0)
                {
                    int pzId = plazas[0].SCWPlazaId;
                    // Required common class to keep all list and sort by date.
                    var emvList = server.TOD.GetEMVList(nwId, pzId, _user.UserId, dt1, dt2);
                    var qrList = server.TOD.GetQRCodeList(nwId, pzId, _user.UserId, dt1, dt2);
                }
            }
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {

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

        public void Setup(User user)
        {
            _user = user;
            _tsb = ops.TSB.GetCurrent().Value();
            if (null != _user && null != _tsb)
            {
                
            }
        }
    }
}
