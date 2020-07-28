using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Windows.Forms;

namespace CriticalCode
{

    public class TesteCriticalCode : Interface1
    {
        public string Procesa_Algo()
        {
            return "Procesado o texto";
        }

        public List<String> ListaArquivos_Windows()
        {
            var path_Directory = @"C:\Windows";
            var olista = new List<String>();

            // Obter arquivo de um diretório específico usando a classe de diretório
            string[] fileNames = Directory.GetFiles(path_Directory);
            foreach (var name in fileNames)
            {
                olista.Add("Arquivo: " + name);
            }

            return olista;
        }

        public void ShowDialog()
        {
            OpenFileDialog window = new OpenFileDialog();
            window.ShowDialog();
        }
    }
}
