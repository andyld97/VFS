using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archiv1
{
    public class Information
    {
        public string Path = string.Empty;
        public Byte[] ByteArray;

        public Information(string Path, Byte[] ByteArray)
        {
            this.Path = Path;
            this.ByteArray = ByteArray;
        }

        public Information(string Path)
        {
            // Read in:
            Archiv x = new Archiv();
            ByteArray = x.GetFile(Path).ByteArray;
            this.Path = Path;
        }

        public Information()
        {

        }
    }
}
