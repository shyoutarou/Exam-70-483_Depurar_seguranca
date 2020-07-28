using System;
using System.Security;
using System.Security.Permissions;

namespace DoNotTrust_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            //PermissionSet perms = new PermissionSet(PermissionState.None);
            //AppDomainSetup objSetup = new AppDomainSetup();
            //AppDomain domain = AppDomain.CreateDomain("Meu domain name", AppDomain.CurrentDomain.Evidence, objSetup, perms);
            //CriticalSafeCode.Interface1 biblioteca = (CriticalSafeCode.TesteCriticalSafe)domain.CreateInstanceAndUnwrap("CriticalSafeCode", "CriticalSafeCode.TesteCriticalSafe");

            //var texto = biblioteca.Procesa_Algo();

            //Console.WriteLine(texto);


            //CriticalCode.TesteCriticalCode biblioteca = new CriticalCode.TesteCriticalCode();
            //var texto = biblioteca.ListaArquivos_Windows();

            //Console.WriteLine(texto);

            //Console.ReadKey();

            //var list_files = biblioteca.ListaArquivos_Windows();

            Escreva_Permissoes();
            Console.ReadKey();
        }

        public static void Escreva_Permissoes()
        {
            IPermission perm1 = new FileIOPermission(FileIOPermissionAccess.Read, @"C:\Windows");
            IPermission perm2 = new FileIOPermission(FileIOPermissionAccess.Write, @"C:\Windows");
            IPermission perm3 = new FileIOPermission(FileIOPermissionAccess.Write, @"C:\Windows");
            IPermission perm4 = new FileIOPermission(FileIOPermissionAccess.AllAccess, @"C:\Windows");
            IPermission all = new FileIOPermission(PermissionState.Unrestricted);
            IPermission none = new FileIOPermission(PermissionState.None);

            Console.WriteLine(perm1.Union(perm2)); // IPermission Read="C:\Windows" e Write="C:\Windows"
            Console.WriteLine(perm2.Union(perm3)); // IPermission Write="C:\Windows"
            Console.WriteLine(perm1.Intersect(perm4)); // IPermission Read="C:\Windows" 
            Console.WriteLine(perm1.Union(all)); // IPermission Unrestricted="true"
            Console.WriteLine(perm1.Intersect(all)); // IPermission Read="C:\Windows"
            Console.WriteLine(perm1.Union(none)); // IPermission Read="C:\Windows"

            PermissionSet perms = new PermissionSet(PermissionState.None);
            perms.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            perms.AddPermission(new UIPermission(UIPermissionWindow.AllWindows));
            perms.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.Open));

            Console.WriteLine(perms);
            // PermissionSet 
            // IPermission FileDialogPermission Access="Open"
            // IPermission SecurityPermission Flags="Execution"
            // IPermission UIPermission Window="AllWindows"

            perms.AddPermission(perm1);
            perms.AddPermission(perm1.Union(perm2));
            Console.WriteLine(perms);
            // Adicionou IPermission Read="C:\Windows" e Write="C:\Windows"
        }
    }
}
