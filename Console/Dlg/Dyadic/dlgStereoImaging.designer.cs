namespace Kaleidoscope
{
    partial class CStereoImagingDlg
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
            this.lblImage1 = new System.Windows.Forms.Label();
            this.pbImage1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblImage2 = new System.Windows.Forms.Label();
            this.pbImage2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.udMaxDisparity = new System.Windows.Forms.NumericUpDown();
            this.udMatchReward = new System.Windows.Forms.NumericUpDown();
            this.udOcclusionPenality = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.udMaxAttractionThreshold = new System.Windows.Forms.NumericUpDown();
            this.udAlpha = new System.Windows.Forms.NumericUpDown();
            this.udReliableThreashold = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMaxDisparity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMatchReward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udOcclusionPenality)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMaxAttractionThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udReliableThreashold)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(244, 427);
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
            this.btnCancel.Location = new System.Drawing.Point(351, 427);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblImage1);
            this.groupBox2.Controls.Add(this.pbImage1);
            this.groupBox2.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox2.Location = new System.Drawing.Point(12, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 293);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Image 1";
            // 
            // lblImage1
            // 
            this.lblImage1.BackColor = System.Drawing.Color.Ivory;
            this.lblImage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblImage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImage1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblImage1.Location = new System.Drawing.Point(11, 261);
            this.lblImage1.Name = "lblImage1";
            this.lblImage1.Size = new System.Drawing.Size(285, 21);
            this.lblImage1.TabIndex = 19;
            this.lblImage1.Text = "label3";
            // 
            // pbImage1
            // 
            this.pbImage1.BackColor = System.Drawing.Color.White;
            this.pbImage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbImage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImage1.Location = new System.Drawing.Point(14, 20);
            this.pbImage1.Name = "pbImage1";
            this.pbImage1.Size = new System.Drawing.Size(282, 229);
            this.pbImage1.TabIndex = 18;
            this.pbImage1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(121, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "Match Reward:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(104, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Occlusion Penalty:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.lblImage2);
            this.groupBox1.Controls.Add(this.pbImage2);
            this.groupBox1.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox1.Location = new System.Drawing.Point(337, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 293);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image 2";
            // 
            // btnBrowse
            // 
            this.btnBrowse.ForeColor = System.Drawing.Color.Black;
            this.btnBrowse.Location = new System.Drawing.Point(263, 260);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(33, 23);
            this.btnBrowse.TabIndex = 21;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblImage2
            // 
            this.lblImage2.BackColor = System.Drawing.Color.Ivory;
            this.lblImage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblImage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImage2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblImage2.Location = new System.Drawing.Point(15, 261);
            this.lblImage2.Name = "lblImage2";
            this.lblImage2.Size = new System.Drawing.Size(242, 21);
            this.lblImage2.TabIndex = 20;
            this.lblImage2.Text = "Select the second image";
            // 
            // pbImage2
            // 
            this.pbImage2.BackColor = System.Drawing.Color.White;
            this.pbImage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbImage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImage2.Location = new System.Drawing.Point(14, 20);
            this.pbImage2.Name = "pbImage2";
            this.pbImage2.Size = new System.Drawing.Size(282, 229);
            this.pbImage2.TabIndex = 18;
            this.pbImage2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "Maximum Disparity:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.udMaxDisparity);
            this.groupBox3.Controls.Add(this.udMatchReward);
            this.groupBox3.Controls.Add(this.udOcclusionPenality);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox3.Location = new System.Drawing.Point(12, 300);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(310, 119);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Preprocessing Factors";
            // 
            // udMaxDisparity
            // 
            this.udMaxDisparity.Location = new System.Drawing.Point(217, 88);
            this.udMaxDisparity.Name = "udMaxDisparity";
            this.udMaxDisparity.Size = new System.Drawing.Size(70, 21);
            this.udMaxDisparity.TabIndex = 17;
            this.udMaxDisparity.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // udMatchReward
            // 
            this.udMatchReward.Location = new System.Drawing.Point(217, 55);
            this.udMatchReward.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.udMatchReward.Name = "udMatchReward";
            this.udMatchReward.Size = new System.Drawing.Size(70, 21);
            this.udMatchReward.TabIndex = 16;
            this.udMatchReward.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // udOcclusionPenality
            // 
            this.udOcclusionPenality.Location = new System.Drawing.Point(217, 22);
            this.udOcclusionPenality.Name = "udOcclusionPenality";
            this.udOcclusionPenality.Size = new System.Drawing.Size(70, 21);
            this.udOcclusionPenality.TabIndex = 15;
            this.udOcclusionPenality.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.udMaxAttractionThreshold);
            this.groupBox4.Controls.Add(this.udAlpha);
            this.groupBox4.Controls.Add(this.udReliableThreashold);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox4.Location = new System.Drawing.Point(337, 300);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(310, 119);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Postprocessing Factors";
            // 
            // udMaxAttractionThreshold
            // 
            this.udMaxAttractionThreshold.Location = new System.Drawing.Point(214, 88);
            this.udMaxAttractionThreshold.Name = "udMaxAttractionThreshold";
            this.udMaxAttractionThreshold.Size = new System.Drawing.Size(70, 21);
            this.udMaxAttractionThreshold.TabIndex = 17;
            this.udMaxAttractionThreshold.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // udAlpha
            // 
            this.udAlpha.Location = new System.Drawing.Point(214, 55);
            this.udAlpha.Name = "udAlpha";
            this.udAlpha.Size = new System.Drawing.Size(70, 21);
            this.udAlpha.TabIndex = 16;
            this.udAlpha.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // udReliableThreashold
            // 
            this.udReliableThreashold.Location = new System.Drawing.Point(214, 22);
            this.udReliableThreashold.Name = "udReliableThreashold";
            this.udReliableThreashold.Size = new System.Drawing.Size(70, 21);
            this.udReliableThreashold.TabIndex = 15;
            this.udReliableThreashold.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(169, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Alpha:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Maximum Attraction Threshold:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Reliable Threshold:";
            // 
            // CStereoImagingDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(659, 457);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
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
            this.Name = "CStereoImagingDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Band";
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMaxDisparity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udMatchReward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udOcclusionPenality)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udMaxAttractionThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udReliableThreashold)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pbImage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pbImage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblImage1;
        private System.Windows.Forms.Label lblImage2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown udMaxDisparity;
        private System.Windows.Forms.NumericUpDown udMatchReward;
        private System.Windows.Forms.NumericUpDown udOcclusionPenality;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown udMaxAttractionThreshold;
        private System.Windows.Forms.NumericUpDown udAlpha;
        private System.Windows.Forms.NumericUpDown udReliableThreashold;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}