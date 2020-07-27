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

namespace LocalDbServerFunctionTest2
{
    public partial class Form1 : Form
    {
        #region Constructur

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
    }
}
