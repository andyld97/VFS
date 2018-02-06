// ------------------------------------------------------------------------
// ExtendedDirectory.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      17.12.2016
// Last update on:  30.06.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFS.Interfaces;
using VFS.Storage;

namespace VFS.ExtendedVFS
{
    /// <summary>
    /// Directory which is modified to store instances of ModifiedFiles
    /// </summary>
    public class ExtendedDirectory : IDirectory
    {
        private IDirectory instanceDir = null;
        private ExtendedDirectory parent = null;
        private string Name = string.Empty;
        private IStorage storage = null;

        /// <summary>
        /// An empty file to use the instance-methods which are non-static
        /// </summary>
        public static ExtendedFile NULLFILE = new ExtendedFile(new HeaderInfo(), new ExtendedDirectory("", null), null);

        /// <summary>
        /// Instantiates a new ModifiedDirectory
        /// </summary>
        /// <param name="name">The name of the virtual directory which will be created</param>
        public ExtendedDirectory(string name, IStorage storage)
        {
            this.storage = storage;
            this.instanceDir = new Directory(name);
            this.Name = name;
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
            foreach (string currentFileName in fileNames)
            {
                if (!currentFileName.Contains(@"\"))
                {
                    string[] segements = currentFileName.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    var currentFile = new ExtendedFile(HeaderInfo.FromString(segements[segements.Length - 1]), this, storage);
                    await currentFile.Initalize();
                    this.GetFiles().Add(currentFile);
                    continue;
                }

                ExtendedDirectory currentNode = this;
                string[] segments = currentFileName.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i <= segments.Length - 2; i++)
                {
                    if (currentNode.Contains(segments[i]))
                        currentNode = (ExtendedDirectory)currentNode.GetSubDirectories()[currentNode.IndexOf(segments[i])];
                    else
                        break;
                }
                if (segments.Length > 0 && !NULLFILE.Contains(currentNode.GetFiles(), String.Join(@"\", segments)))
                {
                    ExtendedFile cF = new ExtendedFile(HeaderInfo.FromString(segments[segments.Length - 1]), currentNode, storage);
                    await cF.Initalize();
                    currentNode.GetFiles().Add(cF);
                }
            }
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
                        IDirectory nd = new ExtendedDirectory(currentName, storage);
                        nd.SetParent(currentNode);
                        currentNode.GetSubDirectories().Add(nd);
                        int indexSubDir = currentNode.IndexOf(currentName);
                        currentNode = currentNode.GetSubDirectories()[indexSubDir];
                    }
                }
            }
        }

        /// <summary>
        /// Create virtual directories and files
        /// </summary>
        /// <param name="data">String-Array which contains pathes of the directories and files</param>
        public async Task AddPathesWithFiles(string[] data)
        {
            string[] directorys = new string[data.Length];
            for (int i = 0; i <= data.Length - 1; i++)
            {
                string[] splittedData = data[i].Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                for (int s = 0; s <= splittedData.Length - 2; s++)
                    directorys[i] += splittedData[s] + @"\";
            }
            this.AddPathes(directorys);
            await this.AddFiles(data);
        }

        /// <summary>
        /// Returns the last directory of the path (directory after last "backslash" ("\"))
        /// </summary>
        /// <param name="path">The path to work with</param>
        /// <returns></returns>
        public IDirectory CalculateLastNode(string path)
        {
            return this.instanceDir.CalculateLastNode(path);
        }

        /// <summary>
        /// /// Returns the last directory of the path (directory after last "backslash" ("\"))
        /// </summary>
        /// <param name="firstNode">The directory to start at</param>
        /// <param name="path">The path to work with</param>
        /// <returns></returns>
        public IDirectory CalculateLastNode(IDirectory firstNode, string path)
        {
            return this.instanceDir.CalculateLastNode(firstNode, path);
        }

        /// <summary>
        /// Returns true if this directory contains a subdirectory with the given name
        /// </summary>
        /// <param name="dir">Name of the subdirectory</param>
        /// <returns></returns>
        public bool Contains(string dir)
        {
            return this.instanceDir.Contains(dir);
        }

        /// <summary>
        /// Returns the files the directory has
        /// </summary>
        /// <returns></returns>
        public List<IFile> GetFiles()
        {
            return this.instanceDir.GetFiles();
        }

        /// <summary>
        /// Returns the unique index of this directory
        /// </summary>
        /// <returns></returns>
        public int GetIndex()
        {
            return this.instanceDir.GetIndex();
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
        /// Returns the parent directory
        /// </summary>
        /// <returns></returns>
        public IDirectory GetParent()
        {
            return this.parent;
        }

        /// <summary>
        /// Returns a list of all subdirectories (TopLevel)
        /// </summary>
        /// <returns></returns>
        public List<IDirectory> GetSubDirectories()
        {
            return this.instanceDir.GetSubDirectories();
        }

        /// <summary>
        /// Returns the index of the given dir
        /// </summary>
        /// <param name="dir">The directory from which you want to get the index (Be careful: This is the index of the list not the unique index)</param>
        /// <returns>-1 if this directory doesn't contains the given directory </returns>
        public int IndexOf(string dir)
        {
            return this.instanceDir.IndexOf(dir);
        }

        /// <summary>
        /// Removes a directroy
        /// </summary>
        /// <param name="path">The path where the directory is stored</param>
        /// <returns></returns>
        public bool Remove(string path)
        {
            return this.instanceDir.Remove(path);
        }

        /// <summary>
        /// Removes a directory
        /// </summary>
        /// <param name="dir">The instance of the directory</param>
        /// <returns></returns>
        public bool Remove(IDirectory dir)
        {
            return this.instanceDir.Remove(dir);
        }

        /// <summary>
        /// Sets the name of the directory (Just necessary because of IDirectory)
        /// </summary>
        /// <param name="name">Name which will be set</param>
        public void SetName(string name)
        {
            this.Name = name;
            this.instanceDir.SetName(name);
        }

        /// <summary>
        /// You can set the owner of this directory
        /// </summary>
        /// <param name="parent">Direcotry instance which will be the owner of this directory</param>
        public void SetParent(IDirectory parent)
        {
            this.parent = (ExtendedDirectory)parent;
        }

        /// <summary>
        /// Returns ALL files in each and every directory
        /// </summary>
        /// <returns></returns>
        public string[] ToFileStringArray()
        {
            return this.instanceDir.ToFileStringArray();
        }

        /// <summary>
        /// Creates the full path till parent, if parent == null, then till start
        /// </summary>
        /// <param name="parent">The directory instance to get a special path from end to parent</param>
        /// <returns></returns>
        public string ToFullPath(IDirectory parent = null)
        {
            Action<ExtendedDirectory> searchAction = null;
            List<string> paths = new List<string>();

            searchAction = new Action<ExtendedDirectory>((ExtendedDirectory dir) =>
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

            string finalValue = String.Join(@"\", paths);
            if (string.IsNullOrEmpty(finalValue))
                return @"\";

            return finalValue;
        }

        /// <summary>
        /// Returns all pathes of ALL directories in each and every directory
        /// </summary>
        /// <returns></returns>
        public string[] ToStringArray()
        {
            return this.instanceDir.ToStringArray();
        }

        /// <summary>
        /// Returns the file instance by path
        /// </summary>
        /// <param name="path">The path where the file is stored at</param>
        /// <param name="rootDir">The main directory (necessary, because static)</param>
        /// <returns></returns>
        public static ExtendedFile ByPath(string path, ExtendedDirectory rootDir)
        {
            string[] segements = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            ExtendedDirectory startNode = rootDir;
            for (int i = 0; i <= segements.Length - 2; i++)
            {
                string currentSegement = segements[i];
                if (startNode.Contains(currentSegement))
                    startNode = (ExtendedDirectory)startNode.GetSubDirectories()[startNode.IndexOf(currentSegement)];
                else
                    return null;
            }

            return (ExtendedFile)NULLFILE.ByPath(startNode.GetFiles(), path);
        }
    }
}
