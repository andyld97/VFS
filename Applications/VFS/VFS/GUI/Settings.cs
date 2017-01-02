// ------------------------------------------------------------------------
// Settings.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      02.08.2016
// Last update on:  03.08.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VFS.GUI
{
    /// <summary>
    /// This class represents the settings for ARCHIV
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// The number of the delimitter byte
        /// </summary>
        public int PackByte = 45;

        /// <summary>
        /// The amout of delimitter bytes
        /// </summary>
        public int MainCounter = 128;

        /// <summary>
        /// Creates a new settings instance - for serialization
        /// </summary>
        public Settings()
        {

        }

        public Settings(int PackByte = 45, int MainCounter = 128)
        {
            this.PackByte = PackByte;
            this.MainCounter = MainCounter;
        }

        /// <summary>
        /// Reads the settings into the settings instance
        /// </summary>
        /// <returns>A settings instance</returns>
        public static Settings Read()
        {
            Settings current = new Settings();

            try
            {
                Serialization.Serialization<Settings> settingsReader = new Serialization.Serialization<Settings>();
                current = settingsReader.Read(System.IO.Path.Combine(Application.StartupPath, "Settings.xml"), Serialization.Serialization<Settings>.Typ.Normal);
            }
            catch (Exception)
            { }

            return current;
        }
    }
}
