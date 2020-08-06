#region Using

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
    /// Interaction logic for SupervisorRevenueEntryPage.xaml
    /// </summary>
    public partial class SupervisorRevenueEntryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SupervisorRevenueEntryPage()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        private DateTime _entryDT = DateTime.MinValue;
        private DateTime _revDT = DateTime.MinValue;

        private User _user = null;
        private UserShift _userShift = null;
        private UserShiftRevenue _plazaRevenue = null;
        private List<LaneAttendance> _laneActivities = null;
        private UserCredit srcObj;

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
                    var user = users[0];
                    srcObj.UserId = user.UserId;
                    srcObj.FullNameEN = user.FullNameEN;
                    srcObj.FullNameTH = user.FullNameTH;
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
                    var user = win.SelectedUser;
                    srcObj.UserId = user.UserId;
                    srcObj.FullNameEN = user.FullNameEN;
                    srcObj.FullNameTH = user.FullNameTH;
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
            // Revenue Entry Page
            var page = new RevenueRevSlipPage();


            page.Setup(_user, _userShift, _entryDT, _revDT);

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
                txtRevDate.Text = _revDT.ToThaiDateTimeString("dd/MM/yyyy");

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

        public void Setup()
        {
            LoadPlazaGroups();
            dtEntryDate.SelectedDate = DateTime.Now;

            if (null == srcObj)
            {
                srcObj = new UserCredit();
            }

            this.DataContext = srcObj;

        }
    }
}
