using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Calc_integral_multithreading
{
    class CalcIntegral1
    {
        private double x0;
        private double xn;
        private double dx;
        private Integral integral;
        private int numberOfThreads;

        public CalcIntegral1(double x0, double xn, double dx, int numberOfThreads)
        {
            this.x0 = x0;
            this.xn = xn;
            this.dx = dx;
            this.numberOfThreads = numberOfThreads;
            this.integral = new Integral();
        }

        public void Start()
        {
            double threadRange = (this.xn - this.x0) / numberOfThreads;
            Thread[] threads = new Thread[numberOfThreads];
            for (int i = 0; i < this.numberOfThreads; i++)
            {
                CalcIntegralThread1 calcIntegralThread = new CalcIntegralThread1(this.integral, this.x0 + i * threadRange, this.x0 + (i + 1) * threadRange, this.dx, "Thread_" + i);
                Thread thread = new Thread(calcIntegralThread.Calc);
                thread.Start();
                threads[i] = thread;
            }

            for (int i = 0; i < numberOfThreads; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine("Calc integral using shared variable");
            Console.WriteLine("Integral result for x0=" + this.x0 + " xn=" + this.xn);
            Console.WriteLine("Result: " + this.integral.result + "\n\n");
        }
    }

    class CalcIntegralThread1
    {
        private double x0;
        private double xn;
        private double dx;
        private Integral integral;
        private string name;

        public CalcIntegralThread1(Integral integral, double x0, double xn, double dx, string name)
        {
            this.x0 = x0;
            this.xn = xn;
            this.dx = dx;
            this.integral = integral;
            this.name = name;
        }

        public void Calc()
        {
            for (double x = this.x0; x < this.xn; x += this.dx)
            {
                double y = this.integral.GetFunctionValue(x) * this.dx;
                this.integral.result += y;
            }
        }
    }
}
