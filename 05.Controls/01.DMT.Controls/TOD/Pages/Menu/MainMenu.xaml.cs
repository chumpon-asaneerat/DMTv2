#region Using

using System.Windows;
using System.Windows.Controls;

using NLib.Services;

using DMT.Windows;
using System;

#endregion

namespace DMT.TOD.Pages.Menu
{
    /// <summary>
    /// Interaction logic for TODMainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
        }

        #endregion

        #region Button (Menu) Command Handlers

        private void beginUserShift_Click(object sender, RoutedEventArgs e)
        {
            var signinWin = new SignInWindow();
            signinWin.Owner = Application.Current.MainWindow;
            signinWin.Setup("COLLECTOR");
            if (signinWin.ShowDialog() == false)
            {
                return;
            }
            var user = signinWin.User;

            // Begin of Job Page
            var jobWindow = new Windows.UserShifts.BOSWindow();
            jobWindow.Owner = Application.Current.MainWindow;
            jobWindow.Setup(user);
            if (jobWindow.ShowDialog() == false)
            {
                return;
            }
        }

        private void revEntry_Click(object sender, RoutedEventArgs e)
        {
            var signinWin = new SignInWindow();
            signinWin.Owner = Application.Current.MainWindow;
            signinWin.Setup("COLLECTOR");
            if (signinWin.ShowDialog() == false)
            {
                return;
            }
            var user = signinWin.User;

            // Revenue Entry
            var page = new Revenue.RevenueDateSelectionPage();
            // setup
            page.Setup(user);
            PageContentManager.Instance.Current = page;
        }

        private void supRevEntry_Click(object sender, RoutedEventArgs e)
        {
            var signinWin = new SignInWindow();
            signinWin.Owner = Application.Current.MainWindow;
            signinWin.Setup("SUPERVISOR");
            if (signinWin.ShowDialog() == false)
            {
                return;
            }
            var user = signinWin.User;

            var page = new Revenue.SupervisorRevenueDateSelectionPage();
            page.Setup(user);
            PageContentManager.Instance.Current = page;
        }

        private void changeShift_Click(object sender, RoutedEventArgs e)
        {
            var signinWin = new SignInWindow();
            signinWin.Owner = Application.Current.MainWindow;
            signinWin.Setup("SUPERVISOR");
            if (signinWin.ShowDialog() == false)
            {
                return;
            }
            var user = signinWin.User;

            // Change Shift
            var page = new TollAdmin.ChangeShiftPage();
            // setup
            page.Setup(user);
            PageContentManager.Instance.Current = page;
        }

        private void reportMenu_Click(object sender, RoutedEventArgs e)
        {
            var signinWin = new SignInWindow();
            signinWin.Owner = Application.Current.MainWindow;
            signinWin.Setup("SUPERVISOR", "AUDIT", "ADMIN", "QFREE");
            if (signinWin.ShowDialog() == false)
            {
                return;
            }
            var user = signinWin.User;

            // Report Main Menu
            var page = new ReportMenu();
            // setup
            page.Setup(user);
            PageContentManager.Instance.Current = page;
        }

        private void emvQRCode_Click(object sender, RoutedEventArgs e)
        {
            var signinWin = new SignInWindow();
            signinWin.Owner = Application.Current.MainWindow;
            signinWin.Setup("SUPERVISOR", "COLLECTOR", "AUDIT", "ADMIN", "QFREE");
            if (signinWin.ShowDialog() == false)
            {
                return;
            }
            var user = signinWin.User;

            var page = new TollAdmin.EMVQRCodePage();
            page.Setup(user);
            PageContentManager.Instance.Current = page;
        }

        private void loginList_Click(object sender, RoutedEventArgs e)
        {
            var signinWin = new SignInWindow();
            signinWin.Owner = Application.Current.MainWindow;
            signinWin.Setup("SUPERVISOR", "COLLECTOR", "AUDIT", "ADMIN", "QFREE");
            if (signinWin.ShowDialog() == false)
            {
                return;
            }
            var user = signinWin.User;

            var page = new Job.LoginListPage();
            page.Setup(user);
            PageContentManager.Instance.Current = page;
        }

        #endregion
    }
}
