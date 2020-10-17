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

        private MasterOperations _Master_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Master Operations.
        /// </summary>
        public MasterOperations Master
        {
            get
            {
                if (null == _Master_Ops)
                {
                    lock (this)
                    {
                        _Master_Ops = new MasterOperations();
                    }
                }
                return _Master_Ops;
            }
        }

        #endregion

        #region MasterOperations class

        /// <summary>
        /// The TSB Operations class.
        /// Used for Manage TSB, Plaza and Lane's operation(s).
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

            #region MCurrency

            public NRestResult<List<MCurrency>> GetCurrencies()
            {
                NRestResult<List<MCurrency>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<MCurrency>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<List<MCurrency>>(
                    RouteConsts.Master.GetCurrencies.Url, new { });
                return ret;
            }

            public NRestResult SaveMCurrencies(List<MCurrency> values)
            {
                NRestResult ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute(
                    RouteConsts.Master.SaveMCurrencies.Url, values);
                return ret;
            }

            #endregion

            #region MCoupon

            public NRestResult<List<MCoupon>> GetCoupons()
            {
                NRestResult<List<MCoupon>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<MCoupon>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<List<MCoupon>>(
                    RouteConsts.Master.GetCoupons.Url, new { });
                return ret;
            }

            public NRestResult SaveMCoupons(List<MCoupon> values)
            {
                NRestResult ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute(
                    RouteConsts.Master.SaveMCoupons.Url, values);
                return ret;
            }

            #endregion

            #region MCardAllow

            public NRestResult<List<MCardAllow>> GetCardAllows()
            {
                NRestResult<List<MCardAllow>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<MCardAllow>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute<List<MCardAllow>>(
                    RouteConsts.Master.GetCardAllows.Url, new { });
                return ret;
            }

            public NRestResult SaveMCardAllows(List<MCardAllow> values)
            {
                NRestResult ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult();
                    ret.RestInvalidConfig();
                    return ret;
                }

                ret = client.Execute(
                    RouteConsts.Master.SaveMCardAllows.Url, values);
                return ret;
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
