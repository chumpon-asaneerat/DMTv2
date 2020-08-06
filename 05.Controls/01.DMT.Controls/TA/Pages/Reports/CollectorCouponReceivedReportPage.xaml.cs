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
    /// Interaction logic for CollectorCouponReceivedReportPage.xaml
    /// </summary>
    public partial class CollectorCouponReceivedReportPage : UserControl
    {
        #region Constructor

        public CollectorCouponReceivedReportPage()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            // print reports.
            this.rptViewer.Print();

            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        #endregion

        private RdlcReportModel GetReportModel()
        {
            Assembly assembly = this.GetType().Assembly;
            RdlcReportModel inst = new RdlcReportModel();
            inst.Definition.EmbededReportName = "DMT.TOD.Pages.Reports.CollectorCouponReceived.rdlc";
            inst.Definition.RdlcInstance = RdlcReportUtils.GetEmbededReport(assembly,
                inst.Definition.EmbededReportName);
            // clear reprot datasource.
            inst.DataSources.Clear();
            /*
            List<RevenueEntry> items = new List<RevenueEntry>();
            if (null != _revenues) items.AddRange(_revenues);

            // assign new data source
            RdlcReportDataSource mainDS = new RdlcReportDataSource();
            mainDS.Name = "main"; // the datasource name in the rdlc report.
            mainDS.Items = items; // setup data source
            // Add to datasources
            inst.DataSources.Add(mainDS);

            // Add parameters (if required).
            //DateTime today = DateTime.Now;
            //string printDate = today.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
            //inst.Parameters.Add(RdlcReportParameter.Create("PrintDate", printDate));
            */
            return inst;
        }

        public void Setup(List<Models.RevenueEntry> revenues)
        {
            /*
            _revenues = revenues;
            if (null != _revenues)
            {
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

                    //MessageBox.Show("No result found.", "DMT - Tour of Duty");
                    //this.rptViewer.ClearReport();
                }
                else
                {
                    this.rptViewer.LoadReport(model);
                }
            }
            */
        }
    }
}
