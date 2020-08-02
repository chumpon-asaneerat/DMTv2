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
    /// Interaction logic for RevenueSlipPreviewNull.xaml
    /// </summary>
    public partial class RevenueSlipPreviewNull : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueSlipPreviewNull()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

        private User _user = null;
        private UserShift _userShift = null;
        private PlazaGroup _plazaGroup = null;
        private UserShiftRevenue _plazaRevenue = null;
        private List<LaneAttendance> _laneActivities = null;


        private DateTime _entryDate = DateTime.MinValue;
        private DateTime _revDate = DateTime.MinValue;

        private Models.RevenueEntry _revenueEntry = null;

#pragma warning disable IDE0052 // Remove unread private members
        private bool isNew = false;
#pragma warning restore IDE0052 // Remove unread private members

        #region Button Handlers

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            if (null == _revenueEntry)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("Revenue Entry is not found.", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    return;
                }

                //MessageBox.Show("Revenue Entry is not found.",
                //    "DMT - Tour of Duty");
                //return;
            }

            this.rptViewer.Print();
            GoMainMenu();

            #region Old
            //if (isNew)
            //{
            //    bool hasActivitied = SaveRevenueEntry();

            //    if (_revenueEntry.RevenueDate != DateTime.MinValue &&
            //        _revenueEntry.EntryDate != DateTime.MinValue)
            //    {
            //        // print reports only date exists.
            //        this.rptViewer.Print();
            //    }

            //    if (!hasActivitied || null == _user)
            //    {
            //        GoMainMenu();
            //        return;
            //    }
            //    if (MessageBox.Show(Application.Current.MainWindow,
            //        "กะปัจจุบันยังป้อนรายได้ไม่ครับ ต้องการป้อนรายได้ตอหรือไม่ ?",
            //        "DMT - Tour of Duty", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //    {
            //        GoRevenuEntry();
            //    }
            //    else
            //    {
            //        GoMainMenu();
            //    }
            //}
            //else
            //{
            //    // print reports.
            //    this.rptViewer.Print();
            //    GoMainMenu();
            //}
            #endregion
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Report Page
            var page = (null != this.CallerPage) ? this.CallerPage : new Menu.ReportMenu();
            PageContentManager.Instance.Current = page;
        }

        private void GoRevenuEntry()
        {
            // Revenue Entry Page
            var page = new Revenue.RevenueDateSelectionPage();
            // setup
            page.Setup(_user);
            PageContentManager.Instance.Current = page;
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
            inst.Definition.EmbededReportName = "DMT.TOD.Pages.Reports.RevenueSlipNull.rdlc";
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
                var sup = ops.Shifts.GetCurrent();
                _revenueEntry.SupervisorId = sup.UserId;
                _revenueEntry.SupervisorNameEN = sup.FullNameEN;
                _revenueEntry.SupervisorNameTH = sup.FullNameTH;
            }
        }

        private bool SaveRevenueEntry()
        {
            // Save information if is new entry.

            if (_revenueEntry.RevenueDate == DateTime.MinValue ||
                _revenueEntry.EntryDate == DateTime.MinValue)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("Entry Date or Revenue Date is not set.", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    return false;
                }

                //MessageBox.Show("Entry Date or Revenue Date is not set.",
                //    "DMT - Tour of Duty");
                //return false;
            }

            // update save data
            string revId = ops.Revenue.SaveRevenue(_revenueEntry);
            if (null != _plazaRevenue)
            {
                // save revenue shift (for plaza)
                var saveOpt = Search.Revenues.SaveRevenueShift.Create(_plazaRevenue,
                    revId, _revenueEntry.RevenueDate);
                ops.Revenue.SaveRevenueShift(saveOpt);
            }
            // sync key to lane attendance list.
            if (null != _laneActivities)
            {
                _laneActivities.ForEach(lane =>
                {
                    lane.RevenueDate = _revenueEntry.RevenueDate;
                    lane.RevenueId = revId;
                    ops.Lanes.SaveAttendance(lane);
                });
            }

            // get all lanes information.
            var search = Search.Lanes.Attendances.ByUserShift.Create(_userShift, null, DateTime.MinValue);
            var existActivities = ops.Lanes.GetAttendancesByUserShift(search);
            if (null == existActivities || existActivities.Count == 0)
            {
                // no lane activitie in user shift.
                ops.UserShifts.EndUserShift(_userShift); // End user job(shift).

                return false;
            }
            else 
            {
                return true;
            }
        }

        public void Setup(User user,Models.RevenueEntry revenueEntry)
        {
            _user = user;

            _revenueEntry = revenueEntry;

            if (null != _revenueEntry)
            {
                if (_revenueEntry.RevenueId == string.Empty ||
                    _revenueEntry.EntryDate == DateTime.MinValue ||
                    _revenueEntry.RevenueDate == DateTime.MinValue)
                {
                    isNew = true;
                    InitNewReport();
                }
                else
                {
                    isNew = false;
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

                //MessageBox.Show("No result found.", "DMT - Tour of Duty");
                //this.rptViewer.ClearReport();
            }
            else
            {
                this.rptViewer.LoadReport(model);
            }
        }
    }
}
