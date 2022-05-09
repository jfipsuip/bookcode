using System;
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

        private List<ZongDuanMian> pointChaZhi = new List<ZongDuanMian>
        {


        };

        internal static void JiSuan()
        {
            string path = @"D:\github\jfipsuip\bookcode\Code\LX\MySection\测试数据\data.txt";
            string[] lines = File.ReadAllLines(path);

            string[] keys = lines[1].Split(',');


            string[] pointLines = lines.Skip(5).ToArray();

            List<Point> points = pointLines.Select(t => new Point(t)).ToList();

            Point k0, k1, k2 = null;

            k0 = points.Where(t => t.ID == keys[0]).First();
            k1 = points.Where(t => t.ID == keys[1]).First();
            string k2Name = keys[2];
            foreach (Point point in points)
            {
                if (point.ID == k2Name)
                {
                    k2 = point;
                }
            }
            List<Point> pointsNC = new List<Point>();
            double fangweijiao = k0.GetFangweijiao(k1);
            double dk0k1 = k0.GetDistince(k1);
            pointsNC.Add(k0);
            double l = 10;
            for (int i = 1; i * l < dk0k1; i++)
            {
                Point v = k0.GetNextPoint(fangweijiao, i * l);

                v.H = v.GetH(points);
                pointsNC.Add(v);

            }

        }
    }
}
