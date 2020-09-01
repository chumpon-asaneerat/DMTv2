#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;
using System.Windows.Forms;
using DMT.Models.ExtensionMethods;

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
            // required networkId, plazaId, userId
            var ret = server.Masters.GetJobList(31, 3101, this.User.UserId);
            var attends = new List<LaneAttendance>();
            if (null != ret && null != ret.list)
            {
                ret.list.ForEach(inst =>
                {
                    var attend = inst.ToLocal();
                    if (null != attend) attends.Add(attend);
                });
            }
            // check exists and save (insert/update)
            attends.ForEach(attend =>
            {

            });
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
            if (null == this.UserShift)
            {
                // user shift not found.
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
        /// Gets related user shift revenue.
        /// </summary>
        public UserShiftRevenue ShiftRevenue { get; internal set; }
        /// <summary>
        /// Gets related LaneAttendance list.
        /// </summary>
        public List<LaneAttendance> Attendances { get; internal set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Sync data from SCW Web Service.
        /// </summary>
        public static void Sync()
        {

        }

        #endregion
    }

    #endregion
}
