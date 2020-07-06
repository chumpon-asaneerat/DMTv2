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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set secure key.
            SmartcardService.SecureKey = SL600SDK.DefaultKey;

            // Init Event Handlers.
            SmartcardService.OnIdle += SmartcardService_OnIdle;
            SmartcardService.OnCardRead += SmartcardService_OnCardRead;
            
            SmartcardService.Start(); // Start listening Smartcard device (USB).
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Release Event Handlers.
            SmartcardService.OnCardRead -= SmartcardService_OnCardRead;
            SmartcardService.OnIdle -= SmartcardService_OnIdle;

            SmartcardService.Shutdown(); // Required when close program to prevent process halt.
        }

        private void SmartcardService_OnIdle(object sender, EventArgs e)
        {
            lbCardExist.ForeColor = Color.Red;
            lbCardExist.Text = "Card not avaliable.";
            lbBlock0.Text = "Block 0: ";
            lbBlock1.Text = "Block 1: ";
            lbBlock2.Text = "Block 2: ";
            lbBlock3.Text = "Block 3: ";
        }

        private void SmartcardService_OnCardRead(object sender, M1CardReadEventArgs e)
        {
            lbCardExist.ForeColor = Color.Green;
            lbCardExist.Text = "Card avaliable.";
            lbBlock0.Text = "Block 0: " + e.Block0;
            lbBlock1.Text = "Block 1: " + e.Block1;
            lbBlock2.Text = "Block 2: " + e.Block2;
            lbBlock3.Text = "Block 3: " + e.Block3;
        }
    }
}
