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

namespace VFS.Uwp
{
    public class VStream : IStream
    {
        public System.IO.Stream Stream { get; set; }

        public long Position { get => Stream.Position; set => Stream.Position = value; }

        public long Length => Stream.Length;

        public VStream(System.IO.Stream stream)
        {
            if (stream == null)
                throw new ArgumentException("stream");

            this.Stream = stream;
        }

        public void Close()
        {
            Stream.Close();
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
