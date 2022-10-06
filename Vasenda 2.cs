using System;
using System.Threading;

namespace Lab2
{
    
    class Program
    {
        static double x = 2, y = 5, z = 4;
        static Semaphore S1 = new Semaphore(0, 1);
        static Semaphore S2 = new Semaphore(0, 1);
        static Semaphore S3 = new Semaphore(0, 1);
        static Semaphore S4 = new Semaphore(0, 1);
        static Semaphore S5 = new Semaphore(0, 1);


        static void Main(string[] args)
        {

            //First Branch
            Thread T1 = new Thread(F1);


            Thread T3 = new Thread(F2);


            //Second Branch
            Thread T2 = new Thread(F3);

            Thread T4 = new Thread(F4);

            Thread T5 = new Thread(F5);
            T1.Start(); T2.Start(); T3.Start(); T4.Start(); T5.Start();
            T1.Join(); T2.Join(); T3.Join(); T4.Join(); T5.Join();
            Console.WriteLine(y);
        }
        static void F1()
        {
            x = x / 4;
            S1.Release();
        }
        static void F2()
        {
            S1.WaitOne();
            y = y - 3;
            S2.Release();
            S3.Release();
        }
        static void F3()
        {
            S2.WaitOne();
            x = x + 8;
            S4.Release();
        }
        static void F4()
        {
            S3.WaitOne();
            y = y * 5;
            S5.Release();
        }
        static void F5()
        {
            S4.WaitOne();
            S5.WaitOne();
            y = x + y;
        }
    }
}