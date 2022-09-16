using System;
using System.Threading;

namespace Main
{
    public class Program
    { 
        // константи
        const int MAX_SIZE = 50;
        const int MAX_VALUE = 1000;
        const int THREADS_COUNT = 5;
        static int rows_n, cols_n;
        static Random rnd = new Random((int)DateTime.Now.Ticks); // використовую час як сід щоб матриці щоразу були різні
        public static void Main()
        {
            Console.WriteLine("Enter rows n: ");
            rows_n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter columns n: ");
            cols_n = Convert.ToInt32(Console.ReadLine());

            // вийти з програми якщо розмір матриці завеликий
            if (rows_n > MAX_SIZE || cols_n > MAX_SIZE)
            {
                Console.WriteLine("Maximum matrix size is " + MAX_SIZE);
                Environment.Exit(32);
            }
            // стартанути N к-сть тредів
            for (int i=0; i < THREADS_COUNT; i++)
              	new Thread((MinElmntsSum)).Start();
        }
        static void MinElmntsSum()
        {
            // ініт матриці
            int[,] matrix = new int[rows_n, cols_n];

            for (int i = 0; i < rows_n; i++)
            {
                for (int j = 0; j < cols_n; j++)
                {
                    matrix[i, j] = rnd.Next(-MAX_VALUE, MAX_VALUE);
                    Console.Write(string.Format("{0} ", matrix[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }

            int sum = 0;
            int cur_min;

            // пошук min елементів та їх сумування
            for (int c = 0; c < cols_n; ++c)
            {
                cur_min = MAX_VALUE + 1;
                for (int r = 0; r < rows_n; ++r)
                    if (matrix[r, c] < cur_min)
                      cur_min = matrix[r, c];
                    
                sum += cur_min;
            }
            Console.Write("Min el sum =" + sum + Environment.NewLine);
        }
    }
}