﻿#region Using

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

namespace DMT.Config.Pages
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

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private List<RoleItem> items = new List<RoleItem>();

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshTree();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void RefreshTree()
        {
            tree.ItemsSource = null;

            items.Clear();

            var roles = ops.Users.GetRoles().Value();
            if (null != roles)
            {
                roles.ForEach(role =>
                {
                    RoleItem item = role.CloneTo<RoleItem>();
                    items.Add(item);
                    var users = ops.Users.GetUsers(role).Value();
                    if (null != users)
                    {
                        users.ForEach(user =>
                        {
                            UserItem uItem = user.CloneTo<UserItem>();
                            item.Users.Add(uItem);
                        });
                    }
                });
            }

            tree.ItemsSource = items;
        }

        #endregion

        #region TreeView Handler

        private void tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            pgrid.SelectedObject = e.NewValue;
        }

        #endregion

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            var user = (pgrid.SelectedObject as User);
            if (null != user)
            {
                var ret = ops.Users.SaveUser(user);
                if (ret.Failed)
                {
                    MessageBox.Show("Save User Error.");
                }
            }
        }
    }

    public class RoleItem : Role
    {
        public RoleItem()
        {
            this.Users = new ObservableCollection<UserItem>();
        }
        public ObservableCollection<UserItem> Users { get; set; }
    }

    public class UserItem : User { }
}
