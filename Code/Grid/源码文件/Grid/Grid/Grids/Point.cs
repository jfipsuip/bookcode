﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid.Grids
{
    public class Point
    {
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double H { get; set; }

        public Point()
        {

        }

        public Point(string name, double x, double y, double h = 0)
        {
            Name = name;
            X = x;
            Y = y;
            H = h;
        }
        public Point(string line) : this()
        {
            string[] strs = line.Split(',');
            Name = strs[0];
            X = Convert.ToDouble(strs[1]);
            Y = Convert.ToDouble(strs[1]);
            H = Convert.ToDouble(strs[1]);
        }
        public override string ToString()
        {
            return $"{Name} {X} {Y} {H}";
        }
    }
}
