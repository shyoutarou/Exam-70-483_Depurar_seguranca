using CasWriterDemo;
using System;
using System.Security;

[assembly: SecurityTransparent()]
namespace CasWriterTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            //CasWriter writer = new CasWriterInheritance();
            //Console.WriteLine(writer.GetMethodsSecurityStatus());

            CasWriter writer = new CasWriter();
            Console.WriteLine(writer.GetMethodsSecurityStatus());
            try
            {
                Console.Write("Custom Sentence: ");
                writer.WriteCustomSentence("Barba non facit philosophum");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n" + ex.Message + "\n\n");
            }
            try
            {
                Console.Write("Default Sentence: ");
                writer.WriteDefaultSentence(new Random().Next(0, 2));
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n" + ex.Message);
            }
            Console.ReadKey();
        }
    }
}
