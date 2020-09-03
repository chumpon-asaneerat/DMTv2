#region Using

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

//using NLib.Services;
using DMT.Models;
using DMT.Services;

#endregion

namespace DMT.Controls.Header
{
    /// <summary>
    /// Interaction logic for HeaderShift.xaml
    /// </summary>
    public partial class HeaderShift : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeaderShift()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUI();
            LocalServiceOperations.Instance.OnChangeShift += Instance_OnChangeShift;
            LocalServiceOperations.Instance.OnActiveTSBChanged += Instance_OnActiveTSBChanged;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            LocalServiceOperations.Instance.OnActiveTSBChanged -= Instance_OnActiveTSBChanged;
            LocalServiceOperations.Instance.OnChangeShift -= Instance_OnChangeShift;
        }

        #endregion

        private void UpdateUI()
        {
            var ret = ops.Shifts.GetCurrent();
            var shift = ret.Value();
            if (null != shift)
            {
                txtShiftDate.Text = shift.BeginDateString;
                txtShiftTime.Text = shift.BeginTimeString;
                txtShiftId.Text = shift.ShiftNameTH;
            }
            else
            {
                txtShiftDate.Text = string.Empty;
                txtShiftTime.Text = string.Empty;
                txtShiftId.Text = string.Empty;
            }
        }

        private void Instance_OnChangeShift(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void Instance_OnActiveTSBChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
