﻿namespace MonitorViewerTestForm
{
    partial class Form1
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
            this.monitorViewer1 = new MonitorViewer.MonitorViewer();
            this.SuspendLayout();
            // 
            // monitorViewer1
            // 
            this.monitorViewer1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.monitorViewer1.Location = new System.Drawing.Point(1, 1);
            this.monitorViewer1.Name = "monitorViewer1";
            this.monitorViewer1.Size = new System.Drawing.Size(974, 653);
            this.monitorViewer1.Speed = 4;
            this.monitorViewer1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 655);
            this.Controls.Add(this.monitorViewer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private MonitorViewer.MonitorViewer monitorViewer1;
    }
}
