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
using Windows.Storage;

namespace VFS.Uwp
{
    public class DirectoryPath : IDirectoryPath
    {
        public StorageFolder LocalFolder { get => path; }

        private StorageFolder path = null;
        
        public DirectoryPath(StorageFolder path)
        {
            this.path = path;
        }

        public bool CreateDirectory(string name)
        {
            try
            {
                var task = Task.Run(async () => await LocalFolder.CreateFolderAsync(name));
                task.Wait();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<IDirectoryPath> GetDirectories()
        {
            List<IDirectoryPath> directories = new List<IDirectoryPath>();
            var task = Task.Run(async () => await LocalFolder.GetFoldersAsync());
            task.Wait();

            foreach (var info in task.Result)
                directories.Add(new DirectoryPath(info));

            return directories;
        }

        public IEnumerable<IFilePath> GetFiles()
        {      
            List<IFilePath> files = new List<IFilePath>();
            var task = Task.Run(async () => await LocalFolder.GetFilesAsync());

            foreach (var info in task.Result)
                files.Add(new FilePath(info));

            return files;
        }

        public string Name()
        {
            return LocalFolder.Name;
        }

        public bool Remove(bool recursive)
        {
            try
            {
                var task = Task.Run(async () => await LocalFolder.DeleteAsync());
                task.Wait();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ToFullPath()
        {
            return LocalFolder.Path;
        }
    }
}
