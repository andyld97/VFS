// ------------------------------------------------------------------------
// HeaderInfo.cs written by Code A Software (http://www.code-a-software.net)
// SP: VHP-0001 (OpenSource-Software)
// Created on:      17.12.2016
// Last update on:  17.12.2016
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VFS.ModifiedVFS
{
    /// <summary>
    /// Struct that contains and can processing path, start - and end - position
    /// </summary>
    public struct HeaderInfo
    {
        /// <summary>
        /// The position where the file starts
        /// </summary>
        public long StartPosition;

        /// <summary>
        /// The position where the file ends
        /// </summary>
        public long EndPosition;

        /// <summary>
        /// The virutal path of the file
        /// </summary>
        public string Path;

        /// <summary>
        /// Input validation if it's true something went wrong
        /// </summary>
        public bool Failed;
        
        /// <summary>
        /// Instantiates a new HeaderInfo
        /// </summary>
        /// <param name="Path">The virtual path of the file</param>
        /// <param name="StartPosition">The position where the file starts</param>
        /// <param name="EndPosition">The position where the file ends</param>
        /// <param name="Failed">Input validation</param>
        public HeaderInfo(string Path, long StartPosition, long EndPosition, bool Failed = false)
        {
            this.Path = Path;
            this.StartPosition = StartPosition;
            this.EndPosition = EndPosition;
            this.Failed = Failed;
        }

        /// <summary>
        /// Returns a HeaderInfo generated from the given string
        /// </summary>
        /// <param name="headerItem">The string which will be used to create HeaderInfo</param>
        /// <returns></returns>
        public static HeaderInfo FromString(string headerItem)
        {
            string[] elements = headerItem.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            return (elements.Length >= 2 ? new HeaderInfo(@"\" + elements[0], long.Parse(elements[1]), long.Parse(elements[2])) : new HeaderInfo() { Failed = true });
        }
    }
}
