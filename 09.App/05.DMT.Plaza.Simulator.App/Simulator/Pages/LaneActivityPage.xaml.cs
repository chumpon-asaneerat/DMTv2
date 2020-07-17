#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Globalization;

using DMT.Models;
using DMT.Services;
using System.Collections.ObjectModel;
using NLib.Reflection;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Runtime.InteropServices;
using System.Net;

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
            private UserShift _Shift = null;

            [Category("Shift")]
            [Browsable(false)]
            public UserShift Shift 
            {
                get { return _Shift;  }
                set
                {
                    _Shift = value;
                    RaiseChanged("BeginDateString");
                    RaiseChanged("BeginTimeString");
                }
            }

            [Category("Shift")]
            [ReadOnly(true)]
            public string BeginDateString
            {
                get { return (null != Shift) ? Shift.BeginDateString : string.Empty; }
                set { }
            }

            [Category("Shift")]
            [ReadOnly(true)]
            public string BeginTimeString
            {
                get { return (null != Shift) ? Shift.BeginTimeString : string.Empty; }
                set { }
            }
        }

        public class LaneItem : Lane
        {
            private LaneAttendance _Attendance = null;

            [Category("Activity")]
            [Browsable(false)]
            public LaneAttendance Attendance
            {
                get { return _Attendance;  }
                set
                {
                    _Attendance = value;
                    RaiseChanged("UserId");
                    RaiseChanged("FullNameTH");
                    RaiseChanged("BeginDateString");
                    RaiseChanged("BeginTimeString");
                }
            }
            [Category("User")]
            [ReadOnly(true)]
            public string UserId 
            {
                get { return (Attendance != null) ? Attendance.UserId : string.Empty; }
                set { }
            }
            [Category("User")]
            [ReadOnly(true)]
            public string FullNameTH 
            {
                get { return (Attendance != null) ? Attendance.FullNameTH : string.Empty; }
                set { }
            }
            [Category("Lane Job")]
            [ReadOnly(true)]
            public string BeginDateString
            {
                get { return (Attendance != null) ? Attendance.Begin.ToThaiDateString() : string.Empty; }
                set { }
            }
            [Category("Lane Job")]
            [ReadOnly(true)]
            public string BeginTimeString
            {
                get { return (Attendance != null) ? Attendance.Begin.ToThaiTimeString() : string.Empty; }
                set { }
            }
        }

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        private List<Shift> shifts = new List<Shift>();
        private List<UserItem> users = new List<UserItem>();
        private List<LaneItem> lanes = new List<LaneItem>();

        private LaneItem currentLane = null;
        private UserItem currentUser = null;

        private CultureInfo culture = new CultureInfo("th-TH");

        #region Loaded/Unloaderd

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            shiftDate.CultureInfo = culture;
            shiftDate.Format = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
            shiftDate.FormatString = "yyyy-MM-dd HH:mm:ss.fff";
            shiftDate.TimeFormat = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
            shiftDate.TimeFormatString = "HH:mm:ss.fff";

            shiftDate.DefaultValue = DateTime.Now;
            shiftDate.Value = DateTime.Now;
            shiftDate.GotFocus += ShiftDate_GotFocus;

            jobDate.CultureInfo = culture;
            jobDate.Format = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
            jobDate.FormatString = "yyyy-MM-dd HH:mm:ss.fff";
            jobDate.TimeFormat = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
            jobDate.TimeFormatString = "HH:mm:ss.fff";
            jobDate.GotFocus += JobDate_GotFocus;

            jobDate.DefaultValue = DateTime.Now;
            jobDate.Value = DateTime.Now;

            RefreshLanes();
            RefreshUsers();
            RefreshShifts();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void RefreshUI()
        {
            gridTools.IsEnabled = false;
            if (null == currentUser) return;
            gridTools.IsEnabled = true;

            // Find UserShift.
            currentUser.Shift = ops.UserShifts.GetCurrent(currentUser);

            if (null == currentUser.Shift)
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

                // Dislable all job begin/end controls.
                jobDate.IsEnabled = false;
                cmdBeginJob.IsEnabled = false;
                cmdEndJob.IsEnabled = false;

                if (null == currentLane) return;

                jobDate.IsEnabled = true;
                cmdBeginJob.IsEnabled = (null == currentLane.Attendance) ? true : false;
                cmdEndJob.IsEnabled = !cmdBeginJob.IsEnabled;
            }
        }

        private void RefreshShifts()
        {
            cbShifts.ItemsSource = null;

            shifts = ops.Shifts.GetShifts();

            cbShifts.ItemsSource = shifts;
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
                        usr.AssignTo(inst);
                        // load user shift.
                        inst.Shift = ops.UserShifts.GetCurrent(usr);
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
                        // find Attendance.
                        var search = Search.Lanes.Current.AttendanceByLane.Create(tsbLane);
                        inst.Attendance = ops.Lanes.GetCurrentAttendancesByLane(search);
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
            if (null == currentLane) return;

            lvAttendances.ItemsSource = null;

            var search = Search.Lanes.Attendances.ByLane.Create(currentLane);
            lvAttendances.ItemsSource = ops.Lanes.GetAttendancesByLane(search);
        }

        private void RefreshLanePayments()
        {
            //var tsbshift = ops.Shifts.GetCurrent();
            //var userShifts = ops.Shifts.GetUserShift(tsbshift);
            //var items = ops.Lanes.GetPaymentsByShift();
        }

        #endregion

        #region DateTimePicker Handlers

        private void ShiftDate_GotFocus(object sender, RoutedEventArgs e)
        {
            shiftDate.Value = DateTime.Now;
        }

        private void JobDate_GotFocus(object sender, RoutedEventArgs e)
        {
            jobDate.Value = DateTime.Now;
        }

        #endregion

        #region ListView Handlers

        private void lvLanes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentLane = lvLanes.SelectedItem as LaneItem;
            
            RefreshLaneAttendances();
            RefreshLanePayments();

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

        #region ComboBox Handler(s)

        private void cbShifts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            if (!shiftDate.Value.HasValue) return;

            if (null == currentUser) return;
            var shift = (cbShifts.SelectedItem as Shift);
            if (null == shift) return;
            var inst = ops.UserShifts.Create(shift, currentUser);

            DateTime dt = shiftDate.Value.Value;
            inst.Begin = dt;
            ops.UserShifts.BeginUserShift(inst);

            RefreshUsers();

            RefreshUI();
        }

        private void cmdEndShift_Click(object sender, RoutedEventArgs e)
        {
            if (null == currentUser) return;
            if (null == currentUser.Shift) return;

            if (!shiftDate.Value.HasValue) return;

            DateTime dt = shiftDate.Value.Value;
            currentUser.Shift.End = dt;

            ops.UserShifts.EndUserShift(currentUser.Shift);
            
            RefreshUsers();

            RefreshUI();
        }

        private void cmdBeginJob_Click(object sender, RoutedEventArgs e)
        {
            if (null == currentLane) return;
            if (null == currentUser) return;
            if (!jobDate.Value.HasValue) return;
            if (null != currentLane.Attendance) return; // has attendance.

            var attd = ops.Lanes.CreateAttendance(currentLane, currentUser);

            DateTime dt = jobDate.Value.Value;
            // Set Begin Job date.
            attd.Begin = dt;

            ops.Lanes.SaveAttendance(attd);
            // Set Attendance
            currentLane.Attendance = attd;

            // update list views
            RefreshLaneAttendances();
            RefreshLanePayments();

            RefreshUI();
        }

        private void cmdEndJob_Click(object sender, RoutedEventArgs e)
        {
            if (null == currentLane) return;
            if (!jobDate.Value.HasValue) return;

            var attd = currentLane.Attendance;
            if (null != attd)
            {
                DateTime dt = jobDate.Value.Value;
                // Set End Job date.
                attd.End = dt;

                // Save to database.
                ops.Lanes.SaveAttendance(attd);
            }
            // Clear Attendance
            currentLane.Attendance = null;

            // update list views
            RefreshLaneAttendances();
            RefreshLanePayments();

            RefreshUI();
        }

        #endregion
    }
}
