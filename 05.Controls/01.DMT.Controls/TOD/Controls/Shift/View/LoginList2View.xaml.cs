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

namespace DMT.TOD.Controls.Revenue.View
{
    /// <summary>
    /// Interaction logic for LaneView.xaml
    /// </summary>
    public partial class LoginList2View : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoginList2View()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private User _user = null;

        private List<UserShift> _userShifts = null;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUsers();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region ListView Handlers

        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = lstUsers.SelectedItem as UserShift;
            RefreshLane(item);
        }

        #endregion

        private void RefreshLane(UserShift userShift)
        {
            lstLaneJobs.ItemsSource = null;
            if (null == userShift) return;

            var ret = ops.Lanes.GetAllAttendancesByUserShift(userShift);
            var lanes = (null != ret && !ret.errors.hasError) ? ret.data : null;

            lstLaneJobs.ItemsSource = lanes;
        }

        public void RefreshUsers() 
        {
            lstLaneJobs.ItemsSource = null;
            lstUsers.ItemsSource = null;

            var ret = ops.UserShifts.GetUnCloseUserShifts();
            _userShifts = (null != ret && !ret.errors.hasError) ? ret.data : null;

            lstUsers.ItemsSource = _userShifts;
        }

        public void Setup(User user)
        {
            _user = user;
        }
    }
}
