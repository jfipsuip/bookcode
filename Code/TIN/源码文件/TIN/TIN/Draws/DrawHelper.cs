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
    public class DrawHelper : DrawHelperBase
    {
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

        public DrawHelper(PictureBox pictureBox) : base(pictureBox)
        {
        }
        /// <summary>
        /// 修改方法 画图
        /// </summary>
        protected override void DrawImage()
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
            Graphics.DrawEllipse(Pens.Black, p.X - n, p.Y - n, m, m);
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
            Graphics.DrawRectangle(pen, p.X - n, p.Y - n, 2 * n, 2 * n);

            Graphics.DrawString(point.Name, SystemFonts.DefaultFont, Brushes.Red, p.X, p.Y);
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
            Graphics.DrawLines(pen, ps);
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
            Graphics.DrawLines(pen, ps);
        }
    }
}
