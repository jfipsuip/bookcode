using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN.TIN
{
    public class TINHelper
    {
        public static void GetTIN(IEnumerable<Point> points)
        {
            GetInitialTriangets(points);

        }
        /// <summary>
        /// 获得初始三角形列表
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private static List<Triangle> GetInitialTriangets(IEnumerable<Point> points)
        {
            double xMax, xMin, yMax, yMin;
            Point p1, p2, p3, p4;
            List<Triangle> triangles;

            xMax = points.Select(t => t.X).Max();
            xMin = points.Select(t => t.X).Min();
            yMax = points.Select(t => t.Y).Max();
            yMin = points.Select(t => t.Y).Max();

            p1 = new Point("P1", xMin - 1, yMin - 1);
            p2 = new Point("P2", xMin - 1, yMax + 1);
            p3 = new Point("P3", xMax + 1, yMax + 1);
            p4 = new Point("P4", xMax + 1, yMin - 1);

            triangles = new List<Triangle>();
            triangles.Add(new Triangle(p1, p3, p2));
            triangles.Add(new Triangle(p1, p3, p4));


            return triangles;
        }

        public static void GetTIN(string path)
        {
            List<Point> points = GetPoints(path);
            GetTIN(points);
        }

        private static List<Point> GetPoints(string path)
        {
            string[] lines = File.ReadAllLines(path);
            string[] strs = lines.Where(t => t.Split(',').Length >= 4).ToArray();
            List<Point> points = strs.Select(t => new Point(t)).ToList();

            return points;
        }
    }
}
