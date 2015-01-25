using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiv1
{
    /// <summary>
    /// This class is for managing the files.
    /// </summary>
    public class Archiv
    {
        private int MainCounter = 128;
        private int PackByte = 45;


        public Archiv()
        {
        }

        /// <summary>
        ///  Reads one file and returns it as bit-array.
        /// </summary>
        /// <param name="Path">Path, where the file is.</param>
        /// <returns></returns>
        public Information GetFile(string Path)
        {
            Information ToReturn = new Information();
            try
            {
                ToReturn.ByteArray = System.IO.File.ReadAllBytes(Path);
            }
            catch (Exception e)
            {
              //  this.Instance.err.AddError("Invalid file or path: " + e.Message);
            }
            ToReturn.Path = Path;
            return ToReturn;
        }

        /// <summary>
        /// Reads more files and returns it as the Information-class.
        /// </summary>
        /// <param name="Path">Das Array, wo die Pfade gespeichert werden.</param>
        /// <returns></returns>
        public Information[] GetFiles(string[] Path)
        {
            Information[] x = new Information[Path.Length];
            for (int i = 0; i <= Path.Length - 1; i++)
            {
                x[i] = GetFile(Path[i]);
            }
            return x;
        }

        private byte[] StringToByteArray(string str)
        {
            return System.Text.Encoding.Default.GetBytes(str);
        }

        private string ByteArrayToString(byte[] arr)
        {
            return System.Text.Encoding.Default.GetString(arr);
        }

        /// <summary>
        /// Show only the file name not the whole path name.
        /// </summary>
        /// <param name="Path">The path, where the file is.</param>
        /// <returns></returns>
        public string GetFileName(string Path)
        {
            System.IO.FileInfo s = new System.IO.FileInfo(Path);
            if (s.Exists)
                return s.Name;
            else
            {
              //  this.Instance.err.AddError("Invalid file or path!");
                return string.Empty;
            }
        }

        private bool CheckNextItems(int Count, int Byte, int index, byte[] btArray)
        {
            bool Check = false;
            int x = 0;
            for (int i = index; i <= index + Count - 1; i++)
            {
                if (i >= btArray.Length - 1)
                    return false;
                if (btArray[i] == Byte)
                {
                    if (x == Count)
                        return true;
                    Check = true;
                    x++;
                }
                else
                    return false;
            }
            return Check;
        }

        /// <summary>
        /// Exctract the files, from "File" to "Folder"
        /// </summary>
        /// <param name="File">The file, which contains the information</param>
        /// <param name="Folder">The folder, where the files should be extracted.</param>
        public void UnpackFiles(string File, string Folder)
        {
            List<List<byte>> getBytes = new List<List<byte>>();
            int s = 0;
            getBytes.Add(new List<byte>());

            var sy = System.IO.File.ReadAllBytes(File);
            for (int i = 0; i <= sy.Length - 1; i++)
            {
                if (CheckNextItems(MainCounter, PackByte, i, sy))
                {
                    getBytes.Add(new List<byte>());
                    i += MainCounter - 1;
                    s++;
                }
                else
                    getBytes[s].Add(Convert.ToByte(sy[i]));
            }

            string[] Files = ByteArrayToString(getBytes[0].ToArray()).Split(new string[] { "<" }, StringSplitOptions.RemoveEmptyEntries);
            if (getBytes.Count == 1 && Files.Length != 1)
            {
             //   this.Instance.err.AddError("Invalid file, it couldn't be extracted!");
                return;
            }

            string[] rFiles = new string[] { };
            string[] rFolders = new string[] {};

            if (Files.Length - 1 == 0 || Files.Length - 1 == 1)
                rFiles = Files[0].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            if (Files.Length - 1 == 1)
                rFolders = Files[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);


            int count = 1;
            for (int x = 0; x <= getBytes.Count - 1; x++)
            {
                if (x != 0)
                {
                    try
                    {
                        rFolders[count - 1] = rFolders[count - 1].Replace(">", string.Empty).Replace("<", string.Empty);
                        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Folder, correctingPath(rFolders[count - 1])));
                    }
                    catch { }
                    string FileName = correctingPath(rFiles[count - 1]);
                    try
                    {
                        string Path = string.Empty;
                        if (FileName.StartsWith(@"\"))
                            Path = System.IO.Path.Combine(Folder, correctingPath(FileName));
                        else
                            Path = System.IO.Path.Combine(Folder, FileName);
                        System.IO.File.WriteAllBytes(Path, getBytes[x].ToArray());
                    }
                    catch (Exception e)
                    { 
                    //    this.Instance.err.AddError("The file couldn't be written: " + e.Message); 
                    }
                    count++;
                }
            }
        }

        private string correctingPath(string file)
        {
            if (file == string.Empty)
                return file;
            string toRet = string.Empty;
            for (int i = 1; i <= file.Length - 1; i++)
            {
                toRet += file[i].ToString();
            }
            return toRet;
        }

        /// <summary>
        /// Returns all filenames of an file.
        /// </summary>
        /// <param name="File">The file.</param>
        /// <returns></returns>
        public string[] GetFileNames(string File)
        {
            List<List<byte>> getBytes = new List<List<byte>>();
            int s = 0;
            getBytes.Add(new List<byte>());
            var sy = new byte[0];

            try
            {
                sy = System.IO.File.ReadAllBytes(File);
            }
            catch (Exception e)
            {
               // this.Instance.err.AddError("File couldn't be read: " + e.Message);
            }
            for (int i = 0; i <= sy.Length - 1; i++)
            {
                if (CheckNextItems(MainCounter, PackByte, i, sy))
                {
                    getBytes.Add(new List<byte>());
                    i += MainCounter - 1;
                    s++;
                }
                else
                    getBytes[s].Add(Convert.ToByte(sy[i]));
            }

            return ByteArrayToString(getBytes[0].ToArray()).Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void SetByteArray(ref List<byte> Arr)
        {
            for (int i = 0; i <= MainCounter - 1; i++)
            {
                Arr.Add((byte)PackByte);
            }
        }


        public void PackFilesWithFolders(Information[] Info, Archiv1.Klassen.IO.Folder[] flds, string File)
        {
            List<byte> ByteArray = new List<byte>();
            string Paths = string.Empty;

            foreach (Information y in Info)
                Paths += @"\" + this.GetFileName(y.Path) + "|";
            
            foreach (Archiv1.Klassen.IO.Folder fld in flds)
            {
                for (int i = 0; i <= fld.getFiles().Length - 1; i++)
                {
                    string fn = @"\" + fld.Name + fld.getFiles()[i].Replace(fld.Path, string.Empty);
                    //string fn = @"\" + fld.getFiles()[i].Replace(fld.Path, string.Empty);
                    Paths += fn + "|";
                }

            }

            if (flds.Length != 0)
            {
                Paths += "<";
                for (int i = 0; i <= flds.Length - 1; i++)
                {
                    Paths += flds[i].getSaveContent() + ",";
                }
                Paths += ">";
            }

            ByteArray.AddRange(StringToByteArray(Paths));
            foreach (Information d in Info)
            {
                SetByteArray(ref ByteArray);
                ByteArray.AddRange(d.ByteArray);
            }

            foreach (Archiv1.Klassen.IO.Folder fld in flds)
            {
                for (int i = 0; i <= fld.getFiles().Length - 1; i++)
                {
                    Information d = new Information(fld.getFiles()[i], null);
                    SetByteArray(ref ByteArray);
                    ByteArray.AddRange(d.ByteArray);
                }
            }    
      
            try
            {
                System.IO.File.WriteAllBytes(File, ByteArray.ToArray());
            }
            catch (Exception e)
            {
            //    this.Instance.err.AddError("The file couldn't be written: " + e.Message); 
            }
        }
    }
}
