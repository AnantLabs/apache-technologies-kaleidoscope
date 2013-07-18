namespace Kaleidoscope
{
    partial class CFilterLogicDlg
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
            this.radThree = new System.Windows.Forms.RadioButton();
            this.radTwo = new System.Windows.Forms.RadioButton();
            this.radOne = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(102, 66);
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
            this.btnCancel.Location = new System.Drawing.Point(198, 66);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radThree);
            this.groupBox2.Controls.Add(this.radTwo);
            this.groupBox2.Controls.Add(this.radOne);
            this.groupBox2.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox2.Location = new System.Drawing.Point(12, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(343, 50);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Type";
            // 
            // radThree
            // 
            this.radThree.Location = new System.Drawing.Point(248, 20);
            this.radThree.Name = "radThree";
            this.radThree.Size = new System.Drawing.Size(57, 19);
            this.radThree.TabIndex = 2;
            this.radThree.TabStop = true;
            this.radThree.Text = "Three";
            this.radThree.UseVisualStyleBackColor = true;
            this.radThree.CheckedChanged += new System.EventHandler(this.radThree_CheckedChanged);
            // 
            // radTwo
            // 
            this.radTwo.Location = new System.Drawing.Point(151, 20);
            this.radTwo.Name = "radTwo";
            this.radTwo.Size = new System.Drawing.Size(48, 19);
            this.radTwo.TabIndex = 1;
            this.radTwo.TabStop = true;
            this.radTwo.Text = "Two";
            this.radTwo.UseVisualStyleBackColor = true;
            this.radTwo.CheckedChanged += new System.EventHandler(this.radTwo_CheckedChanged);
            // 
            // radOne
            // 
            this.radOne.Location = new System.Drawing.Point(52, 20);
            this.radOne.Name = "radOne";
            this.radOne.Size = new System.Drawing.Size(48, 19);
            this.radOne.TabIndex = 0;
            this.radOne.TabStop = true;
            this.radOne.Text = "One";
            this.radOne.UseVisualStyleBackColor = true;
            this.radOne.CheckedChanged += new System.EventHandler(this.radOne_CheckedChanged);
            // 
            // CFilterLogicDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(367, 98);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Ivory;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFilterLogicDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter Logic";
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radTwo;
        private System.Windows.Forms.RadioButton radOne;
        private System.Windows.Forms.RadioButton radThree;
    }
}