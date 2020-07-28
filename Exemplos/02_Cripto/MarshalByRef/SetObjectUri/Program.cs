using System;
using System.Runtime.Remoting;
using System.Security.Permissions;

namespace SetObjectUri
{
    //Exemplo 2

    //O exemplo a seguir demonstra uma classe derivada de MarshalByRefObject 
    //que é usada posteriormente na comunicação remota.
    class TestClass : MarshalByRefObject
    {
    }

    class Program
    {
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
        static void Main(string[] args)
        {
            TestClass obj = new TestClass();

            RemotingServices.SetObjectUriForMarshal(obj, "testUri");
            RemotingServices.Marshal(obj);

            Console.WriteLine(RemotingServices.GetObjectUri(obj));
            Console.ReadLine();
        }
    }
}
