// ------------------------------------------------------------------------
// IStorage.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      05.02.2018
// Last update on:  05.02.2018
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFS.Storage
{
    public interface IStorage
    {
        Task<IStream> OpenFile(IFilePath file, FileAccess access, FileShare share, int BUFFER = 32768);

        IMemoryStream OpenMemoryStream();

        bool FileExists(IFilePath file);

        bool DirectoryExists(IDirectoryPath directory);

        IDirectoryPath CreateDirectory(IDirectoryPath id, string subPath);

        IDirectoryPath CreateDirectory(IDirectoryPath id, string subPath, bool isPath);

        IFilePath CombinePath(IDirectoryPath path, string name);

        void DeleteFile(IFilePath file);

        IDirectoryPath GetWorkSpacePath();
    }
}
