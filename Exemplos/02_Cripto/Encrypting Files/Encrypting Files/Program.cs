using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encrypting_Files
{
    class Program
    {
        static void Main(string[] args)
        {
            var cpt_file = new Criptografia_File();
            var nome_arquivo = @"ml.txt";

            Console.WriteLine("Press enter to encrypt the file...");
            Console.ReadLine();

            cpt_file.EncryptFile(nome_arquivo);
            cpt_file.ReadFile(nome_arquivo);

            Console.WriteLine("Press enter to decrypt the file...");
            Console.Read();

            cpt_file.DecryptFile(nome_arquivo);

            Console.ReadKey();
        }
    }

    class Criptografia_File
    {
        public void ReadFile(string nome_arquivo)
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(nome_arquivo);
            while ((line = file.ReadLine()) != null)
                Console.WriteLine(line);
            counter++;

            file.Close();
        }

        public void EncryptFile(string nome_arquivo)
        {
            File.Encrypt(nome_arquivo);
            Console.WriteLine("Arquivo Criptografado ...");
        }

        public void DecryptFile(string nome_arquivo)
        {
            File.Decrypt(nome_arquivo);
            Console.WriteLine("Arquivo Descriptografado...");
        }
    }
}
