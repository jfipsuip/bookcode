using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN.TIN
{
    public class Test
    {
        public static void Usage1()
        {
            string str = "P01,3778.594,2885.732,9.468";
            Point point = new Point(str);
        }
        public static void Usage2()
        {

            Triangle triangle = new Triangle(new Point("P1,0,0,0"), new Point("P1,4,0,0"), new Point("P1,0,2,0"));

        }
    }
}
