// ------------------------------------------------------------------------
// IStream.cs written by Code A Software (http://www.code-a-software.net)
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
    public interface IStream : IDisposable
    {
        Task WriteAsync(byte[] data, int offset, int count);

        Task<long> ReadAsync(byte[] data, int offset, int count);

        void Write(byte[] data, int offset, int count);

        int Read(byte[] data, int offset, int count);

        long Position { get; set; }

        long Length { get; }

        void CopyTo(IMemoryStream stream);

        void Close();
    }
}
