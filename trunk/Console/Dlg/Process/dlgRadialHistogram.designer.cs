namespace Kaleidoscope
{
    partial class CRadialHistogramDlg
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRingNum = new System.Windows.Forms.Label();
            this.lblRowNum = new System.Windows.Forms.Label();
            this.lblColumnNum = new System.Windows.Forms.Label();
            this.tbColumnNum = new System.Windows.Forms.TrackBar();
            this.tbRingNum = new System.Windows.Forms.TrackBar();
            this.tbRowNum = new System.Windows.Forms.TrackBar();
            this.lblRingWidth = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRingWidth = new System.Windows.Forms.TrackBar();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbColumnNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRingNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRowNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRingWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(144, 212);
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
            this.btnCancel.Location = new System.Drawing.Point(240, 212);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblRingWidth);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbRingWidth);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblRingNum);
            this.groupBox2.Controls.Add(this.lblRowNum);
            this.groupBox2.Controls.Add(this.lblColumnNum);
            this.groupBox2.Controls.Add(this.tbColumnNum);
            this.groupBox2.Controls.Add(this.tbRingNum);
            this.groupBox2.Controls.Add(this.tbRowNum);
            this.groupBox2.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox2.Location = new System.Drawing.Point(12, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 205);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Ring Num:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "Row Num:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Col Num:";
            // 
            // lblRingNum
            // 
            this.lblRingNum.AutoSize = true;
            this.lblRingNum.Location = new System.Drawing.Point(385, 112);
            this.lblRingNum.Name = "lblRingNum";
            this.lblRingNum.Size = new System.Drawing.Size(14, 15);
            this.lblRingNum.TabIndex = 12;
            this.lblRingNum.Text = "0";
            // 
            // lblRowNum
            // 
            this.lblRowNum.AutoSize = true;
            this.lblRowNum.Location = new System.Drawing.Point(385, 67);
            this.lblRowNum.Name = "lblRowNum";
            this.lblRowNum.Size = new System.Drawing.Size(14, 15);
            this.lblRowNum.TabIndex = 11;
            this.lblRowNum.Text = "0";
            // 
            // lblColumnNum
            // 
            this.lblColumnNum.AutoSize = true;
            this.lblColumnNum.Location = new System.Drawing.Point(385, 22);
            this.lblColumnNum.Name = "lblColumnNum";
            this.lblColumnNum.Size = new System.Drawing.Size(14, 15);
            this.lblColumnNum.TabIndex = 10;
            this.lblColumnNum.Text = "0";
            // 
            // tbColumnNum
            // 
            this.tbColumnNum.Location = new System.Drawing.Point(79, 20);
            this.tbColumnNum.Name = "tbColumnNum";
            this.tbColumnNum.Size = new System.Drawing.Size(307, 45);
            this.tbColumnNum.TabIndex = 7;
            this.tbColumnNum.Scroll += new System.EventHandler(this.tbColNum_Scroll);
            // 
            // tbRingNum
            // 
            this.tbRingNum.Location = new System.Drawing.Point(79, 110);
            this.tbRingNum.Maximum = 100;
            this.tbRingNum.Name = "tbRingNum";
            this.tbRingNum.Size = new System.Drawing.Size(307, 45);
            this.tbRingNum.TabIndex = 9;
            this.tbRingNum.TickFrequency = 10;
            this.tbRingNum.Scroll += new System.EventHandler(this.tbRingNum_Scroll);
            // 
            // tbRowNum
            // 
            this.tbRowNum.Location = new System.Drawing.Point(79, 65);
            this.tbRowNum.Name = "tbRowNum";
            this.tbRowNum.Size = new System.Drawing.Size(307, 45);
            this.tbRowNum.TabIndex = 8;
            this.tbRowNum.Scroll += new System.EventHandler(this.tbRowNum_Scroll);
            // 
            // lblRingWidth
            // 
            this.lblRingWidth.AutoSize = true;
            this.lblRingWidth.Location = new System.Drawing.Point(385, 157);
            this.lblRingWidth.Name = "lblRingWidth";
            this.lblRingWidth.Size = new System.Drawing.Size(14, 15);
            this.lblRingWidth.TabIndex = 18;
            this.lblRingWidth.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Ring Width:";
            // 
            // tbRingWidth
            // 
            this.tbRingWidth.Location = new System.Drawing.Point(79, 154);
            this.tbRingWidth.Name = "tbRingWidth";
            this.tbRingWidth.Size = new System.Drawing.Size(307, 45);
            this.tbRingWidth.TabIndex = 16;
            this.tbRingWidth.Scroll += new System.EventHandler(this.tbRingWidth_Scroll);
            // 
            // CRadialHistogramDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(450, 241);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Ivory;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CRadialHistogramDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Contrast Stretch";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbColumnNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRingNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRowNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRingWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblColumnNum;
        private System.Windows.Forms.TrackBar tbColumnNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblRingNum;
        private System.Windows.Forms.Label lblRowNum;
        private System.Windows.Forms.TrackBar tbRingNum;
        private System.Windows.Forms.TrackBar tbRowNum;
        private System.Windows.Forms.Label lblRingWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbRingWidth;
    }
}