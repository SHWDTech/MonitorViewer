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
            this.ViewerBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ViewerBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ViewerBox
            // 
            this.ViewerBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ViewerBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewerBox.Location = new System.Drawing.Point(0, 0);
            this.ViewerBox.Name = "ViewerBox";
            this.ViewerBox.Size = new System.Drawing.Size(150, 163);
            this.ViewerBox.TabIndex = 0;
            this.ViewerBox.TabStop = false;
            // 
            // MonitorViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.ViewerBox);
            this.Name = "MonitorViewer";
            this.Size = new System.Drawing.Size(150, 163);
            ((System.ComponentModel.ISupportInitialize)(this.ViewerBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ViewerBox;
    }
}
