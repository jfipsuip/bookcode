using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 规则网
{
    public struct Tpoint
    {
        public double x;
        public double y;
        public double f;
        public double Xs;
        public double Ys;
        public double Zs;
        public double a;
        public double w;
        public double k;

        public double u;
        public double v;
        public double ww;
    }
    //public struct XYlist
    //{
    //    public double x;
    //    public double y;
    //}
    class Topint
    {
       
        //public class Tpoint
        //{
        //    /// <summary>
        //    /// 点号
        //    /// </summary>
        //    public int Num;
        //    /// <summary>
        //    /// 点名
        //    /// </summary>
        //    public string Name;
        //    public double x;
        //    public double y;
        //    public double f;
        //    public double Xs;
        //    public double Ys;
        //    public double Zs;
        //    public double α;
        //    public double w;
        //    public double k;


        //    public Tpoint(int num, string name, double x, double y, double f, double xs, double ys, double zs, double a, double w, double K)
        //    {
        //        Num = num;
        //        Name = name;
        //        this.x = x;
        //        this.y = y;
        //        this.f = f;
        //        this.Xs = xs;
        //        this.Ys = ys;
        //        this.Zs = zs;
        //        this.α = a;
        //        this.w = w;
        //        this.k = K;
        //    }

        //    public Tpoint(double x, double y, double f, double xs, double ys, double zs, double a, double w, double K)
        //    {
        //        this.x = x;
        //        this.y = y;
        //        this.f = f;
        //        this.Xs = xs;
        //        this.Ys = ys;
        //        this.Zs = zs;
        //        this.α = a;
        //        this.w = w;
        //        this.k = K;
        //    }

        //    public Tpoint()
        //    {

        //    }

        //public override string ToString()
        //{
        //    for (int i = 0; i < length; i++)
        //    {

        //    }
        //}
        public static double Parsess(double number)
        {
            double num = number * Math.PI / 180;
            return num;
        }
        
    }
}
