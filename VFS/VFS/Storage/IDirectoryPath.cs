// ------------------------------------------------------------------------
// IDirectoryPath.cs written by Code A Software (http://www.code-a-software.net)
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
    public interface IDirectoryPath
    {
        string Name();

        IEnumerable<IDirectoryPath>  GetDirectories();

        IEnumerable<IFilePath> GetFiles();

        bool Remove(bool recursive);

        bool CreateDirectory(string name);

        string ToFullPath();


    }
}
