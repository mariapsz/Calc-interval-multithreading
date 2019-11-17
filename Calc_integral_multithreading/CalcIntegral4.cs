using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Calc_integral_multithreading
{
    class CalcIntegral4
    {
        private double x0;
        private double xn;
        private double dx;
        private Integral integral;
        private int numberOfThreads;

        public CalcIntegral4(double x0, double xn, double dx, int numberOfThreads)
        {
            this.x0 = x0;
            this.xn = xn;
            this.dx = dx;
            this.numberOfThreads = numberOfThreads;
            this.integral = new Integral();
        }

        public void Start()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            double threadRange = (this.xn - this.x0) / numberOfThreads;
            Task[] threads = new Task[numberOfThreads];
            for (int i = 0; i < this.numberOfThreads; i++)
            {
                CalcIntegralThread4 calcIntegralThread = new CalcIntegralThread4(this.integral, this.x0 + i * threadRange, this.x0 + (i + 1) * threadRange, this.dx, "Thread_" + i);
                Task thread = new Task(calcIntegralThread.Calc);
                thread.Start();
                threads[i] = thread;
            }

            for (int i = 0; i < numberOfThreads; i++)
            {
                threads[i].Wait();
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Każdy wątek liczy swoją sumę lokalną + użycie tasków");
            Console.WriteLine("Wynik: " + this.integral.result);
            Console.WriteLine("Czas: " + elapsedMs + "ms\n\n");
        }
    }

    class CalcIntegralThread4
    {
        private double x0;
        private double xn;
        private double dx;
        private Integral integral;
        private string name;

        public CalcIntegralThread4(Integral integral, double x0, double xn, double dx, string name)
        {
            this.x0 = x0;
            this.xn = xn;
            this.dx = dx;
            this.integral = integral;
            this.name = name;
        }

        public void Calc()
        {
            double thisRangeResult = 0;            
            for (double x = this.x0; x < this.xn; x += this.dx)
            {
                thisRangeResult += this.integral.GetFunctionValue(x) * this.dx;
            }

            lock (this.integral)
            {
                this.integral.result += thisRangeResult;
            }
        }
    }
}
