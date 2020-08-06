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
    /// Interaction logic for RevenueSupervisorPage.xaml
    /// </summary>
    public partial class RevenueSupervisorPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueSupervisorPage()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        private User _user = null;
        private UserShift _userShift = null;
        private PlazaGroup _plazaGroup = null;
        private UserShiftRevenue _plazaRevenue = null;
        private List<LaneAttendance> _laneActivities = null;

        private DateTime _entryDate = DateTime.MinValue;
        private DateTime _revDate = DateTime.MinValue;

        private Models.RevenueEntry _revenueEntry = null;

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Slip Preview
            var page = new Reports.RevenueSlipPreview();
            page.MenuPage = new Menu.MainMenu(); // Set MenPage to main menu.
            page.CallerPage = this; // Set CallerPage for click back.
            page.Setup(_user, _userShift, _plazaGroup, _plazaRevenue, _laneActivities,
                _entryDate, _revDate, _revenueEntry);
            PageContentManager.Instance.Current = page;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        public void Setup(User user, UserShift userShift, PlazaGroup plazaGroup,
            UserShiftRevenue plazaRevenue,
            List<LaneAttendance> laneActivities,
            DateTime entryDate, DateTime revDate)
        {
            _user = user;
            _userShift = userShift;
            _plazaGroup = plazaGroup;
            _plazaRevenue = plazaRevenue;
            _laneActivities = laneActivities;

            if (null == _userShift || null == _plazaGroup || null == _plazaRevenue)
            {
                _entryDate = DateTime.MinValue;
                _revDate = DateTime.MinValue;

                txtRevDate.Text = string.Empty;
                txtPlazaName.Text = string.Empty;

                txtShiftName.Text = string.Empty;

                txtUserId.Text = string.Empty;
                txtUserName.Text = string.Empty;

                revEntry.DataContext = null;
            }
            else
            {
                _entryDate = entryDate;
                _revDate = revDate;

                txtRevDate.Text = _revDate.ToThaiDateTimeString("dd/MM/yyyy");
                txtPlazaName.Text = _plazaGroup.PlazaGroupNameTH;

                txtShiftName.Text = _userShift.ShiftNameTH;
                
                txtUserId.Text = _userShift.UserId;
                txtUserName.Text = _userShift.FullNameTH;

                var search = Search.UserCredits.GetActiveById.Create(_userShift.UserId, _plazaGroup.PlazaGroupId);
                var userCredit = ops.Credits.GetActiveUserCreditById(search);
                _revenueEntry = new Models.RevenueEntry();
                if (null != userCredit)
                {
                    _revenueEntry.BagNo = userCredit.BagNo;
                    _revenueEntry.BeltNo = userCredit.BeltNo;
                }
                else
                {
                    _revenueEntry.BagNo = string.Empty;
                    _revenueEntry.BeltNo = string.Empty;
                }
                // assigned plaza.
                _revenueEntry.PlazaGroupId = _plazaGroup.PlazaGroupId;

                revEntry.DataContext = _revenueEntry;
            }
        }
    }
}
