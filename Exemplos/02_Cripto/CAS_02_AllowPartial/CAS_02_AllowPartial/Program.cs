using CAS_02;
using System;
using System.Reflection;
using System.Security.Policy;

namespace CAS_02_AllowPartial
{
    class Program
    {
        static void Main(string[] args)
        {
            InfoFromUnhosted();
            Console.ReadKey();
        }

        public static void InfoFromUnhosted()
        {
            //get the assembly zone evidence
            Zone z = Assembly.GetExecutingAssembly().Evidence.GetHostEvidence<Zone>();
            Console.WriteLine("Zone Evidence: " + z.SecurityZone.ToString() + "\n");

            Console.WriteLine(new AssemblyInfo().GetCasSecurityAttributes());
        }
    }
}
