using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN.Draws
{
    public interface IPoint
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// X坐标
        /// </summary>
        double X { get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        double Y { get; set; }
        /// <summary>
        /// H坐标
        /// </summary>
        double H { get; set; }
    }
}
