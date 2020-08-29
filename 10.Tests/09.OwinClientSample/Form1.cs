﻿#region Using

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

namespace OwinClientSample
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
            pgReq.SelectedObject = new { Name = "" };
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var req = pgReq.SelectedObject;
            pgRes.SelectedObject = null;
            // send

            pgRes.SelectedObject = null;
        }

        #endregion

        #region Button Handlers

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        #endregion
    }
}
