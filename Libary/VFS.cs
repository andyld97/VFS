// ------------------------------------------------------------------------
// VFS.cs written by Code A Software (http://www.code-a-software.net)
// SP: VHP-0001 (OpenSource-Software)
// Created on:      11.04.2016
// Last update on:  01.01.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFS.Interfaces;
using VFS.Language;

namespace VFS
{
    /// <summary>
    /// Represents the Virtual File System which consists of File and Directory-class
    /// </summary>
    public class VFS
    {
        #region VAR

        #region Private
        private List<byte> data = new List<byte>();
        #endregion

        #region Protected
        /// <summary>
        /// The root directory
        /// </summary>
        protected IDirectory rootDir = new Directory("");

        /// <summary>
        /// 
        /// </summary>
        protected string logPath = string.Empty, savePath = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        protected Log lgInstance = null;
        #endregion

        #region Public

        /// <summary>
        /// This counter describes how many PackBytes are used to identify files
        /// </summary>
        public readonly int MainCounter = 128;

        /// <summary>
        /// The byte ("-") which is needed for wrapping files and directories in one file
        /// </summary>
        public readonly int PackByte = 45;

        /// <summary>
        /// The unique directory index, e.g. if the name of to directories are the same (but different path)
        /// </summary>
        public static int DirIndex = 0;

        /// <summary>
        /// A File which doesn't relay to somenthing, just to use some methods which aren't static anymore (Since IFile and IDirectory-Interfaces)
        /// </summary>
        public static readonly Interfaces.IFile NULLFILE = new File(string.Empty, null);
        #endregion


        #endregion

        #region Properties
        /// <summary>
        /// Root directory of the currentFile-System
        /// </summary>
        public IDirectory RootDirectory
        {
            get
            {
                return rootDir;
            }
            set
            {
                rootDir = value;
            }
        }
        #endregion

        #region Ctor
        /// <summary>
        /// Creates a new virtual file system.
        /// </summary>
        /// <param name="logPath">Path for file where the log should be</param>
        /// <param name="savePath">Path for storing the VFS</param>
        /// /// <param name="MainCounter">The amout of seperator chars</param>
        /// /// <param name="PackByte">Char type from 1 to 255 for seperator</param>
        public VFS(string logPath, string savePath, int MainCounter = 128, int PackByte = 45)
        {
            // bootSect contains the first two items of the array.
            // bootSect[FS_DIR] contains all directorys and bootSect[FS_FILE] all files.
            this.savePath = savePath;
            this.MainCounter = MainCounter;
            this.PackByte = PackByte;

            // Init log!
            this.logPath = logPath;
            this.lgInstance = new Log(this.logPath, Log.Priority.ALL);

            // Reset dirIndex!
            VFS.DirIndex = 0;

            // Add event
            (this.rootDir as Directory).OnChanged += RootDir_OnChanged;
        }
        #endregion

        #region Events
        /// <summary>
        /// This event is called when thread finishes.
        /// </summary>
        public delegate void onReady();

        /// <summary>
        /// This event is called when thread finishes.
        /// </summary>
        public event onReady OnReady;

        /// <summary>
        /// This event is called when a change was done in the system
        /// </summary>
        public delegate void onSaved();

        /// <summary>
        /// This event is called when a change was done in the system
        /// </summary>
        public event onSaved OnSaved;

        private void RootDir_OnChanged(Directory.Type action)
        {
            //    this.Save();
            this.lgInstance.Add(Localization.RECIEVED_CHANGE, new string[] { "Action: " + action }, string.Empty);
        }
        #endregion

        #region Methods

        #region Private

        /// <summary>
        /// Returns a byte-array from input string with default encoding
        /// </summary>
        /// <param name="data">The input string</param>
        /// <returns></returns>
        protected byte[] calculateFrom(string data)
        {
            return System.Text.Encoding.Default.GetBytes(data);
        }

        /// <summary>
        /// Returns a string from byte-array with default encoding
        /// </summary>
        /// <param name="data">The input byte-array</param>
        /// <returns></returns>
        protected string calculateFrom(byte[] data)
        {
            if (data == null)
                return string.Empty;
            return System.Text.Encoding.Default.GetString(data);
        }

        /// <summary>
        /// Generates a string - based on MainCounter and PackByte to separate the files
        /// </summary>
        /// <returns></returns>
        protected string generateString()
        {
            string splitFiles = string.Empty;
            for (int i = 0; i <= MainCounter - 1; i++)
                splitFiles += Convert.ToChar(this.PackByte);
            return splitFiles;
        }

        /// <summary>
        /// Generates a byte-array - based on MainCounter and PackByte to separate the files
        /// </summary>
        /// <returns></returns>
        protected byte[] generateBytes()
        {
            return this.calculateFrom(this.generateString());
        }

        /// <summary>
        /// Formatting the path to the right format
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string FormatPath(string path)
        {
            if (path[0] == '\\')
            {
                string nPath = string.Empty;
                for (int i = 1; i <= path.Length - 1; i++)
                    nPath += path[i];

                return nPath;
            }
            else
                return path;
        }

        /// <summary>
        /// Returns true if the next count-bytes are all equal to PackByte
        /// </summary>
        /// <param name="count"></param>
        /// <param name="byte_"></param>
        /// <param name="index"></param>
        /// <param name="btArray"></param>
        /// <returns></returns>
        protected bool checkNextItems(int count, int byte_, int index, byte[] btArray)
        {
            bool check = false;
            int x = 0;
            for (int i = index; i <= index + count - 1; i++)
            {
                if (i >= btArray.Length - 1)
                    return false;
                if (btArray[i] == byte_)
                {
                    if (x == count)
                        return true;
                    check = true;
                    x++;
                }
                else
                    return false;
            }
            return check;
        }
        #endregion

        #region Public 
        /// <summary>
        /// Creates a file
        /// </summary>
        /// <param name="content">File content</param>
        /// <param name="name">File name</param>
        /// <param name="dir">Stored directory</param>
        /// <param name="overrideExisting">Determines whether a file which exists should be overriden or not</param>
        /// <returns></returns>
        public virtual bool WriteAllText(string content, string name, IDirectory dir, bool overrideExisting = false)
        {
            return this.WriteAllBytes(this.calculateFrom(content), name, dir, overrideExisting);
        }

        /// <summary>
        /// Creates a file
        /// </summary>
        /// <param name="data">File content in bytes</param>
        /// <param name="name">File name</param>
        /// <param name="dir">Stored directory</param>
        /// <param name="overrideExisting">Determines whether a file which exists should be overriden or not</param>
        /// <returns></returns>
        public virtual bool WriteAllBytes(byte[] data, string name, IDirectory dir, bool overrideExisting = false)
        {
            bool condition = (data != null && !string.IsNullOrEmpty(name) && dir != null && dir.GetFiles() != null);
            string filePath = dir.ToFullPath() + @"\" + name;

            if (condition)
            {
                if (!NULLFILE.Contains(dir.GetFiles(), filePath))
                {
                    IFile currentFile = new File(name, dir);
                    currentFile.SetByes(data.ToList<byte>());
                    dir.GetFiles().Add(currentFile);

                    this.lgInstance.Add(Localization.ADDED_FILE, new string[] { "File: " + name, "Path: " + filePath }, string.Empty);
                }
                else
                {
                    IFile currentFile = NULLFILE.ByPath(dir.GetFiles(), filePath);
                    if (currentFile != null)
                    {
                        this.lgInstance.Add(Localization.FILE_EXISTS, new string[] { "File: " + name, "Path: " + filePath }, string.Empty);
                        if (currentFile.GetBytes().Count == 0)
                        {
                            currentFile.SetByes(data.ToList<byte>());
                            if (data.Length != 0)
                                this.lgInstance.Add(Localization.FILE_BYTES_EMPTY, new string[] { "Bytes:" + this.calculateFrom(new byte[] { data[0] }) + this.calculateFrom(new byte[] { data[1] }), string.Empty }, string.Empty);
                            return true;
                        }
                        else
                        {
                            if (overrideExisting)
                            {
                                currentFile.SetByes(data.ToList<byte>());
                                string param = string.Empty;
                                if (data.Count() >= 2)
                                    param = this.calculateFrom(new byte[] { data[0] }) + this.calculateFrom(new byte[] { data[1] });
                                this.lgInstance.Add(Localization.ADD_FILES_TO_BYTE, new string[] { "Bytes:" + param }, string.Empty);
                                return true;
                            }
                            else
                            {
                                this.lgInstance.Add(Localization.FILE_NOT_EXISTS, new string[] { "File: " + name, "Path: " + filePath }, string.Empty);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        this.lgInstance.Add(Localization.FILE_NOT_EXISTS, new string[] {  }, string.Empty);
                        return false;
                    }
                }
            }

            return condition;
        }

        /// <summary>
        /// Writes file with full path starting from root-directory.
        /// </summary>
        /// <param name="data">Byte-Array with data</param>
        /// <param name="path">Full path</param>
        /// <param name="overrideExisting">Whether an existing file should be replaced by another one</param>
        /// <returns></returns>
        public virtual bool WriteAllBytes(byte[] data, string path, bool overrideExisting = false)
        {
            IDirectory currentNode = this.rootDir;

            if (!path.Contains(@"\"))
            {
                File currentFile = (File)Activator.CreateInstance(typeof(File), new object[] { path, this.rootDir });
                currentFile.Bytes = data.ToList<byte>();
                this.rootDir.GetFiles().Add(currentFile);
                return true;
            }

            string[] segments = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

            for (int x = 0; x <= segments.Length - 2; x++)
            {
                if (currentNode.Contains(segments[x]))
                    currentNode = currentNode.GetSubDirectories()[currentNode.IndexOf(segments[x])];
                else
                    return true;
            }

            if (segments.Length > 0)
            {
                this.WriteAllBytes(data.ToArray(), segments[segments.Length - 1], currentNode, true);
                return true;
            }
            else
            {
                this.lgInstance.Add(Localization.PATH_NOT_EXISTS, new string[] { }, string.Empty);
                return false;
            }
        }

        /// <summary>
        /// Removes a file
        /// </summary>
        /// <param name="path">The virtual path where the file is stored</param>
        /// <param name="startNode">The directory where the path is beginning</param>
        /// <returns></returns>
        public virtual bool RemoveFile(string path, IDirectory startNode)
        {
            // Search suitable Directory instance and check if the file is there and then delete it.
            if (!path.Contains(@"\"))
            {
                string nPath = startNode.ToFullPath() + @"\" + path;
                if (NULLFILE.Contains(startNode.GetFiles(), nPath))
                {
                    IFile currentFile = NULLFILE.ByPath(startNode.GetFiles(), nPath);
                    if (currentFile != null)
                    {
                        startNode.GetFiles().Remove(currentFile);
                        return true;
                    }
                    else
                    {
                        this.lgInstance.Add(Localization.FILE_NOT_EXISTS, new string[] { "FILE Path: " + startNode.ToFullPath() }, string.Empty);
                        return false;
                    }
                }
                else
                {
                    this.lgInstance.Add(Localization.FILE_NOT_EXISTS, new string[] { "FILE Path: " + startNode.ToFullPath() }, string.Empty);
                    return false;
                }
            }

            string[] segments = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            IDirectory currentNode = startNode;

            for (int i = 0; i <= segments.Length - 2; i++)
            {
                if (currentNode.Contains(segments[i]))
                    currentNode = currentNode.GetSubDirectories()[currentNode.IndexOf(segments[i])];
                else
                {
                    this.lgInstance.Add(Localization.FILE_NOT_EXISTS, new string[] { "FILE Path: " + currentNode.ToFullPath() }, string.Empty);
                    return false;
                }
            }

            if (NULLFILE.Contains(currentNode.GetFiles(), path))
            {
                IFile currentFile = NULLFILE.ByPath(currentNode.GetFiles(), path);
                if (currentFile != null)
                {
                    currentNode.GetFiles().Remove(currentFile);
                    return true;
                }
                else
                {
                    this.lgInstance.Add(Localization.FILE_NOT_EXISTS, new string[] { "FILE Path: " + currentNode.ToFullPath() }, string.Empty);
                    return false;
                }
            }
            else
            {
                this.lgInstance.Add(Localization.FILE_NOT_EXISTS, new string[] { "FILE Path: " + currentNode.ToFullPath() }, string.Empty);
                return false;
            }
        }

        /// <summary>
        /// Writes a file from a stream
        /// </summary>
        /// <param name="name">File name</param>
        /// <param name="dir">Direcotry which holds the foöe</param>
        /// <param name="stream">Input stream</param>
        /// <param name="overrideExisting">Whether a file should be replaced, if it exists already</param>
        /// <returns></returns>
        public virtual bool WriteStream(string name, IDirectory dir, System.IO.Stream stream, bool overrideExisting = false)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                stream.CopyTo(ms);
                return this.WriteAllBytes(ms.ToArray(), name, dir, overrideExisting);
            }
        }

        /// <summary>
        /// Returns the file content in byte-Array
        /// </summary>
        /// <param name="path">Full filepath</param>
        /// <param name="startNode">Dir, where you want to start at. It's well to use the root-Dir here!</param>
        /// <param name="different">Just to differentiate between the methods - no usage in in this method</param>
        /// <returns></returns>
        public virtual byte[] ReadAllBytes(string path, IDirectory startNode, bool different = false)
        {
            IDirectory currentNode = startNode;
            if (!path.Contains(@"\"))
            {
                if (NULLFILE.Contains(currentNode.GetFiles(), path))
                {
                    IFile currentFile = NULLFILE.ByPath(currentNode.GetFiles(), path);
                    if (currentNode != null)
                        return currentFile.GetBytes().ToArray();
                    else
                    {
                        this.lgInstance.Add(Localization.FILE_NOT_EXISTS, new string[] { "FILE Path: " + path }, string.Empty);
                        return null;
                    }
                }
                else
                    this.lgInstance.Add(Localization.FILE_NOT_EXISTS, new string[] { "FILE Path: " + path }, string.Empty);
            }
            string[] segments = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i <= segments.Length - 2; i++)
            {
                string currentName = segments[i];
                int nIndex = currentNode.IndexOf(currentName);

                if (nIndex != -1)
                    currentNode = currentNode.GetSubDirectories()[nIndex];
                else
                {
                    this.lgInstance.Add(Localization.FILE_NOT_EXISTS, new string[] { "FILE Path: " + path }, string.Empty);
                    return null;
                }
            }

            if (currentNode != null)
            {
                if (NULLFILE.Contains(currentNode.GetFiles(), path))
                {
                    IFile currentFile = NULLFILE.ByPath(currentNode.GetFiles(), path);
                    return currentFile.GetBytes().ToArray();
                }
            }

            this.lgInstance.Add(Localization.FILE_NOT_EXISTS, new string[] { "Path: " + path }, string.Empty);
            return null;
        }

        /// <summary>
        /// Returns the file content
        /// </summary>
        /// <param name="path">Filename</param>
        /// <param name="startNode">Directory where the path is beginning at</param>
        /// <returns></returns>
        public virtual string ReadAllText(string path, IDirectory startNode)
        {
            return this.calculateFrom(this.ReadAllBytes(path, startNode, true));
        }

        /// <summary>
        /// Returns true if the file is in the RAM.
        /// </summary>
        /// <param name="path">If the path doesn't contains a \, it's just a file name</param>
        /// <param name="startNode">When you choose full path, you have to use root-Dir</param>
        /// <returns></returns>
        public virtual bool FileExists(string path, IDirectory startNode)
        {
            if (!path.Contains(@"\"))
                return NULLFILE.ByPath(startNode.GetFiles(), path) != null;
            else
            {
                IDirectory currentNode = startNode;
                string[] segments = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i <= segments.Length - 2; i++)
                {
                    string currentSegment = segments[i];

                    if (currentNode.Contains(currentSegment))
                        currentNode = currentNode.GetSubDirectories()[currentNode.IndexOf(currentSegment)];
                    else
                        return false;
                }
                return NULLFILE.ByPath(currentNode.GetFiles(), currentNode.ToFullPath() + @"\" + segments[segments.Length - 1]) != null;
            }
        }

        /// <summary>
        /// Writes filesystem to physical file
        /// </summary>
        /// <returns></returns>
        public virtual bool Save()
        {
            // Refresh byte array // | // <,
            string bootSector = String.Join("|", this.RootDirectory.ToFileStringArray()) + "|<" + String.Join(",", this.RootDirectory.ToStringArray()) + ",>" + this.generateString();

            // Add archiv structure
            this.data.Clear();

            // Add file bytes to array
            // Merge all infos to byte-Array
            List<byte> final = new List<byte>();
            final.AddRange(this.calculateFrom(bootSector));

            string[] elements = this.RootDirectory.ToFileStringArray();
            for (int i = 0; i <= elements.Length - 1; i++)
            {
                string str = elements[i];
                if (!this.FormatPath(str).Contains(@"\"))
                {
                    if (NULLFILE.Contains(this.rootDir.GetFiles(), str))
                    {
                        data.AddRange(NULLFILE.ByPath(rootDir.GetFiles(), str).GetBytes());
                        if (i != elements.Length - 1)
                            data.AddRange(this.generateBytes());
                    }                    
                    continue;
                }
                
                string[] segemnts = str.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                string nPath = string.Empty;
                for (int j = 0; j <= segemnts.Length - 2; j++)
                    nPath += segemnts[j] + @"\";

                Directory currentDir = (Directory)this.rootDir.CalculateLastNode(nPath);
                if (currentDir != null && currentDir.Files != null && NULLFILE.Contains(currentDir.Files, str))
                {
                    data.AddRange(NULLFILE.ByPath(currentDir.Files, str).GetBytes());
                    if (i != elements.Length - 1)
                        data.AddRange(this.generateBytes());
                }
            }        
                      
            final.AddRange(this.data);

            this.data.Clear();
            System.IO.File.WriteAllBytes(this.savePath, final.ToArray());

            if (this.OnSaved != null)
                this.OnSaved();
            return true;
        }

        /// <summary>
        /// Loads the content of the file into this instance
        /// </summary>
        /// <param name="filePath">The path where the file is stored</param>
        public virtual void Read(string filePath)
        {
            // Read archiv format.
            // Doing this in a thread
            System.Threading.Thread thr = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                if (System.IO.File.Exists(filePath))
                {
                    List<List<byte>> getBytes = new List<List<byte>>();
                    int s = 0;
                    getBytes.Add(new List<byte>());

                    byte[] sy = System.IO.File.ReadAllBytes(filePath);
                    for (int i = 0; i <= sy.Length - 1; i++)
                    {
                        if (this.checkNextItems(MainCounter, PackByte, i, sy))
                        {
                            getBytes.Add(new List<byte>());
                            i += MainCounter - 1;
                            s++;
                        }
                        else
                            getBytes[s].Add(Convert.ToByte(sy[i]));
                    }

                    string[] files = this.calculateFrom(getBytes[0].ToArray()).Split(new string[] { "<" }, StringSplitOptions.RemoveEmptyEntries);
                    if (getBytes.Count == 1 && files.Length != 1)
                    {
                        this.lgInstance.Add(Localization.FILE_ERROR, new string[] { "FILE Path: " + filePath }, string.Empty);
                        return;
                    }

                    string[] rFiles = new string[] { };
                    string[] rFolders = new string[] { };

                    if (files.Length - 1 == 0 || files.Length - 1 == 1)
                        rFiles = files[0].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    if (files.Length - 1 == 1)
                        rFolders = files[1].Replace(">", string.Empty).Replace(">", string.Empty).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    this.rootDir.AddPathes(rFolders);
                    this.rootDir.AddFiles(rFiles);

                    string[] paths = this.rootDir.ToFileStringArray();

                    if (paths.Length - 1 == getBytes.Count() - 2)
                        for (int i = 0; i <= paths.Length - 1; i++)
                            this.WriteAllBytes(getBytes[i + 1].ToArray(), paths[i], true);
                    else
                    {
                        this.lgInstance.Add(Localization.FILE_ERROR, new string[] { "FILE Path: " + filePath }, string.Empty);
                    }
                }
                if (this.OnReady != null)
                    this.OnReady();
            }));
            thr.Start();
        }

        /// <summary>
        /// Extracts the RAM content to a physical file like Archiv does.
        /// </summary>
        /// <param name="filePath">The path of the physical file</param>
        /// <returns></returns>
        public virtual bool Extract(string filePath)
        {
            if (System.IO.Directory.Exists(filePath))
            {
                // Extract now.
                // Create directories
                Action<IDirectory> passDirs = null;

                passDirs = new Action<IDirectory>((IDirectory dir) => {

                    foreach (IDirectory currentDir in dir.GetSubDirectories())
                    {
                        string path = System.IO.Path.Combine(filePath, this.FormatPath(currentDir.ToFullPath()));
                        try
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }
                        catch (Exception e)
                        {
                            this.lgInstance.Add(Localization.IO_ERROR, new string[] { "DIR Path: " + path }, e.Message);
                        }
                        passDirs(currentDir);
                    }

                });
                passDirs(this.rootDir);

                // Create files
                foreach (File currentFile in this.rootDir.GetFiles())
                {
                    string path = System.IO.Path.Combine(filePath, this.FormatPath(currentFile.Path));
                    try
                    {
                        System.IO.File.WriteAllBytes(path, currentFile.Bytes.ToArray());
                    }
                    catch (Exception e)
                    {
                        this.lgInstance.Add(Localization.IO_ERROR, new string[] { "FILE Path: " + path }, e.Message);
                    }
                }

                Action<IDirectory> passFiles = null;

                passFiles = new Action<IDirectory>((IDirectory dir) => {
                    foreach (IDirectory currentDir in dir.GetSubDirectories())
                    {
                        foreach (IFile currentFile in currentDir.GetFiles())
                        {
                            string path = System.IO.Path.Combine(filePath, this.FormatPath(currentFile.GetPath()));
                            try
                            {
                                System.IO.File.WriteAllBytes(path, currentFile.GetBytes().ToArray());
                            }
                            catch (Exception e)
                            {
                                this.lgInstance.Add(Localization.IO_ERROR, new string[] { "FILE Path: " + path }, e.Message);
                            }
                        }
                        passFiles(currentDir);
                    }
                });

                passFiles(this.rootDir);
                return true;
            }
            else
            {
                this.lgInstance.Add(Localization.PATH_NOT_EXISTS, new string[] { "FILE Path: " + filePath }, string.Empty);
                return false;
            }
        }

        /// <summary>
        /// Extract files - but not all like .Extract(string path) does
        /// </summary>
        /// <param name="path">All file pathes</param>
        /// <param name="directoryPath">The physical directory where you want to write in</param>
        public virtual void ExtractFiles(string[] path, string directoryPath)
        {
            string[] dirs = new string[path.Length];
            for (int i = 0; i <= path.Length - 1; i++)
            {
                string[] segments = path[i].Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                for (int x = 0; x <= segments.Length - 2; x++)
                    dirs[i] += path[x];
            }

            for (int y = 0; y <= dirs.Length - 1; y++)
            {
                if (!this.FormatPath(path[y]).Contains(@"\"))
                {
                    // Those it from root dir.
                    IFile _currentFile = NULLFILE.ByPath(this.rootDir.GetFiles(), path[y]);
                    string p = System.IO.Path.Combine(directoryPath, this.FormatPath(path[y]));
                    if (_currentFile != null)
                    {
                        try
                        {
                            System.IO.File.WriteAllBytes(p, _currentFile.GetBytes().ToArray());
                        }
                        catch (Exception e)
                        {
                            this.lgInstance.Add(Localization.IO_ERROR, new string[] { "FILE Path: " + p }, e.Message);
                        }
                    }
                    else
                        this.lgInstance.Add(Localization.FILE_NOT_EXISTS, new string[] { "FILE Path: " + p }, string.Empty);
                    continue;
                }
                string currentPath = dirs[y];
                string currentPathWithFile = path[y];
                IDirectory lastNodeFromPath = this.RootDirectory.CalculateLastNode(currentPath);

                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(directoryPath, this.FormatPath(lastNodeFromPath.ToFullPath())));
                IFile currentFile = NULLFILE.ByPath(lastNodeFromPath.GetFiles(), currentPathWithFile);
                string pa = System.IO.Path.Combine(directoryPath, this.FormatPath(currentPathWithFile));

                if (currentFile != null)
                {
                    try
                    {
                        System.IO.File.WriteAllBytes(pa, currentFile.GetBytes().ToArray());
                    }
                    catch (Exception e)
                    {
                        this.lgInstance.Add(Localization.IO_ERROR, new string[] { "FILE Path: " + pa }, e.Message);
                    }
                }
                else
                    this.lgInstance.Add(Localization.FILE_NOT_EXISTS, new string[] { "FILE Path: " + pa }, string.Empty);
            }
        }


        /// <summary>
        /// Extracts a directory full with content to toPath
        /// </summary>
        /// <param name="currentDir">The direcotry</param>
        /// <param name="toPath">A vaild file path</param>
        public virtual void ExtractDirectory(IDirectory currentDir, string toPath)
        {
            if (System.IO.Directory.Exists(toPath))
            {
                Action<IDirectory> passDirs = null;

                string path = System.IO.Path.Combine(toPath, currentDir.GetName());
                try
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    this.lgInstance.Add(Localization.IO_ERROR, new string[] { "DIR Path: " + path }, e.Message);
                }


                // Create files from main dir
                foreach (IFile currentFile in currentDir.GetFiles())
                {
                    string p = System.IO.Path.Combine(path, currentFile.GetName());
                    try
                    {
                        System.IO.File.WriteAllBytes(p, currentFile.GetBytes().ToArray());
                    }
                    catch (Exception e)
                    {
                        this.lgInstance.Add(Localization.IO_ERROR, new string[] { "FILE Path: " + p }, e.Message);
                    }
                }

                passDirs = new Action<IDirectory>((IDirectory currentDirectory) =>
                {
                    foreach (IDirectory subDir in currentDirectory.GetSubDirectories())
                    {
                        string dirPath = System.IO.Path.Combine(toPath, currentDir.GetName(), subDir.ToFullPath(currentDir));
                        try
                        {
                            System.IO.Directory.CreateDirectory(dirPath);
                            foreach (IFile currentFile in subDir.GetFiles())
                            {
                                string p = System.IO.Path.Combine(dirPath, currentFile.GetName());
                                try
                                {
                                    System.IO.File.WriteAllBytes(p, currentFile.GetBytes().ToArray());
                                }
                                catch (Exception e)
                                {
                                    this.lgInstance.Add(Localization.IO_ERROR, new string[] { "FILE Path: " + p }, e.Message);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            this.lgInstance.Add(Localization.IO_ERROR, new string[] { "DIR Path: " + dirPath }, e.Message);
                        }
                        passDirs(subDir);
                    }
                });
                passDirs(currentDir);
            }
        }

        /// <summary>
        /// Extracts the directory by the given path
        /// </summary>
        /// <param name="path">The virutal path</param>
        /// <param name="filePath">The path where do you want to extract the directory</param>
        public virtual void ExtractDirectory(string path, string filePath)
        {
            string[] segments = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            IDirectory currentNode = this.RootDirectory;

            for (int i = 0; i <= segments.Length - 1; i++)
            {
                string currentSegment = segments[i];
                if (currentNode.Contains(currentSegment))
                    currentNode = currentNode.GetSubDirectories()[currentNode.IndexOf(currentSegment)];
                else
                {
                    currentNode = null;
                    break;
                }
            }

            if (currentNode != null)
                this.ExtractDirectory(currentNode, filePath);
        }

        /// <summary>
        /// Extract files
        /// </summary>
        /// <param name="files">Array of files</param>
        /// <param name="directoryPath">Path to extract in</param>
        public virtual void ExtractFiles(IFile[] files, string directoryPath)
        {
            string[] fileArr = new string[files.Length];
            int index = 0;
            foreach (IFile currentFile in files)
                fileArr[index++] = currentFile.GetPath();
            this.ExtractFiles(fileArr, directoryPath);
        }


        #endregion
        #endregion
    }
}
