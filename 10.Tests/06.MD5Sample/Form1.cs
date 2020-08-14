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

namespace MD5Sample
{
    public partial class Form1 : Form
    {
        #region Constructor

        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdEncrypt_Click(object sender, EventArgs e)
        {
            string ori = txtOri.Text.Trim();
            if (string.IsNullOrEmpty(ori))
            {
                txtOri.Focus();
            }
        }

        private void cmdDecrypt_Click(object sender, EventArgs e)
        {
            string md5 = txtMD5.Text.Trim();
            if (string.IsNullOrEmpty(md5))
            {
                txtMD5.Focus();
            }
        }

        #endregion

        #region TextBox Handlers

        private void txtOri_Enter(object sender, EventArgs e)
        {
            txtOri.SelectAll();
        }

        private void txtMD5_Enter(object sender, EventArgs e)
        {
            txtMD5.SelectAll();
        }

        #endregion
    }
}
