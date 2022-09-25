using System;
using System.Threading;

namespace Lab1
{
    class Program
    {
        static void getAverage()
        {
            int x, y;
            const int MAX_RAND = 50;
            Console.Write("Введіть висоту матрицю: ");
            x = System.Convert.ToInt32(Console.ReadLine());
            Console.Write("Введіть ширину матрицю: ");
            y = System.Convert.ToInt32(Console.ReadLine());
            

            Random rand = new Random();
            int[,] matrix = new int[x, y];

            for (int i = 0; i < x; i++)// Заповнємо матрицю
            {
                for (int j = 0; j < y; j++)
                {
                    matrix[i, j] = rand.Next(-MAX_RAND, MAX_RAND);
                }
            }
            Console.Write("\n");
            for (int i = 0; i < x; i++)//Виводимо матрицю в консоль
            {
                for (int j = 0; j < y; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.Write("\n");
            }

            int[] maxNumbers = new int[x];// Масив для максимальних чисел в кожному рядку

            for (int i = 0; i < x; ++i) //Знаходимо максимальні числа і переводимо їх в масив 
            {
                int max = 0;
                for (int j = 0; j < y; j++)
                {
                    if (matrix[i, j] > max)
                    {
                        max = matrix[i, j];
                    }
                }
                maxNumbers[i] = max;
            }

            float average = 0;
            foreach( int item in maxNumbers)//Знаходимо суму всіх максимальних чисел
            {
                average += item;
            }

            Console.WriteLine("\nСереднє арефмитечне: " + (average / x));// Ділимо на висоту матриці і виводимо на консоль результат
        }
        static void Main(string[] args)
        {
            Thread t = new Thread(getAverage);
            t.Start();
        }
    }
}
