// ------------------------------------------------------------------------
// Methods.cs written by Code A Software (http://www.code-a-software.net)
// SP: VHP-0001 (OpenSource-Software)
// Created on:      29.12.2016
// Last update on:  08.01.2017
// ------------------------------------------------------------------------
using System;

namespace VFS.ExtendedVFS
{
    /// <summary>
    /// Identfies the methods (MVFS or MVFST)
    /// </summary>
    public enum Methods
    {
        /// <summary>
        /// Method identifier - Default
        /// </summary>
        DEFAULT = 0x00,

        /// <summary>
        /// Method identifier - Extract
        /// </summary>
        EXTRACT = 0x01,

        /// <summary>
        /// Method identifier - ExtractDirectory
        /// </summary>
        EXTRACT_DIR = 0x02,

        /// <summary>
        /// Method identifier - ExtractFiles
        /// </summary>
        EXTRACT_FILES = 0x03,

        /// <summary>
        /// Method identifier - Read
        /// </summary>
        READ = 0x04,

        /// <summary>
        /// Method identifier - ReadAllBytes
        /// </summary>
        READ_ALL_BYTES = 0x05,

        /// <summary>
        /// Method identifier - ReadAllText
        /// </summary>
        READ_ALL_TEXT = 0x06,

        /// <summary>
        /// Method identifier - RemoveFile
        /// </summary>
        REMOVE_FILE = 0x07,

        /// <summary>
        /// Method identifier - WriteAllBytes
        /// </summary>
        WRITE_ALL_BYTES = 0x08,

        /// <summary>
        /// Method identifier - WriteAllText
        /// </summary>
        WRITE_ALL_TEXT = 0x09,

        /// <summary>
        /// Method identifier - WriteStream
        /// </summary>
        WRITE_STREAM = 0x0A,

        /// <summary>
        /// Method identifier - CreateVHP
        /// </summary>
        CREATE = 0x0B,

        /// <summary>
        /// Method identifier - Save
        /// </summary>
        SAVE = 0x0C
    }
}