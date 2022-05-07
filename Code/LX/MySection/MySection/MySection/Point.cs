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

        public Point()
        {

        }


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

        public double GetFangweijiao(Point point)
        {
          return   azimuth.GetAzim(X, Y, point.X, point.Y);
        }
        public double GetDistince(Point point)
        {
            return Chazhi.GetDistance(X, Y, point.X, point.Y);
        }
        /// <summary>
        /// 内插点P的高程值计算
        /// </summary>
        /// <param name="point"></param>
        /// <param name="points"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static double GetH(Point point, IEnumerable<Point> points, int n = 5)
        {
            var list = points.Select(t => new { D = point.GetDistince( t), P = t }).ToList();
            var data = list.OrderBy(t => t.D).Take(5).ToList();
            double sumhd = data.Sum(t => t.P.H / t.D);
            double sum1d = data.Sum(t => 1 / t.D);
            double h = sumhd / sum1d;

            return h;
        }
        public double GetH(IEnumerable<Point> points,int n=5)
        {
            return GetH(this, points, n);
        }

        private static Point GetNextPoint(double x,double y,double fangweijiao,double l)
        {
            double x1, y1;
            x1 = x + l * Math.Cos(fangweijiao);
            y1 = y + l * Math.Sin(fangweijiao);
            Point point = new Point()
            {
                X = x1,
                Y = y1
            };
            return point;
        }
        public Point GetNextPoint(double fangweijiao,double l)
        {
            return GetNextPoint(this.X, this.Y, fangweijiao, l);
        }
        public override string ToString()
        {
            return $"{ID}           {X}     {Y}     {H}";
        }
    }
}
