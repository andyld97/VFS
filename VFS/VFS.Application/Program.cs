using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VFS.Application.GUI;
using APP = System.Windows.Forms;

namespace VFS.Application
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            APP.Application.EnableVisualStyles();
            APP.Application.SetCompatibleTextRenderingDefault(false);
            APP.Application.Run(new frmExplorer());
        }
    }
}
