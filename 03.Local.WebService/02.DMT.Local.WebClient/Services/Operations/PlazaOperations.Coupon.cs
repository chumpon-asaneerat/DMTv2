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

            public TSBCouponBalance GetTSBBalance(TSB value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<TSBCouponBalance>(
                    RouteConsts.Coupon.GetTSBBalance.Url, value);
                return ret;
            }

            #endregion

            #region TSB Coupon Summary

            public List<TSBCouponSummary> GetTSBCouponSummaries(TSB value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponSummary>>(
                    RouteConsts.Coupon.GetTSBCouponSummaries.Url, value);
                return ret;
            }

            #endregion

            #region TSB Coupon Transaction

            public List<TSBCouponTransaction> GetTSBCouponTransactions(TSB value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.GetTSBCouponTransactions.Url, value);
                return ret;
            }

            public void SaveTransaction(TSBCouponTransaction value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Coupon.SaveTSBCouponTransaction.Url, value);
            }

            #endregion

            /*
            public List<TSBCouponTransaction> GetCurrentTSBCoupons()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.GetCurrentTSBCoupons.Url, new { });
                return ret;
            }

            public List<TSBCouponTransaction> GetTSBCoupons(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.GetTSBCoupons.Url, tsb);
                return ret;
            }

            public List<TSBCouponTransaction> GetTSBBHT35Coupons(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.GetTSBBHT35Coupons.Url, tsb);
                return ret;
            }

            public List<TSBCouponTransaction> GetTSBBHT80Coupons(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.GetTSBBHT80Coupons.Url, tsb);
                return ret;
            }

            public List<TSBCouponTransaction> ToTSBBHT35Coupons(Search.UserCoupons.ToTSBCoupons value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.ToTSBBHT35Coupons.Url, value);
                return ret;
            }

            public List<TSBCouponTransaction> ToTSBBHT80Coupons(Search.UserCoupons.ToTSBCoupons value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.ToTSBBHT80Coupons.Url, value);
                return ret;
            }

            public List<UserCouponTransaction> GetUserBHT35Coupons(
                Search.UserCoupons.ByUser value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<UserCouponTransaction>>(
                    RouteConsts.Coupon.GetUserBHT35Coupons.Url, value);
                return ret;
            }

            public List<UserCouponTransaction> GetUserBHT80Coupons(
                Search.UserCoupons.ByUser value)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<UserCouponTransaction>>(
                    RouteConsts.Coupon.GetUserBHT80Coupons.Url, value);
                return ret;
            }

            public List<TSBCouponTransaction> GetCurrentTSBSoldCoupons()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.GetCurrentTSBSoldCoupons.Url, new { });
                return ret;
            }

            public List<TSBCouponTransaction> GetTSBSoldCoupons(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponTransaction>>(
                    RouteConsts.Coupon.GetTSBSoldCoupons.Url, tsb);
                return ret;
            }

            public void SaveTransaction(TSBCouponTransaction value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Coupon.SaveTransaction.Url, value);
            }

            public List<TSBCouponBalance> GetCurrent()
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponBalance>>(
                    RouteConsts.Coupon.GetCurrent.Url, new { });
                return ret;
            }

            public List<TSBCouponBalance> GetTSBCurrent(TSB tsb)
            {
                var ret = NRestClient.Create(port: 9000).Execute<List<TSBCouponBalance>>(
                    RouteConsts.Coupon.GetTSBCurrent.Url, tsb);
                return ret;
            }

            public void UserBorrowCoupons(Search.UserCoupons.BorrowCoupons value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Coupon.UserBorrowCoupons.Url, value);
            }

            public void UserReturnCoupons(Search.UserCoupons.ReturnCoupons value)
            {
                NRestClient.Create(port: 9000).Execute(
                    RouteConsts.Coupon.UserReturnCoupons.Url, value);
            }
            */
            #endregion
        }

        #endregion
    }

    public class TSBCouponManager
    {
        #region Internal Variables

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;

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

        public void Refresh()
        {
            TSB tsb = null;
            Summaries = ops.Coupons.GetTSBCouponSummaries(tsb);
            // get original list.
            _origins = ops.Coupons.GetTSBCouponTransactions(tsb);
            // get work list.
            _coupons = ops.Coupons.GetTSBCouponTransactions(tsb);
        }

        public User User { get; set; }
        public List<TSBCouponSummary> Summaries { get; private set; }

        public List<TSBCouponTransaction> C35Stocks
        {
            get
            {
                if (null == _coupons)
                    return new List<TSBCouponTransaction>();
                return _coupons.FindAll(item =>
                {
                    bool ret = (
                        (item.TransactionType == TSBCouponTransaction.TransactionTypes.Stock ||
                        item.TransactionType == TSBCouponTransaction.TransactionTypes.SoldByLane) &&
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
                        (item.TransactionType == TSBCouponTransaction.TransactionTypes.Stock ||
                        item.TransactionType == TSBCouponTransaction.TransactionTypes.SoldByLane) &&
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
                if (null == _coupons && null == User)
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

        public List<TSBCouponTransaction> C80Users
        {
            get
            {
                if (null == _coupons && null == User)
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

        public List<TSBCouponTransaction> C35UserSolds
        {
            get
            {
                if (null == _coupons && null == User)
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
                if (null == _coupons && null == User)
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

        public void Borrow(TSBCouponTransaction value)
        {
            value.TransactionType = TSBCouponTransaction.TransactionTypes.Lane;
            value.UserId = User.UserId;
            value.UserReceiveDate = DateTime.Now;
        }

        public void Return(TSBCouponTransaction value)
        {
            value.TransactionType = TSBCouponTransaction.TransactionTypes.Stock;
            value.UserId = string.Empty;
            value.UserReceiveDate = DateTime.MinValue;
        }

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
                            origin.UserId != coupon.UserId)
                        {
                            ops.Coupons.SaveTransaction(coupon);
                        }
                    }
                });
            }
        }

        #endregion
    }
}
