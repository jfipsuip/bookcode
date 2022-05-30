using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PointC = Surveying.Commons.Point;

namespace Surveying.Draws
{
    public class DrawHelperBase
    {
        static double rate = 0.9;
        int xImage, yImage;
        double xMin, xMax, yMin, yMax, width, height, xCenter, yCenter, zoom;

        public PictureBox PictureBox { get; set; }
        protected Graphics Graphics { get; set; }
        /// <summary>
        /// 图像偏移坐标
        /// </summary>
        public Point Move { get; set; }
        /// <summary>
        /// 图像放大变小比率
        /// </summary>
        public double Rate { get; set; } = rate;
        /// <summary>
        /// 图像要画的点
        /// </summary>
        public List<PointC> Points { get; set; }
        public DrawHelperBase(PictureBox pictureBox)
        {
            PictureBox = pictureBox;
            AddEvent();
        }
        public void Initialize()
        {
            Bitmap image = new Bitmap(PictureBox.Size.Width, PictureBox.Size.Height);
            PictureBox.Image = image;
            Graphics = Graphics.FromImage(image);
            Graphics.Clear(Color.White);

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
            return ConvertPoint(point, xCenter, yCenter, zoom * Rate, xImage + Move.X, yImage + Move.Y);
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
        public void Reset()
        {
            // 设置初始参数
            Rate = rate;
            Move = new Point();
            Draw();
        }
        /// <summary>
        /// 图表放大
        /// </summary>
        public void Magnify()
        {
            Rate = Rate * 1.1;
            Draw();
        }
        /// <summary>
        /// 图表变小
        /// </summary>
        public void Minish()
        {
            Rate = Rate * 0.9;
            Draw();
        }
        public void MoveImage(Point move)
        {
            Move = move;
            Draw();
        }
        protected virtual void DrawImage()
        {
            int n = 5, m = 2 * n;
            Points?.ForEach(p =>
            {
                var point = ConvertPoint(p);
                Graphics.DrawEllipse(Pens.Red, point.X - n, point.Y - n, m, m);
            });
        }
        #region

        //判断是否进行图形的移动
        bool isMove = false;
        Point position, move, location;


        public void AddEvent()
        {
            PictureBox.DoubleClick += PictureBox_DoubleClick;
            PictureBox.MouseWheel += PictureBox_MouseWheel;
            PictureBox.MouseDown += PictureBox_MouseDown;
            PictureBox.MouseUp += PictureBox_MouseUp;
            PictureBox.MouseMove += PictureBox_MouseMove;
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            position = Cursor.Position;
            isMove = true;
        }
        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            location = move;
            isMove = false;
        }
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove)
            {
                //X、Y方向上的移动距离
                Point go = new Point
                {
                    X = Cursor.Position.X - position.X,
                    Y = Cursor.Position.Y - position.Y,
                };
                //图形移动后的位置
                move.X = location.X + go.X;
                move.Y = location.Y + go.Y;

                MoveImage(move);
            }
        }
        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Magnify();
            }
            else if (e.Delta < 0)
            {
                Minish();
            }
        }

        private void PictureBox_DoubleClick(object sender, EventArgs e)
        {
            Reset();
        }
        #endregion
    }
}
