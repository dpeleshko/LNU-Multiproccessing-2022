using System;
using System.Threading;

public class SquareMatrix
{
    public SquareMatrix(uint n = 3)
    {
        m_size = n;
        m_matrix = new int[n, n];
        Random random = new Random();
        for(int i = 0; i < n; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                m_matrix[i, j] = random.Next() % 10;
            }
        }
    }

    public void print()
    {
        for (int i = 0; i < m_size; ++i)
        {
            for (int j = 0; j < m_size; ++j)
            {
                Console.Write(m_matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    
    public int getMatrixSum()
    {
        int upperSum = 0;
        int bottomSum = 0;
        Thread t1, t2;
        t1 = new Thread(()=>this.calcUpperSum(out upperSum));
        t2 = new Thread(() => this.calcBottomSum(out bottomSum));
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
        Console.WriteLine(upperSum + bottomSum);
        return upperSum + bottomSum;
    }

    protected void calcUpperSum(out int upperSum)
    {
        upperSum = 0;
        for (int i = 0; i < m_size; ++i)
        {
            for(int j = i; j < m_size; ++j)
            {
                upperSum += m_matrix[i, j];
            }
        }
    }

    private void calcBottomSum(out int bottomSum)
    {
        bottomSum = 0;
        for (int i = 1; i < m_size; ++i)
        {
            for (int j = 0; j < i; ++j)
            {
                bottomSum += m_matrix[i, j];
            }
        }
    }

    private int[,] m_matrix;
    private uint m_size;
}

namespace Threads_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            uint n;
            Console.Write("Enter Matrix size: ");
            n = System.Convert.ToUInt16(Console.ReadLine());
            SquareMatrix obj = new SquareMatrix(n);
            obj.print();
            obj.getMatrixSum();
            /*
            Thread thr = new Thread(new ThreadStart(obj.print));
            thr.Start();
            */
        }
    }
}
