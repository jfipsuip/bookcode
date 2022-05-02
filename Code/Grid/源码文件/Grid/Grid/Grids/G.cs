using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid.Grids
{
    public class G
    {
        public string Name { get; set; }
        /// <summary>
        /// 左下角
        /// </summary>
        public Point PointA { get; set; }
        /// <summary>
        /// 左上角
        /// </summary>
        public Point PointB { get; set; }
        /// <summary>
        /// 右上角
        /// </summary>
        public Point PointC { get; set; }
        /// <summary>
        /// 右下角
        /// </summary>
        public Point PointD { get; set; }
        /// <summary>
        /// 中心点
        /// </summary>
        public Point PointCenter
        {
            get
            {
                Point point;
                point = new Point();

                point.X = (PointD.X + PointA.X) / 2;
                point.Y = (PointA.Y + PointB.Y) / 2;

                return point;
            }
        }
        /// <summary>
        /// 中心点是否在凸包内
        /// </summary>
        public bool? IsIN { get; set; }
        public double H { get; set; }
        public double? V
        {
            get
            {

                double v = GetV(PointA.H.Value, PointB.H.Value, PointC.H.Value, PointD.H.Value, H);
                return v;
            }
        }

        public double GetV(double h1, double h2, double h3, double h4, double h)
        {
            double l = PointD.X - PointA.X;
            double v = ((h1 + h2 + h3 + h4) / 4 - h) * (l * l);
            return v;
        }

        public override string ToString()
        {
            return $"{Name} {IsIN}";
        }
    }
}
