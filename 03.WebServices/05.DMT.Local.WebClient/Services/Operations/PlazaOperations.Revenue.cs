﻿#region Usings

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
using System.Reflection;
using NLib;

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

            public NRestResult<List<RevenueEntry>> GetUnsendRevenues()
            {
                NRestResult<List<RevenueEntry>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<RevenueEntry>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<List<RevenueEntry>>(
                    RouteConsts.Revenue.GetUnsendRevenues.Url, null);

                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }

    #region RevenueEntryManager2 - unused

    /// <summary>
    /// The RevenueEntryManager2 class.
    /// </summary>
    public class RevenueEntryManager2
    {
        #region Internal Variables

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private SCWOperations server = SCWServiceOperations.Instance.Plaza;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueEntryManager2() : base()
        {
            // TODO: Need user/password from config table or external file.

            SCWServiceOperations.Instance.UserName = "DMTUSER";
            SCWServiceOperations.Instance.Password = "DMTPASS";

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
            this.PlazaIds = new List<int>();

            plazas.ForEach(plaza => 
            {
                // Gets Job List from WS.
                int nwId = 31; // TODO: network id required.

                int plazaId = Convert.ToInt32(plaza.PlazaId);
                // keep plaza Id.
                if (!this.PlazaIds.Contains(plazaId)) this.PlazaIds.Add(plazaId);

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
                // Sync JobList to LaneAttendance
                SyncJobList();

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

        public void NewRevenueEntry()
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
        }

        public void BuildRevenueEntry()
        {
            if (null == this.UserShift || null == this.PlazaGroup || null == this.RevenueEntry)
            {
                return;
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
            if (null != this.Supervisor)
            {
                this.RevenueEntry.SupervisorId = this.Supervisor.UserId;
                this.RevenueEntry.SupervisorNameEN = this.Supervisor.FullNameEN;
                this.RevenueEntry.SupervisorNameTH = this.Supervisor.FullNameTH;
            }
        }

        public bool SaveRevenueEntry()
        {
            if (null == this.RevenueEntry || null == this.UserShift)
                return false;

            MethodBase med = MethodBase.GetCurrentMethod();

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

            // Need to sync currency and coupon master!!
            var currencies = ops.Master.GetCurrencies().Value();
            var coupons = ops.Master.GetCoupons().Value();

            // Plaza Id send only first match the SCW server will check later.
            SCWDeclare declare = this.RevenueEntry.ToServer(currencies, coupons, 
                this.Attendances, this.PlazaIds[0]);
            med.Info("declare - ");
            //med.Info(declare.ToJson(true));
            var ret = server.TOD.Declare(declare);

            bool sendSucces = false;
            if (null != ret && null != ret.status)
            {
                sendSucces = (ret.status.code != "S200");
                // write log.
                med.Info("declare - code: {0}, msg: {1}", ret.status.code, ret.status.message);
            }
            else 
            {
                // send failed.
                med.Info("declare - code: {0}, msg: {1}", ret.status.code, ret.status.message);
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
        /// Gets or sets Chief/Supervisor.
        /// </summary>
        public User Supervisor { get; set; }
        /// <summary>
        /// Gets avaliable plaza groups.
        /// </summary>
        public List<PlazaGroup> PlazaGroups { get; internal set; }
        /// <summary>
        /// Gets or sets plaza group.
        /// </summary>
        public PlazaGroup PlazaGroup { get; set; }
        /// <summary>
        /// Gets Plaza Id List.
        /// </summary>
        public List<int> PlazaIds { get; internal set; }
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
            // TODO: Need user/password from config table or external file.
            SCWServiceOperations.Instance.UserName = "DMTUSER";
            SCWServiceOperations.Instance.Password = "DMTPASS";

            this.EntryDate = DateTime.Now;
        }

        #endregion

        #region Private Methods

        private void SyncJobList()
        {
            // Sync JobList to LaneAttendance
            if (null == this.User) return;
            if (null == this.PlazaGroup) return;
            var plazas = ops.TSB.GetPlazaGroupPlazas(this.PlazaGroup).Value();
            if (null == plazas) return;

            var attends = new List<LaneAttendance>();
            this.PlazaIds = new List<int>();

            plazas.ForEach(plaza =>
            {
                // Gets Job List from WS.
                int nwId = 31; // TODO: network id required.
                int plazaId = Convert.ToInt32(plaza.PlazaId);
                // keep plaza Id.
                if (!this.PlazaIds.Contains(plazaId)) this.PlazaIds.Add(plazaId);

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

        #endregion

        #region Public Methods

        /// <summary>
        /// Check User Shift and Revenue Date.
        /// </summary>
        public void CheckUserShift()
        {
            if (null == this.User) return;
            // Find user shift.
            MethodBase med = MethodBase.GetCurrentMethod();

            this.UserShift = ops.UserShifts.GetCurrent(this.User).Value();
            if (null != UserShift)
            {
                string msg = string.Format("User Shift found. Begin: {0}, End {1}",
                    UserShift.Begin.ToDateTimeString(), UserShift.End.ToDateTimeString());
                med.Info(msg);

                this.RevenueDate = UserShift.Begin.Date;
            }
            else
            {
                string msg = "User Shift not found.";
                med.Info(msg);
            }
        }
        /// <summary>
        /// Refresh User Job List.
        /// </summary>
        public void RefreshJobs()
        {
            if (null != this.UserShift && null != this.PlazaGroup)
            {
                MethodBase med = MethodBase.GetCurrentMethod();
                // Sync JobList to LaneAttendance
                SyncJobList();
                // Get all lanes information.
                var search = Search.Lanes.Attendances.ByUserShift.Create(
                    this.UserShift, this.PlazaGroup, DateTime.MinValue);
                this.Attendances = ops.Lanes.GetAttendancesByUserShift(search).Value();
                if (!HasAttendance)
                {
                    string msg = "Attendances is null or no lane attendance list.";
                    med.Info(msg);
                }
                else
                {
                    string msg = string.Format("Attendances found : No of lanes: {0}", this.Attendances.Count);
                    med.Info(msg);
                }
            }
        }

        public void CheckRevenueShift()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            this.IsNewRevenueShift = false;
            var revops = Search.Revenues.PlazaShift.Create(this.UserShift, this.PlazaGroup);
            this.RevenueShift = ops.Revenue.GetRevenueShift(revops).Value();

            if (null == this.RevenueShift)
            {
                string msg = "User Revenue Shift not found. Create New!!.";
                med.Info(msg);

                // Create new if not found.
                this.RevenueShift = ops.Revenue.CreateRevenueShift(revops).Value();
                this.IsNewRevenueShift = true;
            }
            else
            {
                string msg = "User Revenue Shift found.";
                med.Info(msg);
            }
        }
        /// <summary>
        /// Create New Revenue Entry and assigned BagNo, BeltNo.
        /// </summary>
        public void NewRevenueEntry()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            if (null == this.UserShift || null == this.PlazaGroup)
            {
                string msg = "PlazaGroup or User Shift is null.";
                med.Info(msg);
                return;
            }
            var search = Search.UserCredits.GetActiveById.Create(
                this.UserShift.UserId, this.PlazaGroup.PlazaGroupId);
            var userCredit = ops.Credits.GetNoRevenueEntryUserCreditBalanceById(search).Value();

            this.RevenueEntry = new Models.RevenueEntry();

            if (null != userCredit)
            {
                string msg = string.Format("User Credit found. BagNo: {0}, BeltNo: {1}",
                    userCredit.BagNo, userCredit.BeltNo);
                med.Info(msg);

                this.RevenueEntry.BagNo = userCredit.BagNo;
                this.RevenueEntry.BeltNo = userCredit.BeltNo;
            }
            else
            {
                string msg = "User Credit not found.";
                med.Info(msg);

                this.RevenueEntry.BagNo = string.Empty;
                this.RevenueEntry.BeltNo = string.Empty;
            }

            if (this.RevenueEntry.RevenueId == string.Empty)
            {
                // TODO: Autogenerate need to change to auto running number
                Random rand = new Random();
                if (string.IsNullOrWhiteSpace(this.RevenueEntry.RevenueId))
                {
                    this.RevenueEntry.RevenueId = rand.Next(100000).ToString("D5"); // auto generate.
                }
            }
        }
        /// <summary>
        /// Load Exists Revenue Entry.
        /// </summary>
        /// <param name="entry"></param>
        public void LoadRevenueEntry(RevenueEntry entry)
        {
            if (null == entry) return;
            this.RevenueEntry = entry;
        }

        public void BuildRevenueEntry()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            if (null == this.UserShift || null == this.PlazaGroup || null == this.RevenueEntry)
            {
                string msg = "PlazaGroup or User Shift or Revenue Entry is null.";
                med.Info(msg);
                return;
            }

            CreateLaneList();

            // assigned plaza group.
            this.RevenueEntry.PlazaGroupId = this.PlazaGroup.PlazaGroupId;
            // update object properties.
            this.PlazaGroup.AssignTo(this.RevenueEntry); // assigned plaza group name (EN/TH)
            this.UserShift.AssignTo(this.RevenueEntry); // assigned user shift

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

            if (null != this.User)
            {
                this.RevenueEntry.CollectorNameEN = this.User.FullNameEN;
                this.RevenueEntry.CollectorNameTH = this.User.FullNameTH;
            }

            if (null != this.Supervisor)
            {
                this.RevenueEntry.SupervisorId = this.Supervisor.UserId;
                this.RevenueEntry.SupervisorNameEN = this.Supervisor.FullNameEN;
                this.RevenueEntry.SupervisorNameTH = this.Supervisor.FullNameTH;
            }
        }
        /// <summary>
        /// Save Revenue Entry.
        /// </summary>
        /// <returns>Returns true if Save and all lane completed and User Shift closed.</returns>
        public bool SaveRevenueEntry()
        {
            if (null == this.RevenueEntry || null == this.UserShift)
                return false;

            MethodBase med = MethodBase.GetCurrentMethod();

            if (this.RevenueEntry.RevenueId == string.Empty)
            {
                // TODO: Autogenerate need to change to auto running number
                Random rand = new Random();
                if (string.IsNullOrWhiteSpace(this.RevenueEntry.RevenueId))
                {
                    this.RevenueEntry.RevenueId = rand.Next(100000).ToString("D5"); // auto generate.
                }
            }

            // Set UserCredits's Revenue Id
            var usrSearch = Search.UserCredits.GetActiveById.Create(
                this.UserShift.UserId, this.PlazaGroup.PlazaGroupId);
            var userCredit = ops.Credits.GetNoRevenueEntryUserCreditBalanceById(usrSearch).Value();
            userCredit.RevenueId = this.RevenueEntry.RevenueId;
            ops.Credits.SaveUserCreditBalance(userCredit);

            // Save Revenue Entry.
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

            bool bCloseUserShift = (null == existActivities || existActivities.Count == 0);
            if (bCloseUserShift)
            {
                // no lane activitie in user shift.
                ops.UserShifts.EndUserShift(this.UserShift); // End user job(shift).
            }
            // Send data to server to mark sync status.
            SendRevnue(this.RevenueEntry);

            return !bCloseUserShift;
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
        /// Gets or sets Chief/Supervisor.
        /// </summary>
        public User Supervisor { get; set; }
        /// <summary>
        /// Gets or sets plaza group.
        /// </summary>
        public PlazaGroup PlazaGroup { get; set; }
        /// <summary>
        /// Gets Plaza Id List.
        /// </summary>
        public List<int> PlazaIds { get; internal set; }

        /// <summary>
        /// Gets related user shift.
        /// </summary>
        public UserShift UserShift { get; internal set; }

        /// <summary>
        /// Gets related user revenue's shift.
        /// </summary>
        public UserShiftRevenue RevenueShift { get; internal set; }

        /// <summary>
        /// Gets related LaneAttendance list.
        /// </summary>
        public List<LaneAttendance> Attendances { get; internal set; }
        /// <summary>
        /// Gets Lane Number List.
        /// </summary>
        public List<int> Lanes { get; internal set; }
        /// <summary>
        /// Gets Lane List string.
        /// </summary>
        public string LaneList { get; internal set; }
        /// <summary>
        /// Checks has lane attendance.
        /// </summary>
        public bool HasAttendance
        {
            get { return (null != Attendances && Attendances.Count > 0); }
        }
        /// <summary>
        /// Gets is new Revenue Shift.
        /// </summary>
        public bool IsNewRevenueShift { get; internal set; }
        /// <summary>
        /// Checks Is Return Bag.
        /// </summary>
        public bool IsReturnBag
        {
            get
            {
                // Set UserCredits's Revenue Id
                var usrSearch = Search.UserCredits.GetActiveById.Create(
                    this.UserShift.UserId, this.PlazaGroup.PlazaGroupId);
                var userCredit = ops.Credits.GetNoRevenueEntryUserCreditBalanceById(usrSearch).Value();
                if (null != userCredit && userCredit.State == UserCreditBalance.StateTypes.Completed)
                {
                    return true;
                }
                return false;
            }
        }
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

        #region Static Methods

        /// <summary>
        /// Send all unsend revenue entry to server.
        /// </summary>
        public static void SendRevnues()
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            var ops = LocalServiceOperations.Instance.Plaza;
            try
            {
                // Get Unsend revenue entries.
                var entries = ops.Revenue.GetUnsendRevenues().Value();
                if (null != entries && entries.Count > 0)
                {
                    bool sendSuccess = true;
                    entries.ForEach(entry => 
                    {
                        if (sendSuccess)
                        {
                            sendSuccess = SendRevnue(entry);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        public static bool SendRevnue(RevenueEntry value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            var ops = LocalServiceOperations.Instance.Plaza;
            var server = SCWServiceOperations.Instance.Plaza;

            // Need to sync currency and coupon master!!
            var currencies = ops.Master.GetCurrencies().Value();
            var coupons = ops.Master.GetCoupons().Value();
            // find lane attendances.
            var attendances = ops.Lanes.GetAttendancesByRevenue(value).Value();
            var plazaGroup = new PlazaGroup() 
            { 
                TSBId = value.TSBId,
                PlazaGroupId = value.PlazaGroupId
            };
            var plazas = ops.TSB.GetPlazaGroupPlazas(plazaGroup).Value();
            int plazaId = (null != plazas && plazas.Count > 0) ? Convert.ToInt32(plazas[0].PlazaId) : -1;

            if (plazaId == -1)
            {
                med.Info("declare error: Cannot search plaza id.");
                return false;
            }

            SCWDeclare declare = value.ToServer(currencies, coupons, attendances, plazaId);
            var ret = server.TOD.Declare(declare);

            bool sendSucces = false;
            if (null != ret && null != ret.status)
            {
                sendSucces = (ret.status.code == "S200");
                // write log.
                med.Info("declare - code: {0}, msg: {1}", ret.status.code, ret.status.message);
            }
            else
            {
                // send failed.
                med.Info("declare error: SCW service connect failed.");
            }

            if (sendSucces)
            {
                value.Status = 1; // send OK.
                value.LastUpdate = DateTime.Now;
                ops.Revenue.SaveRevenue(value);
            }

            return sendSucces;
        }

        #endregion
    }

    #endregion

    #region HistoricalRevenueEntryManager

    /// <summary>
    /// The HistoricalRevenueEntryManager class.
    /// </summary>
    public class HistoricalRevenueEntryManager
    {
        #region Internal Variables

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private SCWOperations server = SCWServiceOperations.Instance.Plaza;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public HistoricalRevenueEntryManager() : base()
        {
            // TODO: Need user/password from config table or external file.
            SCWServiceOperations.Instance.UserName = "DMTUSER";
            SCWServiceOperations.Instance.Password = "DMTPASS";

            this.EntryDate = DateTime.Now;
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        /// <summary>
        /// Create User Shift and Revenue Date.
        /// </summary>
        public void CreateUserShift()
        {
            if (null == this.User || null == this.Shift) return;
            MethodBase med = MethodBase.GetCurrentMethod();

            // Create user shift.
            this.UserShift = ops.UserShifts.Create(this.Shift, this.User).Value();
            UserShift.Begin = DateTime.Now;
            UserShift.End = UserShift.Begin;

            if (null != UserShift)
            {
                string msg = string.Format("User Shift found. Begin: {0}, End {1}",
                    UserShift.Begin.ToDateTimeString(), UserShift.End.ToDateTimeString());
                med.Info(msg);
                // Create User Revenue Shift
                var revops = Search.Revenues.PlazaShift.Create(this.UserShift, this.PlazaGroup);
                this.RevenueShift = ops.Revenue.CreateRevenueShift(revops).Value();
            }
            else
            {
                string msg = "User Shift cannot create.";
                med.Info(msg);
                // Remove User Revenue Shift
                this.RevenueShift = null;
            }
        }
        /// <summary>
        /// Refresh User Job List.
        /// </summary>
        public void RefreshJobs()
        {
            /*
            if (null != this.UserShift && null != this.PlazaGroup)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                // Get all lanes information.
                var search = Search.Lanes.Attendances.ByUserShift.Create(
                    this.UserShift, this.PlazaGroup, DateTime.MinValue);
                this.Attendances = ops.Lanes.GetAttendancesByUserShift(search).Value();
                if (!HasAttendance)
                {
                    string msg = "Attendances is null or no lane attendance list.";
                    med.Info(msg);
                }
                else
                {
                    string msg = string.Format("Attendances found : No of lanes: {0}", this.Attendances.Count);
                    med.Info(msg);
                }
            }
            */
            if (null != this.User)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                // Get all lanes information.
                this.Attendances = ops.Lanes.GetAllNotHasRevenueEntryByUser(this.User).Value();
                if (!HasAttendance)
                {
                    string msg = "Attendances is null or no lane attendance list.";
                    med.Info(msg);
                }
                else
                {
                    string msg = string.Format("Attendances found : No of lanes: {0}", this.Attendances.Count);
                    med.Info(msg);
                }
            }
        }

        public RevenueEntryManager Create()
        {
            RevenueEntryManager inst = new RevenueEntryManager();
            
            inst.EntryDate = this.EntryDate;
            inst.RevenueDate = this.RevenueDate;
            inst.User = this.User;
            inst.Supervisor = this.Supervisor;
            inst.UserShift = this.UserShift;
            inst.PlazaGroup = this.PlazaGroup;

            inst.Attendances = new List<LaneAttendance>();
            if (null != this.Attendances && this.Attendances.Count > 0)
            {
                this.Attendances.ForEach(att => 
                {
                    if (att.Selected) inst.Attendances.Add(att);
                });
            }

            inst.RevenueShift = this.RevenueShift;

            return inst;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Entry Date.
        /// </summary>
        public DateTime EntryDate { get; set; }
        /// <summary>
        /// Gets or sets Revenue Date.
        /// </summary>
        public DateTime RevenueDate { get; set; }
        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Gets or sets Shift.
        /// </summary>
        public Shift Shift { get; set; }
        /// <summary>
        /// Gets or sets Chief/Supervisor.
        /// </summary>
        public User Supervisor { get; set; }
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
        /// Gets related user revenue's shift.
        /// </summary>
        public UserShiftRevenue RevenueShift { get; internal set; }

        #endregion
    }

    #endregion
}
