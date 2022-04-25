using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN.TIN
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

        public Point(string line)
        {
            string[] strs = line.Split(',');
            Name = strs[0];
            X = Convert.ToDouble(strs[1]);
            Y = Convert.ToDouble(strs[2]);
            H = Convert.ToDouble(strs[3]);
        }
    }
}
