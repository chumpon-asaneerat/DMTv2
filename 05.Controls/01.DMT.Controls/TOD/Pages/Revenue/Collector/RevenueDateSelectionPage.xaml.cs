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

namespace DMT.TOD.Pages.Revenue
{
    /// <summary>
    /// Interaction logic for RevenueDateSelectionPage.xaml
    /// </summary>
    public partial class RevenueDateSelectionPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueDateSelectionPage()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private RevenueEntryManager _manager = new RevenueEntryManager();

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Revenue Entry Page
            var page = new RevenueEntryPage();
            if (null == _manager || null == _manager.PlazaGroup)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("กรุณาเลือกด่านของรายได้", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    cbPlazas.Focus();
                    return;
                }
            }

            _manager.CheckRevenueShift();
            if (null != _manager.RevenueShift)
            {
                if (_manager.HasRevenuShift)
                {
                    DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                    msg.Owner = Application.Current.MainWindow;
                    msg.Setup("กะของพนักงานนี้ ถูกป้อนรายได้แล้ว", "DMT - Tour of Duty");
                    if (msg.ShowDialog() == true)
                    {
                        return;
                    }
                }
                if (DMT.Controls.AppStatus.SCWOnline)
                {
                    if (!_manager.HasIncompletedLanes)
                    {
                        DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                        msg.Owner = Application.Current.MainWindow;
                        msg.Setup("ไม่พบข้อมูลเลนที่ยังไม่ถูกป้อนรายได้", "DMT - Tour of Duty");
                        if (msg.ShowDialog() == true)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    // Allow Offline enter.
                }
            }
            else
            {
                if (_manager.IsNewRevenueShift)
                {
                    DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                    msg.Owner = Application.Current.MainWindow;
                    msg.Setup("ไม่สามารถนำส่งรายได้ เนื่องจากไม่พบข้อมูลการทำงาน", "DMT - Tour of Duty");
                    if (msg.ShowDialog() == true)
                    {
                        //return;
                    }
                }
                else
                {
                    DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                    msg.Owner = Application.Current.MainWindow;
                    msg.Setup("กะนี้ถูกจัดเก็บรายได้แล้ว.", "DMT - Tour of Duty");
                    if (msg.ShowDialog() == true)
                    {
                        //return;
                    }
                }
                return;
            }

            if (!_manager.IsReturnBag)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("ระบบตรวจพบว่ายังไม่มีการคืนถุงเงิน กรุณาคืนถุงเงินก่อนป้อนรายได้.", "DMT - Tour of Duty");
                msg.ShowDialog();
                return;
            }

            page.Setup(_manager);

            PageContentManager.Instance.Current = page;
        }

        #endregion

        #region Combobox Handlers

        private void cbPlazas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Load related lane data.
            RefreshLanes();
        }

        #endregion

        private void LoadPlazaGroups()
        {
            cbPlazas.ItemsSource = null;

            var plazaGroups = new List<PlazaGroup>();
            var tsb = ops.TSB.GetCurrent().Value();
            if (null != tsb)
            {
                plazaGroups = ops.TSB.GetTSBPlazaGroups(tsb).Value();
            }

            cbPlazas.ItemsSource = plazaGroups;

            if (null != plazaGroups && plazaGroups.Count > 0)
            {
                cbPlazas.SelectedIndex = 0;
            }
        }

        private void RefreshLanes()
        {
            if (null != _manager && null != _manager.UserShift)
            {
                // get selected plaza group
                _manager.PlazaGroup = cbPlazas.SelectedItem as PlazaGroup;
                // reload jobs.
                _manager.RefreshJobs();
                if (!_manager.HasAttendance)
                {
                    grid.Setup(null); // no attendance data.
                }
                else
                {
                    grid.Setup(_manager.Attendances);
                }
            }
        }

        public void Setup(User user)
        {
            RevenueEntryManager.SyncMasters(); // Sync Currency/Coupon/CardAllow list.

            // check and send offline revenue entries to server.
            // TODO: Generate to Revenue files.
            // This step should removed and used scanning files timer insetad.
            RevenueEntryManager.SendRevnues();

            LoadPlazaGroups();
            // assign user.
            _manager.User = user;
            // assign supervisor.
            var cshf = ops.Shifts.GetCurrent().Value();
            var sup = ops.Users.GetById(Search.Users.ById.Create(cshf.UserId, "CTC")).Value();
            _manager.Supervisor = sup;

            if (null != _manager && null != _manager.User)
            {
                // Find user shift and Revenue Date.
                _manager.CheckUserShift();
                // Update entry date and revenue date.
                txtEntryDate.Text = _manager.EntryDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
                txtRevDate.Text = _manager.RevenueDate.ToThaiDateTimeString("dd/MM/yyyy");
                // Load Lane BOJ/EOJ List.
                RefreshLanes();
            }
        }
    }
}
