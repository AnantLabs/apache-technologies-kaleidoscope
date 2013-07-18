namespace Kaleidoscope
{
    partial class CGlobalThresholdDlg
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbPercentage = new System.Windows.Forms.TrackBar();
            this.tbThreshold = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radPercent = new System.Windows.Forms.RadioButton();
            this.radThreshold = new System.Windows.Forms.RadioButton();
            this.lblPercent = new System.Windows.Forms.Label();
            this.lblThreshold = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbThreshold)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(132, 136);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(66, 26);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(236, 136);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbPercentage
            // 
            this.tbPercentage.Location = new System.Drawing.Point(93, 65);
            this.tbPercentage.Maximum = 100;
            this.tbPercentage.Name = "tbPercentage";
            this.tbPercentage.Size = new System.Drawing.Size(279, 45);
            this.tbPercentage.TabIndex = 8;
            this.tbPercentage.TickFrequency = 10;
            this.tbPercentage.Scroll += new System.EventHandler(this.tbPercent_Scroll);
            // 
            // tbThreshold
            // 
            this.tbThreshold.Location = new System.Drawing.Point(93, 20);
            this.tbThreshold.Maximum = 255;
            this.tbThreshold.Name = "tbThreshold";
            this.tbThreshold.Size = new System.Drawing.Size(279, 45);
            this.tbThreshold.TabIndex = 7;
            this.tbThreshold.TickFrequency = 20;
            this.tbThreshold.Scroll += new System.EventHandler(this.tbThreashold_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radPercent);
            this.groupBox1.Controls.Add(this.radThreshold);
            this.groupBox1.Controls.Add(this.lblPercent);
            this.groupBox1.Controls.Add(this.lblThreshold);
            this.groupBox1.Controls.Add(this.tbThreshold);
            this.groupBox1.Controls.Add(this.tbPercentage);
            this.groupBox1.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 116);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // radPercent
            // 
            this.radPercent.AutoSize = true;
            this.radPercent.Location = new System.Drawing.Point(7, 67);
            this.radPercent.Name = "radPercent";
            this.radPercent.Size = new System.Drawing.Size(88, 19);
            this.radPercent.TabIndex = 12;
            this.radPercent.TabStop = true;
            this.radPercent.Text = "Percentage";
            this.radPercent.UseVisualStyleBackColor = true;
            this.radPercent.CheckedChanged += new System.EventHandler(this.radPercent_CheckedChanged);
            // 
            // radThreshold
            // 
            this.radThreshold.AutoSize = true;
            this.radThreshold.Location = new System.Drawing.Point(6, 22);
            this.radThreshold.Name = "radThreshold";
            this.radThreshold.Size = new System.Drawing.Size(80, 19);
            this.radThreshold.TabIndex = 11;
            this.radThreshold.TabStop = true;
            this.radThreshold.Text = "Threshold";
            this.radThreshold.UseVisualStyleBackColor = true;
            this.radThreshold.CheckedChanged += new System.EventHandler(this.radThreshold_CheckedChanged);
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(375, 67);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(15, 15);
            this.lblPercent.TabIndex = 11;
            this.lblPercent.Text = "P";
            // 
            // lblThreshold
            // 
            this.lblThreshold.AutoSize = true;
            this.lblThreshold.Location = new System.Drawing.Point(375, 22);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(14, 15);
            this.lblThreshold.TabIndex = 10;
            this.lblThreshold.Text = "T";
            // 
            // CGlobalThresholdDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(435, 174);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Ivory;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CGlobalThresholdDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Global Threshold";
            ((System.ComponentModel.ISupportInitialize)(this.tbPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbThreshold)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TrackBar tbPercentage;
        private System.Windows.Forms.TrackBar tbThreshold;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Label lblThreshold;
        private System.Windows.Forms.RadioButton radThreshold;
        private System.Windows.Forms.RadioButton radPercent;
    }
}