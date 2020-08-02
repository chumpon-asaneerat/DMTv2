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

        private void button1_Click(object sender, EventArgs e)
        {
            // Gets TSB Coupons.
            dbgTSBCoupon.DataSource = TSBCouponTransaction.Gets();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Gets User Coupons.
            //dbgUserCoupon.DataSource = UserCouponTransaction.Gets();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Gets TSB Exchanges.
            //dbgTSBExchange.DataSource = TSBExchangeTransaction.Gets();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

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

        private void button19_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
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
