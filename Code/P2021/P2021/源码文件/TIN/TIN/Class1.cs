using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN
{
    class Class1
    {
        public struct Point//起算数据的点号、x、y、h
        {
            public string id;
            public double x;
            public double y;
            public double h;
        }
        public struct Tri//三角三个点
        {
            public int A;
            public int B;
            public int C;
        }
        public class Edge
        {

            public int strat;//边起点
            public int end;//边终点
        }

        public static double juli(double x1, double x2, double y1, double y2)
        {
            double deta_x = x2 - x1;
            double deta_y = y2 - y1;
            double L = Math.Sqrt(deta_x * deta_x + deta_y * deta_y);
            return L;
        }
    }
}
