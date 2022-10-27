using System;
using System.Threading;

namespace Lab3
{
    class Program
    {
        static EventWaitHandle wh1 = new AutoResetEvent(false),
            wh2 = new AutoResetEvent(false), wh3 = new AutoResetEvent(false);
        static private int x1, x2, x3, x4, x5, x6;
        static int A, B, C;
        static void Main(string[] args)
        {
            Thread T0 = new Thread(Func0);
            Thread T1 = new Thread(Func1);
            Thread T2 = new Thread(Func2);
            Thread T3 = new Thread(Func3);
            T0.Start();
            T1.Start();
            T2.Start(); 
            T3.Start();
            T0.Join();
            T1.Join();
            T2.Join();
            T3.Join();
        }
        static void Func0()
        {
            x1 = 1;
            x2 = 2;
            A = x1 + x2;
            wh1.Set();
        }
        static void Func1()
        {
            x3 = 3;
            x4 = 4;
            B = x3 + x4;
            wh2.Set();
        }
        static void Func2()
        {
            x5 = 5;
            x6 = 6;
            C = x5 + x6;
            wh3.Set();
        }
        static void Func3()
        {
            WaitHandle.WaitAll(new EventWaitHandle[] { wh1, wh2, wh3 }, -1);
            Console.WriteLine("F = {0}", A + B + C);
        }

    }
}
