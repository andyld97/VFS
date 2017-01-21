// ------------------------------------------------------------------------
// SearchResult.cs written by Code A Software (http://www.code-a-software.net)
// SP: VHP-0001 (OpenSource-Software)
// Created on:      14.01.2017
// Last update on:  14.01.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFS.Interfaces;

namespace VFS
{
    /// <summary>
    /// Represents a result of a search
    /// </summary>
    public struct SearchResult
    {
        /// <summary>
        /// All directories which match to search-string
        /// </summary>
        public IDirectory[] Directories;

        /// <summary>
        /// All files which match to search-string
        /// </summary>
        public IFile[] Files;

        /// <summary>
        /// Creates a new search result
        /// </summary>
        /// <param name="Directories">All directories which match to search-string</param>
        /// <param name="Files">All files which match to search-string</param>
        public SearchResult(IDirectory[] Directories, IFile[] Files)
        {
            this.Directories = Directories;
            this.Files = Files;
        }
    }
}
