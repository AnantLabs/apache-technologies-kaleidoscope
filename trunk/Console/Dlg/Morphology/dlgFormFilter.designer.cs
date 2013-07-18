namespace Kaleidoscope
{
    partial class CFormFilterDlg
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
            this.txtYLow = new System.Windows.Forms.TextBox();
            this.txtYHigh = new System.Windows.Forms.TextBox();
            this.txtXHigh = new System.Windows.Forms.TextBox();
            this.txtXLow = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbYHigh = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.tbYLow = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.tbXHigh = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.tbXLow = new System.Windows.Forms.TrackBar();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbYHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbYLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbXHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbXLow)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(148, 278);
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
            this.btnCancel.Location = new System.Drawing.Point(244, 278);
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
            this.radStop.CheckedChanged += new System.EventHandler(this.radStop_CheckedChanged);
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
            this.radPass.CheckedChanged += new System.EventHandler(this.radPass_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtYLow);
            this.groupBox1.Controls.Add(this.txtYHigh);
            this.groupBox1.Controls.Add(this.txtXHigh);
            this.groupBox1.Controls.Add(this.txtXLow);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbYHigh);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbYLow);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbXHigh);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbXLow);
            this.groupBox1.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 206);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Threshold";
            // 
            // txtYLow
            // 
            this.txtYLow.Location = new System.Drawing.Point(372, 108);
            this.txtYLow.MaxLength = 3;
            this.txtYLow.Name = "txtYLow";
            this.txtYLow.Size = new System.Drawing.Size(55, 21);
            this.txtYLow.TabIndex = 20;
            // 
            // txtYHigh
            // 
            this.txtYHigh.Location = new System.Drawing.Point(372, 153);
            this.txtYHigh.MaxLength = 3;
            this.txtYHigh.Name = "txtYHigh";
            this.txtYHigh.Size = new System.Drawing.Size(55, 21);
            this.txtYHigh.TabIndex = 19;
            // 
            // txtXHigh
            // 
            this.txtXHigh.Location = new System.Drawing.Point(372, 63);
            this.txtXHigh.MaxLength = 3;
            this.txtXHigh.Name = "txtXHigh";
            this.txtXHigh.Size = new System.Drawing.Size(55, 21);
            this.txtXHigh.TabIndex = 18;
            // 
            // txtXLow
            // 
            this.txtXLow.Location = new System.Drawing.Point(372, 18);
            this.txtXLow.MaxLength = 3;
            this.txtXLow.Name = "txtXLow";
            this.txtXLow.Size = new System.Drawing.Size(55, 21);
            this.txtXLow.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Y-High:";
            // 
            // tbYHigh
            // 
            this.tbYHigh.Location = new System.Drawing.Point(65, 154);
            this.tbYHigh.Maximum = 255;
            this.tbYHigh.Name = "tbYHigh";
            this.tbYHigh.Size = new System.Drawing.Size(307, 45);
            this.tbYHigh.TabIndex = 14;
            this.tbYHigh.TickFrequency = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Y-Low:";
            // 
            // tbYLow
            // 
            this.tbYLow.Location = new System.Drawing.Point(65, 109);
            this.tbYLow.Maximum = 255;
            this.tbYLow.Name = "tbYLow";
            this.tbYLow.Size = new System.Drawing.Size(307, 45);
            this.tbYLow.TabIndex = 12;
            this.tbYLow.TickFrequency = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "X-High:";
            // 
            // tbXHigh
            // 
            this.tbXHigh.Location = new System.Drawing.Point(63, 64);
            this.tbXHigh.Maximum = 255;
            this.tbXHigh.Name = "tbXHigh";
            this.tbXHigh.Size = new System.Drawing.Size(307, 45);
            this.tbXHigh.TabIndex = 2;
            this.tbXHigh.TickFrequency = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "X-Low:";
            // 
            // tbXLow
            // 
            this.tbXLow.Location = new System.Drawing.Point(63, 19);
            this.tbXLow.Maximum = 255;
            this.tbXLow.Name = "tbXLow";
            this.tbXLow.Size = new System.Drawing.Size(307, 45);
            this.tbXLow.TabIndex = 0;
            this.tbXLow.TickFrequency = 20;
            // 
            // CFormFilterDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(459, 309);
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
            this.Name = "CFormFilterDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form Filter";
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbYHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbYLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbXHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbXLow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radStop;
        private System.Windows.Forms.RadioButton radPass;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbYHigh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar tbYLow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbXHigh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbXLow;
        private System.Windows.Forms.TextBox txtYLow;
        private System.Windows.Forms.TextBox txtYHigh;
        private System.Windows.Forms.TextBox txtXHigh;
        private System.Windows.Forms.TextBox txtXLow;
    }
}