using System;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;

namespace WindowsFormsTransparentSecurity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImperativeCAS_Button();
        }

        public static void ImperativeCAS_Button()
        {


            PermissionSet perms = new PermissionSet(PermissionState.None);
            perms.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            perms.AddPermission(new UIPermission(UIPermissionWindow.AllWindows));
            perms.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.Open));

            IPermission perm1 = new FileIOPermission(FileIOPermissionAccess.Read, @"C:\Windows");
            perms.AddPermission(perm1);

            AppDomainSetup objSetup = new AppDomainSetup();
            objSetup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            AppDomain domain = AppDomain.CreateDomain("Mew domain  name", AppDomain.CurrentDomain.Evidence, objSetup, perms);
            CriticalCode.Interface1 biblioteca = (CriticalCode.TesteCriticalCode)domain.CreateInstanceAndUnwrap("CriticalCode", "CriticalCode.TesteCriticalCode");

            var list_files = biblioteca.ListaArquivos_Windows();
            //biblioteca.ShowDialog();
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
