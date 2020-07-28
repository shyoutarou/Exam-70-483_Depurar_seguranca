using System;
using System.Security.Cryptography;
using System.Text;

namespace Classe_ProtectedData
{
    class Program
    {
        //public static byte[] Protect( byte[] userData, byte[] optionalEntropy, DataProtectionScope scope)
        //public static byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy, DataProtectionScope scope)


        static void Main(string[] args)
        {
            var protect_ocripto = new Criptografia_ProtectedData();

            var cipherDataInBytes = protect_ocripto.EncryptProtectedData();
            protect_ocripto.DecryptProtectedData(cipherDataInBytes);

            Console.ReadKey();
        }

        class Criptografia_ProtectedData
        {
            public byte[] EncryptProtectedData()
            {
                string message = "Olá, mundo";
                // Converte dados em uma matriz de bytes
                byte[] userData = Encoding.UTF8.GetBytes(message);
                // criptografa os dados usando o método ProtectedData.Protect
                byte[] encryptedDataInBytes = ProtectedData.Protect(userData, null, DataProtectionScope.CurrentUser);
                string encryptedData = Encoding.UTF8.GetString(encryptedDataInBytes);
                Console.WriteLine("Criptografados por ProtectedData:" + encryptedData);
                return encryptedDataInBytes;
            }

            public void DecryptProtectedData(byte[] encryptedDataInBytes)
            {
                byte[] decryptedDataInBytes = ProtectedData.Unprotect(encryptedDataInBytes, null,
                DataProtectionScope.CurrentUser);
                string decryptedData = Encoding.UTF8.GetString(decryptedDataInBytes);
                Console.WriteLine("DeCriptografados por ProtectedData: " + decryptedData);
            }
        }
    }
}
