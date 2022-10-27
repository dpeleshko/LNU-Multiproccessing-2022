using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace LABARATORNA_3._2
{
    public partial class Form1 : Form
    {
        static public int RandomNumber()
        {
            Random _random = new Random();
            return _random.Next(-10, 10);
        }
        class Matrix
        {
            int n = 5;
            int[,] matrix;
            static int[,] defmatrix;
            public static List<int> temp = new List<int>();
            public static int threadsCount = Environment.ProcessorCount;
            
            public void MultipleThreads(int n)
            {
                threadsCount = n;
            }
            public void build()
            {
                int[,] matrix = new int[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        matrix.SetValue(RandomNumber(), i, j);
                    }
                }

                this.matrix = matrix;

            }
            public int[,] buildafter(List<int> temp)
            {

                int[,] matrix = new int[n, n];
                int counter = 0;
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {
                        matrix.SetValue(temp[counter], i, j);
                        counter = counter + 1;
                    }

                }
                return matrix;
            }
            public int[,] buildaftermultiple(List<int> temp)
            {

                int[,] matrix = new int[n, n];
                int counter = 0;


                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < n; j++)
                    {
                        matrix.SetValue(temp[counter], i, j);
                        counter = counter + 1;
                    }

                }


                return matrix;
            }
            public Matrix(int n)
            {
                
                this.n = n;

                build();
            }

            public void Print()
            {

                Console.WriteLine();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(string.Format("{0} ", matrix[i, j]));
                    }
                    Console.Write(Environment.NewLine);
                }

            }

            public static Matrix operator +(Matrix matrix1, Matrix matrix2)
            {
               
                int n = matrix1.n;
                int step = n / threadsCount;
                defmatrix = matrix1.matrix;

                Thread[] threads = new Thread[threadsCount];


                for (int i = 0; i < threadsCount - 1; i++)
                {
                    int start = i * step, end = i * step + step;
                    threads[i] = new Thread(() => Adding(start, end, matrix1, matrix2));
                    threads[i].Start();
                }
                threads[threadsCount - 1] = new Thread(() => Adding((threadsCount - 1) * step, n, matrix1, matrix2));
                threads[threadsCount - 1].Start();
                foreach (Thread t in threads)
                {
                    t.Join();
                }


                Matrix newmatrix = new Matrix(n);
                newmatrix.matrix = defmatrix;




                return newmatrix;
            }

            public static void Adding(int start, int end, Matrix first, Matrix second)
            {
                for (int i = start; i < end; i++)
                {
                    for (int j = 0; j < first.n; j++)
                    {
                        defmatrix[i, j] = first.matrix[i, j] + second.matrix[i, j];
                    }
                }


            }
            public static Matrix operator *(Matrix temp, Matrix other)
            {

                Matrix result = new Matrix(temp.n);
                int step = temp.n / threadsCount;
                Thread[] threads = new Thread[threadsCount];

                for (int i = 0; i < threadsCount - 1; ++i)
                {
                    int start = i * step, end = i * step + step;
                    threads[i] = new Thread(() => Mult(start, end, ref result, temp, other));
                    threads[i].Start();
                }

                threads[threadsCount - 1] = new Thread(() => Mult((threadsCount - 1) * step, temp.n, ref result, temp, other));
                threads[threadsCount - 1].Start();

                foreach (Thread t in threads)
                {
                    t.Join();
                }

                return result;
            }



            private static int Multip(List<int> thisLine, List<int> otherColumn)
            {
                int result = thisLine[0] * otherColumn[0];
                for (int i = 1; i < thisLine.Count; ++i)
                {
                    result += thisLine[i] * otherColumn[i];
                }
                return result;
            }

            private static void Mult(int start, int end, ref Matrix m, Matrix temp, Matrix other)
            {
                for (int i = start; i < end; ++i)
                {
                    for (int j = 0; j < temp.n; ++j)
                    {
                        var currentLine = temp.getLine(i);
                        m.matrix[i, j] = (Multip(currentLine, other.getColumn(j)));
                    }
                }
            }

            public List<int> getColumn(int index)
            {

                List<int> column = new List<int>();
                for (int i = 0; i < n; ++i)
                {
                    column.Add(matrix[i, index]);
                }
                return column;
            }

            public List<int> getLine(int index)
            {
                List<int> line = new List<int>();
                for (int i = 0; i < n; ++i)
                {
                    line.Add(matrix[index, i]);
                }
                return line;
            }

        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> ns = new List<int>();
            List<long> time = new List<long>();

            List<int> ns1 = new List<int>();
            List<long> time1 = new List<long>();



            var watch = new System.Diagnostics.Stopwatch();

            //Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");


            int m = 100;

            for (int n = 2; n <= m; n++)
            {
                watch.Restart();
                watch.Start();
                Matrix M1 = new Matrix(n);
                Matrix M2 = new Matrix(n);
                Matrix M3 = new Matrix(n);
                Matrix M4 = new Matrix(n);
                Matrix M5 = new Matrix(n);
                Matrix M6 = new Matrix(n);
                M1.MultipleThreads(1);
                M2.MultipleThreads(1);
                M3.MultipleThreads(1);
                M4.MultipleThreads(1);
                M5.MultipleThreads(1);
                M6.MultipleThreads(1);

                Matrix result = (M1 + M2) * (M3 + M4) * (M5 + M6);

                var k = watch.ElapsedMilliseconds;
                time.Add(k);
                ns.Add(n);
                // Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms" + n);
            }

          
            for (int n = 2; n <= m; n++)
            {
                watch.Restart();
                watch.Start();
                Matrix M1 = new Matrix(n);
                Matrix M2 = new Matrix(n);
                Matrix M3 = new Matrix(n);
                Matrix M4 = new Matrix(n);
                Matrix M5 = new Matrix(n);
                Matrix M6 = new Matrix(n);
                M1.MultipleThreads(Environment.ProcessorCount);
                M2.MultipleThreads(Environment.ProcessorCount);
                M3.MultipleThreads(Environment.ProcessorCount);
                M4.MultipleThreads(Environment.ProcessorCount);
                M5.MultipleThreads(Environment.ProcessorCount);
                M6.MultipleThreads(Environment.ProcessorCount);

                Matrix result = (M1 + M2) * (M3 + M4) * (M5 + M6);

                var k = watch.ElapsedMilliseconds;
                time1.Add(k);
                ns1.Add(n);
                // Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms" + n);
            }

            
            int x = time1.Count;
            int i = 0;
            while (i < x)
            {
                this.chart1.Series[0].Points.AddXY(ns[i], time[i]);
                this.chart1.Series[1].Points.AddXY(ns1[i], time1[i]);

                i++;
            }


        }
    }
}
