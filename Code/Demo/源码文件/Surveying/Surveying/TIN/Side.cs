using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN.TIN
{
    /// <summary>
    /// 边
    /// </summary>
    public class Side
    {
        public Point PointA { get; set; }
        public Point PointB { get; set; }

        public Side(Point pointA, Point pointB)
        {
            PointA = pointA;
            PointB = pointB;
        }

        /// <summary>
        /// 判断边是否为公共边
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
        public bool IsCommonSide(Side side)
        {
            bool isCommon = false;
            if (this == side)
            {
                isCommon = false;
            }
            else if (PointA == side.PointA && PointB == side.PointB)
            {
                isCommon = true;
            }
            else if (PointA == side.PointB && PointB == side.PointA)
            {
                isCommon = true;
            }

            return isCommon;
        }

        public override string ToString()
        {
            return $"{PointA.Name} {PointB.Name}";
        }
    }
}
