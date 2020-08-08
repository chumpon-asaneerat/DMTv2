using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DMT.Smartcard;

namespace SmartcardM1Test
{
    public partial class Form1 : Form
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private void InitSmartcardHandlers()
        {
            // Init Event Handlers.
            SmartcardService.OnIdle += SmartcardService_OnIdle;
            SmartcardService.OnCardReadSerial += SmartcardService_OnCardReadSerial;
            SmartcardService.OnCardReadBlock += SmartcardService_OnCardReadBlock;
        }

        private void FreeSmartcardHandlers()
        {
            // Release Event Handlers.
            SmartcardService.OnCardReadBlock -= SmartcardService_OnCardReadBlock;
            SmartcardService.OnCardReadSerial -= SmartcardService_OnCardReadSerial;
            SmartcardService.OnIdle -= SmartcardService_OnIdle;
        }

        private void InitSmartcardService()
        {
            // Read both serial and block.
            SmartcardService.ReadSerialNoOnly = false;
            // Set Secure Key.
            SmartcardService.SecureKey = SL600SDK.DefaultKey;
            // Init all handlers
            InitSmartcardHandlers();
        }

        private void FreeSmartcardService()
        {
            // Required when close program to prevent process halt.
            SmartcardService.Shutdown();
            // Free all handlers
            FreeSmartcardHandlers();
        }

        private void StartService()
        {
            SmartcardService.Start();
        }

        private void ShutdownService()
        {
            SmartcardService.Shutdown();
        }

        #endregion

        #region UI Event Handlers

        #region Load/Closing

        private void Form1_Load(object sender, EventArgs e)
        {
            InitSmartcardService();
            // Set Buttons enable state.
            cmdStart.Enabled = true;
            cmdStop.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FreeSmartcardService();
        }

        #endregion

        #region SmartcardService Event Handlers

        private void SmartcardService_OnIdle(object sender, EventArgs e)
        {
            lbCardExist.ForeColor = Color.Red;
            lbCardExist.Text = "Card not avaliable.";
            lbSN.Text = "-";
            lbBlock0.Text = "Block 0: ";
            lbBlock1.Text = "Block 1: ";
            lbBlock2.Text = "Block 2: ";
            lbBlock3.Text = "Block 3: ";
        }

        private void SmartcardService_OnCardReadSerial(object sender, M1CardReadSerialEventArgs e)
        {
            lbCardExist.ForeColor = Color.Green;
            lbCardExist.Text = "Card avaliable.";
            lbSN.Text = e.SerialNo;
        }

        private void SmartcardService_OnCardReadBlock(object sender, M1CardReadBlockEventArgs e)
        {
            lbBlock0.Text = "Block 0: " + e.Block0;
            lbBlock1.Text = "Block 1: " + e.Block1;
            lbBlock2.Text = "Block 2: " + e.Block2;
            lbBlock3.Text = "Block 3: " + e.Block3;
        }

        #endregion

        #region CheckBox Handlers

        private void chkReadBoth_CheckedChanged(object sender, EventArgs e)
        {
            SmartcardService.ReadSerialNoOnly = !chkReadBoth.Checked;
        }

        #endregion

        #region Button Handlers

        private void cmdStart_Click(object sender, EventArgs e)
        {
            // start listen port.
            StartService();
            // Set Buttons enable state.
            cmdStart.Enabled = false;
            cmdStop.Enabled = true;
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            // stop listen port.
            ShutdownService();
            // Set Buttons enable state.
            cmdStart.Enabled = true;
            cmdStop.Enabled = false;
        }

        #endregion

        #endregion
    }
}
