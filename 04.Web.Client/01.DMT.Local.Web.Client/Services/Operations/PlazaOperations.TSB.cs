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
            private DateTime LastUpdated = DateTime.MinValue;
            private TSB _current = null;

            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal TSBOperations() { }

            #endregion

            #region Public Methods

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
                if (ts.TotalMinutes >= 1)
                {
                    _current = NRestClient.Create(port: 9000).Execute<TSB>(
                        RouteConsts.TSB.GetCurrent.Url, new { });

                    LastUpdated = DateTime.Now;
                }
                return _current;
            }

            #endregion
        }

        #endregion
    }
}
