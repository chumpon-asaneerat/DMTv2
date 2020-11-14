#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using DMT.Controls;

using NLib.Services;
using NLib.Reflection;
using DMT.TOD.Windows;

#endregion

namespace DMT.TOD.Pages.Revenue
{
    /// <summary>
    /// Interaction logic for SupervisorRevenueDateSelectionPage.xaml
    /// </summary>
    public partial class SupervisorRevenueDateSelectionPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SupervisorRevenueDateSelectionPage()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private HistoricalRevenueEntryManager _manager = new HistoricalRevenueEntryManager();
        private UserCreditBalance _selectUser = new UserCreditBalance();

        #region Button Handlers

        private void cmdSearchUser_Click(object sender, RoutedEventArgs e)
        {
            SearchUser();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (null == _manager.User)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("กรุณาเลือกพนักงาน", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    txtUserId.Focus();
                }
                return;
            }

            var shift = cbShifts.SelectedItem as Shift;
            if (null == shift)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("กรุณาเลือกกะของรายได้", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    cbShifts.Focus();
                }
                return;
            }

            var plazaGroup = cbPlazas.SelectedItem as PlazaGroup;

            if (null == plazaGroup)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("กรุณาเลือกด่านของรายได้", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    cbPlazas.Focus();
                }
                return;
            }

            // create new user shift.
            _manager.Shift = shift;
            _manager.PlazaGroup = plazaGroup;
            _manager.CreateUserShift();

            //_manager.EntryDate = DateTime.Now;
            _manager.RevenueDate = dtRevDate.SelectedDate.Value;

            // Revenue Entry Page
            var page = new SupervisorRevenueEntryPage();
            page.Setup(_manager.Create());
            PageContentManager.Instance.Current = page;
        }

        #endregion

        #region Combobox Handlers

        private void cbPlazas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var plazaGroup = cbPlazas.SelectedItem as PlazaGroup;
            _manager.PlazaGroup = plazaGroup;
            _manager.CreateUserShift();

            // Load related lane data.
            RefreshLanes();
        }

        private void cbShifts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var shift = cbShifts.SelectedItem as Shift;
            _manager.Shift = shift;
            _manager.CreateUserShift();

            // Load related lane data.
            RefreshLanes();
        }

        #endregion

        #region TextBox Handlers

        private void txtSearchUserId_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                SearchUser();
            }
        }

        #endregion

        private void SearchUser()
        {
            if (!string.IsNullOrEmpty(txtSearchUserId.Text))
            {
                string userId = txtSearchUserId.Text;
                if (string.IsNullOrEmpty(userId)) return;

                UserSearchManager.Instance.Title = "กรุณาเลือกพนักงานเก็บเงิน";
                _manager.User = UserSearchManager.Instance.SelectUser(userId,
                    "ADMINS",
                    "ACCOUNT",
                    "CTC_MGR", "CTC", "TC",
                    "MT_ADMIN", "MT_TECH",
                    "FINANCE", "SV",
                    "RAD_MGR", "RAD_SUP");
                if (null != _manager.User)
                {
                    _selectUser.UserId = _manager.User.UserId;
                    _selectUser.FullNameEN = _manager.User.FullNameEN;
                    _selectUser.FullNameTH = _manager.User.FullNameTH;

                    RefreshLanes();
                }
            }
        }

        private void LoadShifts()
        {
            var shifts = ops.Shifts.GetShifts().Value();
            cbShifts.ItemsSource = shifts;
            if (null != shifts && shifts.Count > 0)
            {
                cbShifts.SelectedIndex = 0;
            }
        }

        private void LoadPlazaGroups()
        {
            cbPlazas.ItemsSource = null;

            var plazaGroups = new List<PlazaGroup>();
            var tsb = ops.TSB.GetCurrent().Value();
            if (null != tsb)
            {
                plazaGroups = ops.TSB.GetTSBPlazaGroups(tsb).Value();
            }

            cbPlazas.ItemsSource = plazaGroups;
            if (null != plazaGroups && plazaGroups.Count > 0)
            {
                cbPlazas.SelectedIndex = 0;
            }
        }

        private void RefreshLanes()
        {
            // get selected plaza group
            _manager.PlazaGroup = cbPlazas.SelectedItem as PlazaGroup;
            _manager.CreateUserShift();

            if (null != _manager.User)
            {
                _manager.RefreshJobs();
                if (!_manager.HasAttendance)
                {
                    // no data.
                    grid.Setup(null);
                }
                else
                {
                    grid.Setup(_manager.Attendances);
                }
            }
        }

        public void Setup(User supervisor)
        {
            RevenueEntryManager.SyncMasters(); // Sync Currency/Coupon/CardAllow list.

            // check and send offline revenue entries to server.
            // TODO: Generate to Revenue files.
            // This step should removed and used scanning files timer insetad.
            RevenueEntryManager.SendRevnues();

            _manager.Supervisor = supervisor;
            _manager.EntryDate = DateTime.Now;

            LoadShifts();
            LoadPlazaGroups();
            dtEntryDate.SelectedDate = _manager.EntryDate;
            dtRevDate.SelectedDate = DateTime.Now;
            // for binding search user.
            this.DataContext = _selectUser;
        }
    }
}
