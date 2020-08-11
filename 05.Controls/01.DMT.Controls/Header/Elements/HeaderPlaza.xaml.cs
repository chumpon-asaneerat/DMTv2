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
    /// Interaction logic for HeaderPlaza.xaml
    /// </summary>
    public partial class HeaderPlaza : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeaderPlaza()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private DispatcherTimer timer = new DispatcherTimer();

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtPlazaId.Visibility = Visibility.Collapsed;

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
            var tsb = ops.TSB.GetCurrent();
            if (null != tsb)
            {
                txtPlazaId.Text = "รหัสด่าน : " + tsb.TSBId;
                txtPlazaName.Text = "ชื่อด่าน : " + tsb.TSBNameTH;
            }
            else
            {
                txtPlazaId.Text = "รหัสด่าน : ";
                txtPlazaName.Text = "ชื่อด่าน : ";
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
