﻿#region Using

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
    /// Interaction logic for CollectorFundSummaryReportPage.xaml
    /// </summary>
    public partial class CollectorFundSummaryReportPage : UserControl
    {
        #region Constructor

        public CollectorFundSummaryReportPage()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private TSBCouponManager manager = new TSBCouponManager();
        private User _user = null;
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
            inst.Definition.EmbededReportName = "DMT.TA.Pages.Reports.CollectorFundSummaryRep.rdlc";
            inst.Definition.RdlcInstance = RdlcReportUtils.GetEmbededReport(assembly,
                inst.Definition.EmbededReportName);
            // clear reprot datasource.
            inst.DataSources.Clear();

            List<TSBCouponSummary> items = new List<TSBCouponSummary>();
            if (null != _summary) items.Add(_summary);

            // gets coupon list by type.
            manager.User = _user;
            manager.Refresh(); // reload data.

            // assign new data source (main for header)
            RdlcReportDataSource mainDS = new RdlcReportDataSource();
            mainDS.Name = "main"; // the datasource name in the rdlc report.
            mainDS.Items = items; // setup data source
            // Add to datasources
            inst.DataSources.Add(mainDS);

            return inst;
        }

        public void Setup(User user)
        {
            _user = user;

            if (null != _user) 
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
                }
                else
                {
                    this.rptViewer.LoadReport(model);
                }
            }
        }
    }
}
