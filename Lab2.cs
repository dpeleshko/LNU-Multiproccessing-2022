using System;
using System.Threading;

namespace Lab2
{
    class Program
    {
        static double x = 1, y = 5, z = 4;
        static Semaphore S1 = new Semaphore(0, 1);
        static Semaphore S2 = new Semaphore(0, 1);
        static Semaphore S3 = new Semaphore(-1, 2);
        //static Semaphore S4 = new Semaphore(0, 1);
        static void Main(string[] args)
        {

            //First Branch
            Thread T1 = new Thread(() =>
            {
                x *= 5; S1.Release();
            });


            Thread T3 = new Thread(() =>
            {
                S1.WaitOne();
                x += 2; S3.Release();
            });


            //Second Branch
            Thread T2 = new Thread(() =>
            {
                y += 2; S2.Release();
            });


            Thread T4 = new Thread(() =>
            {
                S2.WaitOne();
                y -= 3; S3.Release();
            });

            //end move
            Thread T5 = new Thread(() =>
            {

                S3.WaitOne();
                //S4.WaitOne();
                y *= x;
                S3.Release();
                //S4.Release();
            });

            Thread[] threads = { T1, T2, T3, T4, T5 };
            foreach (Thread t in threads)
            {
                t.Start();
            }
            foreach (Thread t in threads)
            {
                t.Join();
            }

            Console.WriteLine(y);
        }
    }
}

