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

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private UserShift _userShift = null;
        private Plaza _plaza = null;
        private DateTime _entryDate = DateTime.MinValue;
        private DateTime _revDate = DateTime.MinValue;
        private Models.RevenueEntry _revenueEntry = null;

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Report Page
            var page = (null != this.CallerPage) ? this.CallerPage : new Menu.ReportMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // update save data
            ops.Revenue.SaveRevenue(_revenueEntry);
            // sync key to user shift object.
            _userShift.RevenueDate = _revenueEntry.RevenueDate;
            _userShift.RevenueId = _revenueEntry.RevenueId;

            // get all lanes information.
            var search = Search.Lanes.Attendances.ByUserShift.Create(_userShift, null);
            var laneActivities = ops.Lanes.GetAttendancesByUserShift(search);
            if (null == laneActivities || laneActivities.Count == 0)
            {
                // no lane activitie in user shift.
                ops.Jobs.EndJob(_userShift); // End user job(shift).
            }

            // print reports.
            this.rptViewer.Print();


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

            List<RevenueEntry> items = new List<RevenueEntry>();
            if (null != _revenueEntry) items.Add(_revenueEntry);

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

            return inst;
        }

        public void Setup(UserShift userShift, Plaza plaza,
            DateTime entryDate, DateTime revDate, 
            Models.RevenueEntry revenueEntry)
        {
            _userShift = userShift;
            _plaza = plaza;
            _entryDate = entryDate;
            _revDate = revDate;
            _revenueEntry = revenueEntry;
            if (null == _userShift || null == _plaza || null == _revenueEntry)
            {
                // No data.
            }
            else
            {
                // update object properties.
                _revenueEntry.EntryDate = _entryDate; // assigned Entry date.
                _revenueEntry.RevenueDate = _revDate; // assigned Revenue date.
                _plaza.AssignTo(_revenueEntry); // assigned plaza name (EN/TH)
                _userShift.AssignTo(_revenueEntry); // assigned user full name (EN/TH)

                var model = GetReportModel();
                if (null == model ||
                    null == model.DataSources || model.DataSources.Count <= 0 ||
                    null == model.DataSources[0] || null == model.DataSources[0].Items)
                {
                    MessageBox.Show("No result found.");
                    this.rptViewer.ClearReport();
                }
                else
                {
                    this.rptViewer.LoadReport(model);
                }
                
            }
        }
    }
}
