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

#endregion

using DMT.Models;
using DMT.Services;
using NLib.Reflection;
/*
using NLib.Controls.Utils;
using SQLiteNetExtensions.Extensions;
*/

namespace LocalDbServerFunctionTest
{
    public partial class Form1 : Form
    {
        #region Constructor

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Closing

        private void Form1_Load(object sender, EventArgs e)
        {
            LocalDbServer.Instance.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocalDbServer.Instance.Shutdown();
        }

        #endregion

        #region TSB Balance - Credit/Coupon/Addition/User transaction

        #region TSB Balance

        private void button1_Click(object sender, EventArgs e)
        {
            // TSB/Plaza/Lane - refresh
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Roles/Users - refresh
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // TSBCreditBalance - refresh
            pgTSBBalance.SelectedObject = TSBCreditBalance.GetCurrent();

            var tsb = TSB.GetCurrent();
            if (null != tsb)
            {
                lbActiveTSB3.Text = tsb.TSBNameTH;
            }
            else lbActiveTSB3.Text = "-";

            // load users
            var users = User.FindByRole("COLLECTOR");
            cbUsers.DataSource = users;
            cbUsers.DisplayMember = "FullNameTH";

            // load plaza groups
            if (null != tsb)
            {
                var plazaGroups = PlazaGroup.GetTSBPlazaGroups(tsb.TSBId);
                cbPlazaGroups.DataSource = plazaGroups;
                cbPlazaGroups.DisplayMember = "PlazaGroupNameTH";
            }
            else
            {
                cbPlazaGroups.DataSource = null;
                cbPlazaGroups.DisplayMember = "PlazaGroupNameTH";
            }
        }

        #endregion

        #region TSB Credit Transaction

        private void button4_Click_1(object sender, EventArgs e)
        {
            // TSB Credit Transaction - Init
            var inst = TSBCreditTransaction.Create();
            inst.TransactionDate = DateTime.Now;
            inst.TransactionType = TSBCreditTransaction.TransactionTypes.Initial;

            pgTSBCredit.SelectedObject = inst;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            // TSB Credit Transaction - Received (after request exchange success).
            var inst = TSBCreditTransaction.Create();
            inst.TransactionDate = DateTime.Now;
            inst.TransactionType = TSBCreditTransaction.TransactionTypes.Received;

            pgTSBCredit.SelectedObject = inst;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            // TSB Credit Transaction - Returns (after request exchange success).
            var inst = TSBCreditTransaction.Create();
            inst.TransactionDate = DateTime.Now;
            inst.TransactionType = TSBCreditTransaction.TransactionTypes.Returns;

            pgTSBCredit.SelectedObject = inst;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // TSB Credit Transaction - Save.
            var inst = pgTSBCredit.SelectedObject as TSBCreditTransaction;
            var tsb = TSB.GetCurrent();
            if (null == tsb)
            {
                return;
            }
            if (null != inst)
            {
                tsb.AssignTo(inst);
                TSBCreditTransaction.Save(inst);
            }
            pgTSBCredit.SelectedObject = null;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // TSB Credit Transaction - Refresh
            dgTSBCredit.DataSource = null;
            var tsb = TSB.GetCurrent();
            if (null == tsb)
            {
                return;
            }
            dgTSBCredit.DataSource = TSBCreditTransaction.Gets(tsb);
        }

        #endregion

        #region TSB Coupon Transaction

        private void button12_Click(object sender, EventArgs e)
        {
            // TSB Coupon Transaction - Init
            var inst = TSBCouponTransaction.Create();
            inst.TransactionDate = DateTime.Now;
            inst.TransactionType = TSBCouponTransaction.TransactionTypes.Initial;

            pgTSBCoupon.SelectedObject = inst;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            // TSB Coupon Transaction - Received
            var inst = TSBCouponTransaction.Create();
            inst.TransactionDate = DateTime.Now;
            inst.TransactionType = TSBCouponTransaction.TransactionTypes.Received;

            pgTSBCoupon.SelectedObject = inst;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // TSB Coupon Transaction - Save
            var inst = pgTSBCoupon.SelectedObject as TSBCouponTransaction;
            var tsb = TSB.GetCurrent();
            if (null == tsb)
            {
                return;
            }
            if (null != inst)
            {
                tsb.AssignTo(inst);
                TSBCouponTransaction.Save(inst);
            }
            pgTSBCoupon.SelectedObject = null;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // TSB Coupon Transaction - Refresh
            dgTSBCoupon.DataSource = null;
            var tsb = TSB.GetCurrent();
            if (null == tsb)
            {
                return;
            }
            dgTSBCoupon.DataSource = TSBCouponTransaction.Gets(tsb);
        }

        #endregion

        #region TSB Addition Transaction

        private void button15_Click(object sender, EventArgs e)
        {
            // TSB Addition Transaction - Init
            var inst = TSBAdditionTransaction.Create();
            inst.TransactionDate = DateTime.Now;
            inst.TransactionType = TSBAdditionTransaction.TransactionTypes.Initial;

            pgTSBAddition.SelectedObject = inst;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            // TSB Addition Transaction - Borrow
            var inst = TSBAdditionTransaction.Create();
            inst.TransactionDate = DateTime.Now;
            inst.TransactionType = TSBAdditionTransaction.TransactionTypes.Borrow;

            pgTSBAddition.SelectedObject = inst;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            // TSB Addition Transaction - Returns
            var inst = TSBAdditionTransaction.Create();
            inst.TransactionDate = DateTime.Now;
            inst.TransactionType = TSBAdditionTransaction.TransactionTypes.Returns;

            pgTSBAddition.SelectedObject = inst;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // TSB Addition Transaction - Save
            var inst = pgTSBAddition.SelectedObject as TSBAdditionTransaction;
            var tsb = TSB.GetCurrent();
            if (null == tsb)
            {
                return;
            }
            if (null != inst)
            {
                tsb.AssignTo(inst);
                TSBAdditionTransaction.Save(inst);
            }
            pgTSBAddition.SelectedObject = null;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // TSB Addition Transaction - Refresh
            dgTSBAddition.DataSource = null;
            var tsb = TSB.GetCurrent();
            if (null == tsb)
            {
                return;
            }
            dgTSBAddition.DataSource = TSBAdditionTransaction.Gets(tsb);
        }

        #endregion

        #region User Credit Transaction

        private void button4_Click(object sender, EventArgs e)
        {
            /*
            var inst = pgTSBBalance.SelectedObject as TSBBalance;
            if (null != inst)
            {
                TSBBalance.Save(inst);
            }
            */
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Create UserCredit
            var user = cbUsers.SelectedItem as User;
            var plazaGroup = cbPlazaGroups.SelectedItem as PlazaGroup;
            var bagNo = txtBagNo.Text;
            var beltNo = txtBeltNo.Text;
            if (null != user && null != plazaGroup && 
                !string.IsNullOrWhiteSpace(bagNo) &&
                !string.IsNullOrWhiteSpace(beltNo))
            {
                var usrCredit = UserCredit.Create(user, plazaGroup, bagNo, beltNo);
                pgUserCredit.SelectedObject = usrCredit;
            }
            else
            {
                pgUserCredit.SelectedObject = null;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*
            var tsbBalance = pgTSBBalance.SelectedObject as TSBBalance;
            var usrCredit = pgUserCredit.SelectedObject as UserCredit;
            if (null == tsbBalance || null == usrCredit) return;

            if (rbBorrow.Checked)
            {
                // Borrow.
                UserCredit.Borrow(usrCredit, tsbBalance);
                // refresh balance
                pgTSBBalance.SelectedObject = TSBBalance.GetCurrent();
            }
            else if (rbReturn.Checked)
            {
                // Return.
                UserCredit.Return(usrCredit, tsbBalance);
                // refresh balance
                pgTSBBalance.SelectedObject = TSBBalance.GetCurrent();
            }
            */
        }

        private void button7_Click(object sender, EventArgs e)
        {
            /*
            // Refresh Transaction.
            dgUserCredit.DataSource = null;

            var tsb = TSB.GetCurrent();
            // refresh grid
            dgUserCredit.DataSource = UserCredit.GetUserCredits(tsb);
            */
        }

        private void button8_Click(object sender, EventArgs e)
        {
            /*
            if (null != dgUserCredit.SelectedRows &&
                dgUserCredit.SelectedRows.Count > 0 &&
                null != dgUserCredit.SelectedRows[0])
            {
                var tsbBalance = pgTSBBalance.SelectedObject as TSBBalance;
                var usrCredit = dgUserCredit.SelectedRows[0].DataBoundItem as UserCredit;
                if (null == tsbBalance || null == usrCredit) return;

                UserCredit.Undo(usrCredit, tsbBalance);
                // refresh balance
                pgTSBBalance.SelectedObject = TSBBalance.GetCurrent();
                // refresh grid
                var tsb = TSB.GetCurrent();
                dgUserCredit.DataSource = UserCredit.GetUserCredits(tsb);
            }
            */
        }

        #endregion

        #endregion
    }
}
