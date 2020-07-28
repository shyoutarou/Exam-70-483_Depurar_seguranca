using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Load_Recurso_Forms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Assembly assembly = Assembly.LoadFrom("ClassLibrary_Recurso.dll");
            string[] names = assembly.GetManifestResourceNames();

            var oform = new Form1();
            var image = assembly
                .GetManifestResourceStream("ClassLibrary_Recurso.Background.png");

            //Image img = Image.FromStream(image);
            //PictureBox1.Image = img;

            int cont = 4;
            int sqri = ClassLibrary_Recurso.Class1.square(cont);
            MessageBox.Show("Square of " + cont + " = " + sqri);

            oform.BackgroundImage = new Bitmap(image);
            Application.Run(oform);
        }
    }
}
