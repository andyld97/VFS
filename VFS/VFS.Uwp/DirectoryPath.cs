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
        private StorageFolder sd = null;
        public StorageFolder Folder { get => sd; }
                
        public DirectoryPath(StorageFolder sd)
        {
            this.sd = sd;
        }

        public async Task<bool> CreateDirectory(string name)
        {
            try
            {
                await sd.CreateFolderAsync(name);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<IDirectoryPath>> GetDirectories()
        {
            List<IDirectoryPath> directories = new List<IDirectoryPath>();

            foreach (var info in await sd.GetFoldersAsync())
                directories.Add(new DirectoryPath(info));

            return directories;
        }

        public async Task<IEnumerable<IFilePath>> GetFiles()
        {
            List<IFilePath> files = new List<IFilePath>();

            foreach (var info in await sd.GetFilesAsync())
                files.Add(new FilePath(info));

            return files;
        }

        public string Name()
        {
            return sd.Name;
        }

        public async Task<bool> Remove(bool recursive)
        {
            try
            {
                await sd.DeleteAsync(StorageDeleteOption.PermanentDelete);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ToFullPath()
        {
            return sd.Path;
        }
    }
}
