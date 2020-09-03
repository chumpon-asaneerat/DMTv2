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

        // Case Online:
        // 1. อ่าน job list จาก WS โดย กรองจาก Plaza Group Id ซึ่งต้องดึง 1 รอบในกรณี
        //    Plaza Group มี Plaza เดียว และต้องดึงมากกว่า 1 รอบถ้า ใน Plaza group มีมากกว่า 1 Plaza
        //    แล้วเอาข้อมูลทั้งหมดมารวมเป็น List เดียวของ 1 Plaza group โดย WS ข้อมูลที่มี จะมีแค่ 
        //    PlazaId, LaneNo, Begin, End, UserId จากนั้นจึงสร้าง หรือ Update Lane Attendance
        //    โดย ใช้ Key TSBId, PlazaId, LaneId, JobId, UserId และ BeginDate ในการค้นหา
        //    ว่ามีรายการหรือไม่ ถ้ามีก็ทำการ Update แต่ถ้าไม่พบก็ทำการ insert ใหม่
        //
        // 2. ต้องเอามาหา TSBShift และ UserShift
        //    ซึ่งจะแบ่งเป็น 2 case คื่อมี Shift หรือไม่มี Shift (Supervisro/User ไม่ได้เปิด shift)
        //    2.1 กรณีไม่มี TSBShift ให้เปิด TSBShift โดยใช้ Supervisor พิเศษ และใช้กรอบเวลามาตรฐานไปก่อน
        //    2.2 กรณีมี TSBShift แต่ไม่มี UserShift ให้สร้าง UserShift ใหม่ โดยเวลาเริ่มต้นให้เป็นเวลาแรกของ
        //        Job List begin และเวลาสิ้นสุดให้เป็น เวลาที่ Job List ไม่เกินเวลาของ TSB End Shift
        //    2.3 กรณีมี ทั้ง TSBShift/UserShift ให้กรอง Job List ตามข้อ 1.2
        //
        // Case Offline
        // 1. ต้องมี TSBShift/UserShift เนื่องจากต้องมีการระบุจากการทำงานปรกติ
        // 
        // 
        // 
        // 
        // Sync Offline -> Online 
        // 1. กรณี นี้จะมีการป้อนรายได้ไปแล้ว ซึ่งหมายถึง UserShiftId จะมีข้อมูล RevenueId (auto gen) แล้ว
        //    ดังนั้น เมื่อทำการ อ่านรายการ Job List ได้ต้องทำการ ตรวจสอบว่า มีกรายการทำ Revenue ไปแล้วกี่รายการ
        //    ก็ให้เอารายการเหล่านั้น เป็นกรอบในการหารายการ Job ที่เกี่ยวข้องมาก่อน แล้วทำการ ส่ง Update ไปยัง WS
        //    โดยทำการ check flag sync จากตาราง Lane Attendance, Revenue Entry, Lane Payment
        //    โดบเมื่อทำการส่งเสร็จให้ mark sync flag ว่าทำการ sync แล้ว

        private void SyncJobList()
        {
            // Sync JobList to LaneAttendance
            if (null == this.User) return;

            // Gets Job List from WS.
            var ret = server.Masters.GetJobList(31, 3101, this.User.UserId);
            var attends = new List<LaneAttendance>();
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
        public bool HasRevenuEntry
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
        /// Gets related LaneAttendance list.
        /// </summary>
        public List<LaneAttendance> Attendances { get; internal set; }

        #endregion
    }

    #endregion
}
