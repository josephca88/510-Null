﻿namespace Ralid.Park.UserControls.VideoPanels
{
    partial class JingYangVideoPanel
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.video = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // video
            // 
            this.video.BackColor = System.Drawing.Color.Navy;
            this.video.Dock = System.Windows.Forms.DockStyle.Fill;
            this.video.Location = new System.Drawing.Point(0, 0);
            this.video.Name = "video";
            this.video.Size = new System.Drawing.Size(365, 253);
            this.video.TabIndex = 1;
            this.video.Resize += new System.EventHandler(this.video_Resize);
            // 
            // JingYangVideoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.video);
            this.Name = "JingYangVideoPanel";
            this.Controls.SetChildIndex(this.video, 0);
            this.Controls.SetChildIndex(this.TitlePanel, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel video;

    }
}
