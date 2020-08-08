﻿#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;
using DMT.TOD.Windows;

#endregion

namespace DMT.TOD.Pages.Revenue
{
    /// <summary>
    /// Interaction logic for SupervisorRevenueDateSelectionPage.xaml
    /// </summary>
    public partial class SupervisorRevenueDateSelectionPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SupervisorRevenueDateSelectionPage()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        private DateTime _entryDT = DateTime.MinValue;
        private DateTime _revDT = DateTime.MinValue;

        private User _sup = null;
        private User _user = null;
        private UserShift _userShift = null;
        private UserShiftRevenue _plazaRevenue = null;
        private List<LaneAttendance> _laneActivities = null;
        private UserCreditBalance srcObj;

        #region Button Handlers

        private void cmdSearchUser_Click(object sender, RoutedEventArgs e)
        {
            string userId = txtSearchUserId.Text;
            if (string.IsNullOrEmpty(userId)) return;

            var users = ops.Users.SearchById(Search.Users.ById.Create(userId));
            if (null != users)
            {
                if (users.Count == 1)
                {
                    _user = users[0];
                    srcObj.UserId = _user.UserId;
                    srcObj.FullNameEN = _user.FullNameEN;
                    srcObj.FullNameTH = _user.FullNameTH;
                }
                else if (users.Count > 1)
                {
                    var win = new TA.Windows.Collector.Searchs.CollectorFilterWindow();
                    win.Owner = Application.Current.MainWindow;
                    win.Setup(users);
                    if (win.ShowDialog() == false || null == win.SelectedUser)
                    {
                        // No user selected.
                        return;
                    }
                    _user = win.SelectedUser;
                    srcObj.UserId = _user.UserId;
                    srcObj.FullNameEN = _user.FullNameEN;
                    srcObj.FullNameTH = _user.FullNameTH;
                }
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (null == _user)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("กรุณาเลือกพนักงาน", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    txtUserId.Focus();
                }
                return;
            }

            var shift = cbShifts.SelectedItem as Shift;
            if (null == shift)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("กรุณาเลือกกะของรายได้", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    cbShifts.Focus();
                }
                return;
            }
            // create new user shift.
            _userShift = ops.UserShifts.Create(shift, _user);

            var plazaGroup = cbPlazas.SelectedItem as PlazaGroup;

            if (null == plazaGroup)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("กรุณาเลือกด่านของรายได้", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    cbPlazas.Focus();
                }
                return;
            }

            var revops = Search.Revenues.PlazaShift.Create(_userShift, plazaGroup);
            _plazaRevenue = ops.Revenue.CreateRevenueShift(revops);

            _entryDT = dtEntryDate.SelectedDate.Value;
            _revDT = dtRevDate.SelectedDate.Value;

            // Revenue Entry Page
            var page = new SupervisorRevenueEntryPage();
            page.Setup(_sup, _user, _userShift, plazaGroup, _plazaRevenue,
            _laneActivities,
            _entryDT, _revDT);
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

        private void LoadShifts()
        {
            cbShifts.ItemsSource = ops.Shifts.GetShifts();
        }

        private void LoadPlazaGroups()
        {
            cbPlazas.ItemsSource = null;

            var plazaGroups = new List<PlazaGroup>();
            var tsb = ops.TSB.GetCurrent();
            if (null != tsb)
            {
                plazaGroups = ops.TSB.GetTSBPlazaGroups(tsb);
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

                // get all lanes information.
                var search = Search.Lanes.Attendances.ByUserShift.Create(
                    _userShift, plazaGroup, DateTime.MinValue);
                _laneActivities = ops.Lanes.GetAttendancesByUserShift(search);
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

        public void Setup(User sup)
        {
            //TODO: Supervisor Revenue Entry Setup.
            /*
            _sup = sup;

            LoadShifts();
            LoadPlazaGroups();
            dtEntryDate.SelectedDate = DateTime.Now;
            dtRevDate.SelectedDate = DateTime.Now;

            srcObj = new UserCreditBalance();
            this.DataContext = srcObj;
            */
        }
    }
}
