﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TIN.Draws
{
    public class DrawHelper<T> where T : IPoint
    {
        /// <summary>
        /// 画图相关 
        /// </summary>
        double xAverage, yAverage, xMax, yMax, picHeight;
        Graphics graphics;
        /// <summary>
        /// 图形大小
        /// </summary>
        double Zoom = 3.00;
        /// <summary>
        /// 偏移坐标
        /// </summary>
        Point Go = new Point();
        public PictureBox PictureBox { get; }
        /// <summary>
        /// 输入的点集
        /// </summary>
        public List<T> Points { get; set; }
        /// <summary>
        /// 输入的线集
        /// </summary>
        public List<T> PointLines { get; set; }

        public DrawHelper(PictureBox pictureBox)
        {
            PictureBox = pictureBox;
            Image image = new Bitmap(PictureBox.Size.Width, PictureBox.Size.Height);
            PictureBox.Image = image;
            graphics = Graphics.FromImage(image);
        }
        /// <summary>
        /// 画图
        /// </summary>
        public void Draw()
        {
            Initial();
            DrawPoint();
            DrawLine();
        }
        /// <summary>
        /// 计算画图基础参数
        /// </summary>
        void Initial()
        {
            xAverage = Points.Average(t => t.X);
            yAverage = Points.Average(t => t.Y);

            xMax = Math.Max(Points.Max(t => Math.Abs(t.X - xAverage)), 1);
            yMax = Math.Max(Points.Max(t => Math.Abs(t.Y - yAverage)), 1);

            picHeight = PictureBox.Size.Height;
        }

        public void DrawPoint()
        {
            List<Point> points;
            if (Points.Count() == 0)
            {
                return;
            }
            //投影到图像坐标系      
            points = Points.Select(t => GetPoint(t)).ToList();

            //画点
            DrawPoint(graphics, points);
        }
        public void DrawLine()
        {
            List<Point> points;
            if (PointLines.Count() == 0)
            {
                return;
            }
            //投影到图像坐标系
            points = PointLines.Select(t => GetPoint(t)).ToList();

            // 画线
            DrawLine(graphics, points);
        }
        private Point GetPoint(IPoint point)
        {
            return GetPoint(point, picHeight, xAverage, yAverage, xMax, yMax, Zoom, Go.X, Go.Y);
        }
        /// <summary>
        /// 转换一个输入坐标为图像坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private static Point GetPoint(IPoint point, double picHeight, double xAverage, double yAverage, double xMax, double yMax, double Zoom, double xGo, double yGo)
        {
            Point result;

            int x = (int)(picHeight / 2 + xGo + (point.X - xAverage) * picHeight / xMax / Zoom);
            int y = (int)(picHeight / 2 - yGo - (point.Y - yAverage) * picHeight / yMax / Zoom);
            result = new Point(x, y);

            return result;
        }

        private static void DrawPoint(Graphics graphics, IEnumerable<Point> points)
        {
            Bitmap map = CreateMap();
            foreach (var point in points)
            {
                graphics.DrawImage(map, point);
            }
        }
        private static void DrawLine(Graphics graphics, List<Point> points)
        {
            Bitmap map = CreateMap();

            for (int i = 0; i < points.Count(); i++)
            {
                graphics.DrawLine(new Pen(Color.Black), points[i], points[++i]);

            }
        }
        /// <summary>
        /// 画一个点
        /// </summary>
        /// <returns></returns>
        private static Bitmap CreateMap()
        {
            Bitmap map = new Bitmap(3, 3);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    map.SetPixel(i, j, Color.Red);
                }
            }

            return map;
        }



    }
}
