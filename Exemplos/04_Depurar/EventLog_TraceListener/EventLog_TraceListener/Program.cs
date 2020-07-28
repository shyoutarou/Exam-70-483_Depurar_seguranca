using System;
using System.Diagnostics;

namespace EventLog_TraceListener
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceName = "Sample Log";
            string logName = "Application";
            string machineName = ".";// . means local machine
                                     //Creation of log
            if (!EventLog.SourceExists(sourceName, machineName))
            {
                EventLog.CreateEventSource(sourceName, logName);//EventLog created
            }

            //Specifing created log (target)
            EventLog log = new EventLog(logName, machineName, sourceName);

            //specify the EventLog trace listener
            using (var eventLog_listener = new EventLogTraceListener())
            {
                //specify the target to listener
                eventLog_listener.EventLog = log;
                eventLog_listener.Name = "eventLog_listener";

                //specifing the Trace class, just to trace information
                TraceSource trace = new TraceSource("eventLog_SampleSource", SourceLevels.Information);
                //Clearing default listener
                trace.Listeners.Clear();
                //assigning new listener
                trace.Listeners.Add(eventLog_listener);
                //Start tracing

                // Test the filter on the ConsoleTraceListener.
                // Como foi setado na configuração do TraveSource  SourceLevels.Information
                // deveria bloquear a saída do TraceData....o que não ocorre
                trace.Listeners["eventLog_listener"].Filter = new SourceFilter("No match");
                trace.TraceData(TraceEventType.Error, 5,
                    "\nSourceFilter should reject this message for the eventLog trace listener.");
                trace.Listeners["eventLog_listener"].Filter = new SourceFilter("eventLog_SampleSource");
                trace.TraceData(TraceEventType.Error, 6,
                    "\nSourceFilter should let this message through on the eventLog trace listener.");

                trace.TraceInformation("Tracing start to Event Log");
                trace.Flush();
                trace.Close();
            }

            Console.WriteLine("Total entries: " + log.Entries.Count);
            EventLogEntry last = log.Entries[log.Entries.Count - 1];//last(latest) log com nome "Sample Log" 

            Console.WriteLine("Index: " + last.Index);
            Console.WriteLine("Source: " + last.Source);
            Console.WriteLine("Type: " + last.EntryType);
            Console.WriteLine("Time: " + last.TimeWritten);
            Console.WriteLine("Message: " + last.Message);
            Console.WriteLine("Machine Name: " + last.MachineName);
            Console.WriteLine("Category: " + last.Category);

            Console.ReadKey();
        }
    }
}
