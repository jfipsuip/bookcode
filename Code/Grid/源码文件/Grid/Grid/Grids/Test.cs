using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid.Grids
{
    public class Test
    {
        public static void Usage()
        {
            string path = @"D:\github\jfipsuip\bookcode\Code\Grid\测试数据\GRID数据.txt";
            string[] lines = File.ReadAllLines(path);
            string line = lines[0];
            double h = Convert.ToDouble(line.Split(',')[1]);
            string[] linePoints = lines.Skip(3).ToArray();

            List<Point> points = linePoints.Select(t => new Point(t)).ToList();
            Grid grid = new Grid(points, h);
            grid.Calculate();
        }
    }
}
