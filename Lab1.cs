using System;
using System.Threading;
class Program
{
    static int n;
    static int MIN_VALUE = 1;
    static bool IS_MULTI_PROCESS = false;
    static void Main()
    {
        Console.Write("Array lenght: ");
        n = Convert.ToInt32(Console.ReadLine());

        IS_MULTI_PROCESS = Confirm("compute with multiprocessing?");

        if (n < MIN_VALUE)
        {
            Console.WriteLine("Input number should be greater then 0");
            Environment.Exit(1);
        }

        for (int i = 0; i < 5; i++)
        {
            if (IS_MULTI_PROCESS)
            {
                new Thread((Sum)).Start();
            }
            else
            {
                Sum();
            }

        }
    }

    static void Sum()
    {
        int sum = (n * (n + 1)) / 2;
        Console.WriteLine("Result: " + sum);
        Console.WriteLine("Function finished execution at: " + DateTime.Now.ToString("ss:ffff tt"));
    }

    public static bool Confirm(string title)
    {
        ConsoleKey response;
        do
        {
            Console.Write($"{ title } [y/n] ");
            response = Console.ReadKey(false).Key;
            if (response != ConsoleKey.Enter)
            {
                Console.WriteLine();
            }
        } while (response != ConsoleKey.Y && response != ConsoleKey.N);

        return (response == ConsoleKey.Y);
    }
}
