namespace Kaleidoscope
{
    partial class CAnnotateDlg
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
            this.radOpaque = new System.Windows.Forms.RadioButton();
            this.radTransparent = new System.Windows.Forms.RadioButton();
            this.grpText = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnColor = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.grpText.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(102, 144);
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
            this.btnCancel.Location = new System.Drawing.Point(198, 144);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnColor);
            this.groupBox2.Controls.Add(this.radOpaque);
            this.groupBox2.Controls.Add(this.radTransparent);
            this.groupBox2.ForeColor = System.Drawing.Color.Ivory;
            this.groupBox2.Location = new System.Drawing.Point(12, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(343, 50);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Text Characteristic";
            // 
            // radOpaque
            // 
            this.radOpaque.Location = new System.Drawing.Point(248, 20);
            this.radOpaque.Name = "radOpaque";
            this.radOpaque.Size = new System.Drawing.Size(77, 19);
            this.radOpaque.TabIndex = 2;
            this.radOpaque.TabStop = true;
            this.radOpaque.Text = "Opaque";
            this.radOpaque.UseVisualStyleBackColor = true;
            this.radOpaque.CheckedChanged += new System.EventHandler(this.radThree_CheckedChanged);
            // 
            // radTransparent
            // 
            this.radTransparent.Location = new System.Drawing.Point(132, 20);
            this.radTransparent.Name = "radTransparent";
            this.radTransparent.Size = new System.Drawing.Size(97, 19);
            this.radTransparent.TabIndex = 1;
            this.radTransparent.TabStop = true;
            this.radTransparent.Text = "Transparent";
            this.radTransparent.UseVisualStyleBackColor = true;
            this.radTransparent.CheckedChanged += new System.EventHandler(this.radTwo_CheckedChanged);
            // 
            // grpText
            // 
            this.grpText.Controls.Add(this.textBox1);
            this.grpText.ForeColor = System.Drawing.Color.Ivory;
            this.grpText.Location = new System.Drawing.Point(12, 7);
            this.grpText.Name = "grpText";
            this.grpText.Size = new System.Drawing.Size(343, 71);
            this.grpText.TabIndex = 4;
            this.grpText.TabStop = false;
            this.grpText.Text = "Type your text here";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(6, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(331, 21);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Enter Text";
            // 
            // btnColor
            // 
            this.btnColor.ForeColor = System.Drawing.Color.Black;
            this.btnColor.Location = new System.Drawing.Point(21, 18);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(75, 23);
            this.btnColor.TabIndex = 3;
            this.btnColor.Text = "Color";
            this.btnColor.UseVisualStyleBackColor = true;
            // 
            // CAnnotateDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(367, 176);
            this.Controls.Add(this.grpText);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Ivory;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CAnnotateDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Annotate";
            this.groupBox2.ResumeLayout(false);
            this.grpText.ResumeLayout(false);
            this.grpText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radTransparent;
        private System.Windows.Forms.RadioButton radOpaque;
        private System.Windows.Forms.GroupBox grpText;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnColor;
    }
}