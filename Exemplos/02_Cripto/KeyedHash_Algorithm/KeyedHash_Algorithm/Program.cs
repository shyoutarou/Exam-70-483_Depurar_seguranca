using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KeyedHash_Algorithm
{
    class Program
    {
        static private void PrintByteArray(Byte[] arr)
        {
            int i;
            Console.WriteLine("Length: " + arr.Length);
            for (i = 0; i < arr.Length; i++)
            {
                Console.Write("{0:X}", arr[i]);
                Console.Write("    ");
                if ((i + 9) % 8 == 0) Console.WriteLine();
            }
            if (i % 8 != 0) Console.WriteLine();
        }

        static void Main(string[] args)
        {
            // Create a key.
            byte[] key1 = { 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b };
            // Pass the key to the constructor of the HMACMD5 class.
            HMACMD5 hmac1 = new HMACMD5(key1);

            // Create another key.
            byte[] key2 = System.Text.Encoding.ASCII.GetBytes("KeyString");
            // Pass the key to the constructor of the HMACMD5 class.
            HMACMD5 hmac2 = new HMACMD5(key2);

            // Encode a string into a byte array, create a hash of the array,
            // and print the hash to the screen.
            byte[] data1 = System.Text.Encoding.ASCII.GetBytes("Hi There");
            PrintByteArray(hmac1.ComputeHash(data1));

            // Encode a string into a byte array, create a hash of the array,
            // and print the hash to the screen.
            byte[] data2 = System.Text.Encoding.ASCII.GetBytes("This data will be hashed.");
            PrintByteArray(hmac2.ComputeHash(data2));

            Console.ReadKey();
        }
    }
}
