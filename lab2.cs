using System;
using System.Threading;

namespace Lab2
{

    class Program
    {
        static double x = 5, y = 5, z = 4; 
        static Semaphore S1 = new Semaphore(0, 1);
        static Semaphore S2 = new Semaphore(0, 1);
        static Semaphore S3 = new Semaphore(0, 1);

        static void Main(string[] args)
        {
            
            
            Thread T1 = new Thread(F1);


            
            Thread T2 = new Thread(F2);

           
            Thread T3 = new Thread(F3);


            Thread T4 = new Thread(F4);

            Thread T5 = new Thread(F5);

            T1.Start();
            T2.Start();
            T3.Start();
            T4.Start();
            T5.Start();

            T5.Join(); 

            Console.WriteLine("y = " + y);
        }
        static void F1()
        {
            Console.WriteLine("F1");
            x = x * 5;
            S1.Release();
        }
        static void F2()
        {
            Console.WriteLine("F2");
            y = y + 3;
            S1.Release();
        }
        static void F3()
        {
            S1.WaitOne();
            S1.WaitOne();
            Console.WriteLine("F3");
            y = y - 3;
            S2.Release();
        }
        static void F4()
        {
            S2.WaitOne();
            Console.WriteLine("F4");
            x = x + 2;
            S3.Release();
        }
        static void F5()
        {

            S3.WaitOne();
            Console.WriteLine("F5");
            y = x * y;
        }
    }
}
