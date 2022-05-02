using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid.Grids
{
    public class Grid
    {
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
    }
}
