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
                NRestResult<List<TSB>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<TSB>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<List<TSB>>(
                    RouteConsts.TSB.GetTSBs.Url, new { });
                return ret;
            }

            public NRestResult<TSB> GetCurrent()
            {
                NRestResult<TSB> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<TSB>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<TSB>(RouteConsts.TSB.GetCurrent.Url, new { });
                return ret;
            }

            public NRestResult SetActive(TSB value)
            {
                NRestResult ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute(RouteConsts.TSB.SetActive.Url, value);
                }
                else
                {
                    ret = new NRestResult();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<TSB> SaveTSB(TSB value)
            {
                NRestResult<TSB> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<TSB>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<TSB>(RouteConsts.TSB.SaveTSB.Url, value);
                }
                else
                {
                    ret = new NRestResult<TSB>();
                    ret.ParameterIsNull();
                }
                return ret;
            }
            
            #endregion

            #region Plaza

            public NRestResult<List<Plaza>> GetTSBPlazas(TSB value)
            {
                NRestResult<List<Plaza>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<Plaza>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<Plaza>>(RouteConsts.TSB.GetTSBPlazas.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<Plaza>>();
                    ret.ParameterIsNull();
                    ret.data = new List<Plaza>();
                }
                return ret;
            }

            public NRestResult<Plaza> SavePlaza(Plaza value)
            {
                NRestResult<Plaza> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<Plaza>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<Plaza>(RouteConsts.TSB.SavePlaza.Url, value);
                }
                else
                {
                    ret = new NRestResult<Plaza>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            #endregion

            #region PlazaGroup

            public NRestResult<List<PlazaGroup>> GetTSBPlazaGroups(TSB value)
            {
                NRestResult<List<PlazaGroup>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<PlazaGroup>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<PlazaGroup>>(
                        RouteConsts.TSB.GetTSBPlazaGroups.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<PlazaGroup>>();
                    ret.ParameterIsNull();
                    ret.data = new List<PlazaGroup>();
                }
                return ret;
            }

            public NRestResult<PlazaGroup> SavePlazaGroup(PlazaGroup value)
            {
                NRestResult<PlazaGroup> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<PlazaGroup>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<PlazaGroup>(
                        RouteConsts.TSB.SavePlazaGroup.Url, value);
                }
                else
                {
                    ret = new NRestResult<PlazaGroup>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            #endregion

            #region Lane

            public NRestResult<List<Lane>> GetTSBLanes(TSB value)
            {
                NRestResult<List<Lane>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<Lane>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<Lane>>(
                        RouteConsts.TSB.GetTSBLanes.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<Lane>>();
                    ret.ParameterIsNull();
                    ret.data = new List<Lane>();
                }
                return ret;
            }

            public NRestResult<List<Lane>> GetPlazaLanes(Plaza value)
            {
                NRestResult<List<Lane>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<Lane>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<Lane>>(
                        RouteConsts.TSB.GetPlazaLanes.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<Lane>>();
                    ret.ParameterIsNull();
                    ret.data = new List<Lane>();
                }
                return ret;
            }

            public NRestResult<List<Lane>> GetPlazaGroupLanes(PlazaGroup value)
            {
                NRestResult<List<Lane>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<Lane>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<Lane>>(
                        RouteConsts.TSB.GetPlazaGroupLanes.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<Lane>>();
                    ret.ParameterIsNull();
                    ret.data = new List<Lane>();
                }
                return ret;
            }

            public NRestResult<Lane> SaveLane(Lane value)
            {
                NRestResult<Lane> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<Lane>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<Lane>(RouteConsts.TSB.SaveLane.Url, value);
                }
                else
                {
                    ret = new NRestResult<Lane>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
