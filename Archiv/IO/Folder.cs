using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Archiv1.Klassen.IO
{
    public class Folder
    {
        public string Path = string.Empty;
        public string Name = string.Empty;

        public Folder(string Path, string Name)
        {
            this.Path = Path;
            this.Name = Name;
        }

        public string[] getFolders()
        {
            try
            {
                System.IO.DirectoryInfo m = new System.IO.DirectoryInfo(Path);
                System.IO.DirectoryInfo[] dm = m.GetDirectories("*", System.IO.SearchOption.AllDirectories);

                List<string> ret = new List<string>();
                for (int i = 0; i <= dm.Length - 1; i++)
                {
                    ret.Add(@"\" + this.Name + formatPath(Path, dm[i].FullName));
                }
                return ret.ToArray();
            }
            catch
            {
                return new string[] { };
            }
        }

        public string[] getFiles()
        {
            return Directory.GetFiles(Path, "*.*", SearchOption.AllDirectories);
        }

        public string getSaveContent(string splt = ",")
        {
            string[] test = getFolders();
            if (test.Length == 0)
                return @"\" + this.Name;
            return String.Join(",", this.getFolders());
        }

        private string formatPath(string path, string newpath)
        {
            return newpath.Replace(path, string.Empty);
        }

        public static string formatFolderPath(string Path)
        {
            string[] curArr = Path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            if (curArr == null || curArr.Length == 0)
                return string.Empty;
            return curArr[curArr.Length - 1];
        }
    }
}
