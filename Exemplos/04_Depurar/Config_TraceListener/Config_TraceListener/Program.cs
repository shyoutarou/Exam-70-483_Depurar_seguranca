using System;
using System.Diagnostics;

namespace Config_TraceListener
{
    class Program
    {
        static void Main(string[] args)
        {
            //TextWriterTraceListener myListener = new TextWriterTraceListener("TextWriterOutput.log", "myListener");
            //myListener.WriteLine("Test Config SEM ADD Listener message.");
            //myListener.Flush();
            //Console.ReadKey();
            Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "myListener"));
            Trace.TraceInformation("Test Config Listener LOG message.");
            Trace.Flush();
            Console.ReadKey();
        }
    }
}
