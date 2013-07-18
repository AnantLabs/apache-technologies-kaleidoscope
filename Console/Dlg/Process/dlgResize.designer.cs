namespace Kaleidoscope
{
    partial class CResizeDlg
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
            this.label1 = new System.Windows.Forms.Label();
            this.numUDXScale = new System.Windows.Forms.NumericUpDown();
            this.numeUDYScale = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numUDXScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeUDYScale)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnOK.Location = new System.Drawing.Point(47, 88);
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
            this.btnCancel.Location = new System.Drawing.Point(134, 88);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(66, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "X-Scale:";
            // 
            // numUDXScale
            // 
            this.numUDXScale.Location = new System.Drawing.Point(124, 22);
            this.numUDXScale.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numUDXScale.Name = "numUDXScale";
            this.numUDXScale.Size = new System.Drawing.Size(55, 21);
            this.numUDXScale.TabIndex = 4;
            this.numUDXScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUDXScale.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numeUDYScale
            // 
            this.numeUDYScale.Location = new System.Drawing.Point(124, 52);
            this.numeUDYScale.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numeUDYScale.Name = "numeUDYScale";
            this.numeUDYScale.Size = new System.Drawing.Size(55, 21);
            this.numeUDYScale.TabIndex = 6;
            this.numeUDYScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numeUDYScale.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y-Scale:";
            // 
            // CResizeDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(247, 124);
            this.Controls.Add(this.numeUDYScale);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numUDXScale);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Ivory;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CResizeDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Resize";
            ((System.ComponentModel.ISupportInitialize)(this.numUDXScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeUDYScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numUDXScale;
        private System.Windows.Forms.NumericUpDown numeUDYScale;
        private System.Windows.Forms.Label label2;
    }
}