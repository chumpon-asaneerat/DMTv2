namespace SmartcardM1Test
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbBlock3 = new System.Windows.Forms.Label();
            this.lbBlock2 = new System.Windows.Forms.Label();
            this.lbBlock1 = new System.Windows.Forms.Label();
            this.lbBlock0 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbCardExist = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbSN = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkReadBoth = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbBlock3
            // 
            this.lbBlock3.AutoSize = true;
            this.lbBlock3.Location = new System.Drawing.Point(123, 240);
            this.lbBlock3.Name = "lbBlock3";
            this.lbBlock3.Size = new System.Drawing.Size(62, 17);
            this.lbBlock3.TabIndex = 15;
            this.lbBlock3.Text = "Block 0 :";
            // 
            // lbBlock2
            // 
            this.lbBlock2.AutoSize = true;
            this.lbBlock2.Location = new System.Drawing.Point(123, 210);
            this.lbBlock2.Name = "lbBlock2";
            this.lbBlock2.Size = new System.Drawing.Size(62, 17);
            this.lbBlock2.TabIndex = 14;
            this.lbBlock2.Text = "Block 0 :";
            // 
            // lbBlock1
            // 
            this.lbBlock1.AutoSize = true;
            this.lbBlock1.Location = new System.Drawing.Point(123, 183);
            this.lbBlock1.Name = "lbBlock1";
            this.lbBlock1.Size = new System.Drawing.Size(62, 17);
            this.lbBlock1.TabIndex = 13;
            this.lbBlock1.Text = "Block 0 :";
            // 
            // lbBlock0
            // 
            this.lbBlock0.AutoSize = true;
            this.lbBlock0.Location = new System.Drawing.Point(123, 156);
            this.lbBlock0.Name = "lbBlock0";
            this.lbBlock0.Size = new System.Drawing.Size(62, 17);
            this.lbBlock0.TabIndex = 12;
            this.lbBlock0.Text = "Block 0 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Card Date : ";
            // 
            // lbCardExist
            // 
            this.lbCardExist.AutoSize = true;
            this.lbCardExist.Location = new System.Drawing.Point(123, 100);
            this.lbCardExist.Name = "lbCardExist";
            this.lbCardExist.Size = new System.Drawing.Size(62, 17);
            this.lbCardExist.TabIndex = 10;
            this.lbCardExist.Text = "No card.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Card Exist : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(100, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(378, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Please Plug USB device before run this application";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(100, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(367, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "The USB auto detected currenty on development.";
            // 
            // lbSN
            // 
            this.lbSN.AutoSize = true;
            this.lbSN.Location = new System.Drawing.Point(123, 127);
            this.lbSN.Name = "lbSN";
            this.lbSN.Size = new System.Drawing.Size(13, 17);
            this.lbSN.TabIndex = 19;
            this.lbSN.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Card SN : ";
            // 
            // chkReadBoth
            // 
            this.chkReadBoth.AutoSize = true;
            this.chkReadBoth.Checked = true;
            this.chkReadBoth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReadBoth.Location = new System.Drawing.Point(371, 100);
            this.chkReadBoth.Name = "chkReadBoth";
            this.chkReadBoth.Size = new System.Drawing.Size(166, 21);
            this.chkReadBoth.TabIndex = 20;
            this.chkReadBoth.Text = "Read Serial and Data";
            this.chkReadBoth.UseVisualStyleBackColor = true;
            this.chkReadBoth.CheckedChanged += new System.EventHandler(this.chkReadBoth_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 294);
            this.Controls.Add(this.chkReadBoth);
            this.Controls.Add(this.lbSN);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbBlock3);
            this.Controls.Add(this.lbBlock2);
            this.Controls.Add(this.lbBlock1);
            this.Controls.Add(this.lbBlock0);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbCardExist);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "SL600 - Smartcard M1 Card Reader Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbBlock3;
        private System.Windows.Forms.Label lbBlock2;
        private System.Windows.Forms.Label lbBlock1;
        private System.Windows.Forms.Label lbBlock0;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbCardExist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbSN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkReadBoth;
    }
}

