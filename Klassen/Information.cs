using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiv.Klassen
{
    public class Information
    {
        public string Path = string.Empty;
        public Byte[] ByteArray;
        private frmMain Instance = null;

        public Information(string Path, Byte[] ByteArray, frmMain Instance)
        {
            this.Path = Path;
            this.ByteArray = ByteArray;
            this.Instance = Instance;
        }

        public Information(string Path, frmMain Instance)
        {
            // Read in:
            this.Instance = Instance;
            Archiv x = new Archiv(this.Instance);
            ByteArray = x.GetFile(Path).ByteArray;
            this.Path = Path;
        }                              

        public Information(frmMain Instance)
        {
            this.Instance = Instance;
        }

    }
}
