﻿#region Using

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

        private DateTime _entryDT = DateTime.MinValue;
        private DateTime _revDT = DateTime.MinValue;

        private User _user = null;
        private UserShift _userShift = null;
        private UserShiftRevenue _plazaRevenue = null;
        private List<LaneAttendance> _laneActivities = null;


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

            var plazaGroup = cbPlazas.SelectedItem as PlazaGroup;
            if (null == plazaGroup)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("กรุณาเลือกด่านของรายได้", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    cbPlazas.Focus();
                    return;
                }

                //MessageBox.Show("กรุณาเลือกด่านของรายได้", "DMT - Tour of Duty");
                //cbPlazas.Focus();
                //return;
            }

            bool isNew = false;
            var revops = Search.Revenues.PlazaShift.Create(_userShift, plazaGroup);
            _plazaRevenue = ops.Revenue.GetRevenueShift(revops).Value();
            if (null == _plazaRevenue)
            {
                // Create new if not found.
                _plazaRevenue = ops.Revenue.CreateRevenueShift(revops).Value();
                isNew = true;
            }

            if (null != _plazaRevenue)
            {
                if (_plazaRevenue.RevenueDate != DateTime.MinValue)
                {
                    DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                    msg.Owner = Application.Current.MainWindow;
                    msg.Setup("กะของพนักงานนี้ ถูกป้อนรายได้แล้ว", "DMT - Tour of Duty");
                    if (msg.ShowDialog() == true)
                    {
                        return;
                    }
                }
                if (null == _laneActivities || _laneActivities.Count <= 0)
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
                if (isNew)
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

            page.Setup(_user, _userShift, plazaGroup, _plazaRevenue,
                _laneActivities, _entryDT, _revDT);

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
            if (null != _userShift)
            {
                // get selected plaza group
                var plazaGroup = cbPlazas.SelectedItem as PlazaGroup;

                _revDT = _userShift.Begin.Date; // get date part from UserShift.Begin
                txtRevDate.Text = _revDT.ToThaiDateTimeString("dd/MM/yyyy");

                // get all lanes information.
                var search = Search.Lanes.Attendances.ByUserShift.Create(
                    _userShift, plazaGroup, DateTime.MinValue);

                _laneActivities = ops.Lanes.GetAttendancesByUserShift(search).Value();
                if (null == _laneActivities || _laneActivities.Count <= 0)
                {
                    // no data.
                    grid.Setup(null);
                }
                else
                {
                    grid.Setup(_laneActivities);
                }
            }
            else
            {
                //MessageBox.Show("ไม่พบกะของพนักงาน", "DMT - Tour of Duty");
            }
        }

        public void Setup(User user)
        {
            LoadPlazaGroups();

            _user = user;
            if (null != _user)
            {
                _entryDT = DateTime.Now;
                txtEntryDate.Text = _entryDT.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
                // Find user shift.
                _userShift = ops.UserShifts.GetCurrent(_user).Value();
                // Load related lane data.
                RefreshLanes();
            }
        }
    }
}