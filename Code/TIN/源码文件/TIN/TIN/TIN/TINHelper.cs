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
        Point p1, p2, p3, p4, p0;

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
        /// P1 到 P4 组成的 点集
        /// </summary>
        List<Point> PointsP;
        public List<Point> M { get; set; }
        /// <summary>
        /// 凸包点集
        /// </summary>
        public List<Point> CH { get; set; } = new List<Point>();
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
            foreach (var point in M)
            {
                // 生成点P的平面三角网
                GetTriangles(Triangles, point);
            }
            // 1.4  删除包含初始矩形顶点的所有三角形
            //ReMoveTrianges(Triangles);

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
        public List<string> ProgramRigth()
        {
            List<string> list = new List<string>();
            list.Add($"{p2.X:F3}");
            list.Add($"{p2.Y:F3}");
            list.Add($"{p4.X:F3}");
            list.Add($"{p4.Y:F3}");
            list.Add($"{CH[2].X:F3}");
            list.Add($"{CH[2].Y:F3}");
            list.Add($"{CH[4].X:F3}");
            list.Add($"{CH[4].Y:F3}");
            list.Add($"{p0.X:F3}");
            list.Add($"{p0.Y:F3}");
            list.Add($"{InitialTriangets.Count}");
            list.Add($"{Triangles.Where(t => t.IsContain(CH[0])).Count()}");
            list.Add($"{Triangles.Where(t => t.IsContain(CH[2])).Count()}");
            list.Add($"{Triangles.Where(t => t.IsContain(CH[4])).Count()}");
            list.Add($"{Triangles.Count()}");
            double sumHS = Triangles.Select(t => t.Hp * t.S).Sum();
            double sumS = Triangles.Select(t => t.S).Sum();
            double he = sumHS / sumS;
            list.Add($"{sumS:F3}");
            list.Add($"{he:F3}");

            var cutFull = Triangles.Where(t => t.Type == ETriangle.全挖方).ToList();
            list.Add($"{cutFull.Count:F3}");
            list.Add($"{cutFull.Sum(t => t.V):F3}");

            var fillFull = Triangles.Where(t => t.Type == ETriangle.全填方).ToList();
            list.Add($"{fillFull.Count:F3}");
            list.Add($"{fillFull.Sum(t => t.V):F3}");

            var t2 = Triangles.Where(t => t.Type == ETriangle.两个顶点低).ToList();
            list.Add($"{t2.Count:F3}");
            list.Add($"{t2.Sum(t => t.V):F3}");

            var t1 = Triangles.Where(t => t.Type == ETriangle.一个顶点低).ToList();
            list.Add($"{t1.Count:F3}");
            list.Add($"{t1.Sum(t => t.V):F3}");

            list.Add($"{Triangles.Sum(t => t.Vcut):F3}");
            list.Add($"{Triangles.Sum(t => t.Vfill):F3}");

            list.Add($"{Triangles.Sum(t => t.V):F3}");
            List<string> data = new List<string>();
            data.Add("序号,输出,说明");
            string remark = @"P2 点的平面坐标 x
P2 点的平面坐标 y
P4 点的平面坐标 x
P4 点的平面坐标 y
第 3 个凸包点的平面坐标 x
第 3 个凸包点的平面坐标 y
第 5 个凸包点的平面坐标 x
第 5 个凸包点的平面坐标 y
P0 点的平面坐标 x
P0 点的平面坐标 y
初始三角形的个数
包含第 1 个凸包点的三角形个数
包含第 3 个凸包点的三角形个数
包含第 5 个凸包点的三角形个数
总的三角形个数
三角形投影底面面积之和
平衡高程
全挖方三角形的个数
全挖方三角形的挖方体积之和
全填方三角形的个数
全填方三角形的填方体积之和
有 2 个顶点低于参考高程的三角形个数
有 2 个顶点低于参考高程的三角形的填方体积之和
有 1 个顶点低于参考高程的三角形个数
有 1 个顶点低于参考高程的三角形的挖方体积之和
挖方总体积
填方总体积
总体积";
            string[] remarks = remark.Replace("\r\n","\n").Split('\n').ToArray();
            for (int i = 0; i < list.Count; i++)
            {
                string s = $"{i + 1},{list[i]},{remarks[i]}";
                data.Add(s);
            }
            return data;
        }

        private static void SetTinH(List<Triangle> triangles, double hc)
        {
            foreach (var triangle in triangles)
            {
                triangle.Hc = hc;
                triangle.CalculateV();
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

            CH = new List<Point>();
            InitialPoint();
            CalculatePoint();
            triangles = new List<Triangle>();

            p0 = GetPoint(Points);
            for (int i = 1; i < CH.Count; i++)
            {
                Point pointA, pointB;
                pointA = CH[i];
                pointB = CH[i - 1];
                triangles.Add(new Triangle(pointA, pointB, p0));
            }

            return triangles;
        }

        private Point GetPoint(List<Point> points)
        {
            double x = points.Select(t => t.X).Average();
            double y = points.Select(t => t.Y).Average();
            var list = points.Select(t => new { D = Point.GetDistance(t.X, t.Y, x, y), P = t }).ToList();
            Point point = list.OrderBy(t => t.D).Select(t => t.P).First();
            return point;
        }

        private void CalculatePoint()
        {
            CH.Add(p1);

            for (int i = 0; i < PointsP.Count() - 1; i++)
            {
                Point pointA = PointsP[i];
                Point pointB = PointsP[i + 1];
                CalculatePoint(pointA, pointB, M);
            }
        }

        /// <summary>
        /// 获取凸包点
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="points"></param>
        private void CalculatePoint(Point pointA, Point pointB, List<Point> points)
        {
            //获取点A到点B左边的点集
            List<Point> leftPoints = GetLeftPoints(pointA, pointB, points);

            // 如果左边点集为空，刚点B为凸包点
            if (leftPoints.Count == 0)
            {
                CH.Add(pointB);
                M.Remove(pointB);
            }
            // 如果左边点集不为空，则获取最远点P进行递归运算，直到左边点集为空
            else
            {

                Point point = GetFarPoint(pointA, pointB, leftPoints);
                leftPoints.Remove(point);
                // 计算点A 到 点P 凸包点
                CalculatePoint(pointA, point, leftPoints);
                // 计算点P 到 点B 凸包点
                CalculatePoint(point, pointB, leftPoints);
            }
        }
        /// <summary>
        /// 获取离点A-B最远的点
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="leftPoints"></param>
        /// <returns></returns>
        private static Point GetFarPoint(Point pointA, Point pointB, List<Point> leftPoints)
        {
            Point point;
            List<Triangle> triangles;
            Triangle triangle;

            triangles = leftPoints.Select(t => new Triangle(pointA, pointB, t)).ToList();
            triangle = triangles.OrderByDescending(t => t.Area).First();
            point = triangle.PointC;

            return point;
        }

        /// <summary>
        /// 获取点A到点B左边的点集
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        private static List<Point> GetLeftPoints(Point pointA, Point pointB, List<Point> points)
        {
            List<Point> leftPoints = new List<Point>();
            foreach (var point in points)
            {
                if (IsL(pointA, pointB, point))
                {
                    leftPoints.Add(point);
                }
            }

            return leftPoints;
        }
        /// <summary>
        /// 判断点C是否在线A-B的左边
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="pointC"></param>
        /// <returns></returns>
        private static bool IsL(Point pointA, Point pointB, Point pointC)      //判断左转还是右转
        {
            bool isL = false;
            double d;

            d = pointA.X * pointB.Y - pointB.X * pointA.Y + pointC.X * (pointA.Y - pointB.Y) + pointC.Y * (pointB.X - pointA.X);
            if (d > 0)
            {
                isL = true;
            }

            return isL;
        }

        private void InitialPoint()
        {
            xMin = Points.Min(t => t.X);
            xMax = Points.Max(t => t.X);
            yMin = Points.Min(t => t.Y);
            yMax = Points.Max(t => t.Y);

            p1 = Points.OrderBy(t => t.X).FirstOrDefault();
            p2 = Points.OrderByDescending(t => t.Y).FirstOrDefault();
            p3 = Points.OrderByDescending(t => t.X).FirstOrDefault();
            p4 = Points.OrderBy(t => t.Y).FirstOrDefault();

            M = Points.ToList();
            M.Remove(p1);
            M.Remove(p2);
            M.Remove(p3);
            M.Remove(p4);

            PointsP = new List<Point>();
            PointsP.Add(p1);
            PointsP.Add(p2);
            PointsP.Add(p3);
            PointsP.Add(p4);
            PointsP.Add(p1);
        }
        private static bool IsIN(List<Point> points, Point point)
        {
            bool isIn = false;
            int sum = 0;
            for (int i = 0; i < points.Count; i++)
            {
                Point pointA, pointB;

                pointA = points[i];
                if (i == points.Count - 1)
                {
                    pointB = points[0];
                }
                else
                {
                    pointB = points[i + 1];
                }
                int n = GetX(pointA.X, pointA.Y, pointB.X, pointB.Y, point.X, point.Y);
                sum += n;
            }

            if (sum % 2 == 1)
            {
                isIn = true;
            }

            return isIn;
        }
        private static int GetX(double xi, double yi, double xj, double yj, double x, double y)
        {
            int result = 0;

            if ((yi > y && yj < y) || (yi < y && yj > y))
            {
                double x2 = ((xj - xi) / (yj - yi)) * (y - yi) + xi;
                if (x2 > x)
                {
                    result = 1;
                }
            }

            return result;
        }

    }
}
