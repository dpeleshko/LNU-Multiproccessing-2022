using System;
using System.Threading;
class MainClass
{
    static int x = 9, y = 5, z = 4;
    static Semaphore S1 = new Semaphore(0, 1);
    static Semaphore S2 = new Semaphore(0, 1);
    static Semaphore S3 = new Semaphore(0, 1);
    static Semaphore S4 = new Semaphore(0, 1);
    public static void Main(string[] args)
    {
        Thread thread1 = new Thread((ThreadFunction1) => 
        {
            x = x * 5;
            S1.Release();
        });
        Thread thread2 = new Thread((ThreadFunction2) => 
        { 
            y = y + 2; 
            S2.Release();
        });
        Thread thread3 = new Thread((ThreadFunction3) => 
        {
            S1.WaitOne();
            S2.WaitOne();
            x = x + 2;
            S3.Release();
        });
        Thread thread4 = new Thread((ThreadFunction4) => 
        {
            S2.WaitOne();
            S3.WaitOne();
            y = y - 3;
            S4.Release();
        });
        Thread thread5 = new Thread((ThreadFunction5) =>
        {
            S5.WaitOne();
            y = x * y;
        });
        thread1.Start();
        thread2.Start();
        thread3.Start();
        thread4.Start();
        thread5.Start();
        thread1.Join();
        thread2.Join();
        thread3.Join();
        thread4.Join();
        thread5.Join();
        Console.WriteLine("x = {0}; y = {1}; z = {2}", x, y, z);

    }
}