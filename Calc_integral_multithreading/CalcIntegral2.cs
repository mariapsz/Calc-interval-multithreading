using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Calc_integral_multithreading
{
    class CalcIntegral2
    {
        private double x0;
        private double xn;
        private double dx;
        private Integral integral;
        private int numberOfThreads;

        public CalcIntegral2(double x0, double xn, double dx, int numberOfThreads)
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
                CalcIntegralThread2 calcIntegralThread = new CalcIntegralThread2(this.integral, this.x0 + i * threadRange, this.x0 + (i + 1) * threadRange, this.dx, "Thread_" + i);
                Thread thread = new Thread(calcIntegralThread.Calc);
                thread.Start();
                threads[i] = thread;
            }

            for (int i = 0; i < numberOfThreads; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine("Wszystkie wątki sumują wynik w tej samej współdzielonej zmiennej - chronionej przez zamek");
            Console.WriteLine("Wynik: " + this.integral.result + "\n\n");
        }
    }

    class CalcIntegralThread2
    {
        private double x0;
        private double xn;
        private double dx;
        private Integral integral;
        private string name;

        public CalcIntegralThread2(Integral integral, double x0, double xn, double dx, string name)
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
                lock (this.integral)
                {
                    this.integral.result += y;
                }
            }
        }
    }
}
