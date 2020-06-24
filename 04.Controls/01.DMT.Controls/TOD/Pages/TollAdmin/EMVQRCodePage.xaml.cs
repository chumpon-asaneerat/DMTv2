#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

#endregion

namespace DMT.TOD.Pages.TollAdmin
{
    /// <summary>
    /// Interaction logic for EMVQRCodePage.xaml
    /// </summary>
    public partial class EMVQRCodePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EMVQRCodePage()
        {
            InitializeComponent();
        }

        #endregion

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        /*
        public void Setup(List<Models.EMVQRCode> emvQR)
        {
            grid.Setup(emvQR);
        }
        */
    }
}
