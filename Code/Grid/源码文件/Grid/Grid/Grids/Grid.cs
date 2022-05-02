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
        public List<Point> Points { get; }
        /// <summary>
        /// 基准高程
        /// </summary>
        public double H { get; set; }
        public Grid(List<Point> points, double h)
        {
            Points = points;
            H = h;
        }
        public void Calculate()
        {
            InitialPoint();
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
        }



    }
}
