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
            this.SuspendLayout();
            // 
            // lbBlock3
            // 
            this.lbBlock3.AutoSize = true;
            this.lbBlock3.Location = new System.Drawing.Point(109, 198);
            this.lbBlock3.Name = "lbBlock3";
            this.lbBlock3.Size = new System.Drawing.Size(62, 17);
            this.lbBlock3.TabIndex = 15;
            this.lbBlock3.Text = "Block 0 :";
            // 
            // lbBlock2
            // 
            this.lbBlock2.AutoSize = true;
            this.lbBlock2.Location = new System.Drawing.Point(109, 168);
            this.lbBlock2.Name = "lbBlock2";
            this.lbBlock2.Size = new System.Drawing.Size(62, 17);
            this.lbBlock2.TabIndex = 14;
            this.lbBlock2.Text = "Block 0 :";
            // 
            // lbBlock1
            // 
            this.lbBlock1.AutoSize = true;
            this.lbBlock1.Location = new System.Drawing.Point(109, 141);
            this.lbBlock1.Name = "lbBlock1";
            this.lbBlock1.Size = new System.Drawing.Size(62, 17);
            this.lbBlock1.TabIndex = 13;
            this.lbBlock1.Text = "Block 0 :";
            // 
            // lbBlock0
            // 
            this.lbBlock0.AutoSize = true;
            this.lbBlock0.Location = new System.Drawing.Point(109, 114);
            this.lbBlock0.Name = "lbBlock0";
            this.lbBlock0.Size = new System.Drawing.Size(62, 17);
            this.lbBlock0.TabIndex = 12;
            this.lbBlock0.Text = "Block 0 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Card Date : ";
            // 
            // lbCardExist
            // 
            this.lbCardExist.AutoSize = true;
            this.lbCardExist.Location = new System.Drawing.Point(109, 82);
            this.lbCardExist.Name = "lbCardExist";
            this.lbCardExist.Size = new System.Drawing.Size(62, 17);
            this.lbCardExist.TabIndex = 10;
            this.lbCardExist.Text = "No card.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 82);
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
            this.label2.Location = new System.Drawing.Point(20, 9);
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
            this.label3.Location = new System.Drawing.Point(20, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(367, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "The USB auto detected currenty on development.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 245);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbBlock3);
            this.Controls.Add(this.lbBlock2);
            this.Controls.Add(this.lbBlock1);
            this.Controls.Add(this.lbBlock0);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbCardExist);
            this.Controls.Add(this.label1);
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
    }
}

