namespace Game_Of_Life
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.startBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.Menu1 = new System.Windows.Forms.MenuStrip();
            this.메뉴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.게임크기설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.리셋ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.저장ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.불러오기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speed = new System.Windows.Forms.TrackBar();
            this.speedlb = new System.Windows.Forms.Label();
            this.Menu1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speed)).BeginInit();
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(32, 222);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "시작";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.start_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(175, 222);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(75, 23);
            this.stopBtn.TabIndex = 1;
            this.stopBtn.Text = "멈춤";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stop_Click);
            // 
            // Menu1
            // 
            this.Menu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.메뉴ToolStripMenuItem});
            this.Menu1.Location = new System.Drawing.Point(0, 0);
            this.Menu1.Name = "Menu1";
            this.Menu1.Size = new System.Drawing.Size(280, 24);
            this.Menu1.TabIndex = 2;
            this.Menu1.Text = "Menu";
            // 
            // 메뉴ToolStripMenuItem
            // 
            this.메뉴ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.게임크기설정ToolStripMenuItem,
            this.리셋ToolStripMenuItem,
            this.저장ToolStripMenuItem,
            this.불러오기ToolStripMenuItem,
            this.종료ToolStripMenuItem});
            this.메뉴ToolStripMenuItem.Name = "메뉴ToolStripMenuItem";
            this.메뉴ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.메뉴ToolStripMenuItem.Text = "메뉴";
            // 
            // 게임크기설정ToolStripMenuItem
            // 
            this.게임크기설정ToolStripMenuItem.Name = "게임크기설정ToolStripMenuItem";
            this.게임크기설정ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.게임크기설정ToolStripMenuItem.Text = "게임크기설정";
            this.게임크기설정ToolStripMenuItem.Click += new System.EventHandler(this.게임크기설정ToolStripMenuItem_Click);
            // 
            // 리셋ToolStripMenuItem
            // 
            this.리셋ToolStripMenuItem.Name = "리셋ToolStripMenuItem";
            this.리셋ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.리셋ToolStripMenuItem.Text = "리셋";
            this.리셋ToolStripMenuItem.Click += new System.EventHandler(this.리셋ToolStripMenuItem_Click);
            // 
            // 저장ToolStripMenuItem
            // 
            this.저장ToolStripMenuItem.Name = "저장ToolStripMenuItem";
            this.저장ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.저장ToolStripMenuItem.Text = "저장";
            this.저장ToolStripMenuItem.Click += new System.EventHandler(this.저장ToolStripMenuItem_Click);
            // 
            // 불러오기ToolStripMenuItem
            // 
            this.불러오기ToolStripMenuItem.Name = "불러오기ToolStripMenuItem";
            this.불러오기ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.불러오기ToolStripMenuItem.Text = "불러오기";
            this.불러오기ToolStripMenuItem.Click += new System.EventHandler(this.불러오기ToolStripMenuItem_Click);
            // 
            // 종료ToolStripMenuItem
            // 
            this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
            this.종료ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.종료ToolStripMenuItem.Text = "종료";
            this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
            // 
            // speed
            // 
            this.speed.Location = new System.Drawing.Point(32, 171);
            this.speed.Maximum = 19;
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(132, 45);
            this.speed.TabIndex = 3;
            this.speed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.speed.Scroll += new System.EventHandler(this.speed_Scroll);
            // 
            // speedlb
            // 
            this.speedlb.AutoSize = true;
            this.speedlb.Location = new System.Drawing.Point(175, 171);
            this.speedlb.Name = "speedlb";
            this.speedlb.Size = new System.Drawing.Size(38, 12);
            this.speedlb.TabIndex = 4;
            this.speedlb.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 266);
            this.Controls.Add(this.speedlb);
            this.Controls.Add(this.speed);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.Menu1);
            this.MainMenuStrip = this.Menu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Menu1.ResumeLayout(false);
            this.Menu1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.MenuStrip Menu1;
        private System.Windows.Forms.ToolStripMenuItem 메뉴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 게임크기설정ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 리셋ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 저장ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 불러오기ToolStripMenuItem;
        private System.Windows.Forms.TrackBar speed;
        private System.Windows.Forms.Label speedlb;
    }
}

