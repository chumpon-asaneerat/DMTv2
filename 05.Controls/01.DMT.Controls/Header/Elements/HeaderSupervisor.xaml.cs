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
    /// Interaction logic for HeaderSupervisor.xaml
    /// </summary>
    public partial class HeaderSupervisor : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeaderSupervisor()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private DispatcherTimer timer = new DispatcherTimer();

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUI();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (null != timer)
            {
                timer.Stop();
            }
            timer = null;
        }

        #endregion

        private void UpdateUI()
        {
            var shift = ops.Shifts.GetCurrent();
            if (null == shift)
            {
                txtSupervisorId.Text = "รหัสหัวหน้าด่าน : ";
                txtSupervisorName.Text = "หัวหน้าด่าน : ";
            }
            else
            {
                txtSupervisorId.Text = "รหัสหัวหน้าด่าน : " + shift.UserId;
                txtSupervisorName.Text = "หัวหน้าด่าน : " + shift.FullNameTH;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
