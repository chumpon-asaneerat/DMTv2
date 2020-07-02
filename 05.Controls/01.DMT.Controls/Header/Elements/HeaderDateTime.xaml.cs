#region Using

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

//using NLib.Services;
//using DMT.Services;

#endregion

namespace DMT.Controls.Header
{
    /// <summary>
    /// Interaction logic for HeaderDateTime.xaml
    /// </summary>
    public partial class HeaderDateTime : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeaderDateTime()
        {
            InitializeComponent();
        }

        #endregion

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
            DateTime dt = DateTime.Now;
            txtCurrentDate.Text = dt.ToThaiDateString();
            txtCurrentTime.Text = dt.ToThaiTimeString();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
