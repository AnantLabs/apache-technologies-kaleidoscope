namespace Kaleidoscope
{
    partial class CFuzzySegmentDlg
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblHistogram = new System.Windows.Forms.Label();
            this.tbHistogram = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblZeroCrossing = new System.Windows.Forms.Label();
            this.lblHighT = new System.Windows.Forms.Label();
            this.lblLowT = new System.Windows.Forms.Label();
            this.tbLowT = new System.Windows.Forms.TrackBar();
            this.tbZeroCrossing = new System.Windows.Forms.TrackBar();
            this.tbHighT = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbHistogram)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbZeroCrossing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighT)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(138, 246);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(66, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(234, 246);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblHistogram);
            this.groupBox1.Controls.Add(this.tbHistogram);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 70);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Smoothing";
            // 
            // lblHistogram
            // 
            this.lblHistogram.AutoSize = true;
            this.lblHistogram.Location = new System.Drawing.Point(393, 24);
            this.lblHistogram.Name = "lblHistogram";
            this.lblHistogram.Size = new System.Drawing.Size(16, 15);
            this.lblHistogram.TabIndex = 10;
            this.lblHistogram.Text = "H";
            // 
            // tbHistogram
            // 
            this.tbHistogram.Location = new System.Drawing.Point(87, 21);
            this.tbHistogram.Maximum = 15;
            this.tbHistogram.Name = "tbHistogram";
            this.tbHistogram.Size = new System.Drawing.Size(307, 45);
            this.tbHistogram.TabIndex = 0;
            this.tbHistogram.Scroll += new System.EventHandler(this.tbHistogram_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Histogram:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblZeroCrossing);
            this.groupBox2.Controls.Add(this.lblHighT);
            this.groupBox2.Controls.Add(this.lblLowT);
            this.groupBox2.Controls.Add(this.tbLowT);
            this.groupBox2.Controls.Add(this.tbZeroCrossing);
            this.groupBox2.Controls.Add(this.tbHighT);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox2.Location = new System.Drawing.Point(12, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(442, 162);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // lblZeroCrossing
            // 
            this.lblZeroCrossing.AutoSize = true;
            this.lblZeroCrossing.Location = new System.Drawing.Point(393, 112);
            this.lblZeroCrossing.Name = "lblZeroCrossing";
            this.lblZeroCrossing.Size = new System.Drawing.Size(14, 15);
            this.lblZeroCrossing.TabIndex = 12;
            this.lblZeroCrossing.Text = "Z";
            // 
            // lblHighT
            // 
            this.lblHighT.AutoSize = true;
            this.lblHighT.Location = new System.Drawing.Point(393, 67);
            this.lblHighT.Name = "lblHighT";
            this.lblHighT.Size = new System.Drawing.Size(16, 15);
            this.lblHighT.TabIndex = 11;
            this.lblHighT.Text = "H";
            // 
            // lblLowT
            // 
            this.lblLowT.AutoSize = true;
            this.lblLowT.Location = new System.Drawing.Point(393, 22);
            this.lblLowT.Name = "lblLowT";
            this.lblLowT.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblLowT.Size = new System.Drawing.Size(14, 15);
            this.lblLowT.TabIndex = 10;
            this.lblLowT.Text = "L";
            // 
            // tbLowT
            // 
            this.tbLowT.Location = new System.Drawing.Point(87, 20);
            this.tbLowT.Name = "tbLowT";
            this.tbLowT.Size = new System.Drawing.Size(307, 45);
            this.tbLowT.TabIndex = 0;
            this.tbLowT.Scroll += new System.EventHandler(this.tbLowT_Scroll);
            // 
            // tbZeroCrossing
            // 
            this.tbZeroCrossing.LargeChange = 100;
            this.tbZeroCrossing.Location = new System.Drawing.Point(87, 110);
            this.tbZeroCrossing.Maximum = 1500;
            this.tbZeroCrossing.Name = "tbZeroCrossing";
            this.tbZeroCrossing.Size = new System.Drawing.Size(307, 45);
            this.tbZeroCrossing.TabIndex = 2;
            this.tbZeroCrossing.TickFrequency = 150;
            this.tbZeroCrossing.Scroll += new System.EventHandler(this.tbZeroCrossing_Scroll);
            // 
            // tbHighT
            // 
            this.tbHighT.Location = new System.Drawing.Point(87, 65);
            this.tbHighT.Name = "tbHighT";
            this.tbHighT.Size = new System.Drawing.Size(307, 45);
            this.tbHighT.TabIndex = 1;
            this.tbHighT.Scroll += new System.EventHandler(this.tbHighT_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Zero Crossing:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "High:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(55, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Low:";
            // 
            // CFuzzySegmentDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(466, 276);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Ivory;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFuzzySegmentDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fuzzy Segment";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbHistogram)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbZeroCrossing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHighT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar tbHistogram;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHistogram;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblLowT;
        private System.Windows.Forms.TrackBar tbLowT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblZeroCrossing;
        private System.Windows.Forms.Label lblHighT;
        private System.Windows.Forms.TrackBar tbZeroCrossing;
        private System.Windows.Forms.TrackBar tbHighT;
    }
}