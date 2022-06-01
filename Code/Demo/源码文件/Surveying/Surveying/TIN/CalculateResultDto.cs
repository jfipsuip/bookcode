using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN.TIN
{
    public class CalculateResultDto
    {
        private double 挖方体积1;
        private double 填方体积1;
        private double 总体积1;

        public int 序号 { get; set; }
        public string 点名1 { get; set; }
        public string 点名2 { get; set; }
        public string 点名3 { get; set; }
        public double 挖方体积 { get => Math.Round(挖方体积1, 3); set => 挖方体积1 = value; }
        public double 填方体积 { get => Math.Round(填方体积1, 3); set => 填方体积1 = value; }
        public double 总体积 { get => Math.Round(总体积1, 3); set => 总体积1 = value; }
    }
}
