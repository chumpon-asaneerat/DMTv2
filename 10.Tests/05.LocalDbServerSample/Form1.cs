#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NLib;
using DMT.Models;
using DMT.Services;
using DMT;

#endregion

namespace LocalDbServerSample
{
    public partial class Form1 : Form
    {
        #region Constructor

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Load/Closing

        private void Form1_Load(object sender, EventArgs e)
        {
            LocalDbServer.Instance.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocalDbServer.Instance.Shutdown();
        }

        #endregion

        #region Button Handlers

        private void button6_Click(object sender, EventArgs e)
        {
            // Gets TSBs
            var ret = TSB.Gets();
            var ds = (null != ret && !ret.errors.hasError) ? ret.data : null;
            dbgTSB.DataSource = ds;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Gets Plaza Groups
            var ret = PlazaGroup.Gets();
            var ds = (null != ret && !ret.errors.hasError) ? ret.data : null;
            dbgPlazaGroup.DataSource = ds;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Gets Plazas
            var ret = Plaza.Gets();
            var ds = (null != ret && !ret.errors.hasError) ? ret.data : null;
            dbgPlaza.DataSource = ds;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Gets Lanes
            var ret = Lane.Gets();
            var ds = (null != ret && !ret.errors.hasError) ? ret.data : null;
            dbgLane.DataSource = ds;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Gets TSB Credits
            var ret = TSBCreditTransaction.Gets();
            var ds = (null != ret && !ret.errors.hasError) ? ret.data : null;
            dbgTSBCredit.DataSource = ds;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // Init TSB Credit.
            var ret = TSBCreditTransaction.GetInitialTransaction();
            TSBCreditTransaction inst = (null != ret && !ret.errors.hasError) ? ret.data : null;
            if (null != inst)
            {
                inst.TransactionDate = DateTime.Now;
                inst.CountBHT1 = 300;
                inst.CountBHT2 = 300;
                inst.CountBHT5 = 500;
                inst.CountBHT10 = 300;
                inst.CountBHT20 = 200;
                inst.CountBHT50 = 200;
                inst.CountBHT100 = 100;
                inst.CountBHT500 = 50;
                inst.CountBHT1000 = 30;
                TSBCreditTransaction.Save(inst);
            }
            // Gets TSB Credits
            var ret2 = TSBCreditTransaction.Gets();
            var ds = (null != ret2 && !ret2.errors.hasError) ? ret2.data : null;
            dbgTSBCredit.DataSource = ds;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Gets User Credits
            dbgUserCredit.DataSource = UserCreditTransaction.GetUserCreditTransactions();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            var ret = TSB.GetCurrent();
            var tsb = (null != ret && !ret.errors.hasError) ? ret.data : null;
            if (null == tsb) return;
            // Init TSB Coupons.
            string book35Range = "630001-630010";
            var coupon35Ids = book35Range.ParseRange(0, 999999);
            if (null != coupon35Ids)
            {
                coupon35Ids = coupon35Ids.Distinct();
                foreach (var id in coupon35Ids)
                {
                    TSBCouponTransaction item = new TSBCouponTransaction();
                    item.TransactionDate = DateTime.Now;
                    item.TSBId = tsb.TSBId;

                    item.CouponId = "ข" + id.ToString("D6");
                    item.CouponType = CouponType.BHT35;
                    item.Price = 665;

                    TSBCouponTransaction.Save(item);
                }
            }
            string book80Range = "630051-630060";
            var coupon80Ids = book80Range.ParseRange(0, 999999);
            if (null != coupon80Ids)
            {
                coupon80Ids = coupon80Ids.Distinct();
                foreach (var id in coupon80Ids)
                {
                    TSBCouponTransaction item = new TSBCouponTransaction();
                    item.TransactionDate = DateTime.Now;
                    item.TSBId = tsb.TSBId;

                    item.CouponId = "C" + id.ToString("D6");
                    item.CouponType = CouponType.BHT80;
                    item.Price = 1520;

                    TSBCouponTransaction.Save(item);
                }
            }

            // Gets TSB Coupons.
            //dbgTSBCoupon.DataSource = TSBCouponTransaction.Gets();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Gets TSB Coupons (Received).
            var ret = TSBCouponTransaction.GetTSBCouponTransactions();
            var coupons = (null != ret && !ret.errors.hasError) ? ret.data : null;
            dbgTSBCoupon.DataSource = null;
            if (null != coupons)
            {
                dbgTSBCoupon.DataSource = coupons.Find(x => 
                {
                    return x.TransactionType == TSBCouponTransaction.TransactionTypes.Stock;
                });
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Gets Sold Coupons.
            var ret = TSBCouponTransaction.GetTSBCouponTransactions();
            var coupons = (null != ret && !ret.errors.hasError) ? ret.data : null;
            dbgTSBCoupon.DataSource = null;
            if (null != coupons)
            {
                dbgTSBCoupon.DataSource = coupons.Find(x =>
                {
                    return (
                        x.TransactionType == TSBCouponTransaction.TransactionTypes.SoldByLane &&
                        x.TransactionType == TSBCouponTransaction.TransactionTypes.SoldByTSB
                    );
                });
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            /*
            // User Borrow Coupons
            var user = User.Get("14211");
            var tsbCoupons = TSBCouponTransaction.GetTSBCoupons();
            var borrows = new List<TSBCouponTransaction>();
            Random rand = new Random();
            int idx = rand.Next(tsbCoupons.Count);
            var item = tsbCoupons[idx];
            int i = 0;
            do
            {
                if (null == item) continue;

                if (item.TransactionType != TSBCouponTransaction.TransactionTypes.Stock)
                {
                    idx = rand.Next(tsbCoupons.Count);
                    item = tsbCoupons[idx];
                }
                else
                {
                    var coupon = tsbCoupons[idx];
                    // update transaction type.
                    coupon.TransactionType = TSBCouponTransaction.TransactionTypes.Lane;
                    borrows.Add(coupon);
                    i++;
                }
            }
            while (i < 3);

            var search = Search.UserCoupons.BorrowCoupons.Create(user, borrows);
            UserCouponTransaction.UserBorrowCoupons(search.User, search.Coupons);

            // Gets TSB Coupons (Received).
            dbgTSBCoupon.DataSource = TSBCouponTransaction.GetTSBCoupons();
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Gets User Coupons.
            dbgUserCoupon.DataSource = UserCouponTransaction.Gets();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // Gets User 35 Coupons.
            var ret = User.Get("14211");
            var user = (null != ret && !ret.errors.hasError) ? ret.data : null;
            var src = UserCouponTransaction.GetUserBHT35Coupons(user);
            var ds = (null != src && !src.errors.hasError) ? src.data : null;
            dbgUserCoupon.DataSource = ds;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Gets User 80 Coupons.
            var ret = User.Get("14211");
            var user = (null != ret && !ret.errors.hasError) ? ret.data : null;
            var src = UserCouponTransaction.GetUserBHT80Coupons(user);
            var ds = (null != src && !src.errors.hasError) ? src.data : null;
            dbgUserCoupon.DataSource = ds;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            // Sold
            var dbgrid = dbgUserCoupon;

            if (null == dbgrid.SelectedRows || dbgrid.SelectedRows.Count <= 0) return;
            var item = dbgrid.SelectedRows[0].DataBoundItem as UserCouponTransaction;
            if (null != item)
            {
                UserCouponTransaction.Sold(item);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            /*
            // Returns
            var dbgrid = dbgUserCoupon;

            if (null == dbgrid.SelectedRows || dbgrid.SelectedRows.Count <= 0) return;
            var item = dbgrid.SelectedRows[0].DataBoundItem as UserCouponTransaction;
            if (null != item)
            {
                User user = new User();
                user.UserId = item.UserId;
                item.TransactionType = UserCouponTransaction.TransactionTypes.Return;
                UserCouponTransaction.Save(item);

                var coupon = TSBCouponTransaction.FindById(item.CouponId);                
                if (null != coupon)
                {
                    var coupons = new List<TSBCouponTransaction>();
                    coupons.Add(coupon);
                    UserCouponTransaction.UserReturnCoupons(user, coupons);
                }
            }
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Gets TSB Exchanges.
            //dbgTSBExchange.DataSource = TSBExchangeTransaction.Gets();
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region DataGrid Handlers

        private void dbgTSB_SelectionChanged(object sender, EventArgs e)
        {
            var dbgrid = dbgTSB;
            var pgrid = pgTSB;

            pgrid.SelectedObject = null;
            if (null == dbgrid.SelectedRows || dbgrid.SelectedRows.Count <= 0) return;
            pgrid.SelectedObject = dbgrid.SelectedRows[0].DataBoundItem;
        }

        private void dbgPlazaGroup_SelectionChanged(object sender, EventArgs e)
        {
            var dbgrid = dbgPlazaGroup;
            var pgrid = pgPlazaGroup;

            pgrid.SelectedObject = null;
            if (null == dbgrid.SelectedRows || dbgrid.SelectedRows.Count <= 0) return;
            pgrid.SelectedObject = dbgrid.SelectedRows[0].DataBoundItem;
        }

        private void dbgPlaza_SelectionChanged(object sender, EventArgs e)
        {
            var dbgrid = dbgPlaza;
            var pgrid = pgPlaza;

            pgrid.SelectedObject = null;
            if (null == dbgrid.SelectedRows || dbgrid.SelectedRows.Count <= 0) return;
            pgrid.SelectedObject = dbgrid.SelectedRows[0].DataBoundItem;
        }

        private void dbgLane_SelectionChanged(object sender, EventArgs e)
        {
            var dbgrid = dbgLane;
            var pgrid = pgLane;

            pgrid.SelectedObject = null;
            if (null == dbgrid.SelectedRows || dbgrid.SelectedRows.Count <= 0) return;
            pgrid.SelectedObject = dbgrid.SelectedRows[0].DataBoundItem;
        }

        private void dbgTSBCredit_SelectionChanged(object sender, EventArgs e)
        {
            var dbgrid = dbgTSBCredit;
            var pgrid = pgTSBCredit;

            pgrid.SelectedObject = null;
            if (null == dbgrid.SelectedRows || dbgrid.SelectedRows.Count <= 0) return;
            pgrid.SelectedObject = dbgrid.SelectedRows[0].DataBoundItem;
        }

        private void dbgUserCredit_SelectionChanged(object sender, EventArgs e)
        {
            var dbgrid = dbgUserCredit;
            var pgrid = pgUserCredit;

            pgrid.SelectedObject = null;
            if (null == dbgrid.SelectedRows || dbgrid.SelectedRows.Count <= 0) return;
            pgrid.SelectedObject = dbgrid.SelectedRows[0].DataBoundItem;
        }

        private void dbgTSBCoupon_SelectionChanged(object sender, EventArgs e)
        {
            var dbgrid = dbgTSBCoupon;
            var pgrid = pgTSBCoupon;

            pgrid.SelectedObject = null;
            if (null == dbgrid.SelectedRows || dbgrid.SelectedRows.Count <= 0) return;
            pgrid.SelectedObject = dbgrid.SelectedRows[0].DataBoundItem;
        }

        private void dbgUserCoupon_SelectionChanged(object sender, EventArgs e)
        {
            var dbgrid = dbgUserCoupon;
            var pgrid = pgUserCoupon;

            pgrid.SelectedObject = null;
            if (null == dbgrid.SelectedRows || dbgrid.SelectedRows.Count <= 0) return;
            pgrid.SelectedObject = dbgrid.SelectedRows[0].DataBoundItem;
        }

        private void dbgTSBExchange_SelectionChanged(object sender, EventArgs e)
        {
            var dbgrid = dbgTSBExchange;
            var pgrid = pgTSBExchange;

            pgrid.SelectedObject = null;
            if (null == dbgrid.SelectedRows || dbgrid.SelectedRows.Count <= 0) return;
            pgrid.SelectedObject = dbgrid.SelectedRows[0].DataBoundItem;
        }

        #endregion
    }
}
