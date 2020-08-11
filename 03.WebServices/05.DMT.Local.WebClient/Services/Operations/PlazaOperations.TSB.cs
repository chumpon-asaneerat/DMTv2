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
                NRestClient.WebProtocol protocol = 
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ? 
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;
                var ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                    .Execute<List<TSB>>(
                    RouteConsts.TSB.GetTSBs.Url, new { });
                return ret;
            }

            public NRestResult<TSB> GetCurrent()
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;
                var ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                    .Execute<TSB>(RouteConsts.TSB.GetCurrent.Url, new { });
                return ret;
            }

            public NRestResult SetActive(TSB value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute(RouteConsts.TSB.SetActive.Url, value);
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
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<TSB> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<TSB>(RouteConsts.TSB.SaveTSB.Url, value);
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
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<List<Plaza>> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<List<Plaza>>(RouteConsts.TSB.GetTSBPlazas.Url, value);
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
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<Plaza> ret;
                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<Plaza>(RouteConsts.TSB.SavePlaza.Url, value);
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
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<List<PlazaGroup>> ret;

                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<List<PlazaGroup>>(RouteConsts.TSB.GetTSBPlazaGroups.Url, value);
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
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<PlazaGroup> ret;

                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<PlazaGroup>(RouteConsts.TSB.SavePlazaGroup.Url, value);
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

            public NRestResult<List<Lane>> GetTSBLanes(TSB tsb)
            {
                var ret = NRestClient.Create(host: hostName, port: portNo).Execute<List<Lane>>(
                    RouteConsts.TSB.GetTSBLanes.Url, tsb);
                return ret;
            }

            public NRestResult<List<Lane>> GetPlazaLanes(Plaza plaza)
            {
                var ret = NRestClient.Create(host: hostName, port: portNo).Execute<List<Lane>>(
                    RouteConsts.TSB.GetPlazaLanes.Url, plaza);
                return ret;
            }

            public NRestResult<List<Lane>> GetPlazaGroupLanes(PlazaGroup plazaGroup)
            {
                var ret = NRestClient.Create(host: hostName, port: portNo).Execute<List<Lane>>(
                    RouteConsts.TSB.GetPlazaGroupLanes.Url, plazaGroup);
                return ret;
            }

            public NRestResult<Lane> SaveLane(Lane value)
            {
                if (null != value)
                {
                    var ret = NRestClient.Create(host: hostName, port: portNo).Execute<Lane>(
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
