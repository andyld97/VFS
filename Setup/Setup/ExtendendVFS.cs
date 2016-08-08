using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFS;
using VFS.Language;
using System.Windows.Forms;

namespace Setup
{
    public class ExtendendVFS : VFS.VFS
    {
        public delegate void recieveMessage(string message);
        public event recieveMessage RecieveMessage;
        
        public ExtendendVFS(string path) : base(string.Empty, path, 128, 45)
        {
            // 128 = MainCounter => Change this value if it's necessary.
            // 45 = PackByte     => Change this value if it's necessary.
        }

        private void sendMessage(string message)
        {
            string nMessage = DateTime.Now.ToShortTimeString() + ": " + message;
            if (this.RecieveMessage != null)
                this.RecieveMessage(nMessage);
        }


        public override bool Extract(string filePath)
        {
            //return base.Extract(filePath);
            if (System.IO.Directory.Exists(filePath))
            {
                // Extract now.
                // Create directories
                Action<Directory> passDirs = null;

                passDirs = new Action<Directory>((Directory dir) => {

                    foreach (Directory currentDir in dir.SubDirs)
                    {
                        string path = System.IO.Path.Combine(filePath, this.FormatPath(currentDir.ToFullPath()));
                        try
                        {
                            System.IO.Directory.CreateDirectory(path);
                            this.sendMessage("Ordner wurde erstellt: " + path);
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
                foreach (VFS.File currentFile in this.rootDir.Files)
                {
                    string path = System.IO.Path.Combine(filePath, this.FormatPath(currentFile.Path));
                    try
                    {
                        System.IO.File.WriteAllBytes(path, currentFile.Bytes.ToArray());
                        this.sendMessage("Datei wurde kopiert: " + path);
                    }
                    catch (Exception e)
                    {
                        this.lgInstance.Add(Localization.IO_ERROR, new string[] { "FILE Path: " + path }, e.Message);
                    }
                }

                Action<Directory> passFiles = null;

                passFiles = new Action<Directory>((Directory dir) => {
                    foreach (Directory currentDir in dir.SubDirs)
                    {
                        foreach (File currentFile in currentDir.Files)
                        {
                            string path = System.IO.Path.Combine(filePath, this.FormatPath(currentFile.Path));
                            try
                            {
                                System.IO.File.WriteAllBytes(path, currentFile.Bytes.ToArray());
                                this.sendMessage("Datei wurde kopiert: " + path);
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
    }
}
