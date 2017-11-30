namespace Game_Of_Life
{
    partial class Form2
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
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.rbtBox = new System.Windows.Forms.GroupBox();
            this.rbt3 = new System.Windows.Forms.RadioButton();
            this.rbt2 = new System.Windows.Forms.RadioButton();
            this.rbt1 = new System.Windows.Forms.RadioButton();
            this.rbt4 = new System.Windows.Forms.RadioButton();
            this.rbt5 = new System.Windows.Forms.RadioButton();
            this.rowlb = new System.Windows.Forms.Label();
            this.rowtb = new System.Windows.Forms.TextBox();
            this.collb = new System.Windows.Forms.Label();
            this.coltb = new System.Windows.Forms.TextBox();
            this.rbtBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // accept
            // 
            this.accept.Location = new System.Drawing.Point(12, 160);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 0;
            this.accept.Text = "적용";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(93, 160);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "취소";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // rbtBox
            // 
            this.rbtBox.Controls.Add(this.coltb);
            this.rbtBox.Controls.Add(this.rowtb);
            this.rbtBox.Controls.Add(this.collb);
            this.rbtBox.Controls.Add(this.rowlb);
            this.rbtBox.Controls.Add(this.rbt5);
            this.rbtBox.Controls.Add(this.rbt4);
            this.rbtBox.Controls.Add(this.rbt3);
            this.rbtBox.Controls.Add(this.rbt2);
            this.rbtBox.Controls.Add(this.rbt1);
            this.rbtBox.Location = new System.Drawing.Point(3, 3);
            this.rbtBox.Name = "rbtBox";
            this.rbtBox.Size = new System.Drawing.Size(165, 151);
            this.rbtBox.TabIndex = 3;
            this.rbtBox.TabStop = false;
            this.rbtBox.Text = "설정";
            // 
            // rbt3
            // 
            this.rbt3.AutoSize = true;
            this.rbt3.Location = new System.Drawing.Point(9, 64);
            this.rbt3.Name = "rbt3";
            this.rbt3.Size = new System.Drawing.Size(54, 16);
            this.rbt3.TabIndex = 5;
            this.rbt3.TabStop = true;
            this.rbt3.Text = "40x40";
            this.rbt3.UseVisualStyleBackColor = true;
            // 
            // rbt2
            // 
            this.rbt2.AutoSize = true;
            this.rbt2.Location = new System.Drawing.Point(9, 42);
            this.rbt2.Name = "rbt2";
            this.rbt2.Size = new System.Drawing.Size(54, 16);
            this.rbt2.TabIndex = 4;
            this.rbt2.TabStop = true;
            this.rbt2.Text = "30x30";
            this.rbt2.UseVisualStyleBackColor = true;
            // 
            // rbt1
            // 
            this.rbt1.AutoSize = true;
            this.rbt1.Location = new System.Drawing.Point(9, 20);
            this.rbt1.Name = "rbt1";
            this.rbt1.Size = new System.Drawing.Size(54, 16);
            this.rbt1.TabIndex = 3;
            this.rbt1.TabStop = true;
            this.rbt1.Text = "20x20";
            this.rbt1.UseVisualStyleBackColor = true;
            // 
            // rbt4
            // 
            this.rbt4.AutoSize = true;
            this.rbt4.Location = new System.Drawing.Point(9, 86);
            this.rbt4.Name = "rbt4";
            this.rbt4.Size = new System.Drawing.Size(54, 16);
            this.rbt4.TabIndex = 5;
            this.rbt4.TabStop = true;
            this.rbt4.Text = "50x50";
            this.rbt4.UseVisualStyleBackColor = true;
            // 
            // rbt5
            // 
            this.rbt5.AutoSize = true;
            this.rbt5.Location = new System.Drawing.Point(9, 108);
            this.rbt5.Name = "rbt5";
            this.rbt5.Size = new System.Drawing.Size(83, 16);
            this.rbt5.TabIndex = 5;
            this.rbt5.TabStop = true;
            this.rbt5.Text = "사용자설정";
            this.rbt5.UseVisualStyleBackColor = true;
            this.rbt5.CheckedChanged += new System.EventHandler(this.rbt5_CheckedChanged);
            // 
            // rowlb
            // 
            this.rowlb.AutoSize = true;
            this.rowlb.Location = new System.Drawing.Point(10, 133);
            this.rowlb.Name = "rowlb";
            this.rowlb.Size = new System.Drawing.Size(30, 12);
            this.rowlb.TabIndex = 6;
            this.rowlb.Text = "row:";
            // 
            // rowtb
            // 
            this.rowtb.Location = new System.Drawing.Point(46, 130);
            this.rowtb.Name = "rowtb";
            this.rowtb.Size = new System.Drawing.Size(36, 21);
            this.rowtb.TabIndex = 7;
            // 
            // collb
            // 
            this.collb.AutoSize = true;
            this.collb.Location = new System.Drawing.Point(88, 133);
            this.collb.Name = "collb";
            this.collb.Size = new System.Drawing.Size(26, 12);
            this.collb.TabIndex = 6;
            this.collb.Text = "col:";
            // 
            // coltb
            // 
            this.coltb.Location = new System.Drawing.Point(120, 130);
            this.coltb.Name = "coltb";
            this.coltb.Size = new System.Drawing.Size(36, 21);
            this.coltb.TabIndex = 7;
            // 
            // Form2
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(171, 190);
            this.Controls.Add(this.rbtBox);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.Name = "Form2";
            this.Text = "select";
            this.rbtBox.ResumeLayout(false);
            this.rbtBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.GroupBox rbtBox;
        private System.Windows.Forms.RadioButton rbt3;
        private System.Windows.Forms.RadioButton rbt2;
        private System.Windows.Forms.RadioButton rbt1;
        private System.Windows.Forms.RadioButton rbt5;
        private System.Windows.Forms.RadioButton rbt4;
        private System.Windows.Forms.TextBox coltb;
        private System.Windows.Forms.TextBox rowtb;
        private System.Windows.Forms.Label collb;
        private System.Windows.Forms.Label rowlb;
    }
}