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
                    }
                }
                // 从T1移除
                foreach (var triangle in t2)
                {
                    t1.Remove(triangle);
                }
                // 1.3.4 获取边列表S;
                List<Side> sides = GetSide(t2);
            }
        }

        private static List<Side> GetSide(List<Triangle> triangles)
        {
            List<Side> sides, result;

            sides = new List<Side>();
            // 获取三角形列表的所有边
            foreach (var t in triangles)
            {
                sides.Add(new Side(t.PointA, t.PointB));
                sides.Add(new Side(t.PointA, t.PointC));
                sides.Add(new Side(t.PointB, t.PointC));
            }

            // 获取删除公共边后的边列表 
            result = GetSide(sides);
            // 清空T2
            triangles = new List<Triangle>();

            return sides;
        }
        /// <summary>
        /// 获取删除公共边后的边列表
        /// </summary>
        /// <param name="sides"></param>
        /// <returns></returns>
        private static List<Side> GetSide(List<Side> sides)
        {
            List<Side> result = new List<Side>();

            foreach (var side in sides)
            {
                if (IsCommon(sides, side) == false)
                {
                    result.Add(side);
                }
            }

            return result;

        }

        private static bool IsCommon(List<Side> sides, Side sideA)
        {
            foreach (var sideB in sides)
            {
                if (sideA != sideB && sideA.IsCommonSide(sideB))
                {
                    return true;
                }
            }

            return false;
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
            yMin = points.Select(t => t.Y).Min();

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
