using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Calc_integral_multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            CalcIntegral1 calcIntegral1 = new CalcIntegral1(0, 10, 0.0001, 4);
            calcIntegral1.Start();

            CalcIntegral2 calcIntegral2 = new CalcIntegral2(0, 10, 0.0001, 4);
            calcIntegral2.Start();

            Console.ReadKey();
        }
    }
}
