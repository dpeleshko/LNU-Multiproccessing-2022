using System;
using System.Threading;

namespace ClassLibrary1
{
    public class Class1
    {
        static int SIZE;
        private static int[] array;

        public static void Main()
        {
            if (WriteSize())
            {
               CreateVector();
               Thread thread1 = new Thread(CheckFirstPart); 
               thread1.Start();
               Thread thread2 = new Thread(CheckSecondPart); 
               thread2.Start();
            }
            else
            {
                Main();
            }

           

        }

        public static void CheckSecondPart()
        {
            int result = 0;
            for (int i = array.Length/2; i < array.Length; i++)
            {
                result += array[i];
            }
            
            Console.WriteLine("Result from Second Part:"+result);
        }

        public static void CheckFirstPart()
        {
            int result = 1;
            for (int i = 0; i < array.Length/2; i++)
            {
                result *= array[i];
            }
            
            Console.WriteLine("Result from First Part:"+result);
        }

        public static void CreateVector()
        {
            array = new int[SIZE];
            Random rnd = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(1,10);
            }

            var str = string.Join(" ", array);
            Console.WriteLine("Vector: "+str);
        }

        public static bool WriteSize()
        {
            Console.WriteLine("Enter size for array(only even numbers): ");
            SIZE = Convert.ToInt32(Console.ReadLine());
            return SIZE % 2 == 0;
        }
    }
}
