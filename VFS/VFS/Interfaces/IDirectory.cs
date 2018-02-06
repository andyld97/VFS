// ------------------------------------------------------------------------
// IDirectory.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      17.12.2016
// Last update on:  05.02.2018
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFS.Storage;

namespace VFS.Interfaces
{
    /// <summary>
    /// Determines the methods which a special implementation of a virtual directory needs
    /// </summary>
    public interface IDirectory
    {
        /// <summary>
        /// Returns the name of this directory
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        /// Sets the name of the directory (Just necessary because of IDirectory)
        /// </summary>
        /// <param name="name">Name which will be set</param>
        void SetName(string name);

        /// <summary>
        /// Returns a list of all subdirectories (TopLevel)
        /// </summary>
        /// <returns></returns>
        List<IDirectory> GetSubDirectories();

        /// <summary>
        /// Returns list of all files in this directory
        /// </summary>
        /// <returns></returns>
        List<IFile> GetFiles();

        /// <summary>
        /// Returns the unique index of this directory
        /// </summary>
        /// <returns></returns>
        int GetIndex();

        /// <summary>
        /// Returns the files the directory has
        /// </summary>
        /// <returns></returns>
        IDirectory GetParent();

        /// <summary>
        /// You can set the owner of this directory
        /// </summary>
        /// <param name="parent">Direcotry instance which will be the owner of this directory</param>
        void SetParent(IDirectory parent);

        /// <summary>
        /// Creates the full path till parent, if parent == null, then till start
        /// </summary>
        /// <param name="parent">The directory instance to get a special path from end to parent</param>
        /// <returns></returns>
        string ToFullPath(IDirectory parent = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string[] ToStringArray();

        /// <summary>
        /// Returns ALL files in each and every directory
        /// </summary>
        /// <returns></returns>
        string[] ToFileStringArray();

        /// <summary>
        /// Create virtual directories and files
        /// </summary>
        /// <param name="data">String-Array which contains pathes of the directories and files</param>
        void AddPathesWithFiles(string[] data);

        /// <summary>
        /// Create the virtual directories from the pathes given in data-array
        /// </summary>
        /// <param name="data">String-array that contains pathes</param>
        void AddPathes(string[] data);

        /// <summary>
        /// Removes a directory
        /// </summary>
        /// <param name="dir">The instance of the directory</param>
        /// <returns></returns>
        bool Remove(IDirectory dir);

        /// <summary>
        /// Removes a directroy
        /// </summary>
        /// <param name="path">The path where the directory is stored</param>
        /// <returns></returns>
        bool Remove(string path);

        /// <summary>
        /// Returns true if this directory contains a subdirectory with the given name
        /// </summary>
        /// <param name="dir">Name of the subdirectory</param>
        /// <returns></returns>
        bool Contains(string dir);

        /// <summary>
        /// Returns the index of the given dir
        /// </summary>
        /// <param name="dir">The directory from which you want to get the index (Be careful: This is the index of the list not the unique index)</param>
        /// <returns>-1 if this directory doesn't contains the given directory </returns>
        int IndexOf(string dir);

        /// <summary>
        /// Adds a file to this directory - without any content
        /// </summary>
        /// <param name="fileName">The filename which will be used to added this file to this directory</param>
        Task AddFile(string fileName);

        /// <summary>
        /// Add files to this directory - without any content
        /// </summary>
        /// <param name="fileNames">The filesname which will be used to added these files to this directory</param>
        Task AddFiles(string[] fileNames);

        /// <summary>
        /// Returns the last directory of the path (directory after last "backslash" ("\"))
        /// </summary>
        /// <param name="path">The path to work with</param>
        /// <returns></returns>
        IDirectory CalculateLastNode(string path);

        /// <summary>
        /// Returns the last directory of the path (directory after last "backslash" ("\"))
        /// </summary>
        /// <param name="firstNode">The directory to start at</param>
        /// <param name="path">The path to work with</param>
        /// <returns></returns>
        IDirectory CalculateLastNode(IDirectory firstNode, string path);

    }
}
