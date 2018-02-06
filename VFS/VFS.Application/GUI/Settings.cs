// ------------------------------------------------------------------------
// Settings.cs written by Code A Software (http://www.code-a-software.net)
// All rights reserved
// Created on:      02.08.2016
// Last update on:  28.10.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VFS.Application.GUI
{
    /// <summary>
    /// This class represents the settings for VFS
    /// </summary>
    public class Settings
    {
        private string workspaceDir = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Workspace");
        private static string settingsPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Settings.xml");

        /// <summary>
        /// This is to avoid reading the file often from disk
        /// </summary>
        private static Settings lastInstance = null;

        /// <summary>
        /// The number of the delimitter byte
        /// </summary>
        public int PackByte = 45;

        /// <summary>
        /// The amout of delimitter bytes
        /// </summary>
        public int MainCounter = 128;

        /// <summary>
        /// Size of the buffer
        /// </summary>
        public long BufferSize = 32768;

        /// <summary>
        /// The workspace directroy for ModifiedVFS
        /// </summary>
        public string WorkspacePath
        {
            get
            {
                return this.workspaceDir;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    this.workspaceDir = value;
            }
        }

        /// <summary>
        /// Creates a new settings instance - for serialization
        /// </summary>
        public Settings()
        {

        }

        /// <summary>
        /// Creates a new settings instance
        /// </summary>
        /// <param name="PackByte"></param>
        /// <param name="MainCounter"></param>
        /// <param name="BufferSize"></param>
        public Settings(int PackByte = 45, int MainCounter = 128, long BufferSize = 32768)
        {
            this.PackByte = PackByte;
            this.MainCounter = MainCounter;
            this.BufferSize = BufferSize;
        }

        /// <summary>
        /// Reads the settings into the settings instance
        /// </summary>
        /// <returns>A settings instance</returns>
        public static Settings Read()
        {
            Settings currentSettingsInstance = null;

            try
            {
                currentSettingsInstance = Serialization.Serialization.Read<Settings>(settingsPath, Serialization.Serialization.Mode.Normal);
            }
            catch (Exception)
            {
            }

            if (currentSettingsInstance == null)
            {
                currentSettingsInstance = new Settings();
                currentSettingsInstance.Save();
            }

            if (lastInstance == null || lastInstance != currentSettingsInstance)
                lastInstance = currentSettingsInstance;

            return currentSettingsInstance;
        }

        public bool Save()
        {
            try
            {
                Serialization.Serialization.Save<Settings>(settingsPath, this, Serialization.Serialization.Mode.Normal, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
