using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 规则网
{
    class 计算地面坐标
    {
        public struct get_point
        {
            public double X;
            public double Y;
            public double Z;
        }
        public List<get_point> number1 = new List<get_point>();
        public static double[] zuobiao(Tpoint a, Tpoint b,double N1,double N2)
        {
            double X = a.Xs + N1 * a.u;
            double Y = 0.5 * ((a.Ys + N1 * a.v) + (b.Ys + N2 * b.v));
            double Z = a.Zs + (N1 * a.ww);
            double[] number = new double[3];
            number[0] = X;
            number[1] = Y;
            number[2] = Z;
           
            return number;
        }
       
    }
}
