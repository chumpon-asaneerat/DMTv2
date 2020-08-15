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


namespace DMT.TA.Windows.Coupon
{
    /// <summary>
    /// Interaction logic for CouponReportWindow.xaml
    /// </summary>
    public partial class CouponReportWindow : Window
    {
        public CouponReportWindow()
        {
            InitializeComponent();
        }

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        private User _user = null;
        private UserShift _userShift = null;
        private PlazaGroup _plazaGroup = null;
        private UserShiftRevenue _plazaRevenue = null;
        private List<LaneAttendance> _laneActivities = null;


        private DateTime _entryDate = DateTime.MinValue;
        private DateTime _revDate = DateTime.MinValue;

        private Models.RevenueEntry _revenueEntry = null;

        #region Button Handlers

        private void cmdSaveExchange_Click(object sender, RoutedEventArgs e)
        {
            this.rptViewer.Print();
            GoMainMenu();

            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        private void GoMainMenu()
        {
            // Main Report Page
            var page = (null != this.MenuPage) ? this.MenuPage : new DMT.TA.Pages.Menu.ReportMenu();
            PageContentManager.Instance.Current = page;
        }


        public ContentControl MenuPage { get; set; }
        public ContentControl CallerPage { get; set; }

        private RdlcReportModel GetReportModel()
        {
            Assembly assembly = this.GetType().Assembly;
            RdlcReportModel inst = new RdlcReportModel();
            inst.Definition.EmbededReportName = "DMT.TA.Pages.Reports.CouponRep.rdlc";
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

        private void InitNewReport()
        {
            if (null == _userShift || null == _plazaGroup || null == _plazaRevenue ||
                null == _revenueEntry ||
                null == _laneActivities || _laneActivities.Count <= 0)
            {
                // some of parameter(s) is null.
                Console.WriteLine("some of parameter(s) is null.");
            }
            else
            {
                // Find begin/end of revenue.
                DateTime begin = DateTime.MinValue;
                DateTime end = DateTime.MinValue;

                // create lane list.
                List<int> lanes = new List<int>();
                _laneActivities.ForEach(laneAct =>
                {
                    if (begin == DateTime.MinValue || laneAct.Begin < begin)
                    {
                        begin = laneAct.Begin;
                    }
                    if (end == DateTime.MinValue || laneAct.End > end)
                    {
                        end = laneAct.End;
                    }

                    if (!lanes.Contains(laneAct.LaneNo))
                    {
                        lanes.Add(laneAct.LaneNo);
                    }
                });
                int iCnt = 0;
                int iMax = lanes.Count;
                string laneList = string.Empty;
                lanes.ForEach(laneNo =>
                {
                    laneList += laneNo.ToString();
                    if (iCnt < iMax - 1) laneList += ", ";
                    iCnt++;
                });

                // update object properties.
                _plazaGroup.AssignTo(_revenueEntry); // assigned plaza group name (EN/TH)
                _userShift.AssignTo(_revenueEntry); // assigned user full name (EN/TH)

                // assigned date after sync object(s) to RevenueEntry.
                _revenueEntry.EntryDate = _entryDate; // assigned Entry date.
                _revenueEntry.RevenueDate = _revDate; // assigned Revenue date.

                _revenueEntry.Lanes = laneList.Trim();
                _revenueEntry.ShiftBegin = begin;
                _revenueEntry.ShiftEnd = end;

                // assign supervisor.
                var sup = ops.Shifts.GetCurrent().Value();
                _revenueEntry.SupervisorId = sup.UserId;
                _revenueEntry.SupervisorNameEN = sup.FullNameEN;
                _revenueEntry.SupervisorNameTH = sup.FullNameTH;
            }
        }


        public void Setup(User user, UserShift userShift, PlazaGroup plazaGroup,
            UserShiftRevenue plazaRevenue,
            List<LaneAttendance> laneActivities,
            DateTime entryDate, DateTime revDate,
            Models.RevenueEntry revenueEntry)
        {
            _user = user;
            _userShift = userShift;
            _plazaGroup = plazaGroup;
            _plazaRevenue = plazaRevenue;
            _laneActivities = laneActivities;
            _entryDate = entryDate;
            _revDate = revDate;
            _revenueEntry = revenueEntry;

            if (null != _revenueEntry)
            {
                if (_revenueEntry.RevenueId == string.Empty ||
                    _revenueEntry.EntryDate == DateTime.MinValue ||
                    _revenueEntry.RevenueDate == DateTime.MinValue)
                {
                    InitNewReport();
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
        }
    }
}
