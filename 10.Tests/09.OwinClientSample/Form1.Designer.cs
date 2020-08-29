namespace OwinClientSample
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
            this.pgReq = new System.Windows.Forms.PropertyGrid();
            this.pgRes = new System.Windows.Forms.PropertyGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pgReq
            // 
            this.pgReq.Location = new System.Drawing.Point(12, 12);
            this.pgReq.Name = "pgReq";
            this.pgReq.Size = new System.Drawing.Size(314, 426);
            this.pgReq.TabIndex = 0;
            // 
            // pgRes
            // 
            this.pgRes.Location = new System.Drawing.Point(474, 12);
            this.pgRes.Name = "pgRes";
            this.pgRes.Size = new System.Drawing.Size(314, 426);
            this.pgRes.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(332, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 48);
            this.button1.TabIndex = 2;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pgRes);
            this.Controls.Add(this.pgReq);
            this.Name = "Form1";
            this.Text = "Owin Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgReq;
        private System.Windows.Forms.PropertyGrid pgRes;
        private System.Windows.Forms.Button button1;
    }
}

