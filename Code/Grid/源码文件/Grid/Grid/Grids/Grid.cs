using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid.Grids
{
    public class Grid
    {
        int m, n;
        double xMin, xMax, yMin, yMax, h, w, r;
        Point p1, p2, p3, p4;
        Point pointMin
        {
            get
            {
                return new Point()
                {
                    X = xMin,
                    Y = yMin,
                };
            }
        }
        /// <summary>
        /// P1 到 P4 组成的 点集
        /// </summary>
        List<Point> PointsP;
        /// <summary>
        /// 初始点集
        /// </summary>
        public List<Point> Points { get; }
        /// <summary>
        /// 基准高程
        /// </summary>
        public double H { get; set; }
        public double L { get; set; } = 5;
        public List<Point> M { get; set; }
        /// <summary>
        /// 凸包点集
        /// </summary>
        public List<Point> CH { get; set; } = new List<Point>();

        public G[,] Gs { get; set; }
        public double V { get; set; }
        public Grid(List<Point> points, double h)
        {
            Points = points;
            H = h;
        }
        public void Calculate(double l)
        {
            L = l;
            CH = new List<Point>();

            InitialPoint();

            CalculatePoint();

            CalculateGrid();

            V = CalculateV();
        }

        private double CalculateV()
        {
            double v = 0;
            foreach (var g in Gs)
            {
                if (g.IsIN == true)
                {
                    v += g.V.Value;
                }
            }
            return v;
        }

        private void CalculateGrid()
        {
            w = xMax - xMin;
            h = yMax - yMin;
            r = (h + w) / 2 * 0.4;
            n = (int)Math.Ceiling(w / L);
            m = (int)Math.Ceiling(h / L);
            Point[,] points = new Point[n + 1, m + 1];
            for (int i = 0; i < points.GetLength(0); i++)
            {
                for (int j = 0; j < points.GetLength(1); j++)
                {
                    points[i, j] = new Point()
                    {
                        X = xMin + i * L,
                        Y = yMin + j * L,
                    };
                }
            }

            Gs = new G[n, m];
            for (int i = 0; i < Gs.GetLength(0); i++)
            {
                for (int j = 0; j < Gs.GetLength(1); j++)
                {
                    Gs[i, j] = new G();
                    Gs[i, j].Name = $"{i + 1} {j + 1}";
                    Gs[i, j].PointA = points[i, j];
                    Gs[i, j].PointB = points[i, j + 1];
                    Gs[i, j].PointC = points[i + 1, j + 1];
                    Gs[i, j].PointD = points[i + 1, j];
                }
            }

            foreach (var g in Gs)
            {
                g.IsIN = IsIN(CH, g.PointCenter);
                if (g.IsIN == true)
                {
                    GetH(g.PointA);
                    GetH(g.PointB);
                    GetH(g.PointC);
                    GetH(g.PointD);
                    g.H = H;
                }
            }
        }

        private void GetH(Point point)
        {
            if (!point.H.HasValue)
            {
                point.H = GetH(point, Points, r);
            }
        }
        private static double GetH(Point point, List<Point> points, double r)
        {
            double h = 0, hSum = 0, dSum = 0;

            var list = points.Select(t => new { D = Distance(point, t), P = t }).ToList();
            var q = list.Where(t => t.D <= r).Select(t => new { t.P.H, t.D }).ToList();
            foreach (var item in q)
            {
                hSum += item.H.Value / item.D;
                dSum += 1 / item.D;
            }
            h = hSum / dSum;
            return h;
        }
        private static double Distance(Point pointA, Point pointB)
        {
            double d, x1, y1, x2, y2;

            x1 = pointA.X;
            y1 = pointA.Y;
            x2 = pointB.X;
            y2 = pointB.Y;
            d = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

            return d;
        }

        private static bool IsIN(List<Point> points, Point point)
        {
            bool isIn = false;
            int sum = 0;
            for (int i = 0; i < points.Count; i++)
            {
                Point pointA, pointB;

                pointA = points[i];
                if (i == points.Count - 1)
                {
                    pointB = points[0];
                }
                else
                {
                    pointB = points[i + 1];
                }
                int n = GetX(pointA.X, pointA.Y, pointB.X, pointB.Y, point.X, point.Y);
                sum += n;
            }

            if (sum % 2 == 1)
            {
                isIn = true;
            }

            return isIn;
        }
        private static int GetX(double xi, double yi, double xj, double yj, double x, double y)
        {
            int result = 0;

            if ((yi > y && yj < y) || (yi < y && yj > y))
            {
                double x2 = ((xj - xi) / (yj - yi)) * (y - yi) + xi;
                if (x2 > x)
                {
                    result = 1;
                }
            }

            return result;
        }

        private void CalculatePoint()
        {
            CH.Add(p1);

            for (int i = 0; i < PointsP.Count() - 1; i++)
            {
                Point pointA = PointsP[i];
                Point pointB = PointsP[i + 1];
                CalculatePoint(pointA, pointB, M);
            }
        }

        /// <summary>
        /// 获取凸包点
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="points"></param>
        private void CalculatePoint(Point pointA, Point pointB, List<Point> points)
        {
            //获取点A到点B左边的点集
            List<Point> leftPoints = GetLeftPoints(pointA, pointB, points);

            // 如果左边点集为空，刚点B为凸包点
            if (leftPoints.Count == 0)
            {
                CH.Add(pointB);
                M.Remove(pointB);
            }
            // 如果左边点集不为空，则获取最远点P进行递归运算，直到左边点集为空
            else
            {

                Point point = GetFarPoint(pointA, pointB, leftPoints);
                leftPoints.Remove(point);
                // 计算点A 到 点P 凸包点
                CalculatePoint(pointA, point, leftPoints);
                // 计算点P 到 点B 凸包点
                CalculatePoint(point, pointB, leftPoints);
            }
        }
        /// <summary>
        /// 获取离点A-B最远的点
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="leftPoints"></param>
        /// <returns></returns>
        private static Point GetFarPoint(Point pointA, Point pointB, List<Point> leftPoints)
        {
            Point point;
            List<Triangle> triangles;
            Triangle triangle;

            triangles = leftPoints.Select(t => new Triangle(pointA, pointB, t)).ToList();
            triangle = triangles.OrderByDescending(t => t.Area).First();
            point = triangle.PointC;

            return point;
        }

        /// <summary>
        /// 获取点A到点B左边的点集
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private static List<Point> GetLeftPoints(Point pointA, Point pointB, List<Point> points)
        {
            List<Point> leftPoints = new List<Point>();
            foreach (var point in points)
            {
                if (IsL(pointA, pointB, point))
                {
                    leftPoints.Add(point);
                }
            }

            return leftPoints;
        }

        private void InitialPoint()
        {
            xMin = Points.Min(t => t.X);
            xMax = Points.Max(t => t.X);
            yMin = Points.Min(t => t.Y);
            yMax = Points.Max(t => t.Y);

            p1 = Points.OrderBy(t => t.X).FirstOrDefault();
            p2 = Points.OrderByDescending(t => t.Y).FirstOrDefault();
            p3 = Points.OrderByDescending(t => t.X).FirstOrDefault();
            p4 = Points.OrderBy(t => t.Y).FirstOrDefault();

            M = Points.ToList();
            M.Remove(p1);
            M.Remove(p2);
            M.Remove(p3);
            M.Remove(p4);

            PointsP = new List<Point>();
            PointsP.Add(p1);
            PointsP.Add(p2);
            PointsP.Add(p3);
            PointsP.Add(p4);
            PointsP.Add(p1);
        }

        /// <summary>
        /// 判断点C是否在线A-B的左边
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="pointC"></param>
        /// <returns></returns>
        private static bool IsL(Point pointA, Point pointB, Point pointC)      //判断左转还是右转
        {
            bool isL = false;
            double d;

            d = pointA.X * pointB.Y - pointB.X * pointA.Y + pointC.X * (pointA.Y - pointB.Y) + pointC.Y * (pointB.X - pointA.X);
            if (d > 0)
            {
                isL = true;
            }

            return isL;
        }

        public Draws.DrawHelper GetDarw(System.Windows.Forms.PictureBox pictureBox)
        {
            Draws.DrawHelper helper = new Draws.DrawHelper(pictureBox);

            List<Point> pointLines = new List<Point>();
            for (int i = 1; i < CH.Count; i++)
            {
                pointLines.Add(CH[i - 1]);
                pointLines.Add(CH[i]);
            }
            helper.Points = Points;
            helper.PointLines = pointLines;

            return helper;
        }
    }
}
