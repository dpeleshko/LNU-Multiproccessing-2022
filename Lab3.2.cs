using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Numerics;

namespace Lb3Part2_Forms
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int matrixSize = 100;
            Graphics m_graph = pictureBox1.CreateGraphics();
            int step = pictureBox1.Size.Width / Matrix.threadsNum;
            Pen m_pen = new Pen(Color.Red, 3f);
            Matrix M1 = new Matrix(matrixSize),
                M2 = new Matrix(matrixSize),
                M3 = new Matrix(matrixSize),
                M4 = new Matrix(matrixSize),
                M5 = new Matrix(matrixSize),
                M6 = new Matrix(matrixSize);

            Matrix[] ms = { M1, M2, M3, M4, M5, M5, M6};
            foreach(Matrix m in ms)
            {
                m.fillRandom();
            }

            Point[] points = new Point[Matrix.threadsNum];

            for (int i = 0; i < 8; ++i)
            {
                Matrix.threadsNum = i + 1;
                var start = DateTime.Now;
                var mm = (M1 + M2) * (M3 + M4) + M5 * M6;
                var end = DateTime.Now;
                points[i] = new Point(i * step , (end - start).Milliseconds);
                
            }
            m_graph.DrawLines(m_pen, points);
        }


    }
}
