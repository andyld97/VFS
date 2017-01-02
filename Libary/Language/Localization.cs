// ------------------------------------------------------------------------
// Localization.cs written by Code A Software (http://www.code-a-software.net)
// SP: VHP-0001 (OpenSource-Software)
// Created on:      11.04.2016
// Last update on:  30.12.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFS.Language
{
    /// <summary>
    /// Constitudes a possiblity to easier translate consts into different language
    /// </summary>
    public class Localization
    {
        private Dictionary<string, Dictionary<int, string>> data = new Dictionary<string, Dictionary<int, string>>();
        private CultureInfo currentCulture = null;

        /// <summary>
        /// Code: file is read and now further processes can work
        /// </summary>
        public const int INIT = 0x001;

        /// <summary>
        /// Code: File was added 
        /// </summary>
        public const int ADDED_FILE = 0x002;

        /// <summary>
        /// Code: It's just an info that the file already exists
        /// </summary>
        public const int FILE_EXISTS = 0x003;

        /// <summary>
        /// Code: File exists but is empty. Process is going on
        /// </summary>
        public const int FILE_BYTES_EMPTY = 0x004;
        
        /// <summary>
        /// Code: After override existing file
        /// </summary>
        public const int ADD_FILES_TO_BYTE = 0x005;

        /// <summary>
        /// Code: File exists and flag to override is set to false
        /// </summary>
        public const int OVERRIDE_FLAG_NOT_SET = 0x006;

        /// <summary>
        /// Code: Path is invalid
        /// </summary>
        public const int PATH_NOT_EXISTS = 0x007;

        /// <summary>
        /// Code: A change was detected
        /// </summary>
        public const int RECIEVED_CHANGE = 0x008;

        /// <summary>
        /// Code: File doesn't exists
        /// </summary>
        public const int FILE_NOT_EXISTS = 0x009;

        /// <summary>
        /// Code: File to read is in an invalid format
        /// </summary>
        public const int FILE_ERROR = 0x00A;

        /// <summary>
        /// Code: Problems with reading/writing to SSD/HDD
        /// </summary>
        public const int IO_ERROR = 0x00B;

        /// <summary>
        /// All fails gathered in an array 
        /// </summary>
        public static readonly int[] FAILS = new int[] { FILE_EXISTS, OVERRIDE_FLAG_NOT_SET, PATH_NOT_EXISTS, FILE_NOT_EXISTS, FILE_ERROR, IO_ERROR };

        /// <summary>
        /// All warnings gathered in an array
        /// </summary>
        public static readonly int[] WARNING = new int[] { INIT, FILE_BYTES_EMPTY, ADD_FILES_TO_BYTE, RECIEVED_CHANGE  };

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Localization()
        {
            this.currentCulture = CultureInfo.InstalledUICulture;

            // Define strings
            Dictionary<int, string> de = new Dictionary<int, string>();
            de.Add(INIT, "Das Dateisystem wurde initalisiert");
            de.Add(ADDED_FILE, "Eine neue Datei wurde hinzugefügt");
            de.Add(FILE_EXISTS, "Die Datei existiert bereits");
            de.Add(FILE_BYTES_EMPTY, "Die Datei existiert schon, enthält aber keinen Inhalt, deswegen wird die Datei überschrieben");
            de.Add(ADD_FILES_TO_BYTE, "Die Daten wurden zur Datei hinzufgefügt");
            de.Add(OVERRIDE_FLAG_NOT_SET, "Die vorhandene Datei wird nicht überschrieben, dazu müssen Sie overrideExistng auf true setzen. Es wurden keine Änderungen durchgeführt.");
            de.Add(PATH_NOT_EXISTS, "Der angegebene Pfad existiert nicht");
            de.Add(RECIEVED_CHANGE, "Es wurde eine Änderung am Dateisystem vorgenommen");
            de.Add(FILE_NOT_EXISTS, "Die Datei konnte nicht gefunden werden");
            de.Add(FILE_ERROR, "Die Datei ist ungültig");
            de.Add(IO_ERROR, "Es ist ein Dateifehler aufgetreten!");

            Dictionary<int, string> en = new Dictionary<int, string>();
            en.Add(INIT, "The file system was initiated");
            en.Add(ADDED_FILE, "A new file was added");
            en.Add(FILE_EXISTS, "File is existing already");
            en.Add(FILE_BYTES_EMPTY, "Adding bytes to file. Previous file doesn't contain any bytes");
            en.Add(ADD_FILES_TO_BYTE, "Data was added to file");
            en.Add(OVERRIDE_FLAG_NOT_SET, "Flag overrideExistng is not set. So nothing happens to the file");
            en.Add(PATH_NOT_EXISTS, "Path doesn't exists");
            en.Add(RECIEVED_CHANGE, "A change in the filesystem was detected");
            en.Add(FILE_NOT_EXISTS, "The file doesn't exists");
            en.Add(FILE_ERROR, "This file is invalid");
            en.Add(IO_ERROR, "Reading/Writing errors are occuring");

            data.Add("de-DE", de);
            data.Add("en-US", en);
            data.Add("en-GB", en);

        }

        /// <summary>
        /// Translates a code into the right language - dependend of your culture
        /// </summary>
        /// <param name="index">The code</param>
        /// <returns></returns>
        public string GetRessource(int index)
        {
            // Retrive 1 from system langauage.
            switch (this.currentCulture.Name)
            {
                case "de-DE":
                    {
                        return data[this.currentCulture.Name][index];
                    }
                    break;
                case "en-US":
                case "en-GB":
                    {
                        return data[this.currentCulture.Name][index];
                    }
                    break;

            }
            return string.Empty;
        }
    }
}
