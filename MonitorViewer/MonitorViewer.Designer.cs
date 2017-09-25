namespace MonitorViewer
{
    partial class MonitorViewer
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartRealPlay = new System.Windows.Forms.Button();
            this.ViewerBox = new System.Windows.Forms.PictureBox();
            this.btnStopRealPlay = new System.Windows.Forms.Button();
            this.btnPtzUp = new System.Windows.Forms.Button();
            this.btnPtzDwon = new System.Windows.Forms.Button();
            this.btnPtzLeft = new System.Windows.Forms.Button();
            this.btnPtzRight = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ViewerBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartRealPlay
            // 
            this.btnStartRealPlay.Location = new System.Drawing.Point(3, 571);
            this.btnStartRealPlay.Name = "btnStartRealPlay";
            this.btnStartRealPlay.Size = new System.Drawing.Size(75, 23);
            this.btnStartRealPlay.TabIndex = 1;
            this.btnStartRealPlay.Text = "开启预览";
            this.btnStartRealPlay.UseVisualStyleBackColor = true;
            this.btnStartRealPlay.Click += new System.EventHandler(this.StartMonitor);
            // 
            // ViewerBox
            // 
            this.ViewerBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewerBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ViewerBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ViewerBox.Location = new System.Drawing.Point(0, 0);
            this.ViewerBox.Name = "ViewerBox";
            this.ViewerBox.Size = new System.Drawing.Size(800, 565);
            this.ViewerBox.TabIndex = 0;
            this.ViewerBox.TabStop = false;
            // 
            // btnStopRealPlay
            // 
            this.btnStopRealPlay.Enabled = false;
            this.btnStopRealPlay.Location = new System.Drawing.Point(84, 571);
            this.btnStopRealPlay.Name = "btnStopRealPlay";
            this.btnStopRealPlay.Size = new System.Drawing.Size(75, 23);
            this.btnStopRealPlay.TabIndex = 2;
            this.btnStopRealPlay.Text = "结束预览";
            this.btnStopRealPlay.UseVisualStyleBackColor = true;
            this.btnStopRealPlay.Click += new System.EventHandler(this.StopMonitor);
            // 
            // btnPtzUp
            // 
            this.btnPtzUp.Enabled = false;
            this.btnPtzUp.Location = new System.Drawing.Point(165, 571);
            this.btnPtzUp.Name = "btnPtzUp";
            this.btnPtzUp.Size = new System.Drawing.Size(75, 23);
            this.btnPtzUp.TabIndex = 3;
            this.btnPtzUp.Text = "云台上";
            this.btnPtzUp.UseVisualStyleBackColor = true;
            this.btnPtzUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PtzUp);
            this.btnPtzUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PtzStop);
            // 
            // btnPtzDwon
            // 
            this.btnPtzDwon.Enabled = false;
            this.btnPtzDwon.Location = new System.Drawing.Point(246, 571);
            this.btnPtzDwon.Name = "btnPtzDwon";
            this.btnPtzDwon.Size = new System.Drawing.Size(75, 23);
            this.btnPtzDwon.TabIndex = 4;
            this.btnPtzDwon.Text = "云台下";
            this.btnPtzDwon.UseVisualStyleBackColor = true;
            this.btnPtzDwon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PtzDown);
            this.btnPtzDwon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PtzStop);
            // 
            // btnPtzLeft
            // 
            this.btnPtzLeft.Enabled = false;
            this.btnPtzLeft.Location = new System.Drawing.Point(327, 571);
            this.btnPtzLeft.Name = "btnPtzLeft";
            this.btnPtzLeft.Size = new System.Drawing.Size(75, 23);
            this.btnPtzLeft.TabIndex = 5;
            this.btnPtzLeft.Text = "云台左";
            this.btnPtzLeft.UseVisualStyleBackColor = true;
            this.btnPtzLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PtzLeft);
            this.btnPtzLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PtzStop);
            // 
            // btnPtzRight
            // 
            this.btnPtzRight.Enabled = false;
            this.btnPtzRight.Location = new System.Drawing.Point(408, 571);
            this.btnPtzRight.Name = "btnPtzRight";
            this.btnPtzRight.Size = new System.Drawing.Size(75, 23);
            this.btnPtzRight.TabIndex = 6;
            this.btnPtzRight.Text = "云台右";
            this.btnPtzRight.UseVisualStyleBackColor = true;
            this.btnPtzRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PtzRight);
            this.btnPtzRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PtzStop);
            // 
            // MonitorViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.btnPtzRight);
            this.Controls.Add(this.btnPtzLeft);
            this.Controls.Add(this.btnPtzDwon);
            this.Controls.Add(this.btnPtzUp);
            this.Controls.Add(this.btnStopRealPlay);
            this.Controls.Add(this.btnStartRealPlay);
            this.Controls.Add(this.ViewerBox);
            this.Name = "MonitorViewer";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.ViewerBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnStartRealPlay;
        private System.Windows.Forms.PictureBox ViewerBox;
        private System.Windows.Forms.Button btnStopRealPlay;
        private System.Windows.Forms.Button btnPtzUp;
        private System.Windows.Forms.Button btnPtzDwon;
        private System.Windows.Forms.Button btnPtzLeft;
        private System.Windows.Forms.Button btnPtzRight;
    }
}
