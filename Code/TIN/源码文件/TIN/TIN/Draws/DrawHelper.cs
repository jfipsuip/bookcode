using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TIN.Draws
{
    public class DrawHelper
    {
        public PictureBox PictureBox { get; }

        /// <summary>
        /// 输入的点集
        /// </summary>
        public TIN.Point[] Points
        {
            get;
            set;
        }   //输入的点集
        /// <summary>
        /// 输入的线集
        /// </summary>
        public List<TIN.Point> PointsL { get; set; }

        /// <summary>
        /// 输入的线集
        /// </summary>
        public List<TIN.Point> PointLines { get; set; }



        internal List<TIN.Point> contourLine = new List<TIN.Point>(); //等高线

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
        Image image;

        public TIN.Point[] ph;
        public Point[] q;
        public Point[] qh;
        /// <summary>
        /// 图形大小
        /// </summary>
        public double Zoom = 3.00;
        /// <summary>
        /// 偏移坐标
        /// </summary>
        public Point Go = new Point();
        public bool Clicked = false;

        public DrawHelper()
        {

        }
        public DrawHelper(PictureBox pictureBox) : this()
        {
            PictureBox = pictureBox;
            image = new Bitmap(PictureBox.Size.Width, PictureBox.Size.Height);
            PictureBox.Image = image;
        }

        public void DrawPoint()
        {
            List<Point> points;
            if (Points.Length == 0)
            {
                return;
            }
            //投影到图像坐标系      
            points = Points.Select(t => GetPoint(t)).ToList();

            //画点
            DrawPoint(image, points);
        }
        public void DrawLine()
        {
            List<Point> points;
            if (PointsL.Count() == 0)
            {
                return;
            }
            //投影到图像坐标系
            points = PointsL.Select(t => GetPoint(t)).ToList();

            // 画线
            DrawLine(image, points);
        }

        /// <summary>
        /// 转换一个输入坐标为图像坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private Point GetPoint(TIN.Point point)
        {
            Point result;

            int x = (int)(pic_H / 2 + Go.X + (point.X - x_average) * pic_H / x_max / Zoom);
            int y = (int)(pic_H / 2 - Go.Y - (point.Y - y_average) * pic_H / y_max / Zoom);
            result = new Point(x, y);

            return result;
        }

        private static void DrawPoint(Image image, IEnumerable<Point> points)
        {
            Graphics graphics = Graphics.FromImage(image);
            Bitmap map = CreateMap();
            foreach (var point in points)
            {
                graphics.DrawImage(map, point);
            }
        }
        private static void DrawLine(PictureBox pictureBox, List<Point> points)
        {
            Image image = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            Graphics graphics = Graphics.FromImage(image);
            Bitmap map = CreateMap();

            for (int i = 0; i < points.Count(); i++)
            {
                graphics.DrawLine(new Pen(Color.Black), points[i], points[i++]);

            }
            pictureBox.Image = image;
        }
        private static void DrawLine(Image image, List<Point> points)
        {
            Graphics graphics = Graphics.FromImage(image);
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
            //Image image = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            //pictureBox.Image = image;
            //List<Point> points;

            //points = PointsL.Select(t => GetPoint(t)).ToList();

            //DrawLine(image, points);
            DrawPoint();
            DrawLine();
            //GetPic_Line();
        }

    }
}
