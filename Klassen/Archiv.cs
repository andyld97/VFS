using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiv.Klassen
{
    /// <summary>
    /// This class is for managing the files.
    /// </summary>
    public class Archiv
    {
        private frmMain Instance = null;
        private int MainCounter = 128;
        private int PackByte = 45;


        public Archiv(frmMain Instance)
        {
            this.Instance = Instance;
        }

        /// <summary>
        ///  Reads one file and returns it as bit-array.
        /// </summary>
        /// <param name="Path">Path, where the file is.</param>
        /// <returns></returns>
        public Information GetFile(string Path)
        {
            Information ToReturn = new Information(this.Instance);
            try
            {
                ToReturn.ByteArray = System.IO.File.ReadAllBytes(Path);
            }
            catch (Exception e)
            {
                this.Instance.err.AddError("Invalid file or path: " + e.Message);
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
                this.Instance.err.AddError("Invalid file or path!");
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

            string[] Files = ByteArrayToString(getBytes[0].ToArray()).Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            if (getBytes.Count == 1)
            {
                this.Instance.err.AddError("Invalid file, it couldn't be extracted!");
                return;
            }
            int count = 1;
            for (int x = 0; x <= getBytes.Count - 1; x++)
            {
                if (x != 0)
                {
                    string FileName = Files[count - 1];
                    try
                    {
                        System.IO.File.WriteAllBytes(System.IO.Path.Combine(Folder, FileName), getBytes[x].ToArray());
                    }
                    catch (Exception e)
                    { this.Instance.err.AddError("The file couldn't be written: " + e.Message); }
                    count++;
                }
            }
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
                this.Instance.err.AddError("File couldn't be read: " + e.Message);
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

        /// <summary>
        /// Pack files. Bevor it calls "PackFilesUnEasys", it preperare the inputs.
        /// </summary>
        /// <param name="Files">The files, which should be packed.</param>
        /// <param name="File">The files in which all files and information packed.</param>
        public void PackEasy(string[] Files, string File)
        {
            // Do things, and call PackFilesUnEasy.
            // Convert Files to the Infomarion class
            List<Information> xInfos = new List<Information>();
            for (int i = 0; i <= Files.Length - 1; i++)
            {
                xInfos.Add(new Information(Files[i], this.Instance));
            }

            PackFilesUnEasy(xInfos.ToArray(), File);
        }

        /// <summary>
        /// Pack files.
        /// </summary>
        /// <param name="Info">An array of information</param>
        /// <param name="File">The files in which all files and information packed.</param>
        public void PackFilesUnEasy(Information[] Info, string File)
        {
            List<byte> ByteArray = new List<byte>();      
            string Paths = string.Empty; 

            foreach (Information y in Info)
            {
                Paths += this.GetFileName(y.Path) + "|";
            }

            ByteArray.AddRange(StringToByteArray(Paths));

            foreach (Information d in Info)
            {
                SetByteArray(ref ByteArray);
                ByteArray.AddRange(d.ByteArray);
            }
            try
            {
                System.IO.File.WriteAllBytes(File, ByteArray.ToArray());
            }
            catch (Exception e)
            { this.Instance.err.AddError("The file coudn't be written: " + e.Message); }
        }
    }
}
