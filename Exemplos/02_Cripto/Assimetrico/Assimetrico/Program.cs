using System;
using System.Security.Cryptography;
using System.Text;

namespace Assimetrico
{
    class Program
    {
        static void Main(string[] args)
        {
            // especifique os dados
            string containerName = "SecretContainer";
            CspParameters csp = new CspParameters() { KeyContainerName = containerName };

            CspParameters parameter = new CspParameters();
            parameter.KeyContainerName = "KeyContainer";

            //1.Crie um objeto de criptografia assimétrica da classe System.Security.Cryptography 
            using (RSACryptoServiceProvider asymmetricAlgo = new RSACryptoServiceProvider(parameter))
            {
                // salvando as informações principais na estrutura RSAParameters
                RSAParameters RSAKeyInfo = asymmetricAlgo.ExportParameters(false);
                // 2 - gerando as duas chaves (pública e privada)
                string publicKey = asymmetricAlgo.ToXmlString(false);
                string privateKey = asymmetricAlgo.ToXmlString(true);

                //Console.WriteLine(publicKey);
                //Console.WriteLine(privateKey);

                var assim_ocripto = new Criptografia_Assimetrica();

                var encryptedDataInBytes = assim_ocripto.CriptoAssimetrica(asymmetricAlgo, publicKey);
                assim_ocripto.DescriptoAssimetrica(asymmetricAlgo, encryptedDataInBytes, privateKey);

                // Display the key information to the console.  
                Console.WriteLine("Key retrieved from container : \n {0}", asymmetricAlgo.ToXmlString(true));

                //6.Limpe o objeto de criptografia assimétrica de todos os dados confidenciais chamando o método Clear e descarte o objeto.
                asymmetricAlgo.PersistKeyInCsp = false;
                asymmetricAlgo.Clear();
                Console.WriteLine("Key is Deleted");
            }

            Console.ReadKey();
        }

        class Criptografia_Assimetrica
        {
            public byte[] CriptoAssimetrica(RSACryptoServiceProvider asymmetricAlgo, string publicKey)
            {
                //Opção para coversão de texto para bytes
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                byte[] dataToEncrypt = ByteConverter.GetBytes("My Secret Data!");

                // Código de criptografia (no lado do remetente) dados para criptografar
                string dados = "Mensagem Secreta";

                // converte em bytes
                byte[] dataInBytes = Encoding.UTF8.GetBytes(dados);

                // 3 - Especifique a chave pública obtida do receptor
                asymmetricAlgo.FromXmlString(publicKey);

                // 4 - Use o método Encrypt para criptografia
                byte[] encryptedDataInBytes = asymmetricAlgo.Encrypt(dataInBytes, true);

                // coloca os bytes dos dados criptografados na string
                string encryptedData = Encoding.UTF8.GetString(encryptedDataInBytes);

                Console.WriteLine("\nAssimétrica criptografia: " + encryptedData);

                // 5.Envie os dados para o receptor.
                return encryptedDataInBytes;
            }

            public void DescriptoAssimetrica(RSACryptoServiceProvider asymmetricAlgo, byte[] encryptedDataInBytes, string privateKey)
            {
                //3.Obtenha os dados do remetente
                var dadosremetente = encryptedDataInBytes;

                // 4 - Especifique a chave privada
                asymmetricAlgo.FromXmlString(privateKey);

                // 5 - Use o método Decrypt para criptografia
                byte[] decryptedDataInBytes = asymmetricAlgo.Decrypt(encryptedDataInBytes, true);

                // coloca os bytes dos dados descriptografados na string
                string decryptedData = Encoding.UTF8.GetString(decryptedDataInBytes);

                //Assimétrica descriptografia: Mensagem Secreta
                Console.WriteLine("\nAssimétrica descriptografia: " + decryptedData);
            }
        }
    }
}
