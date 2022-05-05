using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MySection
{
    class Point
    {/// <summary>
     /// 点号,我预备使用linq找K012
     /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// XYH分量
        /// </summary>
        public double X { get; set; }
        public double Y { get; set; }
        public double H { get; set; }

        private List<Point> points = new List<Point>
        {

        };

        public Point(string Id, double x, double y, double h = 0)
        {
            ID = Id;
            X = x;
            Y = y;
            H = h;
        }

        public Point(string line)
        {
            string[] strs = line.Split(',');
            ID = strs[0];
            X = Convert.ToDouble(strs[1]);
            Y = Convert.ToDouble(strs[2]);
            H = Convert.ToDouble(strs[3]);

        }
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






        private List<Point> pointPList = new List<Point>
        {
         

        };



        /// <summary>
        /// 从表格中获取数据       
        /// </summary>
        /// <param name="dataPoint"></param>
        /// <returns></returns>
        private static Point GetPoint(DataGridViewRow dataPoint)
        {
            string Id;
            double x, y, h;
            Point point;

            Id = Convert.ToString(dataPoint.Cells[0].Value);
            x = Convert.ToDouble(dataPoint.Cells[1].Value);
            y = Convert.ToDouble(dataPoint.Cells[2].Value);
            h = Convert.ToDouble(dataPoint.Cells[3].Value);
            point = new Point(Id, x, y, h);

            return point;
        }



        public override string ToString()
        {
            return $"{ID} {X} {Y} {H}";
        }





    }
}
