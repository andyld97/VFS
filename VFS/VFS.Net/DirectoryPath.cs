// ------------------------------------------------------------------------
// DirectoryPath.cs written by Code A Software (http://www.code-a-software.net)
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
    public class DirectoryPath : IDirectoryPath
    {
        private string path = string.Empty;
        private System.IO.DirectoryInfo di;

        public DirectoryPath(string path)
        {
            this.path = path;

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            this.di = new System.IO.DirectoryInfo(path);
        }

        public bool CreateDirectory(string name)
        {
            try
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(di.FullName, name));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<IDirectoryPath> GetDirectories()
        {
            this.di = new System.IO.DirectoryInfo(path);
            List<DirectoryPath> dirs = new List<DirectoryPath>();

            foreach (var directory in di.GetDirectories())
                dirs.Add(new DirectoryPath(directory.FullName));

            return dirs;     
        }

        public IEnumerable<IFilePath> GetFiles()
        {
            this.di = new System.IO.DirectoryInfo(path);
            List<FilePath> files = new List<FilePath>();

            foreach (var file in di.GetFiles("*.*", System.IO.SearchOption.TopDirectoryOnly))
                files.Add(new FilePath(file.FullName));

            return files;
        }

        public string Name()
        {
            return di.Name;
        }

        public IDirectoryPath ParentDirectory()
        {
            return new DirectoryPath(di.Parent.FullName);
        }

        public bool Remove(bool recursive)
        {
            try
            {
                System.IO.Directory.Delete(path, recursive);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ToFullPath()
        {
            return di.FullName;
        }
    }
}