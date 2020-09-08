#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;
using NLib.Reports.Rdlc;
using System.Reflection;

#endregion

namespace DMT.TOD.Pages.Reports
{
    /// <summary>
    /// Interaction logic for RevenueSlipPreview.xaml
    /// </summary>
    public partial class RevenueSlipPreview : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueSlipPreview()
        {
            InitializeComponent();
        }

        #endregion

        private RevenueEntryManager _manager = new RevenueEntryManager();

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Report Page
            var page = (null != this.CallerPage) ? this.CallerPage : new Menu.ReportMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (null == _manager || null == _manager.RevenueEntry)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("Revenue Entry is not found.", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    return;
                }
            }

            if (_manager.IsNewRevenueEntry)
            {
                bool hasActivitied = SaveRevenueEntry();
                
                if (_manager.RevenueEntry.RevenueDate != DateTime.MinValue &&
                    _manager.RevenueEntry.EntryDate != DateTime.MinValue)
                {
                    // print reports only date exists.
                    this.rptViewer.Print();
                }

                if (!hasActivitied || null == _manager.User)
                {
                    GoMainMenu();
                    return;
                }

                DMT.Windows.MessageBoxYesNoWindow msg = new DMT.Windows.MessageBoxYesNoWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("กะปัจจุบันยังป้อนรายได้ไม่ครบ ต้องการป้อนรายได้ต่อหรือไม่ ?", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    GoRevenuEntry();
                }
                else
                {
                    GoMainMenu();
                }
            }
            else
            {
                // print reports.
                this.rptViewer.Print();
                GoMainMenu();
            }
            */
        }

        private void GoRevenuEntry()
        {
            if (null == _manager || null == _manager.User)
            {
                GoMainMenu();
            }
            else
            {
                // Revenue Entry Page
                var page = new Revenue.RevenueDateSelectionPage();
                // setup
                page.Setup(_manager.User);
                PageContentManager.Instance.Current = page;
            }
        }

        private void GoMainMenu()
        {
            // Main Report Page
            var page = (null != this.MenuPage) ? this.MenuPage : new Menu.ReportMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        public ContentControl MenuPage { get; set; }
        public ContentControl CallerPage { get; set; }

        private RdlcReportModel GetReportModel()
        {
            Assembly assembly = this.GetType().Assembly;
            RdlcReportModel inst = new RdlcReportModel();
            inst.Definition.EmbededReportName = "DMT.TOD.Pages.Reports.RevenueSlip.rdlc";
            inst.Definition.RdlcInstance = RdlcReportUtils.GetEmbededReport(assembly, 
                inst.Definition.EmbededReportName);
            // clear reprot datasource.
            inst.DataSources.Clear();
            /*
            List<RevenueEntry> items = new List<RevenueEntry>();
            if (null != _manager && null != _manager.RevenueEntry)
            {
                items.Add(_manager.RevenueEntry);
            }

            // assign new data source
            RdlcReportDataSource mainDS = new RdlcReportDataSource();
            mainDS.Name = "main"; // the datasource name in the rdlc report.
            mainDS.Items = items; // setup data source
            // Add to datasources
            inst.DataSources.Add(mainDS);

            // Add parameters (if required).
            DateTime today = DateTime.Now;
            string printDate = today.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
            inst.Parameters.Add(RdlcReportParameter.Create("PrintDate", printDate));
            */
            return inst;
        }

        private void InitNewReport()
        {
            /*
            if (null == _manager || !_manager.CanBuildReport)
            {
                // some of parameter(s) is null.
                Console.WriteLine("some of parameter(s) is null.");
            }
            else
            {
                _manager.BuildRevenueEntry();
            }
            */
        }

        private bool SaveRevenueEntry()
        {
            return false;
            /*
            if (null == _manager ||
                _manager.RevenueEntry.RevenueDate == DateTime.MinValue ||
                _manager.RevenueEntry.EntryDate == DateTime.MinValue)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("Entry Date or Revenue Date is not set.", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    return false;
                }
            }
            // Save information.
            return _manager.SaveRevenueEntry();
            */
        }

        public void Setup(RevenueEntryManager manager)
        {
            /*
            _manager = manager;

            if (null != _manager && null != _manager.RevenueEntry)
            {
                if (_manager.IsNewRevenueEntry)
                {
                    InitNewReport();

                    txtOK.Text = "ยืนยัน นำส่งรายได้";
                    txtCancel.Text = "แก้ไข";
                }
                else
                {
                    txtOK.Text = "พิมพ์";
                    txtCancel.Text = "ยกเลิก";
                }
            }

            var model = GetReportModel();
            if (null == model ||
                null == model.DataSources || model.DataSources.Count <= 0 ||
                null == model.DataSources[0] || null == model.DataSources[0].Items)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("No result found.", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    this.rptViewer.ClearReport();
                }
            }
            else
            {
                this.rptViewer.LoadReport(model);
            }
            */
        }
    }
}
