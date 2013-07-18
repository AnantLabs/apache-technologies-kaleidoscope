namespace Kaleidoscope
{
    partial class CBandDlg
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
            this.lblUpper = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLower = new System.Windows.Forms.Label();
            this.tbUpperT = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLowerT = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radStop = new System.Windows.Forms.RadioButton();
            this.radPass = new System.Windows.Forms.RadioButton();
            this.tbR = new System.Windows.Forms.TrackBar();
            this.tbG = new System.Windows.Forms.TrackBar();
            this.tbB = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbUpperT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowerT)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbB)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(126, 190);
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
            this.btnCancel.Location = new System.Drawing.Point(222, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblUpper);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblLower);
            this.groupBox1.Controls.Add(this.tbUpperT);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbLowerT);
            this.groupBox1.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox1.Location = new System.Drawing.Point(12, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 114);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Threshold";
            // 
            // lblUpper
            // 
            this.lblUpper.AutoSize = true;
            this.lblUpper.Location = new System.Drawing.Point(205, 67);
            this.lblUpper.Name = "lblUpper";
            this.lblUpper.Size = new System.Drawing.Size(41, 15);
            this.lblUpper.TabIndex = 11;
            this.lblUpper.Text = "Upper";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Upper:";
            // 
            // lblLower
            // 
            this.lblLower.AutoSize = true;
            this.lblLower.Location = new System.Drawing.Point(205, 24);
            this.lblLower.Name = "lblLower";
            this.lblLower.Size = new System.Drawing.Size(41, 15);
            this.lblLower.TabIndex = 10;
            this.lblLower.Text = "Lower";
            // 
            // tbUpperT
            // 
            this.tbUpperT.Location = new System.Drawing.Point(40, 64);
            this.tbUpperT.Maximum = 255;
            this.tbUpperT.Name = "tbUpperT";
            this.tbUpperT.Size = new System.Drawing.Size(169, 45);
            this.tbUpperT.TabIndex = 2;
            this.tbUpperT.TickFrequency = 20;
            this.tbUpperT.Scroll += new System.EventHandler(this.tbUpperT_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lower:";
            // 
            // tbLowerT
            // 
            this.tbLowerT.Location = new System.Drawing.Point(40, 21);
            this.tbLowerT.Maximum = 255;
            this.tbLowerT.Name = "tbLowerT";
            this.tbLowerT.Size = new System.Drawing.Size(169, 45);
            this.tbLowerT.TabIndex = 0;
            this.tbLowerT.TickFrequency = 20;
            this.tbLowerT.Scroll += new System.EventHandler(this.tbLowerT_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radStop);
            this.groupBox2.Controls.Add(this.radPass);
            this.groupBox2.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox2.Location = new System.Drawing.Point(12, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(251, 50);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Type";
            // 
            // radStop
            // 
            this.radStop.AutoSize = true;
            this.radStop.Location = new System.Drawing.Point(149, 21);
            this.radStop.Name = "radStop";
            this.radStop.Size = new System.Drawing.Size(50, 19);
            this.radStop.TabIndex = 1;
            this.radStop.TabStop = true;
            this.radStop.Text = "Stop";
            this.radStop.UseVisualStyleBackColor = true;
            // 
            // radPass
            // 
            this.radPass.AutoSize = true;
            this.radPass.Location = new System.Drawing.Point(52, 21);
            this.radPass.Name = "radPass";
            this.radPass.Size = new System.Drawing.Size(52, 19);
            this.radPass.TabIndex = 0;
            this.radPass.TabStop = true;
            this.radPass.Text = "Pass";
            this.radPass.UseVisualStyleBackColor = true;
            // 
            // tbR
            // 
            this.tbR.Location = new System.Drawing.Point(269, 3);
            this.tbR.Maximum = 255;
            this.tbR.Name = "tbR";
            this.tbR.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbR.Size = new System.Drawing.Size(45, 169);
            this.tbR.TabIndex = 4;
            this.tbR.TickFrequency = 20;
            // 
            // tbG
            // 
            this.tbG.Location = new System.Drawing.Point(314, 3);
            this.tbG.Maximum = 255;
            this.tbG.Name = "tbG";
            this.tbG.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbG.Size = new System.Drawing.Size(45, 169);
            this.tbG.TabIndex = 5;
            this.tbG.TickFrequency = 20;
            // 
            // tbB
            // 
            this.tbB.Location = new System.Drawing.Point(359, 3);
            this.tbB.Maximum = 255;
            this.tbB.Name = "tbB";
            this.tbB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbB.Size = new System.Drawing.Size(45, 169);
            this.tbB.TabIndex = 6;
            this.tbB.TickFrequency = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(273, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "R";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(318, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "G";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(364, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "B";
            // 
            // CBandDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(414, 221);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbB);
            this.Controls.Add(this.tbG);
            this.Controls.Add(this.tbR);
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
            this.Name = "CBandDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Band";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbUpperT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLowerT)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radStop;
        private System.Windows.Forms.RadioButton radPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbUpperT;
        private System.Windows.Forms.TrackBar tbLowerT;
        private System.Windows.Forms.TrackBar tbR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbG;
        private System.Windows.Forms.TrackBar tbB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblUpper;
        private System.Windows.Forms.Label lblLower;
    }
}