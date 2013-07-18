namespace Kaleidoscope
{
    partial class CSingleSliderDlg
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
            this.tbSlider = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSliderName = new System.Windows.Forms.Label();
            this.lblSliderValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbSlider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(132, 85);
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
            this.btnCancel.Location = new System.Drawing.Point(236, 85);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbSlider
            // 
            this.tbSlider.Location = new System.Drawing.Point(70, 20);
            this.tbSlider.Name = "tbSlider";
            this.tbSlider.Size = new System.Drawing.Size(302, 45);
            this.tbSlider.TabIndex = 7;
            this.tbSlider.Scroll += new System.EventHandler(this.tbSlider_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSliderValue);
            this.groupBox1.Controls.Add(this.tbSlider);
            this.groupBox1.Controls.Add(this.lblSliderName);
            this.groupBox1.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 68);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // lblSliderName
            // 
            this.lblSliderName.Location = new System.Drawing.Point(6, 13);
            this.lblSliderName.Name = "lblSliderName";
            this.lblSliderName.Size = new System.Drawing.Size(68, 33);
            this.lblSliderName.TabIndex = 13;
            this.lblSliderName.Text = "Contrast:";
            this.lblSliderName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSliderValue
            // 
            this.lblSliderValue.AutoSize = true;
            this.lblSliderValue.Location = new System.Drawing.Point(369, 22);
            this.lblSliderValue.Name = "lblSliderValue";
            this.lblSliderValue.Size = new System.Drawing.Size(16, 15);
            this.lblSliderValue.TabIndex = 10;
            this.lblSliderValue.Text = "R";
            // 
            // CSingleSliderDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(435, 118);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Ivory;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CSingleSliderDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Linear";
            ((System.ComponentModel.ISupportInitialize)(this.tbSlider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TrackBar tbSlider;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSliderName;
        private System.Windows.Forms.Label lblSliderValue;
    }
}