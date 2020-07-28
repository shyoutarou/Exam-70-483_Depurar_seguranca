using System;
using System.Diagnostics;
using System.Threading;

namespace Debugging
{
    class Program
    {
        public static void Main(string[] args)
        {
            Timer t = new Timer(TimerCallback, null, 0, 2000);

#if DEBUG
            Debug.Write("Condition is True\n");
            Console.WriteLine("modo Debug");
#else
        Debug.Write("Condition is False\n");
        Console.WriteLine ("modo Release");
#endif

            Console.ReadLine();
        }

        private static void TimerCallback(object o)
        {
            Console.WriteLine("In TimerCallback:" + DateTime.Now);
            GC.Collect();
        }
    }
}
