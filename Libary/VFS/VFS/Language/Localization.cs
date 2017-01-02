// ------------------------------------------------------------------------
// Language.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      11.04.2016
// Last update on:  01.08.2016
// ------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFS.Language
{
    public class Localization
    {
        private Dictionary<string, Dictionary<int, string>> data = new Dictionary<string, Dictionary<int, string>>();
        private CultureInfo currentCulture = null;

        public const int INIT = 0x001;
        public const int ADDED_FILE = 0x002;
        public const int FILE_EXISTS = 0x003;
        public const int FILE_BYTES_EMPTY = 0x004;
        public const int ADD_FILES_TO_BYTE = 0x005;
        public const int OVERRIDE_FLAG_NOT_SET = 0x006;
        public const int PATH_NOT_EXISTS = 0x007;
        public const int RECIEVED_CHANGE = 0x008;
        public const int FILE_NOT_EXISTS = 0x009;
        public const int FILE_ERROR = 0x00A;
        public const int IO_ERROR = 0x00B;

        public static readonly int[] FAILS = new int[] { FILE_EXISTS, OVERRIDE_FLAG_NOT_SET, PATH_NOT_EXISTS, FILE_NOT_EXISTS, FILE_ERROR, IO_ERROR };
        public static readonly int[] WARNING = new int[] { INIT, FILE_BYTES_EMPTY, ADD_FILES_TO_BYTE, RECIEVED_CHANGE  };

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
