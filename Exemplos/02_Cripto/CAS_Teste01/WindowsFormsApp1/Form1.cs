using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FileIOPermission ff = new FileIOPermission(FileIOPermissionAccess.Write, "c:/temp/xyz.txt");
                //ff.Assert();

                ff.Deny();
                StreamWriter objW = new StreamWriter(File.Open("c:/temp/xyz.txt", FileMode.Open));
                CodeAccessPermission.RevertDeny();
                objW = null;

                objW.WriteLine("testing");
                objW.Flush();
                objW.Close();
                objW = null;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
