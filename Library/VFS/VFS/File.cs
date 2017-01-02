// ------------------------------------------------------------------------
// File.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      11.04.2016
// Last update on:  01.08.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFS
{
    /// <summary>
    /// Represents a virutal file
    /// </summary>
    public class File
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
        public readonly Directory Parent = null;

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
        public File(string Name, Directory Parent)
        {
            this.Name = Name;
            this.Parent = Parent;
        }

        /// <summary>
        /// Proves if a path is in a list of files.
        /// </summary>
        /// <param name="files">The list of the files</param>
        /// <param name="path">The path of the files</param>
        /// <returns>Whether the list have this file or not</returns>
        public static bool Contains(List<File> files, string path)
        {
            foreach (File currentFile in files)
                if (currentFile.Path == path)
                    return true;
            return false;
        }

        /// <summary>
        /// Searches a file in a list by his path
        /// </summary>
        /// <param name="files"></param>
        /// <param name="path"></param>
        /// <returns>A file from the path</returns>
        public static File ByPath(List<File> files, string path)
        {
            foreach (File file in files)
                if (file.Path == path)
                    return file;
            return null;
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
    }
}
