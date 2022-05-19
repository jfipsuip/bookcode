using System;
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
        /// 点集 凸包点
        /// </summary>
        public List<T> Points { get; set; }
        /// <summary>
        /// 点集 内部点
        /// </summary>
        public List<T> Points2 { get; set; }
        /// <summary>
        /// 线集  绘三角形边线
        /// </summary>
        public List<List<T>> Lines { get; set; }
        /// <summary>
        /// 线集 绘制凸多边形
        /// </summary>
        public List<List<T>> Lines2 { get; set; }

        public DrawHelper(PictureBox pictureBox)
        {
            PictureBox = pictureBox;
            Image image = new Bitmap(PictureBox.Size.Width, PictureBox.Size.Height);
            PictureBox.Image = image;
            graphics = Graphics.FromImage(image);
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
        /// <summary>
        /// 画图
        /// </summary>
        public void Draw()
        {
            Initial();

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
        /// <summary>
        /// 画一个点 用黑色○标记
        /// </summary>
        /// <param name="point"></param>
        private void DrawPointBlack(IPoint point)
        {
            int n = 10;
            int m = n / 2;
            var p = GetPoint(point);
            graphics.DrawEllipse(Pens.Black, p.X - m, p.Y - m, 10, 10);
        }
        /// <summary>
        /// 画一个点 用红色□标记
        /// </summary>
        /// <param name="point"></param>
        private void DrawPointRed(IPoint point)
        {
            int n = 10;
            int m = n / 2;
            var p = GetPoint(point);
            var pen = new Pen(Color.Red, 2);
            graphics.DrawRectangle(pen, p.X - m, p.Y - m, 10, 10);
        }
        /// <summary>
        /// 红色实线
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        private void DrawLineRed(List<T> points)
        {
            var ps = points.Select(t => GetPoint(t)).ToArray();
            var pen = new Pen(Color.Red, 3);
            graphics.DrawLines(pen, ps);
        }
        /// <summary>
        /// 灰色虚线
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        private void DrawLineGray(List<T> points)
        {
            var ps = points.Select(t => GetPoint(t)).ToArray();
            var pen = new Pen(Color.Gray, 1);
            graphics.DrawLines(pen, ps);
        }
    }
}
