using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section.Sections
{
    class Common
    {
        /// <summary>
        /// 坐标方位角的计算
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <returns></returns>
        public static double GetFangweijiao(Point pointA, Point pointB)
        {
            double result, y, x;
            y = pointB.Y - pointA.Y;
            x = pointB.X - pointA.X;
            if (x == 0)
            {
                if (y > 0)
                {
                    result = Math.PI / 2;
                }
                else
                {
                    result = Math.PI / 2 * 3;
                }
            }
            result = Math.Atan(y / x);
            if (y > 0 && x < 0)
            {
                result = Math.PI - result;
            }
            else if (y < 0 && x < 0)
            {
                result = Math.PI + result;
            }
            else if (y < 0 && x > 0)
            {
                result = Math.PI * 2 - result;
            }

            return result;
        }
        /// <summary>
        /// 计算两个点距离
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <returns></returns>
        public static double Distance(Point pointA, Point pointB)
        {
            return Distance(pointA.X, pointA.Y, pointB.X, pointB.Y);
        }
        /// <summary>
        /// 计算距离
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        private static double Distance(double x1, double y1, double x2, double y2)
        {
            double d;

            d = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

            return d;
        }
        /// <summary>
        /// 内插点P的高程值计算
        /// </summary>
        /// <param name="point"></param>
        /// <param name="points"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double GetH(Point point, IEnumerable<Point> points, int n = 5)
        {
            var list = points.Select(t => new { D = Distance(point, t), P = t }).ToList();
            var data = list.OrderBy(t => t.D).Take(5).ToList();
            double sumhd = data.Sum(t => t.P.H / t.D);
            double sum1d = data.Sum(t => 1 / t.D);
            double h = sumhd / sum1d;

            return h;
        }
        /// <summary>
        /// 计算梯形面积
        /// </summary>
        /// <param name="h1"></param>
        /// <param name="h2"></param>
        /// <param name="h"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        public static double GetS(double h1, double h2, double h, double l)
        {
            double s = ((h1 + h2) / 2 - h) * l;
            return s;
        }
    }
}
