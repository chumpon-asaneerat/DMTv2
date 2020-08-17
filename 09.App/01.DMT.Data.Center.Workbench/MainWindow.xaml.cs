#region Using

using System;
using System.Security.Principal;
using System.Windows;

using NLib.Services;

//using DMT.Models;
//using DMT.Services;

using Fluent;
using DMT.TAxTOD.Pages;

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

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Initial Page Content Manager
            PageContentManager.Instance.ContentChanged += new EventHandler(Instance_ContentChanged);
            PageContentManager.Instance.StatusUpdated += new StatusMessageEventHandler(Instance_StatusUpdated);
            PageContentManager.Instance.OnTick += new EventHandler(Instance_OnTick);
            PageContentManager.Instance.Start();
        }

        private void RibbonWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            // Release Page Content Manager
            PageContentManager.Instance.Shutdown();
            PageContentManager.Instance.OnTick -= new EventHandler(Instance_OnTick);
            PageContentManager.Instance.StatusUpdated -= new StatusMessageEventHandler(Instance_StatusUpdated);
            PageContentManager.Instance.ContentChanged -= new EventHandler(Instance_ContentChanged);
        }

        #endregion

        #region Page Content Manager Handlers

        void Instance_OnTick(object sender, EventArgs e)
        {

        }

        void Instance_StatusUpdated(object sender, StatusMessageEventArgs e)
        {

        }

        void Instance_ContentChanged(object sender, EventArgs e)
        {
            this.container.Content = PageContentManager.Instance.Current;
        }

        #endregion

        #region Button Handlers

        private void cmdGetCoupons_Click(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.Current = new TAxTOD.Pages.CouponViewPage();
        }

        private void cmdSendUserLog_Click(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.Current = new TAxTOD.Pages.DemoPage();
        }

        private void cmdSendRevenue_Click(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.Current = new TAxTOD.Pages.DemoPage();
        }

        #endregion
    }
}
