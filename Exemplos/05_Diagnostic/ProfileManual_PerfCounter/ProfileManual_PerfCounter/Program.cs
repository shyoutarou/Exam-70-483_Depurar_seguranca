using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ProfileManual_PerfCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!PerformanceCounterCategory.Exists("ShoppingCounter"))
            {
                CounterCreationDataCollection counters = new CounterCreationDataCollection();
                // *1.contador para contar totais(ShoppingDone): PerformanceCounterType.NumberOfItems32 * /
                CounterCreationData totalDone = new CounterCreationData();
                totalDone.CounterName = "ShoppingDone";
                totalDone.CounterHelp = "Número total de compras concluídas";
                totalDone.CounterType = PerformanceCounterType.NumberOfItems32;
                counters.Add(totalDone);

                // 2.contador para contar totais(ShoppingNotDone): PerformanceCounterType.NumberOfItems32
                CounterCreationData totalNotDone = new CounterCreationData();
                totalNotDone.CounterName = "ShoppingNotDone";
                totalNotDone.CounterHelp = "Número total de compras não concluídas";
                totalNotDone.CounterType = PerformanceCounterType.NumberOfItems32;
                counters.Add(totalNotDone);

                // cria uma nova categoria com os contadores acima
                var texto = "Os balcões de compras ajudam a montar quantas compras são feitas e como muitos não são.";
                PerformanceCounterCategory.Create("ShoppingCounter", texto,
                    PerformanceCounterCategoryType.SingleInstance, counters);
                Console.WriteLine("Contador de desempenho criado.");
            }
            else
                Console.WriteLine("Contador de desempenho já criado.");

            Console.ReadKey();
        }


        static void Main2(string[] args)
        {
            if (CreatePerformanceCounters())
            {
                Console.WriteLine("Created performance counters");
                Console.WriteLine("Please restart application");
                Console.ReadKey();
                return;
            }

            var totalOperationsCounter = new PerformanceCounter(
                "MyCategory", "# operations executed", "", false);
            var operationsPerSecondCounter = new PerformanceCounter(
                "MyCategory", "# operations / sec", "", false);

            totalOperationsCounter.Increment();
            operationsPerSecondCounter.Increment();
        }

        private static bool CreatePerformanceCounters()
        {
            if (!PerformanceCounterCategory.Exists("MyCategory"))
            {
                CounterCreationDataCollection counters = new CounterCreationDataCollection
        {
            new CounterCreationData(
                "# operations executed",
                "Total number of operations executed",
                PerformanceCounterType.NumberOfItems32),
            new CounterCreationData(
                "# operations / sec",
                "Number of operations executed per second",
                PerformanceCounterType.RateOfCountsPerSecond32)
        };
                PerformanceCounterCategory.Create(
                    "MyCategory", "Sample category for Codeproject",
                    PerformanceCounterCategoryType.SingleInstance,
                    counters);
                return true;
            }

            return false;
        }

    }
}
