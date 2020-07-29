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
    partial class PlazaOperations
    {
        #region Internal Variables

        private MasterOperations _master_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Master Operations.
        /// </summary>
        public MasterOperations Master
        {
            get
            {
                if (null == _master_Ops)
                {
                    lock (this)
                    {
                        _master_Ops = new MasterOperations();
                    }
                }
                return _master_Ops;
            }
        }

        #endregion

        #region MasterOperations class

        /// <summary>
        /// The Master Operations class.
        /// Used for Manage Master (Direct table access operation).
        /// </summary>
        public class MasterOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal MasterOperations() { }

            #endregion

            #region Public Methods
            /*
            public List<TSB> GetTSBs()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSB>>(
                    RouteConsts.TSB.GetTSBs.Url, new { });
                return ret;
            }

            public List<Plaza> GetTSBPlazas(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Plaza>>(
                    RouteConsts.TSB.GetTSBPlazas.Url, tsb);
                return ret;
            }

            public List<PlazaGroup> GetTSBPlazaGroups(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<PlazaGroup>>(
                    RouteConsts.TSB.GetTSBPlazaGroups.Url, tsb);
                return ret;
            }

            public List<Lane> GetPlazaGroupLanes(PlazaGroup plazaGroup)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Lane>>(
                    RouteConsts.TSB.GetPlazaGroupLanes.Url, plazaGroup);
                return ret;
            }

            public List<Lane> GetTSBLanes(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Lane>>(
                    RouteConsts.TSB.GetTSBLanes.Url, tsb);
                return ret;
            }

            public List<Lane> GetPlazaLanes(Plaza plaza)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<Lane>>(
                    RouteConsts.TSB.GetPlazaLanes.Url, plaza);
                return ret;
            }

            public void SetActive(TSB tsb)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.TSB.SetActive.Url, tsb);
            }

            public TSB GetCurrent()
            {
                TSB ret = _current;
                TimeSpan ts = DateTime.Now - LastUpdated;
                if (ts.TotalSeconds >= 1)
                {
                    _current = NRestClient.Create(port: 9000).Execute<TSB>(
                        RouteConsts.TSB.GetCurrent.Url, new { });

                    LastUpdated = DateTime.Now;
                }
                return _current;
            }
            */
            #endregion
        }

        #endregion
    }
}
