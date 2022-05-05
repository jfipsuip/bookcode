﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MySection
{
    class ZongDuanMian
    {
        /// <summary>
        /// 找出K012，计算中点m1，m2
        /// </summary>
        /// <param name="Kq"></param>
        /// <param name="Kz"></param>
        /// <returns></returns>
        private static double GetMiddle(double Kq, double Kz)
        {
            double Middle;
            Middle = (Kq + Kz) / 2;
            return Middle;
        }

        /// <summary>
        /// 计算两个纵断面，KOK1，K1K2的长度
        /// </summary>
        /// <param name="Kxi"></param>
        /// <param name="Kxj"></param>
        /// <param name="Kyi"></param>
        /// <param name="Kyj"></param>
        /// <returns></returns>
        private static double GetDistance(double Kxi, double Kxj, double Kyi, double Kyj)
        {
            double distance;
            distance = Math.Sqrt((Kxj - Kxi) * (Kxj - Kxi) + (Kyj - Kyi) * (Kyj - Kyi));
            return distance;
        }
 

     


        private List<ZongDuanMian> pointChaZhi = new List<ZongDuanMian>
        {


        };






    }
}
