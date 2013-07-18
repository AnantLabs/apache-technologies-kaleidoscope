namespace Kaleidoscope
{
    partial class CSineDlg
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
            this.lblTwo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOne = new System.Windows.Forms.Label();
            this.tbSlider2 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSlider1 = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkHoriFreq = new System.Windows.Forms.CheckBox();
            this.chkVertFreq = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHorizontalFreq = new System.Windows.Forms.TextBox();
            this.txtVerticalFreq = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSlider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSlider1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(175, 279);
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
            this.btnCancel.Location = new System.Drawing.Point(271, 279);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTwo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblOne);
            this.groupBox1.Controls.Add(this.tbSlider2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbSlider1);
            this.groupBox1.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox1.Location = new System.Drawing.Point(12, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 112);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Threshold";
            // 
            // lblTwo
            // 
            this.lblTwo.AutoSize = true;
            this.lblTwo.Location = new System.Drawing.Point(434, 67);
            this.lblTwo.Name = "lblTwo";
            this.lblTwo.Size = new System.Drawing.Size(41, 15);
            this.lblTwo.TabIndex = 11;
            this.lblTwo.Text = "Upper";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Constant Offset:";
            // 
            // lblOne
            // 
            this.lblOne.AutoSize = true;
            this.lblOne.Location = new System.Drawing.Point(434, 24);
            this.lblOne.Name = "lblOne";
            this.lblOne.Size = new System.Drawing.Size(41, 15);
            this.lblOne.TabIndex = 10;
            this.lblOne.Text = "Lower";
            // 
            // tbSlider2
            // 
            this.tbSlider2.LargeChange = 50;
            this.tbSlider2.Location = new System.Drawing.Point(102, 64);
            this.tbSlider2.Maximum = 255;
            this.tbSlider2.Name = "tbSlider2";
            this.tbSlider2.Size = new System.Drawing.Size(333, 45);
            this.tbSlider2.TabIndex = 2;
            this.tbSlider2.TickFrequency = 20;
            this.tbSlider2.Scroll += new System.EventHandler(this.tbSlider2_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Peak Amplitude:";
            // 
            // tbSlider1
            // 
            this.tbSlider1.Location = new System.Drawing.Point(102, 21);
            this.tbSlider1.Maximum = 127;
            this.tbSlider1.Minimum = 1;
            this.tbSlider1.Name = "tbSlider1";
            this.tbSlider1.Size = new System.Drawing.Size(333, 45);
            this.tbSlider1.TabIndex = 0;
            this.tbSlider1.TickFrequency = 10;
            this.tbSlider1.Value = 1;
            this.tbSlider1.Scroll += new System.EventHandler(this.tbSlider1_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Width:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(251, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Height:";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(302, 18);
            this.txtHeight.MaxLength = 4;
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(67, 21);
            this.txtHeight.TabIndex = 8;
            this.txtHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(160, 18);
            this.txtWidth.MaxLength = 4;
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(67, 21);
            this.txtWidth.TabIndex = 9;
            this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtWidth);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtHeight);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox3.Location = new System.Drawing.Point(12, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(486, 51);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dimensions";
            // 
            // chkHoriFreq
            // 
            this.chkHoriFreq.AutoSize = true;
            this.chkHoriFreq.Location = new System.Drawing.Point(113, 26);
            this.chkHoriFreq.Name = "chkHoriFreq";
            this.chkHoriFreq.Size = new System.Drawing.Size(110, 19);
            this.chkHoriFreq.TabIndex = 11;
            this.chkHoriFreq.Text = "Horizontal Freq";
            this.chkHoriFreq.UseVisualStyleBackColor = true;
            // 
            // chkVertFreq
            // 
            this.chkVertFreq.AutoSize = true;
            this.chkVertFreq.Location = new System.Drawing.Point(113, 58);
            this.chkVertFreq.Name = "chkVertFreq";
            this.chkVertFreq.Size = new System.Drawing.Size(94, 19);
            this.chkVertFreq.TabIndex = 12;
            this.chkVertFreq.Text = "Vertical Freq";
            this.chkVertFreq.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.chkVertFreq);
            this.groupBox2.Controls.Add(this.txtHorizontalFreq);
            this.groupBox2.Controls.Add(this.chkHoriFreq);
            this.groupBox2.Controls.Add(this.txtVerticalFreq);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox2.Location = new System.Drawing.Point(12, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(486, 96);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Frequencies";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(296, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Cycles/Pixels";
            // 
            // txtHorizontalFreq
            // 
            this.txtHorizontalFreq.Location = new System.Drawing.Point(226, 24);
            this.txtHorizontalFreq.MaxLength = 4;
            this.txtHorizontalFreq.Name = "txtHorizontalFreq";
            this.txtHorizontalFreq.Size = new System.Drawing.Size(67, 21);
            this.txtHorizontalFreq.TabIndex = 9;
            this.txtHorizontalFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHorizontalFreq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAlphaNumeric_KeyPress);
            // 
            // txtVerticalFreq
            // 
            this.txtVerticalFreq.Location = new System.Drawing.Point(226, 58);
            this.txtVerticalFreq.MaxLength = 4;
            this.txtVerticalFreq.Name = "txtVerticalFreq";
            this.txtVerticalFreq.Size = new System.Drawing.Size(67, 21);
            this.txtVerticalFreq.TabIndex = 8;
            this.txtVerticalFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVerticalFreq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAlphaNumeric_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(296, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "Cycles/Pixels";
            // 
            // CSineDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(512, 311);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Ivory;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CSineDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sinosoidal";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSlider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSlider1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTwo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOne;
        private System.Windows.Forms.TrackBar tbSlider2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbSlider1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkHoriFreq;
        private System.Windows.Forms.CheckBox chkVertFreq;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtHorizontalFreq;
        private System.Windows.Forms.TextBox txtVerticalFreq;
        private System.Windows.Forms.Label label6;
    }
}