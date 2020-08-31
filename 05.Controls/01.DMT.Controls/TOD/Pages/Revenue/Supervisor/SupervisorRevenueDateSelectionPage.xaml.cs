#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using DMT.Controls;

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

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        private DateTime _entryDT = DateTime.MinValue;
        private DateTime _revDT = DateTime.MinValue;

        private User _sup = null;
        private User _user = null;
        private UserShift _userShift = null;
        private UserShiftRevenue _plazaRevenue = null;
        private List<LaneAttendance> _laneActivities = null;
        private UserCreditBalance srcObj = null;

        #region Button Handlers

        private void cmdSearchUser_Click(object sender, RoutedEventArgs e)
        {
            SearchUser();
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
            _userShift = ops.UserShifts.Create(shift, _user).Value();

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
            _plazaRevenue = ops.Revenue.CreateRevenueShift(revops).Value();

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
            var shifts = ops.Shifts.GetShifts().Value();
            cbShifts.ItemsSource = shifts;
        }

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
        // TODO: Refactor Revenue Entry Here.
        public void Setup(User sup)
        {
            _sup = sup;

            LoadShifts();
            LoadPlazaGroups();
            dtEntryDate.SelectedDate = DateTime.Now;
            dtRevDate.SelectedDate = DateTime.Now;

            srcObj = new UserCreditBalance();
            this.DataContext = srcObj;
        }

        private void txtSearchUserId_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                SearchUser();
            }
        }

        private void SearchUser()
        {
            if (!string.IsNullOrEmpty(txtSearchUserId.Text))
            {
                string userId = txtSearchUserId.Text;
                if (string.IsNullOrEmpty(userId)) return;

                UserSearchManager.Instance.Title = "กรุณาเลือกพนักงานเก็บเงิน";
                _user = UserSearchManager.Instance.SelectUser(userId, "CTC", "TC");
                if (null != _user)
                {
                    srcObj.UserId = _user.UserId;
                    srcObj.FullNameEN = _user.FullNameEN;
                    srcObj.FullNameTH = _user.FullNameTH;
                }
            }
        }

    }
}
