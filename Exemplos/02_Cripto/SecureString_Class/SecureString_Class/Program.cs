using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SecureString_Class
{
    class Program
    {
        static void Main(string[] args)
        {
            var secureString = InputSecureString();

            ConvertToUnsecureString(secureString);
        }

        public static SecureString InputSecureString()
        {
            using (SecureString secureString = new SecureString())
            {
                Console.Write("Please enter your password/Credit Card Number: ");
                while (true)
                {
                    ConsoleKeyInfo enteredKey = Console.ReadKey(true);
                    if (enteredKey.Key == ConsoleKey.Enter)
                        break;
                    secureString.AppendChar(enteredKey.KeyChar);
                    Console.Write("#");
                }
                secureString.MakeReadOnly();

                return secureString;
            }
        }

        public static void ConvertToUnsecureString(SecureString secureString)
        {
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                Console.WriteLine(Marshal.PtrToStringUni(unmanagedString));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
                Console.WriteLine("Memory Cleared.");
            }
        }
    }
}
