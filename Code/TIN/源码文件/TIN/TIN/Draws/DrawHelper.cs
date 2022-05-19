using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = TIN.TIN.Point;

namespace TIN.Draws
{
    public class DrawHelper
    {
        /// <summary>
        /// 画图相关 
        /// </summary>
        double x1, y1, x2, y2, raid;
        Graphics graphics;
        /// <summary>
        /// 图形大小
        /// </summary>
        double Zoom { get; set; } = 0.9;
        /// <summary>
        /// 偏移坐标
        /// </summary>
        public System.Drawing.Point Go { get; set; } = new System.Drawing.Point();
        public PictureBox PictureBox { get; }
        /// <summary>
        /// 点集 凸包点
        /// </summary>
        public List<Point> Points { get; set; }
        /// <summary>
        /// 点集 内部点
        /// </summary>
        public List<Point> Points2 { get; set; }
        /// <summary>
        /// 线集  绘三角形边线
        /// </summary>
        public List<List<Point>> Lines { get; set; }
        /// <summary>
        /// 线集 绘制凸多边形
        /// </summary>
        public List<List<Point>> Lines2 { get; set; }

        public DrawHelper(PictureBox pictureBox)
        {
            PictureBox = pictureBox;
        }
        private void Initial()
        {

            Image image = new Bitmap(PictureBox.Size.Width, PictureBox.Size.Height);
            PictureBox.Image = image;
            graphics = Graphics.FromImage(image);
            graphics.Clear(Color.White);

            double xMin, yMin, xMax, yMax;
            xMin = Points.Min(t => t.X);
            yMin = Points.Min(t => t.Y);
            xMax = Points.Max(t => t.X);
            yMax = Points.Max(t => t.Y);

            //计算中心坐标
            x1 = (xMin + xMax) / 2;
            y1 = (yMin + yMax) / 2;

            x2 = PictureBox.Width / 2;
            y2 = PictureBox.Height / 2;

            //计算缩放系数
            raid = Math.Min(PictureBox.Width / (xMax - xMin), PictureBox.Height / (yMax - yMin));
        }
        private System.Drawing.Point ConvertPoint(Point point)
        {
            return ConvertPoint(point, x1, y1, raid * Zoom, x2 + Go.X, y2 + Go.Y);
        }
        private static System.Drawing.Point ConvertPoint(Point point, double x1, double y1, double raid, double x2, double y2)
        {
            System.Drawing.Point p;


            int x = (int)((point.X - x1) * raid + x2);
            int y = (int)(-(point.Y - y1) * raid + y2);

            p = new System.Drawing.Point(x, y);

            return p;
        }
        /// <summary>
        /// 画图
        /// </summary>
        public void Draw()
        {
            Initial();
            DrawData();
        }
        public void Reset()
        {
            // 设置初始参数
            Zoom = 0.9;
            Go = new System.Drawing.Point();

            Draw();
        }
        public void Magnify()
        {
            Zoom = Zoom * 1.1;
            Draw();
        }
        public void Minish()
        {
            Zoom = Zoom * 0.9;
            Draw();
        }
        /// <summary>
        /// 修改方法 画图
        /// </summary>
        private void DrawData()
        {
            // 画线
            Lines?.ForEach(ps =>
            {
                DrawLineGray(ps);
            });
            // 画线
            Lines2?.ForEach(ps =>
            {
                DrawLineRed(ps);
            });
            // 画点
            Points2?.ForEach(p =>
            {
                DrawPointBlack(p);
            });
            // 画点
            Points?.ForEach(p =>
            {
                DrawPointRed(p);
            });
        }


        /// <summary>
        /// 画一个点 用黑色○标记
        /// </summary>
        /// <param name="point"></param>
        private void DrawPointBlack(Point point)
        {
            // 点的半径 直径
            int n = 5, m = 2 * n;
            var p = ConvertPoint(point);
            graphics.DrawEllipse(Pens.Black, p.X - n, p.Y - n, m, m);
        }
        /// <summary>
        /// 画一个点 用红色□标记
        /// </summary>
        /// <param name="point"></param>
        private void DrawPointRed(Point point)
        {
            // 点的半径
            int n = 5;
            var p = ConvertPoint(point);
            var pen = new Pen(Color.Red, 2);
            graphics.DrawRectangle(pen, p.X - n, p.Y - n, 2 * n, 2 * n);

            graphics.DrawString(point.Name, SystemFonts.DefaultFont, Brushes.Red, p.X, p.Y);
        }
        /// <summary>
        /// 红色实线
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        private void DrawLineRed(List<Point> points)
        {
            var ps = points.Select(t => ConvertPoint(t)).ToArray();
            var pen = new Pen(Color.Red, 3);
            graphics.DrawLines(pen, ps);
        }
        /// <summary>
        /// 灰色虚线
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        private void DrawLineGray(List<Point> points)
        {
            var ps = points.Select(t => ConvertPoint(t)).ToArray();
            var pen = new Pen(Color.Gray, 1);
            // 虚线
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            graphics.DrawLines(pen, ps);
        }
    }
}
