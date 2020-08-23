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
using System.ComponentModel;

#endregion

namespace DMT.TA.Pages.Reports
{
    /// <summary>
    /// Interaction logic for CouponSalesReceiptReportPage.xaml
    /// </summary>
    public partial class CouponSalesReceiptReportPage : UserControl
    {
        #region Constructor

        public CouponSalesReceiptReportPage()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private TSBCouponManager _manager = null;
        private TSBCouponSummary _summary = null;

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = (null != this.CallerPage) ? this.CallerPage : new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // print reports.
            this.rptViewer.Print();

            // Main Menu Page
            var page = (null != this.CallerPage) ? this.CallerPage : new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        public ContentControl MenuPage { get; set; }
        public ContentControl CallerPage { get; set; }

        private RdlcReportModel GetReportModel()
        {
            Assembly assembly = this.GetType().Assembly;
            RdlcReportModel inst = new RdlcReportModel();
            inst.Definition.EmbededReportName = "DMT.TA.Pages.Reports.CouponSalesReceiptRep.rdlc";
            inst.Definition.RdlcInstance = RdlcReportUtils.GetEmbededReport(assembly,
                inst.Definition.EmbededReportName);
            // clear reprot datasource.
            inst.DataSources.Clear();

            List<TSBCouponSummary> items = new List<TSBCouponSummary>();
            if (null != _summary) items.Add(_summary);

            // load C35 items.
            List<TSBCouponTransaction> c35Items = new List<TSBCouponTransaction>();
            var c35coupons = (null != _manager)  ? _manager.C35TSBSolds : null;
            if (null != c35coupons) c35Items.AddRange(c35coupons);
            // load C80 items.
            List<TSBCouponTransaction> c80Items = new List<TSBCouponTransaction>();
            var c80coupons = (null != _manager) ? _manager.C80TSBSolds : null;
            if (null != c80coupons) c80Items.AddRange(c80coupons);

            // assign new data source (main for header)
            RdlcReportDataSource mainDS = new RdlcReportDataSource();
            mainDS.Name = "main"; // the datasource name in the rdlc report.
            mainDS.Items = items; // setup data source
            // Add to datasources
            inst.DataSources.Add(mainDS);

            // assign new data source (main for coupon35)
            RdlcReportDataSource c35DS = new RdlcReportDataSource();
            c35DS.Name = "C35"; // the datasource name in the rdlc report.
            c35DS.Items = c35Items; // setup data source
            // Add to datasources
            inst.DataSources.Add(c35DS);

            // assign new data source (main for coupon80)
            RdlcReportDataSource c80DS = new RdlcReportDataSource();
            c80DS.Name = "C80"; // the datasource name in the rdlc report.
            c80DS.Items = c80Items; // setup data source
            // Add to datasources
            inst.DataSources.Add(c80DS);

            // Add parameters (if required).
            // Coupon Received Date.
            DateTime today = DateTime.Today;            
            //string couponDate = today.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
            string couponDate = today.ToThaiDateTimeString("dd/MM/yyyy");
            inst.Parameters.Add(RdlcReportParameter.Create("couponDate", couponDate));
            // Supervisor (Current User)
            string supervisorFullName = DMT.Controls.TAApp.User.Current.FullNameTH;
            inst.Parameters.Add(RdlcReportParameter.Create("supervisorFullName", supervisorFullName));

            return inst;
        }

        public void Setup(TSBCouponManager manager)
        {
            _manager = manager;
            if (null != _manager) 
            {
                var model = GetReportModel();
                if (null == model ||
                    null == model.DataSources || model.DataSources.Count <= 0 ||
                    null == model.DataSources[0] || null == model.DataSources[0].Items)
                {
                    DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                    msg.Owner = Application.Current.MainWindow;
                    msg.Setup("No result found.", "DMT - Toll Admin");
                    if (msg.ShowDialog() == true)
                    {
                        this.rptViewer.ClearReport();
                    }
                }
                else
                {
                    this.rptViewer.LoadReport(model);
                }
            }
        }
    }
}
