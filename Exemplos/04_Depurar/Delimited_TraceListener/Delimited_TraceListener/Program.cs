using System;
using System.Diagnostics;
using System.IO;

namespace Delimited_TraceListener
{
    class Program
    {
static void Main(string[] args)
{
    using (var delimited_listener = new DelimitedListTraceListener("test.txt"))
    {
        var mySource = new TraceSource("Testsource", SourceLevels.All);
        // limpa o ouvinte padrão
        mySource.Listeners.Clear();

        delimited_listener.Name = "delimited_listener";
        delimited_listener.Delimiter = ",";
        mySource.Listeners.Add(delimited_listener);

        //mySource.Listeners["delimited_listener"].Filter = new SourceFilter("No match");
        //mySource.Listeners["delimited_listener"].Filter = new SourceFilter("Testsource");

        mySource.TraceData(TraceEventType.Information, 1, "x");
        mySource.TraceInformation("y");

        mySource.TraceEvent(TraceEventType.Error, 2, "z");
        mySource.TraceInformation("w");
        mySource.Flush();
        mySource.Close();

        //No modo Debug Imprime as 2 mensagens na janela Output
        //No modo Release Imprime somente a mensagm do Trace
        Debug.Listeners.Add(delimited_listener);
        Debug.WriteLine("Saida DelimitedListTraceListener no Debug");
        Debug.Flush();

        Trace.Listeners.Add(delimited_listener);
        Trace.WriteLine("Saida DelimitedListTraceListener no Trace");
        Trace.Flush();
    }

    FileStream stream = new FileStream("test_stream.txt", FileMode.Create, FileAccess.Write); // = FileStream.Null;
    StreamWriter sw = new StreamWriter(stream);
    
    using (var stream_delimited_listener = new DelimitedListTraceListener(sw))
    {
        TraceEventCache cc = new TraceEventCache();

        stream_delimited_listener.Delimiter = ":";
        stream_delimited_listener.TraceData(cc, null, TraceEventType.Error, 7, "XYZ");
        stream_delimited_listener.TraceData(cc, null, TraceEventType.Error, 7, "ABC", "DEF", 123);
        stream_delimited_listener.TraceEvent(cc, null, TraceEventType.Error, 4, null);

        stream_delimited_listener.TraceOutputOptions = TraceOptions.ProcessId | TraceOptions.ThreadId | TraceOptions.DateTime | TraceOptions.Timestamp;

        stream_delimited_listener.TraceData(cc, null, TraceEventType.Information, 1, "x");
        stream_delimited_listener.TraceEvent(cc, null, TraceEventType.Error, 4, null);
    }

    Console.ReadKey();
}
    }
}
