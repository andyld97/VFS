// ------------------------------------------------------------------------
// ExtendedFile.cs written by Code A Software (http://www.code-a-software.net)
// SP: VHP-0001 (OpenSource-Software)
// Created on:      17.12.2016
// Last update on:  08.01.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFS.Interfaces;
using VFS.Extensions;

namespace VFS.ExtendedVFS
{
    /// <summary>
    /// Represents a file with additonally StartPosition, EndPosition and Size
    /// </summary>
    public class ExtendedFile : IFile
    {
        /// <summary>
        /// The path on the local harddrive (e.g. when you call CreateVHP, the original path will be stored here)
        /// </summary>
        public string OrgPath = string.Empty;

        /// <summary>
        /// The owner directory of this file
        /// </summary>
        public IDirectory Parent = null;

        /// <summary>
        /// The length of this file
        /// </summary>
        public long Size;

        /// <summary>
        /// The position where the file starts
        /// </summary>
        public long StartPosition = 0L;

        /// <summary>
        /// Is written in workspace directory and doesn't contain positions yet. Call yourVFS.Save() to change this state.
        /// </summary>
        public bool IsInvalid = false;

        /// <summary>
        /// Returns the position where the file ends (dependend from StartPosition and Size)
        /// </summary>
        public long EndPosition
        {
            get
            {
                return this.StartPosition + this.Size;
            }
        }

        /// <summary>
        /// The virtual path of this file
        /// </summary>
        public string Path
        {
            get
            {
                if (this.Parent.ToFullPath() == @"\")
                    return @"\" + this.FileName;
                else
                    return this.Parent.ToFullPath() + @"\" + this.FileName;
            }
        }

        /// <summary>
        /// The filename of the original path
        /// </summary>
        public string FileName
        {
            get
            {
                string[] segments = this.OrgPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                if (segments.Length > 0)
                    return segments[segments.Length - 1];
                return this.OrgPath;
            }
        }

        /// <summary>
        /// Creates a new virtual file
        /// </summary>
        /// <param name="hi">Header-Information</param>
        /// <param name="Parent">The owner of this file</param>
        public ExtendedFile(HeaderInfo hi, ExtendedDirectory Parent)
        {
            this.Size = Math.Abs(hi.StartPosition - hi.EndPosition);
            this.OrgPath = hi.Path;
            this.StartPosition = hi.StartPosition;
            this.Parent = Parent;
        }

        /// <summary>
        /// Returns a file instance by path
        /// </summary>
        /// <param name="files">The files where you want to retrieve the instance of the file</param>
        /// <param name="path">The path of the file</param>
        /// <returns></returns>
        public IFile ByPath(List<IFile> files, string path)
        {
            string pt1 = this.normPath(path);

            foreach (IFile file in files)
                if (file.GetPath() == pt1 || file.GetPath() == @"\" + pt1)
                    return file;
            return null;
        }

        private string normPath(string path)
        {
            string nPath = path;
            if (path.StartsWith(@"\") && path.Length >= 2)
            {
                nPath = string.Empty;
                for (int i = 1; i <= path.Length - 1; i++)
                    nPath += path[i];
            }

            // Remove |
            nPath = nPath.Replace("|", string.Empty);
            string[] segments = nPath.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length > 0)
                return segments[0];
            return nPath;
        }

        /// <summary>
        /// Returns the length of this file with the right unit prefix
        /// </summary>
        /// <returns></returns>
        public ConvertLength.Item CalculateLength()
        {
            return ConvertLength.Calculate(this.Size);
        }

        /// <summary>
        /// Proves if the list contains a file with the given path
        /// </summary>
        /// <param name="files">The files which are needed for research</param>
        /// <param name="path">The path of the file</param>
        /// <returns></returns>
        public bool Contains(List<IFile> files, string path)
        {
            string pt1 = this.normPath(path);

            foreach (IFile currentFile in files)
                if (currentFile.GetPath() == pt1 || currentFile.GetPath() == @"\" + pt1)
                    return true;
            return false;
        }

        /// <summary>
        /// Returns a list of the bytes which are stored in RAM - useless here, but necessary for implenting the interface
        /// </summary>
        /// <returns></returns>
        public List<byte> GetBytes()
        {
            return new List<byte>();
        }

        /// <summary>
        /// Returns the name of this file
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.FileName;
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
        /// Returns the path of this file
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            return this.Path;
        }

        /// <summary>
        /// Sets the bytes of this file - useless, nothing will be done when calling this method
        /// </summary>
        /// <param name="bytes"></param>
        public void SetByes(List<byte> bytes)
        {

        }

        /// <summary>
        /// Sets the name of the file
        /// </summary>
        /// <param name="Name">The filename</param>
        public void SetName(string Name)
        {
            // Change name in orgPath
            string[] segements = this.OrgPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            if (segements.Length > 0)
            {
                segements[segements.Length - 1] = Name;
                this.OrgPath = String.Join(@"\", segements);
            }
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
        /// Returns e.g. \Path:0:500 for easier creating a header
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Path + ":" + this.StartPosition + ":" + this.EndPosition + "|";
        }
    }
}
