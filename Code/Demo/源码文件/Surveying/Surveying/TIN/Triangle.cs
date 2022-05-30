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
        /// 平均高程
        /// </summary>
        public double? H
        {
            get
            {
                double? result;
                result = (PointA.H + PointB.H + PointC.H) / 3 - Hc;
                return result;
            }
        }
        public double Hp
        {
            get
            {
                return (PointA.H + PointB.H + PointC.H) / 3;
            }
        }
        /// <summary>
        /// 参考高程
        /// </summary>
        public double? Hc { get; set; }
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
                return GetR(X, Y, PointA.X, PointA.Y);
            }
        }
        /// <summary>
        /// 三角形投影底面积
        /// </summary>
        public double S
        {
            get
            {
                return GetS(PointA.X, PointA.Y, PointB.X, PointB.Y, PointC.X, PointC.Y);
            }
        }
        /// <summary>
        /// 斜三棱柱的体积
        /// </summary>
        public double V
        {
            get
            {
                return Vcut + Vfill;
            }
        }
        public double Vcut { get; set; }
        public double Vfill { get; set; }
        public ETriangle Type { get; set; }
        public string Type2
        {
            get
            {
                string type;
                switch (Type)
                {
                    case ETriangle.全挖方:
                        type = "全挖方";
                        break;
                    case ETriangle.全填方:
                        type = "全填方";
                        break;
                    case ETriangle.两个顶点低:
                        type = "既有挖方又有填方";
                        break;
                    case ETriangle.一个顶点低:
                        type = "既有挖方又有填方";
                        break;
                    default:
                        type = "";
                        break;
                }
                return type;
            }
        }
        public void CalculateV()
        {
            ETriangle etriangle;
            double h1, h2, h3, h0, vcut, vfill, s;
            double x1, y1, x2, y2;
            Point[] points = { PointA, PointB, PointC };
            Point pointA, pointB, pointC;
            int count = points.Where(t => t.H < Hc.Value).Count();

            if (count == 0)
            {
                vcut = S * H ?? 0;
                vfill = 0;
                etriangle = ETriangle.全挖方;
            }
            else if (count == 2)
            {
                points = points.OrderByDescending(t => t.H).ToArray();
                pointA = points[0];
                pointB = points[1];
                pointC = points[2];
                h1 = pointA.H;
                h2 = pointB.H;
                h3 = pointC.H;
                h0 = Hc ?? 0;

                x1 = GetValue(pointA.X, pointA.H, pointB.X, pointB.H, h0);
                y1 = GetValue(pointA.Y, pointA.H, pointB.Y, pointB.H, h0);
                x2 = GetValue(pointA.X, pointA.H, pointC.X, pointC.H, h0);
                y2 = GetValue(pointA.Y, pointA.H, pointC.Y, pointC.H, h0);
                s = GetS(x1, y1, x2, y2, pointA.X, pointA.Y);
                vcut = s * ((h1 + h0 + h0) / 3 - h0);
                vfill = (S - s) * ((h0 + h0 + h2 + h3) / 4 - h0);
                etriangle = ETriangle.两个顶点低;

            }
            else if (count == 1)
            {
                points = points.OrderBy(t => t.H).ToArray();
                pointA = points[0];
                pointB = points[1];
                pointC = points[2];
                h1 = pointA.H;
                h2 = pointB.H;
                h3 = pointC.H;
                h0 = Hc ?? 0;
                x1 = GetValue(pointA.X, pointA.H, pointB.X, pointB.H, h0);
                y1 = GetValue(pointA.Y, pointA.H, pointB.Y, pointB.H, h0);
                x2 = GetValue(pointA.X, pointA.H, pointC.X, pointC.H, h0);
                y2 = GetValue(pointA.Y, pointA.H, pointC.Y, pointC.H, h0);
                s = GetS(x1, y1, x2, y2, pointA.X, pointA.Y);

                vfill = s * ((h1 + h0 + h0) / 3 - h0);
                vcut = (S - s) * ((h0 + h0 + h2 + h3) / 4 - h0);
                etriangle = ETriangle.一个顶点低;

            }
            else
            {
                vcut = 0;
                vfill = S * H ?? 0;
                etriangle = ETriangle.全填方;
            }
            Vcut = vcut;
            Vfill = vfill;
            Type = etriangle;

        }
        private double GetValue(double x1, double h1, double x2, double h2, double h0)
        {
            double x = x1 + Math.Abs((h0 - h1) / (h2 - h1)) * (x2 - x1);
            return x;
        }

        private double GetV(double hc, double h1, double h2, double h3)
        {
            throw new NotImplementedException();
        }

        private double GetS(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            double s;

            s = Math.Abs((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1)) / 2;

            return s;
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

            d = Point.GetDistance(X, Y, point.X, point.Y);
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
        public double Area
        {
            get
            {
                double area;
                area = Math.Abs((1.0 / 2) * (PointA.X * (PointB.Y - PointC.Y) + PointB.X * (PointC.Y - PointA.Y) + PointC.X * (PointA.Y - PointB.Y)));

                return area;
            }
        }

        public override string ToString()
        {
            return $"{PointA.Name} {PointB.Name} {PointC.Name}";
        }
    }
}
