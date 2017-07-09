// ------------------------------------------------------------------------
// File.cs written by Code A Software (http://www.code-a-software.net)
// SP: VHP-0001 (OpenSource-Software)
// Created on:      11.04.2016
// Last update on:  30.06.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFS.Interfaces;

namespace VFS
{
    /// <summary>
    /// Represents a virutal file
    /// </summary>
    public class File : Interfaces.IFile
    {
        /// <summary>
        /// The name of this file
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// The content of this file in bytes
        /// </summary>
        public List<byte> Bytes = new List<byte>();

        /// <summary>
        /// The parent directory of this file
        /// </summary>
        public IDirectory Parent = null;

        /// <summary>
        /// The full path of this file
        /// </summary>
        public string Path
        {
            get
            {
                return this.Parent.ToFullPath() + @"\" + this.Name;
            }
        }
        
        /// <summary>
        /// Initiates a new file
        /// </summary>
        /// <param name="Name">The file name</param>
        /// <param name="Parent">The directory which contains the file</param>
        public File(string Name, IDirectory Parent)
        {
            this.Name = Name;
            this.Parent = Parent;
        }

        /// <summary>
        /// Calculates the length of the file in the apropriate unit prefix
        /// </summary>
        /// <returns>ConvertLenght.Item - Size and Unit Prefix</returns>
        public Extensions.ConvertLength.Item CalculateLength()
        {
            if (this.Bytes.Count() == 0)
                return new Extensions.ConvertLength.Item(0, Extensions.ConvertLength.Type_.B);

            return Extensions.ConvertLength.Calculate(this.Bytes.Count());
        }

        /// <summary>
        /// Returns the name of this file
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.Name;
        }

        /// <summary>
        /// Sets the name of the file
        /// </summary>
        /// <param name="Name">The filename</param>
        public void SetName(string Name)
        {
            this.Name = Name;
        }

        /// <summary>
        /// Returns the path of this file
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            return this.Path;
        }

        /// <summary>
        /// Returns a list of the bytes which are stored in RAM
        /// </summary>
        /// <returns></returns>
        public List<byte> GetBytes()
        {
            return this.Bytes;
        }

        /// <summary>
        /// Sets the bytes of this file
        /// </summary>
        /// <param name="bytes"></param>
        public void SetByes(List<byte> bytes)
        {
            this.Bytes = bytes;
        }

        /// <summary>
        /// Returns the owner of this file
        /// </summary>
        /// <returns></returns>
        public IDirectory GetParentDirectory()
        {
            return this.Parent;
        }

        /// <summary>
        /// Sets the owner of this file
        /// </summary>
        /// <param name="parentDir"></param>
        public void SetParentDirectory(IDirectory parentDir)
        {
            this.Parent = parentDir;
        }

        /// <summary>
        /// Returns a file instance by path
        /// </summary>
        /// <param name="files">The files where you want to retrieve the instance of the file</param>
        /// <param name="path">The path of the file</param>
        /// <returns></returns>
        public IFile ByPath(List<IFile> files, string path)
        {
            foreach (IFile file in files)
                if (file.GetPath() == path)
                    return file;
            return null;
        }

        /// <summary>
        /// Proves if the list contains a file with the given path
        /// <param name="files">The files which are needed for research</param>
        /// <param name="path">The path of the file</param>
        /// <returns></returns>
        /// </summary>
        public bool Contains(List<IFile> files, string path)
        {
            foreach (IFile currentFile in files)
                if (currentFile.GetPath() == path)
                    return true;
            return false;
        }
    }
}
