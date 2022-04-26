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
            List<Triangle> t1, t2;

            // 三角形列表T1
            t1 = GetInitialTriangets(points);
            // 三角形列表T2
            t2 = new List<Triangle>();
            foreach (var point in points)
            {
                // 1.3.3 遍历T1三角形列表，获得T2列表 
                foreach (var triangle in t1)
                {
                    //判断P点是否在三角形ABC外接圆的内部，若是，将该三角形剪切到三角形列表T2中(即从T1移动到T2）
                    if (triangle.IsInCircle(point))
                    {
                        t2.Add(triangle);
                        t1.Remove(triangle);
                    }
                }
            }
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
