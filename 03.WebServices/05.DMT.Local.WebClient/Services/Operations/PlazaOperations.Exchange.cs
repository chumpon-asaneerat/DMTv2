#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;

#endregion

namespace DMT.Services
{
    partial class LocalOperations
    {
        #region Internal Variables

        private ExchangeOperations _Exchange_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Exchanges Operations.
        /// </summary>
        public ExchangeOperations Exchanges
        {
            get
            {
                if (null == _Exchange_Ops)
                {
                    lock (this)
                    {
                        _Exchange_Ops = new ExchangeOperations();
                    }
                }
                return _Exchange_Ops;
            }
        }

        #endregion

        #region ExchangeOperations

        public class ExchangeOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal ExchangeOperations() : base() { }

            #endregion

            #region Public Methods

            #region Exchange Transaction

            public NRestResult<List<TSBExchangeGroup>> GetTSBExchangeGroups(TSB value)
            {
                NRestResult<List<TSBExchangeGroup>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<TSBExchangeGroup>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<TSBExchangeGroup>>(
                        RouteConsts.Exchange.GetTSBExchangeGroups.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<TSBExchangeGroup>>();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult<TSBExchangeGroup> SaveTSBExchangeGroup(
                TSBExchangeGroup value)
            {
                NRestResult<TSBExchangeGroup> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<TSBExchangeGroup>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<TSBExchangeGroup>(
                        RouteConsts.Exchange.SaveTSBExchangeGroup.Url, value);
                }
                else
                {
                    ret = new NRestResult<TSBExchangeGroup>();
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

    public class TSBExchangeManager
    {
        #region Internal Variables

        protected LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBExchangeManager() : base()
        {
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~TSBExchangeManager()
        {

        }

        #endregion

        #region Public Methods

        #region Save

        public void Save(TSBExchangeGroup value)
        {
            if (null != value)
            {
                ops.Exchanges.SaveTSBExchangeGroup(value);
            }
        }

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets TSB.
        /// </summary>
        public TSB TSB { get; set; }
        /// <summary>
        /// Gets or sets Supervisor.
        /// </summary>
        public User Supervisor { get; set; }

        /// <summary>
        /// Gets Request List.
        /// </summary>
        public List<TSBExchangeGroup> Requests
        {
            get
            {
                if (null == this.TSB)
                    return new List<TSBExchangeGroup>();

                var items = ops.Exchanges.GetTSBExchangeGroups(this.TSB).Value();
                if (null == items)
                    return new List<TSBExchangeGroup>();

                var results = items.FindAll(item =>
                {
                    bool ret = (
                        item.State == TSBExchangeGroup.StateTypes.Request &&
                        item.FinishFlag == TSBExchangeGroup.FinishedFlags.Avaliable
                    );
                    return ret;
                }).OrderBy(x => x.TransactionId).ToList();

                return results;
            }
        }

        #endregion
    }
}
