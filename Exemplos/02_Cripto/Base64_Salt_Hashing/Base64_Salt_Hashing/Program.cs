using System;
using System.Security.Cryptography;
using System.Text;

namespace Base64_Salt_Hashing
{
    class Program
    {
        static void Main(string[] args)
        {
            Create_SaltHash();

            Console.ReadKey();
        }

        public static void Create_SaltHash()
        {
            // senha a ser hash
            string password = "HelloWorld";
            // gera Salt (GUID é um identificador uniqe globalmente)
            Guid salt = Guid.NewGuid();
            // Mescla senha com valor Salt
            string saltedPassword = password + salt;
            // senha em bytes
            var passwordInBytes = Encoding.UTF8.GetBytes(saltedPassword);
            // Crie o objeto SHA512
            HashAlgorithm sha512 = SHA512.Create();
            // gera o hash
            byte[] hashInBytes = sha512.ComputeHash(passwordInBytes);
            var hashedData = new StringBuilder();
            foreach (var item in hashInBytes)
            {
                hashedData.Append(item);
            }

            HashAlgorithm sha = SHA256.Create();
            byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(saltedPassword));
            string hashedData_64 = Convert.ToBase64String(hashData);

            Console.WriteLine("A senha Salt Hash InBytes é:" + hashedData.ToString());
            Console.WriteLine("A senha Salt Hash ToBase64 é:" + hashedData_64);
        }
    }
}
