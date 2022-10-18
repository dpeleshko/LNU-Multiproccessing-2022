using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lb3Part2_Forms
{
    public class Matrix
    {
        public int m_size;
        public int[,] m_matrix;
        public static int threadsNum = 8;
        public Matrix(int size)
        {
            m_size = size;
            m_matrix = new int[m_size, m_size];
        }
        public void fillRandom()
        {
            Random rand = new Random();
            for (int i = 0; i < m_size; ++i)
            {
                for (int j = 0; j < m_size; ++j)
                {
                    m_matrix[i, j] = rand.Next(-10, 10);
                }
            }
        }
        public void print()
        {
            for (int i = 0; i < m_size; ++i)
            {
                Console.WriteLine();
                for (int j = 0; j < m_size; ++j)
                {
                    Console.Write(m_matrix[i, j] + " ");
                }
            }
        }
        public static Matrix operator +(Matrix temp, Matrix other)
        {
            Matrix n_matrix = new Matrix(temp.m_size);
            int step = temp.m_size / threadsNum;
            Thread[] threads = new Thread[threadsNum];

            for (int i = 0; i < threadsNum - 1; ++i)
            {
                int start = i * step, end = i * step + step;
                threads[i] = new Thread(() => calcAdding(start, end, ref n_matrix, temp, other));
                threads[i].Start();
            }
            threads[threadsNum - 1] = new Thread(() => calcAdding((threadsNum - 1) * step, temp.m_size, ref n_matrix, temp, other));
            threads[threadsNum - 1].Start();

            foreach (Thread t in threads)
            {
                t.Join();
            }
            return n_matrix;
        }

        public static Matrix operator *(Matrix temp, Matrix other)
        {

            Matrix result = new Matrix(temp.m_size);
            int step = temp.m_size / threadsNum;
            Thread[] threads = new Thread[threadsNum];

            for (int i = 0; i < threadsNum - 1; ++i)
            {
                int start = i * step, end = i * step + step;
                threads[i] = new Thread(() => calcMult(start, end, ref result, temp, other));
                threads[i].Start();
            }

            threads[threadsNum - 1] = new Thread(() => calcMult((threadsNum - 1) * step, temp.m_size, ref result, temp, other));
            threads[threadsNum - 1].Start();

            foreach (Thread t in threads)
            {
                t.Join();
            }

            return result;
        }



        private static int calculateMultiplication(List<int> thisLine, List<int> otherColumn)
        {
            int result = thisLine[0] * otherColumn[0];
            for (int i = 1; i < thisLine.Count; ++i)
            {
                result += thisLine[i] * otherColumn[i];
            }
            return result;
        }

        private static void calcMult(int start, int end, ref Matrix m, Matrix temp, Matrix other)
        {
            for (int i = start; i < end; ++i)
            {
                for (int j = 0; j < temp.m_size; ++j)
                {
                    var currentLine = temp.getLine(i);
                    m.m_matrix[i, j] = (calculateMultiplication(currentLine, other.getColumn(j)));
                }
            }
        }

        private static void calcAdding(int start, int end, ref Matrix m, Matrix temp, Matrix other)
        {
            for (int i = start; i < end; ++i)
            {
                for (int j = 0; j < m.m_size; ++j)
                {
                    m.m_matrix[i, j] = temp.m_matrix[i, j] + other.m_matrix[i, j];
                }
            }
        }

        public List<int> getColumn(int index)
        {

            List<int> column = new List<int>();
            for (int i = 0; i < m_size; ++i)
            {
                column.Add(m_matrix[i, index]);
            }
            return column;
        }

        public List<int> getLine(int index)
        {
            List<int> line = new List<int>();
            for (int i = 0; i < m_size; ++i)
            {
                line.Add(m_matrix[index, i]);
            }
            return line;
        }
    }
}
