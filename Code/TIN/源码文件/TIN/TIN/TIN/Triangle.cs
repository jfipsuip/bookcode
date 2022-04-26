using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN.TIN
{
    /// <summary>
    /// 三角形
    /// </summary>
    public class Triangle
    {
        /// <summary>
        /// 三角形的第一个点A
        /// </summary>
        public Point PointA { get; set; }
        /// <summary>
        /// 三角形的第二个点B
        /// </summary>
        public Point PointB { get; set; }
        /// <summary>
        /// 三角形的第一个点C
        /// </summary>
        public Point PointC { get; set; }

        /// <summary>
        /// 三角形外接圆圆心 Xo 坐标
        /// </summary>
        public double X
        {
            get
            {
                return GetX(PointA.X, PointB.X, PointC.X, PointA.Y, PointB.Y, PointC.Y);
            }
        }
        /// <summary>
        /// 三角形外接圆圆心 Yo 坐标
        /// </summary>
        public double Y
        {
            get
            {
                return GetY(PointA.X, PointB.X, PointC.X, PointA.Y, PointB.Y, PointC.Y);
            }
        }
        /// <summary>
        /// 三角形外接圆半径 r
        /// </summary>
        public double R
        {
            get
            {
                return GetR(X, Y, PointA.X, PointB.Y);
            }
        }



        public Triangle(Point pointA, Point pointB, Point pointC)
        {
            PointA = pointA;
            PointB = pointB;
            PointC = pointC;
        }
        private double GetX(double x1, double x2, double x3, double y1, double y2, double y3)
        {
            double x = ((y2 - y1) * (y3 * y3 - y1 * y1 + x3 * x3 - x1 * x1) - (y3 - y1) * (y2 * y2 - y1 * y1 + x2 * x2 - x1 * x1)) / (2 * (x3 - x1) * (y2 - y1) - 2 * (x2 - x1) * (y3 - y1));
            return x;
        }
        private double GetY(double x1, double x2, double x3, double y1, double y2, double y3)
        {
            double y = ((x2 - x1) * (x3 * x3 - x1 * x1 + y3 * y3 - y1 * y1) - (x3 - x1) * (x2 * x2 - x1 * x1 + y2 * y2 - y1 * y1)) / (2 * (y3 - y1) * (x2 - x1) - 2 * (y2 - y1) * (x3 - x1));
            return y;
        }
        private double GetR(double x, double y, double x1, double y1)
        {
            double r;
            r = Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1));
            return r;
        }

        /// <summary>
        /// 判断点是否在三角形外接圆内部
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsInCircle(Point point)
        {
            double d;
            bool isIn = false;

            d = PointA.GetDistance(point);
            if (R >= d)
            {
                isIn = true;
            }

            return isIn;
        }

        private bool IsContain(Point point)
        {
            bool isContain = false;

            if (PointA == point || PointB == point || PointC == point)
            {
                isContain = true;
            }

            return isContain;
        }
        public bool IsContain(params Point[] points)
        {
            foreach (var point in points)
            {
                if (IsContain(point))
                {
                    return true;
                }
            }
            return false;
        }
        public override string ToString()
        {
            return $"{PointA.Name} {PointB.Name} {PointC.Name}";
        }
    }
}
