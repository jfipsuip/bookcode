using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySection
{
    class azimuth
    {
        /// <summary>
        /// 计算方位角
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double GetAzim(double x1, double y1, double x2, double y2)
        {
            double Azim;
            Azim = Math.Atan((y2 - y1) / (x2 - x1));
            return Azim;
        }

        //判断值域

        private List<Point> pointPList = new List<Point>
        {


        };


    }
}
