using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace 规则网
{
    class 计算空间辅助坐标
    {
        //要求uvw;
        public static double[] XiShu(double a,double w,double k,double x1,double y1,double f)//a = fai,b = w,c = k
        {
            double cosa = Math.Cos(Topint.Parsess(a));
            double sina = Math.Sin(Topint.Parsess(a));
            double cosw = Math.Cos(Topint.Parsess(w));
            double sinw = Math.Sin(Topint.Parsess(w));
            double cosk = Math.Cos(Topint.Parsess(k));
            double sink = Math.Sin(Topint.Parsess(k));

            double a1 = cosa * cosk - cosa * sinw * sink;
            double a2 = -cosa * sink - sina * sinw * sink;
            double a3 = -sina * cosw;

            double b1 = cosw * sink;
            double b2 = cosw * cosk;
            double b3 = -sinw;

            double c1 = sina * cosk + cosa * sinw * sink;
            double c2 = -sinw * cosk + cosa * sinw * sink;
            double c3 = cosa * cosw;

            //double[,] xishu = new double[3, 3]
            //{
            //    {a1,a2,a3},
            //    {b1,b2,b3},
            //    {c1,c2,c3},
            //};
           
 
            double[] zhongjian = new double[3];
            zhongjian[0] = a1 * x1 + a2 * y1 + a3 * f;
            zhongjian[1] = b1 * x1 + b1 * y1 + b3 * f;
            zhongjian[2] = c1 * x1 + c1 * y1 + c3 * f;
            return zhongjian;
        }
    }
}
