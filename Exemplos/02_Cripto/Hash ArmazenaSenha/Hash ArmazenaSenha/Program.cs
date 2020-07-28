using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hash_ArmazenaSenha
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite uma senha para o hash ...");
            // Obtem senha e armazena em pw
            string pw = Console.ReadLine();
            Console.WriteLine();

            // Cria valor hash da senha 
            string pwh = CreateHash_InBytes(pw);
            string pwh_64 = CreateHash_ToBase64(pw);

            // Exibir valor hash da senha 
            Console.WriteLine("The hash value for " + pw + " InBytes: " + pwh);
            Console.WriteLine("The hash value for " + pw + " ToBase64: " + pwh_64);

            //Poderíamos armazenar valor hash da senha em um banco de dados...
            //e usá-la novamente para autenticar o usuário, comparando-a com
            //a senha digitada quando o usuário tentar efetuar o login novamente
            Console.WriteLine();

            // Obtem a senha do usuário novamente
            Console.WriteLine("Digite a senha novamente para comparar os hashes ...");
            string pw2 = Console.ReadLine();

            // Hash a segunda senha e compare as duas
            string pwh2 = CreateHash_InBytes(pw2);
            Console.WriteLine();
            Console.WriteLine("Primeiro hash : " + pwh);
            Console.WriteLine("Segundo hash: " + pwh2);

            if (pwh == pwh2)
            {
                Console.WriteLine("Hashes iguais.");
            }
            else
            {
                Console.WriteLine("Hashes diferentes.");
            }

            Console.ReadKey();
        }

        public static string CreateHash_InBytes(string input)
        {
            // senha em bytes
            var passwordInBytes = Encoding.UTF8.GetBytes(input);

            // Crie o objeto SHA256
            HashAlgorithm sha = SHA256.Create();

            byte[] hashInBytes = sha.ComputeHash(passwordInBytes);

            var hashedData = new StringBuilder();
            foreach (var item in hashInBytes)
            {
                hashedData.Append(item);
            }

            return hashedData.ToString();
        }

        public static string CreateHash_ToBase64(string input)
        {
            HashAlgorithm sha = SHA256.Create();
            byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(input));
            return Convert.ToBase64String(hashData);
        }
    }
}
