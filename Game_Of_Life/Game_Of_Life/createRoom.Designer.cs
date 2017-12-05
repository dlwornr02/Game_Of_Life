namespace Game_Of_Life
{
    partial class createRoom
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Point = new System.Windows.Forms.TextBox();
            this.tb_gen = new System.Windows.Forms.TextBox();
            this.tb_row = new System.Windows.Forms.TextBox();
            this.tb_col = new System.Windows.Forms.TextBox();
            this.create = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Point : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Generation : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Size : ";
            // 
            // tb_Point
            // 
            this.tb_Point.Location = new System.Drawing.Point(94, 34);
            this.tb_Point.Name = "tb_Point";
            this.tb_Point.Size = new System.Drawing.Size(106, 21);
            this.tb_Point.TabIndex = 1;
            // 
            // tb_gen
            // 
            this.tb_gen.Location = new System.Drawing.Point(94, 68);
            this.tb_gen.Name = "tb_gen";
            this.tb_gen.Size = new System.Drawing.Size(106, 21);
            this.tb_gen.TabIndex = 1;
            // 
            // tb_row
            // 
            this.tb_row.Location = new System.Drawing.Point(94, 103);
            this.tb_row.Name = "tb_row";
            this.tb_row.Size = new System.Drawing.Size(50, 21);
            this.tb_row.TabIndex = 1;
            // 
            // tb_col
            // 
            this.tb_col.Location = new System.Drawing.Point(150, 103);
            this.tb_col.Name = "tb_col";
            this.tb_col.Size = new System.Drawing.Size(50, 21);
            this.tb_col.TabIndex = 1;
            // 
            // create
            // 
            this.create.Location = new System.Drawing.Point(34, 151);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(75, 23);
            this.create.TabIndex = 2;
            this.create.Text = "Create";
            this.create.UseVisualStyleBackColor = true;
            this.create.Click += new System.EventHandler(this.create_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(134, 151);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 2;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // createRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 186);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.create);
            this.Controls.Add(this.tb_col);
            this.Controls.Add(this.tb_row);
            this.Controls.Add(this.tb_gen);
            this.Controls.Add(this.tb_Point);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "createRoom";
            this.Text = "createRoom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_Point;
        private System.Windows.Forms.TextBox tb_gen;
        private System.Windows.Forms.TextBox tb_row;
        private System.Windows.Forms.TextBox tb_col;
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.Button cancel;
    }
}