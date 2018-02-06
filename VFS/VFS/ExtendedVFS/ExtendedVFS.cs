// ------------------------------------------------------------------------
// ExtendedVFS.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      17.12.2016
// Last update on:  05.02.2018
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VFS.Interfaces;
using VFS.ExtendedVFS;
using System.Threading.Tasks;
using VFS.Storage;

namespace VFS.ExtendedVFS
{
    /// <summary>
    /// Represents a VFS which is more efficent and can reduce the amount of bytes to save
    /// </summary>
    public class ExtendedVFS : VFS
    {
        /// <summary>
        /// The root directory
        /// </summary>
        private ExtendedDirectory rootDir = null;

        private bool saveAfterChange = true;

        /// <summary>
        /// The path as a cliboard which is needed to work - It's like the temp-directory
        /// </summary>
        public readonly IDirectoryPath WorkSpacePath = null;
        private int offset = 0;

        #region Static, Readonly and const fields

        /// <summary>
        /// A File which doesn't relay to somenthing, just to use some methods which aren't static anymore (Since IFile and IDirectory are Interfaces)
        /// </summary>
        public override IFile NULLFILE => new ExtendedFile(new HeaderInfo(), new ExtendedDirectory("", storage), storage);

        /// <summary>
        /// The sequence to identfy file format [{***VHP***}]
        /// </summary>
        public static readonly byte[] StartSequence = new byte[] { 123, 42, 42, 42, 86, 72, 80, 42, 42, 42, 125 };

        /// <summary>
        /// The restriction if you can read the file or not. It's set to 1 GB
        /// </summary>
        public const long READ_RESTRICTION = 1024 * 1024 * 1024;

        /// <summary>
        /// Determines how much bytes does the buffer-array contains.
        /// </summary>
        public readonly long BUFFER_SIZE;

        #endregion

        #region Properties
        /// <summary>
        /// Returns the length of the header to adjust the start and end position of a file
        /// </summary>
        public int Offset
        {
            get
            {
                return this.offset + ExtendedVFS.StartSequence.Length;
            }
        }

        /// <summary>
        /// Root Directory - Name: ""
        /// </summary>
        public override IDirectory RootDirectory
        {
            get
            {
                return this.rootDir;
            }
        }

        /// <summary>
        /// If true, creates the file completly new after a change like calling WriteAllText or WriteAllBytes
        /// </summary>
        public bool SaveAfterChange
        {
            get
            {
                return this.saveAfterChange;
            }
            set
            {
                this.saveAfterChange = value;
            }
        }
        #endregion

        /// <summary>
        /// Instantiate a new ExtendedVFS
        /// </summary>
        /// <param name="saveFile">The path where the VFS file is stored</param>
        /// <param name="workSpacePath">The path of the temp directory</param>
        /// <param name="BufferSize">The size of the buffer</param>
        public ExtendedVFS(IFilePath saveFile, IStorage storageImplementation, long BufferSize = 32768) : base(storageImplementation)
        {
            if (storageImplementation == null)
                throw new ArgumentNullException("IStorage");

            this.saveFile = saveFile;
            this.WorkSpacePath = storageImplementation.GetWorkSpacePath();
            this.BUFFER_SIZE = BufferSize;
            this.rootDir = new ExtendedDirectory("", storageImplementation);
            this.Handle = this;
        }

        /// <summary>
        /// Creates a new VHP (Version 2)
        /// </summary>
        /// <param name="directory">All files and folders in this directory are used</param>
        /// <returns></returns>
        public override async Task<Result<bool>> Create(IDirectoryPath directory)
        {
            return await this.Create(new IFilePath[] { }, new IDirectoryPath[] { directory });
        }

        /// <summary>
        /// Creates a new VHP (Version 2)
        /// </summary>
        /// <param name="directories">The directories which you want to include</param>
        /// <param name="files">The files which you want to include</param>
        /// <returns></returns>
        public override async Task<Result<bool>> Create(IFilePath[] files, IDirectoryPath[] directories)
        {
            Func<Task<Result<bool>>> func = async () =>
            {
                Progress.Register(0.0, 0.0, this.Handle, Methods.CREATE);
                int steps = 3;
                int currentStep = 0;

                int value = files.Length + directories.Length;
                double currentValue = 0;

                ExtendedFile oldFile = null;

                int counter = 0;
                foreach (IFilePath currentFile in files)
                {
                    currentValue = counter++ / value;
                    Progress.Register(currentValue, this.Handle, Methods.CREATE);


                    long size = 0;
                    long startSize = (oldFile == null ? 0L : oldFile.EndPosition);
                    try
                    {
                        size = currentFile.Length().Result;
                    }
                    catch (Exception) { }
                    HeaderInfo hi = new HeaderInfo(currentFile.GetName(), startSize, startSize + size);
                    ExtendedFile cFile = new ExtendedFile(hi, this.RootDirectory as ExtendedDirectory, storage);
                    await cFile.Initalize();
                    this.RootDirectory.GetFiles().Add(cFile);
                    oldFile = cFile;
                }

                foreach (IDirectoryPath currentDirectory in directories)
                {
                    currentValue = counter++ / value;
                    Progress.Register(currentValue, this.Handle, Methods.CREATE);

                    ExtendedDirectory vDir = new ExtendedDirectory(currentDirectory.Name(), storage);

                    Action<IDirectoryPath> recurseDirs = null;
                    recurseDirs = new Action<IDirectoryPath>(async (IDirectoryPath lastDir) =>
                    {
                        string[] segements = lastDir.ToFullPath().Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

                        string nPath = string.Empty;
                        bool doAdding = false;

                        for (int i = 0; i <= segements.Length - 1; i++)
                        {
                            if (segements[i] == vDir.GetName())
                            {
                                doAdding = true;
                                continue;
                            }

                            if (doAdding)
                                nPath += segements[i] + @"\";
                        }

                        if (!string.IsNullOrEmpty(nPath))
                            vDir.AddPathes(new string[] { nPath });

                        ExtendedDirectory lastDirectory = (nPath == string.Empty ? vDir : (ExtendedDirectory)vDir.CalculateLastNode(vDir, nPath));

                        foreach (var fi in await lastDir.GetFiles())
                        {
                            long size = 0;
                            long startSize = (oldFile == null ? 0L : oldFile.EndPosition);
                            try
                            {
                                size = fi.Length().Result;
                            }
                            catch (Exception) { }
                            HeaderInfo hi = new HeaderInfo(fi.ToFullPath(), startSize, startSize + size);
                            ExtendedFile cFile = new ExtendedFile(hi, lastDirectory, storage);
                            await cFile.Initalize();
                            lastDirectory.GetFiles().Add(cFile);
                            oldFile = cFile;
                        }

                        foreach (var d in currentDirectory.GetDirectories().Result)
                            recurseDirs(d);
                    });

                    recurseDirs(currentDirectory);
                    this.RootDirectory.GetSubDirectories().Add(vDir);
                }
                steps = 3 + this.RootDirectory.ToFileStringArray().Length;
                Progress.Register(currentStep++ / (double)steps, this.Handle, Methods.CREATE, true);

                // Creating file
                // Signature System.IO.FileStream writeStream = new System.IO.FileStream(this.savePath, System.IO.FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write, (int)BUFFER_SIZE, true)
                using (var writeStream = await storage.OpenFile(this.saveFile, Storage.FileAccess.Write, Storage.FileShare.ShareWrite, (int)BUFFER_SIZE))
                {
                    currentValue = 0.0;
                    counter = 0;
                    value = this.RootDirectory.ToFileStringArray().Length;
                    Progress.Register(currentValue, this.Handle, Methods.CREATE);

                    // Creating header
                    string header = string.Empty;

                    foreach (string currentFile in this.RootDirectory.ToFileStringArray())
                    {
                        string nFile = currentFile;

                        // Get orginial file back
                        ExtendedFile orgFile = ExtendedDirectory.ByPath(nFile, this.RootDirectory as ExtendedDirectory);
                        if (orgFile != null)
                        {
                            //File.file:0:2018|\FILEY.txt:2018:2020
                            header += orgFile.ToString();
                        }

                        currentValue = ++counter / (double)value;
                        Progress.Register(currentValue, this.Handle, Methods.CREATE);
                    }

                    Progress.Register(currentStep++ / (double)steps, this.Handle, Methods.CREATE, false);
                    counter = 0;
                    currentValue = 0;

                    // Generating bootSector to identify file
                    string bootSector = header + "|<" + String.Join(",", this.RootDirectory.ToStringArray()) + ",>";

                    // Final header with ^ at the end
                    string finalHeader = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(bootSector)) + "^";

                    // Creating a buffer to write to filestream
                    byte[] buffer = new byte[BUFFER_SIZE];

                    // Writing V2 at first {***VHP***}
                    buffer = ExtendedVFS.StartSequence;
                    await writeStream.WriteAsync(buffer, 0, buffer.Length);

                    // Writing header
                    buffer = System.Text.Encoding.UTF8.GetBytes(finalHeader);
                    await writeStream.WriteAsync(buffer, 0, buffer.Length);

                    // Reset buffer to value
                    buffer = new byte[BUFFER_SIZE];

                    // Read file content buffer wise and write it to writeStream
                    foreach (string file in this.RootDirectory.ToFileStringArray())
                    {
                        // Get orginial file back
                        ExtendedFile orgFile = ExtendedDirectory.ByPath(file, this.RootDirectory as ExtendedDirectory);
                        if (orgFile != null)
                        {
                            try
                            {
                                using (var readStream = await storage.OpenFile(orgFile.OriginalFile, Storage.FileAccess.Read, Storage.FileShare.ShareRead, (int)BUFFER_SIZE))
                                {
                                    long currentPosition = 0L;
                                    while (currentPosition < readStream.Length)
                                    {
                                        // Set appropriate buffer length
                                        if (Math.Abs(currentPosition - readStream.Length) < BUFFER_SIZE)
                                            buffer = new byte[Math.Abs(currentPosition - readStream.Length)];

                                        currentPosition += await readStream.ReadAsync(buffer, 0, buffer.Length);
                                        await writeStream.WriteAsync(buffer, 0, buffer.Length);

                                        Progress.Register(currentPosition / (double)readStream.Length, this.Handle, Methods.CREATE);
                                    }
                                }
                            }
                            catch (Exception es)
                            {
                                return new Result<bool>(false, false, es);
                            }
                            Progress.Register(++currentStep / (double)steps, this.Handle, Methods.CREATE, false);
                        }
                    }
                }
                Progress.Register(1.0, 1.0, this.Handle, Methods.CREATE);
                return new Result<bool>(false, true, null);
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
        /// Loads a VHP-File into the RAM (just header-content)
        /// </summary>
        /// <param name="filePath">The path where the vhp-file is stored</param>
        public override async Task<Result<bool>> Read(IFilePath file)
        {
            Func<Task<Result<bool>>> func = async () =>
            {
                // Check if this file is right, otherwise it cause problems which took very much time
                bool? validation = await ExtendedVFS.IsNewVersion(file, storage);
                if (!validation.HasValue || validation.HasValue && !validation.Value)
                    return new Result<bool>(false, false, null);

                Progress.Register(0.0, 0.0, this.Handle, Methods.READ);

                string header = string.Empty;
                long currentPosition = 0;
                bool foundSegment = false;

                if (await storage.FileExists(file))
                {
                    using (var fs = await storage.OpenFile(file, Storage.FileAccess.Read, Storage.FileShare.ShareRead, 1))
                    {
                        // Just reading the header in this fs
                        byte[] buffer = new byte[1];

                        // Set position to ModifiedVFS.StartSequence
                        fs.Position = ExtendedVFS.StartSequence.Length;
                        currentPosition = fs.Position;

                        while (true)
                        {
                            currentPosition += await fs.ReadAsync(buffer, 0, buffer.Length);

                            if (!foundSegment && System.Text.Encoding.UTF8.GetString(buffer) == "^")
                            {
                                foundSegment = true;
                                buffer = new byte[BUFFER_SIZE];
                                this.offset = header.Length + 1; // For ^
                                header = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(header));
                                break;
                            }
                            else if (!foundSegment)
                                header += System.Text.Encoding.UTF8.GetString(buffer);
                        }

                        // Splitting header <
                        string[] headerSplt = header.Split(new string[] { "<" }, StringSplitOptions.RemoveEmptyEntries);

                        if (headerSplt.Length - 1 == 1)
                        {
                            this.rootDir.AddPathes(headerSplt[1].Replace(">", string.Empty).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                            await this.rootDir.AddFiles(headerSplt[0].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries));
                        }
                    }
                }

                Progress.Register(1.0, 1.0, this.Handle, Methods.READ);
                return new Result<bool>(false, true, null);
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
        /// Returns the content of a virtual file as a string (Max: 1 GB)
        /// </summary>
        /// <param name="path">Path of the virtual file</param>
        /// <param name="startNode">The directory where the path is beginning</param>
        /// <returns></returns>
        public override async Task<Result<string>> ReadAllText(string path, IDirectory startNode)
        {
            Result<byte[]> res = await this.ReadAllBytes(path, startNode, false);

            if (res.HasValue && res.Success)
                return new Result<string>(System.Text.Encoding.UTF8.GetString(res.Value), true, null);

            return new Result<string>(string.Empty, false, null);
        }

        /// <summary>
        /// Returns the bytes of a virutal files (reading from originial file) (Max: 1 GB)
        /// </summary>
        /// <param name="path">Path of the virtual file</param>
        /// <param name="startNode">The directory where the path is beginning</param>
        /// <param name="different">Just to differniate between these methods (not used in this method)</param>
        /// <returns></returns>
        public override async Task<Result<byte[]>> ReadAllBytes(string path, IDirectory startNode, bool different = false)
        {
            Func<Task<Result<byte[]>>> func = async () =>
            {
                Progress.Register(0.0, 0.0, this.Handle, Methods.READ_ALL_BYTES);
                IDirectory currentNode = startNode;
                if (!path.Contains(@"\"))
                {
                    if (NULLFILE.Contains(currentNode.GetFiles(), path))
                    {
                        IFile currentFile = NULLFILE.ByPath(currentNode.GetFiles(), path);
                        if (currentNode != null)
                        {
                            ExtendedFile mf = (ExtendedFile)currentFile;

                            if (!mf.IsInvalid)
                            {
                                // Try to read the file
                                // ToDo: Implement case: When file is "invalid"
                                using (var currentFS = await storage.OpenFile(this.saveFile, Storage.FileAccess.Read, Storage.FileShare.ShareRead, (int)BUFFER_SIZE))
                                {
                                    if (mf.Size > READ_RESTRICTION)
                                        throw new Exception("FILE_TOO_LARGE");

                                    long start = (currentFile as ExtendedFile).StartPosition + this.Offset;
                                    long currentPosition = (currentFile as ExtendedFile).StartPosition + this.Offset;
                                    long endPosition = (currentFile as ExtendedFile).EndPosition + this.Offset;

                                    byte[] buffer = new byte[BUFFER_SIZE];
                                    byte[] finalBytes = new byte[Math.Abs(currentPosition - endPosition)];

                                    currentFS.Position = currentPosition;
                                    List<byte> lstTst = new List<byte>();
                                    while (currentPosition < endPosition)
                                    {
                                        if (Math.Abs(currentPosition - endPosition) < BUFFER_SIZE)
                                            buffer = new byte[Math.Abs(currentPosition - endPosition)];

                                        currentPosition += await currentFS.ReadAsync(buffer, 0, buffer.Length);
                                        lstTst.AddRange(buffer);

                                        double value = (currentPosition - start) / (endPosition - start);
                                        Progress.Register(value, value, this.Handle, Methods.READ_ALL_BYTES);
                                    }
                                    Progress.Register(1.0, 1.0, this.Handle, Methods.READ_ALL_BYTES);
                                    return new Result<byte[]>(lstTst.ToArray());
                                }
                            }
                            else
                            {
                                // This case can't occur if you just call Read("..."), but if you call WriteAllText/Bytes
                                // File is currently in the workspace directory and needs to be read out.

                                // Get file
                                if (mf.OriginalFile.Length().Result <= READ_RESTRICTION)
                                {
                                    List<byte> fileBytes = new List<byte>();

                                    using (var fsRead = await storage.OpenFile(mf.OriginalFile, Storage.FileAccess.Read, Storage.FileShare.ShareRead, (int)BUFFER_SIZE))
                                    {
                                        byte[] buffer = new byte[BUFFER_SIZE];

                                        long currentPosition = 0L;
                                        while (currentPosition < mf.OriginalFile.Length().Result)
                                        {
                                            if (Math.Abs(currentPosition - fsRead.Length) < BUFFER_SIZE)
                                                buffer = new byte[Math.Abs(currentPosition - fsRead.Length)];

                                            currentPosition += await fsRead.ReadAsync(buffer, 0, buffer.Length);
                                            fileBytes.AddRange(buffer);
                                            double value = currentPosition / mf.OriginalFile.Length().Result;
                                            Progress.Register(value, value, this.Handle, Methods.READ_ALL_BYTES);
                                        }
                                    }
                                    Progress.Register(1.0, 1.0, this.Handle, Methods.READ_ALL_BYTES);
                                    return new Result<byte[]>(fileBytes.ToArray());
                                }
                                else
                                    throw new Exception("FILE_TOO_LARGE");
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }

                    return null;
                }
                else
                {
                    string[] segments = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i <= segments.Length - 2; i++)
                    {
                        string currentName = segments[i];
                        int nIndex = currentNode.IndexOf(currentName);

                        if (nIndex != -1)
                            currentNode = currentNode.GetSubDirectories()[nIndex];
                        else
                            return null;
                    }

                    if (currentNode != null)
                    {
                        if (NULLFILE.Contains(currentNode.GetFiles(), path))
                        {
                            // Get the file
                            IFile currentFile = NULLFILE.ByPath(currentNode.GetFiles(), path);
                            ExtendedFile mf = (ExtendedFile)currentFile;

                            if (!mf.IsInvalid)
                            {
                                // Try to read the file
                                using (var currentFS = await storage.OpenFile(this.saveFile, Storage.FileAccess.Read, Storage.FileShare.ShareRead, (int)BUFFER_SIZE))
                                {
                                    if (mf.Size > READ_RESTRICTION)
                                        throw new Exception("FILE_TOO_LARGE");

                                    long start = (currentFile as ExtendedFile).StartPosition + this.Offset;
                                    long currentPosition = (currentFile as ExtendedFile).StartPosition + this.Offset;
                                    long endPosition = (currentFile as ExtendedFile).EndPosition + this.Offset;

                                    byte[] buffer = new byte[BUFFER_SIZE];
                                    byte[] finalBytes = new byte[Math.Abs(currentPosition - endPosition)];

                                    currentFS.Position = currentPosition;
                                    List<byte> lstTst = new List<byte>();
                                    while (currentPosition < endPosition)
                                    {
                                        if (Math.Abs(currentPosition - endPosition) < BUFFER_SIZE)
                                            buffer = new byte[Math.Abs(currentPosition - endPosition)];

                                        currentPosition += await currentFS.ReadAsync(buffer, 0, buffer.Length);
                                        lstTst.AddRange(buffer);

                                        double value = (currentPosition - start) / (endPosition - start);
                                        Progress.Register(value, value, this.Handle, Methods.READ_ALL_BYTES);
                                    }
                                    Progress.Register(1.0, 1.0, this.Handle, Methods.READ_ALL_BYTES);
                                    return new Result<byte[]>(lstTst.ToArray());
                                }
                            }
                            else
                            {
                                if (mf.OriginalFile.Length().Result <= READ_RESTRICTION)
                                {
                                    List<byte> fileBytes = new List<byte>();
                                    using (var fsRead = await storage.OpenFile(mf.OriginalFile, Storage.FileAccess.Read, Storage.FileShare.ShareRead, (int)BUFFER_SIZE))
                                    {
                                        byte[] buffer = new byte[BUFFER_SIZE];

                                        long currentPosition = 0L;
                                        while (currentPosition < mf.OriginalFile.Length().Result)
                                        {
                                            if (Math.Abs(currentPosition - fsRead.Length) < BUFFER_SIZE)
                                                buffer = new byte[Math.Abs(currentPosition - fsRead.Length)];

                                            currentPosition += await fsRead.ReadAsync(buffer, 0, buffer.Length);
                                            fileBytes.AddRange(buffer);
                                            double value = currentPosition / fsRead.Length;
                                            Progress.Register(value, value, this.Handle, Methods.READ_ALL_BYTES);
                                        }
                                    }
                                    Progress.Register(1.0, 1.0, this.Handle, Methods.READ_ALL_BYTES);
                                    return new Result<byte[]>(fileBytes.ToArray());
                                }
                                else
                                    return new Result<byte[]>(null, false, new Exception("FILE_TOO_LARGE"));
                            }
                        }
                    }
                }
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
        /// Returns if the file contains the version 2
        /// </summary>
        /// <param name="path">The file to read in</param>
        /// <returns></returns>
        public static async Task<bool?> IsNewVersion(IFilePath file, IStorage storageImplementation)
        {
            if (await storageImplementation.FileExists(file))
            {
                try
                {
                    byte[] compArray = new byte[ExtendedVFS.StartSequence.Length];
                    using (var fs = await storageImplementation.OpenFile(file, Storage.FileAccess.Read, Storage.FileShare.ShareRead, 1))
                    {
                        byte[] buffer = new byte[1];
                        bool condition = false;
                        int currentCounter = 0;

                        while (currentCounter != ExtendedVFS.StartSequence.Length)
                        {
                            await fs.ReadAsync(buffer, 0, buffer.Length);
                            // Check if both arrays contains the same content
                            if (buffer[0] != ExtendedVFS.StartSequence[currentCounter++])
                            {
                                condition = false;
                                break;
                            }
                            else
                                condition = true;
                        }
                        return condition;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
                return false;
        }

        private async Task<Result<bool>> copyFileStream(ExtendedFile currentFile, IFilePath file, Methods m)
        {
            Func<Task<Result<bool>>> func = async () =>
            {
                using (var readStream = await storage.OpenFile(this.saveFile, Storage.FileAccess.Read, Storage.FileShare.ShareRead, (int)BUFFER_SIZE))
                {
                    using (var writeStream = await storage.OpenFile(file, Storage.FileAccess.Write, Storage.FileShare.ShareWrite, (int)BUFFER_SIZE))
                    {
                        long start = currentFile.StartPosition + this.Offset;
                        long end = currentFile.EndPosition + this.Offset;
                        long currentPosition = start;

                        byte[] buffer = new byte[BUFFER_SIZE];
                        readStream.Position = start;

                        while (currentPosition < end)
                        {
                            if (Math.Abs(currentPosition - end) < BUFFER_SIZE)
                                buffer = new byte[Math.Abs(currentPosition - end)];

                            currentPosition += await readStream.ReadAsync(buffer, 0, buffer.Length);
                            await writeStream.WriteAsync(buffer, 0, buffer.Length);

                            // currentPositon / end doesn't work here because we start not at zero!
                            // % = (currentPosition - start) / (end - start)
                            Progress.Register((currentPosition - start) / (double)(end - start), this.Handle, m);
                        }
                    }
                }
                return new Result<bool>(true, true, null);
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

        private async Task<IFilePath> createFile(string subPath, IDirectoryPath directory)
        {
            IDirectoryPath currentDir = directory;
            string[] segements = subPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < segements.Length - 1; i++)
            {
                await currentDir.CreateDirectory(segements[i]);
                foreach (var value in currentDir.GetDirectories().Result.Where(t => t.Name() == segements[i]))
                {
                    currentDir = value;
                    break;
                }
            }

            return await storage.CombinePath(currentDir, segements[segements.Length - 1]);
        }

        /// <summary>
        /// Extract all files and directories to the given path
        /// </summary>
        /// <param name="filePath">Path where the content will be extracted</param>
        /// <returns></returns>
        public override async Task<Result<bool>> Extract(IDirectoryPath directory)
        {
            Func<Task<Result<bool>>> func = async () =>
            {
                // filePath have to be a directory
                if (await storage.DirectoryExists(directory))
                {
                    // Extract now.
                    // Create directories
                    Func<IDirectory, Task> passDirs = null;
                    passDirs = async (IDirectory dir) =>
                    {
                        foreach (IDirectory currentDir in dir.GetSubDirectories())
                        {
                            try
                            {
                                await storage.CreateDirectory(directory, this.FormatPath(currentDir.ToFullPath()));
                            }
                            catch (Exception)
                            {
                            }
                            await passDirs(currentDir);
                        }

                    };
                    await passDirs(this.rootDir);

                    // Count files
                    int steps = (this.rootDir.GetSubDirectories().Count > 0 ? rootDir.GetSubDirectories()[0].GetFiles().Count : 0);
                    steps += this.rootDir.GetFiles().Count;
                    int counter = 0;

                    // Count files from all directories
                    Func<IDirectory, Task> countFiles = null;

                    countFiles = async (IDirectory dir) =>
                    {
                        foreach (IDirectory currentDir in dir.GetSubDirectories())
                        {
                            foreach (ExtendedFile currentFile in currentDir.GetFiles())
                                steps++;
                            await countFiles(currentDir);
                        }
                    };
                    await countFiles(this.rootDir);
                    Progress.Register(0.0, 0.0, this.Handle, Methods.EXTRACT);

                    // Create files
                    if (this.rootDir.GetSubDirectories().Count > 0)
                    {
                        foreach (ExtendedFile currentFile in this.rootDir.GetSubDirectories()[0].GetFiles())
                        {
                            string subPath = this.FormatPath(currentFile.Path);
                            await this.copyFileStream(currentFile, await createFile(subPath, directory), Methods.EXTRACT);
                            Progress.Register(++counter / (double)steps, this.Handle, Methods.EXTRACT, true);
                        }
                    }

                    // Write files of the directories
                    Func<IDirectory, Task> passFiles = null;

                    passFiles = async (IDirectory dir) =>
                    {
                        foreach (IDirectory currentDir in dir.GetSubDirectories())
                        {
                            foreach (ExtendedFile currentFile in currentDir.GetFiles())
                            {
                                string subPath = this.FormatPath(currentFile.GetPath());
                                await this.copyFileStream(currentFile, await createFile(subPath, directory), Methods.EXTRACT);

                                Progress.Register(++counter / (double)steps, this.Handle, Methods.EXTRACT, true);
                            }
                            await passFiles(currentDir);
                        }
                    };
                    await passFiles(this.rootDir);

                    Progress.Register(1.0, 1.0, this.Handle, Methods.EXTRACT);
                    return new Result<bool>(true);
                }
                else
                {
                    Progress.Register(1.0, 1.0, this.Handle, Methods.EXTRACT);
                    return new Result<bool>(false);
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
        /// Extracts the given directory to the given path
        /// </summary>
        /// <param name="currentDir">The virutal directory which should be extracted</param>
        /// <param name="toPath">Path where the virtual directory will be extracted</param>
        public override async Task<Result<bool>> ExtractDirectory(IDirectory currentDir, IDirectoryPath toPath)
        {
            bool failed = false;
            Exception exTmp = null;

            Func<Task<Result<bool>>> func = async () =>
            {
                if (await storage.DirectoryExists(toPath))
                {
                    Func<IDirectory, Task> passDirs = null;

                    try
                    {
                        await storage.CreateDirectory(toPath, currentDir.GetName());
                    }
                    catch (Exception e)
                    {
                        // ToDo: Handle exception
                    }

                    // Counting files for progress
                    int steps = currentDir.GetFiles().Count();
                    int currentCounter = 0;
                    Func<IDirectory, Task> countFiles = null;

                    countFiles = new Func<IDirectory, Task>(async (IDirectory currentDirectory) =>
                    {
                        foreach (IDirectory subDir in currentDirectory.GetSubDirectories())
                        {
                            foreach (ExtendedFile currentFile in subDir.GetFiles())
                            {
                                if (toPath == this.WorkSpacePath && currentFile.IsInvalid)
                                    continue; // Invalid file shouldn't be overriden
                                steps++;
                            }
                            await countFiles(subDir);
                        }
                    });
                    await countFiles(currentDir);
                    // Register progress
                    Progress.Register(0.0, 0.0, this.Handle, Methods.EXTRACT_DIR);

                    // Create files from main dir
                    foreach (ExtendedFile currentFile in currentDir.GetFiles())
                    {
                        if (toPath == this.WorkSpacePath && currentFile.IsInvalid)
                            continue; // Invalid file shouldn't be overriden

                        await this.copyFileStream(currentFile, await createFile(currentFile.GetName(), toPath), Methods.EXTRACT_DIR);
                        Progress.Register(++currentCounter / (double)steps, this.Handle, Methods.EXTRACT_DIR, true);
                    }

                    passDirs = new Func<IDirectory, Task>(async (IDirectory currentDirectory) =>
                    {
                        foreach (IDirectory subDir in currentDirectory.GetSubDirectories())
                        {
                            try
                            {
                               await storage.CreateDirectory(toPath, currentDir.GetName() + @"\" + subDir.ToFullPath(currentDir));
                                foreach (ExtendedFile currentFile in subDir.GetFiles())
                                {
                                    if (toPath == this.WorkSpacePath && currentFile.IsInvalid)
                                        continue; // Invalid file shouldn't be overriden

                                    await this.copyFileStream(currentFile, await createFile(currentDir.GetName() + @"\" + subDir.ToFullPath(currentDir) + @"\" + currentFile.GetName(), toPath), Methods.EXTRACT_DIR);
                                    Progress.Register(++currentCounter / (double)steps, this.Handle, Methods.EXTRACT_DIR, true);
                                }
                            }
                            catch (Exception ex)
                            {
                                failed = true;
                                exTmp = ex;
                                return;
                            }
                            await passDirs(subDir);
                        }
                    });
                    await passDirs(currentDir);
                }

                Progress.Register(1.0, 1.0, this.Handle, Methods.EXTRACT_DIR);
                return new Result<bool>(failed, !failed, exTmp);
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
        /// Extract the directory to a given path
        /// </summary>
        /// <param name="path">The virutal path where the directory is stored</param>
        /// <param name="filePath">Path where the directory will be extracted</param>
        public override async Task<Result<bool>> ExtractDirectory(string path, IDirectoryPath filePath)
        {
            string[] segments = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            ExtendedDirectory currentNode = this.RootDirectory as ExtendedDirectory;

            for (int i = 0; i <= segments.Length - 1; i++)
            {
                string currentSegment = segments[i];
                if (currentNode.Contains(currentSegment))
                    currentNode = (ExtendedDirectory)currentNode.GetSubDirectories()[currentNode.IndexOf(currentSegment)];
                else
                {
                    currentNode = null;
                    break;
                }
            }

            if (currentNode != null)
                return await this.ExtractDirectory(currentNode, filePath);
            else
                return new Result<bool>(false);
        }

        /// <summary>
        /// Extracts the given files to the given path
        /// </summary>
        /// <param name="files">The files which should be extracted</param>
        /// <param name="directoryPath">Path where the files will be extracted</param>
        public override async Task<Result<bool>> ExtractFiles(string[] files, IDirectoryPath directoryPath)
        {
            // Gathering files as ModifiedFile-Array
            ExtendedFile[] nFiles = new ExtendedFile[files.Length];

            for (int i = 0; i <= files.Length - 1; i++)
            {
                string currentFile = files[i];
                string newFile = string.Empty;
                string[] segements = currentFile.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

                for (int s = 0; s <= segements.Length - 2; s++)
                    newFile += segements[s] + @"\";

                ExtendedDirectory node = (ExtendedDirectory)this.RootDirectory.CalculateLastNode(newFile);
                if (NULLFILE.Contains(node.GetFiles(), currentFile))
                    nFiles[i] = (ExtendedFile)NULLFILE.ByPath(node.GetFiles(), currentFile);
            }

            return await this.ExtractFiles(nFiles, directoryPath);
        }

        /// <summary>
        /// Extracts the given files to the given path
        /// </summary>
        /// <param name="files">The files which should be extracted</param>
        /// <param name="directoryPath">Path where the files will be extracted</param>
        public override async Task<Result<bool>> ExtractFiles(IFile[] files, IDirectoryPath directoryPath)
        {
            if (files == null || directoryPath == null || !(await storage.DirectoryExists(directoryPath)))
                return new Result<bool>(false);

            Progress.Register(0.0, 0.0, this.Handle, Methods.EXTRACT_FILES);

            int steps = files.Length;
            int currentStep = 0;

            try
            {
                foreach (ExtendedFile currentFile in files)
                {
                    string[] segments = currentFile.Path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    if (segments.Length > 0)
                    {
                        if (directoryPath == this.WorkSpacePath && currentFile.IsInvalid)
                            continue; // Invalid file shouldn't be overriden.

                        Result<bool> rst = await this.copyFileStream(currentFile, await createFile(@"\" + segments[segments.Length - 1], directoryPath), Methods.EXTRACT_FILES);
                        if (!rst.Value)
                        {
                            Progress.Register(1.0, 1.0, this.Handle, Methods.EXTRACT_FILES);
                            return rst;
                        }

                        double value = ++currentStep / steps;
                        Progress.Register(value, value, this.Handle, Methods.EXTRACT_FILES);
                    }
                }

                Progress.Register(1.0, 1.0, this.Handle, Methods.EXTRACT_FILES);
                return new Result<bool>(true);
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, false, ex);
            }
        }

        /// <summary>
        /// Removes a virtual file
        /// </summary>
        /// <param name="path">Path of the virtual file</param>
        /// <param name="startNode">The directory where the path is beginning</param>
        /// <returns></returns>
        public override async Task<Result<bool>> RemoveFile(string path, IDirectory startNode)
        {
            Result<bool> result = await base.RemoveFile(path, startNode);
            
            if (this.SaveAfterChange)
                await this.Save();

            return result;
        }

        /// <summary>
        /// Writes bytes into a file in the workspace directory (while saving the file will be saved too)
        /// </summary>
        /// <param name="data">The bytes to write</param>
        /// <param name="name">The name of the file</param>
        /// <param name="dir">The directory where the file is stored in</param>
        /// <param name="overrideExisting">Determines if the file will be replace if the file is already exisiting</param>
        /// <returns></returns>
        public override async Task<Result<bool>> WriteAllBytes(byte[] data, string name, IDirectory dir, bool overrideExisting = false)
        {
            // Writing the file: 1. Put this file into the workspace (Creating all directories to file.GetParent() or dir)
            //                   2. Mark the file as changed
            //                   3. Save if required

            // Check if the file is already there
            if (!overrideExisting)
            {
                foreach (ExtendedFile ms in dir.GetFiles())
                {
                    if (ms.GetName() == name)
                        return new Result<bool>(false);
                }
            }

            IDirectoryPath createdDir = null;
            try
            {
                createdDir = await storage.CreateDirectory(this.WorkSpacePath, this.FormatPath(dir.ToFullPath()));
            }
            catch (Exception ex)
            {
                return new Result<bool>(false, false, ex);
            }

            Progress.Register(0.0, 0.0, this.Handle, Methods.WRITE_ALL_BYTES);

            // CreateFile
            ExtendedFile mf = new ExtendedFile(new HeaderInfo(dir.ToFullPath() + @"\" + name, -data.Length, 0, false), dir as ExtendedDirectory, storage);
            await mf.Initalize();
            mf.IsInvalid = true;

            ExtendedFile oldFile = null;
            foreach (ExtendedFile ms in dir.GetFiles())
            {
                if (ms.GetName() == name)
                {
                    oldFile = ms;
                    break;
                }
            }

            if (oldFile != null)
                dir.GetFiles().Remove(oldFile);
            dir.GetFiles().Add(mf);

            // Write the bytes to dirsToCreate:
            mf.OriginalFile = await storage.CombinePath(createdDir, name);

            Task<Result<bool>> func = Task.Run(async () =>
            {
                using (var writeStream = await storage.OpenFile(mf.OriginalFile, Storage.FileAccess.Write, Storage.FileShare.ShareWrite, (int)BUFFER_SIZE))
                {
                    long endPosition = data.Length;
                    int currentCounter = 0;
                    byte[] buffer = new byte[BUFFER_SIZE];

                    long steps = data.Length / BUFFER_SIZE;
                    long moreSteps = data.Length % BUFFER_SIZE;

                    if (moreSteps < BUFFER_SIZE && moreSteps != 0)
                        steps++;

                    if (steps == 1)
                    {
                        buffer = new byte[data.Length];

                        for (int s = 0; s <= data.Length - 1; s++)
                        {
                            double value = (s + 1) / (double)data.Length;
                            Progress.Register(value, value, this.Handle, Methods.WRITE_ALL_BYTES);
                            buffer[s] = data[s];
                        }

                        await writeStream.WriteAsync(buffer, 0, buffer.Length);
                        Progress.Register(1.0, 1.0, this.Handle, Methods.WRITE_ALL_BYTES);
                    }
                    else
                    {
                        for (int i = 0; i <= steps - 1; i++)
                        {
                            if (i != steps - 1)
                            {
                                for (int s = 0; s <= BUFFER_SIZE - 1; s++)
                                    buffer[s] = data[currentCounter++];

                                await writeStream.WriteAsync(buffer, 0, buffer.Length);
                            }
                            else
                            {
                                buffer = new byte[data.Length - currentCounter];

                                for (int s = 0; s <= buffer.Length - 1; s++)
                                    buffer[s] = data[currentCounter++];

                                await writeStream.WriteAsync(buffer, 0, buffer.Length);
                            }

                            // Progress
                            double value = (i + 1) / (double)steps;
                            Progress.Register(value, value, this.Handle, Methods.WRITE_ALL_BYTES);
                        }

                        Progress.Register(1.0, 1.0, this.Handle, Methods.WRITE_ALL_BYTES);
                    }
                }

                if (this.SaveAfterChange)
                    await this.Save();

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
        /// Writes bytes into a file in the workspace directory (while saving the file will be saved too)
        /// </summary>
        /// <param name="data">The bytes to write</param>
        /// <param name="path">The path where the file is stored in</param>
        /// <param name="overrideExisting">Determines if the file will be replace if the file is already exisiting</param>
        /// <returns></returns>
        public override async Task<Result<bool>> WriteAllBytes(byte[] data, string path, bool overrideExisting = false)
        {
            string[] segments = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

            // Generate path without filename first
            string nPathWithoutFileName = string.Empty;
            for (int i = 0; i <= segments.Length - 2; i++)
                nPathWithoutFileName += segments[i] + @"\";

            string fileName = string.Empty;
            if (string.IsNullOrEmpty(nPathWithoutFileName))
            {
                nPathWithoutFileName = @"\";
                fileName = path.Replace(@"\", string.Empty);
            }
            else
                fileName = segments[segments.Length - 1];

            return await this.WriteAllBytes(data, fileName, this.rootDir.CalculateLastNode(nPathWithoutFileName), overrideExisting);
        }

        /// <summary>
        /// Writes bytes into a file in the workspace directory (while saving the file will be saved too)
        /// </summary>
        /// <param name="content">The string which should be written into the file</param>
        /// <param name="name">The name of the file</param>
        /// <param name="dir">The directory where the file is stored in</param>
        /// <param name="overrideExisting">Determines if the file will be replace if the file is already exisiting</param>
        /// <returns></returns>
        public override async Task<Result<bool>> WriteAllText(string content, string name, IDirectory dir, bool overrideExisting = false)
        {
            return await this.WriteAllBytes(System.Text.Encoding.UTF8.GetBytes(content), name, dir, overrideExisting);
        }


        /// <summary>
        /// Writes a stream (file) into a file in the workspace directory (while saving the file will be saved too)
        /// </summary>
        /// <param name="name">The name of the file</param>
        /// <param name="dir">The directory where the file is stored in</param>
        /// <param name="stream">The stream which will be written to a file stream</param>
        /// <param name="overrideExisting">Determines if the file will be replace if the file is already exisiting</param>
        /// <returns></returns>
        public override async Task<Result<bool>> WriteStream(string name, IDirectory dir, IStream stream, bool overrideExisting = false)
        {
            if (stream == null)
                return new Result<bool>(false);

            Task<Result<bool>> task = Task.Run(async () =>
            {
                // Check if the file is already there
                if (!overrideExisting)
                {
                    foreach (ExtendedFile ms in dir.GetFiles())
                    {
                        if (ms.GetName() == name)
                            return new Result<bool>(false);
                    }
                }

                Progress.Register(0.0, 0.0, this.Handle, Methods.WRITE_STREAM);

                IDirectoryPath currentDir = null;
                try
                {
                    currentDir = await storage.CreateDirectory(this.WorkSpacePath, this.FormatPath(dir.ToFullPath()));
                }
                catch (Exception ex)
                {
                    return new Result<bool>(false, false, ex);
                }

                // CreateFile
                ExtendedFile mf = new ExtendedFile(new HeaderInfo(dir.ToFullPath() + @"\" + name, -stream.Length, 0, false), dir as ExtendedDirectory, storage);
                await mf.Initalize();
                mf.IsInvalid = true;
                dir.GetFiles().Add(mf);

                // Write the bytes to dirsToCreate:
                mf.OriginalFile = await storage.CombinePath(currentDir, name);

                using (var writeStream = await storage.OpenFile(mf.OriginalFile, Storage.FileAccess.Write, Storage.FileShare.ShareWrite, (int)BUFFER_SIZE))
                {
                    byte[] buffer = new byte[BUFFER_SIZE];
                    long currentPosition = 0;

                    while (currentPosition < stream.Length)
                    {
                        if (currentPosition + BUFFER_SIZE > stream.Length)
                            buffer = new byte[stream.Length - currentPosition];

                        currentPosition += await stream.ReadAsync(buffer, 0, buffer.Length);
                        await writeStream.WriteAsync(buffer, 0, buffer.Length);

                        double progress = currentPosition / stream.Length;
                        Progress.Register(progress, progress, this.Handle, Methods.WRITE_STREAM);
                    }
                    Progress.Register(1.0, 1.0, this.Handle, Methods.WRITE_STREAM);
                }

                try
                {
                    stream.Close();
                    stream.Dispose();
                }
                catch (Exception ex)
                {
                    // In this case it's just a problem to close a stream,
                    // but the data is valid
                    if (this.SaveAfterChange)
                        await this.Save();

                    // Using a using-block may causes this exception
                    return new Result<bool>(true, true, ex);
                }

                if (this.SaveAfterChange)
                    await this.Save();

                return new Result<bool>(true); 
            });

            return await task;
        }

        /// <summary>
        /// Clears the content of the workspace-directory
        /// </summary>
        public void ClearWorkspaceDirectory()
        {
           WorkSpacePath.Remove(true);
            /*
            // Delete all files at top and delete all directories in top recursive
            foreach (System.IO.DirectoryInfo di in new System.IO.DirectoryInfo(this.WorkSpacePath).GetDirectories("*.*", SearchOption.TopDirectoryOnly))
                di.Delete(true);

            foreach (System.IO.FileInfo fi in new System.IO.DirectoryInfo(this.WorkSpacePath).GetFiles("*.*", SearchOption.TopDirectoryOnly))
                fi.Delete();*/
        }

        /// <summary>
        /// Automatically called when SaveAfterChange is true.
        /// This method is for saving changes.
        /// </summary>
        /// <returns></returns>
        public override async Task<Result<bool>> Save()
        {
            // -------------------------------------------------------------------------------------------------------
            // Idea: 1. Extract all files to working dir. If the file exists and is invalid, doesn't replace the file
            //       2. Generating a new file with CreateVHP
            //       3. Do a read call
            //       4. Clear workspace directory
            // -------------------------------------------------------------------------------------------------------
            Progress.Register(0.0, 0.0, this.Handle, Methods.SAVE);
            if (this.RootDirectory.GetSubDirectories().Count < 1)
            {
                // We can't return because if we just have files, we have to save them.
                // ToDo: Implement this
                if (this.RootDirectory.GetFiles().Count < 1)
                    return new Result<bool>(false, false, null);

                Progress.Register(1.0, 0.25, this.Handle, Methods.SAVE);
                // Extract files, clear vPathes, CreateVHP
                // Extract files from root directory into workspace directory
                await this.ExtractFiles(this.RootDirectory.GetFiles().ToArray(), this.WorkSpacePath);

                this.RootDirectory.GetSubDirectories().Clear();
                this.RootDirectory.GetFiles().Clear();

                await storage.DeleteFile(this.saveFile);

                Progress.Register(1.0, 0.5, this.Handle, Methods.SAVE);
                //this.CreateVHP(this.WorkSpacePath);
                // Another idea: read all files in workspace dir but only at the top directory
                List<IFilePath> files = new List<IFilePath>();
                foreach (IFilePath fi in this.WorkSpacePath.GetFiles().Result)
                    files.Add(fi);

                // VHP without directories, just with these files.
                await this.Create(files.ToArray(), new IDirectoryPath[] { });

                Progress.Register(1.0, 0.75, this.Handle, Methods.SAVE);
                await this.Read(this.saveFile);
                this.ClearWorkspaceDirectory();

                Progress.Register(1.0, 1.0, this.Handle, Methods.SAVE);
                return new Result<bool>(true, true, null);
            }

            // Step 1
            Progress.Register(1.0, 0.25, this.Handle, Methods.SAVE);
            await this.ExtractDirectory(this.RootDirectory, this.WorkSpacePath);

            // Step 2: Clear root directory
            this.RootDirectory.GetFiles().Clear();
            this.RootDirectory.GetSubDirectories().Clear();

            Progress.Register(1.0, 0.5, this.Handle, Methods.SAVE);

            // Gathering directories and files
            List<IFilePath> files1 = new List<IFilePath>();
            List<IDirectoryPath> directories = new List<IDirectoryPath>();

            foreach (IFilePath fi in this.WorkSpacePath.GetFiles().Result)
                files1.Add(fi);
            foreach (IDirectoryPath di in this.WorkSpacePath.GetDirectories().Result)
                directories.Add(di);

            await this.Create(files1.ToArray(), directories.ToArray());

            // Step 3
            Progress.Register(1.0, 0.75, this.Handle, Methods.SAVE);
            await this.Read(this.saveFile);

            // Step 4
            // Delte directory entirely and create new one
            this.ClearWorkspaceDirectory();
            Progress.Register(1.0, 1.0, this.Handle, Methods.SAVE);

            return new Result<bool>(true, true, null);
        }
    }
}
