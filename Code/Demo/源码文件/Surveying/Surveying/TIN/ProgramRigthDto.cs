using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN.TIN
{
    public class ProgramRigthDto
    {
        private double shuchu;

        public int 序号 { get; set; }
        public double 输出 { get => Math.Round(shuchu, 3); set => shuchu = value; }
        public string 说明 { get; set; }
    }
}
