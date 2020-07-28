using System;
using System.Runtime.Remoting;
using System.Security.Permissions;

//namespace MarshalByRef
//{
//Exemplos

//Esta seção contém dois exemplos de código.O primeiro exemplo de 
//    mostra como criar uma instância de uma classe em outro domínio de 
//    aplicativo.O segundo exemplo de código mostra uma classe simples 
//    que pode ser usada para comunicação remota.

//Exemplo 1

//O exemplo de código a seguir mostra a maneira mais simples de executar 
//    o código em outro domínio de aplicativo. O exemplo define uma classe 
//    chamada Worker que herda MarshalByRefObject, com um método que exibe 
//    o nome do domínio do aplicativo no qual está sendo executado. 
//    O exemplo cria instâncias de Worker no domínio de aplicativo padrão 
//    e em um novo domínio de aplicativo.
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

        // Create a new application domain, create an instance
        // of Worker in the application domain, and execute code
        // there.
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
        Console.ReadLine();
    }
}
//}
