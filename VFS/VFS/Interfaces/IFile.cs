// ------------------------------------------------------------------------
// IFile.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      17.12.2016
// Last update on:  23.11.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFS.Helpers;
using VFS.Storage;

namespace VFS.Interfaces
{
    /// <summary>
    /// Determines the methods which a special implementation of a virtual file needs
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Returns the name of this file
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        /// Sets the name of the file
        /// </summary>
        /// <param name="Name">The filename</param>
        void SetName(string name);

        /// <summary>
        /// Returns the path of this file
        /// </summary>
        /// <returns></returns>
        string GetPath();

        /// <summary>
        /// Returns a list of the bytes which are stored in RAM
        /// </summary>
        /// <returns></returns>
        List<byte> GetBytes();

        /// <summary>
        /// Sets the bytes of this file
        /// </summary>
        /// <param name="bytes"></param>
        void SetByes(List<byte> bytes);

        /// <summary>
        /// Returns the owner of this file
        /// </summary>
        /// <returns></returns>
        IDirectory GetParentDirectory();

        /// <summary>
        /// Sets the owner of this file
        /// </summary>
        /// <param name="parentDir"></param>
        void SetParentDirectory(IDirectory parentDir);

        /// <summary>
        /// Returns a file instance by path
        /// </summary>
        /// <param name="files">The files where you want to retrieve the instance of the file</param>
        /// <param name="path">The path of the file</param>
        /// <returns></returns>
        IFile ByPath(List<IFile> files, string path);

        /// <summary>
        /// Proves if the list contains a file with the given path
        /// </summary>
        /// <param name="files">The files which are needed for research</param>
        /// <param name="path">The path of the file</param>
        /// <returns></returns>
        bool Contains(List<IFile> files, string path);

        /// <summary>
        /// Returns the length of this file with the right unit prefix
        /// </summary>
        /// <returns></returns>
        ConvertLength.Item CalculateLength();
    }
}
