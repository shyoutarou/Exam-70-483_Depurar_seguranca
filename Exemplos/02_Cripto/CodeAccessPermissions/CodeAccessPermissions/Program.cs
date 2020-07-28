using System;
using System.Security;
using System.Security.Permissions;

namespace CodeAccessPermissions
{
    class Program
    {
        static void Main(string[] args)
        {
            DeclarativeCAS();
            ImperativeCAS();
            Console.ReadKey();
        }

        [FileIOPermission(SecurityAction.Demand, AllLocalFiles = FileIOPermissionAccess.Read)]
        public static void DeclarativeCAS()
        {
            try
            {
                System.IO.File.Delete("COLORFUL.txt");
                Console.WriteLine("DeclarativeCAS");
            }
            catch (SecurityException s)
            {
                Console.WriteLine(s.Message);
            }
        }

        public static void ImperativeCAS()
        {
            PermissionSet perms = new PermissionSet(PermissionState.None);
            perms.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read, @"C:\Windows"));
            perms.AddPermission(new FileIOPermission(FileIOPermissionAccess.Write, @"C:\Inetpub"));
            perms.AddPermission(new RegistryPermission(RegistryPermissionAccess.Write, @"HKEY_LOCAL_MACHINE\Software"));

            FileIOPermission myFilePermissions = new FileIOPermission(FileIOPermissionAccess.Write, @"C:\Program Files\");

            FileIOPermission f = new FileIOPermission(PermissionState.None);
            f.AllLocalFiles = FileIOPermissionAccess.Read;
            try
            {
                Console.WriteLine("ImperativeCAS");
                f.Demand();

                perms.Demand();
                myFilePermissions.Demand();
                f.Demand();
            }
            catch (SecurityException s)
            {
                Console.WriteLine(s.Message);
            }
        }
    }
}
