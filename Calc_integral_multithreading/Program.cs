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
            CalcIntegral1 calcIntegral1 = new CalcIntegral1(0, 10, 0.00001, 4);
            calcIntegral1.Start();

            CalcIntegral2 calcIntegral2 = new CalcIntegral2(0, 10, 0.00001, 4);
            calcIntegral2.Start();

            CalcIntegral3 calcIntegral3 = new CalcIntegral3(0, 10, 0.00001, 4);
            calcIntegral3.Start();

            Console.ReadKey();
        }
    }
}
