using System;
using System.Diagnostics;

namespace Logging_tracing
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                //Debug.WriteLine("Starting application");
                //int age = 10;
                //Debug.WriteLineIf(age.GetType() == typeof(int), "Age is Valid");
                //for (int k = 0; k < 5; k++)
                //{
                //    Debug.WriteLine("Loop executed Successfully");
                //}

                //Debug.Indent();
                //int i = 1 + 2;
                //Debug.Assert(i == 3);
                //Debug.WriteLineIf(i > 0, "i is greater than 0");
                //Debug.Print("Tracing Finished");

                Trace.WriteLine("Tracing Start:Numbers must be Int");
                int num1 = 10;
                int num2 = 0;

                Trace.WriteLineIf(num1.GetType() == typeof(int) &&
                                    num2.GetType() == typeof(int), "Numbers are valid");
                if (num2 < 1)
                {
                    num2 = num1;
                    Trace.TraceInformation("num2 has been changed due to zero value");
                }

                int result = num1 / num2;
                Trace.Indent();

                TraceSource traceSource = new TraceSource("myTraceSource", SourceLevels.All);

                traceSource.TraceInformation("Tracing application..");
                traceSource.TraceEvent(TraceEventType.Critical, 0, "Critical trace");
                traceSource.TraceEvent(TraceEventType.Error, 0, "Error trace Event");
                traceSource.TraceData(TraceEventType.Information, 1, new object[] { "a", "b", "c" });
                traceSource.Flush();
                traceSource.Close();

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                //Debug.Assert(false);
                Trace.Assert(false);
                Trace.TraceError(ex.Message);
            }
        }
    }
}
