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
        public PictureBox pictureBox1;

        internal PointF[] Points;    //输入的点集

        public List<PointF> PointLines;
        internal List<PointF> contourLine = new List<PointF>(); //等高线

        bool rdbcheck = false;
        /// <summary>
        /// 画图相关
        /// </summary>
        public double zoom = 3.00;
        public PointF[] p;
        double x_average = 0;
        double y_average = 0;
        double x_max = 0;
        double y_max = 0;
        public PointF[] ph;
        public Point[] q;
        public Point[] qh;
        public Point[] go = { new Point(0, 0), new Point(0, 0) };
        public bool Clicked = false;


        public void GetPic_Point(double zoom, Point go)
        {

            int n = Points.Length;
            p = new PointF[n];
            for (int i = 0; i < n; i++)
            {
                p[i].X = Points[i].X;
                p[i].Y = Points[i].Y;
            }

            for (int i = 0; i < n; i++)
            {
                x_average += p[i].X;
                y_average += p[i].Y;
            }
            x_average /= n;
            y_average /= n;
            for (int i = 0; i < n; i++)
            {
                p[i].X -= x_average;
                p[i].Y -= y_average;
                if (Math.Abs(p[i].X) > x_max)
                {
                    x_max = Math.Abs(p[i].X);
                }
                if (Math.Abs(p[i].Y) > y_max)
                {
                    y_max = Math.Abs(p[i].Y);
                }
            }
            double pic_height = pictureBox1.Size.Height;
            //投影到图像坐标系
            q = new Point[n];
            for (int i = 0; i < n; i++)
            {
                q[i].X = (int)(pic_height / 2 + go.X + p[i].X * pic_height / x_max / zoom);
                q[i].Y = (int)(pic_height / 2 - go.Y - p[i].Y * pic_height / y_max / zoom);
            }
            //画点
            Bitmap map = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            Image ge = map;
            Graphics gra = Graphics.FromImage(ge);

            Bitmap oo = new Bitmap(3, 3);
            Image o = oo;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    oo.SetPixel(i, j, Color.Red);

            for (int i = 0; i < n; i++)
            {
                gra.DrawImage(o, q[i]);
                // gra.DrawString(tpoints[i].Name, new Font("宋体", 8), new SolidBrush(Color.Black), q[i]);
            }
            pictureBox1.Image = ge;
        }
        private void GetPic_Line(double zoom, Point go)
        {
            int n1 = contourLine.Count;
            int n = PointLines.Count;
            p = new PointF[n];
            ph = new PointF[n1];
            //
            for (int i = 0; i < n; i++)
            {
                p[i].X = PointLines[i].X;
                p[i].Y = PointLines[i].Y;
                p[i].Z = PointLines[i].H;
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
                p[i].X -= x_average;
                p[i].Y -= y_average;
            }

            for (int i = 0; i < n1; i++)
            {
                ph[i].X -= x_average;
                ph[i].Y -= y_average;
            }

            double pic_width = pictureBox1.Size.Width;
            double pic_height = pictureBox1.Size.Height;
            //
            for (int i = 0; i < n; i++)
            {
                q[i].X = (int)(pic_height / 2 + go.X + p[i].X * pic_height / x_max / zoom);
                q[i].Y = (int)(pic_height / 2 - go.Y - p[i].Y * pic_height / y_max / zoom);
            }

            for (int i = 0; i < n1; i++)
            {
                qh[i].X = (int)(pic_height / 2 + go.X + ph[i].X * pic_height / zoom / x_max);
                qh[i].Y = (int)(pic_height / 2 - go.Y - ph[i].Y * pic_height / zoom / y_max);
            }
            //
            Bitmap bitmap = new Bitmap(3, 3);
            Image image = bitmap;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    bitmap.SetPixel(i, j, Color.Red);

            Bitmap map = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            Image ge = map;
            Graphics gra = Graphics.FromImage(ge);

            for (int i = 0; i < n; i++)
            {
                gra.DrawImage(image, q[i]);
            }

            if (true)
            {
                Point[] q2 = new Point[q.Length];  //三角形点的偏移坐标
                q.CopyTo(q2, 0);
                for (int i = 0; i < n; i++)
                {
                    q2[i].X += (int)(10 / zoom);
                    q2[i].Y += (int)(10 / zoom);
                }
                for (int i = 0; i < n; i++)
                {
                    gra.DrawString(p[i].Z.ToString("f1"), new Font("Verdana", (int)(pictureBox1.Size.Height / x_max / zoom)), new SolidBrush(Color.Red), q[i]);
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

            pictureBox1.Image = ge;
        }

        public void Draw()
        {
            GetPic_Point(zoom, go[1]);
            GetPic_Line(zoom, go[1]);
        }

    }
}
