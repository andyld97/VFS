// ------------------------------------------------------------------------
// UwpStorage.cs written by Code A Software (http://www.code-a-software.net)
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
    public class UwpStorage : IStorage
    {
        /// <summary>
        /// Singleton (This is the implementation of VFS for .NET Universal App)
        /// </summary>
        public static readonly UwpStorage UWPStorageProvider = new UwpStorage();

        private StorageFolder workspacePath = null;

        public IFilePath CombinePath(IDirectoryPath path, string name)
        {
            var task = Task.Run(async () => {
                var file = await (path as DirectoryPath).LocalFolder.CreateFileAsync(name, Windows.Storage.CreationCollisionOption.OpenIfExists);
                return new FilePath(file);
            });
            task.Wait();
            return task.Result;
        }

        public IDirectoryPath CreateDirectory(IDirectoryPath id, string subPath)
        {
            var task = Task.Run(async () =>
            {
                StorageFolder current = (id as DirectoryPath).LocalFolder;

                string[] segements = subPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < segements.Length; i++)
                {
                    string currentSegement = segements[i];
                    current = await current.CreateFolderAsync(currentSegement);
                }

                return new DirectoryPath(current);
            });
            task.Wait();

            return task.Result;
        }

        public IDirectoryPath CreateDirectory(IDirectoryPath id, string subPath, bool isPath)
        {
            return CreateDirectory(id, subPath);
        }

        public void DeleteFile(IFilePath file)
        {
            var task = Task.Run(async () =>
            {
                await (file as FilePath).LocalFile.DeleteAsync();
            });
            task.Wait();
        }

        public bool DirectoryExists(IDirectoryPath directory)
        {
            var task = Task.Run(async () =>
            {
                try
                {
                    var folder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(directory.ToFullPath());
                    return true;
                }
                catch
                {
                    return false;
                }

            });
            task.Wait();
            return task.Result;
        }

        public bool FileExists(IFilePath file)
        {
            var task = Task.Run(async () =>
            {
                try
                {
                    var folder = await Windows.Storage.StorageFile.GetFileFromPathAsync(file.ToFullPath());
                    return true;
                }
                catch
                {
                    return false;
                }

            });
            task.Wait();
            return task.Result;
        }

        public IDirectoryPath GetWorkSpacePath()
        {
            return new DirectoryPath(workspacePath);
        }

        public void SetWorkspacePath(StorageFolder sf)
        {
            this.workspacePath = sf;
        }

        public async Task<IStream> OpenFile(IFilePath file, FileAccess access, FileShare share, int BUFFER)
        {
            var localFile = await StorageFile.GetFileFromPathAsync(file.ToFullPath());

            Windows.Storage.Streams.IRandomAccessStream stream = null;

            switch (access)
            {
                case FileAccess.Read:
                    {
                        stream = await localFile.OpenAsync(FileAccessMode.Read);
                    }
                    break;
                case FileAccess.Write:
                    {
                        stream = await localFile.OpenAsync(FileAccessMode.ReadWrite);
                    }
                    break;
            }

            return new VStream(System.IO.WindowsRuntimeStreamExtensions.AsStream(stream, BUFFER));
        }

        public IMemoryStream OpenMemoryStream()
        {
            return new VMemoryStream();
        }
    }
}
