using System;
using System.Diagnostics;
using System.IO;

namespace Console_TraceListener
{
    class Program
    {
static void Main(string[] args)
{
    Console.WriteLine("Hello World!");

    using (var console_listener = new ConsoleTraceListener())
    {
        //// limpa o ouvinte trace padrão
        Trace.Listeners.RemoveAt(0);

        //// adicionando o ouvinte
        Trace.Listeners.Add(console_listener);

        if (args.Length >= 1)
        {
            string failMessage = String.Format("\"{0}\" " +
                        "is not a valid number of possibilities.", args[0]);
            console_listener.Fail(failMessage, "erro message");
        }
        else
        {
            // Report that the required argument is not present.
            const string ENTER_PARAM = "Enter the number of " +
                        "possibilities as a command line argument.";
            console_listener.Fail(ENTER_PARAM);
        }


        // especifique a fonte de rastreamento
        TraceSource ts = new TraceSource("ConsoleTraceSource", SourceLevels.All);

        console_listener.Name = "console_listener";
        // limpa o ouvinte padrão
        ts.Listeners.Clear();
        // adicionando o ouvinte
        ts.Listeners.Add(console_listener);
        // rastreando as informações / problemas que entrarão no ouvinte adicionado
        ts.TraceInformation("Rastreio TraceSource a aplicação..");

        // Test the filter on the ConsoleTraceListener.
        ts.Listeners["console_listener"].Filter = new SourceFilter("No match");
        ts.TraceData(TraceEventType.Error, 5,
            "\nSourceFilter should reject this message for the console trace listener.");
        ts.Listeners["console_listener"].Filter = new SourceFilter("ConsoleTraceSource");
        ts.TraceData(TraceEventType.Error, 6,
            "\nSourceFilter should let this message through on the console trace listener.");

        ts.TraceData(TraceEventType.Error, 1, new string[] { "Erro1", "Erro2" });
        ts.Flush();
        ts.Close();
    }

    Console.ReadKey();
}
    }
}
