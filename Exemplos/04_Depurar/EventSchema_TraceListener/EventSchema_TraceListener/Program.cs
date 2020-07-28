#define NOCONFIGFILE
using System;
using System.Diagnostics;
using System.IO;

namespace EventSchema_TraceListener
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            string fileName = "TraceOutput.xml";
            string event_name = "eventschema_listener";

            File.Delete(fileName);

            //specify the EventSchema trace listener
            using (var eventschema_listener = new EventSchemaTraceListener(fileName, event_name))
            {
                TraceSource ts = new TraceSource("TestSource");
#if NOCONFIGFILE
                //ts.Listeners.Add(new EventSchemaTraceListener(fileName, event_name, 65536, TraceLogRetentionOption.LimitedCircularFiles, 20480000, 2));
                ts.Listeners.Add(eventschema_listener);
                ts.Listeners[event_name].TraceOutputOptions = TraceOptions.DateTime | TraceOptions.ProcessId | TraceOptions.Timestamp;
#endif
                ts.Switch.Level = SourceLevels.All;
                string testString = "<Test><InnerElement Val=\"1\" /><InnerElement Val=\"Data\"/><AnotherElement>11</AnotherElement></Test>";
                UnescapedXmlDiagnosticData unXData = new UnescapedXmlDiagnosticData(testString);
                ts.TraceData(TraceEventType.Error, 38, unXData);
                ts.TraceEvent(TraceEventType.Error, 38, testString);

                Trace.Listeners.Add(eventschema_listener);
                Trace.Write("test eventschema", "test");
                Trace.Flush();
                ts.Flush();
                ts.Close();
                DisplayProperties(ts, event_name);
                Process.Start("notepad++.exe", "TraceOutput.xml");

                Console.ReadLine();
            }
        }

        private static void DisplayProperties(TraceSource ts, string event_name)
        {
            Console.WriteLine("IsThreadSafe? " + ((EventSchemaTraceListener)ts.Listeners[event_name]).IsThreadSafe);
            Console.WriteLine("BufferSize =  " + ((EventSchemaTraceListener)ts.Listeners[event_name]).BufferSize);
            Console.WriteLine("MaximumFileSize =  " + ((EventSchemaTraceListener)ts.Listeners[event_name]).MaximumFileSize);
            Console.WriteLine("MaximumNumberOfFiles =  " + ((EventSchemaTraceListener)ts.Listeners[event_name]).MaximumNumberOfFiles);
            Console.WriteLine("Name =  " + ((EventSchemaTraceListener)ts.Listeners[event_name]).Name);
            Console.WriteLine("TraceLogRetentionOption =  " + ((EventSchemaTraceListener)ts.Listeners[event_name]).TraceLogRetentionOption);
            Console.WriteLine("TraceOutputOptions =  " + ((EventSchemaTraceListener)ts.Listeners[event_name]).TraceOutputOptions);
        }
    }
}
