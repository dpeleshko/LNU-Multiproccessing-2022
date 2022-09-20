using System;
using System.Threading;

static public class MainClass
{
    static long n = -1;
    static int threadsNumber = 1;

    static long[] sums;
    static long totalSum = 0;

    static System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();


    static void readInput()
    {
        Console.WriteLine("Enter valid n (0 or bigger):");
        string input = Console.ReadLine();

        Console.WriteLine("Enter valid threads number (1 or bigger):");
        string thrds = Console.ReadLine();

        try
        {
            n = long.Parse(input);
            Console.WriteLine($"n is set to '{n}'.");

            threadsNumber = Int32.Parse(thrds);
            Console.WriteLine($"Threads number is set to '{thrds}'.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Wrong input.");
        }

    }

    public static void Main(string[] args)
    {

        while((n < 0) || (threadsNumber < 1))
        {
            readInput();
        }

        sums = new long[threadsNumber];


        Thread[] threads = new Thread[threadsNumber];

        for (int i = 0; i < threadsNumber; i++)
        {
            var local = i;
            threads[local] =  new Thread(() => ThreadFunction(local));
        }


        watch.Start();


        foreach (Thread t in threads)
        {
            t.Start();
        }


        foreach (Thread t in threads)
        {
            t.Join();
        }

        watch.Stop();

        foreach (long s in sums)
        {
            totalSum += s;
        }

        Console.WriteLine($"The sum is: {totalSum}");
        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
    }

    static void ThreadFunction(int startingIndex)
    {
        for (long i = startingIndex; i < n + 1; i+=threadsNumber)
        {
            sums[startingIndex] += i;
        }
    }
}
