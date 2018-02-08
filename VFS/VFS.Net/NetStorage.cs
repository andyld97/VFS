// ------------------------------------------------------------------------
// NetStorage.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      05.02.2018
// Last update on:  05.02.2018
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VFS.Storage;

namespace VFS.Net
{
    public class NetStorage : IStorage
    {
        /// <summary>
        /// Singleton (This is the implementation of VFS for .NET Framework)
        /// </summary>
        public static readonly IStorage NETStorageProvider = new NetStorage();

        public IDirectoryPath CreateDirectory(IDirectoryPath id, string subPath)
        {
            return new DirectoryPath(System.IO.Path.Combine(id.ToFullPath(), subPath));
        }

        public IDirectoryPath CreateDirectory(IDirectoryPath id, string subPath, bool isPath)
        {
            return CreateDirectory(id, subPath);
        }

        public IFilePath CombinePath(IDirectoryPath path, string name)
        {
            string newPath = System.IO.Path.Combine(path.ToFullPath(), name);
            return new FilePath(newPath);
        }

        public void DeleteFile(IFilePath file)
        {
            System.IO.File.Delete(file.ToFullPath());
        }

        public bool DirectoryExists(IDirectoryPath directory)
        {
            return System.IO.Directory.Exists(directory.ToFullPath());
        }

        public bool FileExists(IFilePath file)
        {
            return System.IO.File.Exists(file.ToFullPath());
        }

        public IDirectoryPath GetWorkSpacePath()
        {
            return new DirectoryPath(System.IO.Path.Combine(Application.StartupPath, "Workspace"));
        }

        public async Task<IStream> OpenFile(IFilePath file, FileAccess access, FileShare share, int BUFFER = 32768)
        {
            return await Task.Run(delegate
            {
                return new VStream(file.ToFullPath(), access, share, BUFFER);
            });
        }

        public IMemoryStream OpenMemoryStream()
        {
            return new VMemoryStream();
        }
    }
}
