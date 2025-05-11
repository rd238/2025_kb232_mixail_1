using System;
using System.Windows.Forms;
using lab10;

namespace lab10
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new lab10());
        }
    }
}