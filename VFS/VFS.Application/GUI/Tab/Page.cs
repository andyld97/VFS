// ------------------------------------------------------------------------
// Page.cs written by Code A Software (http://www.code-a-software.net)
// All rights reserved
// Created on:      11.04.2016
// Last update on:  19.11.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VFS;
using VFS.ExtendedVFS;
using VFS.Interfaces;
using VFS.Net;
using VN = VFS.Net;

namespace VFS.Application.GUI.Tab
{
    public class Page : Design.DesignPanel
    {
        private string path = string.Empty;
        private string filePath = string.Empty;
        public new readonly string Name;
        private VFS currentFileSystem = null;
        private IDirectory currentDirectory = null;
        private Element readElement = null;
        private Settings sc;

        // Controls
        private ListView currentControl = new ListView();
        private Preview currentPreview = null;

        public delegate void onSelectedChanged(Element[] elements);
        public event onSelectedChanged OnSelectedChanged;

        public delegate void onSideChanged(Info information);
        public event onSideChanged OnSideChanged;

        public delegate void onRefresh();
        public event onRefresh OnRefresh;

        public delegate void onDoubleClickOnFile(IFile currentFile);
        public event onDoubleClickOnFile OnDoubleClickOnFile;

        public delegate void onPathChanged(string path);
        public event onPathChanged OnPathChanged;

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;

                bool success = true;
                IDirectory node = RootDirectory;
                string[] segements = value.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

                for (int s = 0; s < segements.Length; s++)
                {
                    string currentSegment = segements[s];
                    if (node.Contains(currentSegment))
                    {
                        IDirectory tempNode = node.GetSubDirectories().Where(t => t.GetName() == currentSegment).FirstOrDefault();
                        if (tempNode != null)
                            node = tempNode;
                        else
                        {
                            success = false;
                            break;
                        }
                    }
                }

                if (success)
                    CurrentDirectory = node;
            }
        }

        public bool DisplayHorizontal
        {
            get
            {
                return this.currentControl.DisplayHorizontal;
            }
            set
            {
                this.currentControl.DisplayHorizontal = value;
            }
        }

        public Page()
        {    

        }

        public IDirectory CurrentDirectory
        {
            get
            {
                return this.currentDirectory;
            }
            set
            {
                if (value != null)
                {
                    this.currentDirectory = value;
                    this.Refresh();
                }
            }
        }

        public IDirectory RootDirectory
        {
            get
            {
                return this.currentFileSystem.RootDirectory;
            }
        }

        public VFS CurrentFileSystem
        {
            get
            {
                return this.currentFileSystem;
            }
        }

        public bool IsModified
        {
            get
            {
                return this.currentFileSystem is ExtendedVFS.ExtendedVFS;
            }
        }

        public Page(string filePath, string name)
        {
            this.path = @"\";
            this.filePath = filePath;
            this.Name = name;

            this.Controls.Add(this.currentControl);
            this.currentControl.Dock = DockStyle.Fill;
            this.currentControl.OnDoubleClickedElement += CurrentControl_OnDoubleClickedElement;
            this.currentControl.OnSelectedIndexChanged += CurrentControl_OnSelectedIndexChanged;
            this.currentControl.OnSizeChanged_ += CurrentControl_OnSizeChanged_;

            // Load settings here
            sc = Settings.Read();

            // ToDo: Check if file is opened alreday
        }

        public async Task Load()
        {
            // Get right VFS version
            bool? result = await ExtendedVFS.ExtendedVFS.IsNewVersion(new VN.FilePath(this.filePath), NetStorage.NETStorageProvider);
            if (result.HasValue && result.Value)
            {
                this.currentFileSystem = new ExtendedVFS.ExtendedVFS(new VN.FilePath(this.filePath), NetStorage.NETStorageProvider, sc.BufferSize);
                var x = await this.currentFileSystem.Read(new VN.FilePath(this.filePath));
            }
            else
            {
                this.currentFileSystem = new SplitVFS(new VN.FilePath(this.filePath), NetStorage.NETStorageProvider, sc.MainCounter, sc.PackByte);
                var x = await this.currentFileSystem.Read(new VN.FilePath(this.filePath));
            }

            // ToDo: Check if var x is true if false show the user the exception
        }

        public void LoadAfterLoad()
        {
            if (this.currentFileSystem is SplitVFS)
                (this.CurrentFileSystem.RootDirectory as Directory).OnChanged += RootDirectory_OnChanged;
            this.currentDirectory = this.CurrentFileSystem.RootDirectory;
            this.RootDirectory_OnChanged(Directory.Type.Default);
            this.Refresh();
        }

        private void CurrentControl_OnSizeChanged_(Info information)
        {
            this.OnSideChanged?.Invoke(information);
        }

        private void CurrentControl_OnSelectedIndexChanged(Element[] elementsSelected)
        {
            this.OnSelectedChanged?.Invoke(elementsSelected);
        }

        private void CurrentControl_OnDoubleClickedElement(Element selectedElement)
        {
            Element toSelectElement = selectedElement;
            if (toSelectElement != null)
            {
                if (toSelectElement.CurrentDir != null)
                {
                    this.currentDirectory = toSelectElement.CurrentDir;
                    this.path = this.CurrentDirectory.ToFullPath(null);
                    this.OnPathChanged?.Invoke(this.path);
                    this.Refresh();
                }
                else
                {
                    // Doing something with a file.
                    if (this.OnDoubleClickOnFile != null)
                        this.OnDoubleClickOnFile(toSelectElement.CurrentFile);
                    this.OpenFile(selectedElement);
                }
            }
        }     

        private void RootDirectory_OnChanged(Directory.Type action)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => this.Refresh()));
            else
                this.Refresh();
        }

        private void CurrentFileSystem_OnReady()
        {
            this.currentDirectory = this.currentFileSystem.RootDirectory;
            this.Invoke(new Action(() => this.Refresh()));
        }

        private void CurrentFileSystem_OnSaved()
        {
            this.Invoke(new Action(() => this.Refresh()));
        }

        public async void OpenFile(Element currentElement)
        {
            if (currentElement.IsFile)
            {
                this.CurrentDirectory = currentElement.CurrentFile.GetParentDirectory();
                this.currentControl.SelectedElement = currentElement;
                this.readElement = currentElement;

                // But check if this file isn't opened already
                bool isOpenedAlready = false;

                if (!isOpenedAlready)
                {
                    Result<byte[]> res = await this.CurrentFileSystem.ReadAllBytes(currentElement.CurrentFile.GetPath(), this.CurrentFileSystem.RootDirectory);
                    // Check for extension and open the appropriate formular
                    string[] spltName = readElement.Name.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    string extension = spltName[spltName.Length - 1].ToLower();
                    if (spltName.Length > 0)
                    {
                        foreach (string ex in Consts.NO_EXTENSIONS)
                        {
                            if (ex == "." + extension)
                                return;
                        }

                        if (Consts.IMAGE_EXTENSIONS.Contains<string>("." + extension))
                        {
                            Image img = null;
                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(res.Value))
                            {
                                img = Image.FromStream(ms);
                            }
                            Bitmap bmp = new Bitmap(img);

                            Form frm = new Form();
                            frm.BackgroundImage = bmp;
                            frm.BackgroundImageLayout = ImageLayout.Stretch;
                            frm.ShowDialog(this);


                            return;
                        }
                        else if (Consts.MUSIC_EXTENSIONS.Contains<string>("." + extension))
                        {

                            return;
                        }
                    }
                    // Open text file              
                    frmNotepad currentNotepad = new frmNotepad();
                    currentNotepad.Show();
                    currentNotepad.AddText(System.Text.Encoding.Default.GetString(res.Value), readElement.CurrentFile.GetPath(), this.currentFileSystem);
                }
                else
                {
                    // ToDo: Show a MessageBox here.
                }
            }
            else
                this.CurrentDirectory = currentElement.CurrentDir;
        }
     
        new public void Refresh()
        {
            // The collection is changed while the filesystem is adding files and doing refresh,
            // this is a dirty workaround with try-catch. // ToDo: Make this better
            try
            {
                this.currentControl.ClearList();

                if (this.currentDirectory == null)
                    return;

                foreach (IDirectory currentDir in this.currentDirectory.GetSubDirectories())
                    this.currentControl.Add(new Element(currentDir.GetName(), Element.Type_.Directory, currentDir));

                foreach (IFile currentFile in this.currentDirectory.GetFiles())
                    this.currentControl.Add(new Element(currentFile.GetName(), Element.Type_.File, null, currentFile));

                this.OnSideChanged?.Invoke(new Info(this.currentControl.CurrentSite, this.currentControl.CalculateSides()));
                this.OnRefresh?.Invoke();
                this.Invalidate();
            }
            catch (Exception)
            {

            }
        }
    }
}
