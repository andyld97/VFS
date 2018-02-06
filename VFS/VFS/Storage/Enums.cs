// ------------------------------------------------------------------------
// Enums.cs written by Code A Software (http://www.code-a-software.net)
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
    /// <summary>
    /// Determines the state of the file when the file is used. 
    /// You can avoid that someone tries to manipulate the file while
    /// you're working on the file
    /// </summary>
    public enum FileShare
    {
        ShareRead,
        ShareWrite,
        ShareBoth,
        ShareNone
    }

    /// <summary>
    /// Determines if you want to read or write from/to the file
    /// </summary>
    public enum FileAccess
    {
        Read,
        Write,
    }
}
