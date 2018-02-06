// ------------------------------------------------------------------------
// VStream.cs written by Code A Software (http://www.code-a-software.net)
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
    public class VStream : IStream
    {
        public System.IO.Stream Stream
        { get; set; }

        public long Position { get => Stream.Position; set => Stream.Position = value; }

        public long Length => Stream.Length;

        public VStream(string path, FileAccess access, FileShare share, int BUFFER)
        {
            System.IO.FileShare ioShare = System.IO.FileShare.Delete;
            System.IO.FileAccess ioAccess = System.IO.FileAccess.Read;
            System.IO.FileMode ioMode = System.IO.FileMode.OpenOrCreate;

            // Translate between System.IO-Enumerations and VFS-Enumerations
            switch (access)
            {
                case FileAccess.Read:
                    {
                        ioShare = System.IO.FileShare.Read;
                        ioAccess = System.IO.FileAccess.Read;
                        ioMode = System.IO.FileMode.Open;
                    }
                    break;
                case FileAccess.Write:
                    {
                        ioShare = System.IO.FileShare.Write;
                        ioAccess = System.IO.FileAccess.Write;
                        ioMode = System.IO.FileMode.OpenOrCreate;
                    }
                    break;
            }
            this.Stream = new System.IO.FileStream(path, ioMode, ioAccess, ioShare, BUFFER, true);
        }

        public void Close()
        {
            this.Stream.Close();
        }

        public void CopyTo(IMemoryStream stream)
        {
            (stream as VMemoryStream).CopyTo(stream);
        }

        public void Dispose()
        {
            this.Close();
        }

        public int Read(byte[] data, int offset, int count)
        {
            return Stream.Read(data, offset, count);
        }

        public async Task<long> ReadAsync(byte[] data, int offset, int count)
        {
            return await Stream.ReadAsync(data, offset, count);
        }

        public void Write(byte[] data, int offset, int count)
        {
            Stream.Write(data, offset, count);
        }

        public async Task WriteAsync(byte[] data, int offset, int count)
        {
            await Stream.WriteAsync(data, offset, count);
        }
    }
}
