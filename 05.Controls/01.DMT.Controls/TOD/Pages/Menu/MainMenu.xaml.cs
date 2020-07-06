#region Using

using System.Windows;
using System.Windows.Controls;

using NLib.Services;

using DMT.Windows;

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

        private void beginJob_Click(object sender, RoutedEventArgs e)
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
            var jobWindow = new Windows.Job.BOJWindow();
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
            // Revenue Entry
            var page = new Revenue.RevenueDateSelectionPage();
            // setup
            //Models.RevenueEntry entry = new Models.RevenueEntry();
            //page.Setup(Models.Job.FindJob("14077"), entry);
            PageContentManager.Instance.Current = page;
        }

        private void reprintRevSlip_Click(object sender, RoutedEventArgs e)
        {
            var signinWin = new SignInWindow();
            signinWin.Owner = Application.Current.MainWindow;
            signinWin.Setup("SUPERVISOR");
            if (signinWin.ShowDialog() == false)
            {
                return;
            }
            var search = new Windows.Reports.RevenueSlipSearchWindow();
            search.Owner = Application.Current.MainWindow;
            if (search.ShowDialog() == false)
            {
                return;
            }

            var page = new Revenue.RevenueDateSelectionPage();
            PageContentManager.Instance.Current = page;
            /*
            // Revenue Slip Preview
            var page = new Reports.RevenueSlipPreview();
            page.MenuPage = this;
            PageContentManager.Instance.Current = page;
            */
        }

        private void changeShift_Click(object sender, RoutedEventArgs e)
        {
            var signinWin = new DMT.Windows.SignInWindow();
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
            var signinWin = new DMT.Windows.SignInWindow();
            signinWin.Owner = Application.Current.MainWindow;
            signinWin.Setup("SUPERVISOR", "COLLECTOR");
            if (signinWin.ShowDialog() == false)
            {
                return;
            }
            var page = new ReportMenu();
            PageContentManager.Instance.Current = page;
        }

        private void emvQRCode_Click(object sender, RoutedEventArgs e)
        {
            var signinWin = new DMT.Windows.SignInWindow();
            signinWin.Owner = Application.Current.MainWindow;
            signinWin.Setup("SUPERVISOR", "COLLECTOR");
            if (signinWin.ShowDialog() == false)
            {
                return;
            }
            var page = new TollAdmin.EMVQRCodePage();

            /*
            List<Models.EMVQRCode> emvQRs = new List<Models.EMVQRCode>();
            Models.EMVQRCode emvQR;

            emvQR = new Models.EMVQRCode();
            emvQR.Type = "EMV";
            emvQR.DateQR = new DateTime(2020, 3, 16, 18, 50, 11);
            emvQR.StaffId = "14055";
            emvQR.LaneId = 1;
            emvQR.ApprovalCode = "459564";
            emvQR.Qty = 100;
            emvQRs.Add(emvQR);

            emvQR = new Models.EMVQRCode();
            emvQR.Type = "EMV";
            emvQR.DateQR = new DateTime(2020, 3, 16, 23, 15, 24);
            emvQR.StaffId = "14147";
            emvQR.LaneId = 3;
            emvQR.ApprovalCode = "485564";
            emvQR.Qty = 170;
            emvQRs.Add(emvQR);

            emvQR = new Models.EMVQRCode();
            emvQR.Type = "QR Code";
            emvQR.DateQR = new DateTime(2020, 3, 17, 12, 1, 47);
            emvQR.StaffId = "12562";
            emvQR.LaneId = 2;
            emvQR.ApprovalCode = "459564";
            emvQR.Qty = 100;
            emvQRs.Add(emvQR);

            page.Setup(emvQRs);
            */

            PageContentManager.Instance.Current = page;
        }

        private void loginList_Click(object sender, RoutedEventArgs e)
        {
            var signinWin = new DMT.Windows.SignInWindow();
            signinWin.Owner = Application.Current.MainWindow;
            signinWin.Setup("SUPERVISOR", "COLLECTOR");
            if (signinWin.ShowDialog() == false)
            {
                return;
            }
            var page = new DMT.TOD.Pages.Job.LoginListPage();
            PageContentManager.Instance.Current = page;

            /*
            var page = new DMT.Pages.TOD.Job.LoginListPage();
            
            List<Models.Lane> Lanes = new List<Models.Lane>();
            Models.Lane lane;

            lane = new Models.Lane();
            lane.Begin = new DateTime(2020, 3, 16, 18, 50, 11);
            lane.End = new DateTime(2020, 3, 17, 10, 00, 11);
            lane.StaffId = "14055";
            lane.StaffName = "นางวิภา สวัสดิวัฒน์";
            Lanes.Add(lane);

            lane = new Models.Lane();
            lane.Begin = new DateTime(2020, 3, 16, 23, 15, 24);
            lane.End = new DateTime(2020, 3, 17, 08, 00, 11);
            lane.StaffId = "14147";
            lane.StaffName = "นางสาว แก้วใส ฟ้ารุ่งโรจณ์";
            Lanes.Add(lane);

            lane = new Models.Lane();
            lane.Begin = new DateTime(2020, 3, 17, 12, 1, 47);
            lane.End = new DateTime(2020, 3, 17, 08, 30, 00);
            lane.StaffId = "12562";
            lane.StaffName = "นาย ภักดี อมรรุ่งโรจน์";
            Lanes.Add(lane);
           
            page.Setup(Lanes);

            PageContentManager.Instance.Current = page;
            */
        }

        #endregion
    }
}
