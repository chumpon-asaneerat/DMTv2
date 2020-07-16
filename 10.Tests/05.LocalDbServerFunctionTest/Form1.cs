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
/*
using NLib.Controls.Utils;
using SQLiteNetExtensions.Extensions;
*/

namespace LocalDbServerFunctionTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LocalDbServer.Instance.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocalDbServer.Instance.Shutdown();
        }

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
            // Credits/Coupons - refresh
            pgTSBBalance.SelectedObject = TSBBalance.GetCurrent();

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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var inst = pgTSBBalance.SelectedObject as TSBBalance;
            if (null != inst)
            {
                TSBBalance.Save(inst);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Create UserCredit
            var user = cbUsers.SelectedItem as User;
            if (null != user)
            {
                var usrCredit = UserCredit.Create(user);
                pgUserCredit.SelectedObject = usrCredit;
            }
            else
            {
                pgUserCredit.SelectedObject = null;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
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
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Refresh Transaction.
            dgUserCredit.DataSource = null;

            var tsb = TSB.GetCurrent();
            // refresh grid
            dgUserCredit.DataSource = UserCredit.GetUserCredits(tsb);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (null != dgUserCredit.SelectedRows && null != dgUserCredit.SelectedRows[0])
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
        }
    }
}
