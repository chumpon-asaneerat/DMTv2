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
            dbgTSB.DataSource = TSB.Gets();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Gets Plaza Groups
            dbgPlazaGroup.DataSource = PlazaGroup.Gets();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Gets Plazas
            dbgPlaza.DataSource = Plaza.Gets();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Gets Lanes
            dbgLane.DataSource = Lane.Gets();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Gets TSB Credits
            dbgTSBCredit.DataSource = TSBCreditTransaction.Gets();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // Init TSB Credit.
            TSBCreditTransaction inst = TSBCreditTransaction.GetInitial();
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

            // Gets TSB Credits
            dbgTSBCredit.DataSource = TSBCreditTransaction.Gets();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Gets User Credits
            dbgUserCredit.DataSource = UserCreditTransaction.Gets();
        }


        private void button20_Click(object sender, EventArgs e)
        {
            var tsb = TSB.GetCurrent();
            if (null == tsb) return;
            // Init TSB Coupons.
            string book35Range = "630001-631000";
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
                    item.Factor = 665;

                    TSBCouponTransaction.Save(item);
                }
            }
            string book80Range = "631001-632000";
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
                    item.Factor = 1520;

                    TSBCouponTransaction.Save(item);
                }
            }

            // Gets TSB Coupons.
            dbgTSBCoupon.DataSource = TSBCouponTransaction.Gets();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Gets TSB Coupons (Received).
            dbgTSBCoupon.DataSource = TSBCouponTransaction.GetTSBCoupons();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Gets Sold Coupons.
            dbgTSBCoupon.DataSource = TSBCouponTransaction.GetTSBSoldCoupons();
        }

        private void button19_Click(object sender, EventArgs e)
        {
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

                if (item.TransactionType == TSBCouponTransaction.TransactionTypes.User ||
                    item.TransactionType == TSBCouponTransaction.TransactionTypes.Sold)
                {
                    idx = rand.Next(tsbCoupons.Count);
                    item = tsbCoupons[idx];
                }
                else
                {
                    borrows.Add(tsbCoupons[idx]);
                    i++;
                }
            }
            while (i < 10);

            var search = Search.UserCoupons.BorrowCoupons.Create(user, borrows);
            UserCouponTransaction.UserBorrowCoupons(search.User, search.Coupons);

            // Gets TSB Coupons (Received).
            dbgTSBCoupon.DataSource = TSBCouponTransaction.GetTSBCoupons();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Gets User Coupons.
            dbgUserCoupon.DataSource = UserCouponTransaction.Gets();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // Gets User 35 Coupons.
            dbgUserCoupon.DataSource = UserCouponTransaction.GetBHT35Coupons();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Gets User 80 Coupons.
            dbgUserCoupon.DataSource = UserCouponTransaction.GetBHT80Coupons();
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

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
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
