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
    }
}
