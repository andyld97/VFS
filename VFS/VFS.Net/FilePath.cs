// ------------------------------------------------------------------------
// FilePath.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      05.02.2018
// Last update on:  05.02.2018
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFS.Storage;

namespace VFS.Net
{
    public class FilePath : IFilePath
    {
        private string path = string.Empty;
        private System.IO.FileInfo fi = null;

        public FilePath(string path)
        {
            this.path = path;

            // File doesn't need to be created! This is always done by using a stream
            try
            {
                this.fi = new System.IO.FileInfo(this.path);
            }
            catch { }
        }

        public string GetName()
        {
            if (string.IsNullOrEmpty(fi.Name))
            {
                var segements = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                if (segements.Length > 0)
                    return segements[segements.Length - 1];
                else
                    return string.Empty;
            }
            else
                return (fi == null ? string.Empty : fi.Name);
        }

        public async Task<long> Length()
        {
            return await Task<long>.Run(delegate {
                return (fi == null ? 0L : fi.Length);
            });            
        }

        public IDirectoryPath ParentDirectory()
        {
            if (fi == null)
                return null;

            return new DirectoryPath(fi.DirectoryName);
        }

        public async Task SetName(string name)
        {
            await Task.Run(delegate
            {
                // Renmae file: move and delete oldSystem.IO.File.Copy(   )
                string[] segements = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

                // Same name as bevor (no need to rename the file)
                if (segements[segements.Length - 1] == name)
                    return;

                string newFile = string.Empty;

                for (int i = 0; i < segements.Length - 1; i++)
                    newFile += segements[i] + @"\";
                newFile += name;

                System.IO.File.Move(this.path, newFile);
                System.IO.File.Delete(this.path);
            });
        }

        public string ToFullPath()
        {
            return path;
        }
    }
}
