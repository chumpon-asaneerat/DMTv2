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
            //pgTSBBalance.SelectedObject = TSBBalance.GetCurrent();
        }
    }
}
