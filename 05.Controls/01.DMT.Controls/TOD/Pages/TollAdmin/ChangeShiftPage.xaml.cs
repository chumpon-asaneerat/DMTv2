#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

using DMT.Models;
using DMT.Services;
using NLib.Reflection;

#endregion

namespace DMT.TOD.Pages.TollAdmin
{
    /// <summary>
    /// Interaction logic for ChangeShiftPage.xaml
    /// </summary>
    public partial class ChangeShiftPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ChangeShiftPage()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private User _user = null;

        private List<LaneAttendance> _laneActivities = null;

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (cbShifts.SelectedIndex == -1)
            {
                cbShifts.Focus();
                return;
            }
            Shift shift = cbShifts.SelectedItem as Shift;
            if (null != shift)
            {
                var ret = ops.Shifts.Create(shift, _user);
                TSBShift inst = (null != ret && !ret.errors.hasError) ? ret.data : null;
                if (null != inst) shift.AssignTo(inst);
                ops.Shifts.ChangeShift(inst);
            }
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void RefreshLanes()
        {
            // get all lanes information.
            var ret = ops.Lanes.GetAllNotHasRevenueEntry();
            _laneActivities = (null != ret && !ret.errors.hasError) ? ret.data : null;
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

        public void Setup(User user)
        {
            _user = user;
            if (null != _user)
            {
                DateTime dt = DateTime.Now;
                var ret = ops.Shifts.GetShifts();
                var shifts = (null != ret && !ret.errors.hasError) ? ret.data : null;
                cbShifts.ItemsSource = shifts;
                // Load related lane data.
                RefreshLanes();
            }
        }
    }
}
