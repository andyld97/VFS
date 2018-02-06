// ------------------------------------------------------------------------
// SplitVFS.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      11.04.2016
// Last update on:  05.02.2018
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VFS.Interfaces;
using VFS.Storage;

namespace VFS
{
    /// <summary>
    /// Represents the Virtual File System which consists of File and Directory-class
    /// </summary>
    public class SplitVFS : VFS
    {
        #region VAR
        private List<byte> data = new List<byte>();
        private IDirectory rootDir = new Directory("");

        #region Public

        /// <summary>
        /// This counter describes how many PackBytes are used to identify files
        /// </summary>
        public readonly int MainCounter = 128;

        /// <summary>
        /// The byte ("-") which is needed for wrapping files and directories in one file
        /// </summary>
        public readonly int PackByte = 45;
        #endregion


        #endregion

        #region Properties
        /// <summary>
        /// Root directory of the currentFile-System
        /// </summary>
        public override IDirectory RootDirectory
        {
            get
            {
                return rootDir;
            }
        }

        /// <summary>
        /// A File which doesn't relay to somenthing, just to use some methods which aren't static anymore (Since IFile and IDirectory-Interfaces)
        /// </summary>
        public override IFile NULLFILE => new File(string.Empty, new Directory(string.Empty));
        #endregion

        #region Ctor
        /// <summary>
        /// Creates a new virtual file system.
        /// </summary>
        /// <param name="savePath">Path for storing the VFS</param>
        /// <param name="storage">The storage implementation for the special .NET Platform</param>
        /// <param name="MainCounter">The amout of seperator chars</param>
        /// <param name="PackByte">Char type from 0 to 255 for seperator</param>
        public SplitVFS(IFilePath savePath, IStorage storage, int MainCounter = 128, int PackByte = 45) : base(storage)
        {
            // bootSect contains the first two items of the array.
            // bootSect[FS_DIR] contains all directorys and bootSect[FS_FILE] all files.
            this.saveFile = savePath;
            this.MainCounter = MainCounter;
            // Avoid invalid byte
            if (PackByte < 0 || PackByte > 255)
                this.PackByte = 45;
            else
                this.PackByte = PackByte;

            // Reset dirIndex!
            VFS.DirIndex = 0;

            this.Handle = this;
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
        /// Returns true if the next count-bytes are all equal to PackByte
        /// </summary>
        /// <param name="count"></param>
        /// <param name="byte_"></param>
        /// <param name="index"></param>
        /// <param name="btArray"></param>
        /// <returns></returns>
        private bool checkNextItems(int count, int byte_, int index, byte[] btArray)
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

        /// <summary>
        /// Async-Implementation of System.IO.WriteAllBytes
        /// </summary>
        /// <param name="file">Path where the file is stored</param>
        /// <param name="data">Data to write to the file</param>
        /// <returns></returns>
        private async Task writeAllBytes(IFilePath file, byte[] data)
        {
            using (var fs = await storage.OpenFile(file, FileAccess.Write, FileShare.ShareWrite, 4096))
            {
                await fs.WriteAsync(data, 0, data.Length);
            }
        }

        /// <summary>
        /// Async-Implementation of System.IO.ReadAllBytes
        /// </summary>
        /// <param name="file">Path where the file is stored</param>
        /// <returns></returns>
        private async Task<byte[]> readAllBytes(IFilePath file)
        {
            List<byte> byteBuilder = new List<byte>();

            using (var fs = await storage.OpenFile(file, FileAccess.Read, FileShare.ShareRead, 4096))
            {
                long currentPosition = 0;
                byte[] buffer = new byte[4096];

                while (currentPosition < fs.Length)
                {
                    if (currentPosition + buffer.Length > fs.Length)
                        buffer = new byte[fs.Length - currentPosition];

                    currentPosition += await fs.ReadAsync(buffer, 0, buffer.Length);
                    for (int b = 0; b <= buffer.Length - 1; b++)
                        byteBuilder.Add(buffer[b]);
                }
            }

            return byteBuilder.ToArray();
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
        public override async Task<Result<bool>> WriteAllText(string content, string name, IDirectory dir, bool overrideExisting = false)
        {
            return await this.WriteAllBytes(this.calculateFrom(content), name, dir, overrideExisting);
        }

        /// <summary>
        /// Creates a file
        /// </summary>
        /// <param name="data">File content in bytes</param>
        /// <param name="name">File name</param>
        /// <param name="dir">Stored directory</param>
        /// <param name="overrideExisting">Determines whether a file which exists should be overriden or not</param>
        /// <returns></returns>
        public override async Task<Result<bool>> WriteAllBytes(byte[] data, string name, IDirectory dir, bool overrideExisting = false)
        {
            Progress.Register(0.0, 0.0, this.Handle, Methods.WRITE_ALL_BYTES);

            Func<Result<bool>> func = () => { 
                bool condition = (data != null && !string.IsNullOrEmpty(name) && dir != null && dir.GetFiles() != null);
                string filePath = dir.ToFullPath() + @"\" + name;

                if (condition)
                {
                    if (!NULLFILE.Contains(dir.GetFiles(), filePath))
                    {
                        IFile currentFile = new File(name, dir);
                        currentFile.SetByes(data.ToList<byte>());
                        dir.GetFiles().Add(currentFile);
                    }
                    else
                    {
                        IFile currentFile = NULLFILE.ByPath(dir.GetFiles(), filePath);
                        if (currentFile != null)
                        {
                            if (currentFile.GetBytes().Count == 0)
                            {
                                currentFile.SetByes(data.ToList<byte>());
                                Progress.Register(1.0, 1.0, this.Handle, Methods.WRITE_ALL_BYTES);
                                return new Result<bool>(true);
                            }
                            else
                            {
                                if (overrideExisting)
                                {
                                    currentFile.SetByes(data.ToList<byte>());
                                    Progress.Register(1.0, 1.0, this.Handle, Methods.WRITE_ALL_BYTES);
                                    return new Result<bool>(true);
                                }
                                else
                                {
                                    Progress.Register(1.0, 1.0, this.Handle, Methods.WRITE_ALL_BYTES);
                                    return new Result<bool>(false);
                                }
                            }
                        }
                        else
                        {
                            Progress.Register(1.0, 1.0, this.Handle, Methods.WRITE_ALL_BYTES);
                            return new Result<bool>(false);
                        }
                    }
                }

                Progress.Register(1.0, 1.0, this.Handle, Methods.WRITE_ALL_BYTES);
                return new Result<bool>(condition);
            };

            try
            {
                return await Task.Run(func);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, false, ex);
            }
        }

        /// <summary>
        /// Writes file with full path starting from root-directory.
        /// </summary>
        /// <param name="data">Byte-Array with data</param>
        /// <param name="path">Full path</param>
        /// <param name="overrideExisting">Whether an existing file should be replaced by another one</param>
        /// <returns></returns>
        public override async Task<Result<bool>> WriteAllBytes(byte[] data, string path, bool overrideExisting = false)
        {
            IDirectory currentNode = this.rootDir;

            if (!path.Contains(@"\"))
            {
                File currentFile = (File)Activator.CreateInstance(typeof(File), new object[] { path, this.rootDir });
                currentFile.Bytes = data.ToList<byte>();
                this.rootDir.GetFiles().Add(currentFile);
                return new Result<bool>(true);
            }

            string[] segments = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

            for (int x = 0; x <= segments.Length - 2; x++)
            {
                if (currentNode.Contains(segments[x]))
                    currentNode = currentNode.GetSubDirectories()[currentNode.IndexOf(segments[x])];
                else
                    return new Result<bool>(true);
            }

            if (segments.Length > 0)
                return await this.WriteAllBytes(data.ToArray(), segments[segments.Length - 1], currentNode, true);
            else
                return new Result<bool>(true);
        }

        /// <summary>
        /// Writes a file from a stream
        /// </summary>
        /// <param name="name">File name</param>
        /// <param name="dir">Direcotry which holds the foöe</param>
        /// <param name="stream">Input stream</param>
        /// <param name="overrideExisting">Whether a file should be replaced, if it exists already</param>
        /// <returns></returns>
        public override async Task<Result<bool>> WriteStream(string name, IDirectory dir, IStream stream, bool overrideExisting = false)
        {
            try
            {
                using (IMemoryStream ms = storage.OpenMemoryStream())
                {
                    stream.CopyTo(ms);
                    return await this.WriteAllBytes(ms.ToArray(), name, dir, overrideExisting);
                }
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, false, ex);
            }
        }

        /// <summary>
        /// Returns the file content in byte-Array
        /// </summary>
        /// <param name="path">Full filepath</param>
        /// <param name="startNode">Dir, where you want to start at. It's well to use the root-Dir here!</param>
        /// <param name="different">Just to differentiate between the methods - no usage in in this method</param>
        /// <returns></returns>
        public override async Task<Result<byte[]>> ReadAllBytes(string path, IDirectory startNode, bool different = false)
        {
            Progress.Register(0.0, 0.0, this.Handle, Methods.READ_ALL_BYTES);

            Func<Result<byte[]>> func = () =>
            {
                IDirectory currentNode = startNode;
                if (!path.Contains(@"\"))
                {
                    if (NULLFILE.Contains(currentNode.GetFiles(), path))
                    {
                        IFile currentFile = NULLFILE.ByPath(currentNode.GetFiles(), path);
                        if (currentNode != null)
                        {
                            Progress.Register(1.0, 1.0, this.Handle, Methods.READ_ALL_BYTES);
                            return new Result<byte[]>(currentFile.GetBytes().ToArray());
                        }
                        else
                        {
                            Progress.Register(1.0, 1.0, this.Handle, Methods.READ_ALL_BYTES);
                            return new Result<byte[]>(null, false, null);
                        }
                    }
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
                        Progress.Register(1.0, 1.0, this.Handle, Methods.READ_ALL_BYTES);
                        return new Result<byte[]>(null, false, null);
                    }                        
                }

                if (currentNode != null)
                {
                    if (NULLFILE.Contains(currentNode.GetFiles(), path))
                    {
                        IFile currentFile = NULLFILE.ByPath(currentNode.GetFiles(), path);
                        Progress.Register(1.0, 1.0, this.Handle, Methods.READ_ALL_BYTES);
                        return new Result<byte[]>(currentFile.GetBytes().ToArray());
                    }
                }
                Progress.Register(1.0, 1.0, this.Handle, Methods.READ_ALL_BYTES);
                return null;
            };

            try
            {
                return await Task.Run(func);
            }
            catch (Exception ex)
            {
                return new Result<byte[]>(null, false, ex);
            }
        }

        /// <summary>
        /// Returns the file content
        /// </summary>
        /// <param name="path">Filename</param>
        /// <param name="startNode">Directory where the path is beginning at</param>
        /// <returns></returns>
        public override async Task<Result<string>> ReadAllText(string path, IDirectory startNode)
        {
            Result<byte[]> data = await this.ReadAllBytes(path, startNode, true);
            if (data.Success && data.HasValue)
                return new Result<string>(this.calculateFrom(data.Value), true, null);
            else
                return new Result<string>(string.Empty, false, null);
        }

        /// <summary>
        /// Writes filesystem to physical file
        /// </summary>
        /// <returns></returns>
        public override async Task<Result<bool>> Save()
        {
            Progress.Register(0.0, 0.0, this.Handle, Methods.SAVE);

            Func<Task<Result<bool>>> func = async () =>
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

                int step = 0;
                int count = elements.Length + 1; //+1 because after this there is one operation left.

                for (int i = 0; i <= elements.Length - 1; i++)
                {
                    Progress.Register(step / count, 0.0, this.Handle, Methods.SAVE);

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

                    Progress.Register(step++ / count, 1.0, this.Handle, Methods.SAVE);
                }
                final.AddRange(this.data);

                this.data.Clear();
                await this.writeAllBytes(this.saveFile, final.ToArray());
                Progress.Register(1.0, 1.0, this.Handle, Methods.SAVE);
                return new Result<bool>(true);
            };
            try
            {
                return await Task.Run(func);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, false, ex);
            }
        }

        /// <summary>
        /// Loads the content of the file into this instance
        /// </summary>
        /// <param name="file">The path where the file is stored</param>
        public override async Task<Result<bool>> Read(IFilePath file)
        {
            Progress.Register(0.0, 0.0, this.Handle, Methods.READ);

            Func<Task<Result<bool>>> func = async () =>
            {
                // Read archiv format.
                // Doing this in a thread
                if (await storage.FileExists(file))
                {
                    List<List<byte>> getBytes = new List<List<byte>>();
                    getBytes.Add(new List<byte>());

                    int s = 0;
                    byte[] data = await this.readAllBytes(file); 

                    for (int i = 0; i <= data.Length - 1; i++)
                    {
                        if (this.checkNextItems(MainCounter, PackByte, i, data))
                        {
                            getBytes.Add(new List<byte>());
                            i += MainCounter - 1;
                            s++;
                        }
                        else
                            getBytes[s].Add(Convert.ToByte(data[i]));
                    }

                    string[] files = this.calculateFrom(getBytes[0].ToArray()).Split(new string[] { "<" }, StringSplitOptions.RemoveEmptyEntries);
                    if (getBytes.Count == 1 && files.Length != 1)
                        return new Result<bool>(false);


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
                    {
                        List<Result<bool>> results = new List<Result<bool>>();
                        for (int i = 0; i <= paths.Length - 1; i++)
                            results.Add(await this.WriteAllBytes(getBytes[i + 1].ToArray(), paths[i], true));

                        Result<bool> final = null;
                        foreach (Result<bool> result in results)
                        {
                            if (!result.Success)
                            {
                                final = result;
                                break;
                            }
                        }

                        if (final != null)
                        {
                            Progress.Register(1.0, 1.0, this.Handle, Methods.READ);
                            return new Result<bool>(final.Value, final != null, final.FailInfo);
                        }
                        else
                        {
                            Progress.Register(1.0, 1.0, this.Handle, Methods.READ);
                            return new Result<bool>(true);
                        }
                    }
                    else
                    {
                        Progress.Register(1.0, 1.0, this.Handle, Methods.READ);
                        return new Result<bool>(false);
                    }
                }
                else
                {
                    Progress.Register(1.0, 1.0, this.Handle, Methods.READ);
                    return new Result<bool>(false);
                }
            };

            try
            {
                return await Task.Run(func);
            }
            catch (Exception ex)
            {
                Progress.Register(1.0, 1.0, this.Handle, Methods.READ);
                return new Result<bool>(false, false, ex);
            }
        }

        /// <summary>
        /// Extracts the RAM content to a physical file like Archiv does.
        /// </summary>
        /// <param name="path">The path of the physical file</param>
        /// <returns></returns>
        public override async Task<Result<bool>> Extract(IDirectoryPath path)
        {
            Progress.Register(0.0, 0.0, this.Handle, Methods.EXTRACT);

            Func<Task<Result<bool>>> func = async () =>
            {
                if ( await storage.DirectoryExists(path))
                {
                    // Extract now.
                    // Create directories
                    Func<IDirectory, Task> passDirs = null;
                    bool failed = false;
                    Exception exs = null;

                    passDirs = new Func<IDirectory,Task> (async (IDirectory dir) =>
                    {
                        foreach (IDirectory currentDir in dir.GetSubDirectories())
                        {
                            IDirectoryPath dirPath = null;
                            try
                            {
                                dirPath = await storage.CreateDirectory(path, this.FormatPath(currentDir.ToFullPath()));
                            }
                            catch (Exception ex)
                            {
                                failed = true;
                                exs = ex;
                                return;
                            }
                            await passDirs(currentDir);
                        }
                    });
                    await passDirs(this.rootDir);

                    if (failed)
                        return new Result<bool>(false, false, exs);

                    int step = 0;
                    int count = this.rootDir.GetFiles().Count + 1; // +1 Because one operation left after this operation finished

                    // Create files
                    foreach (File currentFile in this.rootDir.GetFiles())
                    {
                        Progress.Register(step / count, 0.0, this.Handle, Methods.EXTRACT);
                        try
                        {
                            IFilePath currentFileToWrite = await storage.CombiniePath(path, this.FormatPath(currentFile.Path));
                            byte[] dataToWrite = currentFile.Bytes.ToArray();

                            using (IStream writeStream = await storage.OpenFile(currentFileToWrite, FileAccess.Write, FileShare.ShareWrite, 4096))
                            {
                                await writeStream.WriteAsync(dataToWrite, 0, dataToWrite.Length);
                            }
                        }
                        catch (Exception ex)
                        {
                            return new Result<bool>(false, false, ex);
                        }

                        Progress.Register(step++ / count, 1.0, this.Handle, Methods.EXTRACT);
                    }

                    Action<IDirectory> passFiles = null;
                    failed = false;
                    exs = null;

                    passFiles = new Action<IDirectory>(async (IDirectory dir) =>
                    {
                        foreach (IDirectory currentDir in dir.GetSubDirectories())
                        {
                            foreach (IFile currentFile in currentDir.GetFiles())
                            {
                                IFilePath fileToWrite = await storage.CombiniePath(path, this.FormatPath(currentFile.GetPath()));
                                try
                                {
                                    var data = currentFile.GetBytes().ToArray();
                                    using (var writeStream = await storage.OpenFile(fileToWrite, FileAccess.Write, FileShare.ShareWrite, 4096))
                                    {
                                        await writeStream.WriteAsync(data, 0, data.Length);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    failed = true;
                                    exs = ex;
                                    return;
                                }
                            }
                            passFiles(currentDir);
                        }
                    });

                    Progress.Register(1.0, 1.0, this.Handle, Methods.EXTRACT);

                    if (failed)
                        return new Result<bool>(false, false, exs);

                    passFiles(this.rootDir);
                    return new Result<bool>(true);
                }
                else
                {
                    Progress.Register(1.0, 1.0, this.Handle, Methods.EXTRACT);
                    return new Result<bool>(false, false, new Exception("Dir doesn't exists!"));
                }
            };

            try
            {
                return await Task.Run(func);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, false, ex);
            }
        }

        /// <summary>
        /// Extract files - but not all like .Extract(string path) does
        /// </summary>
        /// <param name="path">All file pathes</param>
        /// <param name="targetDirectory">The physical directory where you want to write in</param>
        public override async Task<Result<bool>> ExtractFiles(string[] path, IDirectoryPath targetDirectory)
        {
            Progress.Register(0.0, 0.0, this.Handle, Methods.EXTRACT_FILES);

            Task<Result<bool>> func = Task.Run(async () =>
            {
                string[] dirs = new string[path.Length];
                for (int i = 0; i <= path.Length - 1; i++)
                {
                    string[] segments = path[i].Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int x = 0; x <= segments.Length - 2; x++)
                        dirs[i] += path[x];
                }

                int step = 0;
                int count = dirs.Length;

                for (int y = 0; y <= dirs.Length - 1; y++)
                {
                    Progress.Register(step / count, 0.0, this.Handle, Methods.EXTRACT_FILES);
                    if (!this.FormatPath(path[y]).Contains(@"\"))
                    {
                        // Those it from root dir.
                        IFile _currentFile = NULLFILE.ByPath(this.rootDir.GetFiles(), path[y]);
                        IFilePath cF = await storage.CombiniePath(targetDirectory, this.FormatPath(path[y]));

                        if (_currentFile != null)
                        {
                            try
                            {
                                using (IStream writeStream = await storage.OpenFile(cF, FileAccess.Write, FileShare.ShareWrite, 4096))
                                {
                                    var data = _currentFile.GetBytes().ToArray();
                                    await writeStream.WriteAsync(data, 0, data.Length);
                                }
                            }
                            catch (Exception ex)
                            {
                                // ToDo Handle exception
                                return new Result<bool>(false, false, ex);
                            }
                        }
                        Progress.Register(step++ / count, 1.0, this.Handle, Methods.EXTRACT_FILES);
                        continue;
                    }
                    string currentPath = dirs[y];
                    string currentPathWithFile = path[y];
                    IDirectory lastNodeFromPath = this.RootDirectory.CalculateLastNode(currentPath);

                    await storage.CreateDirectory(targetDirectory, this.FormatPath(lastNodeFromPath.ToFullPath()));

                    IFile currentFile = NULLFILE.ByPath(lastNodeFromPath.GetFiles(), currentPathWithFile);
                    IFilePath toWriteFile = await storage.CombiniePath(targetDirectory, this.FormatPath(currentPathWithFile));

                    if (currentFile != null)
                    {
                        try
                        {
                            var data = currentFile.GetBytes().ToArray();
                            using (IStream writeStream = await storage.OpenFile(toWriteFile, FileAccess.Write, FileShare.ShareWrite, 4096))
                            {
                                await writeStream.WriteAsync(data, 0, data.Length);
                            }
                        }
                        catch (Exception ex)
                        {
                            return new Result<bool>(false, false, ex);
                        }
                    }
                    Progress.Register(step++ / count, 1.0, this.Handle, Methods.EXTRACT_FILES);
                }
                return new Result<bool>(true);
            });

            try
            {
                return await func;
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, false, ex);
            }
        }


        /// <summary>
        /// Extracts a directory full with content to toPath
        /// </summary>
        /// <param name="currentDir">The direcotry</param>
        /// <param name="toPath">A vaild file path</param>
        public override async Task<Result<bool>> ExtractDirectory(IDirectory currentDir, IDirectoryPath targetDir)
        {
            Progress.Register(0.0, 0.0, this.Handle, Methods.EXTRACT_DIR);

            Task<Result<bool>> func = Task.Run(async () =>
            {
                if (await storage.DirectoryExists(targetDir))
                {
                    Action<IDirectory> passDirs = null;

                    IDirectoryPath dir = null;
                    try
                    {
                        dir = await storage.CreateDirectory(dir, currentDir.GetName());
                    }
                    catch (Exception ex)
                    {
                        return new Result<bool>(false, false, ex);
                    }


                    int step = 0;
                    int count = currentDir.GetFiles().Count + 1;

                    // Create files from main dir
                    foreach (IFile currentFile in currentDir.GetFiles())
                    {
                        Progress.Register(step / count, 0.0, this.Handle, Methods.EXTRACT_DIR);

                        IFilePath file = await storage.CombiniePath(dir, currentFile.GetName());
                        try
                        {
                            using (var writeStream = await storage.OpenFile(file, FileAccess.Write, FileShare.ShareWrite, 4096))
                            {
                                var data = currentFile.GetBytes().ToArray();
                                await writeStream.WriteAsync(data, 0, data.Length);
                            }
                        }
                        catch (Exception ex)
                        {
                            return new Result<bool>(false, false, ex);
                        }

                        Progress.Register(step++ / count, 1.0, this.Handle, Methods.EXTRACT_DIR);
                    }

                    bool failed = false;
                    Exception exTmp = null;

                    passDirs = new Action<IDirectory>(async (IDirectory currentDirectory) =>
                    {
                        foreach (IDirectory subDir in currentDirectory.GetSubDirectories())
                        {
                            try
                            {
                                IDirectoryPath subSubDir = await storage.CreateDirectory(targetDir, currentDir.GetName() + @"\" + currentDir);

                                foreach (IFile currentFile in subDir.GetFiles())
                                {
                                    IFilePath localFile = await storage.CombiniePath(subSubDir, currentFile.GetName());

                                    try
                                    {
                                        var data = currentFile.GetBytes().ToArray();
                                        using (IStream writeStream = await storage.OpenFile(localFile, FileAccess.Write, FileShare.ShareWrite, 4096))
                                        {
                                            await writeStream.WriteAsync(data, 0, data.Length);
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        failed = true;
                                        exTmp = e;
                                        return;
                                    }
                                }
                            }
                            catch (Exception ess)
                            {
                                failed = true;
                                exTmp = ess;
                                return;
                            }
                            passDirs(subDir);
                        }
                    });
                    passDirs(currentDir);

                    Progress.Register(1.0, 1.0, this.Handle, Methods.EXTRACT_DIR);
                    return new Result<bool>(failed, !failed, exTmp);
                }
                else
                {
                    Progress.Register(1.0, 1.0, this.Handle, Methods.EXTRACT_DIR);
                    return new Result<bool>(false, false, new Exception("File doesn't exists!"));
                }
            });

            try
            {
                return await func;
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, false, ex);
            }
        }

        /// <summary>
        /// Extracts the directory by the given path
        /// </summary>
        /// <param name="path">The virutal path</param>
        /// <param name="localDirectory">The path where do you want to extract the directory</param>
        public override async Task<Result<bool>> ExtractDirectory(string path, IDirectoryPath localDirectory)
        {
            Task<Result<bool>> func = Task.Run(async () =>
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
                    return await this.ExtractDirectory(currentNode, localDirectory);
                else
                    return new Result<bool>(false);
            });

            try
            {
                return await func;
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, false, ex);
            }
        }

        /// <summary>
        /// Extract files
        /// </summary>
        /// <param name="files">Array of files</param>
        /// <param name="directoryPath">Path to extract in</param>
        public override async Task<Result<bool>> ExtractFiles(IFile[] files, IDirectoryPath directoryPath)
        {
            string[] fileArr = new string[files.Length];
            int index = 0;
            foreach (IFile currentFile in files)
                fileArr[index++] = currentFile.GetPath();

            return await this.ExtractFiles(fileArr, directoryPath);
        }

        /// <summary>
        /// Creates a new VFS
        /// </summary>
        /// <param name="directory">All files and folders in this directory are used</param>
        /// <returns></returns>
        public override async Task<Result<bool>> Create(IDirectoryPath directory)
        {
            return await this.Create(new IFilePath[] { }, new IDirectoryPath[] { directory });
        }

        /// <summary>
        /// Creates a new VFS
        /// </summary>
        /// <param name="files">Files which will be processed</param>
        /// <param name="directories">Directories which will be processed</param>
        public override async Task<Result<bool>> Create(IFilePath[] files, IDirectoryPath[] directories)
        {
            Progress.Register(0.0, 0.0, this.Handle, Methods.CREATE);

            Func<Task<Result<bool>>> func = async () =>
            {
                VFS currentSystem = new SplitVFS(this.saveFile, storage, MainCounter, PackByte);

                // Add files at first
                foreach (IFilePath file in files)
                {
                    string[] segements = file.ToFullPath().Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    if (segements.Length > 0)
                    {
                        try
                        {
                            File currentFile = new File(segements[segements.Length - 1], currentSystem.RootDirectory);

                            // Read file 
                            currentFile.Bytes.Clear();
                            currentFile.Bytes.AddRange(await readAllBytes(file));
                            currentSystem.RootDirectory.GetFiles().Add(currentFile);
                        }
                        catch (Exception ex)
                        {
                            // Log exception
                            return new Result<bool>(false, false, ex);
                        }
                    }
                }

                int step = 0;
                int count = directories.Length;

                foreach (IDirectoryPath dir in directories)
                {
                    Progress.Register(step / count, 0.0, this.Handle, Methods.CREATE);

                    Directory vDir = new Directory(dir.Name());

                    bool failed = false;
                    Exception exTmp = null;

                    Action<IDirectoryPath> recurseDirs = null;
                    recurseDirs = new Action<IDirectoryPath>(async (IDirectoryPath lastDir) =>
                    {
                        string[] segements = lastDir.ToFullPath().Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

                        string nPath = string.Empty;
                        bool doAdding = false;

                        for (int i = 0; i <= segements.Length - 1; i++)
                        {
                            Progress.Register(step / count, (i / (segements.Length - 1)), this.Handle, Methods.CREATE);
                            if (segements[i] == vDir.Name)
                            {
                                doAdding = true;
                                continue;
                            }

                            if (doAdding)
                                nPath += segements[i] + @"\";
                        }

                        if (!string.IsNullOrEmpty(nPath))
                            vDir.AddPathes(new string[] { nPath });

                        Interfaces.IDirectory lastDirectory = (nPath == string.Empty ? vDir : vDir.CalculateLastNode(nPath));

                        // ToDo: Consider this loop \/ in progress calculation!
                        foreach (var fi in lastDir.GetFiles().Result)
                        {
                            File currentFile = new File(fi.GetName(), lastDirectory);
                            try
                            {
                                currentFile.Bytes.Clear();
                                currentFile.Bytes.AddRange(await readAllBytes(fi));
                            }
                            catch (Exception ex)
                            {
                                // Log exception
                                failed = true;
                                exTmp = ex;
                                return;
                            }
                            lastDirectory.GetFiles().Add(currentFile);
                        }

                        foreach (var d in lastDir.GetDirectories().Result)
                            recurseDirs(d);
                    });

                    recurseDirs(dir);
                    currentSystem.RootDirectory.GetSubDirectories().Add(vDir);

                    if (failed)
                    {
                        Progress.Register(1.0, 1.0, this.Handle, Methods.CREATE);
                        return new Result<bool>(failed, !failed, exTmp);
                    }

                    Progress.Register(step++ / count, 1.0, this.Handle, Methods.CREATE);
                }

                // Make sure 1.0/1.0
                Progress.Register(1.0, 1.0, this.Handle, Methods.CREATE);
                return new Result<bool>(true);
            };

            try
            {
                return await Task.Run(func);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, false, ex);
            }
        }

        #endregion
        #endregion
    }
}
