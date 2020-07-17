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

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        private DateTime _entryDT = DateTime.MinValue;
        private DateTime _revDT = DateTime.MinValue;

        private UserShift _userShift = null;
        private UserShiftRevenue _plazaRevenue = null;
        private List<LaneAttendance> _laneActivities = null;

        private User _user = null;

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

            var plaza = cbPlazas.SelectedItem as Plaza;
            if (null == plaza)
            {
                MessageBox.Show("กรุณาเลือกด่านของรายได้");
                cbPlazas.Focus();
                return;
            }

            bool isNew = false;
            var revops = Search.Revenues.PlazaShift.Create(_userShift, plaza);
            _plazaRevenue = ops.Revenue.GetRevenueShift(revops);
            if (null == _plazaRevenue)
            {
                // Create new if not found.
                _plazaRevenue = ops.Revenue.CreateRevenueShift(revops);
                isNew = true;
            }

            if (null != _plazaRevenue)
            {
                if (_plazaRevenue.RevenueDate != DateTime.MinValue)
                {
                    MessageBox.Show("กะของพนักงานนี้ ถูกป้อนรายได้แล้ว");
                    return;
                }
                if (null == _laneActivities || _laneActivities.Count <= 0)
                {
                    MessageBox.Show("ไม่พบข้อมูลเลนที่ยังไม่ถูกป้อนรายได้");
                    return;
                }
            }
            else
            {
                if (isNew)
                {
                    MessageBox.Show("ไม่สามารถสร้างรายการสำหรับจัดเก็บกะรายได้.");
                }
                else
                {
                    MessageBox.Show("กะนี้ถูกจัดเก็บรายได้แล้ว.");
                }
                return;
            }


            page.Setup(_userShift, plaza, _plazaRevenue, _laneActivities, _entryDT, _revDT);

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
                //MessageBox.Show("ไม่พบกะของพนักงาน");
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
                _userShift = ops.UserShifts.GetCurrent(_user);
                // Load related lane data.
                RefreshLanes();
            }
        }
    }
}
