using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid.Grids
{
    public class Grid
    {
        double xMin, xMax, yMin, yMax;
        Point p1, p2, p3, p4;
        /// <summary>
        /// 初始点集
        /// </summary>
        public List<Point> Points { get; }
        /// <summary>
        /// 基准高程
        /// </summary>
        public double H { get; set; }

        public List<Point> M { get; set; }
        /// <summary>
        /// 凸包点集
        /// </summary>
        public List<Point> CH { get; set; } = new List<Point>();
        public Grid(List<Point> points, double h)
        {
            Points = points;
            H = h;
        }
        public void Calculate()
        {
            InitialPoint();

            CH.Add(p1);
            int i = 1;
            CalculatePoint(p1, p2, M);
            CalculatePoint(p2, p3, M);
            CalculatePoint(p3, p4, M);
            CalculatePoint(p4, p1, M);
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
    }
}
