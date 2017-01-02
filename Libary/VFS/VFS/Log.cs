// ------------------------------------------------------------------------
// Log.cs written by Code A Software (http://www.seite.bplaced.net)
// All rights reserved
// Created on:      11.04.2016
// Last update on:  01.08.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFS.Language;

namespace VFS
{
    /// <summary>
    /// Represents the information for logging
    /// </summary>
    public class Log
    {
        private string logString = string.Empty;
        private string fPath = string.Empty;
        private Localization locale = new Localization();
        private Priority pri = Priority.ALL;

        /// <summary>
        /// The log priority
        /// </summary>
        public enum Priority
        {
            /// <summary>
            /// All messages
            /// </summary>
            ALL,
            /// <summary>
            /// Just fail messages
            /// </summary>
            JustFails,

            /// <summary>
            /// Just warnings
            /// </summary>
            JustWarnings
        }

        /// <summary>
        /// Initates a new log
        /// </summary>
        /// <param name="fPath">The path of the text-file</param>
        /// <param name="priorty">The log priority</param>
        public Log(string fPath, Priority priorty)
        {
            this.fPath = fPath;
            this.pri = priorty;
            this.Add(Localization.INIT, new string[] { fPath }, string.Empty);
        } 

        /// <summary>
        /// Adds an entry to the log file
        /// </summary>
        /// <param name="index">The message itself (see consts above)</param>
        /// <param name="param">Additional params for analysis</param>
        /// <param name="fail">The text of the exception - if one exists</param>
        public void Add(int index, string[] param, string fail)
        {
            if (this.pri != Priority.ALL)
            {
                switch (this.pri)
                {
                    case Priority.JustFails:
                        {
                            if (!Localization.FAILS.Contains<int>(index))
                                return;
                        }
                        break;
                    case Priority.JustWarnings:
                        {
                            if (!Localization.WARNING.Contains<int>(index))
                                return;
                        }
                        break;
                }
            }


            string finalParam = String.Join(",", param);
            string[] arr = new string[]
            {
               DateTime.Now.ToShortDateString(),
               DateTime.Now.ToLongTimeString(),
               locale.GetRessource(index),
               finalParam,
               fail
            };

            if (fail != string.Empty)
                logString += String.Format("{0} @ {1}: {2} | {3} - Info: {4}", arr) + Environment.NewLine;
            else
                logString += String.Format("{0} @ {1}: {2} | {3}{4}", arr) + Environment.NewLine;

            try
            {
                System.IO.File.WriteAllText(this.fPath, logString);
            }
            catch
            {
                // It's not well to write this to the log because they user can't get this log!
            }
        }
    }
}
