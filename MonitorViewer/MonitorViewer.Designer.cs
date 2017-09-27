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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPtzZoomout = new System.Windows.Forms.Button();
            this.btnPtzZoomin = new System.Windows.Forms.Button();
            this.btnStartRealPlay = new System.Windows.Forms.Button();
            this.btnPtzRight = new System.Windows.Forms.Button();
            this.btnStopRealPlay = new System.Windows.Forms.Button();
            this.btnPtzLeft = new System.Windows.Forms.Button();
            this.btnPtzUp = new System.Windows.Forms.Button();
            this.btnPtzDwon = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ViewerBox = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPlayBack = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFiles = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewerBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnPtzZoomout);
            this.groupBox1.Controls.Add(this.btnPtzZoomin);
            this.groupBox1.Controls.Add(this.btnStartRealPlay);
            this.groupBox1.Controls.Add(this.btnPtzRight);
            this.groupBox1.Controls.Add(this.btnStopRealPlay);
            this.groupBox1.Controls.Add(this.btnPtzLeft);
            this.groupBox1.Controls.Add(this.btnPtzUp);
            this.groupBox1.Controls.Add(this.btnPtzDwon);
            this.groupBox1.Location = new System.Drawing.Point(3, 419);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(794, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "视频预览";
            // 
            // btnPtzZoomout
            // 
            this.btnPtzZoomout.Enabled = false;
            this.btnPtzZoomout.Location = new System.Drawing.Point(573, 19);
            this.btnPtzZoomout.Name = "btnPtzZoomout";
            this.btnPtzZoomout.Size = new System.Drawing.Size(75, 23);
            this.btnPtzZoomout.TabIndex = 8;
            this.btnPtzZoomout.Text = "远景";
            this.btnPtzZoomout.UseVisualStyleBackColor = true;
            this.btnPtzZoomout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PtzZoomout);
            this.btnPtzZoomout.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PtzStop);
            // 
            // btnPtzZoomin
            // 
            this.btnPtzZoomin.Enabled = false;
            this.btnPtzZoomin.Location = new System.Drawing.Point(492, 19);
            this.btnPtzZoomin.Name = "btnPtzZoomin";
            this.btnPtzZoomin.Size = new System.Drawing.Size(75, 23);
            this.btnPtzZoomin.TabIndex = 7;
            this.btnPtzZoomin.Text = "近景";
            this.btnPtzZoomin.UseVisualStyleBackColor = true;
            this.btnPtzZoomin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PtzZoomin);
            this.btnPtzZoomin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PtzStop);
            // 
            // btnStartRealPlay
            // 
            this.btnStartRealPlay.Location = new System.Drawing.Point(6, 19);
            this.btnStartRealPlay.Name = "btnStartRealPlay";
            this.btnStartRealPlay.Size = new System.Drawing.Size(75, 23);
            this.btnStartRealPlay.TabIndex = 1;
            this.btnStartRealPlay.Text = "开启预览";
            this.btnStartRealPlay.UseVisualStyleBackColor = true;
            this.btnStartRealPlay.Click += new System.EventHandler(this.StartMonitor);
            // 
            // btnPtzRight
            // 
            this.btnPtzRight.Enabled = false;
            this.btnPtzRight.Location = new System.Drawing.Point(411, 19);
            this.btnPtzRight.Name = "btnPtzRight";
            this.btnPtzRight.Size = new System.Drawing.Size(75, 23);
            this.btnPtzRight.TabIndex = 6;
            this.btnPtzRight.Text = "云台右";
            this.btnPtzRight.UseVisualStyleBackColor = true;
            this.btnPtzRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PtzRight);
            this.btnPtzRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PtzStop);
            // 
            // btnStopRealPlay
            // 
            this.btnStopRealPlay.Enabled = false;
            this.btnStopRealPlay.Location = new System.Drawing.Point(87, 19);
            this.btnStopRealPlay.Name = "btnStopRealPlay";
            this.btnStopRealPlay.Size = new System.Drawing.Size(75, 23);
            this.btnStopRealPlay.TabIndex = 2;
            this.btnStopRealPlay.Text = "结束预览";
            this.btnStopRealPlay.UseVisualStyleBackColor = true;
            this.btnStopRealPlay.Click += new System.EventHandler(this.StopMonitor);
            // 
            // btnPtzLeft
            // 
            this.btnPtzLeft.Enabled = false;
            this.btnPtzLeft.Location = new System.Drawing.Point(330, 19);
            this.btnPtzLeft.Name = "btnPtzLeft";
            this.btnPtzLeft.Size = new System.Drawing.Size(75, 23);
            this.btnPtzLeft.TabIndex = 5;
            this.btnPtzLeft.Text = "云台左";
            this.btnPtzLeft.UseVisualStyleBackColor = true;
            this.btnPtzLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PtzLeft);
            this.btnPtzLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PtzStop);
            // 
            // btnPtzUp
            // 
            this.btnPtzUp.Enabled = false;
            this.btnPtzUp.Location = new System.Drawing.Point(168, 19);
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
            this.btnPtzDwon.Location = new System.Drawing.Point(249, 19);
            this.btnPtzDwon.Name = "btnPtzDwon";
            this.btnPtzDwon.Size = new System.Drawing.Size(75, 23);
            this.btnPtzDwon.TabIndex = 4;
            this.btnPtzDwon.Text = "云台下";
            this.btnPtzDwon.UseVisualStyleBackColor = true;
            this.btnPtzDwon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PtzDown);
            this.btnPtzDwon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PtzStop);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ViewerBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // ViewerBox
            // 
            this.ViewerBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ViewerBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ViewerBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewerBox.Location = new System.Drawing.Point(3, 3);
            this.ViewerBox.Name = "ViewerBox";
            this.ViewerBox.Size = new System.Drawing.Size(794, 410);
            this.ViewerBox.TabIndex = 0;
            this.ViewerBox.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnPlayBack);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbFiles);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtpEnd);
            this.groupBox2.Controls.Add(this.dtpStart);
            this.groupBox2.Location = new System.Drawing.Point(3, 489);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(794, 108);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "视频回放";
            // 
            // btnPlayBack
            // 
            this.btnPlayBack.Enabled = false;
            this.btnPlayBack.Location = new System.Drawing.Point(426, 70);
            this.btnPlayBack.Name = "btnPlayBack";
            this.btnPlayBack.Size = new System.Drawing.Size(75, 23);
            this.btnPlayBack.TabIndex = 7;
            this.btnPlayBack.Text = "开始回放";
            this.btnPlayBack.UseVisualStyleBackColor = true;
            this.btnPlayBack.Click += new System.EventHandler(this.PlayBackControl);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "搜索结果";
            // 
            // cmbFiles
            // 
            this.cmbFiles.FormattingEnabled = true;
            this.cmbFiles.Location = new System.Drawing.Point(79, 70);
            this.cmbFiles.Name = "cmbFiles";
            this.cmbFiles.Size = new System.Drawing.Size(341, 21);
            this.cmbFiles.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(426, 26);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.SearchFiles);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "结束时间";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "开始时间";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(292, 29);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(128, 20);
            this.dtpEnd.TabIndex = 1;
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(79, 28);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(128, 20);
            this.dtpStart.TabIndex = 0;
            // 
            // MonitorViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MonitorViewer";
            this.Size = new System.Drawing.Size(800, 600);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewerBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStartRealPlay;
        private System.Windows.Forms.Button btnPtzRight;
        private System.Windows.Forms.Button btnStopRealPlay;
        private System.Windows.Forms.Button btnPtzLeft;
        private System.Windows.Forms.Button btnPtzUp;
        private System.Windows.Forms.Button btnPtzDwon;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox ViewerBox;
        private System.Windows.Forms.Button btnPtzZoomout;
        private System.Windows.Forms.Button btnPtzZoomin;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPlayBack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFiles;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
    }
}
