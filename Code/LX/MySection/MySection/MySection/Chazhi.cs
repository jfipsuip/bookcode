using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySection
{
    class Chazhi
    {       //public static List<Point> ToGetKs(DataGridView dataGridView)
        //{
        //    List<List<string>>list = new List<List<string>>();
        //    for (int i = 0; i < dataGridView.Rows.Count; i++)
        //    {
        //        List<string> data = new List<string>();
        //        for (int j = 0; j < dataGridView.Rows.Count; j++)
        //        {
        //            data.Add(dataGridView[j, i].Value.ToString());
        //        }
        //        list.Add(data);
        //    }
        //    return ;
        //}
        public Point pointNieChaP { get; set; }


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
        /// 计算直线距离，可以用来计算两个纵断面，KOK1，K1K2的长度
        /// </summary>
        /// <param name="Kxi"></param>
        /// <param name="Kxj"></param>
        /// <param name="Kyi"></param>
        /// <param name="Kyj"></param>
        /// <returns></returns>
        public  static double GetDistance(double Kxi,double Kyi, double Kxj,  double Kyj)
        {
            double distance;
            distance = Math.Sqrt((Kxj - Kxi) * (Kxj - Kxi) + (Kyj - Kyi) * (Kyj - Kyi));
            return distance;
        }


        public Point ZongDuanMianChaZhi
        {
            get
            {
                Point pointCha;
                pointCha = new Point();
                double l = 5;
                double Cazi = azimuth.Cazi();
                pointNieChaP.X = pointCha.X+l* Cazi;
                pointNieChaP.Y = pointCha.Y;
                ///两内插点之间距离应为5
                return pointCha;

            }


        }
    }
}
