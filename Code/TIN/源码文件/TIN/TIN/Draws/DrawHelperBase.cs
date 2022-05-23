using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PointC = TIN.TIN.Point;

namespace TIN.Draws
{
    class DrawHelperBase
    {
        int xImage, yImage;
        double xMin, xMax, yMin, yMax, width, height, xCenter, yCenter, zoom;

        public PictureBox PictureBox { get; set; }
        protected Graphics Graphics { get; set; }
        public List<PointC> Points { get; set; }
        public DrawHelperBase(PictureBox pictureBox)
        {
            PictureBox = pictureBox;
            Bitmap image = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height);
            pictureBox.Image = image;
            Graphics = Graphics.FromImage(image);
        }
        public void Initialize()
        {
            // 图形矩形的中心
            xImage = PictureBox.Width / 2;
            yImage = PictureBox.Height / 2;

            // 坐标矩形的XY
            xMin = Points.Min(t => t.X);
            xMax = Points.Max(t => t.X);
            yMin = Points.Min(t => t.Y);
            yMax = Points.Max(t => t.Y);
            // 坐标矩形的宽和高
            width = xMax - xMin;
            height = yMax - yMin;
            // 坐标矩形的中心
            xCenter = (xMin + xMax) / 2;
            yCenter = (yMin + yMax) / 2;

            // 比率尺为宽或高中的最小比率尺
            double zoomWidth = PictureBox.Width / width;
            double zoomHeight = PictureBox.Height / height;
            zoom = Math.Min(zoomWidth, zoomHeight);
        }

        public Point ConvertPoint(PointC point)
        {
            return ConvertPoint(point, xCenter, yCenter, zoom, xImage, yImage);
        }
        private static Point ConvertPoint(PointC point, double x, double y, double zoom, int xMove, int yMove)
        {
            Point p = new Point();
            p.X = (int)((point.X - x) * zoom + xMove);
            // width - y - 0.5width == -y + 0.5 move
            p.Y = (int)(-(point.Y - y) * zoom + yMove);

            return p;
        }

        public void Draw()
        {
            Initialize();
            DrawImage();
        }

        private void DrawImage()
        {
            int n = 5, m = 2 * n;
            Points?.ForEach(p =>
            {
                var point = ConvertPoint(p);
                Graphics.DrawEllipse(Pens.Red, point.X - n, point.Y - n, m, m);
            });
        }
    }
}
