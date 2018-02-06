// ------------------------------------------------------------------------
// Extensions.cs written by Code A Software (http://www.code-a-software.net)
// Created on:      11.04.2016
// Last update on:  23.11.2017
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFS.Helpers
{
    /// <summary>
    /// Calculates the appropriate length of bytes
    /// </summary>
    public class ConvertLength
    {
        /// <summary>
        /// Unit prefix
        /// </summary>
        public enum Type_
        {
            /// <summary>
            /// Bytes
            /// </summary>
            B = 0,

            /// <summary>
            /// KiloBytes
            /// </summary>
            KB = 1,

            /// <summary>
            /// MegaBytes
            /// </summary>
            MB = 2,

            /// <summary>
            /// GigaBytes
            /// </summary>
            GB = 3,

            /// <summary>
            /// TerraBytes
            /// </summary>
            TB = 4
        }

        /// <summary>
        /// Represents a result of a calculation
        /// </summary>
        public struct Item
        {
            /// <summary>
            /// Final calculated value
            /// </summary>
            public double Length;

            /// <summary>
            /// Final calculated unit prefix
            /// </summary>
            public Type_ Type;

            /// <summary>
            /// Instantiates a new result
            /// </summary>
            /// <param name="Length">Final calculated value</param>
            /// <param name="Type">Final calculated unit prefix</param>
            public Item(double Length, Type_ Type)
            {
                this.Length = Length;
                this.Type = Type;
            }

            /// <summary>
            /// Returns a formatted string with length and unit prefix
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return this.Length + " " + this.Type.ToString();
            }
        }

        /// <summary>
        /// Converts the value into the right unit prefix
        /// </summary>
        /// <param name="value">The length in bytes</param>
        /// <returns></returns>
        public static Item Calculate(double value)
        {
            // Get right unit prefix
            int index = 0;
            double nValue = value;

            while (nValue > 1024.0)
            {
                nValue /= 1024.0;
                index++;
            }

            return new Item(Math.Round(value / Math.Pow(1024, index), 2), (Type_)index);
        }

        /// <summary>
        /// Converts the value into the right unit prefix
        /// </summary>
        /// <param name="source">Converts a result into a special unit prefix</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Item Calculate(Item source, Type_ type)
        {
            // Calculate difference:
            int difference = (int)source.Type - (int)type;
            return new Item(Math.Round(difference < 0 ? source.Length / Math.Pow(1024, (int)Math.Abs(difference)) : source.Length * Math.Pow(1024, (int)Math.Abs(difference)), 2), type);
        }
    }
}
