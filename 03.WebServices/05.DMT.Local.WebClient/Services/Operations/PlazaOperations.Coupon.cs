#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using RestSharp;

using DMT.Models;
using DMT.Models.ExtensionMethods;

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
                NRestResult<TSBCouponBalance> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<TSBCouponBalance>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<TSBCouponBalance>(
                        RouteConsts.Coupon.GetTSBBalance.Url, value);
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
                NRestResult<List<TSBCouponSummary>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<TSBCouponSummary>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<TSBCouponSummary>>(
                        RouteConsts.Coupon.GetTSBCouponSummaries.Url, value);
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
                NRestResult<List<TSBCouponTransaction>> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<List<TSBCouponTransaction>>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<List<TSBCouponTransaction>>(
                        RouteConsts.Coupon.GetTSBCouponTransactions.Url, value);
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
                NRestResult<TSBCouponTransaction> ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult<TSBCouponTransaction>();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != value)
                {
                    ret = client.Execute<TSBCouponTransaction>(
                        RouteConsts.Coupon.SaveTSBCouponTransaction.Url, value);
                }
                else
                {
                    ret = new NRestResult<TSBCouponTransaction>();
                    ret.ParameterIsNull();
                    ret.data = null;
                }
                return ret;
            }

            public NRestResult SaveTransactions(List<TSBCouponTransaction> values)
            {
                NRestResult ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != values)
                {
                    ret = client.Execute<NRestResult>(
                        RouteConsts.Coupon.SaveTSBCouponTransactions.Url, values);
                }
                else
                {
                    ret = new NRestResult();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult SyncTransaction(TSBCouponTransaction value)
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
                    ret = client.Execute(
                        RouteConsts.Coupon.SyncTSBCouponTransaction.Url, value);
                }
                else
                {
                    ret = new NRestResult();
                    ret.ParameterIsNull();
                }
                return ret;
            }

            public NRestResult SyncTransactions(List<TSBCouponTransaction> values)
            {
                NRestResult ret;
                NRestClient client = NRestClient.CreateLocalClient();
                if (null == client)
                {
                    ret = new NRestResult();
                    ret.RestInvalidConfig();
                    return ret;
                }

                if (null != values)
                {
                    ret = client.Execute(
                        RouteConsts.Coupon.SyncTSBCouponTransactions.Url, values);
                }
                else
                {
                    ret = new NRestResult();
                    ret.ParameterIsNull();
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
        private TAxTODOperations server = TODxTAServiceOperations.Instance.Plaza;

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

        /// <summary>
        /// Refresh with auto sync data from TAxTOD server to local database 
        /// before reload new coupon list.
        /// </summary>
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
                Sync(); // auto sync when refresh.

                Summaries = ops.Coupons.GetTSBCouponSummaries(tsb).Value();
                // get original list.
                _origins = ops.Coupons.GetTSBCouponTransactions(tsb).Value();
                // get work list.
                _coupons = ops.Coupons.GetTSBCouponTransactions(tsb).Value();
            }
        }

        #endregion

        #region For Borrow/Return between Stock-Lane

        /// <summary>
        /// Update Coupon Transaction - Assigned to User/Lane.
        /// </summary>
        /// <param name="value">The TSB coupon transaction.</param>
        public void Borrow(TSBCouponTransaction value)
        {
            if (null == User) return;
            if (value.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
            value.TransactionType = TSBCouponTransaction.TransactionTypes.Lane;
            value.UserId = User.UserId;
            value.FullNameEN = User.FullNameEN;
            value.FullNameTH = User.FullNameTH;
            value.UserReceiveDate = DateTime.Now;
        }
        /// <summary>
        /// Update Coupon Transaction - Return coupon (restore back to stock).
        /// </summary>
        /// <param name="value">The TSB coupon transaction.</param>
        public void Return(TSBCouponTransaction value)
        {
            if (value.TransactionType != TSBCouponTransaction.TransactionTypes.Lane) return;
            value.TransactionType = TSBCouponTransaction.TransactionTypes.Stock;
            value.UserId = string.Empty;
            value.FullNameEN = string.Empty;
            value.FullNameTH = string.Empty;
            value.UserReceiveDate = DateTime.MinValue;
        }

        #endregion

        #region For Lane Sold/Unsold Coupon (User on hand)

        /// <summary>
        /// Update Coupon Transaction - Sold user coupon.
        /// </summary>
        /// <param name="value">The TSB coupon transaction.</param>
        public void SoldByLane(TSBCouponTransaction value)
        {
            //if (null == User) return;
            if (value.TransactionType != TSBCouponTransaction.TransactionTypes.Lane) return;
            value.TransactionType = TSBCouponTransaction.TransactionTypes.SoldByLane;
            value.SoldBy = User.UserId;
            value.SoldByFullNameEN = User.FullNameEN;
            value.SoldByFullNameTH = User.FullNameTH;
            value.SoldDate = DateTime.Now;
            //value.UserReceiveDate = DateTime.Now;
        }
        /// <summary>
        /// Update Coupon Transaction - Unsold user coupon (restore back to Lane).
        /// </summary>
        /// <param name="value">The TSB coupon transaction.</param>
        public void UnsoldByLane(TSBCouponTransaction value)
        {
            //if (null == User) return;
            if (value.TransactionType != TSBCouponTransaction.TransactionTypes.SoldByLane) return;
            value.TransactionType = TSBCouponTransaction.TransactionTypes.Lane;
            value.SoldBy = string.Empty;
            value.SoldByFullNameEN = string.Empty;
            value.SoldByFullNameTH = string.Empty;
            value.SoldDate = DateTime.MinValue;
            //value.UserReceiveDate = DateTime.MinValue;
        }

        #endregion

        #region For Lane Sold/Unsold Coupon (TSB)

        /// <summary>
        /// Update Coupon Transaction - Sold by TSB.
        /// </summary>
        /// <param name="value">The TSB coupon transaction.</param>
        public void SoldByTSB(TSBCouponTransaction value)
        {
            //if (null == User) return;
            if (value.TransactionType != TSBCouponTransaction.TransactionTypes.Stock) return;
            value.TransactionType = TSBCouponTransaction.TransactionTypes.SoldByTSB;
            value.SoldBy = User.UserId;
            value.SoldByFullNameEN = User.FullNameEN;
            value.SoldByFullNameTH = User.FullNameTH;
            value.SoldDate = DateTime.Now;
            //value.UserReceiveDate = DateTime.Now;
        }
        /// <summary>
        /// Update Coupon Transaction - Unsold by TSB (restored back to stock).
        /// </summary>
        /// <param name="value">The TSB coupon transaction.</param>
        public void UnsoldByTSB(TSBCouponTransaction value)
        {
            //if (null == User) return;
            if (value.TransactionType != TSBCouponTransaction.TransactionTypes.SoldByTSB) return;
            value.TransactionType = TSBCouponTransaction.TransactionTypes.Stock;
            value.SoldBy = User.UserId;
            value.SoldByFullNameEN = User.FullNameEN;
            value.SoldByFullNameTH = User.FullNameTH;
            value.SoldDate = DateTime.MinValue;
        }

        #endregion

        #region Save

        /// <summary>
        /// Save all transactions (auto sync after saved).
        /// </summary>
        public void Save()
        {
            if (null != _coupons)
            {
                var saveCoupons = new List<TSBCouponTransaction>();
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
                            if (coupon.TransactionType == TSBCouponTransaction.TransactionTypes.Stock)
                            {
                                coupon.UserId = string.Empty; // force clear when return to stock.
                                coupon.FullNameEN = string.Empty;
                                coupon.FullNameTH = string.Empty;
                            }
                            saveCoupons.Add(coupon);
                        }
                    }
                });

                ops.Coupons.SaveTransactions(saveCoupons);

                if (null != server)
                {
                    // TODO: Need to Wrap with transaction. Server need to implements save array.
                    saveCoupons.ForEach(coupon =>
                    {
                        var cp = coupon.ToServer();
                        server.Coupons.SaveTransaction(cp);
                    });
                }
            }
        }

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Gets Coupon Summaries (TSB).
        /// </summary>
        public List<TSBCouponSummary> Summaries { get; private set; }
        /// <summary>
        /// Gets Coupon Transactions (TSB).
        /// </summary>
        public List<TSBCouponTransaction> Coupons
        {
            get
            {
                if (null == _coupons)
                    return new List<TSBCouponTransaction>();
                return _coupons;
            }
        }

        #region For Borrow/Return between Stock-Lane

        /// <summary>
        /// Gets Coupon 35 BHT Transactions (TSB) in Stock.
        /// </summary>
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
        /// <summary>
        /// Gets Coupon 80 BHT Transactions (TSB) in Stock.
        /// </summary>
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
        /// <summary>
        /// Gets Coupon 35 BHT Transactions (TSB) on Lane.
        /// </summary>
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
        /// <summary>
        /// Gets Coupon 80 BHT Transactions (TSB) on Lane.
        /// </summary>
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

        /// <summary>
        /// Gets Coupon 35 BHT Transactions (TSB) Sold By Lane.
        /// </summary>
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
        /// <summary>
        /// Gets Coupon 80 BHT Transactions (TSB) Sold By Lane.
        /// </summary>
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
        /// <summary>
        /// Gets Coupon 35 BHT Transactions (TSB) on hand (user).
        /// </summary>
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
        /// <summary>
        /// Gets Coupon 80 BHT Transactions (TSB) on hand (user).
        /// </summary>
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

        #region For TSB Sold Coupon

        /// <summary>
        /// Gets Coupon 35 BHT Transactions (TSB) Sold.
        /// </summary>
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
        /// <summary>
        /// Gets Coupon 80 BHT Transactions (TSB) Sold.
        /// </summary>
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

        #region Static Methods

        /// <summary>
        /// Sync data from TAxTOD Web Service.
        /// </summary>
        public static void Sync()
        {
            var ops = LocalServiceOperations.Instance.Plaza;
            var server = TODxTAServiceOperations.Instance.Plaza;
            TSB tsb = ops.TSB.GetCurrent().Value();
            if (null != tsb && null != server)
            {
                var ret = server.Coupons.GetTAServerCouponTransactions(
                    tsb.TSBId, null, null, null);
                if (ret.Ok)
                {
                    var serverCoupons = ret.Value();
                    if (null != serverCoupons)
                    {
                        ops.Coupons.SyncTransactions(serverCoupons.ToLocals());
                    }
                }
            }
        }

        #endregion
    }

    #endregion
}
