﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.UserControls.VideoPanels
{
    public partial class UCVideoPanelGrid : UserControl
    {
        #region 私有变量
        private readonly int _highLightCount = 10;
        private VideoPanel _activePanel;
        private readonly int _gap = 2;  //各个视频之间的空隙
        private int _Rows;
        private int _Columns;
        private List<VideoPanel> _Videoes = new List<VideoPanel>();
        #endregion

        #region 构造函数
        public UCVideoPanelGrid()
        {
            InitializeComponent();
            this.SetShowMode(2, 2);
            this.Resize += new EventHandler(UCVideoPanelGrid_Resize);
        }
        #endregion

        #region 私有方法
        private void UCVideoPanelGrid_Resize(object sender, EventArgs e)
        {
            LayoutVideoes();
        }

        private void LayoutVideoes()
        {
            int width = 0;
            int height = 0;

            if (_Rows > 0 && _Columns > 0)
            {
                this.SuspendLayout();
                width = (this.Width - _gap * (_Columns - 1)) / _Columns;
                height = (this.Height - _gap * (_Rows - 1)) / _Rows;

                for (int i = 0; i < _Rows; i++)
                {
                    for (int j = 0; j < _Columns; j++)
                    {
                        _Videoes[i * _Columns + j].Left = (width + _gap) * j;
                        _Videoes[i * _Columns + j].Top = (height + _gap) * i;
                        _Videoes[i * _Columns + j].Width = width;
                        _Videoes[i * _Columns + j].Height = height;
                    }
                }
                this.ResumeLayout(false);
            }
        }

        /// <summary>
        /// 创建视频窗口
        /// </summary>
        /// <param name="videoType"></param>
        /// <param name="name"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        private VideoPanel CreateVideoPanel(int videoType, string name, bool visible)
        {
            VideoPanel video = VideoPanelFactory.CreatePanel(videoType);
            video.AllowDrop = true;
            video.Name = name;
            video.ShowTitle = true;
            video.StretchToFit = true;
            video.VideoSource = null;
            this.Controls.Add(video);
            video.Visible = visible;
            video.VideoDragDropHandling -= VideoDragDropHandling;
            video.VideoDragDropHandling += VideoDragDropHandling;
            return video;
        }

        //从窗体控件中移除视频控件
        private void RemoveVideoPanel(VideoPanel p)
        {
            p.Close();
            p.VideoDragDropHandling -= VideoDragDropHandling;
            this.Controls.Remove(p);
        }

        //获取空闲的视频
        private VideoPanel GetFreeVideoPanel(VideoSourceInfo video)
        {
            if (video != null)
            {
                VideoPanel panel = _Videoes.FirstOrDefault(vp => vp.VideoSource == null);
                if (panel != null)
                {
                    if (video.VideoSourceType != panel.VideoType)
                    {
                        int index = _Videoes.FindIndex(vp => vp == panel);
                        if (index >= 0)
                        {
                            RemoveVideoPanel(panel);

                            //视频类型不同的，重新创建视频
                            VideoPanel p = CreateVideoPanel(video.VideoSourceType, panel.Name, panel.Visible);
                            p.Left = panel.Left;
                            p.Top = panel.Top;
                            p.Width = panel.Width;
                            p.Height = panel.Height;
                            _Videoes[index] = p;

                            return p;
                        }
                    }
                }
                return panel;
            }
            return null;
        }
        #endregion

        #region 事件处理
        private void VideoDragDropHandling(object sender, VideoSourceInfo video)
        {
            VideoPanel panel = sender as VideoPanel;
            if (panel != null)
            {
                int index = _Videoes.FindIndex(vp => vp == panel);
                if (index >= 0)
                {
                    RemoveVideoPanel(panel);

                    //视频类型不同的，重新创建视频
                    VideoPanel p = CreateVideoPanel(video.VideoSourceType, panel.Name, panel.Visible);
                    p.Left = panel.Left;
                    p.Top = panel.Top;
                    p.Width = panel.Width;
                    p.Height = panel.Height;
                    _Videoes[index] = p;

                    p.VideoSource = video;
                    p.Play(true);
                }
            }
        }
        #endregion

        #region 公共方法和属性
        /// <summary>
        /// 获取视频网格的行数
        /// </summary>
        public int Rows
        {
            get { return _Rows; }
        }

        /// <summary>
        /// 获取视频网格的列数
        /// </summary>
        public int Columns
        {
            get
            {
                return _Columns;
            }
        }

        /// <summary>
        /// 获取视频网格中所有视频
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public IEnumerable<VideoPanel> VideoPanelCollection
        {
            get
            {
                return _Videoes.ToArray();
            }
        }

        /// <summary>
        /// 获取视频网格可以显示的最多视频数量
        /// </summary>
        public int VideoCapacity
        {
            get
            {
                return Rows * Columns;
            }
        }

        /// <summary>
        /// 显示并播放视频,如果视频已经在网格中，则播放它.如果已经打开的视频大于网格可以显示的最大视频数量,则超出的视频不会显示出来
        /// </summary>
        public void RenderAndPlayVideoes(List<VideoSourceInfo> videoes)
        {
            if (videoes != null)
            {
                foreach (VideoSourceInfo vs in videoes)
                {
                    RenderAndPlayVideo(vs);
                }
            }
        }

        /// <summary>
        /// 显示并播放视频,如果视频已经在网格中，则播放它.如果已经打开的视频大于网格可以显示的最大视频数量,则超出的视频不会显示出来
        /// </summary>
        /// <param name="video"></param>
        public void RenderAndPlayVideo(VideoSourceInfo video)
        {
            VideoPanel p = _Videoes.SingleOrDefault(vp => vp.VideoSource == video);
            if (p != null)
            {
                p.Play(true);
            }
            else
            {
                p = GetFreeVideoPanel(video) ;
                if (p != null)
                {
                    p.VideoSource = video;
                    p.Play(true);
                }
            }
        }

        /// <summary>
        /// 显示视频,如果视频已经在网格中，则不进行操作.如果已经打开的视频多于网格的行列数,则超出的视频不会显示出来
        /// </summary>
        public void RenderVideoes(List<VideoSourceInfo> videoes)
        {
            if (videoes != null)
            {
                foreach (VideoSourceInfo vs in videoes)
                {
                    if (!_Videoes.Exists(vp => vp.VideoSource == vs))
                    {
                        VideoPanel p = GetFreeVideoPanel(vs);
                        if (p != null)
                        {
                            p.VideoSource = vs;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 把视频网格设置成行列格局
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public void SetShowMode(int rows, int columns)
        {
            if (rows > 0 && columns > 0)
            {
                _Rows = rows;
                _Columns = columns;
                int videoType = (int)VideoServerType.ACTi;
                if (UserSetting.Current != null)
                {
                    videoType = UserSetting.Current.VideoType;
                }
                for (int i = _Videoes.Count; i < rows * columns; i++)
                {
                    VideoPanel video = CreateVideoPanel(videoType, "actiVideoPanel" + i.ToString(), true);

                    //VideoPanel video = VideoPanelFactory.CreatePanel();
                    //video.AllowDrop = true;
                    //video.Name = "actiVideoPanel" + i.ToString();
                    //video.ShowTitle = true;
                    //video.StretchToFit = true;
                    //video.VideoSource = null;
                    //this.Controls.Add(video);
                    //video.Visible = true;
                    _Videoes.Add(video);
                }
                //只显示行列数,其它的隐藏
                for (int i = 0; i < _Videoes.Count; i++)
                {
                    _Videoes[i].Visible = i < rows * columns ? true : false;
                }

                LayoutVideoes();
            }
        }

        /// <summary>
        /// 清空所有视频
        /// </summary>
        public void Clear()
        {
            foreach (VideoPanel vp in _Videoes)
            {
                if (vp.Status == VideoStatus.Playing)
                {
                    vp.Close();
                }
            }
        }
        #endregion
    }
}
