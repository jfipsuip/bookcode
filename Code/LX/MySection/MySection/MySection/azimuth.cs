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
            double y21 = y2 - y1;
            double x21 = x2 - x1;    
            Azim = Math.Atan(y21 / x21);
            if (y21 > 0 && x21 > 0)
            {
                Azim = 1*Azim;
            }
            else if(y21 > 0 && x21 <0)
            {
                Azim = Math.PI-Azim;
            }
            else if (y21 < 0 && x21 < 0)
            {
                Azim = Math.PI + Azim;
            }
            else if (y21 < 0 && x21 >0)
            {
                Azim = 2 * Math.PI - Azim;
            }
            else if (y21 < 0 && x21 ==0)
            {
                Azim = 1.5*Math.PI ;
            }
            else if (y21 > 0 && x21 == 0)
            {
                Azim = 0.5 * Math.PI;
            }

            return Azim;
        }
        

        public static double Cazi()
        {
            double Cazim=0;
            double Sazim;
            //double y21 = y2 - y1;
            //double x21 = x2 - x1;
            //Cazim = Math.Cos(y21 / x21);
            //Sazim= Math.Sin(y21 / x21);
            return Cazim;
        }
        

    }
}
