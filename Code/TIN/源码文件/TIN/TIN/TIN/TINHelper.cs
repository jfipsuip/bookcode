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

        double xMax, xMin, yMax, yMin;
        Point p1, p2, p3, p4;

        public List<Point> Points { get; }

        public List<Triangle> Triangles { get; } = new List<Triangle>();
        public List<Triangle> InitialTriangets { get; } = new List<Triangle>();

        public List<Triangle> TrianglesMin5
        {
            get
            {
                return Triangles.OrderBy(t => t.V).Take(5).ToList();
            }
        }
        List<Triangle> TrianglesMAX5
        {
            get
            {
                return Triangles.OrderByDescending(t => t.V).Take(5).ToList();
            }
        }
        /// <summary>
        /// 基准高程
        /// </summary>
        public double H { get; set; }

        /// <summary>
        /// 计算总体积
        /// </summary>
        public double? V
        {
            get
            {
                return Triangles.Select(t => t.V).Sum();
            }
        }
        /// <summary>
        /// 初始化三角网
        /// </summary>
        /// <param name="points">构成三角网的所有点</param>
        /// <param name="h">三角网的基准高程</param>
        public TINHelper(List<Point> points, double h = 0)
        {
            Points = points;
            H = h;
        }

        public void Calculate()
        {
            if (Points?.Count() == 0)
            {
                return;
            }
            // 三角形列表T1
            var ts = GetInitialTriangets(Points);
            InitialTriangets.AddRange(ts);
            Triangles.AddRange(ts);
            // 1.3 遍历离散点 生成平面三角网
            foreach (var point in Points)
            {
                // 生成点P的平面三角网
                GetTriangles(Triangles, point);
            }
            // 1.4  删除包含初始矩形顶点的所有三角形
            ReMoveTrianges(Triangles);

            SetTinH(Triangles, H);

        }

        public string Report()
        {
            List<string> list = new List<string>();
            list.Add("------------基本信息------------");
            list.Add($"基准高程：{H}m");
            list.Add($"三角形个数：{Triangles.Count}");
            list.Add($"体积：{V:F3}");
            list.Add($"");
            list.Add($"------------20个三角形说明------------");
            list.Add($"序号     三个顶点");
            for (int i = 0; i < 20 && i < Triangles.Count(); i++)
            {
                list.Add($"{i + 1}     {Triangles[i]}");
            }
            list.Add($"");
            list.Add($"------------体积最小的5个三棱柱体积------------");
            for (int i = 0; i < TrianglesMin5.Count; i++)
            {
                list.Add($"{i + 1}：   {TrianglesMin5[i].V:F3}");
            }
            list.Add($"");
            list.Add($"------------体积最大的5个三棱柱体积------------");
            for (int i = 0; i < TrianglesMAX5.Count; i++)
            {
                list.Add($"{i + 1}：   {TrianglesMAX5[i].V:F3}");
            }
            string result = list.Aggregate((m, n) => m + "\n" + n);

            return result;
        }


        private static void SetTinH(List<Triangle> triangles, double hc)
        {
            foreach (var triangle in triangles)
            {
                triangle.Hc = hc;
            }
        }

        /// <summary>
        /// 删除包含初始矩形顶点的所有三角形
        /// </summary>
        /// <param name="t1"></param>
        private void ReMoveTrianges(List<Triangle> t1)
        {
            List<Triangle> ts = new List<Triangle>();
            foreach (var triangle in t1)
            {
                if (triangle.IsContain(p1, p2, p3, p4))
                {
                    ts.Add(triangle);
                }
            }
            foreach (var triangle in ts)
            {
                t1.Remove(triangle);
            }
        }

        /// <summary>
        /// 生成点P的平面三角网
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="point"></param>
        private static void GetTriangles(List<Triangle> t1, Point point)
        {
            List<Triangle> t2;

            // 三角形列表T2
            t2 = new List<Triangle>();
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

            // 1.3.5 添加新三角形到列表T1
            var triangles = GetAddTriangle(sides, point);
            t1.AddRange(triangles);
        }

        private static List<Triangle> GetAddTriangle(List<Side> sides, Point point)
        {
            List<Triangle> triangles;

            triangles = new List<Triangle>();
            foreach (var side in sides)
            {
                triangles.Add(new Triangle(side.PointA, side.PointB, point));
            }

            return triangles;
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

            return result;
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
        private List<Triangle> GetInitialTriangets(IEnumerable<Point> points)
        {
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
    }
}
