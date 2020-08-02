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
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Gets Plaza Groups
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Gets Plazas
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Gets Lanes
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Gets TSB Credits
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Gets User Credits
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Gets TSB Coupons.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Gets User Coupons.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Gets TSB Exchanges.
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

        private void button14_Click(object sender, EventArgs e)
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
    }
}
