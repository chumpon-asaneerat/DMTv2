#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DMT.Models;
using DMT.Services;
using System.Collections.ObjectModel;
using NLib.Reflection;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Runtime.InteropServices;

#endregion

namespace DMT.Simulator.Pages
{
    /// <summary>
    /// Interaction logic for LaneActivityPage.xaml
    /// </summary>
    public partial class LaneActivityPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LaneActivityPage()
        {
            InitializeComponent();
        }

        #endregion

        public class UserItem : User
        {
            public string RoleNameTH { get; set; }
        }

        public class LaneItem : Lane
        {
            public string UserId { get; set; }
            public string FullNameTH { get; set; }
        }

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        private List<UserItem> users = new List<UserItem>();
        private List<LaneItem> lanes = new List<LaneItem>();

        private LaneItem currentLane = null;
        private UserItem currentUser = null;

        #region Loaded/Unloaderd

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshLanes();
            RefreshUsers();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void RefreshUI()
        {
            gridTools.IsEnabled = false;
            if (null == currentLane) return;
            if (null == currentUser) return;
            gridTools.IsEnabled = true;

            var shift = ops.Jobs.GetCurrent(currentUser);
            if (null == shift)
            {
                shiftDate.IsEnabled = true;
                cmdBeginShift.IsEnabled = true;
                cmdEndShift.IsEnabled = false;

                jobDate.IsEnabled = false;
                cmdBeginJob.IsEnabled = false;
                cmdEndJob.IsEnabled = false;
            }
            else
            {
                shiftDate.IsEnabled = true;
                cmdBeginShift.IsEnabled = false;
                cmdEndShift.IsEnabled = true;

                jobDate.IsEnabled = false;
                cmdBeginJob.IsEnabled = false;
                cmdEndJob.IsEnabled = false;
            }
        }

        private void RefreshUsers()
        {
            lstUsers.ItemsSource = null;

            users.Clear();
            var role = ops.Users.GetRole(Search.Roles.ById.Create("COLLECTOR"));
            if (null != role)
            {
                var usrs = ops.Users.GetUsers(role);
                if (null != usrs)
                {
                    usrs.ForEach(usr =>
                    {
                        var inst = new UserItem();
                        inst.RoleNameTH = role.RoleNameTH;
                        usr.AssignTo(inst);
                        users.Add(inst);
                    });
                }
            }

            lstUsers.ItemsSource = users;

            RefreshUI();
        }

        private void RefreshLanes()
        {
            lvLanes.ItemsSource = null;

            lanes.Clear();
            var tsb = ops.TSB.GetCurrent();
            if (null != tsb)
            {
                var tsbLanes = ops.TSB.GetTSBLanes(tsb);
                if (null != tsbLanes)
                {
                    tsbLanes.ForEach(tsbLane =>
                    {
                        var inst = new LaneItem();

                        tsbLane.AssignTo(inst);
                        lanes.Add(inst);
                    });
                }
            }

            lvLanes.ItemsSource = lanes;

            RefreshUI();
        }

        private void RefreshLaneAttendances()
        {
            //var tsbshift = ops.Shifts.GetCurrent();
            //var userShifts = ops.Shifts.GetUserShift(tsbshift);
            //var items = ops.Lanes.GetAttendancesByShift();
        }

        private void RefreshLanePayments()
        {
            //var tsbshift = ops.Shifts.GetCurrent();
            //var userShifts = ops.Shifts.GetUserShift(tsbshift);
            //var items = ops.Lanes.GetPaymentsByShift();
        }

        #endregion

        #region ListView Handlers

        private void lvLanes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentLane = lvLanes.SelectedItem as LaneItem;
            RefreshUI();
        }

        #endregion

        #region ListBox Handlers

        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentUser = lstUsers.SelectedItem as UserItem;
            RefreshUI();
        }

        #endregion

        #region Button Handler(s)

        private void cmdRefreshAttendences_Click(object sender, RoutedEventArgs e)
        {
            RefreshLaneAttendances();
        }

        private void cmdRefreshPayments_Click(object sender, RoutedEventArgs e)
        {
            RefreshLanePayments();
        }

        private void cmdBeginShift_Click(object sender, RoutedEventArgs e)
        {
            /*
            var tsbshift = ops.Shifts.GetCurrent();
            Shift shift = Shift.Create();
            var inst = ops.Jobs.Create();
            ops.Jobs.BeginJob();
            */
        }

        private void cmdEndShift_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdBeginJob_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdEndJob_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
