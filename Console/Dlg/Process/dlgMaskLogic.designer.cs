namespace Kaleidoscope
{
    partial class CMaskLogicDlg
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
            this.lblMaskValue = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSliderMask = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radXor = new System.Windows.Forms.RadioButton();
            this.radOr = new System.Windows.Forms.RadioButton();
            this.radAnd = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSliderMask)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(102, 131);
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
            this.btnCancel.Location = new System.Drawing.Point(198, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMaskValue);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbSliderMask);
            this.groupBox1.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox1.Location = new System.Drawing.Point(12, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 69);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Threshold";
            // 
            // lblMaskValue
            // 
            this.lblMaskValue.AutoSize = true;
            this.lblMaskValue.Location = new System.Drawing.Point(297, 24);
            this.lblMaskValue.Name = "lblMaskValue";
            this.lblMaskValue.Size = new System.Drawing.Size(38, 15);
            this.lblMaskValue.TabIndex = 10;
            this.lblMaskValue.Text = "Value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mask:";
            // 
            // tbSliderMask
            // 
            this.tbSliderMask.Location = new System.Drawing.Point(40, 21);
            this.tbSliderMask.Maximum = 255;
            this.tbSliderMask.Name = "tbSliderMask";
            this.tbSliderMask.Size = new System.Drawing.Size(258, 45);
            this.tbSliderMask.TabIndex = 0;
            this.tbSliderMask.TickFrequency = 20;
            this.tbSliderMask.Scroll += new System.EventHandler(this.tbSliderMask_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radXor);
            this.groupBox2.Controls.Add(this.radOr);
            this.groupBox2.Controls.Add(this.radAnd);
            this.groupBox2.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox2.Location = new System.Drawing.Point(12, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(343, 50);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Type";
            // 
            // radXor
            // 
            this.radXor.Location = new System.Drawing.Point(248, 20);
            this.radXor.Name = "radXor";
            this.radXor.Size = new System.Drawing.Size(51, 19);
            this.radXor.TabIndex = 2;
            this.radXor.TabStop = true;
            this.radXor.Text = "XOR";
            this.radXor.UseVisualStyleBackColor = true;
            this.radXor.CheckedChanged += new System.EventHandler(this.radXor_CheckedChanged);
            // 
            // radOr
            // 
            this.radOr.Location = new System.Drawing.Point(151, 20);
            this.radOr.Name = "radOr";
            this.radOr.Size = new System.Drawing.Size(43, 19);
            this.radOr.TabIndex = 1;
            this.radOr.TabStop = true;
            this.radOr.Text = "OR";
            this.radOr.UseVisualStyleBackColor = true;
            this.radOr.CheckedChanged += new System.EventHandler(this.radOr_CheckedChanged);
            // 
            // radAnd
            // 
            this.radAnd.Location = new System.Drawing.Point(52, 20);
            this.radAnd.Name = "radAnd";
            this.radAnd.Size = new System.Drawing.Size(50, 19);
            this.radAnd.TabIndex = 0;
            this.radAnd.TabStop = true;
            this.radAnd.Text = "AND";
            this.radAnd.UseVisualStyleBackColor = true;
            this.radAnd.CheckedChanged += new System.EventHandler(this.radAnd_CheckedChanged);
            // 
            // CMaskLogicDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(367, 165);
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
            this.Name = "CMaskLogicDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mask Logic";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSliderMask)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radOr;
        private System.Windows.Forms.RadioButton radAnd;
        private System.Windows.Forms.TrackBar tbSliderMask;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMaskValue;
        private System.Windows.Forms.RadioButton radXor;
    }
}