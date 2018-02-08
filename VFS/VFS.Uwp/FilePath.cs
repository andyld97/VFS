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
using Windows.Storage;

namespace VFS.Uwp
{
    public class FilePath : IFilePath
    {
        private StorageFile sf = null;

        public StorageFile LocalFile { get => sf; }

        public FilePath(StorageFile sf)
        {
            this.sf = sf;
        }

        public string GetName()
        {
            return sf.Name;
        }

        public long Length()
        {
            Task<ulong> task = Task.Run(async () => {
                var properties = await sf.GetBasicPropertiesAsync();
                return properties.Size;
            });

            task.Wait();
            return (long)task.Result;
        }

        public async Task SetName(string name)
        {;
            await sf.RenameAsync(name);
        }

        public string ToFullPath()
        {
            return sf.Path;
        }
    }
}
