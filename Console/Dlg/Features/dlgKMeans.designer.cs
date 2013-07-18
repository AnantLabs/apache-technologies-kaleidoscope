namespace Kaleidoscope
{
    partial class CKMeansDlg
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
            this.tbVectorType = new System.Windows.Forms.TrackBar();
            this.tbSegments = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblVectorType = new System.Windows.Forms.Label();
            this.lblSegments = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbVectorType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSegments)).BeginInit();
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
            // tbVectorType
            // 
            this.tbVectorType.LargeChange = 1;
            this.tbVectorType.Location = new System.Drawing.Point(81, 65);
            this.tbVectorType.Maximum = 5;
            this.tbVectorType.Name = "tbVectorType";
            this.tbVectorType.Size = new System.Drawing.Size(291, 45);
            this.tbVectorType.TabIndex = 3;
            this.tbVectorType.Value = 3;
            this.tbVectorType.Scroll += new System.EventHandler(this.tbBrightness_Scroll);
            // 
            // tbSegments
            // 
            this.tbSegments.LargeChange = 2;
            this.tbSegments.Location = new System.Drawing.Point(81, 20);
            this.tbSegments.Maximum = 9;
            this.tbSegments.Minimum = 2;
            this.tbSegments.Name = "tbSegments";
            this.tbSegments.Size = new System.Drawing.Size(291, 45);
            this.tbSegments.TabIndex = 7;
            this.tbSegments.Value = 5;
            this.tbSegments.Scroll += new System.EventHandler(this.tbContrast_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblVectorType);
            this.groupBox1.Controls.Add(this.lblSegments);
            this.groupBox1.Controls.Add(this.tbSegments);
            this.groupBox1.Controls.Add(this.tbVectorType);
            this.groupBox1.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 116);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Vector Type:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Segments:";
            // 
            // lblVectorType
            // 
            this.lblVectorType.Location = new System.Drawing.Point(374, 67);
            this.lblVectorType.Name = "lblVectorType";
            this.lblVectorType.Size = new System.Drawing.Size(30, 15);
            this.lblVectorType.TabIndex = 11;
            this.lblVectorType.Text = "3";
            // 
            // lblSegments
            // 
            this.lblSegments.Location = new System.Drawing.Point(374, 22);
            this.lblSegments.Name = "lblSegments";
            this.lblSegments.Size = new System.Drawing.Size(30, 15);
            this.lblSegments.TabIndex = 10;
            this.lblSegments.Text = "5";
            // 
            // CKMeansDlg
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
            this.Name = "CKMeansDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "K-Means";
            ((System.ComponentModel.ISupportInitialize)(this.tbVectorType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSegments)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TrackBar tbVectorType;
        private System.Windows.Forms.TrackBar tbSegments;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblVectorType;
        private System.Windows.Forms.Label lblSegments;
    }
}