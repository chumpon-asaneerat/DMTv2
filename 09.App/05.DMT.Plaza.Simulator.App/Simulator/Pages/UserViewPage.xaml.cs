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
    /// Interaction logic for UserViewPage.xaml
    /// </summary>
    public partial class UserViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserViewPage()
        {
            InitializeComponent();
        }

        #endregion

        public class UserItem : User
        {
            private UserShift _Shift = null;

            public UserShift Shift
            {
                get { return _Shift; }
                set
                {
                    _Shift = value;
                    RaiseChanged("BeginDateString");
                    RaiseChanged("BeginTimeString");
                }
            }

            public string BeginDateString
            {
                get { return (null != Shift) ? Shift.BeginDateString : string.Empty; }
                set { }
            }
            public string BeginTimeString
            {
                get { return (null != Shift) ? Shift.BeginTimeString : string.Empty; }
                set { }
            }
        }

        public class LaneItem : Lane
        {
            public string UserId { get; set; }
            public string FullNameTH { get; set; }
        }

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        private List<UserItem> users = new List<UserItem>();

        #region Loaded/Unloaderd

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUsers();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void RefreshUsers()
        {
            lstUsers.ItemsSource = null;

            users.Clear();
            /*
            var roles = ops.Users.GetRoles();
            if (null != roles)
            {
                roles.ForEach(role =>
                {
                    var usrs = ops.Users.GetUsers(role);
                    if (null != usrs)
                    {
                        usrs.ForEach(usr =>
                        {
                            var inst = new UserItem();
                            usr.AssignTo(inst);
                            users.Add(inst);
                        });
                    }
                });
            }
            */
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
                        users.Add(inst);
                    });
                }
            }
            lstUsers.ItemsSource = users;
        }

        private void RefreshUserShifts(UserItem user)
        {
            lvUserShifts.ItemsSource = null;

            if (null == user) return;

            var userShifts = ops.UserShifts.GetUserShifts(user);

            lvUserShifts.ItemsSource = userShifts;
        }

        #endregion

        #region ListBox Handler(s)

        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = lstUsers.SelectedItem as UserItem;
            pgrid.SelectedObject = item;

            RefreshUserShifts(item);
        }

        #endregion

        #region Button Handler(s)

        #endregion
    }
}
