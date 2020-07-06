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
        private User _user = null;
        private UserShift _userShift = null;
        private DateTime _entryDT = DateTime.MinValue;
        private DateTime _revDT = DateTime.MinValue;

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
            PageContentManager.Instance.Current = page;
            page.Setup(_userShift, _entryDT, _revDT);
        }

        #endregion

        public void Setup(User user)
        {
            _user = user;
            if (null != _user)
            {
                _entryDT = DateTime.Now;
                txtEntryDate.Text = _entryDT.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");

                _userShift = ops.Jobs.GetCurrent(_user);
                if (null != _userShift)
                {
                    if (_userShift.RevenueDate == DateTime.MinValue)
                    {
                        _revDT = _userShift.Begin.Date; // get date part from UserShift.Begin
                        txtRevDate.Text = _revDT.ToThaiDateTimeString("dd/MM/yyyy");

                        var search = Search.Lanes.Attendances.ByUserShift.Create(_userShift);
                        var laneActivities = ops.Lanes.GetAttendancesByUserShift(search);
                        if (null == laneActivities || laneActivities.Count <= 0)
                        {
                            // no data.
                            grid.DataContext = null;
                        }
                        else
                        {
                            grid.DataContext = laneActivities;
                        }
                    }
                    else
                    {
                        MessageBox.Show("กะของพนักงานนี้ ถูกป้อนรายได้แล้ว");
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบกะของพนักงาน");
                }
            }
        }
    }
}
