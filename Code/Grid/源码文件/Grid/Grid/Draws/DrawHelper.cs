using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grid.Draws
{
    public class DrawHelper
    {
        public PictureBox PictureBox { get; }

        /// <summary>
        /// 输入的点集
        /// </summary>
        public List<Grids.Point> Points
        {
            get;
            set;
        }   //输入的点集
        /// <summary>
        /// 输入的线集
        /// </summary>
        public List<Grids.Point> PointLines { get; set; }
        // bool rdbcheck = false;
        /// <summary>
        /// 画图相关 
        /// </summary>
        double x_average
        {
            get
            {
                return Points.Average(t => t.X);
            }
        }
        double y_average
        {
            get
            {
                return Points.Average(t => t.Y);
            }
        }
        double x_max
        {
            get
            {
                double result = Points.Max(t => Math.Abs(t.X - x_average));
                return Math.Max(result, 1);
            }
        }
        double y_max
        {
            get
            {
                double result = Points.Max(t => Math.Abs(t.Y - y_average));
                return Math.Max(result, 1);
            }
        }

        double pic_H
        {
            get
            {
                return PictureBox.Size.Height;
            }
        }
        Graphics graphics;

        /// <summary>
        /// 图形大小
        /// </summary>
        public double Zoom = 3.00;
        /// <summary>
        /// 偏移坐标
        /// </summary>
        public Point Go = new Point();

        public DrawHelper()
        {

        }
        public DrawHelper(PictureBox pictureBox) : this()
        {
            Image image = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            pictureBox.Image = image;
            graphics = Graphics.FromImage(image);
            PictureBox = pictureBox;
        }

        public void DrawPoint()
        {
            List<Point> points;
            if (Points == null || Points.Count() == 0)
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
            if (PointLines == null || PointLines.Count == 0)
            {
                return;
            }
            //投影到图像坐标系
            points = PointLines.Select(t => GetPoint(t)).ToList();

            // 画线
            DrawLine(graphics, points);
        }

        /// <summary>
        /// 转换一个输入坐标为图像坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private Point GetPoint(Grids.Point point)
        {
            Point result;

            int x = (int)(pic_H / 2 + Go.X + (point.X - x_average) * pic_H / x_max / Zoom);
            int y = (int)(pic_H / 2 - Go.Y - (point.Y - y_average) * pic_H / y_max / Zoom);
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


        public void Draw()
        {

            DrawPoint();
            DrawLine();
        }

    }
}
