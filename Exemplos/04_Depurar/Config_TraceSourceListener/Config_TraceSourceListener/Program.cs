using System.Diagnostics;

namespace Config_TraceSourceListener
{
class Program
{
    //Criado um TraceSource pelo nome definido no App.config
    public static TraceSource traceSource = new TraceSource("ConfigTraceSource");

    static void Main(string[] args)
    {
        traceSource.TraceEvent(TraceEventType.Warning, 0, "Some strange warning message");
        traceSource.TraceInformation("info message");
        traceSource.TraceEvent(TraceEventType.Error, 0, "Fatal error occured");
        traceSource.TraceEvent(TraceEventType.Verbose, 0, "Some debug message");
    }
}
}
