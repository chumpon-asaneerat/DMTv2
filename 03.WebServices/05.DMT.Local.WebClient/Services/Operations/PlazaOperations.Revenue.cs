﻿#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;

using RestSharp;

using DMT.Models;
using System.Windows.Forms;
using DMT.Models.ExtensionMethods;
using NLib.Reflection;
using System.ComponentModel;
using System.Reflection;
using NLib;
using NLib.IO;
using System.Runtime.InteropServices;

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

            #region Unique Code

            public NRestResult<UniqueCode> GetUniqueId(string value)
            {
                NRestResult<UniqueCode> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<UniqueCode>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<UniqueCode>(
                        RouteConsts.Revenue.GetUniqueId.Url, value);
                }
                else
                {
                    ret = new NRestResult<UniqueCode>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<UniqueCode> IncreaseUniqueId(string value)
            {
                NRestResult<UniqueCode> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<UniqueCode>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<UniqueCode>(
                        RouteConsts.Revenue.IncreaseUniqueId.Url, value);
                }
                else
                {
                    ret = new NRestResult<UniqueCode>();
                    ret.ParameterIsNull();
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
            // TODO: Need user/password from config table or external file.
            SCWServiceOperations.Instance.UserName = "DMTUSER";
            SCWServiceOperations.Instance.Password = "DMTPASS";

            this.ByChief = false;
            this.EntryDate = DateTime.Now;
        }

        #endregion

        #region Private Methods

        public void SyncJobList()
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
                int plazaId = plaza.SCWPlazaId;
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
                            Search.Users.ById.Create(attend.UserId, 
                                "ADMINS",
                                "ACCOUNT",
                                "CTC_MGR", "CTC", "TC",
                                "MT_ADMIN", "MT_TECH",
                                "FINANCE", "SV",
                                "RAD_MGR", "RAD_SUP")).Value();
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

            UserCreditBalance userCredit = null;
            if (!this.ByChief)
            {
                userCredit = ops.Credits.GetNoRevenueEntryUserCreditBalanceById(search).Value();
            }
            else
            {
                // By chief create empty balance - not saved but temp for disaply.
                userCredit = new UserCreditBalance();
                userCredit.State = UserCreditBalance.StateTypes.Completed; // set completed state.
                userCredit.BagNo = string.Empty;
                userCredit.BeltNo = string.Empty;
                userCredit.TSBId = this.UserShift.TSBId;
                userCredit.TSBNameEN = this.UserShift.TSBNameEN;
                userCredit.TSBNameTH = this.UserShift.TSBNameTH;
                userCredit.UserId = this.UserShift.UserId;
                userCredit.FullNameEN = this.UserShift.FullNameEN;
                userCredit.FullNameTH = this.UserShift.FullNameTH;
            }

            this.RevenueEntry = new Models.RevenueEntry();
            UpdateRevenueEntry();

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

        private void UpdateRevenueEntry()
        {
            if (null == this.UserShift || null == this.PlazaGroup || null == this.RevenueEntry)
            {
                return;
            }
            // Check is historical
            this.RevenueEntry.IsHistorical = this.ByChief;
            // assigned plaza group.
            this.RevenueEntry.PlazaGroupId = this.PlazaGroup.PlazaGroupId;
            // update object properties.
            this.PlazaGroup.AssignTo(this.RevenueEntry); // assigned plaza group name (EN/TH)
            this.UserShift.AssignTo(this.RevenueEntry); // assigned user shift

            // assigned date after sync object(s) to RevenueEntry.
            this.RevenueEntry.EntryDate = this.EntryDate; // assigned Entry date.
            var dtNow = DateTime.Now;
            this.RevenueEntry.RevenueDate = new DateTime(
                this.RevenueDate.Year, this.RevenueDate.Month, this.RevenueDate.Day,
                dtNow.Hour, dtNow.Minute, dtNow.Second, dtNow.Millisecond);

            CreateLaneList();
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

        public void BuildRevenueEntry()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            if (null == this.UserShift || null == this.PlazaGroup || null == this.RevenueEntry)
            {
                string msg = "PlazaGroup or User Shift or Revenue Entry is null.";
                med.Info(msg);
                return;
            }

            UpdateRevenueEntry();
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
                var unique = ops.Revenue.GetUniqueId("RevenueEntry").Value();
                if (string.IsNullOrWhiteSpace(this.RevenueEntry.RevenueId))
                {
                    string yr = DateTime.Now.ToThaiDateTimeString("yy");
                    string autoId = (null != unique) ? yr + unique.LastNumber.ToString("D5") : string.Empty; // auto generate.
                    this.RevenueEntry.RevenueId = autoId;
                    ops.Revenue.IncreaseUniqueId("RevenueEntry");
                }
            }

            // Set UserCredits's Revenue Id
            var usrSearch = Search.UserCredits.GetActiveById.Create(
                this.UserShift.UserId, this.PlazaGroup.PlazaGroupId);

            UserCreditBalance userCredit = null;
            if (!this.ByChief)
            {
                userCredit = ops.Credits.GetNoRevenueEntryUserCreditBalanceById(usrSearch).Value();
                userCredit.RevenueId = this.RevenueEntry.RevenueId;
                ops.Credits.SaveUserCreditBalance(userCredit);
            }
            else
            {
                // By chief create empty balance - update from Revenue Entry and save.
                userCredit = new UserCreditBalance();
                userCredit.State = UserCreditBalance.StateTypes.Completed; // set completed state.
                userCredit.BagNo = this.RevenueEntry.BagNo;
                userCredit.BeltNo = this.RevenueEntry.BeltNo;
                userCredit.TSBId = this.UserShift.TSBId;
                userCredit.TSBNameEN = this.UserShift.TSBNameEN;
                userCredit.TSBNameTH = this.UserShift.TSBNameTH;
                userCredit.UserId = this.UserShift.UserId;
                userCredit.FullNameEN = this.UserShift.FullNameEN;
                userCredit.FullNameTH = this.UserShift.FullNameTH;
                userCredit.RevenueId = this.RevenueEntry.RevenueId;
                ops.Credits.SaveUserCreditBalance(userCredit);
            }

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

            if (!this.ByChief)
            {
                if (bCloseUserShift)
                {
                    // no lane activitie in user shift.
                    ops.UserShifts.EndUserShift(this.UserShift); // End user job(shift).
                }
            }
            else
            {
                ops.UserShifts.SaveUserShift(this.UserShift); // direct save.
            }
            // Generte Revenue (declare) File and mark sync status.
            GenerateRevnueFile(this);

            return !bCloseUserShift;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets is revenue by chief.
        /// </summary>
        public bool ByChief { get; internal set; }
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
        /// Gets local scw message json folder path name.
        /// </summary>
        public static string LocalSCWMessageFolder
        {
            get
            {
                string localFilder = Folders.Combine(
                    Folders.Assemblies.CurrentExecutingAssembly, "scw");
                if (!Folders.Exists(localFilder))
                {
                    Folders.Create(localFilder);
                }
                return localFilder;
            }
        }
        /// <summary>
        /// Gets local TA declare message json folder path name.
        /// </summary>
        public static string LocalTODDeclareolder
        {
            get
            {
                string localFilder = Folders.Combine(LocalSCWMessageFolder, "declare");
                if (!Folders.Exists(localFilder))
                {
                    Folders.Create(localFilder);
                }
                return localFilder;
            }
        }

        private static void WriteFile(string fullFileName, string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            MethodBase med = MethodBase.GetCurrentMethod();
            // Save message.
            try
            {
                using (var stream = File.CreateText(fullFileName))
                {
                    stream.Write(message);
                    stream.Flush();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }
        }

        private static void WriteDeclareFile(string revenueId, string userId, string message)
        {
            // Create file.
            string fileName = "declare." + 
                DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss.ffffff", System.Globalization.DateTimeFormatInfo.InvariantInfo) +
                "." + revenueId +
                "." + userId +
                ".json";
            string fullFileName = Path.Combine(LocalTODDeclareolder, fileName);
            // Save message.
            WriteFile(fullFileName, message);
        }

        public static bool GenerateRevnueFile(RevenueEntryManager value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();
            try
            {
                var ops = LocalServiceOperations.Instance.Plaza;

                if (null == value || null == value.RevenueEntry) return false;
                var entry = value.RevenueEntry;

                var tsb = ops.TSB.GetCurrent().Value();
                // Need to sync currency and coupon master!!
                var currencies = ops.Master.GetCurrencies().Value();
                var coupons = ops.Master.GetCoupons().Value();
                var cardAllows = ops.Master.GetCardAllows().Value();
                var emv = RevenueEntryManager.GetEMVList(tsb, value, value.RevenueEntry);
                var qrCode = RevenueEntryManager.GetQRCodeList(tsb, value, value.RevenueEntry);

                // find lane attendances.
                var attendances = ops.Lanes.GetAttendancesByRevenue(entry).Value();
                var plazaGroup = new PlazaGroup()
                {
                    TSBId = entry.TSBId,
                    PlazaGroupId = entry.PlazaGroupId
                };
                var plazas = ops.TSB.GetPlazaGroupPlazas(plazaGroup).Value();
                int plazaId = (null != plazas && plazas.Count > 0) ? plazas[0].SCWPlazaId : -1;

                if (plazaId == -1)
                {
                    med.Info("declare error: Cannot search plaza id.");
                    return false;
                }

                // Create declare json file.
                SCWDeclare declare = entry.ToServer(currencies, coupons, cardAllows, 
                    attendances, emv, qrCode, plazaId);
                WriteDeclareFile(entry.RevenueId, entry.UserId, declare.ToJson());

                // Update local database status.
                entry.Status = 1; // generated json file OK.
                entry.LastUpdate = DateTime.Now;
                ops.Revenue.SaveRevenue(entry);

                return true;
            }
            catch (Exception ex)
            {
                med.Err(ex);
                return false;
            }
        }

        public static void SyncMasters()
        {
            var ops = LocalServiceOperations.Instance.Plaza;
            var server = SCWServiceOperations.Instance.Plaza;

            // TODO: Need user/password from config table or external file.
            SCWServiceOperations.Instance.UserName = "DMTUSER";
            SCWServiceOperations.Instance.Password = "DMTPASS";
            int nw = 31;

            var currencies = server.Masters.GetCurrencyList(nw);
            if (null != currencies && null != currencies.list && null != currencies.status && 
                currencies.status.code == "S200")
            {
                List<MCurrency> mCurrencies = new List<MCurrency>();
                currencies.list.ForEach(scw =>
                {
                    var inst = new MCurrency();
                    inst.currencyId = scw.currencyId;
                    inst.denomTypeId = scw.denomTypeId;
                    inst.currencyDenomId = scw.currencyDenomId;
                    inst.denomValue = scw.denomValue;
                    inst.abbreviation = scw.abbreviation;
                    inst.description = scw.description;
                    mCurrencies.Add(inst);
                });

                ops.Master.SaveMCurrencies(mCurrencies);
            }
            var coupon = server.Masters.GetCouponList(nw);
            if (null != coupon && null != coupon.list && null != coupon.status &&
                coupon.status.code == "S200")
            {
                List<MCoupon> mCoupons = new List<MCoupon>();
                coupon.list.ForEach(scw =>
                {
                    var inst = new MCoupon();
                    inst.couponId = scw.couponId;
                    inst.couponValue = scw.couponValue;
                    inst.abbreviation = scw.abbreviation;
                    inst.description = scw.description;
                    mCoupons.Add(inst);
                });

                ops.Master.SaveMCoupons(mCoupons);
            }
            var cardAllows = server.Masters.GetCardAllowList(nw);
            if (null != cardAllows && null != cardAllows.list && null != cardAllows.status &&
                cardAllows.status.code == "S200")
            {
                List<MCardAllow> mCardAllows = new List<MCardAllow>();
                cardAllows.list.ForEach(scw =>
                {
                    var inst = new MCardAllow();
                    inst.cardAllowId = scw.cardAllowId;
                    inst.abbreviation = scw.abbreviation;
                    inst.description = scw.description;
                    mCardAllows.Add(inst);
                });

                ops.Master.SaveMCardAllows(mCardAllows);
            }
        }

        public static List<SCWEMV> GetEMVList(TSB tsb, 
            RevenueEntryManager manager, RevenueEntry entry)
        {
            if (null == manager && null == entry)
            {
                return new List<SCWEMV>();
            }
            DateTime dt1, dt2;
            if (entry.IsHistorical)
            {
                // Note. On this step the attendance should be order continuous without skip job between.
                if (null == manager.Attendances || manager.Attendances.Count <= 0)
                {
                    // No job list so no EMV avaliable.
                    return new List<SCWEMV>();
                }
                else
                {
                    // NOTE:::
                    // THIS CODE USE FOR MATCH EMV IN HISTORICAL MODE
                    /// NEED TO PORT
                    dt1 = manager.Attendances[0].Begin;
                    dt2 = manager.Attendances[manager.Attendances.Count - 1].End;
                    // Check case job has no End Of Job.
                    if (dt2 == DateTime.MinValue)
                    {
                        dt2 = manager.Attendances[manager.Attendances.Count - 1].Begin;
                    }
                }
            }
            else
            {
                dt1 = entry.ShiftBegin;
                dt2 = (entry.ShiftEnd == DateTime.MinValue) ? DateTime.Now : entry.ShiftEnd;
            }

            return GetEMVList(tsb, entry.UserId, dt1, dt2);
        }

        public static List<SCWEMV> GetEMVList(TSB tsb, string userId, DateTime start, DateTime end)
        {
            LocalOperations ops = LocalServiceOperations.Instance.Plaza;
            SCWOperations server = SCWServiceOperations.Instance.Plaza;
            // TODO: Need user/password from config table or external file.
            SCWServiceOperations.Instance.UserName = "DMTUSER";
            SCWServiceOperations.Instance.Password = "DMTPASS";
            // TODO: network id required.
            int nwId = 31;
            List<SCWEMV> results = new List<SCWEMV>();
            if (null != tsb && !string.IsNullOrWhiteSpace(userId))
            {
                List<SCWEMV> items = new List<SCWEMV>();
                var plazas = ops.TSB.GetTSBPlazas(tsb).Value();
                if (null != plazas && plazas.Count > 0) 
                {
                    plazas.ForEach(plaza =>
                    {
                        int pzId = plaza.SCWPlazaId;
                        var emvList = server.TOD.GetEMVList(nwId, pzId, userId, start, end);
                        if (null != emvList && null != emvList.list)
                        {
                            items.AddRange(emvList.list);
                        }
                    });

                    results = items.OrderBy(o => o.trxDateTime).Distinct().ToList();
                }
                else
                {
                    results = new List<SCWEMV>();
                }
            }
            return results;
        }

        public static List<SCWQRCode> GetQRCodeList(TSB tsb, 
            RevenueEntryManager manager, RevenueEntry entry)
        {
            if (null == manager && null == entry)
            {
                return new List<SCWQRCode>();
            }
            DateTime dt1, dt2;
            if (entry.IsHistorical)
            {
                // Note. On this step the attendance should be order continuous without skip job between.
                if (null == manager.Attendances || manager.Attendances.Count <= 0)
                {
                    // No job list so no QR Code avaliable.
                    return new List<SCWQRCode>();
                }
                else
                {
                    dt1 = manager.Attendances[0].Begin;
                    dt2 = manager.Attendances[manager.Attendances.Count - 1].End;
                    // Check case job has no End Of Job.
                    if (dt2 == DateTime.MinValue)
                    {
                        dt2 = manager.Attendances[manager.Attendances.Count - 1].Begin;
                    }
                }
            }
            else
            {
                dt1 = entry.ShiftBegin;
                dt2 = (entry.ShiftEnd == DateTime.MinValue) ? DateTime.Now : entry.ShiftEnd;
            }

            return GetQRCodeList(tsb, entry.UserId, dt1, dt2);
        }

        public static List<SCWQRCode> GetQRCodeList(TSB tsb, string userId, DateTime start, DateTime end)
        {
            LocalOperations ops = LocalServiceOperations.Instance.Plaza;
            SCWOperations server = SCWServiceOperations.Instance.Plaza;
            // TODO: Need user/password from config table or external file.
            SCWServiceOperations.Instance.UserName = "DMTUSER";
            SCWServiceOperations.Instance.Password = "DMTPASS";
            // TODO: network id required.
            int nwId = 31;
            List<SCWQRCode> results = new List<SCWQRCode>();
            if (null != tsb && !string.IsNullOrWhiteSpace(userId))
            {
                List<SCWQRCode> items = new List<SCWQRCode>();
                var plazas = ops.TSB.GetTSBPlazas(tsb).Value();
                if (null != plazas && plazas.Count > 0)
                {
                    plazas.ForEach(plaza =>
                    {
                        int pzId = plaza.SCWPlazaId;
                        var qrList = server.TOD.GetQRCodeList(nwId, pzId, userId, start, end);
                        if (null != qrList && null != qrList.list)
                        {
                            items.AddRange(qrList.list);
                        }
                    });

                    results = items.OrderBy(o => o.trxDateTime).Distinct().ToList();
                }
                else 
                {
                    results = new List<SCWQRCode>();
                }
            }
            return results;
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

        private void SyncJobList()
        {
            // Sync JobList to LaneAttendance
            if (null == this.User) return;
            if (null == this.PlazaGroup) return;
            var plazas = ops.TSB.GetPlazaGroupPlazas(this.PlazaGroup).Value();
            var plazas2 = new List<Plaza>();
            plazas2.AddRange(plazas);

            if (null == plazas) return;

            var attends = new List<LaneAttendance>();
            this.PlazaIds = new List<int>();

            plazas.ForEach(plaza =>
            {
                // Gets Job List from WS.
                int nwId = 31; // TODO: network id required.
                int plazaId = plaza.SCWPlazaId;
                // keep plaza Id.
                if (!this.PlazaIds.Contains(plazaId)) this.PlazaIds.Add(plazaId);

                var ret = server.TOD.GetJobList(nwId, plazaId, this.User.UserId);
                if (null != ret && null != ret.list)
                {
                    ret.list.ForEach(inst =>
                    {
                        // Bug cannot call ToLocal here because is not has database connection.
                        //var attend = inst.ToLocal();

                        var attend = new LaneAttendance();

                        Plaza plaza2 = null;
                        //value.networkId;
                        if (inst.plazaId.HasValue)
                        {
                            plaza2 = plazas2.Find(pz2 => { return pz2.SCWPlazaId == inst.plazaId.Value; });
                        }
                        if (null == plaza2) return;
                        attend.PlazaId = plaza2.PlazaId;
                        if (inst.laneId.HasValue)
                        {
                            attend.LaneNo = inst.laneId.Value;
                        }
                        attend.UserId = inst.staffId;
                        attend.JobId = (inst.jobNo.HasValue) ?
                            inst.jobNo.Value.ToString() : string.Empty;
                        attend.Begin = inst.bojDateTime.Value();
                        attend.End = inst.eojDateTime.Value();


                        var lane = ops.TSB.GetPlazaLane(
                            Search.Plaza.LaneByNo.Create(attend.PlazaId, attend.LaneNo)).Value();
                        if (null != lane) lane.AssignTo(attend);

                        var user = ops.Users.GetById(
                            Search.Users.ById.Create(attend.UserId,
                                "ADMINS",
                                "ACCOUNT",
                                "CTC_MGR", "CTC", "TC",
                                "MT_ADMIN", "MT_TECH",
                                "FINANCE", "SV",
                                "RAD_MGR", "RAD_SUP")).Value();
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
            if (null != this.User)
            {
                MethodBase med = MethodBase.GetCurrentMethod();
                // Sync JobList to LaneAttendance
                SyncJobList();
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

            inst.ByChief = true; // set revenue entry by chief
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
        /// Gets Plaza Id List.
        /// </summary>
        public List<int> PlazaIds { get; internal set; }

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
