#if PROD
#undef DEBUG
#undef TRACE
#endif
#if TEST
#undef TRACE
#endif
#if DEV
#define DEBUG
#define TRACE
#endif

#if !PROD && !TEST && !DEV
#define MyMessage
#else
#undef MyMessage
#warning Running with hard coded Tier information
#endif

using System;
using System.Diagnostics;
using System.Reflection;

namespace Diretiva_Precompilador
{
    //#line 1 "Warning line.cs" //Se ativar essa linha o breakpoint não funciona mais
    class Program
    {
        public static void Main(string[] args)
        {
            //#if MyMessage
            //            System.Console.WriteLine("Cannot compile as the Tier is not specified");
            //#endif

            //#if PROD
            //            System.Console.WriteLine("Target is PROD!");          
            //#elif TEST
            //            System.Console.WriteLine("Target is TEST");
            //#elif DEV
            //            System.Console.WriteLine("Target is DEV");
            //#endif
            //#if DEBUG
            //            System.Console.WriteLine("DEBUG is ENABLED!");
            //#else
            //            System.Console.WriteLine("DEBUG is DISABLED!");
            //#endif
            //#if TRACE
            //            System.Console.WriteLine("TRACE is ENABLED!");
            //#else
            //            System.Console.WriteLine("TRACE is DISABLED!");
            //#endif

            //diferenças entre o WinRT e o .NET 4.5. no .NET 4.5
#if !WINRT
            Assembly assembly = typeof(int).Assembly;
            Console.WriteLine("!WINRT typeof(int).Assembly: " + assembly);
#else
            Assembly assembly = typeof(int).GetTypeInfo().Assembly;
             Console.WriteLine("!WINRT typeof(int).Assembly: " + assembly);
#endif



            Console.WriteLine("Default/Normal Line No");
#line 100 //Pula para linha 100 na depuração
            Console.WriteLine("Override Line No #line 100 = Pula para linha 100 na depuração");
#line hidden // Ignora a próxima linha na depuração, mas imprime no console
            Console.WriteLine("Hidden Line No #line hidden = Ignora a próxima linha na depuração, mas imprime no console");
#line default
            Console.WriteLine("Default/Noraml Line No #line default");


            //#warning Warning from different filename

            //#line 200 "OtherFileName"
            //            int a; // line 200
            //#line default
            //            int b; // line 66
            //#line hidden
            //            int c; // hidden
            //            int d; // line 69

#pragma warning disable
            int k; // Variavel não utilizada
#pragma warning restore

            int j; // Variavel não utilizada

#pragma warning disable 0162, 0168
            int i; // Variavel não utilizada
#pragma warning restore 0162

            while (false)
            {
                Console.WriteLine("Unreachable code");
            }

#pragma warning restore

            Log("Step1");

            var person = new Person() { FirstName = "Diane", LastName = "Birch" };
            Console.WriteLine(person);

            Console.ReadLine();
        }

        [Conditional("DEBUG")]
        private static void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    [DebuggerDisplay("Name = {FirstName} {LastName}")]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
