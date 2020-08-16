#region Using

using System;
using System.Security.Principal;
using System.Windows;

using NLib.Services;

//using DMT.Models;
using DMT.Services;

using Fluent;

#endregion

namespace DMT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load Config service.
            ConfigManager.Instance.LoadConfig();

            LocalServiceOperations.Instance.ServiceMonitor.ScanConpleted += ServiceMonitor_ScanConpleted;
            LocalServiceOperations.Instance.ServiceMonitor.Start();

            // Initial Page Content Manager
            PageContentManager.Instance.ContentChanged += new EventHandler(Instance_ContentChanged);
            PageContentManager.Instance.StatusUpdated += new StatusMessageEventHandler(Instance_StatusUpdated);
            PageContentManager.Instance.OnTick += new EventHandler(Instance_OnTick);
            PageContentManager.Instance.Start();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            // Release Page Content Manager
            PageContentManager.Instance.Shutdown();
            PageContentManager.Instance.OnTick -= new EventHandler(Instance_OnTick);
            PageContentManager.Instance.StatusUpdated -= new StatusMessageEventHandler(Instance_StatusUpdated);
            PageContentManager.Instance.ContentChanged -= new EventHandler(Instance_ContentChanged);

            LocalServiceOperations.Instance.ServiceMonitor.Shutdown();
            LocalServiceOperations.Instance.ServiceMonitor.ScanConpleted -= ServiceMonitor_ScanConpleted;
        }

        #endregion

        #region Page Content Manager Handlers

        void Instance_OnTick(object sender, EventArgs e)
        {
            //UpdateTime();
            //UpdateConnectionStatus();
        }

        void Instance_StatusUpdated(object sender, StatusMessageEventArgs e)
        {
            //txtStatus.Text = e.Message;
        }

        void Instance_ContentChanged(object sender, EventArgs e)
        {
            this.container.Content = PageContentManager.Instance.Current;
        }

        #endregion

        class InstallStatus
        {

        }

        private void ServiceMonitor_ScanConpleted(object sender, EventArgs e)
        {

        }

        #region Button Handlers

        #region Local Uri/TSB Manager/User View

        private void cmdSetupLocalUri_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void cmdTSBManage_Click(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.Current = new Config.Pages.TSBViewPage();
        }

        private void cmdUserView_Click(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.Current = new Config.Pages.UserViewPage();
        }

        private void cmdShiftView_Click(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.Current = new Config.Pages.ShiftViewPage();
        }

        #endregion

        #region Credit/Coupon

        private void cmdTSBCreditView_Click(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.Current = new Config.Pages.TSBCreditViewPage();
        }

        #endregion

        #region Data Center

        private void cmdHQSetupUri_Click(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.Current = new Config.Pages.HQSetupUriPage();
        }

        #endregion

        #region Windows Services (Install/Unstall/CheckStatus)

        private void cmdInstall_Click(object sender, RoutedEventArgs e)
        {
            LocalServiceOperations.Instance.Install();
        }

        private void cmdUninstall_Click(object sender, RoutedEventArgs e)
        {
            LocalServiceOperations.Instance.Uninstall();
        }

        private void cmdCheckWindowServiceStatus_Click(object sender, RoutedEventArgs e)
        {
            var status = LocalServiceOperations.Instance.CheckInstalled();
            if (status.PlazaLocalServiceInstalled)
            {
                MessageBox.Show("Plaza Sercice installed and running", "DMT - Config");
            }
            else
            {
                MessageBox.Show("Plaza Sercice is not installed or stopped", "DMT - Config");
            }
        }

        #endregion

        #endregion
    }
}
