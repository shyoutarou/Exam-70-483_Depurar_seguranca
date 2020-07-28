using System;
using System.Diagnostics;
using System.Text;

namespace ProfileManual_StopWatch
{
    class Program
    {
        const int numberOfIterations = 100000;
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Inicio: " + sw.Elapsed);
            Algorithm1();
            sw.Stop();
            Console.WriteLine("Fim Algorithm1: " + sw.Elapsed);
            sw.Reset();
            sw.Start();
            Console.WriteLine("Inicio 2: " + sw.Elapsed);
            Algorithm2();
            sw.Stop();
            Console.WriteLine("Fim Algorithm2: " + sw.Elapsed);
            Console.ReadLine();
        }

        private static void Algorithm1()
        {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < numberOfIterations; x++)
            {
                sb.Append('a');
            }
            string result = sb.ToString();
        }

        private static void Algorithm2()
        {
            string result = "";
            for (int x = 0; x < numberOfIterations; x++)
            {
                result += 'a';
            }
        }
    }
}
