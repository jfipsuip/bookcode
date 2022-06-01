using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 规则网
{
    class 获得投影系数
    {
       
        
        public static double[] Touying(Tpoint a,Tpoint b)
        {
            double Bv = b.Xs - a.Xs;
            double By = b.Ys - a.Ys;
            double Bw = b.Zs - a.Zs;

            double N1 = (Bv * b.ww - Bw * b.u) / ( a.u * b.ww - b.u * a.ww);
            double N2 = (Bv * a.ww - Bw * a.u) / (a.u * b.ww - b.u * a.ww);
            double[] number = new double[2];
            number[0] = N1;
            number[1] = N2;
            return number;
        }
        

    }
}
