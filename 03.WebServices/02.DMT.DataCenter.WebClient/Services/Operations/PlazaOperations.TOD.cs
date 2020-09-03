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
    partial class SCWOperations
    {
        #region Internal Variables

        private TODOperations _TOD_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets TOD Operations.
        /// </summary>
        public TODOperations TOD
        {
            get
            {
                if (null == _TOD_Ops)
                {
                    lock (this)
                    {
                        _TOD_Ops = new TODOperations();
                    }
                }
                return _TOD_Ops;
            }
        }

        #endregion

        #region TODOperations

        public class TODOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal TODOperations() : base() { }

            #endregion

            #region Public Methods

            #region Get Job List

            public SCWJobList GetJobList(
                int nwId, int pzId, string usrId)
            {
                SCWJobList ret;
                NRestClient client = NRestClient.CreateDCClient();
                if (null == client)
                {
                    ret = new SCWJobList();
                    return ret;
                }

                var url = "dmt-scw/api/tod/jobList";
                var value = new
                {
                    networkId = nwId,
                    plazaId = pzId,
                    staffId = usrId
                };

                string usr = SCWServiceOperations.Instance.UserName;
                string pwd = SCWServiceOperations.Instance.Password;

                ret = client.Execute2<SCWJobList>(url, value, username: usr, password: pwd);
                return ret;
            }

            #endregion

            #region Send Declare

            public SCWStatus Declare(SCWDeclare value)
            {
                SCWStatus ret;
                NRestClient client = NRestClient.CreateDCClient();
                if (null == client)
                {
                    ret = new SCWStatus();
                    ret.code = "NULL_CONN";
                    ret.message = "Client connection is null.";
                    return ret;
                }

                var url = "dmt-scw/api/tod/declare";

                string usr = SCWServiceOperations.Instance.UserName;
                string pwd = SCWServiceOperations.Instance.Password;

                ret = client.Execute2<SCWStatus>(url, value, username: usr, password: pwd);
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
