namespace Server
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
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtServerLog = new System.Windows.Forms.RichTextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btSend = new System.Windows.Forms.Button();
            this.btStart = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(12, 24);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(100, 21);
            this.txtIp.TabIndex = 0;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(130, 24);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 21);
            this.txtPort.TabIndex = 1;
            // 
            // txtServerLog
            // 
            this.txtServerLog.Location = new System.Drawing.Point(22, 78);
            this.txtServerLog.Name = "txtServerLog";
            this.txtServerLog.Size = new System.Drawing.Size(238, 171);
            this.txtServerLog.TabIndex = 2;
            this.txtServerLog.Text = "";
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(12, 278);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(238, 21);
            this.txtSend.TabIndex = 3;
            this.txtSend.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSend_KeyUp);
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(256, 278);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(75, 23);
            this.btSend.TabIndex = 4;
            this.btSend.Text = "보내기";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(256, 47);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 5;
            this.btStart.Text = "시작";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(256, 76);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(75, 23);
            this.btStop.TabIndex = 6;
            this.btStop.Text = "중지";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 344);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtServerLog);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIp);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.RichTextBox txtServerLog;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btStop;
    }
}

