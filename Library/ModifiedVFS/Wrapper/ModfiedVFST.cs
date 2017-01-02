// ------------------------------------------------------------------------
// ModifiedVFST.cs written by Code A Software (http://www.code-a-software.net)
// SP: VHP-0001 (OpenSource-Software)
// Created on:      27.12.2016
// Last update on:  30.12.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using VFS.Interfaces;
using System.IO;

namespace VFS.ModifiedVFS.Wrapper
{
    /// <summary>
    /// Thread-based and exception handling ModifiedVFS
    /// </summary>
    public class ModifiedVFST : ModifiedVFS
    {
        private Thread currentThread = null;
        private bool isBusy = false;
        private Result result = null;
        private Stopwatch stw = null;

        /// <summary>
        /// A result which is false - no need to create a false result every time when needed a false result
        /// </summary>
        public readonly Result defaultResult = new Result(false, false);

        /// <summary>
        /// This event is called when the method is finishing the progress
        /// </summary>
        /// <param name="result">The result information from the method</param>
        public delegate void onFinishedEvent(Result result);

        /// <summary>
        /// This event is called when the method is finishing the progress (Be careful with calling another method, because it will be called every time when onFinished is fired)
        /// </summary>
        public event onFinishedEvent OnFinished;
                
        /// <summary>
        /// Returns the StopWatch-Instance of the current process
        /// </summary>
        public Stopwatch CurrentStopWatch
        {
            get
            {
                return this.stw;
            }
        }

        /// <summary>
        /// Instantiate a new VFS which is modified and thread-based
        /// </summary>
        /// <param name="logPath">The path for the logging-file</param>
        /// <param name="savePath">The path where the file will be created</param>
        /// <param name="workSpacePath">The path of the cliboard (Please do not save an files or directories there!)</param>
        /// <param name="BufferSize">The size of the file-buffer in bytes</param>
        public ModifiedVFST(string logPath, string savePath, string workSpacePath, long BufferSize = 32768) : base(logPath, savePath, workSpacePath, 0, 0, BufferSize)
        { }

        #region Private
        private void endStopWatch()
        {
            if (this.stw != null)
            {
                this.stw.Stop();
                this.stw = null;
            }
        }

        private void onFinished(Result result)
        {
            this.isBusy = false;
            this.result = result;

            this.OnFinished?.Invoke(result);
        }

        private bool changeBusy()
        {
            if (this.isBusy)
            {
                this.OnFinished?.Invoke(defaultResult);
                return false;
            }

            this.isBusy = true;
            return true;
        }
#endregion

        /// <summary>
        /// Creates a new VHP
        /// </summary>
        /// <param name="directory">All files and directories of this directory will be progressed into the VHP</param>
        /// <returns></returns>
        public override bool CreateVHP(string directory)
        {
            if (!this.changeBusy())
                return false;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    bool result = base.CreateVHP(directory);
                    this.onFinished(new Result(result, Methods.CREATE));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.CREATE));
                }
            }));

            currentThread.Start();
            return true;
        }

        /// <summary>
        /// Creates a new VHP
        /// </summary>
        /// <param name="file">Just this one file will be taken to be progressed</param>
        /// <param name="f">Just for differnitate the methods</param>
        /// <returns></returns>
        public override bool CreateVHP(string file, bool f = true)
        {
            if (!this.changeBusy())
                return false;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    bool result = base.CreateVHP(file, f);
                    this.onFinished(new Result(result, Methods.CREATE));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.CREATE));
                }
            }));

            currentThread.Start();
            return true;
        }

        /// <summary>
        /// Creates a new VHP
        /// </summary>
        /// <param name="directories">This directories will be taken to be progressed</param>
        /// <param name="files">This files will be taken to be progressed</param>
        /// <returns></returns>
        public override bool CreateVHP(string[] directories, string[] files)
        {
            if (!this.changeBusy())
                return false;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    bool result = base.CreateVHP(directories, files);
                    this.onFinished(new Result(result, Methods.CREATE));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.CREATE));
                }
            }));

            currentThread.Start();
            return true;
        }

        /// <summary>
        /// Extract all files and directories to the given path
        /// </summary>
        /// <param name="filePath">Path where the content will be extracted</param>
        /// <returns></returns>
        public override bool Extract(string filePath)
        {
            if (!this.changeBusy())
                return false;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    bool result = base.Extract(filePath);
                    this.onFinished(new Result(result, Methods.EXTRACT));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.EXTRACT));
                }
            }));

            currentThread.Start();
            return true;
        }

        /// <summary>
        /// Extract the directory to a given path
        /// </summary>
        /// <param name="path">The virutal path where the directory is stored</param>
        /// <param name="filePath">Path where the directory will be extracted</param>
        public override void ExtractDirectory(string path, string filePath)
        {
            if (!this.changeBusy())
                return;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {

                try
                {
                    stw = Stopwatch.StartNew();
                    base.ExtractDirectory(path, filePath);
                    this.onFinished(new Result(true, Methods.EXTRACT_DIR));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.EXTRACT_DIR));
                }
            }));
            currentThread.Start();
        }

        /// <summary>
        /// Extracts the given directory to the given path
        /// </summary>
        /// <param name="currentDir">The virutal directory which should be extracted</param>
        /// <param name="toPath">Path where the virtual directory will be extracted</param>
        public override void ExtractDirectory(IDirectory currentDir, string toPath)
        {
            if (!this.changeBusy())
                return;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    base.ExtractDirectory(currentDir, toPath);
                    this.onFinished(new Result(true, Methods.EXTRACT_DIR));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.EXTRACT_DIR));
                }
            }));

            currentThread.Start();
        }

        /// <summary>
        /// Extracts the given files to the given path
        /// </summary>
        /// <param name="files">The files which should be extracted</param>
        /// <param name="directoryPath">Path where the files will be extracted</param>
        public override void ExtractFiles(IFile[] files, string directoryPath)
        {
            if (!this.changeBusy())
                return;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    base.ExtractFiles(files, directoryPath);
                    this.onFinished(new Result(true, Methods.EXTRACT_FILES));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.EXTRACT_FILES));
                }
            }));

            currentThread.Start();
        }

        /// <summary>
        /// Extracts the given files to the given path
        /// </summary>
        /// <param name="files">The files which should be extracted</param>
        /// <param name="directoryPath">Path where the files will be extracted</param>
        public override void ExtractFiles(string[] files, string directoryPath)
        {
            if (!this.changeBusy())
                return;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    base.ExtractFiles(files, directoryPath);
                    this.onFinished(new Result(true, Methods.EXTRACT_FILES));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.EXTRACT_FILES));
                }
            }));

            currentThread.Start();
        }

        /// <summary>
        /// Returns true if a file is already existing
        /// </summary>
        /// <param name="path">Path of the virtual file</param>
        /// <param name="startNode">The directory where the path is beginning</param>
        /// <returns></returns>
        public override bool FileExists(string path, IDirectory startNode)
        {
            // No thread based needed for this method. Overriding is just to be sure
            return base.FileExists(path, startNode);
        }

        /// <summary>
        /// Removes a virtual file
        /// </summary>
        /// <param name="path">Path of the virtual file</param>
        /// <param name="startNode">The directory where the path is beginning</param>
        /// <returns></returns>
        public override bool RemoveFile(string path, IDirectory startNode)
        {
            // No thread based needed for this method.
            // It just deletes the file out of the list, but if you save then thread based.
            if (!this.SaveAfterChange)
                return base.RemoveFile(path, startNode);
            else
            {
                // Thread based
                if (!this.changeBusy())
                    return false;

                this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                    try
                    {
                        stw = Stopwatch.StartNew();
                        base.RemoveFile(path, startNode);
                        this.onFinished(new Result(true, Methods.REMOVE_FILE));
                        this.endStopWatch();
                    }
                    catch (Exception e)
                    {
                        this.onFinished(new Result(false, e, Methods.REMOVE_FILE));
                    }
                }));
                currentThread.Start();
            }

            return true;
        }

        /// <summary>
        /// Loads a VHP-File into the RAM (just header-content)
        /// </summary>
        /// <param name="filePath">The path where the vhp-file is stored</param>
        public override void Read(string filePath)
        {
            if (!this.changeBusy())
                return;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {

                try
                {
                    stw = Stopwatch.StartNew();
                    base.Read(filePath);
                    this.onFinished(new Result(true, Methods.READ));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.READ));
                }
            }));
            currentThread.Start();
        }

        /// <summary>
        /// Returns the bytes of a virutal files (reading from originial file) (Max: 1 GB)
        /// </summary>
        /// <param name="path">Path of the virtual file</param>
        /// <param name="startNode">The directory where the path is beginning</param>
        /// <param name="different">Just to differniate between these methods (not used in this method)</param>
        /// <returns></returns>
        public override byte[] ReadAllBytes(string path, IDirectory startNode, bool different = false)
        {
            if (!this.changeBusy())
                return null;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    byte[] temp = base.ReadAllBytes(path, startNode, different);
                    this.onFinished(new Result(true, temp, Methods.READ_ALL_BYTES));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.READ_ALL_BYTES));
                }
            }));
            currentThread.Start();

            return null;
        }

        /// <summary>
        /// Returns the content of a virtual file as a string (Max: 1 GB)
        /// </summary>
        /// <param name="path">Path of the virtual file</param>
        /// <param name="startNode">The directory where the path is beginning</param>
        /// <returns></returns>
        public override string ReadAllText(string path, IDirectory startNode)
        {
            if (!this.changeBusy())
                return null;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    string temp = base.ReadAllText(path, startNode);
                    this.onFinished(new Result(true, temp, Methods.READ_ALL_TEXT));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.READ_ALL_TEXT));
                }
            }));
            currentThread.Start();

            return null;
        }

        /// <summary>
        /// Writes a stream (file) into a file in the workspace directory (while saving the file will be saved too)
        /// </summary>
        /// <param name="name">The name of the file</param>
        /// <param name="dir">The directory where the file is stored in</param>
        /// <param name="stream">The stream which will be written to a file stream</param>
        /// <param name="overrideExisting">Determines if the file will be replace if the file is already exisiting</param>
        /// <returns></returns>
        public override bool WriteStream(string name, IDirectory dir, Stream stream, bool overrideExisting = false)
        {
            if (!this.changeBusy())
                return false;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    bool result = base.WriteStream(name, dir, stream, overrideExisting);
                    this.onFinished(new Result(result, Methods.WRITE_STREAM));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.WRITE_STREAM));
                }
            }));

            currentThread.Start();
            return true;
        }

        /// <summary>
        /// Writes bytes into a file in the workspace directory (while saving the file will be saved too)
        /// </summary>
        /// <param name="data">The bytes to write</param>
        /// <param name="name">The name of the file</param>
        /// <param name="dir">The directory where the file is stored in</param>
        /// <param name="overrideExisting">Determines if the file will be replace if the file is already exisiting</param>
        /// <returns></returns>
        public override bool WriteAllBytes(byte[] data, string name, IDirectory dir, bool overrideExisting = false)
        {
            if (!this.changeBusy())
                return false;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    base.WriteAllBytes(data, name, dir, overrideExisting);
                    this.onFinished(new Result(true, Methods.WRITE_ALL_BYTES));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.WRITE_ALL_BYTES));
                }
            }));
            currentThread.Start();

            return true;
        }

        /// <summary>
        /// Writes bytes into a file in the workspace directory (while saving the file will be saved too)
        /// </summary>
        /// <param name="data">The bytes to write</param>
        /// <param name="path">The path where the file is stored in</param>
        /// <param name="overrideExisting">Determines if the file will be replace if the file is already exisiting</param>
        /// <returns></returns>
        public override bool WriteAllBytes(byte[] data, string path, bool overrideExisting = false)
        {
            if (!this.changeBusy())
                return false;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    base.WriteAllBytes(data, path, overrideExisting);
                    this.onFinished(new Result(true, Methods.WRITE_ALL_BYTES));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.WRITE_ALL_BYTES));
                }
            }));
            currentThread.Start();

            return true;
        }

        /// <summary>
        /// Writes bytes into a file in the workspace directory (while saving the file will be saved too)
        /// </summary>
        /// <param name="content">The string which should be written into the file</param>
        /// <param name="name">The name of the file</param>
        /// <param name="dir">The directory where the file is stored in</param>
        /// <param name="overrideExisting">Determines if the file will be replace if the file is already exisiting</param>
        /// <returns></returns>
        public override bool WriteAllText(string content, string name, IDirectory dir, bool overrideExisting = false)
        {
            if (!this.changeBusy())
                return false;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    base.WriteAllText(content, name, dir, overrideExisting);
                    this.onFinished(new Result(true, Methods.WRITE_ALL_TEXT));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.WRITE_ALL_TEXT));
                }
            }));
            currentThread.Start();

            return true;
        }

        /// <summary>
        /// Automatically called when SaveAfterChange is true.
        /// This method is for saving changes.
        /// </summary>
        /// <returns></returns>
        public override bool Save()
        {
            if (!this.changeBusy())
                return false;

            this.currentThread = new Thread(new ParameterizedThreadStart(delegate {
                try
                {
                    stw = Stopwatch.StartNew();
                    bool result = base.Save();
                    this.onFinished(new Result(result, Methods.SAVE));
                    this.endStopWatch();
                }
                catch (Exception e)
                {
                    this.onFinished(new Result(false, e, Methods.SAVE));
                }
            }));

            currentThread.Start();
            return true;
        }
    }
}
