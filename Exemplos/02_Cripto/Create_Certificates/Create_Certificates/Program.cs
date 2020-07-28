using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Create_Certificates
{
    class Program
    {
        static void Main(string[] args)
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            Console.WriteLine("Friendly Name\t\t\t\t\t Expiration date");
            foreach (X509Certificate2 certificate in store.Certificates)
            {
                Console.WriteLine("{0}\t{1}", certificate.FriendlyName, certificate.NotAfter);
            }
            store.Close();



            string textToSign = "Test paragraph";
            byte[] signature = Sign(textToSign);

            var hashedData = new StringBuilder();
            foreach (var item in signature)
            {
                hashedData.Append(item);
            }

            Console.WriteLine(textToSign + " em hash = " + hashedData.ToString());

            // Uncomment this line to make the verification step fail
            // signature[0] = 0;
            var verifica = Verify(textToSign, signature);

            Console.WriteLine(verifica);
            Console.ReadKey();
        }

        public static byte[] Sign(string text)
        {
            X509Certificate2 cert = GetCertificate();
            var csp = (RSACryptoServiceProvider)cert.PrivateKey;
            byte[] hash = HashData(text);
            return csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
        }

        static bool Verify(string text, byte[] signature)
        {
            X509Certificate2 cert = GetCertificate();
            var csp = (RSACryptoServiceProvider)cert.PublicKey.Key;
            byte[] hash = HashData(text);
            return csp.VerifyHash(hash,
            CryptoConfig.MapNameToOID("SHA1"),
            signature);
        }

        private static byte[] HashData(string text)
        {
            HashAlgorithm hashAlgorithm = new SHA1Managed();
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] data = encoding.GetBytes(text);
            byte[] hash = hashAlgorithm.ComputeHash(data);
            return hash;
        }

        private static X509Certificate2 GetCertificate()
        {
            X509Store my = new X509Store("myCertStore", StoreLocation.CurrentUser);
            my.Open(OpenFlags.ReadOnly);
            var certificate = my.Certificates[0];
            return certificate;
        }
    }
}
