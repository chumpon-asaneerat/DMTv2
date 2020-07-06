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
        private List<LaneAttendance> _laneActivities = null;

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
            page.Setup(_userShift, plaza, _laneActivities, _entryDT, _revDT);

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

        private void LoadPlazas()
        {
            cbPlazas.ItemsSource = null;

            List<Models.Plaza> plazas = new List<Plaza>();
            var tsb = ops.TSB.GetCurrent();
            if (null != tsb)
            {
                plazas = ops.TSB.GetTSBPlazas(tsb);
            }

            cbPlazas.ItemsSource = plazas;
        }

        private void RefreshLanes()
        {
            if (null != _userShift)
            {
                if (_userShift.RevenueDate == DateTime.MinValue)
                {
                    _revDT = _userShift.Begin.Date; // get date part from UserShift.Begin
                    txtRevDate.Text = _revDT.ToThaiDateTimeString("dd/MM/yyyy");

                    // get selected plaza
                    var plaza = cbPlazas.SelectedItem as Plaza;
                    // get all lanes information.
                    var search = Search.Lanes.Attendances.ByUserShift.Create(_userShift, plaza, DateTime.MinValue);
                    _laneActivities = ops.Lanes.GetAttendancesByUserShift(search);
                    if (null == _laneActivities || _laneActivities.Count <= 0)
                    {
                        // no data.
                        grid.DataContext = null;
                    }
                    else
                    {
                        grid.DataContext = _laneActivities;
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

        public void Setup(User user)
        {
            LoadPlazas();

            _user = user;
            if (null != _user)
            {
                _entryDT = DateTime.Now;
                txtEntryDate.Text = _entryDT.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
                // Find user shift.
                _userShift = ops.Jobs.GetCurrent(_user);
                // Load related lane data.
                RefreshLanes();
            }
        }
    }
}
