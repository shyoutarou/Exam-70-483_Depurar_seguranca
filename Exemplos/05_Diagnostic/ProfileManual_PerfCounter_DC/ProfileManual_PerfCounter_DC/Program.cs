using System;
using System.Diagnostics;

namespace ProfileManual_PerfCounter_DC
{
    class Program
    {
        static void Main(string[] args)
        {
            int noOfShoppingsDone = 1500;
            int noOfShoppingsNotDone = 20000;

            // successfully Done shpping (Counter)
            using (PerformanceCounter successfullCounter = new PerformanceCounter("ShoppingCounter", "ShoppingDone"))
            {
                successfullCounter.MachineName = ".";
                successfullCounter.ReadOnly = false;

                for (int i = 0; i < noOfShoppingsDone; i++)
                {
                    Console.WriteLine("Shopping Done Successfully..");
                    successfullCounter.Increment();
                }
            }

            using (PerformanceCounter NotsuccessfullCounter = new PerformanceCounter())
            {
                NotsuccessfullCounter.CategoryName = "ShoppingCounter";
                NotsuccessfullCounter.CounterName = "ShoppingNotDone";
                NotsuccessfullCounter.MachineName = ".";
                NotsuccessfullCounter.ReadOnly = false;

                for (int i = 0; i < noOfShoppingsNotDone; i++)
                {
                    Console.WriteLine("Shoppings Not Done..");
                    NotsuccessfullCounter.Increment();
                }
            }

            //Console.ReadKey();
        }
    }
}
