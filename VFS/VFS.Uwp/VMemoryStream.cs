// ------------------------------------------------------------------------
// VMemoryStream.cs written by Code A Software (http://www.code-a-software.net)
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
    public class VMemoryStream : IMemoryStream
    {
        private System.IO.MemoryStream ms = null;

        public long Position { get => ms.Position; set => ms.Position = value; }

        public long Length => ms.Length;

        public VMemoryStream()
        {
            ms = new System.IO.MemoryStream();
        }

        public void Close()
        {
            ms.Dispose();
        }

        public void CopyTo(IMemoryStream stream)
        {
            ms.CopyTo((stream as VMemoryStream).ms);
        }

        public void Dispose()
        {
            this.Close();
        }

        public int Read(byte[] data, int offset, int count)
        {
            return ms.Read(data, offset, count);
        }

        public async Task<long> ReadAsync(byte[] data, int offset, int count)
        {
            return await ms.ReadAsync(data, offset, count);
        }

        public byte[] ToArray()
        {
            return ms.ToArray();
        }

        public void Write(byte[] data, int offset, int count)
        {
            ms.Write(data, offset, count);
        }

        public async Task WriteAsync(byte[] data, int offset, int count)
        {
            await ms.WriteAsync(data, offset, count);
        }
    }
}
