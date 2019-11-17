using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Calc_integral_multithreading
{
    //(i) Wszystkie wątki sumują wynik w tej samej 
    //współdzielonej zmiennej - bez synchronizacji
    //(jest to, oczywiście, wersja niepoprawna).

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
                CalcIntegralThread calcIntegralThread = new CalcIntegralThread(this.integral, this.x0 + i * threadRange, this.x0 + (i + 1) * threadRange, this.dx, "Thread_" + i);
                Thread thread = new Thread(calcIntegralThread.Calc);
                thread.Start();
                threads[i] = thread;
            }

            for (int i = 0; i < numberOfThreads; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine("Integral result for x0=" + this.x0 + " xn=" + this.xn);
            Console.WriteLine("Result: " + this.integral.result);
        }
    }

    class Integral
    {
        public double result;
        public Integral()
        {
            this.result = 0;
        }
    }

    class CalcIntegralThread
    {
        private double x0;
        private double xn;
        private double dx;
        private Integral integral;
        private string name;

        public CalcIntegralThread(Integral integral, double x0, double xn, double dx, string name)
        {
            this.x0 = x0;
            this.xn = xn;
            this.dx = dx;
            this.integral = integral;
            this.name = name;
        }

        public void Calc()
        {
            for (double arg = this.x0; arg < this.xn; arg += this.dx)
            {
                double y = this.GetFunctionValue(arg) * this.dx;
                lock (this.integral)
                {
                    this.integral.result += y;
                }
            }
        }

        private double GetFunctionValue(double arg)
        {
            return arg;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            CalcIntegral1 calcIntegral1 = new CalcIntegral1(0, 10, 0.0001, 4);
            calcIntegral1.Start();
            Console.ReadKey();
        }
    }
}
