using System;
using System.Windows.Forms;

namespace AnimationEditor
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (FormMain frmMain = new FormMain())
            {
                Application.Run(frmMain);
            }
        }
    }
#endif
}