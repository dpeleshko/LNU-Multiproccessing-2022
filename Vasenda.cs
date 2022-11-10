using System;
using System.Threading;

/*
Знайти попарний скалярний добуток рядків прямокутної матриці. Кількість рядків 
матриці є парною. Матриця задається рандомно. Розмірність матриці вводиться з 
консолі
 */

namespace lab1
{
    class Program
    {
        static void findPairScalars()
        {
            int m, n;
            Console.Write("Enter Matrix size: ");
            m = System.Convert.ToUInt16(Console.ReadLine());
            n = System.Convert.ToUInt16(Console.ReadLine());
            if (m % 2 == 0)
            {
                Random rand = new Random();
                int[,] matrix = new int[m, n];

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        matrix[i, j] = rand.Next() % 10;
                    }
                }

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(matrix[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                for (int i = 0; i < m; i += 2)
                {
                    int scalar = 0;
                    for (int j = 0; j < n; j++)
                    {
                        scalar += matrix[i, j] * matrix[i + 1, j];
                    }
                    Console.WriteLine("scalar multiplication for: vector" + i + " and" + (i + 1) + " = " + scalar);
                }
            }
        }
        static void Main(string[] args)
        {
            Thread(findPairScalars).Start().Join();
        }
    }
}
