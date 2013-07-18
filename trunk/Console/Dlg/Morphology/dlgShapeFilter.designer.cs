namespace Kaleidoscope
{
    partial class CShapeFilterDlg
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
            this.radStop = new System.Windows.Forms.RadioButton();
            this.radPass = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtYScale = new System.Windows.Forms.TextBox();
            this.txtXScale = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtShapeHigh = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtShapeLow = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(75, 175);
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
            this.btnCancel.Location = new System.Drawing.Point(171, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radStop);
            this.groupBox1.Controls.Add(this.radPass);
            this.groupBox1.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 49);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Band Type";
            // 
            // radStop
            // 
            this.radStop.Location = new System.Drawing.Point(182, 17);
            this.radStop.Name = "radStop";
            this.radStop.Size = new System.Drawing.Size(56, 19);
            this.radStop.TabIndex = 3;
            this.radStop.TabStop = true;
            this.radStop.Text = "Stop";
            this.radStop.UseVisualStyleBackColor = true;
            this.radStop.CheckedChanged += new System.EventHandler(this.radStop_CheckedChanged);
            // 
            // radPass
            // 
            this.radPass.Location = new System.Drawing.Point(67, 17);
            this.radPass.Name = "radPass";
            this.radPass.Size = new System.Drawing.Size(55, 19);
            this.radPass.TabIndex = 2;
            this.radPass.TabStop = true;
            this.radPass.Text = "Pass";
            this.radPass.UseVisualStyleBackColor = true;
            this.radPass.CheckedChanged += new System.EventHandler(this.radPass_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtYScale);
            this.groupBox2.Controls.Add(this.txtXScale);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox2.Location = new System.Drawing.Point(12, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 55);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scale";
            // 
            // txtYScale
            // 
            this.txtYScale.Location = new System.Drawing.Point(199, 20);
            this.txtYScale.MaxLength = 3;
            this.txtYScale.Name = "txtYScale";
            this.txtYScale.Size = new System.Drawing.Size(55, 21);
            this.txtYScale.TabIndex = 17;
            // 
            // txtXScale
            // 
            this.txtXScale.Location = new System.Drawing.Point(83, 20);
            this.txtXScale.MaxLength = 3;
            this.txtXScale.Name = "txtXScale";
            this.txtXScale.Size = new System.Drawing.Size(55, 21);
            this.txtXScale.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(145, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "Y-Scale:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "X-Scale:";
            // 
            // txtShapeHigh
            // 
            this.txtShapeHigh.Location = new System.Drawing.Point(199, 20);
            this.txtShapeHigh.MaxLength = 3;
            this.txtShapeHigh.Name = "txtShapeHigh";
            this.txtShapeHigh.Size = new System.Drawing.Size(55, 21);
            this.txtShapeHigh.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "High:";
            // 
            // txtShapeLow
            // 
            this.txtShapeLow.Location = new System.Drawing.Point(81, 21);
            this.txtShapeLow.MaxLength = 3;
            this.txtShapeLow.Name = "txtShapeLow";
            this.txtShapeLow.Size = new System.Drawing.Size(55, 21);
            this.txtShapeLow.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Low:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtShapeHigh);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtShapeLow);
            this.groupBox3.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox3.Location = new System.Drawing.Point(12, 115);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(288, 54);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Shape";
            // 
            // CShapeFilterDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(313, 208);
            this.Controls.Add(this.groupBox3);
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
            this.Name = "CShapeFilterDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shape Filter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtShapeLow;
        private System.Windows.Forms.TextBox txtYScale;
        private System.Windows.Forms.TextBox txtXScale;
        private System.Windows.Forms.TextBox txtShapeHigh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radStop;
        private System.Windows.Forms.RadioButton radPass;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}