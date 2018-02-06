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

        public async Task<IFilePath> CombinePath(IDirectoryPath path, string name)
        {
            var file = await (path as DirectoryPath).Folder.CreateFileAsync(name, Windows.Storage.CreationCollisionOption.OpenIfExists);
            return new FilePath(file);
        }

        public async Task<IDirectoryPath> CreateDirectory(IDirectoryPath id, string subPath)
        {
            StorageFolder current = (id as DirectoryPath).Folder;

            string[] segements = subPath.Split(new string[] { @"\"}, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < segements.Length; i++)
            {
                string currentSegement = segements[i];
                current = await current.CreateFolderAsync(currentSegement);
            }

            return new DirectoryPath(current);
        }

        public async Task<IDirectoryPath> CreateDirectory(IDirectoryPath id, string subPath, bool isPath)
        {
            return await CreateDirectory(id, subPath);
        }

        public async Task DeleteFile(IFilePath file)
        {
            await (file as FilePath).LocalFile.DeleteAsync();
        }

        public async Task<bool> DirectoryExists(IDirectoryPath directory)
        {
            try
            {
                var folder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(directory.ToFullPath());
                var parentFolder = await folder.GetParentAsync();

                await parentFolder.GetItemAsync(folder.Name);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> FileExists(IFilePath file)
        {
            try
            {
                var f = await Windows.Storage.StorageFile.GetFileFromPathAsync(file.ToFullPath());
                var parentFolder = await f.GetParentAsync();
                await parentFolder.GetItemAsync(file.GetName());

                return true;
            }
            catch
            {
                return false;
            }
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
            var localFile = (file as FilePath).LocalFile;
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

            return new VStream(System.IO.WindowsRuntimeStreamExtensions.AsStream(stream));
        }

        public IMemoryStream OpenMemoryStream()
        {
            return new VMemoryStream();
        }
    }
}
