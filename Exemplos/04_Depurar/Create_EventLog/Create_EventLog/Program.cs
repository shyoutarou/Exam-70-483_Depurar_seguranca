using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Create_EventLog
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceName = "Sample Log";
            string logName = "Application";
            string machineName = ".";// . means local machine
            string entryTowritten = "Some random entry into Event Log";


            if (!EventLog.SourceExists(sourceName, machineName))
            {
                EventLog.CreateEventSource(sourceName, logName);
            }
            else
            {
                foreach (var app_log in EventLog.GetEventLogs().Where(x => x.Log == "Application"))
                {
                    foreach (var entries in app_log.Entries)
                    {
                        var evento = (entries as EventLogEntry);

                        if (evento.Source == "Sample Log")
                            Console.WriteLine("Evento[" + evento.Index + "]" +
                            string.Format("{0} {1} {2} {3} {4} {5} {6}", evento.EntryType,
                               evento.TimeGenerated, evento.Source, evento.Category, 
                               evento.InstanceId, evento.UserName, evento.Message));
                    }
                }
            }

            EventLog.WriteEntry(sourceName, entryTowritten, EventLogEntryType.Information);

            EventLog log = new EventLog(logName, machineName, sourceName);
           
            Console.WriteLine("Total entries: " + log.Entries.Count);
            //last(latest) log com nome "Sample Log" 
            EventLogEntry last = log.Entries[log.Entries.Count - 1];

            Console.WriteLine("Index: " + last.Index);
            Console.WriteLine("Source: " + last.Source);
            Console.WriteLine("Type: " + last.EntryType);
            Console.WriteLine("Time: " + last.TimeWritten);
            Console.WriteLine("Message: " + last.Message);
            Console.WriteLine("Machine Name: " + last.MachineName);
            Console.WriteLine("Category: " + last.Category);

            log.EntryWritten += (sender, e) =>
            {
                Console.WriteLine(e.Entry.Message);
            };
            log.EnableRaisingEvents = true;
            log.WriteEntry("Test message", EventLogEntryType.Information);

            //log.Clear();
            Console.ReadKey();
        }
    }
}
