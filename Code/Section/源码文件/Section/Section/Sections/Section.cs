using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section.Sections
{
    class Section
    {
        /// <summary>
        /// 内插点
        /// </summary>
        static int N = 1;
        // n 内插点数
        double n, fangweijiao;
        public Point Ka { get; set; }
        public Point Kb { get; set; }
        public Point M { get; set; }
        public double L { get; set; }
        List<Point> Points { get; set; }

        public void Calculate()
        {
            fangweijiao = Common.GetFangweijiao(Ka, Kb);
            double d = Common.Distance(Ka, Kb);
            n = Math.Floor(d / L);
            Points = new List<Point>();
            Points.Add(Ka);
            for (int i = 0; i < n; i++)
            {
                Points.Add(GetPoint(Ka, fangweijiao, L * i));
            }
            Points.Add(Kb);
        }

        public static Point GetPoint(Point point, double fangweijiao, double l)
        {
            double x, y;
            x = point.X + l * Math.Cos(fangweijiao);
            y = point.Y + l * Math.Sin(fangweijiao);
            Point p = new Point("V" + N++, x, y);
            return p;
        }
        /// <summary>
        /// 获取中心点位置
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <returns></returns>
        public static Point GetCenterPoint(Point pointA, Point pointB)
        {
            double X = (pointA.X + pointB.X) / 2;
            double Y = (pointA.Y + pointB.Y) / 2;
            return new Point() { X = X, Y = Y };
        }
    }
}
