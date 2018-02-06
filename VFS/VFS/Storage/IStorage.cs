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

        Task<bool> FileExists(IFilePath file);

        Task<bool> DirectoryExists(IDirectoryPath directory);

        Task<IDirectoryPath> CreateDirectory(IDirectoryPath id, string subPath);

        Task<IDirectoryPath> CreateDirectory(IDirectoryPath id, string subPath, bool isPath);

        Task<IFilePath> CombiniePath(IDirectoryPath path, string name);

        Task DeleteFile(IFilePath file);

        IDirectoryPath GetWorkSpacePath();
    }
}
