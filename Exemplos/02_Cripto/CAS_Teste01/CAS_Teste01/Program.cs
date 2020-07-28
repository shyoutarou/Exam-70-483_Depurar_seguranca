using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CAS_Teste01
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load another assembly
            Assembly asb = Assembly.LoadFrom(@"C:\Users\RICARDO\Documents\Code_Estudos\01_Certificado_70-483\04_Depurar_segurança\Exemplos\Chapter 16\Parse\Parse\bin\Debug\Parse.exe");

            Console.WriteLine("***** Evidence Viewer *****\n");

            if (asb != null)
            {
                DisplayEvidence(asb);
            }
            Console.ReadLine();
        }

        private static void DisplayEvidence(Assembly asm)
        {
            // Get evidence collection using enumerator.
            Evidence e = asm.Evidence;
            IEnumerator obj = e.GetHostEnumerator();
            // Now print out the evidence.
            while (obj.MoveNext())
            {
                Console.WriteLine(" **** Press Enter to continue ****");
                Console.ReadLine();
                Console.WriteLine(obj.Current);
            }
        }
    }
}
