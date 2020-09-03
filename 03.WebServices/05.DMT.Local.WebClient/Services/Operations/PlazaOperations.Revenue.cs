#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;
using System.Windows.Forms;
using DMT.Models.ExtensionMethods;
using NLib.Reflection;
using System.ComponentModel;

#endregion


namespace DMT.Services
{
    partial class LocalOperations
    {
        #region Internal Variables

        private RevenueOperations _Revenue_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Revenue Operations.
        /// </summary>
        public RevenueOperations Revenue
        {
            get
            {
                if (null == _Revenue_Ops)
                {
                    lock (this)
                    {
                        _Revenue_Ops = new RevenueOperations();
                    }
                }
                return _Revenue_Ops;
            }
        }

        #endregion

        #region Revenue class

        /// <summary>
        /// The Revenue Operations class.
        /// Used for Manage Revenue Entry's operation(s).
        /// </summary>
        public class RevenueOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal RevenueOperations() { }

            #endregion

            #region Public Methods

            #region UserShiftRevenue

            public NRestResult<UserShiftRevenue> CreateRevenueShift(
                Search.Revenues.PlazaShift value)
            {
                NRestResult<UserShiftRevenue> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<UserShiftRevenue>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<UserShiftRevenue>(
                        RouteConsts.Revenue.CreatePlazaRevenue.Url, value);
                }
                else
                {
                    ret = new NRestResult<UserShiftRevenue>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            public NRestResult<UserShiftRevenue> SaveRevenueShift(
                Search.Revenues.SaveRevenueShift value)
            {
                NRestResult<UserShiftRevenue> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<UserShiftRevenue>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<UserShiftRevenue>(
                        RouteConsts.Revenue.SavePlazaRevenue.Url, value);
                }
                else
                {
                    ret = new NRestResult<UserShiftRevenue>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            public NRestResult<UserShiftRevenue> GetRevenueShift(Search.Revenues.PlazaShift value)
            {
                NRestResult<UserShiftRevenue> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<UserShiftRevenue>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<UserShiftRevenue>(
                        RouteConsts.Revenue.GetPlazaRevenue.Url, value);
                }
                else
                {
                    ret = new NRestResult<UserShiftRevenue>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            #endregion

            #region Revenue Entry

            public NRestResult<RevenueEntry> SaveRevenue(RevenueEntry value)
            {
                NRestResult<RevenueEntry> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<RevenueEntry>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<RevenueEntry>(
                        RouteConsts.Revenue.SaveRevenue.Url, value);
                }
                else
                {
                    ret = new NRestResult<RevenueEntry>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            public NRestResult<List<RevenueEntry>> GetRevenues(DateTime value)
            {
                NRestResult<List<RevenueEntry>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<RevenueEntry>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<RevenueEntry>>(
                        RouteConsts.Revenue.GetRevenues.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<RevenueEntry>>();
                    ret.ParameterIsNull();
                    ret.data = new List<RevenueEntry>();
                }
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }

    #region RevenueEntryManager

    /// <summary>
    /// The RevenueEntryManager class.
    /// </summary>
    public class RevenueEntryManager
    {
        #region Internal Variables

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private SCWOperations server = SCWServiceOperations.Instance.Plaza;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueEntryManager() : base()
        {
            this.EntryDate = DateTime.Now;
            LoadPlazaGroups();
        }

        #endregion

        #region Private Methods

        private void LoadPlazaGroups()
        {
            var tsb = ops.TSB.GetCurrent().Value();
            if (null != tsb)
            {
                this.PlazaGroups = ops.TSB.GetTSBPlazaGroups(tsb).Value();
            }
        }

        private void SyncJobList()
        {
            // Sync JobList to LaneAttendance
            if (null == this.User) return;
            if (null == this.PlazaGroup) return;
            var plazas = ops.TSB.GetPlazaGroupPlazas(this.PlazaGroup).Value();
            if (null == plazas) return;

            var attends = new List<LaneAttendance>();

            plazas.ForEach(plaza => 
            {
                // Gets Job List from WS.
                int nwId = 31;
                int plazaId = Convert.ToInt32(plaza.PlazaId);
                var ret = server.TOD.GetJobList(nwId, plazaId, this.User.UserId);
                if (null != ret && null != ret.list)
                {
                    ret.list.ForEach(inst =>
                    {
                        var attend = inst.ToLocal();
                        var lane = ops.TSB.GetPlazaLane(
                            Search.Plaza.LaneByNo.Create(attend.PlazaId, attend.LaneNo)).Value();
                        if (null != lane) lane.AssignTo(attend);

                        var user = ops.Users.GetById(
                            Search.Users.ById.Create(attend.UserId, "CTC", "TC")).Value();
                        if (null != user) user.AssignTo(attend);

                        if (null != attend) attends.Add(attend);
                    });
                }
            });

            // Save (insert/update) all rows.
            ops.Lanes.SaveAttendances(attends);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Refresh.
        /// </summary>
        public void Refresh()
        {
            if (null == this.User) return;
            // Sync JobList to LaneAttendance
            SyncJobList();
            // Find user shift.
            this.UserShift = ops.UserShifts.GetCurrent(this.User).Value();
            if (null != UserShift)
            {
                this.RevenueDate = UserShift.Begin.Date;
            }
        }

        public void RefreshJobs()
        {
            if (null != this.UserShift && null != this.PlazaGroup)
            {
                // get all lanes information.
                var search = Search.Lanes.Attendances.ByUserShift.Create(
                    this.UserShift, this.PlazaGroup, DateTime.MinValue);
                this.Attendances = ops.Lanes.GetAttendancesByUserShift(search).Value();
            }
        }

        private void CreateLaneList()
        {
            // create lane list.
            this.Lanes = new List<int>();
            if (null != this.Attendances)
            {
                this.Attendances.ForEach(laneAct =>
                {
                    if (!this.Lanes.Contains(laneAct.LaneNo))
                    {
                        // add to list
                        this.Lanes.Add(laneAct.LaneNo);
                    }
                });
            }
            // Build Lane List String.
            int iCnt = 0;
            int iMax = this.Lanes.Count;
            string laneList = string.Empty;
            this.Lanes.ForEach(laneNo =>
            {
                laneList += laneNo.ToString();
                if (iCnt < iMax - 1) laneList += ", ";
                iCnt++;
            });
            this.LaneList = laneList;
        }

        public void LoadRevenueShift()
        {
            this.IsNewRevenueShift = false;
            var revops = Search.Revenues.PlazaShift.Create(this.UserShift, this.PlazaGroup);
            this.RevenueShift = ops.Revenue.GetRevenueShift(revops).Value();
            if (null == this.RevenueShift)
            {
                // Create new if not found.
                this.RevenueShift = ops.Revenue.CreateRevenueShift(revops).Value();
                this.IsNewRevenueShift = true;
            }
        }

        public void BuildRevenueEntry()
        {
            if (null == this.UserShift || null == this.PlazaGroup)
            {
                return;
            }
            var search = Search.UserCredits.GetActiveById.Create(
                this.UserShift.UserId, this.PlazaGroup.PlazaGroupId);
            var userCredit = ops.Credits.GetActiveUserCreditBalanceById(search).Value();

            this.RevenueEntry = new Models.RevenueEntry();
            
            if (null != userCredit)
            {
                this.RevenueEntry.BagNo = userCredit.BagNo;
                this.RevenueEntry.BeltNo = userCredit.BeltNo;
            }
            else
            {
                this.RevenueEntry.BagNo = string.Empty;
                this.RevenueEntry.BeltNo = string.Empty;
            }

            CreateLaneList();

            // assigned plaza group.
            this.RevenueEntry.PlazaGroupId = this.PlazaGroup.PlazaGroupId;
            // update object properties.
            this.PlazaGroup.AssignTo(this.RevenueEntry); // assigned plaza group name (EN/TH)
            this.UserShift.AssignTo(this.RevenueEntry); // assigned user full name (EN/TH)

            // assigned date after sync object(s) to RevenueEntry.
            this.RevenueEntry.EntryDate = this.EntryDate; // assigned Entry date.
            this.RevenueEntry.RevenueDate = this.RevenueDate; // assigned Revenue date.
            this.RevenueEntry.Lanes = this.LaneList.Trim();

            // Find begin/end of revenue.
            DateTime begin = DateTime.MinValue;
            DateTime end = DateTime.MinValue;

            if (begin == DateTime.MinValue)
            {
                begin = this.UserShift.Begin; // Begin time used start of shift.
            }
            if (end == DateTime.MinValue)
            {
                end = DateTime.Now; // End time used printed date
            }

            if (this.RevenueEntry.ShiftBegin == DateTime.MinValue)
            {
                this.RevenueEntry.ShiftBegin = begin;
            }
            if (this.RevenueEntry.ShiftEnd == DateTime.MinValue)
            {
                this.RevenueEntry.ShiftEnd = end;
            }
            // assign supervisor.
            var sup = ops.Shifts.GetCurrent().Value();
            if (null != sup)
            {
                this.RevenueEntry.SupervisorId = sup.UserId;
                this.RevenueEntry.SupervisorNameEN = sup.FullNameEN;
                this.RevenueEntry.SupervisorNameTH = sup.FullNameTH;
            }
        }

        public bool SaveRevenueEntry()
        {
            if (null == this.RevenueEntry || null == this.UserShift)
                return false;

            // update save data
            var revInst = ops.Revenue.SaveRevenue(this.RevenueEntry).Value();
            string revId = (null != revInst) ? revInst.RevenueId : string.Empty;
            if (null != this.RevenueShift)
            {
                // save revenue shift (for plaza)
                var saveOpt = Search.Revenues.SaveRevenueShift.Create(this.RevenueShift,
                    revId, this.RevenueDate);
                ops.Revenue.SaveRevenueShift(saveOpt);
            }
            // sync key to lane attendance list.
            if (null != this.Attendances)
            {
                this.Attendances.ForEach(lane =>
                {
                    lane.RevenueDate = this.RevenueDate;
                    lane.RevenueId = revId;
                    ops.Lanes.SaveAttendance(lane);
                });
            }
            // get all lanes information.
            var search = Search.Lanes.Attendances.ByUserShift.Create(
                this.UserShift, null, DateTime.MinValue);
            var existActivities = ops.Lanes.GetAttendancesByUserShift(search).Value();
            if (null == existActivities || existActivities.Count == 0)
            {
                // no lane activitie in user shift.
                ops.UserShifts.EndUserShift(this.UserShift); // End user job(shift).

                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Entry Date.
        /// </summary>
        public DateTime EntryDate { get; internal set; }
        /// <summary>
        /// Gets or sets Revenue Date.
        /// </summary>
        public DateTime RevenueDate { get; set; }
        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Gets avaliable plaza groups.
        /// </summary>
        public List<PlazaGroup> PlazaGroups { get; internal set; }
        /// <summary>
        /// Gets or sets plaza group.
        /// </summary>
        public PlazaGroup PlazaGroup { get; set; }
        /// <summary>
        /// Gets related user shift.
        /// </summary>
        public UserShift UserShift { get; internal set; }
        /// <summary>
        /// Gets related LaneAttendance list.
        /// </summary>
        public List<LaneAttendance> Attendances { get; internal set; }
        /// <summary>
        /// Checks has lane attendance.
        /// </summary>
        public bool HasAttendance
        {
            get { return (null != Attendances && Attendances.Count > 0); }
        }
        /// <summary>
        /// Gets Lane Number List.
        /// </summary>
        public List<int> Lanes { get; internal set; }
        /// <summary>
        /// Gets Lane List string.
        /// </summary>
        public string LaneList { get; internal set; }
        /// <summary>
        /// Gets related user revenue's shift.
        /// </summary>
        public UserShiftRevenue RevenueShift { get; internal set; }
        /// <summary>
        /// Gets is new Revenue Shift.
        /// </summary>
        public bool IsNewRevenueShift { get; internal set; }
        /// <summary>
        /// Checks the current revenue shift is already has revenue entry.
        /// </summary>
        public bool HasRevenuShift
        {
            get
            {
                return (null != this.RevenueShift &&
                    this.RevenueShift.RevenueDate != DateTime.MinValue);
            }
        }
        /// <summary>
        /// Checks has one or more lane attendance record that not enter revenue entry,
        /// </summary>
        public bool HasIncompletedLanes
        {
            get
            {
                return (null != this.Attendances && this.Attendances.Count > 0);
            }
        }
        /// <summary>
        /// Gets related Revenue Entry.
        /// </summary>
        public RevenueEntry RevenueEntry { get; internal set; }
        /// <summary>
        /// Checks is New Revenue Entry.
        /// </summary>
        public bool IsNewRevenueEntry
        {
            get 
            {
                return (null == this.RevenueEntry ||
                    this.RevenueEntry.RevenueId == string.Empty ||
                    this.RevenueEntry.EntryDate == DateTime.MinValue ||
                    this.RevenueEntry.RevenueDate == DateTime.MinValue);
            }
        }
        /// <summary>
        /// Checks all information to build report is loaded.
        /// </summary>
        public bool CanBuildReport
        {
            get
            {
                return (null != this.UserShift &&
                    null != this.PlazaGroup &&
                    null != this.RevenueShift &&
                    null != this.Attendances &&
                    null != this.RevenueEntry);
            }
        }

        #endregion
    }

    #endregion
}
