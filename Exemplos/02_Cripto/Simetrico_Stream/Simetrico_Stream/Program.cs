using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Simetrico_Stream
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] EncryptString(string plainData, byte[] IV, byte[] key)
            {
                //1.Crie um objeto de algoritmo simétrico
                SymmetricAlgorithm cryptoAlgorythm = SymmetricAlgorithm.Create();

                // 2.Pode definir uma chave e um IV, cryptoAlgorythm.GenerateIV();

                //3.Crie um objeto criptografador chamando o método CreateEncryptor
                ICryptoTransform encryptor = cryptoAlgorythm.CreateEncryptor(key, IV);
                byte[] cipherData = new byte[0];

                // 4. Crie os fluxos usados para criptografia.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    //5.Crie um objeto CryptoStream
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        //6.Grave dados no objeto CryptoStream usando um StreamWriter ou thread 
                        StreamWriter swEncrypt = new StreamWriter(csEncrypt);
                        swEncrypt.Write(plainData);

                        //7.Limpe o objeto CryptoStream chamando o método Clear e descarte o objeto.
                        swEncrypt.Close();
                        csEncrypt.Clear();
                        cipherData = msEncrypt.ToArray();
                    }
                }
                return cipherData;
            }

            string DecryptString(byte[] cipherData, byte[] IV, byte[] key)
            {
                //1.Crie um objeto de algoritmo simétrico
                SymmetricAlgorithm cryptoAlgorythm = SymmetricAlgorithm.Create();

                // 2.Pode definir uma chave e um IV, cryptoAlgorythm.GenerateIV();

                //3.Crie um objeto criptografador chamando o método CreateEncryptor
                ICryptoTransform decryptor = cryptoAlgorythm.CreateDecryptor(key, IV);
                string plainText = string.Empty;

                // 4. Crie os fluxos usados para criptografia.
                using (MemoryStream msDecrypt = new MemoryStream(cipherData))
                {
                    //5.Crie um objeto CryptoStream
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        //6.Leia os dados do objeto CryptoStream usando um StreamReader ou thread 
                        StreamReader srDecrypt = new StreamReader(csDecrypt);
                        plainText = srDecrypt.ReadToEnd();

                        //7.Limpe o objeto CryptoStream chamando o método Clear e descarte o objeto.
                        srDecrypt.Close();
                        csDecrypt.Clear();
                    }
                }
                return plainText;
            }

            //1.Crie um objeto de algoritmo simétrico chamando o método Create da classe SymmetricAlgorithm, 
            //configurando o parâmetro opcional string para o nome do algoritmo desejado.
            using (SymmetricAlgorithm symmetricAlgo = SymmetricAlgorithm.Create())
            {
                //2.Se desejar, você pode definir uma chave e um IV, mas isso 
                //não é necessário porque eles serão gerados por padrão.
                //symmetricAlgo.GenerateIV();
                //symmetricAlgo.GenerateKey();

                var stream_cripto = new Criptografia_Stream();

                var cipherDataInBytes = stream_cripto.EncryptStream(symmetricAlgo);
                stream_cripto.DecryptStream(symmetricAlgo, cipherDataInBytes);
            }

            Console.ReadKey();
        }
    }

    class Criptografia_Stream
    {
        public byte[] EncryptStream(SymmetricAlgorithm symmetricAlgo)
        {
            // especifique os dados
            string plainData = "Mensagem Secreta STREAM";

            byte[] cipherDataInBytes;

            //3.Crie um objeto criptografador chamando o método CreateEncryptor.Novamente, você pode optar 
            //por enviar a chave e o IV como parâmetros para esse método ou usar o padrão gerado.
            ICryptoTransform encryptor = symmetricAlgo.CreateEncryptor(symmetricAlgo.Key, symmetricAlgo.IV);
            //4. Create the streams used for encryption.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                //5.Crie um objeto CryptoStream.O construtor do CryptoStream espera três parâmetros.O primeiro parâmetro é o 
                //fluxo para o qual você envia os dados criptografados; o segundo é o criptografador que você criou na etapa 
                //anterior; e o terceiro é o modo de operação de fluxo, que neste caso é gravação.
                // crptoStream conhece o criptografador e o stream no qual os dados serão gravados
                using (CryptoStream crptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    //6.Grave dados no objeto CryptoStream chamando um dos métodos de gravação expostos pelo CryptoStream, 
                    //usando um StreamWriter ou thread - os para outro fluxo.
                    // writer tem referência de cryptoStream (o que criptografar e onde)
                    using (StreamWriter streamWriter = new StreamWriter(crptoStream))
                    {
                        //Write all data to the stream.
                        streamWriter.Write(plainData);
                    }

                    cipherDataInBytes = memoryStream.ToArray();

                    // coloca os bytes dos dados criptografados na string
                    string cipherData = Encoding.UTF8.GetString(cipherDataInBytes);

                    // Simétrica criptografia:o???w????r?7v?l?Zf?zzi?J?c
                    Console.WriteLine("Stream criptografado:" + cipherData);
                }
            }

            return cipherDataInBytes;
        }

        public void DecryptStream(SymmetricAlgorithm symmetricAlgo, byte[] cipherDataInBytes)
        {
            //3.Crie um objeto criptografador chamando o método CreateEncryptor.Novamente, você pode optar 
            //por enviar a chave e o IV como parâmetros para esse método ou usar o padrão gerado.
            ICryptoTransform decryptor = symmetricAlgo.CreateDecryptor(symmetricAlgo.Key, symmetricAlgo.IV);

            //4. Create the streams used for encryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherDataInBytes))
            {
                //5.Crie um objeto CryptoStream.O construtor do CryptoStream espera três parâmetros.O primeiro parâmetro é o 
                //fluxo para o qual você envia os dados criptografados; o segundo é o criptografador que você criou na etapa 
                //anterior; e o terceiro é o modo de operação de fluxo, que neste caso é gravação.
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    //6.Leia os dados do objeto CryptoStream chamando um dos métodos Read expostos por CryptoStream, 
                    // usando um StreamReader ou thread - o para outro fluxo.
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        var plaintext = srDecrypt.ReadToEnd();

                        Console.WriteLine("Stream Descriptografado..." + plaintext);
                    }
                }
            }
        }
    }
}
