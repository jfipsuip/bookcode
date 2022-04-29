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
        public PictureBox pictureBox;

        /// <summary>
        /// 输入的点集
        /// </summary>
        public PointF[] Points { get; set; }   //输入的点集
        /// <summary>
        /// 输入的线集
        /// </summary>
        public List<PointF> PointLines { get; set; }


        internal List<PointF> contourLine = new List<PointF>(); //等高线

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
                return Points.Max(t => Math.Abs(t.X - x_average));
            }
        }
        double y_max
        {
            get
            {
                return Points.Max(t => Math.Abs(t.Y - y_average));
            }
        }
        public PointF[] ph;
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


        public void GetPic_Point()
        {
            List<Point> points;
            double pic_H;

            pic_H = pictureBox.Size.Height;

            points = new List<Point>();
            //投影到图像坐标系
            foreach (var point in Points)
            {
                int x = (int)(pic_H / 2 + Go.X + (point.X - x_average) * pic_H / x_max / Zoom);
                int y = (int)(pic_H / 2 - Go.Y - (point.Y - y_average) * pic_H / y_max / Zoom);
                points.Add(new Point(x, y));
            }

            //画点
            DrawPoint(pictureBox, points);
        }

        private static void DrawPoint(PictureBox pictureBox, IEnumerable<Point> points)
        {
            Image image = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            Graphics graphics = Graphics.FromImage(image);
            Bitmap map = CreateMap();
            foreach (var point in points)
            {
                graphics.DrawImage(map, point);
            }
            pictureBox.Image = image;
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

        private void GetPic_Line()
        {
            int n1 = contourLine.Count;
            int n = PointLines.Count;

            PointF[] PointsD;
            PointsD = new PointF[n];
            ph = new PointF[n1];
            //
            for (int i = 0; i < n; i++)
            {
                PointsD[i].X = PointLines[i].X;
                PointsD[i].Y = PointLines[i].Y;
                PointsD[i].Z = PointLines[i].H;
            }
            for (int i = 0; i < n1; i++)
            {
                ph[i].X = contourLine[i].X;
                ph[i].Y = contourLine[i].Y;
                ph[i].Z = contourLine[i].Z;
            }
            //
            q = new Point[n];
            qh = new Point[n1];
            //
            for (int i = 0; i < n; i++)
            {
                PointsD[i].X -= x_average;
                PointsD[i].Y -= y_average;
            }

            for (int i = 0; i < n1; i++)
            {
                ph[i].X -= x_average;
                ph[i].Y -= y_average;
            }

            double pic_width = pictureBox.Size.Width;
            double pic_height = pictureBox.Size.Height;
            //
            for (int i = 0; i < n; i++)
            {
                q[i].X = (int)(pic_height / 2 + Go.X + PointsD[i].X * pic_height / x_max / Zoom);
                q[i].Y = (int)(pic_height / 2 - Go.Y - PointsD[i].Y * pic_height / y_max / Zoom);
            }

            for (int i = 0; i < n1; i++)
            {
                qh[i].X = (int)(pic_height / 2 + Go.X + ph[i].X * pic_height / Zoom / x_max);
                qh[i].Y = (int)(pic_height / 2 - Go.Y - ph[i].Y * pic_height / Zoom / y_max);
            }
            //
            Bitmap bitmap = new Bitmap(3, 3);
            Image image = bitmap;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    bitmap.SetPixel(i, j, Color.Red);

            Bitmap map = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            Image ge = map;
            Graphics gra = Graphics.FromImage(ge);

            for (int i = 0; i < n; i++)
            {
                gra.DrawImage(image, q[i]);
            }

            if (Clicked)
            {
                Point[] q2 = new Point[q.Length];  //三角形点的偏移坐标
                q.CopyTo(q2, 0);
                for (int i = 0; i < n; i++)
                {
                    q2[i].X += (int)(10 / Zoom);
                    q2[i].Y += (int)(10 / Zoom);
                }
                for (int i = 0; i < n; i++)
                {
                    gra.DrawString(PointsD[i].Z.ToString("f1"), new Font("Verdana", (int)(pictureBox.Size.Height / x_max / Zoom)), new SolidBrush(Color.Red), q[i]);
                }
            }
            else
            {

            }



            for (int i = 0; i < n; i++)
            {
                if (i % 3 == 0)
                {
                    gra.DrawLine(new Pen(Color.Black), q[i], q[i + 1]);
                }
                else if (i % 3 == 1)
                {
                    gra.DrawLine(new Pen(Color.Black), q[i], q[i + 1]);
                }
                else
                {
                    gra.DrawLine(new Pen(Color.Black), q[i], q[i - 2]);
                }
            }

            //设置虚线
            Pen pen = new Pen(Color.Brown);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen.DashPattern = new float[] { 5, 5 };
            for (int i = 0; i < n1 - 1; i += 2)
            {
                gra.DrawLine(pen, qh[i], qh[i + 1]);
            }

            pictureBox.Image = ge;
        }

        public void Draw()
        {
            GetPic_Point();
            GetPic_Line();
        }

    }
}
