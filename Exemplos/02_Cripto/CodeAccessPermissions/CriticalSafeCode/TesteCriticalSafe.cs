using CriticalCode;
using System;
using System.Collections.Generic;
using System.Security;

namespace CriticalSafeCode
{
    [SecuritySafeCritical]
    public class TesteCriticalSafe : Interface1
    {
        [SecurityCritical]
        public List<string> ListaArquivos_Windows()
        {
            TesteCriticalCode biblioteca = new TesteCriticalCode();
            return biblioteca.ListaArquivos_Windows();
        }

        public string Procesa_Algo()
        {
            TesteCriticalCode biblioteca = new TesteCriticalCode();
            return biblioteca.Procesa_Algo();
        }

        public void ShowDialog()
        {
            TesteCriticalCode biblioteca = new TesteCriticalCode();
            biblioteca.ShowDialog();
        }
    }
}
