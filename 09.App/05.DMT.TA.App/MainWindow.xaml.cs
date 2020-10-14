#region Using

using System;
using System.Security.Principal;
using System.Windows;
using NLib.Services;

using DMT.Models;
using DMT.Services;

#endregion

namespace DMT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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

        

        #region Load/Unload

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TANofifyService.Instance.OnActiveTSBChanged += Instance_OnActiveTSBChanged;
            TANofifyService.Instance.OnChangeShift += Instance_OnChangeShift;

            // Initial Page Content Manager
            PageContentManager.Instance.ContentChanged += new EventHandler(Instance_ContentChanged);
            PageContentManager.Instance.StatusUpdated += new StatusMessageEventHandler(Instance_StatusUpdated);
            PageContentManager.Instance.OnTick += new EventHandler(Instance_OnTick);
            PageContentManager.Instance.Start();
            // Init Sign In
            var page = new Pages.SignInPage();
            page.Setup("ADMINS", "ACCOUNT", "CTC");
            PageContentManager.Instance.Current = page;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            // Release Page Content Manager
            PageContentManager.Instance.Shutdown();
            PageContentManager.Instance.OnTick -= new EventHandler(Instance_OnTick);
            PageContentManager.Instance.StatusUpdated -= new StatusMessageEventHandler(Instance_StatusUpdated);
            PageContentManager.Instance.ContentChanged -= new EventHandler(Instance_ContentChanged);

            TANofifyService.Instance.OnActiveTSBChanged -= Instance_OnActiveTSBChanged;
            TANofifyService.Instance.OnChangeShift -= Instance_OnChangeShift;
        }

        #endregion

        #region Notify Service Handlers

        private void Instance_OnActiveTSBChanged(object sender, EventArgs e)
        {
            AppNofifyService.Instance.RaiseActiveTSBChanged();
        }

        private void Instance_OnChangeShift(object sender, EventArgs e)
        {
            AppNofifyService.Instance.RaiseChangeShift();
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
    }
}
