using System;

using System.Threading;
namespace lab3
{
    class Program
    {
        static EventWaitHandle 
          wh1 = new AutoResetEvent(false),
          wh2 = new AutoResetEvent(false),
          wh3 = new AutoResetEvent(false),
          wh4 = new AutoResetEvent(false);


        static int x1, x2, x3, x4, x5, x6;
        static int A, B, C;
        static void Main(string[] args)
        {
            Console.WriteLine("(x1 + x2)(x3 + x4) + x5 * x6");
            var T0 = new Thread(Func1);
            var T1 = new Thread(Func2);
            var T2 = new Thread(Func3);
            var T3 = new Thread(Func4);
            var T4 = new Thread(Func5);
            T0.Start();
            T1.Start();
            T2.Start();
            T3.Start();
            T4.Start();
            T4.Join();

            Console.ReadKey();
        }
        //x1 = 1, x2 = 2, x3 = 3, x4 = 4,  x5 = 5, x6 = 6.
        static void Func1()
        {
            x1 = 1;
            x2 = 2;
            A = x1 + x2;
            Console.WriteLine("A = (x1 + x2) = " + A);
            wh1.Set();
        }

        static void Func2()
        {

            x3 = 3;
            x4 = 4;
            B = x3 + x4;
            Console.WriteLine("B = (x3 + x4) = " + B);
            wh2.Set();
        }
        static void Func3()
        {
            x5 = 5;
            x6 = 6;
            
            C = x5 * x6;
            Console.WriteLine("C = x5 * x6 = " + C);
            wh3.Set();
        }

        static void Func4()
        {

            WaitHandle.WaitAll(new EventWaitHandle[] { wh1, wh2 }, -1);
            A = A * B;
            wh4.Set();
            Console.WriteLine("D = A * B = " + A);
        }

        static void Func5()
        {
            WaitHandle.WaitAll(new EventWaitHandle[] { wh3, wh4 }, -1);
            Console.WriteLine("D + C = " + (A + C));
        }
    }
}