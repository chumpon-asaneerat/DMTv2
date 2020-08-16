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

        private CouponOperations _Coupon_Ops = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Coupons Operations.
        /// </summary>
        public CouponOperations Coupons
        {
            get
            {
                if (null == _Coupon_Ops)
                {
                    lock (this)
                    {
                        _Coupon_Ops = new CouponOperations();
                    }
                }
                return _Coupon_Ops;
            }
        }

        #endregion

        #region CouponOperations

        public class CouponOperations
        {
            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            internal CouponOperations() : base() { }

            #endregion

            #region Public Methods

            #region TSB Coupon Balance

            public NRestResult<TSBCouponBalance> GetTSBBalance(TSB value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<TSBCouponBalance> ret;

                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<TSBCouponBalance>(RouteConsts.Coupon.GetTSBBalance.Url, value);
                }
                else
                {
                    ret = new NRestResult<TSBCouponBalance>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            #endregion

            #region TSB Coupon Summary

            public NRestResult<List<TSBCouponSummary>> GetTSBCouponSummaries(TSB value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<List<TSBCouponSummary>> ret;

                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<List<TSBCouponSummary>>(RouteConsts.Coupon.GetTSBCouponSummaries.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<TSBCouponSummary>>();
                    ret.ParameterIsNull();
                    ret.data = new List<TSBCouponSummary>();
                }
                return ret;
            }

            #endregion

            #region TSB Coupon Transaction

            public NRestResult<List<TSBCouponTransaction>> GetTSBCouponTransactions(TSB value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<List<TSBCouponTransaction>> ret;

                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<List<TSBCouponTransaction>>(RouteConsts.Coupon.GetTSBCouponTransactions.Url, value);
                }
                else
                {
                    ret = new NRestResult<List<TSBCouponTransaction>>();
                    ret.ParameterIsNull();
                    ret.data = new List<TSBCouponTransaction>();
                }
                return ret;
            }

            public NRestResult<TSBCouponTransaction> SaveTransaction(TSBCouponTransaction value)
            {
                NRestClient.WebProtocol protocol =
                    (AppConsts.WindowsService.Local.WebServer.Protocol == "http") ?
                    NRestClient.WebProtocol.http : NRestClient.WebProtocol.https;
                string hostName = AppConsts.WindowsService.Local.WebServer.HostName;
                int portNo = AppConsts.WindowsService.Local.WebServer.PortNumber;

                NRestResult<TSBCouponTransaction> ret;

                if (null != value)
                {
                    ret = NRestClient.Create(protocol: protocol, host: hostName, port: portNo)
                        .Execute<TSBCouponTransaction>(RouteConsts.Coupon.SaveTSBCouponTransaction.Url, value);
                }
                else
                {
                    ret = new NRestResult<TSBCouponTransaction>();
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

    #region TSBCouponManager

    public class TSBCouponManager
    {
        #region Internal Variables

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        private List<TSBCouponTransaction> _origins = null;
        private List<TSBCouponTransaction> _coupons = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBCouponManager() : base() 
        { 
        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~TSBCouponManager()
        {

        }

        #endregion

        #region Public Methods

        #region Refresh

        public void Refresh()
        {
            TSB tsb = ops.TSB.GetCurrent().Value();
            if (null == tsb)
            {
                Summaries = new List<TSBCouponSummary>();
                _origins = new List<TSBCouponTransaction>();
                _coupons = new List<TSBCouponTransaction>();
            }
            else
            {
                Summaries = ops.Coupons.GetTSBCouponSummaries(tsb).Value();
                // get original list.
                _origins = ops.Coupons.GetTSBCouponTransactions(tsb).Value();
                // get work list.
                _coupons = ops.Coupons.GetTSBCouponTransactions(tsb).Value();
            }
        }

        #endregion

        #region For Borrow/Return between Stock-Lane

        public void Borrow(TSBCouponTransaction value)
        {
            if (null == User) return;
            if (value.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
            value.TransactionType = TSBCouponTransaction.TransactionTypes.Lane;
            value.UserId = User.UserId;
            value.UserReceiveDate = DateTime.Now;
        }

        public void Return(TSBCouponTransaction value)
        {
            if (value.TransactionType != TSBCouponTransaction.TransactionTypes.Lane) return;
            value.TransactionType = TSBCouponTransaction.TransactionTypes.Stock;
            value.UserId = string.Empty;
            value.UserReceiveDate = DateTime.MinValue;
        }

        #endregion

        #region For Lane Sold/Unsold Coupon (User on hand)

        public void SoldByLane(TSBCouponTransaction value)
        {
            //if (null == User) return;
            if (value.TransactionType != TSBCouponTransaction.TransactionTypes.Lane) return;
            value.TransactionType = TSBCouponTransaction.TransactionTypes.SoldByLane;
            //value.UserId = User.UserId;
            //value.UserReceiveDate = DateTime.Now;
        }

        public void UnsoldByLane(TSBCouponTransaction value)
        {
            //if (null == User) return;
            if (value.TransactionType != TSBCouponTransaction.TransactionTypes.SoldByLane) return;
            value.TransactionType = TSBCouponTransaction.TransactionTypes.Lane;
            //value.UserId = string.Empty;
            //value.UserReceiveDate = DateTime.MinValue;
        }

        #endregion

        #region For Lane Sold/Unsold Coupon (TSB)

        public void SoldByTSB(TSBCouponTransaction value)
        {
            //if (null == User) return;
            if (value.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
            value.TransactionType = TSBCouponTransaction.TransactionTypes.SoldByTSB;
            value.SoldBy = User.UserId;
            value.SoldDate = DateTime.Now;
            //value.UserReceiveDate = DateTime.Now;
        }

        public void UnsoldByTSB(TSBCouponTransaction value)
        {
            //if (null == User) return;
            if (value.TransactionType != TSBCouponTransaction.TransactionTypes.SoldByTSB) return;
            value.TransactionType = TSBCouponTransaction.TransactionTypes.Stock;
            value.SoldBy = User.UserId;
            value.SoldDate = DateTime.MinValue;
        }

        #endregion

        #region Save

        public void Save()
        {
            if (null != _coupons)
            {
                _coupons.ForEach(coupon =>
                {
                    var origin = (null != _origins) ? _origins.Find(x => x.CouponId == coupon.CouponId) : null;
                    if (null != origin)
                    {
                        if (origin.TransactionType != coupon.TransactionType ||
                            origin.UserId != coupon.UserId ||
                            origin.SoldBy != coupon.UserId ||
                            origin.FinishFlag != coupon.FinishFlag)
                        {
                            ops.Coupons.SaveTransaction(coupon);
                        }
                    }
                });
            }
        }

        #endregion

        #endregion

        #region Public Properties

        public User User { get; set; }
        public List<TSBCouponSummary> Summaries { get; private set; }

        #region For Borrow/Return between Stock-Lane

        public List<TSBCouponTransaction> C35Stocks
        {
            get
            {
                if (null == _coupons)
                    return new List<TSBCouponTransaction>();
                return _coupons.FindAll(item =>
                {
                    bool ret = (
                        item.TransactionType == TSBCouponTransaction.TransactionTypes.Stock &&
                        item.CouponType == CouponType.BHT35
                    );
                    return ret;
                }).OrderBy(x => x.CouponId).ToList();
            }
        }

        public List<TSBCouponTransaction> C80Stocks
        {
            get
            {
                if (null == _coupons)
                    return new List<TSBCouponTransaction>();
                return _coupons.FindAll(item =>
                {
                    bool ret = (
                        item.TransactionType == TSBCouponTransaction.TransactionTypes.Stock &&
                        item.CouponType == CouponType.BHT80
                    );
                    return ret;
                }).OrderBy(x => x.CouponId).ToList();
            }
        }

        public List<TSBCouponTransaction> C35Users
        {
            get
            {
                if (null == _coupons || null == User)
                    return new List<TSBCouponTransaction>();
                return _coupons.FindAll(item =>
                {
                    bool ret = (
                        (item.TransactionType == TSBCouponTransaction.TransactionTypes.Lane ||
                        item.TransactionType == TSBCouponTransaction.TransactionTypes.SoldByLane) &&
                        item.CouponType == CouponType.BHT35 &&
                        item.UserId == User.UserId
                    );
                    return ret;
                }).OrderBy(x => x.CouponId).ToList();
            }
        }

        public List<TSBCouponTransaction> C80Users
        {
            get
            {
                if (null == _coupons || null == User)
                    return new List<TSBCouponTransaction>();
                return _coupons.FindAll(item =>
                {
                    bool ret = (
                        (item.TransactionType == TSBCouponTransaction.TransactionTypes.Lane ||
                        item.TransactionType == TSBCouponTransaction.TransactionTypes.SoldByLane) &&
                        item.CouponType == CouponType.BHT80 &&
                        item.UserId == User.UserId
                    );
                    return ret;
                }).OrderBy(x => x.CouponId).ToList();
            }
        }

        #endregion

        #region For Lane Sold/Unsold Coupon (User on hand)

        public List<TSBCouponTransaction> C35UserSolds
        {
            get
            {
                if (null == _coupons || null == User)
                    return new List<TSBCouponTransaction>();
                return _coupons.FindAll(item =>
                {
                    bool ret = (
                        item.TransactionType == TSBCouponTransaction.TransactionTypes.SoldByLane &&
                        item.CouponType == CouponType.BHT35 &&
                        item.UserId == User.UserId
                    );
                    return ret;
                }).OrderBy(x => x.CouponId).ToList();
            }
        }

        public List<TSBCouponTransaction> C80UserSolds
        {
            get
            {
                if (null == _coupons || null == User)
                    return new List<TSBCouponTransaction>();
                return _coupons.FindAll(item =>
                {
                    bool ret = (
                        item.TransactionType == TSBCouponTransaction.TransactionTypes.SoldByLane &&
                        item.CouponType == CouponType.BHT80 &&
                        item.UserId == User.UserId
                    );
                    return ret;
                }).OrderBy(x => x.CouponId).ToList();
            }
        }

        public List<TSBCouponTransaction> C35UserOnHands
        {
            get
            {
                if (null == _coupons || null == User)
                    return new List<TSBCouponTransaction>();
                return _coupons.FindAll(item =>
                {
                    bool ret = (
                        item.TransactionType == TSBCouponTransaction.TransactionTypes.Lane &&
                        item.CouponType == CouponType.BHT35 &&
                        item.UserId == User.UserId
                    );
                    return ret;
                }).OrderBy(x => x.CouponId).ToList();
            }
        }

        public List<TSBCouponTransaction> C80UserOnHands
        {
            get
            {
                if (null == _coupons || null == User)
                    return new List<TSBCouponTransaction>();
                return _coupons.FindAll(item =>
                {
                    bool ret = (
                        item.TransactionType == TSBCouponTransaction.TransactionTypes.Lane &&
                        item.CouponType == CouponType.BHT80 &&
                        item.UserId == User.UserId
                    );
                    return ret;
                }).OrderBy(x => x.CouponId).ToList();
            }
        }

        #endregion

        #region For Lane Sold/Unsold Coupon (TSB)

        public List<TSBCouponTransaction> C35TSBSolds
        {
            get
            {
                if (null == _coupons || null == User)
                    return new List<TSBCouponTransaction>();
                return _coupons.FindAll(item =>
                {
                    bool ret = (
                        item.TransactionType == TSBCouponTransaction.TransactionTypes.SoldByTSB &&
                        item.CouponType == CouponType.BHT35 &&
                        item.SoldBy == User.UserId
                    );
                    return ret;
                }).OrderBy(x => x.CouponId).ToList();
            }
        }

        public List<TSBCouponTransaction> C80TSBSolds
        {
            get
            {
                if (null == _coupons || null == User)
                    return new List<TSBCouponTransaction>();
                return _coupons.FindAll(item =>
                {
                    bool ret = (
                        item.TransactionType == TSBCouponTransaction.TransactionTypes.SoldByTSB &&
                        item.CouponType == CouponType.BHT80 &&
                        item.SoldBy == User.UserId
                    );
                    return ret;
                }).OrderBy(x => x.CouponId).ToList();
            }
        }

        #endregion

        #endregion
    }

    #endregion
}
