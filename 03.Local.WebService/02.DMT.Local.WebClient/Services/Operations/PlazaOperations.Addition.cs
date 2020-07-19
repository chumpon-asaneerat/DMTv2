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
    partial class PlazaOperations
    {
        #region Internal Variables

        private AdditionOperations _Addition_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Additions Operations.
        /// </summary>
        public AdditionOperations Additions
        {
            get
            {
                if (null == _Addition_Ops)
                {
                    lock (this)
                    {
                        _Addition_Ops = new AdditionOperations();
                    }
                }
                return _Addition_Ops;
            }
        }

        #endregion

        #region AdditionOperations

        public class AdditionOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal AdditionOperations() : base() { }

            #endregion

            #region Public Methods

            public TSBAdditionTransaction GetCurrentInitial()
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBAdditionTransaction>(
                    RouteConsts.Addition.GetCurrentInitial.Url, new { });
                return ret;
            }

            public TSBAdditionTransaction GetInitial(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBAdditionTransaction>(
                    RouteConsts.Addition.GetInitial.Url, tsb);
                return ret;
            }

            public void SaveTransaction(TSBAdditionTransaction value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Addition.SaveTransaction.Url, value);
            }

            /*
            public TSAdditionBalance GetCurrent()
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSAdditionBalance>(
                    RouteConsts.Addition.GetCurrent.Url, new { });
                return ret;
            }

            public TSAdditionBalance GetTSBCurrent(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSAdditionBalance>(
                    RouteConsts.Addition.GetTSBCurrent.Url, tsb);
                return ret;
            }
            */

            #endregion
        }

        #endregion
    }
}
