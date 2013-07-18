namespace Kaleidoscope
{
    partial class CSizeFilterDlg
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radStop = new System.Windows.Forms.RadioButton();
            this.radPass = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtHighT = new System.Windows.Forms.TextBox();
            this.txtLowT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbHigherT = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLowerT = new System.Windows.Forms.TrackBar();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbHigherT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowerT)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(147, 180);
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
            this.btnCancel.Location = new System.Drawing.Point(243, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radStop);
            this.groupBox2.Controls.Add(this.radPass);
            this.groupBox2.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox2.Location = new System.Drawing.Point(12, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(435, 50);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Band Type";
            // 
            // radStop
            // 
            this.radStop.Location = new System.Drawing.Point(251, 17);
            this.radStop.Name = "radStop";
            this.radStop.Size = new System.Drawing.Size(56, 19);
            this.radStop.TabIndex = 1;
            this.radStop.TabStop = true;
            this.radStop.Text = "Stop";
            this.radStop.UseVisualStyleBackColor = true;
            this.radStop.CheckedChanged += new System.EventHandler(this.radSparkle_CheckedChanged);
            // 
            // radPass
            // 
            this.radPass.Location = new System.Drawing.Point(128, 17);
            this.radPass.Name = "radPass";
            this.radPass.Size = new System.Drawing.Size(55, 19);
            this.radPass.TabIndex = 0;
            this.radPass.TabStop = true;
            this.radPass.Text = "Pass";
            this.radPass.UseVisualStyleBackColor = true;
            this.radPass.CheckedChanged += new System.EventHandler(this.radRandom_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtHighT);
            this.groupBox1.Controls.Add(this.txtLowT);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbHigherT);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbLowerT);
            this.groupBox1.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 111);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Threshold";
            // 
            // txtHighT
            // 
            this.txtHighT.Location = new System.Drawing.Point(372, 63);
            this.txtHighT.MaxLength = 3;
            this.txtHighT.Name = "txtHighT";
            this.txtHighT.Size = new System.Drawing.Size(55, 21);
            this.txtHighT.TabIndex = 18;
            // 
            // txtLowT
            // 
            this.txtLowT.Location = new System.Drawing.Point(372, 18);
            this.txtLowT.MaxLength = 3;
            this.txtLowT.Name = "txtLowT";
            this.txtLowT.Size = new System.Drawing.Size(55, 21);
            this.txtLowT.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "High T:";
            // 
            // tbHigherT
            // 
            this.tbHigherT.Location = new System.Drawing.Point(63, 64);
            this.tbHigherT.Maximum = 255;
            this.tbHigherT.Name = "tbHigherT";
            this.tbHigherT.Size = new System.Drawing.Size(307, 45);
            this.tbHigherT.TabIndex = 2;
            this.tbHigherT.TickFrequency = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Low T:";
            // 
            // tbLowerT
            // 
            this.tbLowerT.Location = new System.Drawing.Point(63, 19);
            this.tbLowerT.Maximum = 255;
            this.tbLowerT.Name = "tbLowerT";
            this.tbLowerT.Size = new System.Drawing.Size(307, 45);
            this.tbLowerT.TabIndex = 0;
            this.tbLowerT.TickFrequency = 20;
            // 
            // CSizeFilterDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(459, 213);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Ivory;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CSizeFilterDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Size Filter";
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbHigherT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowerT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radStop;
        private System.Windows.Forms.RadioButton radPass;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbHigherT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbLowerT;
        private System.Windows.Forms.TextBox txtHighT;
        private System.Windows.Forms.TextBox txtLowT;
    }
}