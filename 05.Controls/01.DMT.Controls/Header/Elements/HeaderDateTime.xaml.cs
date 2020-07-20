#region Using

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        private NLib.Components.PingManager ping;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ping = new NLib.Components.PingManager();
            ping.OnReply += Ping_OnReply;
            ping.Add(@"www.google.com2");
            ping.Interval = 1000;
            ping.Start();

            UpdateUI();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void Ping_OnReply(object sender, NLib.Networks.PingResponseEventArgs e)
        {
            if (null != e.Reply && 
                e.Reply.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                borderDT.Background = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                borderDT.Background = new SolidColorBrush(Colors.Maroon);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (null != ping)
            {
                ping.OnReply -= Ping_OnReply;
                ping.Stop();
                ping.Dispose();
            }
            ping = null;

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
