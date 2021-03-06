﻿namespace MD5Sample
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtOri = new System.Windows.Forms.TextBox();
            this.cmdEncrypt = new System.Windows.Forms.Button();
            this.txtOut1 = new System.Windows.Forms.TextBox();
            this.rbASCII = new System.Windows.Forms.RadioButton();
            this.rbUTF8v1 = new System.Windows.Forms.RadioButton();
            this.rbUTF8v2 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Original";
            // 
            // txtOri
            // 
            this.txtOri.Location = new System.Drawing.Point(15, 81);
            this.txtOri.Name = "txtOri";
            this.txtOri.Size = new System.Drawing.Size(502, 22);
            this.txtOri.TabIndex = 2;
            this.txtOri.Enter += new System.EventHandler(this.txtOri_Enter);
            // 
            // cmdEncrypt
            // 
            this.cmdEncrypt.Location = new System.Drawing.Point(523, 76);
            this.cmdEncrypt.Name = "cmdEncrypt";
            this.cmdEncrypt.Size = new System.Drawing.Size(125, 33);
            this.cmdEncrypt.TabIndex = 4;
            this.cmdEncrypt.Text = "Encrypt";
            this.cmdEncrypt.UseVisualStyleBackColor = true;
            this.cmdEncrypt.Click += new System.EventHandler(this.cmdEncrypt_Click);
            // 
            // txtOut1
            // 
            this.txtOut1.Location = new System.Drawing.Point(15, 109);
            this.txtOut1.Name = "txtOut1";
            this.txtOut1.ReadOnly = true;
            this.txtOut1.Size = new System.Drawing.Size(502, 22);
            this.txtOut1.TabIndex = 6;
            // 
            // rbASCII
            // 
            this.rbASCII.AutoSize = true;
            this.rbASCII.Checked = true;
            this.rbASCII.Location = new System.Drawing.Point(15, 12);
            this.rbASCII.Name = "rbASCII";
            this.rbASCII.Size = new System.Drawing.Size(62, 21);
            this.rbASCII.TabIndex = 8;
            this.rbASCII.TabStop = true;
            this.rbASCII.Text = "ASCII";
            this.rbASCII.UseVisualStyleBackColor = true;
            // 
            // rbUTF8v1
            // 
            this.rbUTF8v1.AutoSize = true;
            this.rbUTF8v1.Location = new System.Drawing.Point(101, 12);
            this.rbUTF8v1.Name = "rbUTF8v1";
            this.rbUTF8v1.Size = new System.Drawing.Size(83, 21);
            this.rbUTF8v1.TabIndex = 9;
            this.rbUTF8v1.Text = "UTF8 v1";
            this.rbUTF8v1.UseVisualStyleBackColor = true;
            // 
            // rbUTF8v2
            // 
            this.rbUTF8v2.AutoSize = true;
            this.rbUTF8v2.Location = new System.Drawing.Point(208, 12);
            this.rbUTF8v2.Name = "rbUTF8v2";
            this.rbUTF8v2.Size = new System.Drawing.Size(83, 21);
            this.rbUTF8v2.TabIndex = 10;
            this.rbUTF8v2.Text = "UTF8 v2";
            this.rbUTF8v2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 171);
            this.Controls.Add(this.rbUTF8v2);
            this.Controls.Add(this.rbUTF8v1);
            this.Controls.Add(this.rbASCII);
            this.Controls.Add(this.txtOut1);
            this.Controls.Add(this.cmdEncrypt);
            this.Controls.Add(this.txtOri);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Test MD5";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOri;
        private System.Windows.Forms.Button cmdEncrypt;
        private System.Windows.Forms.TextBox txtOut1;
        private System.Windows.Forms.RadioButton rbASCII;
        private System.Windows.Forms.RadioButton rbUTF8v1;
        private System.Windows.Forms.RadioButton rbUTF8v2;
    }
}

