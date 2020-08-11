#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;
using System.Windows.Forms;

#endregion

namespace DMT.Services
{
    partial class LocalOperations
    {
        #region Internal Variables

        private TSBOperations _TSB_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets TSB Operations.
        /// </summary>
        public TSBOperations TSB 
        {
            get 
            { 
                if (null == _TSB_Ops)
                {
                    lock (this)
                    {
                        _TSB_Ops = new TSBOperations();
                    }
                }
                return _TSB_Ops;
            }
        }

        #endregion

        #region TSBOperations class

        /// <summary>
        /// The TSB Operations class.
        /// Used for Manage TSB, Plaza and Lane's operation(s).
        /// </summary>
        public class TSBOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal TSBOperations() { }

            #endregion

            #region Public Methods

            #region TSB

            public NRestResult<List<TSB>> GetTSBs()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSB>>(
                    RouteConsts.TSB.GetTSBs.Url, new { });
                return ret;
            }

            public NRestResult<TSB> GetCurrent()
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSB>(
                    RouteConsts.TSB.GetCurrent.Url, new { });
                return ret;
            }

            public NRestResult SetActive(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute(
                    RouteConsts.TSB.SetActive.Url, tsb);
                return ret;
            }

            public NRestResult<TSB> SaveTSB(TSB value)
            {
                if (null != value)
                {
                    var ret = NRestClient.Create(port: 9000).Execute<TSB>(
                        RouteConsts.TSB.SaveTSB.Url, value);
                    return ret;
                }
                return new NRestResult<TSB>();
            }

            #endregion

            #region Plaza

            public NRestResult<List<Plaza>> GetTSBPlazas(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Plaza>>(
                    RouteConsts.TSB.GetTSBPlazas.Url, tsb);
                return ret;
            }

            public NRestResult<Plaza> SavePlaza(Plaza value)
            {
                if (null != value)
                {
                    var ret = NRestClient.Create(port: 9000).Execute<Plaza>(
                        RouteConsts.TSB.SavePlaza.Url, value);
                    return ret;
                }
                return new NRestResult<Plaza>();
            }

            #endregion

            #region PlazaGroup

            public NRestResult<List<PlazaGroup>> GetTSBPlazaGroups(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<PlazaGroup>>(
                    RouteConsts.TSB.GetTSBPlazaGroups.Url, tsb);
                return ret;
            }

            public NRestResult<PlazaGroup> SavePlazaGroup(PlazaGroup value)
            {
                if (null != value)
                {
                    var ret = NRestClient.Create(port: 9000).Execute<PlazaGroup>(
                        RouteConsts.TSB.SavePlazaGroup.Url, value);
                    return ret;
                }
                return new NRestResult<PlazaGroup>();
            }

            #endregion

            #region Lane

            public NRestResult<List<Lane>> GetTSBLanes(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Lane>>(
                    RouteConsts.TSB.GetTSBLanes.Url, tsb);
                return ret;
            }

            public NRestResult<List<Lane>> GetPlazaLanes(Plaza plaza)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Lane>>(
                    RouteConsts.TSB.GetPlazaLanes.Url, plaza);
                return ret;
            }

            public NRestResult<List<Lane>> GetPlazaGroupLanes(PlazaGroup plazaGroup)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Lane>>(
                    RouteConsts.TSB.GetPlazaGroupLanes.Url, plazaGroup);
                return ret;
            }

            public NRestResult<Lane> SaveLane(Lane value)
            {
                if (null != value)
                {
                    var ret = NRestClient.Create(port: 9000).Execute<Lane>(
                        RouteConsts.TSB.SaveLane.Url, value);
                    return ret;
                }
                return new NRestResult<Lane>();
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
