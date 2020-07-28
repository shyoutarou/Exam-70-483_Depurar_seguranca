using System;

namespace ConvertClass
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = Convert.ToInt32(null);
            Console.WriteLine(i); // 0

            double d = 23.15;
            int inteiro = Convert.ToInt32(d);
            Console.WriteLine(inteiro); //23
            
            Console.ReadKey();
        }
    }
}
