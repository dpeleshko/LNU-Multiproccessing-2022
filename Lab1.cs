using System;
using System.Threading;

/*
Знайти попарний скалярний добуток рядків прямокутної матриці. Кількість рядків 
матриці є парною. Матриця задається рандомно. Розмірність матриці вводиться з 
консолі
 */

namespace lab1
{
    public class VectorPair{

        public int[] FirstVector { get; set; }
        public int[] SecondVector { get; set; }
        public int Lenght { get; set; }
        public int RowNumber { get; set; }

        public VectorPair(int[] first, int[] second, int rowNumber)
        {
            FirstVector = first;
            SecondVector = second;
            Lenght = second.Length;
            RowNumber = rowNumber;
        }
    }
    class Program
    {
        static int[,] GenerateMatrix()
        {
            int m, n;
            Console.Write("Enter Matrix size: ");
            m = int.Parse(Console.ReadLine()!);
            n = int.Parse(Console.ReadLine()!);

            int[,] matrix = new int[m, n];
            if (m % 2 == 0)
            {
                Random rand = new Random();
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
                return matrix;
            }
            else { return null; }
        }

        static void FindPairScalarsRow(object obj) 
        {
            var vectorPair = (VectorPair)obj; 

            int scalar = 0;
            for (int i = 0; i < vectorPair.Lenght; i++)
            {
                scalar  += vectorPair.FirstVector[i] * vectorPair.SecondVector[i];
            }

            Console.WriteLine($"Scalar multiplication Of vectors {vectorPair.RowNumber + 1} and {vectorPair.RowNumber + 2}  = " + scalar);
        }

        static void ThreadStarter(VectorPair pair) {
            Thread myThread = new Thread(FindPairScalarsRow);
            myThread.Start(pair);
        }
        static void Main(string[] args)
        {
            var matrix = GenerateMatrix();
            
                
            var rowNumber = 0;

            for (int j = 0; j < matrix.GetLength(1) - 1; j++)
            {

                var first = new int[matrix.GetLength(1)];
                var second = new int[matrix.GetLength(1)];

                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    first[i] = (int)matrix.GetValue(rowNumber, i)!;
                    second[i] = (int)matrix.GetValue(rowNumber + 1, i)!;
                }

                ThreadStarter(new VectorPair(first, second, rowNumber));

                rowNumber++;
            }
        }
    }
}