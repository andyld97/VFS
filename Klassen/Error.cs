using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archiv.Klassen
{
    public class Error
    {
        private string Errors;

        public void AddError(string Exception)
        {
            MessageBox.Show("Es ist ein Fehler aufgetreten, näheres siehe Error.log", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Errors += DateTime.Now.ToShortDateString() + " @ " + DateTime.Now.ToShortTimeString() + ": " + Exception + Environment.NewLine;
            string Path = System.IO.Path.Combine(Application.StartupPath, "Error.log");
            string OldVars = string.Empty;
            if (System.IO.File.Exists(Path))
                OldVars = System.IO.File.ReadAllText(Path);
            System.IO.File.WriteAllText(Path, OldVars + Errors);
        }

        public string GetErrors
        {
            get { return Errors; }
        }
    }
}
