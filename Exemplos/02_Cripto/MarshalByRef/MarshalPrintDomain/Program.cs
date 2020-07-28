using System;

//namespace MarshalPrintDomain
//{
public class Worker : MarshalByRefObject
{
    public void PrintDomain()
    {
        Console.WriteLine("Object is executing in AppDomain \"{0}\"",
            AppDomain.CurrentDomain.FriendlyName);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create an ordinary instance in the current AppDomain
        Worker localWorker = new Worker();
        localWorker.PrintDomain();

        // Create a new application domain, create an instance of Worker in the 
        // application domain, and execute code there.
        // Nota: O exemplo e Worker não podem estar no mesmo namespace
        // Se sim, o código a seguir lançará uma exceção:
        // Não foi possível carregar o tipo 'Worker' do assembly 
        // Por isso foi comentado o namespace da aplicação
        AppDomain ad = AppDomain.CreateDomain("ByRef domain");
        Worker remoteWorker = (Worker)ad.CreateInstanceAndUnwrap(
        typeof(Worker).Assembly.FullName,
        typeof(Worker).Name);
        remoteWorker.PrintDomain();

        Console.ReadKey();
    }
}
//}
