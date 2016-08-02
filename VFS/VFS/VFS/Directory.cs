// ------------------------------------------------------------------------
// Directory.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      11.04.2016
// Last update on:  01.08.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace VFS
{
    /// <summary>
    /// Represents a virtual directory
    /// </summary>
    public class Directory
    {
        /// <summary>
        /// The name of the directory
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// The directorys which are this directory contains
        /// </summary>
        public List<Directory> SubDirs = new List<Directory>();

        /// <summary>
        /// The files of this directory
        /// </summary>
        public List<File> Files = new List<File>();

        /// <summary>
        /// The index to identify each folder right
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// The parent directory of this directory
        /// </summary>
        private Directory parent = null;

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
            /// 
            /// </summary>           
            //Refresh,
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
        public Directory Parent
        {
            get
            {
                return this.parent;
            }
            private set
            {
                this.parent = value;
                this.Parent.OnChanged += this.OnChanged;
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
        /// Creates the whole path of this directory
        /// </summary>
        /// <param name="parent">The path from this to parent. If parent is null, you get the full path. Otherwise you get the path to the parent directory</param>
        /// <returns>The full path of this directory</returns>
        public string ToFullPath(Directory parent = null)
        {
            Action<Directory> searchAction = null;
            List<string> paths = new List<string>();

            searchAction = new Action<Directory>((Directory dir) =>
            {
                if (dir.parent == parent)
                {
                    paths.Add(dir.Name);
                    return;
                }

                paths.Add(dir.Name);
                searchAction(dir.parent);
            });
            searchAction(this);
            paths.Reverse();

            return String.Join(@"\", paths);
        }

        /// <summary>
        /// Creates a string array of all pathes
        /// </summary>
        /// <returns></returns>
        public string[] ToStringArray()
        {
            List<string> paths = new List<string>();

            Action<Directory> passDirs = null;
            Directory node = this;

            passDirs = new Action<Directory>((Directory currentDir) => {
                foreach (Directory dir in currentDir.SubDirs)
                {
                    paths.Add(dir.ToFullPath());
                    passDirs(dir);
                }
            });

            passDirs(node);
            return paths.ToArray();
        }

        public string[] ToFileStringArray()
        {
            List<string> paths = new List<string>();

            Action<Directory> passDirs = null;
            Directory node = this;

            passDirs = new Action<Directory>((Directory currentDir) => {
                foreach (Directory dir in currentDir.SubDirs)
                {
                    foreach (File currentFile in dir.Files)
                        if (!string.IsNullOrEmpty(currentFile.Path))
                            paths.Add(currentFile.Path);
                    passDirs(dir);
                }
            });

            foreach (File currentFile in this.Files)
                if (!string.IsNullOrEmpty(currentFile.Path))
                    paths.Add(currentFile.Path);

            passDirs(this);

            return paths.ToArray();
        }

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

        public bool Remove(Directory dir)
        {
            return this.Remove(dir.ToFullPath());
        }

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

            Directory firstNode = this;
            int index = 0;

            while (index <= segments.Length - 1)
            {
                foreach (Directory subDir in firstNode.SubDirs)
                {
                    if (segments[index] == subDir.Name)
                    {
                        firstNode = subDir;
                        index++;
                        break;
                    }
                }
            }

            if (firstNode != null && firstNode.Parent != null)
            {
                firstNode.Parent.SubDirs.Remove(firstNode);
                this.throwChangedEvent(Type.DeletedDirectory);
            }
            else
                return false;

            return true;
        }

        public bool Contains(string dir)
        {
            foreach (Directory currentDir in this.SubDirs)
                if (currentDir.Name == dir)
                    return true;
            return false;
        }

        public int IndexOf(string dir)
        {
            for (int i = 0; i <= this.SubDirs.Count() - 1; i++)
                if (this.SubDirs[i].Name == dir)
                    return i;
            return -1;
        }

        public void AddFile(string fileName)
        {
            this.AddFiles(new string[] { fileName });
        }

        public void AddFiles(string[] fileName)
        {
            foreach (string currentFileName in fileName)
            {
                if (!currentFileName.Contains(@"\"))
                {
                    this.Files.Add(new File(currentFileName, this));
                    continue;
                }

                Directory currentNode = this;
                string[] segments = currentFileName.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i <= segments.Length - 2; i++)
                {
                    if (currentNode.Contains(segments[i]))
                        currentNode = currentNode.SubDirs[currentNode.IndexOf(segments[i])];
                    else
                        break;
                }
                if (segments.Length > 0 && !File.Contains(currentNode.Files, String.Join(@"\", segments)))
                {
                    File currentFile = new File(segments[segments.Length - 1], currentNode);
                    currentNode.Files.Add(currentFile);
                }
            }

            this.throwChangedEvent(Type.AddedFile);
        }

        public void AddPathes(string[] path)
        {
            foreach (string dirName in path)
            {
                string[] segments = dirName.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                Directory currentNode = this;
                bool goOn = false;
                int i = 0;
                if (currentNode.SubDirs.Count() > 0)
                {
                    for (; i <= segments.Length - 1; i++)
                    {
                        string currentName = segments[i];
                        goOn = currentNode.Contains(currentName);
                        if (!goOn)
                            break;
                        int indexSubDir = currentNode.IndexOf(currentName);
                        currentNode = currentNode.SubDirs[indexSubDir];
                    }
                }
                if (!goOn)
                {
                    for (int j = i; j <= segments.Length - 1; j++)
                    {
                        string currentName = segments[j];
                        Directory nd = new Directory(currentName);
                        nd.Parent = currentNode;
                        currentNode.SubDirs.Add(nd);
                        int indexSubDir = currentNode.IndexOf(currentName);
                        currentNode = currentNode.SubDirs[indexSubDir];
                    }
                }
            }
            this.throwChangedEvent(Type.AddedPath);
        }

        public static Directory CalculateLastNode(Directory firstNode, string path)
        {
            if (!path.Contains(@"\"))
            {
                int index = firstNode.IndexOf(path);
                if (index != -1)
                    return firstNode.SubDirs[index];

                return null;
            }
            else
            {
                Directory startNode = firstNode;
                string[] segments = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i <= segments.Length - 1; i++)
                {
                    if (startNode.Contains(segments[i]))
                        startNode = startNode.SubDirs[startNode.IndexOf(segments[i])];
                    else
                        return null;
                }

                return startNode;
            }
        }
    }
}
