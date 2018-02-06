// ------------------------------------------------------------------------
// Direcotry.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      11.04.2016
// Last update on:  30.06.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VFS.Interfaces;

namespace VFS
{
    /// <summary>
    /// Represents a virtual directory
    /// </summary>
    public class Directory : IDirectory
    {
        /// <summary>
        /// The name of the directory
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// The directorys which are this directory contains
        /// </summary>
        public List<IDirectory> SubDirs = new List<IDirectory>();

        /// <summary>
        /// The files of this directory
        /// </summary>
        public List<IFile> Files = new List<IFile>();

        /// <summary>
        /// The index to identify each folder right
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// The parent directory of this directory
        /// </summary>
        private IDirectory parent = null;

        /// <summary>
        /// This event throws if something was changed.
        /// </summary>
        /// <param name="action"></param>
        public delegate void onChanged(Type action);

        /// <summary>
        /// This event throws if something was changed.
        /// </summary>
        public event onChanged OnChanged;

        /// <summary>
        /// The distinguish between the different changes
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// If a path was added - If a directory was added
            /// </summary>
            AddedPath,
            /// <summary>
            /// If a file was added
            /// </summary>
            AddedFile,
            /// <summary>
            /// If a directory was deleted
            /// </summary>
            DeletedDirectory,
            /// <summary>
            /// If a file was deleted
            /// </summary>
            DeletedFile,             
            /// <summary>
            /// Default action
            /// </summary>
            Default
        }

        private void throwChangedEvent(Type type)
        {
            if (this.OnChanged != null)
                this.OnChanged(type);
        }

        /// <summary>
        /// The parent of this directory - the main directory doesn't have a parent
        /// </summary>
        public IDirectory Parent
        {
            get
            {
                return this.parent;
            }
            private set
            {
                this.parent = value;
                if (this.Parent is Directory)
                    (this.Parent as Directory).OnChanged += this.OnChanged;
            }
        }

        /// <summary>
        /// Initates a new virtual directroy
        /// </summary>
        /// <param name="Name">The name of this directroy</param>
        public Directory(string Name)
        {
            this.Name = Name;
            this.Index = VFS.DirIndex++;           
        }

        /// <summary>
        /// Creates a string array of all pathes
        /// </summary>
        /// <returns></returns>
        public string[] ToStringArray()
        {
            List<string> paths = new List<string>();

            Action<IDirectory> passDirs = null;
            Directory node = this;

            passDirs = new Action<IDirectory>((IDirectory currentDir) => {
                foreach (IDirectory dir in currentDir.GetSubDirectories())
                {
                    paths.Add(dir.ToFullPath());
                    passDirs(dir);
                }
            });

            passDirs(node);
            return paths.ToArray();
        }

        /// <summary>
        /// Returns ALL files in each and every directory
        /// </summary>
        /// <returns></returns>
        public string[] ToFileStringArray()
        {
            List<string> paths = new List<string>();

            Action<IDirectory> passDirs = null;
            IDirectory node = this;

            passDirs = new Action<IDirectory>((IDirectory currentDir) => {
                foreach (IDirectory dir in currentDir.GetSubDirectories())
                {
                    foreach (IFile currentFile in dir.GetFiles())
                        if (!string.IsNullOrEmpty(currentFile.GetPath()))
                            paths.Add(currentFile.GetPath());
                    passDirs(dir);
                }
            });

            foreach (IFile currentFile in this.Files)
                if (!string.IsNullOrEmpty(currentFile.GetPath()))
                    paths.Add(currentFile.GetPath());

            passDirs(this);

            return paths.ToArray();
        }

        /// <summary>
        /// Create virtual directories and files
        /// </summary>
        /// <param name="data">String-Array which contains pathes of the directories and files</param>
        public void AddPathesWithFiles(string[] data)
        {
            string[] directorys = new string[data.Length];
            for (int i = 0; i <= data.Length - 1; i++)
            {
                string[] splittedData = data[i].Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                for (int s = 0; s <= splittedData.Length - 2; s++)
                    directorys[i] += splittedData[s] + @"\";
            }
            this.AddPathes(directorys);
            this.AddFiles(data);

            this.throwChangedEvent(Type.AddedPath);
        }

        /// <summary>
        /// Removes a directory
        /// </summary>
        /// <param name="dir">The instance of the directory</param>
        /// <returns></returns>
        public bool Remove(IDirectory dir)
        {
            return this.Remove(dir.ToFullPath());
        }

        /// <summary>
        /// Removes a directroy
        /// </summary>
        /// <param name="path">The path where the directory is stored</param>
        /// <returns></returns>
        public bool Remove(string path)
        {
            if (!path.Contains(@"\"))
            {
                int index_ = this.IndexOf(path);
                if (index_ != -1)
                    this.SubDirs.RemoveAt(index_);
                return index_ != -1;
            }

            string[] segments = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

            IDirectory firstNode = this;

            foreach (IDirectory subDir in firstNode.GetSubDirectories())
            {
                for (int i = 0; i <= segments.Length - 1; i++)
                {
                    if (segments[i] == subDir.GetName())
                    {
                        firstNode = subDir;
                        break;
                    }
                }
            }            

            if (firstNode != null && firstNode.GetParent() != null)
            {
                firstNode.GetParent().GetSubDirectories().Remove(firstNode);
                this.throwChangedEvent(Type.DeletedDirectory);
            }
            else
                return false;

            return true;
        }

        /// <summary>
        /// Returns true if this directory contains a subdirectory with the given name
        /// </summary>
        /// <param name="dir">Name of the subdirectory</param>
        /// <returns></returns>
        public bool Contains(string dir)
        {
            foreach (IDirectory currentDir in this.SubDirs)
                if (currentDir.GetName() == dir)
                    return true;
            return false;
        }

        /// <summary>
        /// Returns the index of the given dir
        /// </summary>
        /// <param name="dir">The directory from which you want to get the index (Be careful: This is the index of the list not the unique index)</param>
        /// <returns>-1 if this directory doesn't contains the given directory </returns>
        public int IndexOf(string dir)
        {
            for (int i = 0; i <= this.SubDirs.Count() - 1; i++)
                if (this.SubDirs[i].GetName() == dir)
                    return i;
            return -1;
        }

        /// <summary>
        /// Adds a file to this directory - without any content
        /// </summary>
        /// <param name="fileName">The filename which will be used to added this file to this directory</param>
        public async Task AddFile(string fileName)
        {
            await this.AddFiles(new string[] { fileName });
        }

        /// <summary>
        /// Add files to this directory - without any content
        /// </summary>
        /// <param name="fileNames">The filesname which will be used to added these files to this directory</param>
        public async Task AddFiles(string[] fileNames)
        {
            await Task.Run(delegate
            {
                foreach (string currentFileName in fileNames)
                {
                    if (!currentFileName.Contains(@"\"))
                    {
                        this.Files.Add(new File(currentFileName, this));
                        continue;
                    }

                    IDirectory currentNode = this;
                    string[] segments = currentFileName.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i <= segments.Length - 2; i++)
                    {
                        if (currentNode.Contains(segments[i]))
                            currentNode = currentNode.GetSubDirectories()[currentNode.IndexOf(segments[i])];
                        else
                            break;
                    }
                    if (segments.Length > 0 && !(new File(string.Empty, new Directory(string.Empty)).Contains(currentNode.GetFiles(), String.Join(@"\", segments))))
                    {
                        IFile currentFile = new File(segments[segments.Length - 1], currentNode);
                        currentNode.GetFiles().Add(currentFile);
                    }
                }

                this.throwChangedEvent(Type.AddedFile);
            });
        }

        /// <summary>
        /// Create the virtual directories from the pathes given in data-array
        /// </summary>
        /// <param name="data">String-array that contains pathes</param>
        public void AddPathes(string[] data)
        {
            foreach (string dirName in data)
            {
                string[] segments = dirName.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                IDirectory currentNode = this;
                bool goOn = false;
                int i = 0;
                if (currentNode.GetSubDirectories().Count() > 0)
                {
                    for (; i <= segments.Length - 1; i++)
                    {
                        string currentName = segments[i];
                        goOn = currentNode.Contains(currentName);
                        if (!goOn)
                            break;
                        int indexSubDir = currentNode.IndexOf(currentName);
                        currentNode = currentNode.GetSubDirectories()[indexSubDir];
                    }
                }
                if (!goOn)
                {
                    for (int j = i; j <= segments.Length - 1; j++)
                    {
                        string currentName = segments[j];
                        IDirectory nd = new Directory(currentName);
                        nd.SetParent(currentNode);
                        currentNode.GetSubDirectories().Add(nd);
                        int indexSubDir = currentNode.IndexOf(currentName);
                        currentNode = currentNode.GetSubDirectories()[indexSubDir];
                    }
                }
            }
            this.throwChangedEvent(Type.AddedPath);
        }

        /// <summary>
        /// Returns the name of this directory
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.Name;
        }

        /// <summary>
        /// Sets the name of the directory (Just necessary because of IDirectory)
        /// </summary>
        /// <param name="name">Name which will be set</param>
        public void SetName(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Returns a list of all subdirectories (TopLevel)
        /// </summary>
        /// <returns></returns>
        public List<IDirectory> GetSubDirectories()
        {
            return this.SubDirs;
        }

        /// <summary>
        /// Returns list of all files in this directory
        /// </summary>
        /// <returns></returns>
        public List<IFile> GetFiles()
        {
            return this.Files;
        }

        /// <summary>
        /// Returns the unique index of this directory
        /// </summary>
        /// <returns></returns>
        public int GetIndex()
        {
            return this.Index;
        }

        /// <summary>
        /// Returns the files the directory has
        /// </summary>
        /// <returns></returns>
        public IDirectory GetParent()
        {
            return this.Parent;
        }

        /// <summary>
        /// You can set the owner of this directory
        /// </summary>
        /// <param name="parent">Direcotry instance which will be the owner of this directory</param>
        public void SetParent(IDirectory parent)
        {
            this.Parent = parent;
        }

        /// <summary>
        /// Creates the whole path of this directory
        /// </summary>
        /// <param name="parent">The path from this to parent. If parent is null, you get the full path. Otherwise you get the path to the parent directory</param>
        /// <returns>The full path of this directory</returns>
        public string ToFullPath(IDirectory parent = null)
        {
            Action<IDirectory> searchAction = null;
            List<string> paths = new List<string>();

            searchAction = new Action<IDirectory>((IDirectory dir) =>
            {
                if (dir.GetParent() == parent)
                {
                    paths.Add(dir.GetName());
                    return;
                }

                paths.Add(dir.GetName());
                searchAction(dir.GetParent());
            });
            searchAction(this);
            paths.Reverse();

            return String.Join(@"\", paths);
        }

        /// <summary>
        /// Returns the last directory of the path (directory after last "backslash" ("\"))
        /// </summary>
        /// <param name="path">The path to work with</param>
        /// <returns></returns>
        public IDirectory CalculateLastNode(string path)
        {
            return this.CalculateLastNode(this, path);
        }

        /// <summary>
        /// Returns the last directory of the path (directory after last "backslash" ("\"))
        /// </summary>
        /// <param name="firstNode">The directory to start at</param>
        /// <param name="path">The path to work with</param>
        /// <returns></returns>
        public IDirectory CalculateLastNode(IDirectory firstNode, string path)
        {
            if (!path.Contains(@"\"))
            {
                int index = firstNode.IndexOf(path);
                if (index != -1)
                    return firstNode.GetSubDirectories()[index];

                return null;
            }
            else
            {
                IDirectory startNode = firstNode;
                string[] segments = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i <= segments.Length - 1; i++)
                {
                    if (startNode.Contains(segments[i]))
                        startNode = startNode.GetSubDirectories()[startNode.IndexOf(segments[i])];
                    else
                        return null;
                }

                return startNode;
            }
        }
    }
}
