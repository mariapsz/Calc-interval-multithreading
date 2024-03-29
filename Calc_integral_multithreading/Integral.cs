﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc_integral_multithreading
{
    class Integral
    {
        public double result;
        public Integral()
        {
            this.result = 0;
        }

        public double GetFunctionValue(double x)
        {
            return 3 * Math.Pow(x, 3) + Math.Cos(7 * x) - Math.Log(2 * x);
        }
    }
}
