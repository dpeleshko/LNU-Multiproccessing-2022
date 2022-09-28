using System;
using System.Threading;

namespace Lab1
{
    class Program
    {
        static void get_Diagonals_Length_Sum()
        {
            int matrix_size;
            Console.Write("input matrix size: ");
            matrix_size = System.Convert.ToInt32(Console.ReadLine());

            Random random = new Random();
            int[,] matrix = new int[matrix_size, matrix_size];

            for (int i = 0; i < matrix_size; i++)
            {
                for (int j = 0; j < matrix_size; j++)
                {
                    matrix[i, j] = random.Next() % 10;
                }
            }
            Console.Write("\n");

            double main_diagonal_length = 0;
            double second_diagonal_length = 0;

            for (int i = 0; i < matrix_size; ++i) 
            {
                main_diagonal_length += Math.Pow(matrix[i, i], 2); 
            }
            main_diagonal_length = Math.Sqrt(main_diagonal_length);

            int col = matrix_size - 1;
            for (int i = 0; i < matrix_size; ++i)
            {
                second_diagonal_length += Math.Pow(matrix[i, col], 2);
                --col;
            }
            second_diagonal_length = Math.Sqrt(second_diagonal_length);

            for (int i = 0; i < matrix_size; i++)
            {
                for (int j = 0; j < matrix_size; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine(main_diagonal_length + second_diagonal_length);
        }
        static void Main(string[] args)
        {
            Thread t1 = new Thread(get_Diagonals_Length_Sum);
            t1.Start();
            
        }
    }
}
