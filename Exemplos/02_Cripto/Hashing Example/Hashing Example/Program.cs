using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hashing_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Hash e compare 
            CompareHash_InBytes();

            var input = "Teste";
            var Base64Hash = ComputeHash("Teste");
            var valida = VerifyHash_Base64(input, Base64Hash);

            Console.ReadKey();
        }

        public static void CompareHash_InBytes()
        {
            string data = "A paragraph of text";
            //Opção de criar bytes apartir de texto
            byte[] passwordInBytes = Encoding.UTF8.GetBytes(data);

            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] input = byteConverter.GetBytes(data);

            //OU SHA256 sha256 = SHA256.Create();
            HashAlgorithm sha256 = SHA256.Create();

            byte[] hashA = sha256.ComputeHash(byteConverter.GetBytes(data));

            data = "A paragraph of changed text";
            byte[] hashB = sha256.ComputeHash(byteConverter.GetBytes(data));

            data = "A paragraph of text";

            byte[] hashC = sha256.ComputeHash(byteConverter.GetBytes(data));

            Console.WriteLine(hashA.SequenceEqual(hashB)); // Displays: false
            Console.WriteLine(hashA.SequenceEqual(hashC)); // Displays: true
        }

        static string ComputeHash(string input)
        {
            //1.Crie um objeto de algoritmo de hash.
            HashAlgorithm hmac;

            //2.Defina a chave de hash se o algoritmo usado for um com chave.
            byte[] key1 = { 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b };
            hmac = new HMACSHA256(key1);

            //3.Chame o método ComputeHash.
            byte[] hashData = hmac.ComputeHash(Encoding.Default.GetBytes(input));

            //4.Salve o hash dos dados.
            return Convert.ToBase64String(hashData);
        }

        static bool VerifyHash_Base64(string input, string Base64Hash)
        {
            //1.Crie um objeto de algoritmo de hash usando o mesmo algoritmo 
            //usado para fazer hash dos dados.
            HashAlgorithm hmac;

            //2.Se um algoritmo com chave de hash foi usado, 
            //defina a chave com o mesmo valor usado para o hash.
            byte[] key1 = { 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b };
            hmac = new HMACSHA256(key1);

            //3.Extraia o hash original dos dados.
            var hashData = Base64Hash;

            //4.Chame o método ComputeHash.
            byte[] hashinput = hmac.ComputeHash(Encoding.Default.GetBytes(input));

            //5.Compare o hash extraído com o calculado. Se forem iguais, 
            //significa que os dados não foram alterados.
            return Convert.ToBase64String(hashinput) == hashData;
        }

    }

}
