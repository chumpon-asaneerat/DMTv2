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
        //private DispatcherTimer timer = new DispatcherTimer();

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUI();
            /*
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            */
            LocalServiceOperations.Instance.OnChangeShift += Instance_OnChangeShift;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            LocalServiceOperations.Instance.OnChangeShift -= Instance_OnChangeShift;
            /*
            if (null != timer)
            {
                timer.Stop();
            }
            timer = null;
            */
        }

        #endregion

        private void UpdateUI()
        {
            var shift = ops.Shifts.GetCurrent();
            if (null != shift)
            {
                txtShiftDate.Text = shift.Begin.ToThaiDateString();
                txtShiftTime.Text = shift.Begin.ToThaiTimeString();
                txtShiftId.Text = shift.ShiftNameTH;
            }
            else
            {
                txtShiftDate.Text = string.Empty;
                txtShiftTime.Text = string.Empty;
                txtShiftId.Text = string.Empty;
            }
        }
        /*
        void timer_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }
        */

        private void Instance_OnChangeShift(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
