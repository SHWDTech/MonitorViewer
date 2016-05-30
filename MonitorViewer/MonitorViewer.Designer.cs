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
            ViewerBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(ViewerBox)).BeginInit();
            SuspendLayout();
            // 
            // ViewerBox
            // 
            ViewerBox.Cursor = System.Windows.Forms.Cursors.Hand;
            ViewerBox.Dock = System.Windows.Forms.DockStyle.Fill;
            ViewerBox.Location = new System.Drawing.Point(0, 0);
            ViewerBox.Name = "ViewerBox";
            ViewerBox.Size = new System.Drawing.Size(150, 150);
            ViewerBox.TabIndex = 0;
            ViewerBox.TabStop = false;
            // 
            // MonitorViewer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            Controls.Add(ViewerBox);
            Name = "MonitorViewer";
            ((System.ComponentModel.ISupportInitialize)(ViewerBox)).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ViewerBox;
    }
}
