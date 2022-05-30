using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveying.Commons
{
    /// <summary>
    /// 点
    /// </summary>
    public class Point
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// X坐标
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// H坐标
        /// </summary>
        public double H { get; set; }

        public Point(string name, double x, double y, double h = 0)
        {
            Name = name;
            X = x;
            Y = y;
            H = h;
        }

        public override string ToString()
        {
            return $"{Name} {X} {Y} {H}";
        }
    }

}
