using System;
using System.Threading;
using System.Collections.Generic;

class Program
{
    const int THREADS_COUNT = 5;

    static double x = 7, y = 5, z = 4;
    static List<Semaphore> semaphores = new List<Semaphore>();
    static List<Thread> threads = new List<Thread>();

    static void Main(string[] args)
    {
        for (int i = 0; i < THREADS_COUNT; i++)
            semaphores.Add(new Semaphore(0, 1));

        threads.Add(new Thread(() =>
        {
            y += 2;
            Console.WriteLine("T1: y = y + 2");
            semaphores[0].Release();
        }));
        threads.Add(new Thread(() =>
        {
            x *= 5;
            Console.WriteLine("T2: x = x * 5");
            semaphores[1].Release();
        }));
        threads.Add(new Thread(() =>
        {
            z /= 2;
            Console.WriteLine("T3: z = z / 2");
            semaphores[2].Release();
        }));
        threads.Add(new Thread(() =>
        {
            semaphores[1].WaitOne();
            semaphores[2].WaitOne();
            z = x - z;
            Console.WriteLine("T4: z = x - z");
            semaphores[3].Release();
        }));
        threads.Add(new Thread(() =>
        {
            semaphores[0].WaitOne();
            semaphores[3].WaitOne();
            y = x * z;
            Console.WriteLine("T5: y = x * z");
        }));

        Console.WriteLine("x = {0}; y = {1}; z = {2}", x, y, z);
        foreach (Thread t in threads)
            t.Start();
        foreach (Thread t in threads)
            t.Join();

        Console.WriteLine("x = {0}; y = {1}; z = {2}", x, y, z);
    }
}