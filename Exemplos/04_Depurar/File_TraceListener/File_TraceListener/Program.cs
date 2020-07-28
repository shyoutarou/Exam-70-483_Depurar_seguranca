using System.Diagnostics;
using System.IO;

namespace File_TraceListener
{
    class Program
    {
        static void Main(string[] args)
        {
            TraceSource traceSource = new TraceSource("myTraceSource", SourceLevels.All);

            //OU Stream file = new FileStream("TraceFile.txt", FileMode.Append);
            Stream outputFile = File.Create("tracefile.txt");
            TextWriterTraceListener textListener = new TextWriterTraceListener(outputFile);
            traceSource.Listeners.Clear();
            traceSource.Listeners.Add(textListener);

            traceSource.TraceInformation("Config_TraceListener application..");
            traceSource.TraceEvent(TraceEventType.Critical, 0, "Critical trace");
            traceSource.TraceData(TraceEventType.Information, 1, new object[] { "a", "b", "c" });
            traceSource.TraceData(TraceEventType.Error, 1, new string[] { "Error1", "Error2" });
            traceSource.Flush();
            traceSource.Close();
        }
    }
}
