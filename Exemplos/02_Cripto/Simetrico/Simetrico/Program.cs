using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Simetrico
{
    class Program
    {
        static void Main(string[] args)
        {
            string original = "My secret data!";
            using (SymmetricAlgorithm symmetricAlgorithm = new AesManaged())
            {
                byte[] encrypted = Encrypt(symmetricAlgorithm, original);
                string roundtrip = Decrypt(symmetricAlgorithm, encrypted);

                Console.WriteLine("Original: {0}", original);
                Console.WriteLine("Round Trip: {0}", roundtrip);
            }

            var sim_cripto = new Criptografia_Simetrica();

            // 1.Crie um objeto de criptografia chamando o método Create da classe SymmetricAlgorithm, 
            // configurando o parâmetro opcional string para o nome do algoritmo desejado.
            SymmetricAlgorithm symmetricAlgo = SymmetricAlgorithm.Create("AES");

            // 2.	Se desejar, você pode definir uma chave e um IV, mas isso não é necessário 
            // porque eles são gerados por padrão ou porque você pode defini-los na próxima etapa.
            //symmetricAlgo.GenerateIV();
            //symmetricAlgo.GenerateKey();

            var cipherDataInBytes = sim_cripto.CriptoSimetrica(symmetricAlgo);
            sim_cripto.DescriptoSimetrica(symmetricAlgo, cipherDataInBytes);


            byte[] EncryptData(byte[] plainData, byte[] IV, byte[] key)
            {
                // 1.Crie um objeto de criptografia
                SymmetricAlgorithm cryptoAlgorythm = SymmetricAlgorithm.Create();

                // 2.Pode definir uma chave e um IV, cryptoAlgorythm.GenerateIV();

                // 3.Crie um objeto criptografador
                ICryptoTransform encryptor = cryptoAlgorythm.CreateEncryptor(key, IV);

                //4.Chame o método TransformFinalBlock no criptografador
                byte[] cipherData = encryptor.TransformFinalBlock(plainData, 0,
                plainData.Length);
                return cipherData;
            }


            byte[] DecryptData(byte[] cipherData, byte[] IV, byte[] key)
            {
                // 1.Crie um objeto de criptografia
                SymmetricAlgorithm cryptoAlgorythm = SymmetricAlgorithm.Create();

                // 2.Pode definir uma chave e um IV, cryptoAlgorythm.GenerateIV();

                // 3.Crie um objeto criptografador
                ICryptoTransform decryptor = cryptoAlgorythm.CreateDecryptor(key, IV);

                //4.Chame o método TransformFinalBlock no criptografador
                byte[] plainData = decryptor.TransformFinalBlock(cipherData, 0,
                cipherData.Length);
                return plainData;
            }

            Console.ReadKey();
        }

        static byte[] Encrypt(SymmetricAlgorithm aesAlg, string plainText)
        {
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt =
                new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return msEncrypt.ToArray();
                }
            }
        }

        static string Decrypt(SymmetricAlgorithm aesAlg, byte[] cipherText)
        {
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt =
                new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        class Criptografia_Simetrica
        {
            public byte[] CriptoSimetrica(SymmetricAlgorithm symmetricAlgo)
            {
                // especifique os dados
                string plainData = "Mensagem Secreta";

                // converte em bytes da matriz
                byte[] plainDataInBytes = Encoding.UTF8.GetBytes(plainData);

                // 3.	Crie um objeto criptografador chamando o método CreateEncryptor. 
                // Novamente, você pode optar por enviar a chave e o IV como parâmetros 
                // para esse método ou usar o padrão gerado.
                ICryptoTransform encryptor = symmetricAlgo.CreateEncryptor();
                // ICryptoTransform encryptor = symmetricAlgo.CreateEncryptor(symmetricAlgo.Key, symmetricAlgo.IV);

                //4.	Chame o método TransformFinalBlock no criptografador, que recebe como entrada uma matriz de bytes, 
                // representando os dados simples, o deslocamento de onde iniciar a criptografia e o comprimento dos dados 
                // a serem criptografados. Retorna os dados criptografados de volta.
                byte[] cipherDataInBytes = encryptor.TransformFinalBlock(plainDataInBytes, 0, plainDataInBytes.Length);

                // coloca os bytes dos dados criptografados na string
                string cipherData = Encoding.UTF8.GetString(cipherDataInBytes);

                // Simétrica criptografia:o???w????r?7v?l?Zf?zzi?J?c
                Console.WriteLine("Simétrica criptografia:" + cipherData);

                return cipherDataInBytes;
            }

            public void DescriptoSimetrica(SymmetricAlgorithm symmetricAlgo, byte[] cipherDataInBytes)
            {
                //3.Crie um objeto de decodificador chamando o método CreateDecryptor. Agora você deve definir 
                // a chave e o IV enviando-os como parâmetros para esse método, se não o fez na etapa anterior.
                //A chave e o IV devem ser os mesmos que os usados para criptografia.
                ICryptoTransform decryptor = symmetricAlgo.CreateDecryptor(symmetricAlgo.Key, symmetricAlgo.IV);

                //4.Chame o método TransformFinalBlock no decodificador, que recebe como entrada uma matriz de bytes,
                //que são os dados do chipper, o deslocamento de onde iniciar a descriptografia e o comprimento 
                //dos dados a serem descriptografados e retorna os dados simples.
                byte[] plainDataInBytes = decryptor.TransformFinalBlock(cipherDataInBytes, 0, cipherDataInBytes.Length);
                string plainData = Encoding.UTF8.GetString(plainDataInBytes);

                //Simétrica descriptografia:Mensagem Secreta
                Console.WriteLine("Simétrica descriptografia:" + plainData);
            }
        }
    }
}
