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

            public TSBAdditionTransaction GetInitial()
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBAdditionTransaction>(
                    RouteConsts.Addition.GetInitial.Url);
                return ret;
            }

            #endregion
        }

        #endregion
    }
}
