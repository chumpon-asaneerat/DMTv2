#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;
using NLib.Reports.Rdlc;
using System.Reflection;
using System.ComponentModel;

#endregion

namespace DMT.TA.Windows.Collector.Credit
{
    /// <summary>
    /// Interaction logic for CollectorCreditBorrowWindow.xaml
    /// </summary>
    public partial class CollectorCreditBorrowWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CollectorCreditBorrowWindow()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private UserCredit _credit = null;

        #region Button Handlers

        private void cmdSearchUser_Click(object sender, RoutedEventArgs e)
        {
            string userId = txtSearchUserId.Text;
            if (string.IsNullOrEmpty(userId) || userId.Length < 5) return;
            var user = ops.Users.GetById(Search.Users.ById.Create(userId));
            if (null != user && null != _credit)
            {
                _credit.UserId = user.UserId;
                _credit.FullNameEN = user.FullNameEN;
                _credit.FullNameTH = user.FullNameTH;
            }
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (null != _credit)
            {
                Console.WriteLine(_credit);
            }
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        private void LoadMasters()
        {
            var tsb = ops.TSB.GetCurrent();
            if (null != tsb)
            {
                var plazaGroups = ops.TSB.GetTSBPlazaGroups(tsb);
                cbPlzaGroups.DisplayMemberPath = "PlazaGroupNameTH";
                cbPlzaGroups.ItemsSource = plazaGroups;
                if (null != plazaGroups && plazaGroups.Count > 0)
                {
                    cbPlzaGroups.SelectedIndex = 0;
                }
            }
        }

        public void Setup(UserCredit credit)
        {
            _credit = credit;
            if (null == _credit)
            {
                
            }
            else
            {
                if (_credit.UserCreditId == 0)
                {
                    panelSearch.Visibility = Visibility.Visible;
                }
                else panelSearch.Visibility = Visibility.Collapsed;
            }
            LoadMasters();
            this.DataContext = _credit;
        }
    }
}
